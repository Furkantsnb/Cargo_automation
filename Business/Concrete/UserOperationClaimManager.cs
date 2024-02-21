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
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IMapper _mapper;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CreateUserOperationClaimDtoValidator))]
        public IResult Add(CreateUserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }
  
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.DeletedUserOperationClaim);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccesDataResult<UserOperationClaim>(_userOperationClaimDal.Get(i=>i.Id== id));  
        }

        public IDataResult<List<UserOperationClaim>> GetList(int userId,int unitId)
        {
            return new SuccesDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetList(p=>p.UserId==userId && p.UnitId==unitId));
        }
        [ValidationAspect(typeof(UpdateUserOperationClaimDtoValidator))]
        public IResult Update(UpdateUserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }
    }
}
