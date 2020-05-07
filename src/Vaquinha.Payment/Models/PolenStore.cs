namespace Vaquinha.Payment.Models
{
    /// <summary>
    /// Apesar de vários campos opcionais, recomendamos o preenchimento do maior número de dados.
    /// Isso ajudará a filtrar facilmente as doaçoes e a gerar métricas mais detalhadas.
    /// </summary>
    public class PolenStore
    {
        /// <summary>
        /// (obrigatório) O Identificador único da loja que você utiliza. Ex.: "walmart" ou o CNPJ
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// (opcional-recomendado++) E-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// (opcional-recomendado) Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// //domain (opcional) website da loja.
        /// </summary>
        public string Domain { get; set; }
    }
}
