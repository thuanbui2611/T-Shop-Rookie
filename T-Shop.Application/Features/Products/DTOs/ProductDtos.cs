﻿using System.Text.Json.Serialization;

namespace T_Shop.Application.Features.Products.ViewModels
{
    public class ProductDtos
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("category")]
        public CategoryOfProduct Category { get; set; }
    }

    public class CategoryOfProduct
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
