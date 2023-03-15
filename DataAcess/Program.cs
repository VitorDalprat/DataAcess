using Microsoft.Data.SqlClient;
using Dapper;
using DataAcess.Models;
using System.Data;

const string connectionString = "TrustServerCertificate=True;Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

using (var connection = new SqlConnection(connectionString)) //Váriavel criada no inicio
{
    //UpdateCategory(connection);
    //ListCategories(connection);
    //CreateCategory(connection); 
    //CreateManyCategory(connection);
    //ExecuteProcedure(connection);
    //ExecuteReadProcedure(connection);
    ExecuteScalar(connection);
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
    var rows = connection.Execute(updateQuery, new { Id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"), Title = "FrontEnd 2021" });
    Console.WriteLine("Registro Atualizados: " + rows);
}

static void CreateManyCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Description = "Categoria destinado a serviços AWS";
    category.Order = 8;
    category.Summary = "AWS Cloud";
    category.Featured = false;

    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria Nova";
    category2.Url = "categoria-nova";
    category2.Description = "Categoria nova para Iniciantes";
    category2.Order = 9;
    category2.Summary = "Categoai New";
    category2.Featured = true;
    var insertSql = "INSERT INTO [Category] VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

    var rows = connection.Execute(insertSql, new[] {new  { category.Id, category.Title, category.Url, category.Summary, category.Order, category.Description, category.Featured},
        new{category2.Id, category2.Title, category2.Url, category2.Summary, category2.Order, category2.Description, category2.Featured }
    });
    Console.WriteLine("Linhas Inseridas: " + rows);
}

static void ExecuteProcedure(SqlConnection connection)
{
    var procedure = "[spDeleteStudent]";
    var pars = new { StudentId = "896fa0f2-109d-48b9-bf6e-60e812b7275e" };
    var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);
    Console.WriteLine("Linhas Afetadas: " + affectedRows);
}

static void ExecuteReadProcedure(SqlConnection connection)
{
    var procedure = "[spGetCoursesByCategory]";
    var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
    var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

    foreach (var item in courses)
    {
        Console.WriteLine(item.Title);
    }
}

static void ExecuteScalar(SqlConnection connection)
{
    var category3 = new Category();
    category3.Title = "PARPICO";
    category3.Url = "parpica-vartz";
    category3.Description = "Destino Varmicos";
    category3.Order = 10;
    category3.Summary = "Verpico";
    category3.Featured = false;
    var insertSql = "INSERT INTO [Category] VALUES (NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured)";

    var rows = connection.Execute(insertSql, new {category3.Title, category3.Url, category3.Summary, category3.Order, category3.Description, category3.Featured });
    Console.WriteLine("Linhas Inseridas: " + rows);
}

