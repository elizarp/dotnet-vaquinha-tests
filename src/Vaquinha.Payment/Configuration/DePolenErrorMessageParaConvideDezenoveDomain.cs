using System.Collections.Generic;
using System.Linq;
using Vaquinha.Payment.PolenEntities.Enumns;

namespace Vaquinha.Payment.Configuration
{
    public class DePolenErrorMessageParaConvideDezenoveDomain
    {
        private static Dictionary<DonationStatus, string> DePolenParaConvideMensagens { get; set; }

        static DePolenErrorMessageParaConvideDezenoveDomain()
        {
            DePolenParaConvideMensagens = new Dictionary<DonationStatus, string>
            {
                { DonationStatus.CreditCardFirstNameInvalid,"O campo Nome do Titular está inválido." },
                { DonationStatus.CreditCardLastNameInvalid,"O campo Nome do Titular está inválido." },
                { DonationStatus.CreditCardMonthCodeInvalid,"O campo Data de Validade está inválido." },
                { DonationStatus.CreditCardYearInvalid,"O campo Data de Validade está inválido." },
                { DonationStatus.CreditCardCardNumberInvalid,"O campo Numero do Cartão está inválido." },
                { DonationStatus.CreditCardSecurityCodeInvalid,"O campo CVV está inválido." },

                { DonationStatus.CreditCardInvalid,"Seu pagamento não foi autorizado para sua segurança. Por favor, contate seu banco." },
                { DonationStatus.PossibleDuplicationDonation,"Uma doação com os mesmos dados acabou de ocorrer nos ultimos 3 minutos. Por favor aguarde para tentar novamente." },

                { DonationStatus.ApiTokenInvalid,"A transação não pode ser concluída." },
                { DonationStatus.CompanyNotFound,"A transação não pode ser concluída." },
                { DonationStatus.CreditCardDataNotFound,"A transação não pode ser concluída." },
                { DonationStatus.DonorNotFound,"A transação não pode ser concluída." },
                { DonationStatus.InternalServerError,"A transação não pode ser concluída." },
                { DonationStatus.NgoNotFound,"A transação não pode ser concluída." },
                { DonationStatus.TransacaoNaoAprovada,"A transação não pode ser concluída." }
            };
        }

        public static string RecuperarMensagemDominio(DonationStatus polenErrorCode,string defaultDescriptionMessage = null)
        {
            return ExisteDePara(polenErrorCode)
                ? RecuperarDePara(polenErrorCode)
                : defaultDescriptionMessage;
        }

        private static bool ExisteDePara(DonationStatus errorCode)
        {
            return DePolenParaConvideMensagens?.Keys?.Contains(errorCode) ?? false;
        }

        private static string RecuperarDePara(DonationStatus errorCode)
        {
            return DePolenParaConvideMensagens[errorCode];
        }
    }
}