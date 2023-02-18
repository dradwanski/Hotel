using Hotel.Database.Entities;
using Hotel.Database.Seeders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Repository;
using Hotel.Database.Repository;
using Hotel.Database.Repository.UserAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Hotel.Database.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HotelDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HotelDatabase"));
            });

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IMethodOfPaymentRepository, MethodOfPaymentRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserPasswordHasher, UserPasswordHasher>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }

        public static void DatabaseCheck(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<HotelDbContext>();
            dbContext.Database.Migrate();

            var hotelSeeder = serviceProvider.GetRequiredService<HotelSeeder>();

            hotelSeeder.Seed<MethodOfPaymentSeeder, MethodOfPayment>();
            hotelSeeder.Seed<RoomTypeSeeder, RoomType>();
            hotelSeeder.Seed<RoleSeeder, Role>();
        }
    }
}
