﻿using Entities.Dtos.OperationClaims;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UpdateOperationClaimDtoValidator : AbstractValidator<UpdateOperationClaimDto>
    {
        public UpdateOperationClaimDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

        }
    }
}
