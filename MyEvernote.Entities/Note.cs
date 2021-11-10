using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Notes")]
    public class Note : MyEntityBase
    {
        [Required, StringLength(60)]
        public string Title { get; set; }
        [Required, StringLength(2000)]
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        //cetegory tablosunun primary key i ile eşleştirdik
        public int CategoryId { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key user ile
        public virtual EvernoteUser Owner { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ kategori ile
        public virtual Category Category { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ yorum ile
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
