using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vaquinha.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Doadores = new List<DoadorViewModel>();
            Instituicoes = new List<InstituicaoViewModel>();
        }

        [DisplayName("Valor Restante para Meta:")]
        public double ValorRestanteMeta { get; set; }

        [DisplayName("Valor Total Arrecadado")]
        public double ValorTotalArrecadado { get; set; }

        [DisplayName("Percentual Arrecadado:")]
        public double PorcentagemTotalArrecadado { get; set; }

        [DisplayName("Quantidade de Doadores:")]
        public int QuantidadeDoadores { get; set; }

        [DisplayName("Quantidade de Doadores:")]
        public int TempoRestanteDias { get; set; }

        [DisplayName("Horas Restantes:")]
        public int TempoRestanteHoras { get; set; }

        [DisplayName("Minutos Restantes:")]
        public int TempoRestanteMinutos { get; set; }

        public IEnumerable<DoadorViewModel> Doadores { get; set; }
        public IEnumerable<InstituicaoViewModel> Instituicoes { get; set; }
    }
}