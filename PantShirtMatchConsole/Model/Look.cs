using Newtonsoft.Json;
using System.Collections.Generic;

namespace PantShirtMatchConsole
{
    public class Look
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Pant Pant { get; set; }
        public virtual Shirt Shirt { get; set; }
    }
}
