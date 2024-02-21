using Core.DataAccsess.EntityFramework;
using DataAccess.Abstract;
using DataAccsess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAgentaDal :EfEntityRepositoryBase<Agenta,ContextDb>, IAgentaDal
    {
    }
}
