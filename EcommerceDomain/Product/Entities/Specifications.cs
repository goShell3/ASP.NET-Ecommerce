

using System.Data;

namespace EcommerceDomain.Product.Entities
{
    public sealed class Specifications
    {
        public string display {get; private set;}
        public string processor {get; private set;}
        public string ram {get; private set;}
        public string storage {get; private set;}
        

        public Specifications (string display, string processor, string ram , string storage)
        {
            this.display = display;
            this.processor = processor;
            this.ram = ram;
            this.storage = storage;

        }
    }
}