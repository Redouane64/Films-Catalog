using System.Collections.Generic;

namespace Films.WebSite.Models.ViewModels
{
    public class ItemsPage<T>
    {
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public bool HasNext => Offset < Total - 1;
        public bool HasPrevious => Offset > 1;

        public IEnumerable<T> Items { get; set; }

        public ItemsPage(IEnumerable<T> items, int total)
        {
            Items = items;
            Total = total;
        }
    }
}
