using Core.Entities;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lines
{
    public class UpdateLineDto : IDto
    {

        public int LineId { get; set; }
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; }
        public int? TransferCenterId { get; set; }
        public List<int> Stations { get; set; }
    }
}
