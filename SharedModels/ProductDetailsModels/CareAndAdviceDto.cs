using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ProductDetailsModels
{
    public class CareAndAdviceDto
    {
        public int Id { get; set; }
        public string CareAdvice { get; set; }
        public int ProductDetailsId { get; set; }
    }
}
