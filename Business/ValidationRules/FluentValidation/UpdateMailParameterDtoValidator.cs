using Entities.Dtos.Mails;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UpdateMailParameterDtoValidator : AbstractValidator<UpdateMailParameterDto>
    {
        public UpdateMailParameterDtoValidator()
        {
            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("UnitId boş olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.");

            RuleFor(x => x.SMTP)
                .NotEmpty().WithMessage("SMTP boş olamaz.");

            RuleFor(x => x.Port)
                .NotEmpty().WithMessage("Port boş olamaz.")
                .GreaterThan(0).WithMessage("Port pozitif bir değer olmalıdır.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.");
        }
    }
}
