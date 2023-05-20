using Core.HttpDynamo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatisticumDare.Models;

namespace StatisticumDare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LudumDareData? LudumDareData { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            try
            {
                var ludumUser = (string)RouteData.Values["ludumUser"];
                LudumDareData = await HttpDynamo.GetRequestAsync<LudumDareData>(_httpClientFactory, $"https://projectsludumdare20221120164815.azurewebsites.net/Projects/LudumDare/Games/{ludumUser}");
            }
            catch
            {
                LudumDareData = new LudumDareData();
            }
        }
    }
}