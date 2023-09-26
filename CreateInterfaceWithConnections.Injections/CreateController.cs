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
        public static bool AddController(string name, string folder, List<string> empty, string mynamespace, List<string> values)
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

            foreach (var item in values)
            {
                if (!item.Contains("("))
                {
                    File.AppendAllText(path, $@"        {item}");
                    File.AppendAllLines(path, empty);
                }
            }

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
                if (item.Contains("("))
                {
                    if (item.Contains("Get"))
                    {
                        File.AppendAllLines(path, empty);
                        File.AppendAllText(path, @"        [HttpGet]");
                        BaseMethods.AutoFill(path, empty, item);
                    }
                    else if (item.Contains("Post"))
                    {
                        File.AppendAllLines(path, empty);
                        File.AppendAllText(path, @"        [HttpPost]");
                        BaseMethods.AutoFill(path, empty, item);
                    }
                    else if (item.Contains("Put"))
                    {
                        File.AppendAllLines(path, empty);
                        File.AppendAllText(path, @"        [HttpPut]");
                        BaseMethods.AutoFill(path, empty, item);
                    }
                    else if (item.Contains("Patch"))
                    {
                        File.AppendAllLines(path, empty);
                        File.AppendAllText(path, @"        [HttpPatch]");
                        BaseMethods.AutoFill(path, empty, item);
                    }
                    else if (item.Contains("Delete"))
                    {
                        File.AppendAllLines(path, empty);
                        File.AppendAllText(path, @"        [HttpDelete]");
                        BaseMethods.AutoFill(path, empty, item);
                    }
                    else
                    {
                        File.AppendAllLines(path, empty);
                        BaseMethods.AutoFill(path, empty, item);
                    }
                }
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
