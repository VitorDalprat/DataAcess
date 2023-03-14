using Microsoft.Data.SqlClient;
using Dapper;
using DataAcess.Models;

const string connectionString = "TrustServerCertificate=True;Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

using (var connection = new SqlConnection(connectionString)) //Váriavel criada no inicio
{
    UpdateCategory(connection);
    ListCategories(connection);
    //CreateCategory(connection); *Já Foi Criado
}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] from [Category]");
    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria destinado a serviços AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;
    var insertSql = "INSERT INTO [Category] VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

    var rows = connection.Execute(insertSql, new { category.Id, category.Title, category.Url, category.Summary, category.Order, category.Description, category.Featured });
    Console.WriteLine("Linhas Inseridas: " + rows);
}

static void UpdateCategory(SqlConnection connection)
{
    var updateQuery = "UPDATE [Category] SET [Title]=@Title WHERE [Id]=@Id";
    var rows = connection.Execute(updateQuery, new {Id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"), Title = "FrontEnd 2021" });
    Console.WriteLine("Registro Atualizados: " + rows);
}



