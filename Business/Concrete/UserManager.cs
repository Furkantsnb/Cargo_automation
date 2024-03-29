﻿using Business.Abstract;

using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccsess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
           

            _userDal.Add(user);
        }
      
        public List<OperationClaim> GetClaims(User user,int unitId)
        {
            return _userDal.GetClaims(user,unitId);
        }
        [CacheAspect(60)]
        public User GetByMail(string email)
        {
           return _userDal.Get(p=>p.Email== email);
        }
        [CacheAspect(60)]
        public User GetByMailConfirmValue(string value)
        {
            return _userDal.Get(p=>p.MailConfirmValue== value);
        }

        [PerformanceAspect(2)]
        [CacheRemoveAspect("IUserService.Get")]
        public void Update(User user)
        {
           _userDal.Update(user);
        }
        [CacheAspect(60)]
        public User GetById(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }
    }
}
