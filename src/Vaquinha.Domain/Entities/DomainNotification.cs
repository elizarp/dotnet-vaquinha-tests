namespace Vaquinha.Domain.Entities
{
    public class DomainNotification
    {
        public string Id { get; private set; }
        public string MensagemErro { get; private set; }

        public DomainNotification(string errorCode, string errorMessage)
        {
            Id = errorCode;
            MensagemErro = errorMessage;
        }
    }
}
