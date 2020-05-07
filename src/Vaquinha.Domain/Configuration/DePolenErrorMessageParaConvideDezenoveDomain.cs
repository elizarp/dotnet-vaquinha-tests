using System.Collections.Generic;
using System.Linq;
using Vaquinha.Domain.Models.Response;

namespace Vaquinha.Domain.Configuration
{
    public class DePolenErrorMessageParaConvideDezenoveDomain
    {
        private static Dictionary<PolenResponseStatus, string> DePolenParaConvideMensagens { get; set; }

        static DePolenErrorMessageParaConvideDezenoveDomain()
        {
            DePolenParaConvideMensagens = new Dictionary<PolenResponseStatus, string>
            {
                { PolenResponseStatus.CreditCardFirstNameInvalid,"O campo Nome do Titular está inválido." },
                { PolenResponseStatus.CreditCardLastNameInvalid,"O campo Nome do Titular está inválido." },
                { PolenResponseStatus.CreditCardMonthCodeInvalid,"O campo Data de Validade está inválido." },
                { PolenResponseStatus.CreditCardYearInvalid,"O campo Data de Validade está inválido." },
                { PolenResponseStatus.CreditCardCardNumberInvalid,"O campo Numero do Cartão está inválido." },
                { PolenResponseStatus.CreditCardSecurityCodeInvalid,"O campo CVV está inválido." },

                { PolenResponseStatus.CreditCardInvalid,"Seu pagamento não foi autorizado para sua segurança. Por favor, contate seu banco." },
                { PolenResponseStatus.PossibleDuplicationDonation,"Uma doação com os mesmos dados acabou de ocorrer nos ultimos 3 minutos. Por favor aguarde para tentar novamente." },

                { PolenResponseStatus.ApiTokenInvalid,"A transação não pode ser concluída." },
                { PolenResponseStatus.CompanyNotFound,"A transação não pode ser concluída." },
                { PolenResponseStatus.CreditCardDataNotFound,"A transação não pode ser concluída." },
                { PolenResponseStatus.DonorNotFound,"A transação não pode ser concluída." },
                { PolenResponseStatus.InternalServerError,"A transação não pode ser concluída." },
                { PolenResponseStatus.NgoNotFound,"A transação não pode ser concluída." },
                { PolenResponseStatus.TransacaoNaoAprovada,"A transação não pode ser concluída." }
            };
        }

        public static string RecuperarMensagemDominio(PolenResponseStatus polenErrorCode,string defaultDescriptionMessage = null)
        {
            return ExisteDePara(polenErrorCode)
                ? RecuperarDePara(polenErrorCode)
                : defaultDescriptionMessage;
        }

        private static bool ExisteDePara(PolenResponseStatus errorCode)
        {
            return DePolenParaConvideMensagens?.Keys?.Contains(errorCode) ?? false;
        }

        private static string RecuperarDePara(PolenResponseStatus errorCode)
        {
            return DePolenParaConvideMensagens[errorCode];
        }
    }
}