using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Domain.interfaces
{
    public interface IEditar<TDto, TEntidadID>
    {
        void Editar(TEntidadID id, TDto dto);
    }
}

