using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class A
    {
        public A()
        {
            Console.WriteLine("pbark");
        }

        static A()
        {
            Console.WriteLine("bark");
        }
    }

    public class B : A
    {
        static B()
        {
            Console.WriteLine("Bstatic");
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#=> load");
            var a = new A();
            Console.WriteLine("#=> next load");
            var b = new A();
            Console.WriteLine("#=> next load");
            var c = new B();
            Console.WriteLine("#=> next load");
            var d = new B();
            Console.ReadKey();
        }
    }
}
