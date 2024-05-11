using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_Shop.Application.Features.Products.ViewModels;

namespace T_Shop.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQuery : IRequest<List<ProductDtos>>
    {
    }
}
