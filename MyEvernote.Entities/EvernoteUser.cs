using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser : MyEntityBase
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Surname { get; set; }
        [Required, StringLength(25)]
        public string Username { get; set; }
        [Required, StringLength(70)]
        public string Email { get; set; }
        [Required, StringLength(25)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ note ile
        public virtual List<Note> Notes { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ comment ile
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
    }
}
