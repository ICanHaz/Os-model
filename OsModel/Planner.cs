﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ookii.Collections.Generic;
using OsModel.Processes;
using OsModel.Comparers;

namespace OsModel
{
    public delegate void OSEventHandler();

    public static class Planner
    {
        public static Process CurrentProcess;
        public static int GlobalTimer;
        public static event OSEventHandler ProcessExecuted;

        static Planner()
        {
        }

        private static void PickProcess()
        {
           CurrentProcess = Core.ReadyProcessQueue.Dequeue();
           CurrentProcess.State = Processes.State.Active;
        }

        private static bool ProcessIsAlreadyAdded(Process process)
        {
            var result = Core.ReadyProcessQueue.SingleOrDefault(p => p.Id == process.Id);
            return result != null;
        }

        private static void DistributeResources()
        {
            foreach (var resource in Core.ResourcesList)
            {
                if (resource.State == Resources.State.Free && resource.WaitingProcesses.Count > 0 && !ProcessIsAlreadyAdded(resource.WaitingProcesses.Peek()))
                {
                    var process = resource.WaitingProcesses.Peek();
                    process.State = State.Ready;
                    Core.ReadyProcessQueue.Enqueue(process);
                    //resource.State = Resources.State.Occupied;
                }
            }
        }

        public static void Start()
        {
            while(!Core.FinishedWork)
            {
                PickProcess();
                CurrentProcess.Execute();
                DistributeResources();    
                ProcessExecuted();
            }
        }

        public static void ExecuteNext()
        {
                PickProcess();
                CurrentProcess.Execute();
                DistributeResources();
                ProcessExecuted();
        }
    }
}
