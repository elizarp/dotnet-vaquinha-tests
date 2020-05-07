namespace Vaquinha.Payment.Models
{
    /// <summary>
    /// Traz os detalhes de uma loja específica. Parametros obrigatórios:
    /// </summary>
    public class PolenStoreDetail
    {
        /// <summary>
        /// (obrigatório) chave de acesso à aplicação
        /// </summary>
        public string Api_token { get; set; }

        /// <summary>
        /// (obrigatório) O Identificador único da loja que você utiliza. Ex.: "walmart" ou o CNPJ
        /// </summary>
        public string StoreId { get; set; }
    }
}
