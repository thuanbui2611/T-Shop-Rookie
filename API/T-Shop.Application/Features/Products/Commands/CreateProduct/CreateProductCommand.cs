﻿using MediatR;
using T_Shop.Shared.DTOs.Product.RequestModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand : ProductCreationRequestModel, IRequest<ProductResponseModel>
    {

    }
}
