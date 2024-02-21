using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Enums;
using Entities.Dtos.Lines;
using System.Linq;

namespace Business.Concrete
{
    public class LineManager : ILineService
    {
        private readonly ILineDal _lineDal;
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;
        private readonly IStationDal _stationDal;
        private readonly IAgentaDal _agentaDal;
        private readonly ITransferCenterDal _transferCenterDal;
        private readonly IUnitDal _unitDal;

        public LineManager(ILineDal lineDal, IStationService stationService, IMapper mapper, IStationDal stationDal, ITransferCenterDal transferCenterDal, IAgentaDal agentaDal, IUnitDal unitDal)
        {
            _lineDal = lineDal;
            _stationService = stationService;
            _mapper = mapper;
            _stationDal = stationDal;
            _transferCenterDal = transferCenterDal;
            _agentaDal = agentaDal;
            _unitDal = unitDal;
        }
        [ValidationAspect(typeof(CreateLineDtoValidator))]
        public IResult Add(CreateLineDto createLineDto)
        {
          
                var result = _transferCenterDal.Get(p => p.UnitId == createLineDto.TransferCenterId);
                if (result == null)
                {
                    return new ErrorDataResult<TransferCenter>(Messages.TransferCenterNotFound);
                }
            

            // Aynı hatın veri tabanında daha önce eklenip eklenmediğini kontrol et
            var existingLine = _lineDal.Get(l => l.LineName == createLineDto.LineName && l.LineType == createLineDto.LineType);
            if (existingLine != null)
            {
                return new ErrorDataResult("Bu hat zaten eklenmiş.");
            }

            // Girilen istasyonlar veri tabanında mevcut mu kontrol et
            foreach (var stationId in createLineDto.Stations)
            {
                var existingUnit = _unitDal.Get(u => u.UnitId == stationId);
                if (existingUnit == null || createLineDto.TransferCenterId == stationId)
                {
                    return new ErrorDataResult($"Girilen istasyonlar arasında fazla ya da geçersiz bir Station bulundu  : {stationId}");
                }
            }

            // AraHat için kontrol
            if (createLineDto.LineType == LineType.AraHat)
            {
                // İlk durak transferCenter olmalı
               
                if (createLineDto.TransferCenterId == null && createLineDto.Stations.FirstOrDefault() != createLineDto.TransferCenterId)
                {
                    return new ErrorDataResult("Ara hat için ilk durak transfer merkezi olmalıdır.");
                }

              
                // Diğer duraklar sadece agentalardan oluşmalı
                foreach (var stationId in createLineDto.Stations.Skip(1))
                {
                    var unit = _agentaDal.Get(u => u.UnitId == stationId);
                    if (unit == null || unit is not Agenta)
                    {
                        return new ErrorDataResult("Ara hat için diğer duraklar sadece acentalardan oluşmalıdır.");
                    }
                }
            }
            else if (createLineDto.LineType == LineType.AnaHat)
            {
                // Tüm duraklar sadece TransferCenter olmalı
                foreach (var stationId in createLineDto.Stations)
                {
                    var unit = _transferCenterDal.Get(u => u.UnitId == stationId);
                    if (unit == null || unit is not TransferCenter)
                    {
                        return new ErrorDataResult("Ana hat için tüm duraklar bir transfer merkezi olmalıdır.");
                    }
                }
            }

            Line line = _mapper.Map<Line>(createLineDto);

            _lineDal.Add(line);

            var stations = GenerateStations(createLineDto, line);
            _stationService.AddRange(stations);

            return new SuccessResult(Messages.AddLine);
        }


        private List<Station> GenerateStations(CreateLineDto createLineDto, Line line)
        {
            var stations = new List<Station>();

            if (createLineDto.TransferCenterId != 0)
            {
                var transferStation = CreateTransferStation(createLineDto, line);
                stations.Add(transferStation);
            }

            for (int i = 0; i < createLineDto.Stations.Count; i++)
            {
                var stationUnitId = createLineDto.Stations[i];
                var station = CreateStation(line, stationUnitId, i + 1);
                stations.Add(station);
            }

            return stations;
        }

        private Station CreateTransferStation(CreateLineDto createLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.LineId,
                IsActive = true,
                UnitId = createLineDto.TransferCenterId
            };
        }

        private Station CreateStation(Line line, int stationUnitId, int orderNumber)
        {
            return new Station
            {
                StationName = $"{line.LineName} Durak{orderNumber}",
                OrderNumber = orderNumber + 1,
                LineId = line.LineId,
                IsActive = true,
                UnitId = stationUnitId
            };
        }

        public IResult Delete(int id)
        {
            // Hat bilgisini getir
            var lineToDelete = _lineDal.Get(l => l.LineId == id);

            // Hat bulunamazsa hata döndür
            if (lineToDelete == null)
            {
                return new ErrorDataResult("Hat bulunamadı");
            }

            // Hat silinirken, o hatta bağlı tüm istasyonları getir
            var stationsToDelete = _stationDal.GetList(s => s.LineId == id);

            // Her bir istasyon için silme işlemi yap
            foreach (var station in stationsToDelete)
            {
                _stationDal.Delete(station);
            }

            // Hatı sil
            _lineDal.Delete(lineToDelete);

            return new SuccessResult("Hat ve bağlı istasyonlar başarıyla silindi");
        }

        public IDataResult<Line> GetById(int id)
        {
            var line = _lineDal.Get(l => l.LineId == id);
            if (line != null)
            {
                return new SuccesDataResult<Line>(line);
            }
            return new ErrorDataResult<Line>("Hat bulunamadı");
        }

        public IDataResult<List<Line>> GetList()
        {
            var lines = _lineDal.GetList();
            return new SuccesDataResult<List<Line>>(lines);
        }

        [ValidationAspect(typeof(UpdateLineDtoValidator))]
        public IResult Update(UpdateLineDto updateLineDto)
        {
           
            var lineToUpdate = _lineDal.Get(l => l.LineId == updateLineDto.LineId);
           
            if (lineToUpdate == null)
            {
                return new ErrorDataResult("Hat bulunamadı");
            }

            // TransferCenterId kontrolü
                var transferCenter = _transferCenterDal.Get(u => u.UnitId == updateLineDto.TransferCenterId);
                if (transferCenter == null)
                {
                    return new ErrorDataResult("Geçersiz transfer merkezi IDsi");
                }
           
            // Girilen istasyonlar veri tabanında mevcut mu kontrol et
            foreach (var stationId in updateLineDto.Stations)
            {
                var existingUnit = _unitDal.Get(u => u.UnitId == stationId);
                if (existingUnit == null || updateLineDto.TransferCenterId == stationId)
                {
                    return new ErrorDataResult($"Girilen istasyonlar arasında fazla ya da geçersiz bir Station bulundu  : {stationId}");
                }
                

            }
            // Aynı hatın veri tabanında daha önce eklenip eklenmediğini kontrol et
            var existingLine = _lineDal.Get(l => l.LineName == updateLineDto.LineName && l.LineType == updateLineDto.LineType);
            if (existingLine != null )
            {
                return new ErrorDataResult("Bu hat zaten eklenmiş.");
            }
            // AraHat için kontrol
            if (updateLineDto.LineType == LineType.AraHat)
            {
                // İlk durak transferCenter olmalı

                if (updateLineDto.TransferCenterId == null && updateLineDto.Stations.FirstOrDefault() != updateLineDto.TransferCenterId)
                {
                    return new ErrorDataResult("Ara hat için ilk durak transfer merkezi olmalıdır.");
                }


                // Diğer duraklar sadece agentalardan oluşmalı
                foreach (var stationId in updateLineDto.Stations.Skip(1))
                {
                    var unit = _agentaDal.Get(u => u.UnitId == stationId);
                    if (unit == null || unit is not Agenta)
                    {
                        return new ErrorDataResult("Ara hat için diğer duraklar sadece acentalardan oluşmalıdır.");
                    }
                }
            }
            else if (updateLineDto.LineType == LineType.AnaHat)
            {
                // Tüm duraklar sadece TransferCenter olmalı
                foreach (var stationId in updateLineDto.Stations)
                {
                    var unit = _transferCenterDal.Get(u => u.UnitId == stationId);
                    if (unit == null || unit is not TransferCenter)
                    {
                        return new ErrorDataResult("Ana hat için tüm duraklar bir transfer merkezi olmalıdır.");
                    }
                }
            }

            // Update line properties
            lineToUpdate.LineName = updateLineDto.LineName;
            // Update other properties as needed
            Line line = _mapper.Map<Line>(updateLineDto);
            // Update line in database
            _lineDal.Update(line);

            // Delete existing stations for this line
            var existingStations = _stationDal.GetList(s => s.LineId == updateLineDto.LineId);
            foreach (var station in existingStations)
            {
                _stationDal.Delete(station);
            }

            // Generate and add new stations
            var newStations = GenerateStationss(updateLineDto, lineToUpdate);
            _stationService.AddRange(newStations);

            return new SuccessResult("Line Güncellendi");
        }

        private List<Station> GenerateStationss(UpdateLineDto updateLineDto, Line line)
        {
            var stations = new List<Station>();

            if (updateLineDto.TransferCenterId != null)
            {
                var transferStation = CreateTransferStations(updateLineDto, line);
                stations.Add(transferStation);
            }

            for (int i = 0; i < updateLineDto.Stations.Count; i++)
            {
               
                    var stationUnitId = updateLineDto.Stations[i];
                var station = CreateStation(line, stationUnitId, i + 1);
                stations.Add(station);
            }

            return stations;
        }
        private Station CreateTransferStations(UpdateLineDto updateLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.LineId,
                IsActive = true,
                UnitId = updateLineDto.TransferCenterId.Value
            };
        }
    }
}

