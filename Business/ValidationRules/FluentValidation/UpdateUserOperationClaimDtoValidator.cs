using Entities.Dtos.OperationClaims;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UpdateUserOperationClaimDtoValidator : AbstractValidator<UpdateUserOperationClaimDto>
    {
        public UpdateUserOperationClaimDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id boş olamaz.")
                .GreaterThan(0).WithMessage("Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id boş olamaz.")
                .GreaterThan(0).WithMessage("Kullanıcı Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.OperationClaimId)
                .NotEmpty().WithMessage("İşlem yetkisi Id boş olamaz.")
                .GreaterThan(0).WithMessage("İşlem yetkisi Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Birim Id boş olamaz.")
                .GreaterThan(0).WithMessage("Birim Id pozitif bir değer olmalıdır.");

        }
    }
}
