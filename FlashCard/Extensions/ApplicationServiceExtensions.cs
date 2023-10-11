using FlashCard.Mediator.Words;
using MediatR;

namespace FlashCard.Extentions;
public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services,
		IConfiguration config)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		
		services.AddCors(opt =>
		{
			opt.AddPolicy("CorsPolicy", policy =>
			{
				policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
			});
		});
		services.AddMediatR(typeof(ListWords.Handler));
		services.AddAutoMapper(typeof(MappingProfiles).Assembly);

		return services;
	}
}