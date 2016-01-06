using BonApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BonApp.Data
{
    public class F2fDataAccess
    {
        private HttpClient client;

        public F2fDataAccess()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://food2fork.com/api/search?key=217401dcb0a4ad131cd118a528ce6cb4&q=");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Recipe>> GetAllRecipes(String ingredients)
        {
            HttpResponseMessage response = await client.GetAsync(ingredients);
            string json = await response.Content.ReadAsStringAsync();
            var recipes = new List<Recipe>();
            var f2fResponse = JsonConvert.DeserializeObject<F2fResponse>(json);

            if(f2fResponse.recipes.Any())
            {
                recipes = f2fResponse.recipes;
            }

            return recipes;

        }
    }
}
