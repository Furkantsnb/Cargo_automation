using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos.OperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IResult Add(CreateOperationClaimDto operationClaim);

        IResult Update(UpdateOperationClaimDto operationClaim);
        IResult Delete(OperationClaim operationClaim);
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<List<OperationClaim>> GetList();
    }
}
