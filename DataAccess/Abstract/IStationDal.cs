using Core.DataAccsess;
using Entities.Concrete;
using Entities.Dtos.Stations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStationDal : IEntityRepository<Station>
    {
        void AddRange(List<Station> stations); // AddRange metodu eklendi
    }
}
