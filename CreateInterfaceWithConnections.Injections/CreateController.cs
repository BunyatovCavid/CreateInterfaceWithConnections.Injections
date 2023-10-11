using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class CreateController
    {
        public static bool AddController(string name, string folder, List<string> empty, string mynamespace, List<Datas> values)
        {
            string controllername = name + "Controller";
            bool response = false;
            string response_text;

            if (name == "")
            {
                response_text = MyExceptions.FileEmptyNameException();
                Console.WriteLine(response_text);
                return response;
            }

            if (BaseMethods.CheckFile(folder, controllername))
            {
                response_text = MyExceptions.FileNameException(controllername, "Controller");
                Console.WriteLine(response_text);
                return response;
            }


            string path = "";
            string[] folders = Directory.GetDirectories(folder);

            foreach (var item in folders)
            {
                if (item.Contains("Controllers"))
                {
                    path = $@"{item}\{controllername}.cs";
                    break;
                }
                else
                {
                    path = $@"{folder}\{controllername}.cs";
                }
            }

            File.AppendAllText(path, $@"using Microsoft.AspNetCore.Mvc;");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"using {mynamespace};");
            File.AppendAllLines(path, empty);
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"namespace {mynamespace}");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "{");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, @"    [ApiController]
    [Route(""[controller]"")]");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"    public class {controllername} : ControllerBase");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "    {");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"        private readonly I{name} _{name.ToLower()};");
            File.AppendAllLines(path, empty);

            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"        public {controllername}(I{name} {name.ToLower()})");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "        {");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, $@"            _{name.ToLower()} = {name.ToLower()};");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "        }");
            File.AppendAllLines(path, empty);

            foreach (var item in values)
            {
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, $@"        {item.Protocole}");
                BaseMethods.AutoFill(path, empty, item.Signature);
            }


            File.AppendAllText(path, "    }");
            File.AppendAllLines(path, empty);
            File.AppendAllText(path, "}");
            File.AppendAllLines(path, empty);


            response_text = MyExceptions.FileNameSuccesful(name, "Controller");
            Console.WriteLine(response_text);

            response = true;

            return response;
        }
    }
}
