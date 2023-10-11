using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    public class UseLiblary
    {
        static UseLiblary()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter the interface information line by line without using \\\";\\\". Only methods starting with [HttpGet/...] are created in the controller. When finished, type \\\"yes\\\" in the console and press Enter.\r\nExample : [HttpGet] public IActionResult GetStudents()");
            Console.WriteLine("");
        }
        private List<string> GetData()
        {
            string check = "";
            List<string> response = new List<string>();

            Console.WriteLine("\nPlease enter the information.");
            Console.WriteLine();

            while (check.ToLower() != "yes")
            {
                check = Console.ReadLine();
                if (check.ToLower() != "yes")
                    response.Add(check);
            }
            return response;
        }
        private List<string> GetForInterfaceData(List<string> values)
        {
            List<string> request = new List<string>();
            int s_index = 0;
            int e_index = 0;
            string _word;

            foreach (var item in values)
            {

                if (item.Contains("[") || item.Contains("]"))
                {
                    _word = "";
                    foreach (var word in item)
                    {
                        if (word == '[') s_index = item.IndexOf(word);
                        else if (word == ']') e_index = item.IndexOf(word);
                    }

                    for (int i = 0; i < item.Length; i++)
                    {
                        if (i < s_index)
                        {
                            _word += item[i];
                        }

                        else if (i > e_index+1)
                        {
                            _word += item[i];
                        }
                    }

                    request.Add(_word);
                }
                else
                {
                    request.Add(item);
                }

            }

            List<string> response = new List<string>();
            List<string> methods = new List<string>();
            List<string> properties = new List<string>();

            foreach (var item in request)
            {
                if (!item.Contains("("))
                {
                    properties.Add(item);
                }
            }
            foreach (var item in request)
            {
                if (item.Contains("("))
                {
                    methods.Add(item);
                }
            }

            foreach (var item in properties)
            {
                response.Add(item);
            }
            foreach (var item in methods)
            {

                response.Add(item);
            }

            return response;
        }

        private List<Datas> GetForControllerData(List<string> values)
        {
            List<Datas> response = new List<Datas>();
            int s_index = 0;
            int e_index = 0;
            string _signature_word;
            string _protocole_word;

            foreach (var item in values)
            {
                if (item.Contains("("))
                {
                    if (item.Contains("[") || item.Contains("]"))
                    {
                        _signature_word = "";
                        _protocole_word = "";

                        foreach (var word in item)
                        {
                            if (word == '[') s_index = item.IndexOf(word);
                            else if (word == ']') e_index = item.IndexOf(word);
                        }

                        for (int i = 0; i < item.Length; i++)
                        {
                            if (i < s_index || i > e_index+1) _signature_word += item[i];
                        }

                        for (int i = 0; i <= e_index; i++)
                        {
                            if (i >= s_index || i <= e_index) _protocole_word += item[i];
                        }

                        response.Add(new Datas() { Protocole = _protocole_word, Signature = _signature_word });

                    }

                }
            }

            return response;
        }
        public void UML()
        {
            string folder = BaseMethods.BaseMethod();
            string mynamespace = BaseMethods.GetNamespace(folder);
            List<string> empty = new List<string>() { " " };


            Console.WriteLine();
            Console.WriteLine("\nPlease enter the name of the interface.");
            Console.WriteLine();
            Console.Write("Name : ");
            string name = Console.ReadLine();


            List<string> base_data = GetData();

            List<string> request_for_interface = GetForInterfaceData(base_data);
            List<Datas> request_for_controller = GetForControllerData(base_data);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Responses :");
            Console.WriteLine();

            bool check_i = false;
            bool check_s = false;
            bool check_c = false;


            check_i = CreateInterface.AddInterface(name, folder, empty, mynamespace, request_for_interface);
            Console.WriteLine();
            if (check_i)
            {
                check_s = CreateService.AddService(name, folder, empty, mynamespace, request_for_interface);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(MyExceptions.CreatedIsFailed(name, "Service", $"I{name}"));
                Console.WriteLine();
            }
            if (check_i && check_s)
            {
                check_c = CreateController.AddController(name, folder, empty, mynamespace, request_for_controller);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(check_i ? MyExceptions.CreatedIsFailed(name, "Controller", $"{name}Service") : MyExceptions.CreatedIsFailed(name, "Controller", $"I{name}"));
            }
            if (check_i && check_s)
                Console.WriteLine(Injection.PutProgram(name, mynamespace, empty));
            else
                Console.WriteLine(check_i ? MyExceptions.InjectionFailed(name, $"{name}Service") : MyExceptions.InjectionFailed(name, $"I{name}"));

        }


    }
}
