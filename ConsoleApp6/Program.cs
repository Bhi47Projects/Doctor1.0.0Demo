using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Web;
using System.Net;

namespace ConsoleApp6
{


    internal class Program
    {
        public class Disease
        {
            public List<int> DiseaseIds { get; private set; }
            public string DiseaseName { get; set; }
            public string Specialization { get; set; }
            public Disease(List<int> diseaseIds, string diseaseName, string specialization)
            {
                DiseaseIds = diseaseIds;
                DiseaseName = diseaseName;
                Specialization = specialization;
            }
        }

        public class Diagnosis
        {
            private readonly List<Disease> _diseases;

            public Diagnosis()
            {
                _diseases = new List<Disease>
         {
             new Disease(new List<int> {10000, 10300, 10040}, "Influenza Or Bacterial Infection", "Internal Medicine"),
             new Disease(new List<int> {02000, 02300, 02340}, "URTI (Upper Respiratory Tract Infection)", "Pulmonologist"),
             new Disease(new List<int> {00005, 10005}, "Food Poisoning", "Gastroenterologist"),
             new Disease(new List<int> {00300}, "Allergy", "ENT Specialist"),
             new Disease(new List<int> {00040}, "Tonsillitis", "ENT Specialist")
         };
            }

            public string CatchDiseases(int id)
            {
                bool diseaseFound = false;
                string diseaseName = "";
                foreach (var disease in _diseases)
                {
                    if (disease.DiseaseIds.Contains(id))
                    {
                        diseaseFound = true;
                        DisplayDisease(disease.DiseaseName);
                        diseaseName = disease.DiseaseName;
                        return disease.DiseaseName;
                    }
                }

                if (!diseaseFound)
                {
                    Console.WriteLine("Sorry, I can't help you. You should visit a doctor.");
                }
                return diseaseName;
            }

            public int SendDisease(string diseaseName)
            {
                if (diseaseName == "Influenza Or Bacterial Infection")
                    return 1;
                if (diseaseName == "URTI (Upper Respiratory Tract Infection)")
                    return 2;
                if (diseaseName == "Food Poisoning")
                    return 3;
                if (diseaseName == "Allergy")
                    return 4;
                if (diseaseName == "Tonsillitis")
                    return 5;

                return LogErrorAndReturnDefault();
            }

            private int LogErrorAndReturnDefault()
            {
                Console.WriteLine("Error! There is an error in the Send Disease process.");
                return -1;
            }

            public void DisplayDisease(string diseaseName)
            {
                Console.WriteLine($"I'm sad to say this, but I think from your symptoms you have: {diseaseName}");
            }
        }
        public class Medicibe
        {
            public int Id { get; set; }
            public string Treatment { get; set; }

            public Medicibe(int Id, string Treatment)
            {
                this.Id = Id;
                this.Treatment = Treatment;
            }
        }

        public class MedicineDiagnosis
        {
            private List<Medicibe> diseases;

            public MedicineDiagnosis()
            {
                string MassegeDiognosis1Frist = "- Pain relievers\r\n (paracetamol or ibuprofen).\r\n- Drink warm fluids and rest.";
                string MassegeDiognosis1Second = "- *Decongestants* (like Pseudoephedrine).\r\n- *Pain relievers*.\r\n- *Steam inhalation* or a *humidifier*.\r\n";
                string MassegeDiognosis1Third = "- *Oral Rehydration Solutions*.\r\n- *Rest* and *avoid heavy foods*.\r\n- If bacterial poisoning, *antibiotics* may be needed.";
                string MassegeDiognosis1Fourth = "- *Antihistamines* (like Loratadine or Cetirizine).\r\n- *Nasal sprays* (steroid-based or decongestant).\r\n- *Avoiding triggers* (dust, pollen).\r\n";
                string MassegeDiognosis1Fifth = "- *Antibiotics* if bacterial infection.\r\n- *Pain relievers*.\r\n- *Saltwater gargle*.\r\n";

                diseases = new List<Medicibe>
            {
                   
                new Medicibe(1, MassegeDiognosis1Frist),
                new Medicibe(2,MassegeDiognosis1Second),
                new Medicibe(3, MassegeDiognosis1Third),
                new Medicibe(4, MassegeDiognosis1Fourth),
                new Medicibe(5, MassegeDiognosis1Fifth)
            };
            }

            public string GetTreatmentById(int Id)
            {
                foreach (var disease in diseases)
                {
                    if (disease.Id == Id)
                    {
                        return disease.Treatment;
                    }
                }
                return "No matching treatment found!";
            }
        }

        public class Display
        {
            public void ShowTreatment(string treatment)
            {
                string output = $"The recommended treatment is: {treatment}";
                Console.WriteLine(output);
            }
        }

        public class Patient
        {
            public int Id { get; set; }

            public Patient(int id)
            {
                Id = id;
            }
        }

        static string ReverseString(string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        public class Doctors_Near_by
        {
            string name; //name of the doctor
            string speciality; //speciality of the doctor
            string address; //address of the doctor
            string contact; //contact number of the doctor

            public void Doctors_Near_by_set(string name, string speciality, string address, string contact)
            {
                this.name = name;
                this.speciality = speciality;
                this.address = address;
                this.contact = contact;
            }
            public void Doctors_Near_by_get()
            {
                Console.WriteLine("Name: " + name);
                Console.WriteLine("Speciality: " + speciality);
                Console.WriteLine("Address: " + address);
                Console.WriteLine("Contact: " + contact);
            }
        }
        class ExcelReaderForDoctors
        {
            public string[] namesArray = new string[6];
            public string[] specialityArray = new string[6];
            public string[] addressArray = new string[6];
            public string[] contactArray = new string[6];
            public string[] addArray = new string[6];
            public int[] specFinder = new int[6];

            public void ReadDoctorsFromExcel()
            {
                Application doctorsExcelReader = new Application();
                Workbook doctorsWorkbook = doctorsExcelReader.Workbooks.Open("C:\\Users\\hp\\source\\repos\\ConsoleApp6\\CopyofDoctors.xlsx");
                Worksheet worksheet = doctorsWorkbook.Worksheets["Sheet1"];

                Range names = worksheet.Range["A2:A7"];
                Range speciality = worksheet.Range["D2:D7"];
                Range address = worksheet.Range["C2:C7"];
                Range contact = worksheet.Range["B2:B7"];
                Range add = worksheet.Range["E2:E7"];
                Range specFinderRange = worksheet.Range["F2:F7"];

                for (int i = 0; i < 6; i++)
                {
                    namesArray[i] = names.Cells[i + 1, 1].Value;
                    specialityArray[i] = speciality.Cells[i + 1, 1].Value;
                    addressArray[i] = address.Cells[i + 1, 1].Value;
                    contactArray[i] = Convert.ToString("+" + contact.Cells[i + 1, 1].Value);
                    addArray[i] = Convert.ToString("+" + add.Cells[i + 1, 1].Value);
                    specFinder[i] = Convert.ToInt32(specFinderRange.Cells[i + 1, 1].Value);
                }
            }

            public void PrintDoctorsFromExcel(string addressFunctionParameter)
            {
                Doctors_Near_by[] doctors = new Doctors_Near_by[6];
                bool doctorFound = false;

                for (int i = 0; i < 6; i++)
                {
                    if (addressArray[i].Equals(addressFunctionParameter, StringComparison.OrdinalIgnoreCase))
                    {
                        doctors[i] = new Doctors_Near_by();
                        doctors[i].Doctors_Near_by_set(namesArray[i], specialityArray[i], addressArray[i], contactArray[i]);
                        doctors[i].Doctors_Near_by_get();
                        Console.WriteLine("-----------------------------------------");
                        doctorFound = true;
                    }
                }

                if (!doctorFound)
                {
                    Console.WriteLine("No doctors found in the specified address.");
                }
            }
        }
        public class DatabaseIn
        {
            public void TypeDataInExcelSheet(
                string[] userName,
                string[] phoneNumber,
                string[] userAge,
                string[] userHistory,
                string[] userNationality,
                string[] userAddress)
            {
                Application databaseWriter = new Application();
                Workbook databaseWorkbook = databaseWriter.Workbooks.Open("C:\\Users\\hp\\source\\repos\\ConsoleApp6\\DataBase.xlsx");
                Worksheet databaseWorksheet = databaseWorkbook.Worksheets["Sheet1"];
                int x = Convert.ToInt32(databaseWorksheet.Range["G2"].Value);
                databaseWriter.Visible = true; // to see the excel sheet

                for (int i = 0; i < userName.Length; i++)
                {
                    databaseWorksheet.Range["A" + (x + 2)].Value = userName[i];
                    databaseWorksheet.Range["B" + (x + 2)].Value = phoneNumber[i];
                    databaseWorksheet.Range["C" + (x + 2)].Value = userAge[i];
                    databaseWorksheet.Range["D" + (x + 2)].Value = userHistory[i];
                    databaseWorksheet.Range["E" + (x + 2)].Value = userNationality[i];
                    databaseWorksheet.Range["F" + (x + 2)].Value = userAddress[i];
                    x++;
                    databaseWorksheet.Range["G" + (x + 2)].Value = x;
                }
                
            }
        }
        public interface iUi
        {
            string Name();
            string PhoneNumber();
            string Age();
            string History();
            string Address();

        }
        public class User
        {
            protected string name;
            protected string phoneNumber;
            protected string age;
            protected string history;
            protected string address;
            public User(string name, string phoneNumber, string age, string history, string address)
            {
                this.name = name;
                this.phoneNumber = phoneNumber;
                this.age = age;
                this.history = history;
                this.address = address;
            }
        }
        public class MianMethodesDataEnglish : User, iUi
        {
            public MianMethodesDataEnglish(string name, string phoneNumber, string age, string history, string address) : base(name, phoneNumber, age, history, address)
            {

            }
            public string Name()
            {

                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                return name;
            }
            public string PhoneNumber()
            {
                Console.WriteLine("Enter your phone number: ");
                phoneNumber = Console.ReadLine();
                return phoneNumber;
            }
            public string Age()
            {
                Console.WriteLine("Enter your age: ");
                age = Console.ReadLine();
                return age;
            }
            public string History()
            {
                Console.WriteLine("Enter your medical history: ");
                history = Console.ReadLine();
                return history;
            }
            public string Address()
            {
                Console.WriteLine("Enter your addrees ");
                address = Console.ReadLine();
                return address;
            }

        }
        public class MianMethodesDataArabic : User, iUi
        {
            public MianMethodesDataArabic(string name, string phoneNumber, string age, string history, string address) : base(name, phoneNumber, age, history, address)
            {

            }
            public string Name()
            {
                string Reversed = ReverseString("دخل اسمك");
                Console.WriteLine(Reversed);
                name = Console.ReadLine();
                return name;
            }
            public string PhoneNumber()
            {
                string Reversed = ReverseString("رقم تليفونك");
                Console.WriteLine(Reversed);
                phoneNumber = Console.ReadLine();
                return phoneNumber;
            }
            public string Age()
            {
                string Reversed = ReverseString("ٍسنك");
                Console.WriteLine(Reversed);
                age = Console.ReadLine();
                return age;
            }
            public string History()
            {
                string Reversed = ReverseString("ـاريخك المرضي  ");
                Console.WriteLine(Reversed);
                history = Console.ReadLine();
                return history;
            }
            public string Address()
            {
                string Reversed = ReverseString("عنوانك ");
                Console.WriteLine(Reversed);
                address = Console.ReadLine();
                return address;
            }

        }
        public interface iAskingUserQuestionsAboutDeseas
        {
            int fever();
            int cough();
            int RunnyOrBlockedNose();
            int SoreThroat();
            int cramps();
        }
        public class AskingUserQuestionsAboutDeseas : iAskingUserQuestionsAboutDeseas

        {
            public int[] symptoms = new int[5];
            public int fever()
            {
                const string message = "Do you have a fever? (yes/no)";
                Console.WriteLine(message);
                string Answer = Console.ReadLine();
                if (Answer == "yes")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            public int cough()
            {
                const string message = "Do you have a cough? (yes/no)";
                Console.WriteLine(message);
                string Answer = Console.ReadLine();
                if (Answer == "yes")
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            public int RunnyOrBlockedNose()
            {
                const string message = "Do you have a RunnyOrBlockedNose? (yes/no)";
                Console.WriteLine(message);
                string Answer = Console.ReadLine();
                if (Answer == "yes")
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            public int SoreThroat()
            {
                const string message = "Do you have a SoreThroat? (yes/no)";
                Console.WriteLine(message);
                string Answer = Console.ReadLine();
                if (Answer == "yes")
                {
                    return 4;
                }
                else
                {
                    return 0;
                }
            }
            public int cramps()
            {
                const string message = "Do you have a cramps? (yes/no)";
                Console.WriteLine(message);
                string Answer = Console.ReadLine();
                if (Answer == "yes")
                {
                    return 5;
                }
                else
                {
                    return 0;
                }
            }

            public int SendDataToDeases(string code)
            {
                symptoms[0] = fever();
                symptoms[1] = cough();
                symptoms[2] = RunnyOrBlockedNose();
                symptoms[3] = SoreThroat();
                symptoms[4] = cramps();
                Console.WriteLine("");
                Console.WriteLine("Your code is: ");
                foreach (int symptom in symptoms) { code += symptom.ToString(); }
                return Convert.ToInt32(code);

            }


            static void Main(string[] args)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
                string[] userName = new string[2];
                string[] phoneNumber = new string[2];
                string[] userAge = new string[2];
                string[] userHistory = new string[2];
                string[] userNationality = new string[2];
                string[] userAddress = new string[2];
                AskingUserQuestionsAboutDeseas askingUserQuestionsAboutDeseas = new AskingUserQuestionsAboutDeseas();
                MianMethodesDataArabic mianMethodesDataArabic = new MianMethodesDataArabic(userName[1], phoneNumber[1], userAge[1], userHistory[1], userAddress[1]);
                MianMethodesDataEnglish mianMethodesDataEnglish = new MianMethodesDataEnglish(userName[1], phoneNumber[1], userAge[1], userHistory[1], userAddress[1]);
                ExcelReaderForDoctors excelReaderForDoctors = new ExcelReaderForDoctors();
                Diagnosis diagnosis = new Diagnosis();
                DatabaseIn database = new DatabaseIn();
                excelReaderForDoctors.ReadDoctorsFromExcel();
                Console.WriteLine("\t\t\t\t\t\t\r\n██████╗░░█████╗░░█████╗░████████╗░█████╗░██████╗░  ░░███╗░░░░░░█████╗░░░░░█████╗░\r\n██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗██╔══██╗  ░████║░░░░░██╔══██╗░░░██╔══██╗\r\n██║░░██║██║░░██║██║░░╚═╝░░░██║░░░██║░░██║██████╔╝  ██╔██║░░░░░██║░░██║░░░██║░░██║\r\n██║░░██║██║░░██║██║░░██╗░░░██║░░░██║░░██║██╔══██╗  ╚═╝██║░░░░░██║░░██║░░░██║░░██║\r\n██████╔╝╚█████╔╝╚█████╔╝░░░██║░░░╚█████╔╝██║░░██║  ███████╗██╗╚█████╔╝██╗╚█████╔╝\r\n╚═════╝░░╚════╝░░╚════╝░░░░╚═╝░░░░╚════╝░╚═╝░░╚═╝  ╚══════╝╚═╝░╚════╝░╚═╝░╚════╝░");
                Console.WriteLine("");
                Console.WriteLine("");

                Console.WriteLine("For english press 1 For arabic press 2");
                Console.WriteLine("-----------------------------------------");
                int coiches = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("-----------------------------------------");

                if (coiches == 1)
                {
                    userName[1] = mianMethodesDataEnglish.Name();
                    Console.WriteLine("-----------------------------------------");

                    phoneNumber[1] = mianMethodesDataEnglish.PhoneNumber();
                    Console.WriteLine("-----------------------------------------");

                    userAge[1] = mianMethodesDataEnglish.Age();
                    Console.WriteLine("-----------------------------------------");

                    userAddress[1] = mianMethodesDataEnglish.Address();
                    Console.WriteLine("-----------------------------------------");

                    userNationality[1] = "English";

                }
                else if (coiches == 2)
                {
                    userName[1] = mianMethodesDataArabic.Name();
                    Console.WriteLine("-----------------------------------------");

                    phoneNumber[1] = mianMethodesDataArabic.PhoneNumber();
                    Console.WriteLine("-----------------------------------------");

                    userAge[1] = mianMethodesDataArabic.Age();
                    Console.WriteLine("-----------------------------------------");

                    userAddress[1] = mianMethodesDataArabic.Address();
                    Console.WriteLine("-----------------------------------------");

                    userNationality[1] = "Arabic";

                }
                else
                {
                    const string message = "You did not enter a valid number";
                    Console.WriteLine(message);
                }
                User user = new User(userName[0], phoneNumber[0], userAge[0], userHistory[0], userAddress[0]);
                database.TypeDataInExcelSheet(userName, phoneNumber, userAge, userHistory, userNationality, userAddress);
                string addressTakenFromUser = userAddress[1];
                int code = askingUserQuestionsAboutDeseas.SendDataToDeases("");
                Console.WriteLine("-----------------------------------------");

                excelReaderForDoctors.PrintDoctorsFromExcel(addressTakenFromUser);

                // deseas
                string DeseasNamePrameter = diagnosis.CatchDiseases(code);

                int DeseasMainCode = diagnosis.SendDisease(DeseasNamePrameter);


                // Medicine
                MedicineDiagnosis diagnosismedicin = new MedicineDiagnosis();
                

                Display display = new Display();

                int input = DeseasMainCode;
                Patient patient = new Patient(input);
                string treatment = diagnosismedicin.GetTreatmentById(patient.Id);
                Console.WriteLine("-----------------------------------------");

                display.ShowTreatment(treatment);
                Console.WriteLine("-----------------------------------------");




            }
        }
    }
}


