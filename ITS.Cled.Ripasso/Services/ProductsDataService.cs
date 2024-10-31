namespace ITS.Cled.Ripasso.Services;

using Dapper;
using Dapper.Contrib;
using Npgsql;

public class ProductsDataService
{
	private readonly string _connectionString;

	public ProductsDataService(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("db")
			?? throw new Exception("Missing connectionstring 'db'");
	}

}