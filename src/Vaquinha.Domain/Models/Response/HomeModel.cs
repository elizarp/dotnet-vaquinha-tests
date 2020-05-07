using System.Collections.Generic;

namespace Vaquinha.Domain.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            Doadores = new List<DoadorModel>();
            Instituicoes = new List<InstituicaoModel>();
        }

        public decimal ValorFaltanteMeta { get; set; }
        public decimal ValorTotalArrecadado { get; set; }
        public decimal PorcentagemTotalArrecadado { get; set; }
        public int QuantidadeDoadores { get; set; }
        public int TempoRestanteDias { get; set; }
        public int TempoRestanteHoras { get; set; }
        public int TempoRestanteMinutos { get; set; }
        public IEnumerable<DoadorModel> Doadores { get; set; }
        public IEnumerable<InstituicaoModel> Instituicoes { get; set; }
    }
}