using System;

namespace Vaquinha.Payment.Models
{
    public class PolenDonation
    {
        public PolenDonation()
        {
            OrderId = Guid.NewGuid().ToString();
            CreateAt = DateTime.Now;
        }

        /// <summary>
        /// (obrigatório) é o identificador da loja. ex.: um cnpj ou nome sem espaço e sem caracteres especiais.
        /// Caso nao exista em nosso banco de dados,criamos uma nova loja com este id.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// (obrigatório) o identificador que você utilizará para identificar esta transação.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// (obrigatório) o valor da doação.
        /// </summary>
        public decimal Donation { get; set; }

        /// <summary>
        /// (opcional-recomendado) O Id de identificação do usuario. ex.: cpf, email.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// (opcional) Caso tenha a Ong que quer apoiar, pode passar o id dela por aqui.
        /// Caso nao tenha, o Polen aplicará a regra prévia para associar uma causa para esta doação.
        /// </summary>
        public string NgoId { get; set; }

        /// <summary>
        /// (opcional) O status dessa doação. Status default: Confirmed (0-Pending; 1-Confirmed; 2-Canceled; 3-Reversed; 4-Paid)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// purchase (opcional) o valor da transação realizada. É opcional mas ao preencher conseguimos entregar um analytics mais completo do seu impacto.
        /// </summary>
        public decimal Purchase => Donation;

        /// <summary>
        ///  (opcional) facilita a visualizacao e a criação da landing page para a loja.
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// (opcional) Analytics específico para cada usuário
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// (opcional) Analytics específico para cada usuário
        /// </summary>
        public string UserName { get; set; }


        public DateTime CreateAt { get; set; }
    }
}
