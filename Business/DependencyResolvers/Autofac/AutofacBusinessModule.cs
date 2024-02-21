using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.InterCeptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework;
using DataAccsess.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AgentaManager>().As<IAgentaService>();
            builder.RegisterType<EfAgentaDal>().As<IAgentaDal>();

           // builder.RegisterType<LineManager>().As<ILineService>();
            builder.RegisterType<EfLineDal>().As<ILineDal>();

            builder.RegisterType<StationManager>().As<IStationService>();
            builder.RegisterType<EfStationDal>().As<IStationDal>();

            builder.RegisterType<TransferCenterManager>().As<ITransferCenterService>();
            builder.RegisterType<EfTransferCenterDal>().As<ITransferCenterDal>();

            builder.RegisterType<UnitManager>().As<IUnitService>();
            builder.RegisterType<EfUnitDal>().As<IUnitDal>();

            builder.RegisterType<MailParameterManager>().As<IMailParameterService>();
            builder.RegisterType<EfMailParameterDal>().As<IMailParameterDal>();

            builder.RegisterType<MailTemplateManager>().As<IMailTemplateService>();
            builder.RegisterType<EfMailTemplateDal>().As<ImailTemplateDal>();

            builder.RegisterType<MailManager>().As<IMailService>();
            builder.RegisterType<EfMailDal>().As<IMailDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JWTHelper>().As<ITokenHelper>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
