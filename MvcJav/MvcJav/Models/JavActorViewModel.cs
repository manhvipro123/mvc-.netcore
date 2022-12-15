using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcJav.Models
{
    public class JavActorViewModel
    {
        public List<Jav>? Javs { get; set; }
        public SelectList? Actors { get; set; }
        public string? JavActor { get; set; }
        public string? SearchString { get; set; }
    }
}
