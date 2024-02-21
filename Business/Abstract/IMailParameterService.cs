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
    public interface IMailParameterService
    {
        IResult Update(UpdateMailParameterDto mailParameter);
       IDataResult< MailParameter> Get(int unitId);
    }
}
