using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    //generic class verilebilir id farklı formatlarda gelebilir diye
    //entities içindekiler tablolara karşılık gelen klas lar
    public class MyEntityBase
    {
        //primary ve oto increment yaptık
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime ModifiedOn { get; set; }
        [Required, StringLength(30)]
        public string ModifiedUserName { get; set; }
    }
}
