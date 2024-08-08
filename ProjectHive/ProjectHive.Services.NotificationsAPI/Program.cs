
using ProjectHive.Services.NotificationsAPI.Handlers;

namespace ProjectHive.Services.NotificationsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            var webSocketHandler = new WebSocketHandler();
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await webSocketHandler.Echo(webSocket);
                }
                else
                {
                    await next();
                }
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.RunAsync();
        }
    }
}
