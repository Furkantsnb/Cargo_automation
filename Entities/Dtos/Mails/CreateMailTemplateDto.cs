﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Mails
{
    public class CreateMailTemplateDto :IDto
    {
        public int UnitId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
