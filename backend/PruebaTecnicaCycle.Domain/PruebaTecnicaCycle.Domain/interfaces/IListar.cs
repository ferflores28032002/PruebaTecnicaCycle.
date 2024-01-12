using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Domain.interfaces
{
    public interface IListar<TDto, TEntidadID>
    {
        List<TDto> Listar();
        TDto SeleccionarPorID(TEntidadID entidadId);
    }
}


