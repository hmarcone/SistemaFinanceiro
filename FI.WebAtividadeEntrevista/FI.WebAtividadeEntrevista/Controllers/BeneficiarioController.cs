using FI.AtividadeEntrevista.BLL;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebAtividadeEntrevista.Helpers;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FindCpfBeneficiario(string cpf)
        {
            BoBeneficiario bo = new BoBeneficiario();

            cpf = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

            if (bo.VerificarExistencia(cpf))
            {
                return Json("CPF do beneficiário já existente na base de dados!", JsonRequestBehavior.AllowGet);
            }

            if (!Validacao.ValidaCPF.IsValidCpf(cpf))
                return Json("O número do CPF é inválido!", JsonRequestBehavior.AllowGet);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}