using System;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCycle.Infrastructure.Data.context;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creando la DB si no existe...");
        using (var db = new PruebaTecnicaCycleContext())
        {
            db.Database.EnsureCreated();
            CrearStoredProcedures(db);
        }
        Console.WriteLine("Listo!!!!!");
        Console.ReadKey();
    }

    private static void CrearStoredProcedures(PruebaTecnicaCycleContext db)
    {
        // Crear el SP para obtener todas las categorías
        var spGetAllCategories = @"
        IF OBJECT_ID('GetAllCategories', 'P') IS NULL
        BEGIN
            EXEC('CREATE PROCEDURE GetAllCategories AS BEGIN SELECT * FROM Category; END');
        END";

        db.Database.ExecuteSqlRaw(spGetAllCategories);
     

        // Crear el SP para obtener todos los productos
        var spGetAllProducts = @"
        IF OBJECT_ID('GetAllProducts', 'P') IS NULL
        BEGIN
            EXEC('CREATE PROCEDURE GetAllProducts AS BEGIN SELECT * FROM Products; END');
        END";

        db.Database.ExecuteSqlRaw(spGetAllProducts);
      
    }
}
