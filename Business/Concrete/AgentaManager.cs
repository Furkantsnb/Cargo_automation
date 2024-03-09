using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos.Agentas;
using log4net.Core;

namespace Business.Concrete
{
    public class AgentaManager : IAgentaService
    {
        private readonly IAgentaDal _agentaDal;
        private readonly ITransferCenterDal _transferCenterDal;
        private readonly IMapper _mapper;
        private readonly IStationDal _stationDal;



        public AgentaManager(IAgentaDal agentaDal, ITransferCenterDal transferCenterDal, IMapper mapper, IStationDal stationDal)
        {
            _agentaDal = agentaDal;
            _transferCenterDal = transferCenterDal;
            _mapper = mapper;
            _stationDal = stationDal;
        }

        [ValidationAspect(typeof(CreateAgentaDtoValidator))]
        public IResult Add(CreateAgentaDto agentaDto)
        {
            Agenta agenta = _mapper.Map<Agenta>(agentaDto);
            var result = _agentaDal.Get(a => a.UnitName == agenta.UnitName);
            if (result != null)
            {
                return new SuccessResult(Messages.AgentaAlreadyExists);

            }

            var getCenter = _transferCenterDal.Get(a => a.UnitId == agenta.TransferCenterId);
            if (getCenter == null)
            {
                return new SuccessResult("Transfer Center Id Değerini Yanlış Girdiniz...");
            }
            _agentaDal.Add(agenta);
            return new SuccessResult(Messages.AddedAgenta);
        }

        //&& a.ManagerName == agenta.ManagerName && a.ManagerSurname ==agenta.ManagerSurname
        public IResult AgentaExists(Agenta agenta)
        {
            var result = _agentaDal.Get(a => a.UnitName == agenta.UnitName);
            if (result != null)
            {
                return new ErrorDataResult(Messages.AgentaAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            // Agentayı al
            var agentaToDelete = _agentaDal.GetById(id);
            if (agentaToDelete == null)
            {
                return new ErrorDataResult(Messages.AgentaNotFound);
            }

            // Eğer agentaya bağlı bir istasyon varsa istasyonu sil
            var relatedStation = _stationDal.Get(s=>s.UnitId ==id);
            if (relatedStation != null)
            {
                _stationDal.Delete(relatedStation);
            }

            // Agentayı sil
            _agentaDal.Delete(agentaToDelete);

            return new SuccessResult(Messages.DeletedAgenta);
        }

        public IDataResult<Agenta> GetById(int id)
        {
            var result = _agentaDal.GetById(id);
            if (result == null)
            {
                return new ErrorDataResult<Agenta>(Messages.AgentaNotFound);
            }
            return new SuccesDataResult<Agenta>(result);
        }

        public IDataResult<List<Agenta>> GetList()
        {
            return new SuccesDataResult<List<Agenta>>(_agentaDal.GetList());
        }

        public IResult SoftDelete(int id)
        {

            var result = _agentaDal.GetById(id);
            if (result == null)
            {
                return new ErrorDataResult(Messages.AgentaNotFound);
            }
            result.IsDeleted = true;
            _agentaDal.Update(result);
            return new SuccessResult(Messages.SoftDeleteAgenta);

        }

        [ValidationAspect(typeof(UpdateAgentaDtoValidator))]
        public IResult Update(UpdateAgentaDto agentaDto)
        {
            Agenta agenta = _mapper.Map<Agenta>(agentaDto);
            var result = _agentaDal.Get(p => p.UnitId == agenta.UnitId);
            if (result == null)
            {
                return new ErrorDataResult<Agenta>(Messages.AgentaNotFound);
            }
            _agentaDal.Update(agenta);
            return new SuccessResult(Messages.UpdatedAgenta);
        }
    }
}
