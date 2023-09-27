using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInterfaceWithConnections.Injections
{
    internal static class MyExceptions
    {
        public static string FileNameException(string file, string filetype)
        {
            string exception = $"Unable to create a {filetype} named {file} because a {filetype} named {file} already exists ";
            return exception;
        }
        public static string FileEmptyNameException()
        {
            string exception = $"The process cannot be executed because the name variable is empty";
            return exception;
        }
        public static string FileNameSuccesful(string file, string filetype)
        {
            string exception = $"Created {filetype} named {file}.";
            return exception;
        }
        public static string InjectionSuccesful(string file)
        {
            string exception = $"Successful I{file} and {file}Service injection";
            return exception;
        }
        public static string InjectionFailed(string file, string failedfile)
        {
            string exception = $"Failed I{file} and {file}Service injection because creation of {failedfile} failed.";
            return exception;
        }
        public static string CreatedIsFailed(string file, string filetype, string failedfile)
        {
            string exception = $"Unable create {filetype} name {file} because creation of {failedfile} failed.";
            return exception;
        }
    }
}
