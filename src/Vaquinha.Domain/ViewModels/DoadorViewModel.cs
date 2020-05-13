using System;
using System.ComponentModel;

namespace Vaquinha.Domain.ViewModels
{
    public class DoadorViewModel
    {
        private string _nome { get; set; }
        public string Nome
        {
            get { return Anonima ? "Doação anonima" : _nome; }
            set { _nome = value; }
        }

        [DisplayName("Doação anonima?")]
        public bool Anonima { get; set; }

        [DisplayName("Mensagem de apoio")]
        public string MensagemApoio { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataHora { get; set; }

        [DisplayName("Quando?")]
        public string DescricaoTempo => GerarDescricaoTempo();

        private string GerarDescricaoTempo()
        {
            var descricao = string.Empty;

            if (DataHora != DateTime.MinValue)
            {
                TimeSpan intervalo = (DateTime.Now - DataHora);

                if (intervalo.Days > 365)
                {
                    var ano = intervalo.Days / 365;
                    descricao = ano + " ano";
                    if (ano > 1)
                    {
                        descricao += "s";
                    }
                }
                else if (intervalo.Days > 30)
                {
                    var mes = intervalo.Days / 30;
                    descricao = mes + " mês";
                    if (mes > 1)
                    {
                        descricao += "es";
                    }
                }
                else if (intervalo.Days > 0)
                {
                    descricao = intervalo.Days + " dia";
                    if (intervalo.Days > 1)
                    {
                        descricao += "s";
                    }
                }
                else if (intervalo.Hours > 0)
                {
                    descricao = intervalo.Hours + " hora";
                    if (intervalo.Hours > 1)
                    {
                        descricao += "s";
                    }
                }
                else if (intervalo.Minutes > 0)
                {
                    descricao = intervalo.Minutes + " minuto";
                    if (intervalo.Minutes > 1)
                    {
                        descricao += "s";
                    }
                }
                else
                {
                    return "nesse instante";
                }

                descricao += " atrás";
            }

            return descricao;
        }
    }
}