using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BonApp.Model;

namespace BonApp.Data
{
    class DataAccess
    {
        public DataAccess()
        {

        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://bonapp2.azurewebsites.net/api/recipes");
            string json = await response.Content.ReadAsStringAsync();
            var listRecipes = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe[]>(json);
            return listRecipes.ToList<Recipe>();
        }
    }
}
