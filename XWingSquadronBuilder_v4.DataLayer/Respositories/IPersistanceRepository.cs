using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XWingSquadronBuilder_v4.DataLayer.RawData;

namespace XWingSquadronBuilder_v4.DataLayer.Respositories
{
    public interface IPersistanceRepository
    {
        void SaveSquadron(SquadronSaveData squadron);
        List<SquadronSaveData> SavedSquadrons { get; }
        Task SaveDataToDrive(IEnumerable<SquadronSaveData> saveData);
    }
}