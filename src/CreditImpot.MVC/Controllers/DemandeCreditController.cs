using CreditImpot.MVC.Interface;
using CreditImpot.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CreditImpot.MVC.Controllers
{
    public class DemandeCreditController : Controller
    {
        private readonly IFraisGardeService _DemandeCreditProxy;

        public DemandeCreditController(IFraisGardeService demandeCreditProxy)
        {
            _DemandeCreditProxy = demandeCreditProxy;
        }


        // GET: DemandeCreditController
        public async Task<ActionResult> Index(string? NAS)
        {
            return View(await _DemandeCreditProxy.ObtenirSelonNAs(NAS));
        }

        // Get: DemandeCreditController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DemandeCreditController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DemandeCredit demandeCredit)
        {
            if (ModelState.IsValid)
            {
                if(demandeCredit.MontantFrais >= 2300)
                {
                    var reponse = await _DemandeCreditProxy.Ajouter(demandeCredit);
                    //On validate que la requête a été executée avec succès
                    if (reponse.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    //On ajoute le message d'erreur au modelState sinon
                    else
                    {
                        var ErrorMessage = JsonConvert.DeserializeObject<dynamic>(await reponse.Content.ReadAsStringAsync());

                        ModelState.AddModelError("NAS", Convert.ToString(ErrorMessage.errors.NAS));
                    }
                } else
                {
                    ModelState.AddModelError("MontantFrais", "Le Montant des frais de garde doit être supérieur à 2 300$ pour avoir droit au crédit.");
                }
            }
            return View(demandeCredit);
        }

        [HttpGet]
        public async Task<ActionResult> Supprimer(int id)
        {
            await _DemandeCreditProxy.Supprimer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
