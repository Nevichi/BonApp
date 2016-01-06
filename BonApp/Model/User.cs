using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Model
{
    public class User
    {
        public String username { get; set; }
        public int  userid { get; set; }
        public String password { get; set; }
        public List<UserFavorite> userfavorites { get; set; }

        public User()
        {

        }
    }
}
