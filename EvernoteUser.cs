using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    public class EvernoteUser : MyEntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ note ile
        public virtual List<Note> Notes { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ comment ile
        public virtual List<Comment> Comments{ get; set; }
        public virtual List<Liked> Likes{ get; set; }
    }
}
