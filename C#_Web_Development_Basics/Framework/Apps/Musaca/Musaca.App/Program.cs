using SIS.MvcFramework;
using System;

namespace Musaca.App
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
