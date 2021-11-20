using MyEvernote.DAL.EF;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {

            EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if(user != null)
            {
                if(user.Username == data.Username)
                {
                    //yazdığımız enum ile hata mesajlarını verdik
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanıcı adı kayıtlı !");
                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Mail adresi kayıtlı !");
                }
            }
            else
            {

                //bilgileri tam girmezsem validation hatası döner
                int dbResult = repo_user.Insert(new EvernoteUser() { 
                    Username = data.Username,
                    Email = data.EMail,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbResult > 1)
                {
                    res.Result = repo_user.Find(x => x.Email == data.EMail && x.Username == data.Username);

                    //aktivasyon maili atılacak..
                    //layerResult.Result.ActivateGuid
                }
            }

    //layerResult.Result =
                return res;
        } 

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktif değil !");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "E-posta adresinizi kontrol ediniz");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre hatalı :/");
            }

            return res;
        }
    }
}
