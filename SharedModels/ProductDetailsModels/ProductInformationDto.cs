using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ProductDetailsModels
{
    public class ProductInformationDto
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Fit { get; set; }
        public string? Arm { get; set; }
        public string? Lenght { get; set; }
        public string? Zipper { get; set; }
        public string? ArticleNumber { get; set; }
        public string? Belt { get; set; }
        public string? Details { get; set; }
        public int ProductDetailsId { get; set; }
    }
}
