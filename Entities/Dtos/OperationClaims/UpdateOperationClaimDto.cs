﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.OperationClaims
{
    public class UpdateOperationClaimDto :IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}