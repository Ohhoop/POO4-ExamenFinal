using System.ComponentModel.DataAnnotations;

namespace CreditImpot.MVC.Models
{
    public class DemandeCredit
    {
        [Display(Name = "Numéro d'assurance social")]
        [Required(ErrorMessage = "Le numéro d'assurance social est requis")]
        public string NAS { get; set; }

        [Display(Name = "Nom de l'enfant")]
        [Required(ErrorMessage = "Le nom de l'enfant est requis")]
        public string NomEnfant { get; set; }

        [Display(Name = "Est atteint de déficience ?")]
        public bool EstAtteintDeficience { get; set; } = false;

        [Display(Name = "Date de naissance")]
        [Required(ErrorMessage = "La date de naissance est requis")]
        public DateTime DateNaissance { get; set; }

        [Display(Name = "Salaire de la mère")]
        public int SalaireMere { get; set; }

        [Display(Name = "Salaire du père")]
        public int SalairePere { get; set; }

        [Display(Name = "Montant des frais de garde")]
        [Required(ErrorMessage = "Le montant des frais de garde est requis")]
        public int MontantFrais { get; set; }
        
    }
}
