using CommandLine;
using System;

namespace KeePass_HaveIBeenPwned
{
    class Program
    {
        private static Checker checker;

        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(options => Run(options))
                   .WithNotParsed(errors => HandleParseError(errors));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private static bool Run(Options options)
        {
            bool success = false;

            checker = new Checker(options);

            return success;
        }

        private static void HandleParseError(object errors)
        {
            
        }
    }
}
