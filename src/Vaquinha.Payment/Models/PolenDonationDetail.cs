namespace Vaquinha.Payment.Models
{
    /// <summary>
    /// Traz os detalhes de uma doações específica a partir de seu Id. 
    /// Esse Id é enviado como Response ao cadastrar/atualizar uma doa doação ou ao listar as doacoes Parametros obrigatórios:
    /// </summary>
    public class PolenDonationDetail
    {
        /// <summary>
        /// (obrigatório) é o identificador da loja. ex.: um cnpj ou nome sem espaço e sem caracteres especiais.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// (opcional) se você armazena a transactionId do Polen que retornamos ao criar uma nova doacao, recomendamos usar esse parametro.
        /// </summary>
        public string PolenTransactionId { get; set; }

        /// <summary>
        /// (opcional) o identificador que você nos passou quando criou a doacao.
        /// </summary>
        public string OrderId { get; set; }
    }
}