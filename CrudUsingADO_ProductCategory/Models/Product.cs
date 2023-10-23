using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO_ProductCategory.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]

        public double? Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public int Cid { get; set; }
        [ScaffoldColumn(false)]
        public string? Cname { get; set; }
        [ScaffoldColumn(false)]

        public int IsActive { get; set; }

    }
}
