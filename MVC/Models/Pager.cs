namespace MVC.Models
{
    public class Pager
    {
        public Pager(int maxPages, int pageNumber, int totalRecords)
        {
            MaxPages = maxPages;
            CurrentPage = pageNumber;
            IsLastPage = pageNumber == maxPages;
            IsFirstPage = pageNumber == 1;
            TotalRecords = totalRecords;
        }
        public int CurrentPage { get; protected set; }
        public int MaxPages { get; protected set; }
        public int TotalRecords { get; protected set; }
        public bool IsLastPage { get; protected set; }
        public bool IsFirstPage { get; protected set; }
    }
}
