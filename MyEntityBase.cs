using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    //generic class verilebilir id farklı formatlarda gelebilir diye
    //entities içindekiler tablolara karşılık gelen klas lar
    public class MyEntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
