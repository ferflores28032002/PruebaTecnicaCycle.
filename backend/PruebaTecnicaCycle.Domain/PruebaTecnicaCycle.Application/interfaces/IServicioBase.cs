using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PruebaTecnicaCycle.Domain.interfaces;

namespace PruebaTecnicaCycle.Application.interfaces
{
    interface IServicioBase<TEntidad, TEntidadID>
         : IAgregar<TEntidad>, IEditar<TEntidad, TEntidadID>, IEliminar<TEntidadID>
    {
    }
}
