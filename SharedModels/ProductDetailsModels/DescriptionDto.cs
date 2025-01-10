using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ProductDetailsModels
{
    public class DescriptionDto
    {
        public int Id { get; set; }
        public string? Material { get; set; }
        public string? Fabric { get; set; }
        public string? OurModel { get; set; }
        public string? ModelSize { get; set; }
        public int ProductDetailsId { get; set; }
    }
}
