using Core.DataAccsess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccsess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos.Stations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStationDal : EfEntityRepositoryBase<Station, ContextDb>, IStationDal
    {
        public void AddRange(List<Station> stations)
        {
            using (var context = new ContextDb())
            {
                context.Set<Station>().AddRange(stations);
                context.SaveChanges();
            }
        }
    }
}
