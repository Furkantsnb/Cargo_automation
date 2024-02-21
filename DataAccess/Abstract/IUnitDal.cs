using Core.DataAccsess;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitDal : IEntityRepository<Unit>
    {
        void UserUnitAdd(int userId, int unitId);
        UserUnit GetUnit(int userId);
    }
}
