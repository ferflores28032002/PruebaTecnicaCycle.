using PruebaTecnicaCycle.Application.Dtos;
using PruebaTecnicaCycle.Domain;
using PruebaTecnicaCycle.Domain.Dtos;

using PruebaTecnicaCycle.Domain.interfaces.repositories;


namespace PruebaTecnicaCycle.Application.services
{
    public class ProductServices 
    {
        private readonly IRepositorioBase<Product, Guid> repoProducto;
        private readonly S3ImageUploaderServices s3ImageUploader;

        public ProductServices(
            IRepositorioBase<Product, Guid> _repoProducto,
            S3ImageUploaderServices _s3ImageUploader) 
        {
            repoProducto = _repoProducto;
            s3ImageUploader = _s3ImageUploader;
        }


        public async Task<ProductDto> Agregar(ProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El 'Producto' es requerido.");

            if (dto.Image == null || dto.Image.Length == 0)
                throw new ArgumentException("Se debe proporcionar una imagen para el producto.");

            // Sube la imagen a S3 y obtiene la URL o el Base64
            string filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            var uploadResult = await s3ImageUploader.UploadImageAsync(filePath);
            File.Delete(filePath); // Elimina el archivo temporal

            if (!uploadResult.Success)
                throw new Exception("No se pudo subir la imagen: " + uploadResult.Message);

            var nuevoProducto = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Image = uploadResult.ImageUrl, // Aquí iría la URL de la imagen
                Status = dto.Status
            };

            // Guarda el nuevo producto en la base de datos usando el repositorio
            var resultProducto = repoProducto.Agregar(nuevoProducto);
            repoProducto.GuardarTodosLosCambios();

            // Mapea de nuevo a ProductDto si es necesario
            dto.Image = null; // El IFormFile no debe ser devuelto en la respuesta
            return dto;
        }

        public async Task Editar(Guid id, ProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El 'Producto' es requerido para editar");

            var productoExistente = repoProducto.SeleccionarPorID(id);
            if (productoExistente == null)
                throw new KeyNotFoundException("Producto no encontrado con el ID proporcionado.");

            // Actualiza los campos que se han proporcionado
            productoExistente.Name = dto.Name;
            productoExistente.Price =  dto.Price; 
            productoExistente.CategoryId = dto.CategoryId;
            productoExistente.Description = dto.Description;
            productoExistente.Status = dto.Status;


            // Manejo de la imagen
            if (dto.Image != null && dto.Image.Length > 0)
            {
                string filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                var uploadResult = await s3ImageUploader.UploadImageAsync(filePath);
                File.Delete(filePath);

                if (!uploadResult.Success)
                    throw new Exception("No se pudo subir la imagen: " + uploadResult.Message);

                productoExistente.Image = uploadResult.ImageUrl; // Actualiza la URL de la imagen
            }

            repoProducto.Editar(id, productoExistente);
            repoProducto.GuardarTodosLosCambios();
        }


        public void Eliminar(Guid id)
        {
            var producto = repoProducto.SeleccionarPorID(id);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado con el ID proporcionado.");

            repoProducto.Eliminar(id);
            repoProducto.GuardarTodosLosCambios();
        }

        public List<ProductListDto> Listar()
        {
            return repoProducto.Listar()
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Image = p.Image,
                    Status = p.Status
                                   
                })
                .ToList();
        }

        public ProductListDto SeleccionarPorID(Guid id)
        {
            var producto = repoProducto.SeleccionarPorID(id);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado con el ID proporcionado.");

            return new ProductListDto
            {
                Id = producto.Id, 
                Name = producto.Name,
                Price = producto.Price,
                CategoryId = producto.CategoryId,
                Description = producto.Description,
                Image = producto.Image, 
                Status = producto.Status
               
            };
        }
    }
}
