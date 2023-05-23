using Core.HttpDynamo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projects.LudumDare.ViewModels;

namespace StatisticumDare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LudumDareGameData? LudumDareData { get; set; }


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
                if(ludumUser != null)
                    LudumDareData = await HttpDynamo.GetRequestAsync<LudumDareGameData>(_httpClientFactory, $"https://projectsludumdare20221120164815.azurewebsites.net/Projects/LudumDare/GameData/{ludumUser}");
            }
            catch
            {
                LudumDareData = new LudumDareGameData();
            }
        }
    }
}