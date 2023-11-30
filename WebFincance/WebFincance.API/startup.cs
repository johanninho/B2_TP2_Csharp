using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Ajoutez cette ligne pour OpenApi
using WebFincance.API.Data;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuration de la base de données en mémoire
        services.AddDbContext<FinanceContext>(options =>
            options.UseInMemoryDatabase("FinanceDb"));

        // Ajout de services pour les contrôleurs
        services.AddControllers();

        // Configuration de Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinancesAPI", Version = "v1" });
            // Ajoutez autant de configurations personnalisées ici que nécessaire
        });

        // ...autres configurations...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            // Activation de Swagger dans l'environnement de développement
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancesAPI v1"));
        }

        // Configuration de base
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
