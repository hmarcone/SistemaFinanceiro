namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            var beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.VerificarExistencia(CPF);
        }

    }
}
