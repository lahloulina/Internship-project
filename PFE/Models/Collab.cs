using System;
using System.ComponentModel.DataAnnotations;

namespace PFE.Models
{
    public class Collab
    {
        [Key]
        public int id_Collab { get; set; }

        [Required(ErrorMessage = "Le champ Matricule est obligatoire.")]
        public string Matricule { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Prénom est obligatoire.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ N° de téléphone est obligatoire.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Le champ N° de téléphone doit contenir exactement 10 chiffres.")]
        public string N_Tel { get; set; }

       
        [Required(ErrorMessage = "Le champ CIN est obligatoire.")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Le champ Adresse est obligatoire.")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Le champ Date d'entrée à SQLI est obligatoire.")]
        public DateTime Anciennete_SQLI { get; set; }

        [Required(ErrorMessage = "Le champ Date d'entrée au travail est obligatoire.")]
        public DateTime Anciennete_Travail { get; set; }

        [Required(ErrorMessage = "Le champ Nombre de projets actuels est obligatoire.")]
        public int Nbr_Prj_act { get; set; }
    }
}
