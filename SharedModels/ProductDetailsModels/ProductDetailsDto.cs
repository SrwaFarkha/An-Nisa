using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ProductDetailsModels
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int CareAndAdviceId { get; set; }
        public CareAndAdviceDto? CareAndAdvice { get; set; }

        public int ProductInformationId { get; set; }
        public ProductInformationDto? ProductInformation { get; set; }

        public int DescriptionId { get; set; }
        public DescriptionDto? Description { get; set; }

    }
}
