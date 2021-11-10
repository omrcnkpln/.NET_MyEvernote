using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    // sınıfı türettik
    public class Category : MyEntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //başka klas ile ilişkili olduğu için virtul tanımladık
        //foreign key 1+ note ile
        public virtual List<Note> Notes { get; set; }
    }
}
