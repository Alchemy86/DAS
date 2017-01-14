using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAS.Domain.GoDaddy;

namespace MVC.Models.Pages
{
    public class SearchModel : IEnumerable<Auction>
    {
        private readonly IEnumerable<Auction> items;

        public string SearchText { get; set; }

        public int SearchLimit { get; set; }

        public IEnumerator<Auction> GetEnumerator()
        {
            return items.Skip(0).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SearchModel(IEnumerable<Auction> items, string searchText, int searchLimit)
        {
            this.items = items;
            SearchText = searchText;
            SearchLimit = searchLimit;
        }

        public SearchModel()
        {
            items = Enumerable.Empty<Auction>();
        }
    }
}
