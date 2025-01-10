using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels.Models
{
    public class CareAndAdvice
    {
        [Key]
        public int Id { get; set; }
        public string? CareAdvice { get; set; }
        public int ProductDetailsId { get; set; }
        public virtual ProductDetails ProductDetails { get; set; } = null!;
    }
}
