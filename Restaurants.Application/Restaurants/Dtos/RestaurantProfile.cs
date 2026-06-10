using AutoMapper;
using Microsoft.Extensions.Options;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(scr => scr.Address == null ? null : scr.Address.Street))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(scr => scr.Address == null ? null : scr.Address.PostalCode))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(scr => scr.Dishes));


            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        Street = src.Street,
                        PostalCode = src.PostalCode
                    }));

            //CreateMap<UpdateRestaurantCommand, Restaurant>()
            //    .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
            //    {
            //        City = src.City,
            //        Street = src.Street,
            //        PostalCode = src.PostalCode
            //    }));
            //CreateMap<UpdateRestaurantCommand, Restaurant>()
            //    .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
            //    {
            //        City = src.City,
            //        Street = src.Street,
            //        PostalCode = src.PostalCode
            //    }))
            //    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateRestaurantCommand, Restaurant>();
        }
    }
}
