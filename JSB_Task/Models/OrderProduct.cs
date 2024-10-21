namespace JSB_Task.Models
{
    public class OrderProduct : BaseEntity
    {
        public Order Oerder { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
