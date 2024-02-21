
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Dtos;


namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUnitService _unitService;
        private readonly IUnitDal _unitDal;
        private readonly IMailParameterService _mailparameterService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;



        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMailParameterService mailparameterService, IMailService mailService, IMailTemplateService mailTemplateService, IUserOperationClaimService userOperationClaimService, IOperationClaimService operationClaimService, IUnitService unitService, IUnitDal unitDal)
        {
            _mailparameterService = mailparameterService;
            _mailService = mailService;
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mailTemplateService = mailTemplateService;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
            _unitService = unitService;
            _unitDal = unitDal;
        }



        public IDataResult<AccessToken> CreateAccessToken(User user,int unitId)
        {
            var claims = _userService.GetClaims(user,unitId);
            var accessToken = _tokenHelper.CreateToken(user, claims,unitId);
            return new SuccesDataResult<AccessToken>(accessToken);

        }
       
        public IDataResult<User> GetById(int id)
        {
            return new SuccesDataResult<User>(_userService.GetById(id));

        }
    
        public IDataResult<User> GetByMailConfirmValue(string value)
        {
            return new SuccesDataResult<User>(_userService.GetByMailConfirmValue(value));
        }


        [ValidationAspect(typeof(UserForLoginValidator))]
        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToChech = _userService.GetByMail(userForLogin.Email);
            if(userToChech == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if(!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToChech.PasswordHash, userToChech.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccesDataResult<User>(userToChech, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(UserForRegisterValidator))]
        public IDataResult<UserUnitDto> Register(UserForRegister userForRegister, string password, int unitId)
        {

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User()
            { //bu tür yerlerde mapper kullansan iyidir
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name

            };
            _userService.Add(user);
          
            _unitService.UserUnitAdd(user.Id, unitId);
            UserUnitDto userUnitDto = new UserUnitDto() //**
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AddedAt = user.AddedAt,
                UnitId = unitId,
                IsActive = true,
                MailConfirm = user.MailConfirm,
                MailConfirmValue = user.MailConfirmValue,
                MailConfirmDate = user.MailConfirmDate,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,


            };
            SendConfirmEmail(user);

            return new SuccesDataResult<UserUnitDto>(userUnitDto, Messages.UserRegistered);
        }

        void SendConfirmEmail(User user)
        {
            string subject = "Kullanıcı Kayıt Onay Maili";
            string body = " kullanıcınız sisteme kayıt oldu. Kaydınızı tamamlamak için aşağıdaki linke tıklamanız gerekmektedir.";

            string link = "https://localhost:7174/api/Auth/confirmuser?value=" + user.MailConfirmValue;
            string linkDescription = "Kaydı Onaylamak İçin Tıkla";
            var mailTemplate = _mailTemplateService.GetByTemplateName("kayıt", 1);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

            var mailParameter = _mailparameterService.Get(1);
            Entities.Dtos.Mails.SendMailDto sendMailDto = new Entities.Dtos.Mails.SendMailDto()
            {
                mailParameter = mailParameter.Data,
                email = user.Email,
                subject = "Kullanıcı Kayıt Onay Maili",
                body = templateBody,
            };
            _mailService.SendMail(sendMailDto);
            user.MailConfirmDate=DateTime.Now;
            _userService.Update(user);
        }

        [ValidationAspect(typeof(UserForRegisterValidator))]
        public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password, int unitId)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User() //burada auto mapper ullansan iyi olur. adam kullanmamiş ama sen kullan aşınalık olsun ilerde çok kullanacaksın
            {
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name
            };
            _userService.Add(user);
            _unitService.UserUnitAdd(user.Id, unitId);
            SendConfirmEmail(user);
            return new SuccesDataResult<User>(user, Messages.UserRegistered);
        }
        public IResult Update(User user)
        {
            _userService.Update(user);
            return new SuccessResult(Messages.UserMailConfirmSuccessful);
        }

        public IResult UserExists(string email)
        {
           if(_userService.GetByMail(email) != null)
            {
                return new ErrorDataResult(Messages.UserAlREadyExist);
            }
            return new SuccessResult();
        }

        IResult IAuthService.SendConfirmEmail(User user)
        {
            if (user.MailConfirm == true)
            {
                return new ErrorDataResult(Messages.MailAlreadConfirm);
            }

            DateTime confirmMailDate = user.MailConfirmDate;
            DateTime now = DateTime.Now;
            if(confirmMailDate.ToShortDateString() == now.ToShortDateString())
            {
                if (confirmMailDate.Hour == now.Hour && confirmMailDate.AddMinutes(5).Minute <= now.Minute)
                {
                    SendConfirmEmail(user);
                    return new SuccessResult(Messages.MailConfirmSendSuccessful);
                }
                else
                {
                    return new ErrorDataResult(Messages.MailConfirmTimeHasNotExpire);
                }
            }
            SendConfirmEmail(user);
            return new SuccessResult(Messages.MailConfirmSendSuccessful);
        }

        public IDataResult<UserUnit> GetUnit(int userId)
        {
            return new SuccesDataResult<UserUnit>(_unitService.GetUnit(userId).Data);
        }
    }
}
