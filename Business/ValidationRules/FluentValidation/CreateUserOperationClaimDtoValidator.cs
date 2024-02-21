using Entities.Dtos.OperationClaims;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CreateUserOperationClaimDtoValidator : AbstractValidator<CreateUserOperationClaimDto>
    {
        public CreateUserOperationClaimDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id boş olamaz.")
                .GreaterThan(0).WithMessage("Kullanıcı Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.OperationClaimId)
                .NotEmpty().WithMessage("İşlem yetkisi Id boş olamaz.")
                .GreaterThan(0).WithMessage("İşlem yetkisi Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Birim Id boş olamaz.")
                .GreaterThan(0).WithMessage("Birim Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.AddedAt)
                .NotEmpty().WithMessage("Ekleme tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ekleme tarihi bugünden büyük veya bugüne eşit olmalıdır.");
        }
    }
}
