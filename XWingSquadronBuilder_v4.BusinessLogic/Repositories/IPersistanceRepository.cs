﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWingSquadronBuilder_v4.BusinessLogic.Repositories
{
    public interface IPersistanceRepository
    {
        Task SaveToStorageAsync();
    }
}
