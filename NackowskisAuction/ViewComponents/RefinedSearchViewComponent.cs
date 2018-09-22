using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NackowskisAuctionHouse.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NackowskisAuctionHouse.ViewComponents
{
    public class RefinedSearchViewComponent : ViewComponent
    {
        
            public async Task<IViewComponentResult> InvokeAsync(string searchString = " ")
            {
                var model = new SearchVM ();
            model.SearchParams = new List<SelectListItem>
            {
                new SelectListItem{Text="Titel", Value = "Titel"},
                new SelectListItem{Text="Beskrivning", Value = "Beskrivning"}
            };
            model.OrderBys = new List<SelectListItem>
            {
                new SelectListItem{Text="Slutdatum", Value = "Slutdatum"},
                new SelectListItem{Text="Utropspris", Value = "Utropspris"}
            };
            model.SearchString = searchString;
            //model.SearchParam = searchParam;
            //model.OrderBy = orderBy;


            return View(model);
            }
        
    }
}
