using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance
{
    internal class Program
    {
        static EmergencyService _service = new EmergencyService();

        static void Main(string[] args)
        {
            InitializeData();

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateEmergencyCase();
                        break;
                    case "2":
                        AssignAmbulance();
                        break;
                    case "3":
                        StartDispatch();
                        break;
                    case "4":
                        CompleteCase();
                        break;
                    case "5":
                        GetCaseByNumber();
                        break;
                    case "6":
                        GetAllCases();
                        break;
                    case "7":
                        FilterByStatus();
                        break;
                    case "8":
                        HighPriorityCases();
                        break;
                    case "9":
                        AvailableAmbulances();
                        break;
                    case "10":
                        SystemInfo();
                        break;
                    case "0":
                        Console.WriteLine("Exiting");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("EMERGENCY SERVICE MENU");
            Console.WriteLine("1. Create Emergency Case");
            Console.WriteLine("2. Assign Ambulance");
            Console.WriteLine("3. Start Dispatch");
            Console.WriteLine("4. Complete Case");
            Console.WriteLine("5. Get Case by CaseNo");
            Console.WriteLine("6. Get All Cases");
            Console.WriteLine("7. Filter by Status");
            Console.WriteLine("8. High Priority Cases");
            Console.WriteLine("9. Available Ambulances");
            Console.WriteLine("10. System Info");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
        }

        static void InitializeData()
        {
            var ambulance1 = new Ambulance("AZ001", "Elchin");
            var ambulance2 = new Ambulance("AZ002", "Mixail");
            var ambulance3 = new Ambulance("AZ003", "Rauf");

            _service.AddAmbulance(ambulance1);
            _service.AddAmbulance(ambulance2);
            _service.AddAmbulance(ambulance3);
        }

        static void CreateEmergencyCase()
        {
            Console.Write("Enter patient full name: ");
            string name = Console.ReadLine();

            Console.Write("Enter patient phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter patient location: ");
            string location = Console.ReadLine();

            Console.WriteLine("Priority levels: 0-Low, 1-Medium, 2-High");
            Console.Write("Enter priority: ");
            if (int.TryParse(Console.ReadLine(), out int priorityInput) && priorityInput >= 0 && priorityInput <= 2)
            {
                var patient = new Patient(name, phone, location);
                Priority priority = (Priority)priorityInput;
                var emergencyCase = _service.CreateEmergencyCase(patient, priority);
                Console.WriteLine($"Case created. Case No: {emergencyCase.CaseNo}");
            }
            else
            {
                Console.WriteLine("Invalid priority");
            }
        }

        static void AssignAmbulance()
        {
            Console.Write("Enter Case Number: ");
            string caseNo = Console.ReadLine();

            try
            {
                _service.AssignAmbulance(caseNo);
                Console.WriteLine("Ambulance assigned successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void StartDispatch()
        {
            Console.Write("Enter Case Number: ");
            string caseNo = Console.ReadLine();

            try
            {
                _service.StartDispatch(caseNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CompleteCase()
        {
            Console.Write("Enter Case Number: ");
            string caseNo = Console.ReadLine();

            try
            {
                _service.CompleteCase(caseNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GetCaseByNumber()
        {
            Console.Write("Enter Case Number: ");
            string caseNo = Console.ReadLine();

            var caseItem = _service.GetCase(caseNo);
            if (caseItem != null)
            {
                DisplayCaseInfo(caseItem);
            }
            else
            {
                Console.WriteLine("Case not found");
            }
        }

        static void GetAllCases()
        {
            var cases = _service.GetAllCases();
            if (cases.Count == 0)
            {
                Console.WriteLine("No cases found");
                return;
            }

            Console.WriteLine($"Total Cases: {cases.Count}");
            foreach (var caseItem in cases)
            {
                DisplayCaseInfo(caseItem);
                Console.WriteLine();
            }
        }

        static void FilterByStatus()
        {
            Console.WriteLine("Status options: Created, Assigned, OnRoute, Completed");
            Console.Write("Enter status to filter: ");
            string status = Console.ReadLine();

            var cases = _service.GetCasesByStatus(status);
            if (cases.Count == 0)
            {
                Console.WriteLine("No cases found with this status");
                return;
            }

            Console.WriteLine($"Cases with status {status}: {cases.Count}");
            foreach (var caseItem in cases)
            {
                DisplayCaseInfo(caseItem);
                Console.WriteLine();
            }
        }

        static void HighPriorityCases()
        {
            var cases = _service.GetHighPriorityCases();
            if (cases.Count == 0)
            {
                Console.WriteLine("No high priority cases");
                return;
            }

            Console.WriteLine($"High Priority Cases: {cases.Count}");
            foreach (var caseItem in cases)
            {
                DisplayCaseInfo(caseItem);
                Console.WriteLine();
            }
        }

        static void AvailableAmbulances()
        {
            var ambulances = _service.GetAvailableAmbulances();
            if (ambulances.Count == 0)
            {
                Console.WriteLine("No available ambulances");
                return;
            }

            Console.WriteLine($"Available Ambulances: {ambulances.Count}");
            foreach (var amb in ambulances)
            {
                Console.WriteLine($"ID: {amb.Id}, Plate: {amb.PlateNumber}, Driver: {amb.DriverName}");
            }
        }

        static void SystemInfo()
        {
            Console.WriteLine("SYSTEM INFORMATION");
            Console.WriteLine($"Total Cases: {_service.TotalCases}");
            Console.WriteLine($"Active Cases: {_service.ActiveCases}");
            Console.WriteLine($"Completed Cases: {_service.CompletedCases}");
            Console.WriteLine($"Available Ambulances: {_service.AvailableAmbulancesCount}");
        }

        static void DisplayCaseInfo(EmergencyCase caseItem)
        {
            Console.WriteLine($"Case No: {caseItem.CaseNo}");
            Console.WriteLine($"Patient: {caseItem.Patient.FullName}");
            Console.WriteLine($"Phone: {caseItem.Patient.PhoneNumber}");
            Console.WriteLine($"Location: {caseItem.Patient.Location}");
            Console.WriteLine($"Priority: {caseItem.Priority}");
            Console.WriteLine($"Status: {caseItem.Status}");
            if (caseItem.AssignedAmbulance != null)
            {
                Console.WriteLine($"Ambulance: {caseItem.AssignedAmbulance.PlateNumber}");
            }
        }
    }
}
