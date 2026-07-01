using ShopDomain.Models;
using System.Text.Json;

namespace Shop.Api.Middleware;

public class UserCheckMiddleware(ILogger<UserCheckMiddleware> _logger, RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        //var watch = System.Diagnostics.Stopwatch.StartNew();

        // 1. Код ДО наступного компонента
        _logger.LogInformation("Початок запиту: {Path}", context.Request.Path);

        if (context.Request.Method == "POST" && context.Request.Path == "/api/user/register")
        {
            context.Request.EnableBuffering();

            var user = await context.Request.ReadFromJsonAsync<User>();

            context.Request.Body.Position = 0;
            if (user == null || user.Id != 1 || user.Login != "admin")
            {
                context.Response.StatusCode = 404;

                context.Response.ContentType = "application/json";
                var response = new
                {
                    message = "No authorization"
                };


                await context.Response.WriteAsJsonAsync(response);

                return;
            }
        }



        // 2. Передаємо керування далі
        await _next(context);

        //// 3. Код ПІСЛЯ того, як відпрацював контролер
        //watch.Stop();
        //_logger.LogInformation("Запит завершено за {Ms} мс", watch.ElapsedMilliseconds);


    }
}
