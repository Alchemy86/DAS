using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAS.Domain.GoDaddy;

namespace MVC.Models.Pages
{
    public class SearchModel
    {
        public IEnumerable<Auction> Items;

        public string SearchText { get; set; }

        public int SearchLimit { get; set; }


        public SearchModel(IEnumerable<Auction> items, string searchText, int searchLimit)
        {
            this.Items = items;
            SearchText = searchText;
            SearchLimit = searchLimit;
        }

        public SearchModel()
        {
            Items = Enumerable.Empty<Auction>();
        }
    }
}
