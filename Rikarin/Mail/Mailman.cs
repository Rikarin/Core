using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Rikarin.Mail {
    public class Mailman : IMailman {
        readonly SmtpClient _client;

        public Mailman(IOptionsSnapshot<MailmanSettings> settings) {
            _client = new SmtpClient(settings.Value.Hostname) {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(settings.Value.Username, settings.Value.Password)
            };
        }

        public Task SendAsync(MailAddress from, MailAddress to, string subject, string body) {
            var message = new MailMessage(from, to) {
                Body    = body,
                Subject = subject
            };

            return _client.SendMailAsync(message);
        }
    }
}
