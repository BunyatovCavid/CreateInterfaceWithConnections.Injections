using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class BaseMethods
    {
        private static StringBuilder GetPath(string l_folder)
        {
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
                check = Check(data);
                if (check)
                {
                    s_folder.Remove(s_folder.Length - 2, 2);
                    break;
                }
                s_folder.Append(item);

            }
            return s_folder;
        }
        public static string BaseMethod()
        {
            var l_folder = Directory.GetCurrentDirectory();
            StringBuilder s_folder = GetPath(l_folder);
            return s_folder.ToString();
        }
        public static bool Check(char[] data)
        {
            bool a = data.Contains('b');
            bool b = data.Contains('i');
            bool c = data.Contains('n');
            if (a && b && c)
            {
                return true;
            }
            return false;
        }
        public static bool CheckFile(string folder, string file)
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
        public static string GetNamespace(string folder)
        {
            var datas = folder.Split('\\');
            int index = datas.Length - 2;
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
        public static void AutoFill(string path, List<string> _empty, string item)
        {
            File.AppendAllLines(path, _empty);
            File.AppendAllText(path, $@"        {item}");
            File.AppendAllLines(path, _empty);
            File.AppendAllText(path, "        {");
            File.AppendAllLines(path, _empty);
            File.AppendAllLines(path, _empty);
            File.AppendAllText(path, "        }");
            File.AppendAllLines(path, _empty);
        }
    }
}
