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
using Entities.Dtos.TransferCenter;

namespace Business.Concrete
{
    public class TransferCenterManager : ITransferCenterService
    {
        private readonly ITransferCenterDal _transferCenterDal;
        private readonly IAgentaDal _agentaDal;
        private readonly IMapper _mapper;
        private readonly IStationDal _stationDal;

        public TransferCenterManager(ITransferCenterDal transferCenterDal, IAgentaDal agentaDal, IMapper mapper, IStationDal stationDal )
        {
            _transferCenterDal = transferCenterDal;
            _agentaDal = agentaDal;
            _mapper = mapper;
            _stationDal = stationDal;
        }
        [ValidationAspect(typeof(TransferCenterValidator))]
        public IResult Add(CreateTransferCenterDto transferCenterDto)
        {
            TransferCenter transferCenter = _mapper.Map<TransferCenter>(transferCenterDto);
            var result =TransferCenterExists(transferCenter);
            if(result == null) 
            {
                return new SuccessResult(Messages.TransferCenterAlreadyExists);
            }
            _transferCenterDal.Add(transferCenter);
            return new SuccessResult(Messages.AddedTransferCenterDal);
        }


        public IResult TransferCenterExists(TransferCenter transferCenter)
        {
            var result = _transferCenterDal.Get(a => a.UnitName == transferCenter.UnitName && a.ManagerName == transferCenter.ManagerName && a.ManagerSurname == transferCenter.ManagerSurname);
            if (result != null)
            {
                return new ErrorDataResult(Messages.TransferCenterAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Agenta>> GetAgentasByTransferCenterId(int transferCenterId)
        {
           
            var agenta = _agentaDal.GetList(a => a.TransferCenterId == transferCenterId);
            // Agentalar varsa ve listesi boş değilse, bu listeyi IDataResult içinde döndür
            if (agenta != null)
            {
                return new SuccesDataResult<List<Agenta>>(agenta);
            }

            // Agentalar yoksa veya listesi boşsa, bu durumu IDataResult içinde belirt
            return new ErrorDataResult<List<Agenta>>(Messages.AgentaNotFoundForTransferCenter);
        }

        public IDataResult<TransferCenter> GetById(int id)
        {
            var result = _transferCenterDal.Get(p => p.UnitId == id);
            if (result == null)
            {
                return new ErrorDataResult<TransferCenter>(Messages.TransferCenterNotFound);
            }
            return new SuccesDataResult<TransferCenter>(result);
        }

        public IDataResult<List<TransferCenter>> GetList()
        {
            return new SuccesDataResult<List<TransferCenter>>(_transferCenterDal.GetList());
        }
        [ValidationAspect(typeof(UpdateTransferCenterValidator))]
        public IResult Update(UpdateTransferCenterDto transferCenterDto)
        {
            TransferCenter transferCenter = _mapper.Map<TransferCenter>(transferCenterDto);
            var result = _transferCenterDal.Get(p => p.UnitId == transferCenter.UnitId);
            if (result == null)
            {
                return new ErrorDataResult<TransferCenter>(Messages.TransferCenterNotFound);
            }
            _transferCenterDal.Update(transferCenter);
            return new SuccessResult(Messages.UpdatedTransferCenter);
        }


        public IResult Delete(int id)
        {
            var TransferCenterToDelete = _transferCenterDal.GetById(id);
            if (TransferCenterToDelete == null)
            {
                return new ErrorDataResult(Messages.TransferCenterNotFound);
            }
            // Eğer TransferCenter  bağlı bir istasyon varsa istasyonu sil
            var relatedStation = _stationDal.Get(s => s.UnitId == id);
            if (relatedStation != null)
            {
                _stationDal.Delete(relatedStation);
            }

            _transferCenterDal.Delete(TransferCenterToDelete);
            return new SuccessResult(Messages.DeletedTransferCenter);
        }

        public IResult SoftDelete(int id)
        {
            var result = _transferCenterDal.GetById(id);
            if (result == null)
            {
                return new ErrorDataResult(Messages.TransferCenterNotFound);
            }
            result.IsDeleted = true;
            _transferCenterDal.Update(result);
            return new SuccessResult(Messages.SoftDeleteTransferCenter);
        }
    }
}

