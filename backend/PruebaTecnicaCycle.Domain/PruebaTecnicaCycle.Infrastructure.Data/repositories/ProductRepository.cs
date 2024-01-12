using System;
using System.Collections.Generic;
using System.Linq;
using PruebaTecnicaCycle.Domain;
using PruebaTecnicaCycle.Domain.interfaces.repositories;
using PruebaTecnicaCycle.Infrastructure.Data.context;

namespace PruebaTecnicaCycle.Infrastructure.Data.repositories
{
    public class ProductRepository : IRepositorioBase<Product, Guid>
    {
        private readonly PruebaTecnicaCycleContext db;

        public ProductRepository(PruebaTecnicaCycleContext _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(_db));
        }

        public Product Agregar(Product entidad)
        {
            if (entidad.Id == Guid.Empty) // Asumiendo que el Id es un autoincrementable definido en la base de datos
            {
                db.Products.Add(entidad);
                db.SaveChanges();
            }
            return entidad;
        }

        public void Editar(Guid id, Product entidad)
        {
            var productoExistente = db.Products.Find(id);
            if (productoExistente != null)
            {
                productoExistente.Name = entidad.Name;
                productoExistente.Description = entidad.Description;
                productoExistente.Price = entidad.Price;
                productoExistente.CategoryId = entidad.CategoryId; // Asegúrate de que exista la propiedad CategoryId
                productoExistente.Image = entidad.Image;
                productoExistente.Status = entidad.Status; // Asegúrate de que la propiedad se llame Status

                db.Entry(productoExistente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Producto no encontrado con el ID proporcionado.");
            }
        }

        public void Eliminar(Guid id)
        {
            var productoSeleccionado = db.Products.Find(id);
            if (productoSeleccionado != null)
            {
                db.Products.Remove(productoSeleccionado);
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Producto no encontrado con el ID proporcionado.");
            }
        }

        public List<Product> Listar()
        {
            return db.Products.ToList();
        }

        public Product SeleccionarPorID(Guid entidadId)
        {
            return db.Products.Find(entidadId);
        }

        public void GuardarTodosLosCambios()
        {
            db.SaveChanges();
        }
    }
}
