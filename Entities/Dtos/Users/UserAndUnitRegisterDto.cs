using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserAndUnitRegisterDto :IDto
    {
      public UserForRegister UserForRegister { get; set; }
       public  int UnitId { get; set; }
    }
}
