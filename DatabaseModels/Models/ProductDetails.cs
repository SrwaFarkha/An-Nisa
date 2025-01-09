using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels.Models
{
	public class ProductDetails
	{
        [Key, ForeignKey("Product")]

        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int CareAndAdviceId { get; set; }
        public virtual CareAndAdvice CareAndAdvice { get; set; }

        public int ProductInformationId { get; set; }
        public virtual ProductInformation ProductInformation { get; set; }
        public int DescriptionId { get; set; }
        public virtual Description Description { get; set; }



        //public string Material { get; set; }
        //public string Fabric { get; set; }

        //public string CareAdvice { get; set; }
        //public string Belt { get; set; }
        //public string Zipper { get; set; }
        //public string ArticleNumber { get; set; }
        //public string OurModel { get; set; }
        //public string Fit { get; set; }
        //public string Arm { get; set; }
        //public string Lenght { get; set; }
        //public string ModelSize { get; set; }
        //public string Details { get; set; }
        //public string Color { get; set;}

    }
}
