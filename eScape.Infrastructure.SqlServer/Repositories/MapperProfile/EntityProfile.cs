using AutoMapper;
using eScape.Entities;
using eScape.UseCase;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.Infrastructure.SqlServer.Repositories.MapperProfile
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<SubCategory, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, SubCategory>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<ProductAttribute, ProductAttributeDTO>();
            CreateMap<ProductAttributeDTO, ProductAttribute>();

            CreateMap<ProductDetails, ProductDetailsDTO>();
            CreateMap<ProductDetailsDTO, ProductDetails>();

            CreateMap<Cart, CartDTO>();
            CreateMap<CartDTO, Cart>();

            CreateMap<Promotion, PromotionDTO>();
            CreateMap<PromotionDTO, Promotion>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RefreshTokenDTO, RefreshToken>();
        }
    }
}
