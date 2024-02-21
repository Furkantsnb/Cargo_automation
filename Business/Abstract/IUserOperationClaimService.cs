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
    public interface IUserOperationClaimService
    {
        IResult Add(CreateUserOperationClaimDto userOperationClaim);

        IResult Update(UpdateUserOperationClaimDto userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<UserOperationClaim> GetById(int id);
        IDataResult<List<UserOperationClaim>> GetList(int userId,int unitId);
    }
}
