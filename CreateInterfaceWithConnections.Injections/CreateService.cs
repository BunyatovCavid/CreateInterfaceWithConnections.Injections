using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class CreateService
    {
        public static bool AddService(string name, string folder, List<string> empty, string mynamespace, List<string> values)
        {
            string servicename = name + "Service";
            bool response = false;
            string response_text;

            if (name == "")
            {
                response_text = MyExceptions.FileEmptyNameException();
                Console.WriteLine(response_text);
                return response;
            }

            if (BaseMethods.CheckFile(folder, servicename))
            {
                response_text = MyExceptions.FileNameException(servicename, "Service");
                Console.WriteLine(response_text);
                return response;
            }


            string path = "";
            string[] folders = Directory.GetDirectories(folder);
            foreach (var item in folders)
            {
                if (item.Contains("Services"))
                {
                    path = $@"{item}\{servicename}.cs";
                    break;
                }
                else
                {
                    path = $@"{folder}\{servicename}.cs";
                }
            }


            File.AppendAllText(path, $@"namespace {mynamespace}");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "{");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"    public class {servicename} : I{name}");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "    {");
            File.AppendAllLines(path, empty);
            foreach (var item in values)
            {
                if (item.Contains("("))
                {
                    BaseMethods.AutoFill(path, empty, item);
                }
                else
                {
                    File.AppendAllText(path, $@"        {item}");
                    File.AppendAllLines(path, empty);
                }
            }
            File.AppendAllText(path, "    }");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "}");
            File.AppendAllLines(path, empty);


            response_text = MyExceptions.FileNameSuccesful(name, "Service");
            Console.WriteLine(response_text);

            response = true;

            return response;
        }
    }
}
