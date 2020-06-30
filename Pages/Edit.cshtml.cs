using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static CheckList.Pages.IndexModel;

namespace CheckList.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        
        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }
        
        
       [BindProperty]
        public Record Record { get; set; }
       
        public async Task<IActionResult> OnGetAsync(string recordId)
        {
            using var client = new HttpClient();
        
            HttpResponseMessage result = await client.GetAsync("https://api.airtable.com/v0/app8mbWFlYecob4zb/ShoppingList?api_key=keynRLAU0he1laXga");
            var jsonText = result.Content.ReadAsStringAsync().Result;
            Records recordList = JsonConvert.DeserializeObject<Records>(jsonText);
            Record = recordList.records.Where(x => x.id == recordId).FirstOrDefault();
            if (Record == null)
            {
                RedirectToPage("/Index");
            }
            return Page();
        }

    }
}

