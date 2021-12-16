using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TwoTasks
{
    public class ClassA
    {
        private double _result = 0;
        public event Func<double, double, double> SumEventHandler;
        public event Action<Action> ActionEvent;
        public static double Sum(double x, double y) => x + y;

        public double Sum2(double x, double y) => _result += x + y;

        public void Run()
        {
            var result = 0.0;
            ActionEvent = TryCatch;
            ActionEvent += TryCatch;
            ActionEvent(() => result += Sum(2, 3));
            Console.WriteLine($"First method: sum = {result}");
            SumEventHandler = Sum2;
            SumEventHandler += Sum2;
            Console.WriteLine($"Second method: sum = {Executor(SumEventHandler, 2, 3)}");
            result = 0;
            Func<double, double, double> func = (x, y) => result += x + y;
            SumEventHandler = func;
            SumEventHandler += func;
            Console.WriteLine($"Third method: sum = {Executor(SumEventHandler, 2.0, 3.0)}");
        }

        public object Executor(MulticastDelegate multicastDelegate, params object[] parameters)
        {
            try
            {
                var list = multicastDelegate.GetInvocationList();
                object result = null;
                foreach (var item in list)
                {
                    result = item.DynamicInvoke(parameters);
                }

                return result;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public void TryCatch(Action code)
        {
            try
            {
                code();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
