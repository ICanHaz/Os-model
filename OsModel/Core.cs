﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ookii.Collections.Generic;
using OsModel.Processes;
using OsModel.Comparers;
using OsModel.Resources;

namespace OsModel
{
    public static class Core
    {
        public static List<Process> ProcessList;
        public static PriorityQueue<Process> ReadyProcessQueue;
        public static List<Resource> ResourcesList;

        static Core()
        {
            ProcessList = new List<Process>();
            ReadyProcessQueue = new PriorityQueue<Process>(new ProcessComparer());
            ResourcesList = new List<Resource>();
        }

        void createProcess(Process process)
        {
            ProcessList.Add(process);
        }

        void deleteProcess(Process process)
        {
            process.Delete();
            //TODO: delete from ReadyProcessQueue
        }
    }
}
