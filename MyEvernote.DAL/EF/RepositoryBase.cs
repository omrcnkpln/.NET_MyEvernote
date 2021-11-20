using MyEvernote.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DAL.EF
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();

        //new lenmesini istemediğim için protected contructor tanımladım
        //miras alan sadece newleyebilir
        protected RepositoryBase()
        {
            CreateContext();
        }

        //sadece bir kere context oluşturduk singleton
        //static metodlar new lenmeden çalışırlar
        //static metodlar static değişkenlere erişebilir
        private static void CreateContext()
        {
            if(context == null)
            {
                //multi thread çalışmayı engellemek için
                lock (_lockSync)
                {
                    //extra kontrol
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }
    }
}
