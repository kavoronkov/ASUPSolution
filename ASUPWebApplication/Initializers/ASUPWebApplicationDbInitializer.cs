using ASUPWebApplication;
using ASUPWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASUPWebApplication.Initializers
{
    public class ASUPWebApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            // var roleAdmin = new IdentityRole { Name = "admin" };
            // var roleUser = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            // roleManager.Create(roleAdmin);
            // roleManager.Create(roleUser);

            // // создаем пользователей
            // var admin = new ApplicationUser { Email = "kavoronkov@interpipe.biz", UserName = "kavoronkov" };
            // string password = "Kav_201285";
            // var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            // if (result.Succeeded)
            // {
            //    // добавляем для пользователя роль
            //    userManager.AddToRole(admin.Id, roleAdmin.Name);
            //    userManager.AddToRole(admin.Id, roleUser.Name);
            // }

            List<string> listRolesName = new List<string> { "admin", "user" };
            List<IdentityRole> listIdentityRole = new List<IdentityRole>();
            Dictionary<string, string> dictionaryEmailPassword = new Dictionary<string, string>();
            dictionaryEmailPassword.Add("kavoronkov@interpipe.biz", "kaV_201285");
            dictionaryEmailPassword.Add("aykirichko@interpipe.biz", "ayK_090785");

            if (listRolesName.Count != 0)
            {
                foreach (var item in listRolesName)
                {
                    var identityRole = new IdentityRole { Name = item };
                    roleManager.Create(identityRole);
                    listIdentityRole.Add(identityRole);
                }
            }

            for (int i = 0; i < dictionaryEmailPassword.Count; i++)
            {
                CreateAdmin(userManager, listIdentityRole, dictionaryEmailPassword.ElementAt(i).Key, dictionaryEmailPassword.ElementAt(i).Value);
            }


            base.Seed(context);
        }

        private void CreateAdmin(ApplicationUserManager manager, List<IdentityRole> listIdentityRoles, string email, string password)
        {
            var admin = new ApplicationUser { Email = email, UserName = email };
            // var admin = new ApplicationUser { Email = email, UserName = email.Remove(email.IndexOf('@')) };
            // var admin = new ApplicationUser { Email = email, UserName = email.Substring(0, email.LastIndexOf('@')) };
            var result = manager.Create(admin, password);

            if (result.Succeeded)
            {
                // // добавляем для пользователя роль
                // manager.AddToRole(admin.Id, adminRole.Name);
                // manager.AddToRole(admin.Id, userRole.Name);
                foreach (var item in listIdentityRoles)
                {
                    manager.AddToRole(admin.Id, item.Name);
                }

            }
        }
    }

}