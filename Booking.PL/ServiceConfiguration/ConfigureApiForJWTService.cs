



namespace Booking.PL.ServiceConfiguration;
public static class ConfigureApiForJWTService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddConfigureApiForJWTService(this IServiceCollection services, IConfiguration config)
    {
        // watch this vedio https://www.youtube.com/watch?v=yEQoDNHWzlE&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=74



        //   على هاي التوكن Validate وكيف يعمل jwt هون انا بقول للسستم تاعي انو يدعم ال 
        // اما عملية انشاء التوكن والتحقق منها بتكون في الكلاس اللي بعمل فيه السيرفس

        var jwtSettings = config.GetSection("JWT").Get<JWT>();


        services.AddAuthentication(options =>
        {
            // here i put JwtBearer as the default authentication scheme
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;




            // if the jwt is not default authentication scheme, we should writre => AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, b =>
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = false,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),// SymmetricSecurityKey is accept only byte array, so we need to convert the key to byte array

                ValidateLifetime = true,
            };
        });



        return services;
    }
}
