using Core.HttpDynamo;
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

    public class Helpers {
        public static double weightValue(double value) {
            double amount = 0.1;
            double remainder = value % 1;
            double baseNumber = Math.Floor(value);
            double weightedRemaineder = 0;
            if (value <= 1) {
                weightedRemaineder = (remainder * (1-amount/2));
            } else 
            if (value >= 4) {
                weightedRemaineder = (remainder * (1-amount/2) + (amount/2));
            } else {
                weightedRemaineder = (remainder * (1-amount) + (amount/2));
            }
            
            return (baseNumber + weightedRemaineder)/5*100;
        }
    }
}