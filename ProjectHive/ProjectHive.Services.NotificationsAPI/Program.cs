

using ProjectHive.Services.AuthAPI;
using ProjectHive.Services.NotificationsAPI.Controllers;
using ProjectHive.Services.NotificationsAPI.Hubs;

namespace ProjectHive.Services.NotificationsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterServicesForNotifications(builder.Configuration);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                app.MapHub<NotificationHub>("/notificationHub");
            });

            app.RunAsync();
        }
    }
}
