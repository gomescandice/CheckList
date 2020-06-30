using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using static CheckList.Pages.IndexModel;

namespace CheckList.Pages
{
    public class CreateModel : PageModel
    {

        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public Item1 Item { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public class Item1
        {
            public string Description { get; set; }
            public string Completed { get; set; }
        }
        public class Field
        {
            public Item1 fields { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Field> ListToCreate = new List<Field>();
            Item1 item1 = new Item1() { Description = Item.Description.ToString(), Completed = false.ToString() };
            Field field = new Field() { fields = item1 };
            ListToCreate.Add(field);
            var content = JsonConvert.SerializeObject(ListToCreate);
            var bodyContent = new StringContent(content);//, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "keynRLAU0he1laXga");
            HttpResponseMessage response = await client.PostAsync("https://api.airtable.com/v0/app8mbWFlYecob4zb/ShoppingList", bodyContent);
            return RedirectToPage("/Index");
        }
    }
}
