namespace EncantoApadrinhamento.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, bool isBodyHtml);
        Task SendEmailAsync(List<string> emails, string subject, string message, bool isBodyHtml);
    }
}
