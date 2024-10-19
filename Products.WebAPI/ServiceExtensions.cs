namespace Products.WebAPI
{
	public static class ServiceExtensions
	{
		public static void ConfigureWebAPI(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(policy => policy
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
				);
			});
		}

		public static void ConfigureWebAPI(this WebApplication webApplication)
		{
			webApplication.UseCors();
		}
	}
}
