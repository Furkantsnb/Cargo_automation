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
using Entities.Dtos.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Concrete
{
    public class StationManager : IStationService
    {
        private readonly IStationDal _stationDal;
        private readonly IMapper _mapper;

        public StationManager(IStationDal stationDal, IMapper mapper)
        {
            _stationDal = stationDal;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(CreateStationDtoValidator))]
        public IResult Add(CreateStationDto createStationDto)
        {
            var station = _mapper.Map<Station>(createStationDto);
            _stationDal.Add(station);
            return new SuccessResult("İstasyon başarıyla eklendi.");
        }

        public IResult AddRange(List<Station> stations)
        {
            var stationss = _mapper.Map<List<Station>>(stations);
            _stationDal.AddRange(stationss);
            return new SuccessResult("İstasyonlar başarıyla eklendi.");
        }

        [ValidationAspect(typeof(UpdateStationDtoValidator))]
        public IResult Update(UpdateStationDto updateStationDto)
        {
            var station = _mapper.Map<Station>(updateStationDto);

            // Güncellenecek istasyonun varlığını kontrol et
            var existingStation = _stationDal.Get(s => s.StationId == updateStationDto.StationId);
            if (existingStation == null)
            {
                return new ErrorDataResult("Güncellenmek istenen istasyon bulunamadı.");
            }
            _stationDal.Update(station);
            return new SuccessResult("İstasyon başarıyla güncellendi.");
        }

        public IResult Delete(int stationId)
        {
            var stationToDelete = _stationDal.Get(s => s.StationId == stationId);
            if (stationToDelete == null)
            {
                return new ErrorDataResult("Belirtilen istasyon bulunamadı.");
            }
            _stationDal.Delete(stationToDelete);
            return new SuccessResult("İstasyon başarıyla silindi.");
        }

        public IDataResult<StationDto> GetById(int stationId)
        {
            var station = _stationDal.Get(s => s.StationId == stationId);
            if (station == null)
            {
                return new ErrorDataResult<StationDto>("Belirtilen istasyon bulunamadı.");
            }
            var stationDto = _mapper.Map<StationDto>(station);
            return new SuccesDataResult<StationDto>(stationDto);
        }

        public IDataResult<List<StationDto>> GetList()
        {
            var stations = _stationDal.GetList();
            var stationDtos = _mapper.Map<List<StationDto>>(stations);
            return new SuccesDataResult<List<StationDto>>(stationDtos);
        }

       

    }
}
