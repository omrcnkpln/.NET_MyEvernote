using MyEvernote.DAL.EF;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        //artık repository üzerinden new leyeceğiz istediğimiz methoda göre
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note= new Repository<Note>();

        //contructor içinde yazmadığım için hata aldım
        //db oluşturmak için deneme amaçlı
        public Test()
        {
            //DAL.DatabaseContext db = new DAL.DatabaseContext();
            ////db yok ise oluştur demek
            ////burası örnek data ile çalıştırmaz
            ////db.Database.CreateIfNotExists();
            ////db.EvernoteUsers.ToList();

            ////eğer bir tane select atarsak seed i çalıştırır ve örnek fata ile birlikte db oluşur
            //db.Categories.ToList();

            //mesela
            //repo.List(x => x.Id > 5);

            //repo_category.List();
            List<Category> categories = repo_category.List();
            //List<Category> categories_filtered = repo_category.List(x => x.Id > 5);
        }

        public void InsertTest()
        {
            //ilgili koleksiyon setine ekleyip save yapıyor
            int result = repo_user.Insert(new EvernoteUser() {
                Name = "İnanç",
                Surname = "GÜN",
                Email = "i@i.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "ii",
                Password = "123",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "ii"
            }); 
        }

        public void UpdateTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "İnanç");

            if(user != null)
            {
                user.Username = "xxx";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "xxx");

            if (user != null)
            {
                int result = repo_user.Delete(user);

                //repo_user.Delete(user);
                //repo_user.Save();
            }
        }

        public void CommentTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);

            Comment comment = new Comment()
            {
                Text = "Bu bir test 'dir.",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "omrcnkpln",
                Note = null,
                Owner = null
            };

            repo_comment.Insert(comment);
        }
    }
}
