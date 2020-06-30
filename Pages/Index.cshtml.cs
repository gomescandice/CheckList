using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CheckList.Pages
{
    public class IndexModel : PageModel
    {
        public string Title { get; private set; } 
        public List<Record> FullList { get; set; }

        public class Records
        {
            public List<Record> records { get; set; }
        }

        public class Record
        {
            public string id { get; set; }
            public string createdTime { get; set; }
            public Item fields { get; set; }
        }
        public class Item
        {
            public string Description { get; set; }
            public bool Completed { get; set; }
        }
        
        public void OnGet()
        {
            Title += $"Shopping List";
            Records rec = new Records();
            rec = GetRequest();
            FullList = new List<Record>();
            foreach (Record r in rec.records)
            {
                FullList.Add(new Record() {id = r.id, createdTime= r.createdTime,fields= new Item() { Description = r.fields.Description, Completed = r.fields.Completed } });
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "keynRLAU0he1laXga");
            HttpResponseMessage response = await client.DeleteAsync("https://api.airtable.com/v0/app8mbWFlYecob4zb/ShoppingList/" + id);
            return RedirectToPage();
        }
        private Records GetRequest()
        {
            using var client = new HttpClient();

            var result = client.GetAsync("https://api.airtable.com/v0/app8mbWFlYecob4zb/ShoppingList?api_key=keynRLAU0he1laXga").Result;
            var jsonText = result.Content.ReadAsStringAsync().Result;
            var returnlist = JsonConvert.DeserializeObject<Records>(jsonText);
            return returnlist;

        }
    }
}
