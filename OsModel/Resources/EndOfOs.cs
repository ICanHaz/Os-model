﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsModel.Processes;

namespace OsModel.Resources
{
    public class EndOfOs : Resource
    {
         public EndOfOs(Process creator, State state, string id, List<string> processList)
            : base(creator, state, id, processList)
        {

        }
    }
}
