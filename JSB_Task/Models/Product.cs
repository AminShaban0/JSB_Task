namespace JSB_Task.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Descration { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
