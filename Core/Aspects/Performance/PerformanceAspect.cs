using Castle.DynamicProxy;
using Core.Utilities.InterCeptors;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect :MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch=ServiceTool.ServiceProvider.GetService<Stopwatch>();
            
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                //mail kodları
                string body = $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}";
                SendConfirmEmail(body);
            }
            _stopwatch.Reset();
        }

        void SendConfirmEmail(string body)
        {
            string subject = "Performans Maili";
           


         
            SendMailDto sendMailDto = new SendMailDto()
            {
               Email = "furkantsnb72@hotmail.com",
               Password="Furkan.t0772",
               Port=587,
               SMTP= "smtp-mail.outlook.com",
               SSL=true,
               email= "furkantsnb72@hotmail.com",
               subject=subject,
               body=body,
               //smtp-mail.outlook.com
                
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.Email);
                mail.To.Add(sendMailDto.email);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.Email,
                        sendMailDto.Password);
                    smtp.EnableSsl = sendMailDto.SSL;
                    smtp.Send(mail);
                }
            }

        
        }
    }


    
}
