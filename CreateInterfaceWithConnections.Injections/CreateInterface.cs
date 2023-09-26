using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class CreateInterface
    {
        public static bool AddInterface(string name, string folder, List<string> empty, string mynamespace, List<string> values)
        {
            string response_text;
            bool response = false;
            string interfacename = "I" + name;

            if (name == "")
            {
                response_text = MyExceptions.FileEmptyNameException();
                Console.WriteLine(response_text);
                return response;
            }

            if (BaseMethods.CheckFile(folder, interfacename))
            {
                response_text = MyExceptions.FileNameException(interfacename, "Interface");
                Console.WriteLine(response_text);
                return response;
            }

            string path = "";
            string[] folders = Directory.GetDirectories(folder);
            foreach (var item in folders)
            {
                if (item.Contains("Interfaces"))
                {
                    path = $@"{item}\{interfacename}.cs";
                    break;
                }
                else
                {
                    path = $@"{folder}\{interfacename}.cs";
                }
            }

            File.AppendAllText(path, $@"namespace {mynamespace}");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "{");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"    public interface {interfacename}");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "    {");
            File.AppendAllLines(path, empty);
            foreach (var item in values)
            {
                if (item.Contains("("))
                    File.AppendAllText(path, $@"        {item};");
                else
                    File.AppendAllText(path, $@"        {item}");
                File.AppendAllLines(path, empty);
            }
            File.AppendAllText(path, "    }");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "}");
            File.AppendAllLines(path, empty);


            response_text = MyExceptions.FileNameSuccesful(name, "Interface");
            Console.WriteLine(response_text);

            response = true;

            return response;
        }
    }
}
