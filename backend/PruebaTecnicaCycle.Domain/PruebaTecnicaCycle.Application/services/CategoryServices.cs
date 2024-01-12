using System;
using System.Collections.Generic;
using System.Linq;
using PruebaTecnicaCycle.Application.interfaces;
using PruebaTecnicaCycle.Domain;
using PruebaTecnicaCycle.Domain.interfaces.repositories;
using PruebaTecnicaCycle.Application.Dtos;
using System.Data.SqlClient;
using System.Data;
using PruebaTecnicaCycle.Domain.Dtos;


namespace PruebaTecnicaCycle.Application.services
{
    public class CategoryServices : IServicioBase<CategoryDto, Guid>
    {
        private readonly IRepositorioBase<Category, Guid> repoCategoria;

        public CategoryServices(IRepositorioBase<Category, Guid> _repoCategoria)
        {
            repoCategoria = _repoCategoria ?? throw new ArgumentNullException(nameof(_repoCategoria), "El repositorio de categorías es requerido.");
        }

        public CategoryDto Agregar(CategoryDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "La 'Categoría' es requerida");

            var nuevaCategoria = new Category
            {
                CategoryName = dto.CategoryName
            };

            var resultCategoria = repoCategoria.Agregar(nuevaCategoria);
            repoCategoria.GuardarTodosLosCambios();

            return new CategoryDto
            {
                CategoryName = resultCategoria.CategoryName
            };
        }

        public void Editar(Guid id, CategoryDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "La 'Categoría' es requerida para editar");
            }

            var categoriaExistente = repoCategoria.SeleccionarPorID(id);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada con el ID proporcionado.");
            }

            categoriaExistente.CategoryName = dto.CategoryName;
            repoCategoria.Editar(id, categoriaExistente);
            repoCategoria.GuardarTodosLosCambios();
        }

        public void Eliminar(Guid entityId)
        {
            var categoria = repoCategoria.SeleccionarPorID(entityId);
            if (categoria == null)
                throw new KeyNotFoundException("Categoría no encontrada con el ID proporcionado.");

            repoCategoria.Eliminar(entityId);
            repoCategoria.GuardarTodosLosCambios();
        }



        // Este es tu nuevo método que devuelve una lista de entidades Category
        public List<CategoryListDto> Listar()
        {
            return repoCategoria.Listar().Select(category => new CategoryListDto {
                            Id = category.CategoryId, CategoryName = category.CategoryName })
                        .ToList();
        }



        public CategoryListDto SeleccionarPorID(Guid entityId)
        {
            var categoria = repoCategoria.SeleccionarPorID(entityId);
            if (categoria == null)
                throw new KeyNotFoundException("Categoría no encontrada con el ID proporcionado.");

            return new CategoryListDto
            {
                Id = categoria.CategoryId,
                CategoryName = categoria.CategoryName
             
            };
        }
    }
}
