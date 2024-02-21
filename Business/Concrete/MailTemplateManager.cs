using AutoMapper;
using Business.Abstract;

using Business.BusinessAspects.Autofac;
using Business.Constans;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccsess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Mails;

namespace Business.Concrete
{
    public class MailTemplateManager : IMailTemplateService
    {
        private readonly ImailTemplateDal _mailTemplateDal;
        private readonly IMapper _mapper;

        public MailTemplateManager(ImailTemplateDal mailTemplateDal, IMapper mapper)
        {
            _mailTemplateDal = mailTemplateDal;
            _mapper = mapper;
        }

        public IResult Add(CreateMailTemplateDto mailTemplateDto)
        {
            MailTemplate mailTemplate = _mapper.Map<MailTemplate>(mailTemplateDto);
            _mailTemplateDal.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }
   
        public IResult Delete(MailTemplate mailTemplate)
        {
            _mailTemplateDal.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }

        public IDataResult<MailTemplate> Get(int id)
        {
            return new SuccesDataResult<MailTemplate>(_mailTemplateDal.Get(m => m.Id == id));
        }

        public IDataResult<List<MailTemplate>> GetAll(int unitId)
        {
            return new SuccesDataResult<List<MailTemplate>>(_mailTemplateDal.GetList(m => m.UnitId == unitId));

        }
  
        public IDataResult<MailTemplate> GetByTemplateName(string name, int unitId)
        {
            return new SuccesDataResult<MailTemplate>(_mailTemplateDal.Get(m => m.Type == name && m.UnitId == unitId));
        }

        public IResult Update(UpdateMailTemplateDto mailTemplateDto)
        {
            MailTemplate mailTemplate = _mapper.Map<MailTemplate>(mailTemplateDto);
            _mailTemplateDal.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }
    }
}
