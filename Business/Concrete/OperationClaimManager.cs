using AutoMapper;
using Business.Abstract;

using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccsess.Abstract;
using Entities.Concrete;
using Entities.Dtos.OperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager :IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IMapper _mapper;
        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CreateOperationClaimDtoValidator))]
        //[SecuredOperation("Admin")]
        public IResult Add(CreateOperationClaimDto operationClaimDto)
        {
            OperationClaim operationClaim = _mapper.Map<OperationClaim>(operationClaimDto);
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.AddedOperationClaim);
        }
    
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.DeletedOperationClaim);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccesDataResult<OperationClaim>(_operationClaimDal.Get(i=>i.Id==id));
        }
       
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccesDataResult<List<OperationClaim>>(_operationClaimDal.GetList());
        }

        [ValidationAspect(typeof(UpdateOperationClaimDtoValidator))]
        public IResult Update(UpdateOperationClaimDto operationClaimDto)
        {
            OperationClaim operationClaim = _mapper.Map<OperationClaim>(operationClaimDto);
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.UpdatedOperationClaim);
        }
    }
}
