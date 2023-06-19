using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFE.Models
{
    public class Projet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Nom du projet est obligatoire.")]
        public string Nom_Projet { get; set; }

        [Required(ErrorMessage = "Le champ Description est obligatoire.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le champ Membres d'équipe est obligatoire.")]
        public string Membre_Equipe { get; set; }

        [Required(ErrorMessage = "Le champ Date de début du projet est obligatoire.")]
        public DateTime Date_Debut { get; set; }
    }
}
