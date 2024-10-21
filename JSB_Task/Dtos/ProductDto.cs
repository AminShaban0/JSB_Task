using System.ComponentModel.DataAnnotations;

namespace JSB_Task.Dtos
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is Required")]
        public string Descration { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
        public decimal Price { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Stock must be non-negative")]
        public int Stock { get; set; }
    }
}
