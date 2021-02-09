using System;
using DbUsers = Zlotyy.GoldenFarmer.Database.Users;

namespace Zlotyy.GoldenFarmer.TransportModels.Users
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(DbUsers dbUser)
        {
            UserId = dbUser.UserId;
            Login = dbUser.Login;
            FullName = dbUser.FullName;
            IsAdmin = dbUser.IsAdmin;
        }

        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
