using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiário
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="">Objeto de beneficiário</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome),
                new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF),
                new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente)
            };

            DataSet ds = Consultar("FI_SP_IncBeneficiario", parametros);

            long ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        /// <summary>
        /// Consultar um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        internal Beneficiario Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = Consultar("FI_SP_ConsBeneficiario", parametros);
            List<Beneficiario> beneficiario = Converter(ds);

            return beneficiario.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            List<DML.Beneficiario> beneficiario = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return beneficiario;
        }

        /// <summary>
        /// Lista todos os beneficiários
        /// </summary>
        internal List<DML.Beneficiario> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<DML.Beneficiario> beneficiario = Converter(ds);

            return beneficiario;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome),
                new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF),
                new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id)
            };

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }


        /// <summary>
        /// Excluir Beneficiário
        /// </summary>
        /// <param name="id">Objeto de Beneficiário</param>
        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("Id", id)
            };

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var beneficiario = new Beneficiario
                    {
                        Id = row.Field<long>("Id"),
                        CPF = row.Field<string>("CPF"),
                        Nome = row.Field<string>("Nome"),
                        IdCliente = row.Field<long>("IdCliente")
                    };
                    lista.Add(beneficiario);
                }
            }

            return lista;
        }
    }
}
