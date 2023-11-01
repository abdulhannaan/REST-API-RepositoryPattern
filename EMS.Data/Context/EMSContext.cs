using EMS.Model.DbModels.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class EMSContext : DbContext
{
	public EMSContext()
	{
	}

	public EMSContext(DbContextOptions<EMSContext> options)
		: base(options)
	{
	}
	public DbSet<Employee> Employees { get; set; }
	protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// Set up configuration from appsettings.json in the Data project
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		// Retrieve the connection string
		string connectionString =  configuration.GetConnectionString("DefaultConnection");

		optionsBuilder.UseSqlServer(connectionString);
	}
}
