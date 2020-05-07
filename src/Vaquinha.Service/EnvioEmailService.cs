using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Models.Input;

namespace Vaquinha.Service
{
    public class EnvioEmailService : IEnvioEmailService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IPollyService _pollyService;
        private readonly GloballAppConfig _globallAppConfig;
        private readonly ILogger<EnvioEmailService> _logger;
        private readonly IDomainNotificationService _domainNotificationService;

        public EnvioEmailService(IPollyService pollyService,
                                 IWebHostEnvironment env,
                                 GloballAppConfig globallAppConfig,
                                 ILogger<EnvioEmailService> logger,
                                 IDomainNotificationService domainNotificationService)
        {
            _env = env;
            _logger = logger;
            _pollyService = pollyService;
            _globallAppConfig = globallAppConfig;
            _domainNotificationService = domainNotificationService;
        }

        public async Task EnviarEmailAsync(ConvidarPorEmailModel destinatariosEmail)
        {
            var validadorEmail = new ConvidarPorEmailModelValidacao();
            if (!destinatariosEmail.Valido(validadorEmail))
            {
                _domainNotificationService.Adicionar(validadorEmail.ValidationResult);
                return;
            }

            await EnviarEmailTo(destinatariosEmail);
        }

        public bool EmailValido(ConvidarPorEmailModel destinatariosEmail)
        {
            var validadorEmail = new ConvidarPorEmailModelValidacao();

            var valido = destinatariosEmail.Valido(validadorEmail);

            if (!valido)
            {
                _domainNotificationService.Adicionar(validadorEmail.ValidationResult);
            }

            return valido;
        }

        private async Task EnviarEmailTo(ConvidarPorEmailModel destinatariosEmail)
        {
            try
            {
                var policy = _pollyService.CreateAsyncWaitAndRetryPolicyFor("EnviarEmailAsync");
                await policy.ExecuteAsync(async () => await EnviarEmail(destinatariosEmail));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar email: {ex.Message}");
                throw;
            }
        }

        private async Task EnviarEmail(ConvidarPorEmailModel destinatariosEmail)
        {
            using var smtp = new SmtpClient(_globallAppConfig.Email.Host)
            {
                EnableSsl = true,
                Port = _globallAppConfig.Email.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(_globallAppConfig.Email.Username, _globallAppConfig.Email.Password)
            };

            var mail = ReperarMensagemEmail(destinatariosEmail);
            await smtp.SendMailAsync(mail);
        }

        private MailMessage ReperarMensagemEmail(ConvidarPorEmailModel destinatariosEmail)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_globallAppConfig.Email.From)
            };

            foreach (var mailTo in destinatariosEmail.Emails)
            {
                mail.Bcc.Add(mailTo);
            }

            mail.Subject = _globallAppConfig.Email.Subject;

            mail.IsBodyHtml = true;

            mail.AlternateViews.Add(RecuperarAlternativeView());

            return mail;
        }

        private AlternateView RecuperarAlternativeView()
        {
            var idImagemCampanha = Guid.NewGuid().ToString();

            var body = RecuperarBodyEmail(idImagemCampanha);

            var res = new LinkedResource(ImagemCampanha)
            {
                ContentId = idImagemCampanha
            };

            var alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);

            return alternateView;
        }

        private string RecuperarBodyEmail(string idImagemCampanha)
        {
            var body = new StringBuilder();

            const string TEXT_STYLE = "style='font-family:sans-serif;font-size:20px;color:#4216ab'";

            var textoEmail = $@"<p>Olá! Você foi convidado a participar da nossa campanha!<br/>
                                   Para saber mais sobre como ajudar, acesse <b>{_globallAppConfig.LinkCampanha}</b></p>";

            body.AppendLine($"<div {TEXT_STYLE}>{textoEmail}</div>");

            body.AppendLine($"<img src=\"cid:{idImagemCampanha}\">");

            return body.ToString();
        }

        private string ImagemCampanha => Path.Combine(_env.WebRootPath, "images", _globallAppConfig.ImagemEmailCampanha);
    }
}