using Microsoft.EntityFrameworkCore;
using Note.Api.Data;

namespace Note.Api
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
			var note = builder.Configuration.GetConnectionString("NoteConnection");

			builder.Services.AddDbContext<NotesDbContext>(n => n.UseSqlServer(note));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
