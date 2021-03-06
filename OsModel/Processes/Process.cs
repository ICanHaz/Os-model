﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ookii;
using Ookii.Collections.Generic;
using OsModel.Comparers;
using OsModel.Resources;

namespace OsModel.Processes
{
    
    public enum State { Blocked, Ready, Active }

    public abstract class Process
    {
        public string Id { get; set; }   
        public State State { get; set; }
        public int Priority { get; set; }
        public List<Resource> CreatedResources { get; set; }
        public List<Process> CreatedProcesses { get; set; }
        public Process ParentProcess { get; set; }
        public int Checkpoint { get; set; }

        public Process(int priority, State state, Process parentProcess, string id)
        {
            Id = id;
            Priority = priority;
            State = state;
            ParentProcess = parentProcess;
            CreatedProcesses = new List<Process>();
            CreatedResources = new List<Resource>();
            Checkpoint = 1;
        }

        public abstract void Execute();

        public void CreateResource(Resource resource)
        {
            CreatedResources.Add(resource);
            Core.ResourcesList.Add(resource);
        }

        public Resource RequestResource(string resourceName)
        {
            var resource = Core.ResourcesList.SingleOrDefault(r => r.Id == resourceName);
            if (resource != null && resource.State == Resources.State.Free)
            {
                resource.WaitingProcesses.Dequeue();
                resource.State = Resources.State.Occupied;
                return resource;
            }
            else
            {
                State = Processes.State.Blocked;
                if (resource != null)
                {
                    resource.WaitingProcesses.Enqueue(this);
                }
                return null;
            }
        }

        public void Delete()
        {
            CreatedResources.Clear();
            foreach (Process proc in CreatedProcesses)
                proc.Delete();
            CreatedProcesses.Clear();
        }
    }
}
