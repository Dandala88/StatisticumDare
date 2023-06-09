using Microsoft.AspNetCore.Mvc;
using StatisticumDare.Models;

namespace StatisticumDare.Pages.Components.GameData
{
    [ViewComponent]
    public class GameDataViewComponent : ViewComponent
    {
        public GameDataViewComponent()
        {
            // we can do what we like here, including using Dependency Injection to bring in services and what not
        }
        
        public IViewComponentResult Invoke(CategoryModel category)
        {
            // here we have access to any parameters passed in when we render the ViewComponent (name in this case)
            // so we could do something like this for example...
            
            // _someService.GetUserDetails(name)
            // although in reality you'd probably want to accept an id rather than a name!
            
            return View("Default", category);
        }
    }

    public class CategoryModel {
        public Category _data { get; set; }
        public string _name { get; set; }

        public CategoryModel(string? name, Category? data) {
            _name = name ?? "Unknown";
            _data = data ?? new Category();
        }

        public double? getWeightedValue() {
            return Helpers.weightValue(this._data.AverageScore);
        }
    }
}
