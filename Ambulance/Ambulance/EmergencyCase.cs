using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ambulance
{
    internal class EmergencyCase
    {
        private static int _counter = 1000;
        public string CaseNo { get; private set; }
        public Ambulance? AssignedAmbulance;
        public Priority Priority { get; private set; }
        public Patient Patient { get; private set; }
        public EmergencyStatus Status
        { get; private set; }
        public EmergencyCase(Patient patient, Priority priority)

        {
            _counter++;
            CaseNo = Helper.GenerateCaseNo(_counter);
            Patient = patient;
            priority = priority;
            Status = EmergencyStatus.Created;
        }
        public void AssignAmbulance(Ambulance ambulance)
        {
            if (!ambulance.IsAvailable)
                throw new Exception("Bu ambulans musait deyil.");
            AssignedAmbulance = ambulance;
            ambulance.IsAvailable = false;
            Status = EmergencyStatus.Assigned;
        }
        public void UpdateStatus(EmergencyStatus status)
        {
            Status = status;
        }
    }
        



        
}
