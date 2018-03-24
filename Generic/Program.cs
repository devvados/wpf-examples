using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    class A { };
    class B
    {
        public B(int i) { }
    };
    class Base<T>
    {

    }
    class Derived : Base<string>
    {

    }

    class Program
    {
        static void foo<tparam, K>(tparam arg, K ty)
        {
            Console.WriteLine(arg);
            Console.WriteLine(ty);
        }

        static void bar<T>(T arg) where T : A {}

        static void foo1<T>(T arg) where T : new() { }

        static void Main(string[] args)
        {
            int x = 10;
            double d = 1.0;

            foo<int, double>(x, d);
            foo(x, d);

            A obA = new A();
            B obB = new B(10);

            bar(obA);
            //bar(10); - error
            foo1(10);
        }
    }
}
