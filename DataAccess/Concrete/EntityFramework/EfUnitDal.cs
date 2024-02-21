using Core.DataAccsess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccsess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUnitDal : EfEntityRepositoryBase<Unit, ContextDb>, IUnitDal
    {
        public UserUnit GetUnit(int userId)
        {
            using (var context = new ContextDb())
            {
                var result = context.UserUnits.Where(p => p.UserId == userId).FirstOrDefault();
                return result;
            }
        }

        public void UserUnitAdd(int userId, int unitId)
        {
            using (var context = new ContextDb())
            {
                UserUnit userUnit = new UserUnit()
                {
                    UserId = userId,
                    UnitId = unitId,
                    AddedAt = DateTime.Now,
                    IsActive = true,
                };
                context.UserUnits.Add(userUnit);
                context.SaveChanges();
            }
        }
    }
}
