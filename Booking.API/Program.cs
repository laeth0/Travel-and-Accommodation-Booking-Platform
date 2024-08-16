


using Booking.API.Middlewares;
using Booking.Application;
using Booking.Infrastrature;
using Booking.Infrastrature.DataSeeding;

namespace Booking.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //i create object from WebApplicationBuilder to determine the configuration of the application
        //dependency injection  هو عبارة عن وعاء بضع فيه الخدمات يلي رح تكون متاحة في المشروع تاعي عشان استعملها فيما بعد عن طريق  Services object  ال 


        builder.Logging.AddDefaultLoggingService();


        builder.Services.AddWebComponents()
            .AddInfrastructure(builder.Configuration)
            .AddApplication();




        var app = builder.Build();
        // any thing that start with app.Use is a middleware(او بتضل مسكرة request واما هاي البوابة بتفتح امام  request عبارة عن بوابة يمر فيها ال middleware ال )
        /*
         request يلي رح تعالج هاد ال action method يلي داخل وال request هي عبارة عن سوفتوير وسيط بين ال middleware ال  
         خارج بمرق من عنده response بدخل بمرق عنه وكل request وكل action method يعني زي كانو شخص واقف على باب ال
          middleware هو عبارة عن  app.Use اي اشي ببدا في 
        */


        app.UseMiddleware<ExceptionHandlingMiddleware>();// must be the first middleware, to ensure exceptions at all levels are handled

        app.UseHttpsRedirection();// This line configures the application to automatically redirect HTTP requests to HTTPS. This is a common practice for improving security. (HTTPS الى HTTP تاعي وبحولو من  request بوخذ ال )

        app.UseStaticFiles();

        app.UseRouting();// This line configures the middleware to enable routing. Routing determines how URLs are mapped to controllers and actions.

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();// This line configures the middleware to enable authorization. It indicates that the application should process authorization requirements. (configuration يعني مش بفعل الحماية. عشان افعل الحماية لازم اضيف كمان   middleware هون بس بضيف ال ) 

        app.Migrate();

        /*
         MapControllers :-
             تاعتها URL على mapping يلي جواتها وبعملها EndPoint بوخذ Controllers هون بوخذ لفه على كل 
             تاعتها EndPoint باشر على URL يعني كل
             Route Table وبعدين بطلع منها Route Builder على mapping يعني بوخذ الميثود وبعملها 
         */
        app.MapControllers();


        //if(app.Environment.IsDevelopment()) // ⇒ This line checks the current environment and only enables the Swagger UI in the Development environment. This is a common practice to prevent the Swagger UI from being exposed in production environments.
        //{    كلهن وقتها رح يكون عندة تصور للمشروع ويغدر يخترق المشروقع تاعي EndPoint لانو اذا شخص قدر يشوف ال deploy ما بنظهر واجهة سواجر في ال
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Booking Platform";
            // Swagger وبحولو للواجهة المعروفة تاعت Json file رح يوخذ Swagger بتاع UI وقتها app.UseSwaggerUI  ولما انا بنادي على json file رح يطلع Swagger في النهاية 
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Platform");

        });
        //}

        await app.SeedRoles();
        await app.SeedUsers();
        await app.SeedAmenity();
        await app.SeedRoomType();
        await app.SeedResidenceType();

        app.MapGet("/", () => "Hello World!");

        app.Run();// بتشغل البرنامج وبضلو البرنامج واقف هون لحتى ينتهي البرنامج وبس اطفأ البرنامج بروح ينفذ الاسطر يلي بعد هاد السطر
        /*
         lunch Profiles => Web Server كل بروفايل بحدد لما بدي اشغل فجوال ستديو رح يشتغل ازاي يعني ؤح يحدد اي 
            Type of Web Server in Asp.net
            1.	IIS Express : هو القديد وبشتغل على وندوز فقط
            2.	Kestrel : default in asp.net core هو الجديد وبشتغل على كل الانظمة وهو 
         */
    }
}
