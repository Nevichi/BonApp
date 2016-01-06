using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Model
{
    public class F2fResponse
    {
        public int count { get; set; }
        public List<Recipe> recipes { get; set; }

        public F2fResponse(int count, List<Recipe>recipes)
        {
            this.count = count;
            this.recipes = recipes;
        }
    }
}
