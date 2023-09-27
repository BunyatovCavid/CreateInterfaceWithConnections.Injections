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
            Console.WriteLine("Enter the interface information line by line without using \";\" and start the method name with the words \"Get/Post/Put/Patch/Delete\" if you want the http protocol to be added to your methods in the controller. When finished, type \"Yes\" in the console and press Enter.");
            Console.WriteLine("");
        }

        private List<string> GetData()
        {

            List<string> values = new List<string>();
            string check = "";
            Console.WriteLine("\nPlease enter the information that will appear in the interface.");
            Console.WriteLine();
            while (check != "Yes")
            {
                check = Console.ReadLine();
                if (check != "" && check != "Yes")
                    values.Add(check);
            }

            List<string> request = new List<string>();
            List<string> methods = new List<string>();
            List<string> properties = new List<string>();

            foreach (var item in values)
            {
                if (!item.Contains("("))
                    properties.Add(item);
            }
            foreach (var item in values)
            {
                if (item.Contains("("))
                    methods.Add(item);
            }

            foreach (var item in properties)
            {
                request.Add(item);
            }
            foreach (var item in methods)
            {
                request.Add(item);
            }

            return request;
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


            List<string> request = GetData();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Responses :");
            Console.WriteLine();

            bool check_i = false;
            bool check_s = false;
            bool check_c = false;


            check_i = CreateInterface.AddInterface(name, folder, empty, mynamespace, request);
            Console.WriteLine();
            if (check_i)
            {
                check_s = CreateService.AddService(name, folder, empty, mynamespace, request);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(MyExceptions.CreatedIsFailed(name, "Service", $"I{name}"));
                Console.WriteLine();
            }
            if (check_i && check_s)
            {
                check_c = CreateController.AddController(name, folder, empty, mynamespace, request);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(check_i? MyExceptions.CreatedIsFailed(name, "Controller", $"{name}Service") : MyExceptions.CreatedIsFailed(name, "Controller", $"I{name}"));
            }
            if (check_i && check_s)
                Console.WriteLine(Injection.PutProgram(name, mynamespace, empty));
            else
                Console.WriteLine(check_i ? MyExceptions.InjectionFailed(name, $"{name}Service") : MyExceptions.InjectionFailed(name, $"I{name}"));

        }


    }
}
