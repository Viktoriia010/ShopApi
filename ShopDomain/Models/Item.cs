using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Models;

public class Item
{
    private static int _nextId = 1;
    public int Id { get; }
    public string Title { get; set; } 
    public Item(string title)
    {
        Id = _nextId++;
        Title = title;

    }
}
