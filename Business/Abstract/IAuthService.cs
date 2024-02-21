using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserUnitDto> Register(UserForRegister userForRegister, string password,int unitId);
        IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password,int unitId);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<User> GetByMailConfirmValue(string value);
        IDataResult<User> GetById(int id);
        IResult UserExists(string email);
        IResult Update(User user);
    
        IResult SendConfirmEmail(User user);
        IDataResult<AccessToken> CreateAccessToken(User user,int unitId);
        IDataResult<UserUnit> GetUnit(int userId);


    }
}
