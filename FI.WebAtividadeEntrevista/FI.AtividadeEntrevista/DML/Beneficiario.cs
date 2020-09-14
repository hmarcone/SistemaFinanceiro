using System.ComponentModel.DataAnnotations;

namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:000.000.000-00}")]
        public string CPF { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        public long IdCliente { get; set; }

    }
}
