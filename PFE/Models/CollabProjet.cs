using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFE.Models
{
    public partial class CollabProjet
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCollabProjet { get; set; }
        public int? id_Collab { get; set; }
        public int? Id { get; set; }

        public virtual Projet? IdNavigation { get; set; }
        public virtual Collab? id_CollabNavigation { get; set; }
    }
}