using Vaquinha.Domain;

namespace Vaquinha.Payment.Models
{
    /// <summary>
    /// Apesar de vários campos opcionais, recomendamos o preenchimento do maior número de dados. Isso ajudará a filtrar facilmente as doaçoes e a gerar métricas mais detalhadas.   
    /// </summary>
    public class PolenUser : IConvideDezenoveDiagnostics
    {
        /// <summary>
        /// (obrigatório) O Identificador único da loja que você utiliza. Ex.: "walmart" ou o CNPJ
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// (obrigatório) o identificador único deste usuario.
        /// Você pode utilizar a chave que utiliza no seu sistema, ou o email, cpf, etc. O importante é que seja um id único.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// (opcional-recomendado++) E-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// (opcional-recomendado) Nome do usuário.
        /// </summary>
        public string Name { get; set; }
        
        public long ElapsedMilliseconds { get; set; }
    }
}
