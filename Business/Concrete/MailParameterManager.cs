using AutoMapper;
using Business.Abstract;

using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccsess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Mails;

namespace Business.Concrete
{
    public class MailParameterManager : IMailParameterService
    {
        private readonly IMailParameterDal _mailParameterDal;
        private readonly IMapper _mapper;

        public MailParameterManager(IMailParameterDal mailParameterDal, IMapper mapper)
        {
            _mailParameterDal = mailParameterDal;
            _mapper = mapper;
        }
     
        public IDataResult<MailParameter> Get(int unitId)
        {
            return new SuccesDataResult<MailParameter>(_mailParameterDal.Get(m => m.Id == unitId));

        }
        [ValidationAspect(typeof(UpdateMailParameterDtoValidator))]
        public IResult Update(UpdateMailParameterDto mailParameterDto)
        {
            MailParameter mailParameter = _mapper.Map<MailParameter>(mailParameterDto);
            var result = Get(mailParameter.UnitId);
            if(result.Data == null)
            {
                _mailParameterDal.Add(mailParameter);
            }
            else
            {
                result.Data.SMTP = mailParameter.SMTP;
                result.Data.Port = mailParameter.Port;
                result.Data.SSL = mailParameter.SSL;
                result.Data.Email = mailParameter.Email;
                result.Data.Password = mailParameter.Password;
                _mailParameterDal.Update(result.Data);

            }
            return new SuccessResult(Messages.MailParameterAdded);
        }
    }
}
