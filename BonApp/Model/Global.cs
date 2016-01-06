using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Model
{
    public class Global
    {
        private static readonly Global instance = new Global();
        public static Global Instance
        {
            get
            {
                return instance;
            }
        }
        private Global()
        {
        }
        public int userId
        {
            get;
            set;
        }
    }
}
