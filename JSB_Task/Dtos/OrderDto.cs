using System.Collections;

namespace JSB_Task.Dtos
{
    public class OrderDto
    {
        public int CustomerId { get; set; }

        public IList<int> ProductId { get; set; }=new List<int>();

    }
}
