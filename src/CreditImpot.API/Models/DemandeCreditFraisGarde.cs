using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreditImpot.API.Models
{
    public class DemandeCreditFraisGarde 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
        public string? NAS { get; set; }

        public string? NomPrenomEnfant { get; set; }
    
        public bool EstAtteintDeficience { get; set; }
        public DateTime DateNaissance { get; set; }

        public DateTime DateDemande { get; set; }

       
        public int RevenuFamilial { get; set; }

        
        public int MontantFraisPaye { get; set; }

        public decimal CreditImpot { get; set; }
    }
}
