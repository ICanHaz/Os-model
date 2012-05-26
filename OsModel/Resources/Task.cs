﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsModel.Processes;

namespace OsModel.Resources
{
    class Task : Resource
    {
        TaskInSupervisorMemory taskSource;       
        public Task(Process creator, State state, string id, TaskInSupervisorMemory taskSource)
            : base(creator, state, id)
        {
            this.taskSource = taskSource;
        }

        public string this[int i]
        {
            get
            {
                return taskSource[i];
            }

            set
            {
                taskSource[i] = value;
            }
        }
    }
}
