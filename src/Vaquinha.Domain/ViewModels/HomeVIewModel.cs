using System.Collections.Generic;
using System.ComponentModel;

namespace Vaquinha.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Doadores = new List<DoadorViewModel>();
            Instituicoes = new List<CausaViewModel>();
        }

        [DisplayName("Quanto falta arrecadar?")]
        public double ValorRestanteMeta { get; set; }

        [DisplayName("Arrecadamos quanto?")]
        public double ValorTotalArrecadado { get; set; }

        [DisplayName("Percentual Arrecadado")]
        public double PorcentagemTotalArrecadado { get; set; }

        [DisplayName("Quantidade de Doadores")]
        public int QuantidadeDoadores { get; set; }

        [DisplayName("Dias Restantes")]
        public int TempoRestanteDias { get; set; }

        [DisplayName("Horas Restantes")]
        public int TempoRestanteHoras { get; set; }

        [DisplayName("Minutos Restantes")]
        public int TempoRestanteMinutos { get; set; }

        public IEnumerable<DoadorViewModel> Doadores { get; set; }
        public IEnumerable<CausaViewModel> Instituicoes { get; set; }
    }
}