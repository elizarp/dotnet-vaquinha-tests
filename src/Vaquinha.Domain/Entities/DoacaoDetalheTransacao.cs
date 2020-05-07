using System;

namespace Vaquinha.Domain.Entities
{
    public class DoacaoDetalheTransacao 
    {
        public DoacaoDetalheTransacao(string success, int? errorCode, string description,bool? isDuplicated, string invoiceId, 
                                      string invoiceUrl, string metodo, string orderId, long elapsedMillisecondsTransacao)
        {
            Id = Guid.NewGuid();

            Success = success;
            ErrorCode = errorCode;
            Description = description;
            IsDuplicated = isDuplicated;
            InvoiceId = invoiceId;
            InvoiceUrl = invoiceUrl;
            Metodo = metodo;
            OrderId = orderId;
            ElapsedMillisecondsTransacao = elapsedMillisecondsTransacao;
        }

        public Guid Id { get; set; }

        public long ElapsedMillisecondsTransacao { get; set; }

        public string Success { get; private set; }
        public int? ErrorCode { get; private set; }
        public string Description { get; private set; }
        public bool? IsDuplicated { get; private set; }

        public string InvoiceId { get; private set; }
        public string InvoiceUrl { get; private set; }
        public string Metodo { get; private set; }
        public string OrderId { get; private set; }

        public Doacao Doacao { get; set; }
    }
}