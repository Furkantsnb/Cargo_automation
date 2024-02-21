using Business.Abstract;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UnitManager : IUnitService
    {
        private readonly IUnitDal _unitDal;
        public UnitManager(IUnitDal unitDal)
        {
            _unitDal = unitDal;
        }
       

        [CacheAspect(60)]
        public IDataResult<UserUnit> GetUnit(int userId)
        {
            return new SuccesDataResult<UserUnit>(_unitDal.GetUnit(userId));
        }

        [CacheRemoveAspect("IUnitService.Get")]
        public IResult UserUnitAdd(int userId, int unitId)
        {
            _unitDal.UserUnitAdd(userId, unitId);
            return new SuccessResult();
        }
    }
}
