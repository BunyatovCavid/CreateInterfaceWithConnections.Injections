using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    public static class LiblaryExtensionMethod
    {
        private static string ExtensionBaseMethod()
        {
            var l_folder = Directory.GetCurrentDirectory();
            StringBuilder s_folder = new StringBuilder();
            char[] data = new char[3];
            bool check;
            int i = 0;
            foreach (var item in l_folder)
            {
                data[i] = item;
                i++;
                if (i > 2)
                {
                    i = 0;
                }

                bool a = data.Contains('b');
                bool b = data.Contains('i');
                bool c = data.Contains('n');
                if (a && b && c)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }

                if (check)
                {
                    s_folder.Remove(s_folder.Length - 2, 2);
                    break;
                }
                s_folder.Append(item);

            }
            return s_folder.ToString();
        }
        private static string ExtensionGetNamespace(string folder)
        {
            var datas = folder.Split('\\');
            int index = datas.Length - 1;
            string request = datas[index];
            if (request.Contains('.'))
            {
                int symbolindex = request.IndexOf('.');
                StringBuilder response = new StringBuilder();
                response.Append(request);
                response.Insert(symbolindex + 1, "_");
                return response.ToString();
            }
            if (request.Contains('('))
            {
                int symbolindex = request.IndexOf('(');
                StringBuilder response = new StringBuilder();
                response.Append(request);
                response.Insert(symbolindex - 1, "_");
                return response.ToString();
            }
            if (request.Contains(')'))
            {
                int symbolindex = request.IndexOf(')');
                StringBuilder response = new StringBuilder();
                response.Append(request);
                response.Insert(symbolindex + 1, "_");
                return response.ToString();
            }
            return request;
        }
        private static bool ExtensionCheckFile(string folder, string file)
        {
            var files = Directory.GetFiles(folder);

            string check_file = folder + "\\" + file + ".cs";
            bool check = false;

            foreach (var item in files)
            {
                if (item == check_file)
                {
                    check = true;
                    break;
                }
            }


            bool check_controller = false;
            bool check_service = false;
            bool check_interface = false;
            var direcoties = Directory.GetDirectories(folder);
            foreach (var item in direcoties)
            {
                if (item.Contains("Controller"))
                    check_controller = true;
                if (item.Contains("Services"))
                    check_service = true;
                if (item.Contains("Interfaces"))
                    check_interface = true;
            }


            string b_check_file;
            if (file.Contains("Controller") && check_controller)
            {
                b_check_file = folder + "\\Controllers\\" + file + ".cs";
                string[] b_files = Directory.GetFiles(folder + "\\Controllers");
                foreach (var item in b_files)
                {
                    if (item == b_check_file)
                    {
                        check = true;
                        break;
                    }
                }
            }
            else if (file.Contains("Service") && check_service)
            {
                b_check_file = folder + "\\Services\\" + file + ".cs";
                string[] b_files = Directory.GetFiles(folder + "\\Services");
                foreach (var item in b_files)
                {
                    if (item == b_check_file)
                    {
                        check = true;
                        break;
                    }
                }
            }
            else if (file.Contains("I") && check_interface)
            {
                b_check_file = folder + "\\Interfaces\\" + file + ".cs";
                string[] b_files = Directory.GetFiles(folder + "\\Interfaces");
                foreach (var item in b_files)
                {
                    if (item == b_check_file)
                    {
                        check = true;
                        break;
                    }
                }
            }


            return check;

        }

        public static void UseUML(this IApplicationBuilder app)
        {
            string name = "UML";
            string folder = ExtensionBaseMethod();
            string mynamespace = ExtensionGetNamespace(folder);
            List<string> empty = new List<string>() { " " };


            string controllername = name + "Controller";

            if (ExtensionCheckFile(folder, controllername))
            {

            }
            else
            {


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
                File.AppendAllText(path, $@"using CreateInterfaceWithConnections.Injections;");
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
                File.AppendAllText(path, $@"        private readonly UseLiblary _use;");
                File.AppendAllLines(path, empty);
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, $@"        public {controllername}()");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "        {");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, $@"            _use = new UseLiblary();");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "        }");
                File.AppendAllLines(path, empty);

                File.AppendAllLines(path, empty);
                File.AppendAllText(path, @"        [HttpGet]");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, $@"        public void UseILiblary()");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "        {");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "           _use.UML();");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "        }");
                File.AppendAllLines(path, empty);


                File.AppendAllText(path, "    }");
                File.AppendAllLines(path, empty);
                File.AppendAllText(path, "}");
                File.AppendAllLines(path, empty);
              
            }
        }

    }
}
