using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Items
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Media { get; set; }
    }

    public class CatalogItems
    {
        public List<Item> Items { get; set; }
    }
}
