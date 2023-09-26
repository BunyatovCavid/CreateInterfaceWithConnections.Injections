using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class Injection
    {
        public static string PutProgram(string name, string mynamespace, List<string> empty)
        {
            string folder = BaseMethods.BaseMethod();
            string file = folder + "\\" + "Program.cs";

            string program = File.ReadAllText(file);

            var array_program = program.Split(';');
            int using_index = 0;
            int builder_index = 0;

            foreach (var item in array_program)
            {
                if (item.Contains("using"))
                    using_index = Array.IndexOf(array_program, item);
                if (item.Contains("var app"))
                    builder_index = Array.IndexOf(array_program, item) - 1;
            }


            if (!array_program[using_index].Contains($"using {mynamespace}"))
                if (using_index == 0)
                    array_program[using_index] = $@"using {mynamespace}; 
" + array_program[using_index];
                else
                    array_program[using_index] = $@";
using {mynamespace}";
            array_program[builder_index] += $@";
builder.Services.AddScoped<I{name},{name}Service>()";

            File.Delete(file);


            foreach (var item in array_program)
            {
                File.AppendAllText(file, item + ";");
                if (item.Contains("app.Run()"))
                    break;
            }


            string response = MyExceptions.InjectionSuccesful(name);
            return response;
        }
    }
}
