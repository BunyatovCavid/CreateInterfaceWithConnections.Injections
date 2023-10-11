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

            int builder_index = 0;
            int array_index = 0;

            foreach (var item in array_program)
            {
                if (item.Contains("using"))
                    array_index = Array.IndexOf(array_program, item);
                if (item.Contains("var app"))
                    builder_index = Array.IndexOf(array_program, item) - 1;
            }

            if (!array_program.Contains(mynamespace))
                array_program[array_index] += $@";
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
