using System.Net.Mail;
using System.Threading.Tasks;

namespace Rikarin.Mail {
    public interface IMailman {
        Task SendAsync(MailAddress from, MailAddress to, string subject, string body);
    }
}
