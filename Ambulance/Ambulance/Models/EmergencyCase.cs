using System;

namespace Ambulance
{
    internal class EmergencyCase
    {
        private static int _counter = 1000;
        public string CaseNo { get; private set; }
        public Ambulance AssignedAmbulance { get; set; }
        public Priority Priority { get; private set; }
        public Patient Patient { get; private set; }
        public EmergencyStatus Status { get; private set; }

        public EmergencyCase(Patient patient, Priority priority)
        {
            _counter++;
            CaseNo = $"EMG{_counter:D4}";
            Patient = patient;
            Priority = priority;
            Status = EmergencyStatus.Created;
        }

        public void AssignAmbulance(Ambulance ambulance)
        {
            if (!ambulance.IsAvailable)
                throw new Exception("Ambulance not available");
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
