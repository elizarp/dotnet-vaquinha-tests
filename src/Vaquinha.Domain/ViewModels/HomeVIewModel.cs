using System.Collections.Generic;

namespace Vaquinha.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Doadores = new List<DoadorViewModel>();
            Instituicoes = new List<InstituicaoViewModel>();
        }

        public decimal ValorRestanteMeta { get; set; }
        public decimal ValorTotalArrecadado { get; set; }
        public decimal PorcentagemTotalArrecadado { get; set; }

        public int QuantidadeDoadores { get; set; }
        public int TempoRestanteDias { get; set; }
        public int TempoRestanteHoras { get; set; }
        public int TempoRestanteMinutos { get; set; }

        public IEnumerable<DoadorViewModel> Doadores { get; set; }
        public IEnumerable<InstituicaoViewModel> Instituicoes { get; set; }
    }
}