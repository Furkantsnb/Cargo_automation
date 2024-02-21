﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Mails
{
    public class UpdateMailParameterDto:IDto
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string Email { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string Password { get; set; }
    }
}
