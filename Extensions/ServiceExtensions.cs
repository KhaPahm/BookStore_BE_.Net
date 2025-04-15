using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Repository;
using BookStore.Services;

namespace BookStore.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookImageRepository, BookImageRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewImageRepository, ReviewImageRepository>();
            services.AddScoped<IReviewLikeRepository, ReviewLikeRepository>();
            services.AddScoped<IReviewReplyRepository, ReviewReplyRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IPaypalService, PaypalService>();
        }
    }
}