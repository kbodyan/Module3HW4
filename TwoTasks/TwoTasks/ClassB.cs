using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTasks
{
    public class ClassB
    {
        public List<Contact> Base { get; set; } = new List<Contact>
        {
            new Contact { FirstName = "Ваня", LastName = "Дуров", Phones = new string[] { "+380675556677" } },
            new Contact { FirstName = "Петя", LastName = "Иванов", Phones = new string[] { "+380671111111" } },
            new Contact { FirstName = "Соня", LastName = "Ручка", Phones = new string[] { "+380672222222" } },
            new Contact { FirstName = "Jordg", LastName = "Washington", Phones = new string[] { "+380673333333" } },
            new Contact { FirstName = "Jon", LastName = "Vain", Phones = new string[] { "+380674444444" } },
            new Contact { FirstName = "Fam", LastName = "Thu", Phones = new string[] { "+380675555555" } },
            new Contact { FirstName = "Oliver", LastName = "Twist", Phones = new string[] { "+380506666666" } }
        };

        public void Run()
        {
            var first = Base.Where<Contact>(w => w.LastName.FirstOrDefault<char>() >= 'А' && w.LastName.FirstOrDefault<char>() <= 'Я').Select<Contact, string>(s => s.FullName);
            Console.WriteLine($"{Environment.NewLine}Первая выборка - русские фамилии:");
            foreach (var item in first)
            {
                Console.WriteLine(item);
            }

            var second = Base.OrderBy(o => o.FirstName).ThenBy(t => t.LastName);
            Console.WriteLine($"{Environment.NewLine}Вторая выборка - сортировка по имени и фамилии:");
            foreach (var item in second)
            {
                Console.WriteLine(item);
            }

            var third = Base.SelectMany(s => s.Phones, (f, p) => new { User = f.FullName, Phone = p }).Where(w => w.Phone.Contains("067"));
            Console.WriteLine($"{Environment.NewLine}Третья выборка - у кого Киевстар:");
            foreach (var item in third)
            {
                Console.WriteLine(item);
            }
        }
    }
}
