using Entities.Dtos.Stations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CreateStationDtoValidator : AbstractValidator<CreateStationDto>
    {
        public CreateStationDtoValidator()
        {
            RuleFor(x => x.StationName)
                .NotEmpty().WithMessage("Durak adı boş olamaz.");

            RuleFor(x => x.OrderNumber)
                .NotEmpty().WithMessage("Sıra numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Sıra numarası pozitif bir değer olmalıdır.");

            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Birim Id boş olamaz.")
                .GreaterThan(0).WithMessage("Birim Id pozitif bir değer olmalıdır.");

            RuleFor(x => x.LineId)
                .NotEmpty().WithMessage("Hat Id boş olamaz.")
                .GreaterThan(0).WithMessage("Hat Id pozitif bir değer olmalıdır.");
        }
    }
}
