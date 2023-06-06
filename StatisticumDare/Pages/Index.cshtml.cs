using Core.HttpDynamo;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatisticumDare.Models;
using StatisticumDare.Services.Interfaces;

namespace StatisticumDare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILudumDareService _ludumDareService;

        public LudumDareGameData? LudumDareData { get; set; }


        public IndexModel(ILogger<IndexModel> logger, ILudumDareService ludumDareService)
        {
            _logger = logger;
            _ludumDareService = ludumDareService;
        }

        public async Task OnGet()
        {
            try
            {
                var ludumUser = (string?)RouteData.Values["ludumUser"];
                if(ludumUser != null)
                    LudumDareData = await _ludumDareService.GetGameDataByUsername(ludumUser);
            }
            catch
            {
                LudumDareData = new LudumDareGameData();
            }
        }
    }

    public class Helpers {
        public static double? weightValue(double? value) {
            if (value == null) return null;
            var val = value.Value;
            double amount = 0.1;
            double remainder = val % 1;
            double baseNumber = Math.Floor(val);
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