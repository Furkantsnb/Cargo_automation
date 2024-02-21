using Entities.Dtos.Lines;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CreateLineDtoValidator : AbstractValidator<CreateLineDto>
    {
        public CreateLineDtoValidator()
        {
            RuleFor(x => x.LineName)
                .NotEmpty().WithMessage("Hat adı boş olamaz.")
                .MaximumLength(50).WithMessage("Hat adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Stations)
                .Must(stations => stations != null && stations.Count <= 10)
                .WithMessage("Bir hat için en fazla 10 istasyon eklenebilir.");

            RuleFor(dto => dto.Stations).Must(stations => stations.Distinct().Count() == stations.Count)
                .WithMessage("Aynı durak birden fazla kez eklenemez.");
        }
    }
}
