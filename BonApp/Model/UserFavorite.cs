using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Model
{
    public class UserFavorite
    {

        public String userfavorite_id { get; set; }
        public int userid_fav { get; set; }
        public String recipeid_fav { get; set; }

        public UserFavorite(String r, int s, String t)
        {
            userfavorite_id = r;
            recipeid_fav = t;
            userid_fav = s;
        }
    }
}
