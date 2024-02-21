using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos.TransferCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITransferCenterService
    {
        IResult Add(CreateTransferCenterDto transferCenter);
        IResult Update(UpdateTransferCenterDto transferCenter);
        IResult Delete(int id);
        IResult SoftDelete(int id);
        IDataResult<TransferCenter> GetById(int id);
        IDataResult<List<TransferCenter>> GetList();
        IResult TransferCenterExists(TransferCenter transferCenter);// TransferCenter veri tabanında olup olmadığını kontrol ediyor. 

        IDataResult<List<Agenta>> GetAgentasByTransferCenterId(int transferCenterId); // transfer merkezine bağlı acentaları listeleme
    }
}
