
using CreditImpot.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditImpot.API.Data
{
    public class DBInitializer
    {
        public static void Initialize(CreditImpotContext context)
        {
            //On valide s'il y a des données dans la BD
            if (context.DemandeCreditFraisGardes.Any())
            {
                return;
            }

            var demandecredits = new List<DemandeCreditFraisGarde>()
            {
                new DemandeCreditFraisGarde
                {
                    NAS = "123456789",
                    NomPrenomEnfant = "Larabi Simon",
                    EstAtteintDeficience = false,
                    DateNaissance = new DateTime(2020, 1, 1),
                    DateDemande = new DateTime(2023, 2, 14),
                    RevenuFamilial = 45000,
                    MontantFraisPaye = 5000,
                    CreditImpot = 1890
                },
                new DemandeCreditFraisGarde
                {
                    NAS = "245678765",
                    NomPrenomEnfant = "LeBlanc Mathieu",
                    EstAtteintDeficience = false,
                    DateNaissance = new DateTime(2019, 1, 1),
                    DateDemande = new DateTime(2023, 2, 26),
                    RevenuFamilial = 180000,
                    MontantFraisPaye = 20000,
                    CreditImpot = 10675
                }
            };

            context.AddRange(demandecredits);
            context.SaveChanges();

        }
    }
  }
