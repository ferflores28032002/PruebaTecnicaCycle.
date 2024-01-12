using System;
using System.Collections.Generic;
using System.Linq;
using PruebaTecnicaCycle.Domain;
using PruebaTecnicaCycle.Domain.interfaces.repositories;
using PruebaTecnicaCycle.Infrastructure.Data.context;

namespace PruebaTecnicaCycle.Infrastructure.Data.repositories
{
    public class CategoryRepository : IRepositorioBase<Category, Guid>
    {
        private readonly PruebaTecnicaCycleContext db;

        public CategoryRepository(PruebaTecnicaCycleContext _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(_db));
        }

        public Category Agregar(Category entidad)
        {
            entidad.CategoryId = Guid.NewGuid(); // Asumiendo que CategoryId debe ser generado aquí y no por la base de datos
            db.Category.Add(entidad);
            db.SaveChanges(); // Guardar los cambios inmediatamente o asegurarse de que se llame a GuardarTodosLosCambios después de esta operación
            return entidad;
        }

        public void Editar(Guid id, Category entidad)
        {
            var categoriaExistente = db.Category.Find(id);
            if (categoriaExistente != null)
            {
                categoriaExistente.CategoryName = entidad.CategoryName;
                // Actualizar otras propiedades si es necesario

                db.Entry(categoriaExistente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges(); // Guardar los cambios inmediatamente o asegurarse de que se llame a GuardarTodosLosCambios después de esta operación
            }
            else
            {
                throw new KeyNotFoundException("Categoría no encontrada con el ID proporcionado.");
            }
        }

        public void Eliminar(Guid entidadId)
        {
            var categoriaSeleccionada = db.Category.Find(entidadId);
            if (categoriaSeleccionada != null)
            {
                db.Category.Remove(categoriaSeleccionada);
                db.SaveChanges(); // Guardar los cambios inmediatamente o asegurarse de que se llame a GuardarTodosLosCambios después de esta operación
            }
        }

        public List<Category> Listar()
        {
            return db.Category.ToList();
        }

        public Category SeleccionarPorID(Guid entidadId)
        {
            return db.Category.Find(entidadId);
        }

        // Implementar otros métodos si la interfaz IRepositorioBase los define
        public void GuardarTodosLosCambios()
        {
            db.SaveChanges();
        }
    }
}
