using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreditImpot.MVC.Models
{
    public class DemandeCreditFraisGarde 
    {
       
        public virtual int ID { get; set; }
        public string? NAS { get; set; }

        [Display(Name = "Nom")]
        public string? NomPrenomEnfant { get; set; }
    
        public bool EstAtteintDeficience { get; set; }
        public DateTime DateNaissance { get; set; }

        [Display(Name = "Date Demande")]
        public DateTime DateDemande { get; set; }

        [Display(Name = "Revenu Familial")]
        public int RevenuFamilial { get; set; }

        [Display(Name = "Montant des frais payé")]
        public int MontantFraisPaye { get; set; }

        [Display(Name= "Credit accordé")]
        public decimal CreditImpot { get; set; }
    }
}
