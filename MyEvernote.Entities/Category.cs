using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    // sınıfı türettik
    //ve bu isimde oluştur dedik el ile
    [Table("Categories")]
    public class Category : MyEntityBase
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ note ile
        public virtual List<Note> Notes { get; set; }

        //note oluştururken null hatası almamak için contr. da bu işlemi yaptık
        public Category()
        {
            Notes = new List<Note>();
        }
    }
}
