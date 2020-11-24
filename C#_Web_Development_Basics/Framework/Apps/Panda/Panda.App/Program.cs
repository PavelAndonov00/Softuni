using Panda.Models.Enums;
using SIS.MvcFramework;
using System;

namespace Panda.App
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new Startup());
        }
    }
}
