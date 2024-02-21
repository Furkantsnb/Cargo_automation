using Core.Entities;
using Entities.Concrete.Enums;
using Entities.Dtos.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lines
{
    public class LineDto :IDto
    {
        public int LineId { get; set; }
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public int? TransferCenterId { get; set; }
        public List<StationDto> Stations { get; set; }
    }
}
