using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFE.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Email est obligatoire.")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Le format de l'email est invalide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champ Mot de passe est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le mot de passe doit comporter au moins 8 caractères.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Le mot de passe doit comporter au moins une lettre majuscule, une lettre minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; }
    }

}