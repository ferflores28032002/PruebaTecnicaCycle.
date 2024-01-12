using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Domain.interfaces.repositories
{
    public interface IRepositorioBase<TEntity, TIdentity>
          : IAgregar<TEntity>, IEditar<TEntity, TIdentity>, IEliminar<TIdentity>, IListar<TEntity, TIdentity>
    {
        // La interfaz puede incluir métodos adicionales si es necesario
        void GuardarTodosLosCambios();
    }
}

