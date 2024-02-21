using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMailTemplateService
    {
        IResult Add(CreateMailTemplateDto mailTemplate);
        IResult Update(UpdateMailTemplateDto mailTemplate);
        IResult Delete (MailTemplate mailTemplate);
        IDataResult<MailTemplate> Get(int id);
        IDataResult<MailTemplate> GetByTemplateName(string name, int unitId);
        IDataResult<List<MailTemplate>> GetAll(int unitId);
    }
}
