using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BonApp.Model;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security;
using Windows.UI.Xaml;

namespace BonApp.Data
{
    public class AzureDataAccess
    {
        private HttpClient client;
        private App currentApp = Application.Current as App;

        public AzureDataAccess()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://bonappuwp.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            HttpResponseMessage response = await client.GetAsync("api/recipes");
            var recipes = new List<Recipe>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
            }
            return recipes;


            /*string json = await response.Content.ReadAsStringAsync();
            var listRecipes = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe[]>(json);
            return listRecipes.ToList<Recipe>();*/
        }

        public async Task<List<Recipe>> GetRecipeFavorite()
        {
            var recipes = new List<Recipe>();
            var user = new User();
            var recipe = new Recipe();
            HttpResponseMessage response = await client.GetAsync("api/users/" + currentApp.GlobalInstance.userId);
            if (response.IsSuccessStatusCode) {
                string json = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(json);
                foreach (var item in user.userfavorites) {
                    String idrecipe = item.recipeid_fav;
                    HttpResponseMessage resp2 = await client.GetAsync("api/recipes/" + idrecipe);
                    if (resp2.IsSuccessStatusCode) {
                        string json2 = await resp2.Content.ReadAsStringAsync();
                        recipes.Add(JsonConvert.DeserializeObject<Recipe>(json2));
                    }
                }
            }
            return recipes;
        }

        public async Task<bool> AddToFavorite(Recipe r) {
            string json = JsonConvert.SerializeObject(r);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/recipes", content);
            if ((response.IsSuccessStatusCode) || (response.StatusCode.ToString().Equals("Conflict"))) {
                String recipeId = r.recipe_id + currentApp.GlobalInstance.userId;
                UserFavorite uFav = new UserFavorite(recipeId, currentApp.GlobalInstance.userId, r.recipe_id);
                string jsonfav = JsonConvert.SerializeObject(uFav);
                HttpContent contentfav = new StringContent(jsonfav, Encoding.UTF8, "application/json");
                HttpResponseMessage responsefav = await client.PostAsync("api/userfavorites", contentfav);
                if (responsefav.IsSuccessStatusCode) {
                    return true;
                }
                    return false;
            }

            return false;
        }

        public async Task<bool> RemoveFavorite(Recipe r)
        {
            HttpResponseMessage response = await client.DeleteAsync("api/userfavorites/" + r.recipe_id + currentApp.GlobalInstance.userId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public async Task<String> FindUser(String user, String password)
        {
            var users = new List<User>();
            HttpResponseMessage response = await client.GetAsync("api/users");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(json);
                foreach (var item in users)
                {
                    if (item.username.Equals(user) && item.password.Equals(password))
                    {
                        currentApp.GlobalInstance.userId = item.userid;
                        Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;
                        localSetting.Values["userid"] = item.userid;
                        return "success";
                    }
                }
            }
            return "errorLogin";
        }


        public async Task<String> createUser(String user, String password)
        {
            var userToCreate = new User();
            userToCreate.username = user;
            var users = new List<User>();
            HttpResponseMessage response = await client.GetAsync("api/users");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(json);
                foreach (var item in users)
                {
                    if (item.username.Equals(user))
                    {
                        return "errorSub";
                    }
                }
            }
            userToCreate.password = password;
            userToCreate.userfavorites = new List<UserFavorite>();
            string json2 = JsonConvert.SerializeObject(userToCreate);
            HttpContent content = new StringContent(json2, Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = await client.PostAsync("api/users", content);
            if (response2.StatusCode.ToString().Equals("Conflict")){ return "errorSub"; }
            return "success";
        }



    }
}
