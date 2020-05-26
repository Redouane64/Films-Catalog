using System.Collections.Generic;

namespace Films.WebSite.Models.ViewModels
{
    public class ItemsPage<T>
    {
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public bool HasNext => (Offset + Size) < Total;
        public bool HasPrevious => Offset > 0;

        public IEnumerable<T> Items { get; set; }

        public ItemsPage(IEnumerable<T> items, int total)
        {
            Items = items;
            Total = total;
        }
    }
}
