using Microsoft.AspNetCore.Mvc;
using CreditImpot.API.Interfaces;
using CreditImpot.API.Data;
using CreditImpot.API.Models;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditImpot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FraisGardeController : ControllerBase
    {
        private readonly CreditImpotContext _context;
        private readonly ICalculCredit _calculCredit;

        public FraisGardeController(CreditImpotContext context, ICalculCredit calculCredit)
        {
            _context = context;
            _calculCredit = calculCredit;
        }

        // GET: api/<FraisGardeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandeCreditFraisGarde>>> Get(string? NAS)
        {
            if (NAS == null)
            {
                return Ok(await _context.DemandeCreditFraisGardes.ToListAsync());
            }
            else
            {
                return Ok(await _context.DemandeCreditFraisGardes.Where(d => d.NAS == NAS).ToListAsync());
            }
        }


        // POST api/<FraisGardeController>
        [HttpGet("{id}")]
        public async Task<ActionResult<DemandeCreditFraisGarde>> GetById(string id)
        {
            var demande = await _context.DemandeCreditFraisGardes.FindAsync(id);

            if (demande == null)
            {
                return NotFound();
            }
            return demande;

        }

        
        // POST api/<FraisGardeController>
        [HttpPost]
        public async Task<ActionResult<DemandeCreditFraisGarde>> Post([FromBody] DemandeCredit demande)
        {
            if(ModelState.IsValid)
            {
                var demandeCredit = new DemandeCreditFraisGarde
                {
                    NAS = demande.NAS,
                    NomPrenomEnfant = demande.NomEnfant,
                    DateNaissance = demande.DateNaissance,
                    EstAtteintDeficience = demande.EstAtteintDeficience,
                    DateDemande = DateTime.Now,
                    RevenuFamilial = demande.SalairePere + demande.SalaireMere,
                    MontantFraisPaye = demande.MontantFrais,
                    CreditImpot = _calculCredit.CalculerCredit(demande)
                };

                 _context.DemandeCreditFraisGardes.Add(demandeCredit);

                 await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(Post), new { id = demandeCredit.ID }, demandeCredit);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            DemandeCreditFraisGarde? demande = await _context.DemandeCreditFraisGardes.SingleOrDefaultAsync(d=>d.ID==id);

            if (demande == null)
            {
                return NotFound();
            }

            _context.Remove(demande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
