using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.DataLayer.Respositories
{
    public interface IDataPortal
    {
        IInventoryRepository InventoryRepository { get; }
        IPersistanceRepository PersistanceRepository { get; }
    }
}
