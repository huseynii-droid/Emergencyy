using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance
{
    internal class EmergencyService
    {
        private List<EmergencyCase> _emergencyCases = new List<EmergencyCase>();
        private List<Ambulance> _ambulances = new List<Ambulance>();

        public void AddAmbulance(Ambulance ambulance)
        {
            _ambulances.Add(ambulance);
        }

        public EmergencyCase CreateEmergencyCase(Patient patient, Priority priority)
        {
            var newCase = new EmergencyCase(patient, priority);
            _emergencyCases.Add(newCase);
            return newCase;
        }

        public void AssignAmbulance(string caseNo)
        {
            var caseItem = _emergencyCases.FirstOrDefault(c => c.CaseNo == caseNo);
            var ambulance = _ambulances.FirstOrDefault(a => a.IsAvailable);
            if (caseItem == null || ambulance == null)
                throw new Exception("Case or ambulance not found");

            caseItem.AssignAmbulance(ambulance);
        }

        public void StartDispatch(string caseNo)
        {
            var caseItem = _emergencyCases.FirstOrDefault(c => c.CaseNo == caseNo);
            if (caseItem == null)
                throw new Exception("Case not found");

            if (caseItem.AssignedAmbulance == null)
                throw new Exception("No ambulance assigned");

            caseItem.UpdateStatus(EmergencyStatus.OnRoute);
            Console.WriteLine($"Case {caseNo} dispatch started");
        }

        public void CompleteCase(string caseNo)
        {
            var emergencyCase = _emergencyCases.FirstOrDefault(c => c.CaseNo == caseNo);
            if (emergencyCase == null)
                throw new Exception("Case not found");

            if (emergencyCase.AssignedAmbulance == null)
            {
                Console.WriteLine("No ambulance assigned");
                return;
            }

            emergencyCase.UpdateStatus(EmergencyStatus.Completed);
            emergencyCase.AssignedAmbulance.IsAvailable = true;
            Console.WriteLine($"Case {emergencyCase.CaseNo} completed");
        }

        public EmergencyCase GetCase(string caseNo)
        {
            return _emergencyCases.FirstOrDefault(c => c.CaseNo == caseNo);
        }

        public List<EmergencyCase> GetAllCases()
        {
            return _emergencyCases.ToList();
        }

        public List<EmergencyCase> GetCasesByStatus(string status)
        {
            return _emergencyCases.Where(c => c.Status.ToString() == status).ToList();
        }

        public List<EmergencyCase> GetHighPriorityCases()
        {
            return _emergencyCases.Where(c => c.Priority == Priority.High).ToList();
        }

        public List<Ambulance> GetAvailableAmbulances()
        {
            return _ambulances.Where(a => a.IsAvailable).ToList();
        }

        public int TotalCases => _emergencyCases.Count;

        public int ActiveCases => _emergencyCases.Count(c => c.Status == EmergencyStatus.Created || c.Status == EmergencyStatus.Assigned || c.Status == EmergencyStatus.OnRoute);

        public int CompletedCases => _emergencyCases.Count(c => c.Status == EmergencyStatus.Completed);

        public int AvailableAmbulancesCount => _ambulances.Count(a => a.IsAvailable);
    }
}
