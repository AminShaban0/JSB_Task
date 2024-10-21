namespace JSB_Task.Dtos
{
    public class OrderReturnDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IList<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();

    }
}
