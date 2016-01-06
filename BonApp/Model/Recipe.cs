using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Model
{
    public class Recipe
    {
        public String publisher { get; set; }
        public String f2f_url { get; set; }
        public String title { get; set; }
        public String source_url { get; set; }
        public String recipe_id { get; set; }
        public String image_url { get; set; }
        public double social_rank { get; set; }
        public String publisher_url { get; set; }


        public Recipe()
        {

        }

        public Recipe(String publisher, String f2f_url, String title, String source_url, String recipe_id, String image_url, double social_rank, String publisher_url)
        {
            this.publisher = publisher;
            this.f2f_url = f2f_url;
            this.title = title;
            this.source_url = source_url;
            this.recipe_id = recipe_id;
            this.image_url = image_url;
            this.social_rank = social_rank;
            this.publisher_url = publisher_url;
        }
    }
}
