using System.ComponentModel.DataAnnotations;
namespace PFE.Models
{
    public class Objectif
    {
        [Key]
        public int id_Objectif { get; set; }
        [Required(ErrorMessage = "Le champ Label est obligatoire.")]
        public string Label_Obj { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ Nom du collaborateur est obligatoire.")]
        public string Nom_Collaborateur { get; set; }
        [Required(ErrorMessage = "Le champ Date de début est obligatoire.")]
        public DateTime Date_Debut { get; set; }
        public DateTime Date_fin { get; set; }
        public string Notation_Globale { get; set; }
        public string Rmq { get; set; }
    }
}
