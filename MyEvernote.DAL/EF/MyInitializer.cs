using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.DAL.EF
{
    //database oluşturur
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        //veri girişi için
        protected override void Seed(DatabaseContext context)
        {
            //admin kullanıcısı oluşturma
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Ömer Can",
                Surname = "KAPLAN",
                Email = "omrcnkpln@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "omrcnkpln",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "omrcnkpln"
            };

            //standart kallanıcı oluşturma
            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Furkan",
                Surname = "AYDIN",
                Email = "f@f.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "frknaydn",
                Password = "123",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "omrcnkpln"
            };

            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);


            //10 tane de fake user eklicez ekstra
            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    //yeni string formatlama
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{i}"
                };

                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            //her seferinde context ile select atmasın diye döngüden önce bir kere users listesini aldık
            List<EvernoteUser> userlist = context.EvernoteUsers.ToList();

            //fake kategori oluşturacağız
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "omrcnkpln"
                };

                context.Categories.Add(cat);

                //kategorilerin notelarını oluşturacağız
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    EvernoteUser note_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1,9),
                        Owner = note_owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = note_owner.Username
                    };

                    cat.Notes.Add(note);

                    //note ların da yorumlarını oluşturacağız
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EvernoteUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                     
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_owner.Username
                        };

                        note.Comments.Add(comment);
                    }

                    //fake like ekleme işlemi
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };

                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
