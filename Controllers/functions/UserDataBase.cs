using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VK_Analyze.Models.EntityFramework;

namespace VK_Analyze.Controllers.functions
{
    public class UserDataBase
    {
        public static void AddUserAsync(string login,string password)
        {
            VkAnalysesDBEntities dataBase = new VkAnalysesDBEntities();
            User userContext = new User();
            userContext.Login = login;
            userContext.Password = password;
            userContext.Token = "";
            userContext.uid = dataBase.Users.Count();
            dataBase.Users.Add(userContext);
            dataBase.SaveChanges();
            dataBase.Dispose();
        }

        public static bool isDataBaseContains(User user)
        {
            VkAnalysesDBEntities dataBase = new VkAnalysesDBEntities();
            List<User> users = dataBase.Users.ToList();
            int index = users.FindIndex(x => x.Login.ToUpper() == user.Login.ToUpper() && x.Password.ToUpper() == user.Password.ToUpper());
            bool isContains =  index != -1;
            dataBase.Dispose();
            return isContains;
        }
    }
}