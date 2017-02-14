using Autofac;
using FluentNHibernate.Automapping;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.DI;
using QMand;
using ProjectA.Configuration.Base.Enums;
using System;

namespace ProjectA.Console
{
    public class Program
    {
        private static void BootDI()
        {
            ConfigurationBootstraper.Load(new ContainerBuilder(), new AppSettings());
        }

        static void Main(string[] args)
        {
            BootDI();

            var marshal = new CommandMarshal();
            marshal.RegisterCommandAssembly(typeof(Program).Assembly);

            if (args.Length > 0)
            {
                foreach (var line in args)
                {
                    marshal.ExecuteCommandString(line);
                }
            }
            else
            {
                while (true)
                {
                    System.Console.Write(">");
                    var line = System.Console.ReadLine();
                    marshal.ExecuteCommandString(line);
                }
            }
        }
    }
}
