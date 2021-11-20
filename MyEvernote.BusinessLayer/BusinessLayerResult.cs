using MyEvernote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class BusinessLayerResult <T> where T:class
    {
        //List de iki param kullanamayız, bu şekil bir şey kullanmalıyız
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }
        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        //key value ile kolay ekleme yapabilmek için
        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
        }
    }
}
