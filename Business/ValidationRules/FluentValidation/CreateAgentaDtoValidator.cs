using Entities.Concrete;
using Entities.Dtos.Agentas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CreateAgentaDtoValidator :AbstractValidator<CreateAgentaDto>
    {
        public CreateAgentaDtoValidator() 
        {
           
            RuleFor(p => p.UnitName)
                .NotEmpty().WithMessage("Acenta Adı Boş olamaz")
                .MinimumLength(4).WithMessage("Acenta Adı En Az 4 Karakter Olmalıdır")
                .MaximumLength(20).WithMessage("en fazla 20 karakter olabilir");

            RuleFor(p => p.ManagerName)
                .NotEmpty().WithMessage("Adı Boş olamaz")
                .MinimumLength(4).WithMessage("Adı En Az 4 Karakter Olmalıdır")
                .MaximumLength(20).WithMessage("en fazla 20 karakter olabilir");

            RuleFor(p => p.ManagerSurname)
                .NotEmpty().WithMessage("Adı Boş olamaz").MinimumLength(4)
                .WithMessage("Adı En Az 4 Karakter Olmalıdır").MaximumLength(20)
                .WithMessage("en fazla 20 karakter olabilir");

            RuleFor(p => p.PhoneNumber)
                 .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                 .Matches(@"^\d{10}$").WithMessage("Telefon numarası 10 haneli olmalıdır.");

            RuleFor(p => p.Gsm)
                .NotEmpty().WithMessage("Gsm numarası boş olamaz.")
                .Matches(@"^\d{10}$").WithMessage("Gsm numarası 10 haneli olmalıdır.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olmalıdır.");

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("Şehir boş olamaz.");

            RuleFor(p => p.District)
                .NotEmpty().WithMessage("İlçe boş olamaz.");

            RuleFor(p => p.NeighbourHood)
                .NotEmpty().WithMessage("Mahalle boş olamaz.");

            RuleFor(p => p.Street)
                .NotEmpty().WithMessage("Cadde boş olamaz.");

            RuleFor(p => p.AddressDetail)
                .NotEmpty().WithMessage("Adres detayı boş olamaz.");


        }
    }
}
