using AutoMapper;
using JSB_Task.Dtos;
using JSB_Task.Models;

namespace JSB_Task.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto , Product>().ReverseMap();
            CreateMap<Order, OrderReturnDto>();
            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<Product, ProductInOrderDto>();
        }
    }
}
