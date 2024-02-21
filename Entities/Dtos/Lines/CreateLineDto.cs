using Core.Entities;
using Entities.Concrete;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Lines
{
    public class CreateLineDto : IDto
    {
        public string LineName { get; set; }
        public bool IsActive { get; set; } = true;
        public LineType LineType { get; set; } // Enum tipi olarak tanımlanmış LineType
        public int? TransferCenterId { get; set; } //hattın ana hat olması durumunda başlangıç duragı.

        public List<int> Stations { get; set; }
    }
}
