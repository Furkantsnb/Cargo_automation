using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Abstract
{
    public interface IUnitService
    {
       
        IResult UserUnitAdd(int userId, int unitId);
        IDataResult<UserUnit> GetUnit(int userId);
    }
}
