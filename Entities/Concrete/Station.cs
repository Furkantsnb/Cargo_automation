using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Station :IEntity
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int LineId { get; set; }
        public Line Line { get; set; }
        


    }
}
