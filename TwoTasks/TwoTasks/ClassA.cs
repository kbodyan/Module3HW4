using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTasks
{
    public class ClassA
    {
        private double _result = 0;
        public event Func<double, double, double> SumEventHandler;
        public static double Sum(double x, double y) => x + y;

        public double Sum2(double x, double y) => _result += x + y;

        public void Run()
        {
            SumEventHandler += Sum;
            SumEventHandler += Sum;
            Console.WriteLine($"First method: sum = {Executor(SumEventHandler, 2, 3)}");
            SumEventHandler = Sum2;
            SumEventHandler += Sum2;
            Console.WriteLine($"Second method: sum = {Executor(SumEventHandler, 2, 3)}");
            {
                double result = 0;
                Func<double, double, double> func = (x, y) => result += x + y;
                SumEventHandler = func;
                SumEventHandler += func;

                Console.WriteLine($"Third method: sum = {Executor(SumEventHandler, 2.0, 3.0)}");
            }
        }

        public object Executor(MulticastDelegate multicastDelegate, params object[] parameters)
        {
            try
            {
                var list = multicastDelegate.GetInvocationList();
                object result = null;
                var sum = 0.0;
                foreach (var item in list)
                {
                    result = item.DynamicInvoke(parameters);
                    if (result.GetType().IsValueType)
                    {
                        sum += (double)result;
                    }
                }

                if (result.GetType().IsValueType)
                {
                    return sum;
                }

                return result;
            }
            catch
            {
                return "Exeption";
            }
        }
    }
}
