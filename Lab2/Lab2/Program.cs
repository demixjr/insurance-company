using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Lab2
{
    /// <summary>
    /// Клас є точкою входу у програму
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Ініціалізуємо базу даних з файлами для клієнтів і полісів
            var db = new DataBase();
            // Ініціалізуємо інтерфейс користувача, який працює з базою даних
            var ui = new UI(db);

            // Основний цикл роботи програми — буде виконуватись доки користувач не вибере вихід
            while (true)
            {
                // Виводимо меню для користувача
                Console.WriteLine("1. Додати клієнта");
                Console.WriteLine("2. Додати поліс");
                Console.WriteLine("3. Переглянути усі поліси");
                Console.WriteLine("4. Переглянути усіх клієнтів");
                Console.WriteLine("5. Знайти поліс за ID");
                Console.WriteLine("6. Знайти поліс за прізвищем");
                Console.WriteLine("7. Виплата компенсацій");
                Console.WriteLine("8. Оновити поліс");
                Console.WriteLine("0. Вихід");

                // Зчитуємо вибір користувача
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Додавання нового клієнта
                        Console.Write("Прізвище: ");
                        var surname = Console.ReadLine();

                        Console.Write("Ім'я: ");
                        var name = Console.ReadLine();

                        Console.Write("Телефон: ");
                        var phone = Console.ReadLine();

                        Console.Write("Банківський номер: ");
                        var bank = Console.ReadLine();

                        // Виклик методу інтерфейсу користувача для додавання клієнта
                        ui.AddClient(surname, name, phone, bank);
                        break;

                    case "2":
                        // Додавання нового страхового полісу
                        Console.Write("Введіть ID клієнта: ");
                        // Безпечне перетворення введеного тексту у число
                        if (!int.TryParse(Console.ReadLine(), out int clientId))
                        {
                            Console.WriteLine("Некоректний формат ID");
                            break; // Повертаємося до меню
                        }

                        Console.Write("Тип об'єкту (1 - автомобіль, 2 - нерухомість, 3 - здоров'я): ");
                        var type = int.Parse(Console.ReadLine());

                        Console.Write("Об'єкт страхування: ");
                        var obj = Console.ReadLine();

                        Console.Write("Кінцева дата (yyyy-MM-dd): ");
                        // Перевірка правильності формату дати
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                        {
                            Console.WriteLine("Некоректний формат дати");
                            break;
                        }

                        Console.Write("Страхова сума: ");
                        // Перевірка правильності формату суми
                        if (!double.TryParse(Console.ReadLine(), out double premium))
                        {
                            Console.WriteLine("Некоректний формат суми");
                            break;
                        }

                        // Додаємо поліс через UI
                        ui.AddPolicy(clientId, type, obj, endDate, premium);
                        break;

                    case "3":
                        // Відображення всіх страхових полісів
                        ui.ViewAllPolicies();
                        break;

                    case "4":
                        // Відображення всіх клієнтів
                        ui.ViewAllClients();
                        break;

                    case "5":
                        // Пошук поліса за ID
                        Console.Write("ID полісу: ");
                        if (!int.TryParse(Console.ReadLine(), out int policyId))
                        {
                            Console.WriteLine("Некоректний формат ID");
                            break;
                        }

                        var policy = ui.FindPolicyByID(policyId);
                        if (policy != null)
                            Console.WriteLine(policy.GetInfo());
                        else
                            Console.WriteLine("Поліс не знайдено");
                        break;

                    case "6":
                        // Пошук полісів за прізвищем клієнта
                        Console.Write("Прізвище клієнта: ");
                        var sname = Console.ReadLine();

                        var list = ui.FindPolicyBySurname(sname);
                        if (list.Count == 0)
                            Console.WriteLine("Поліси не знайдено");
                        else
                            foreach (var p in list)
                                Console.WriteLine(p.GetInfo());
                        break;

                    case "7":
                        // Виплата компенсації за полісом за ID
                        Console.Write("ID полісу: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Некоректний формат ID");
                            break;
                        }

                        var policyDeact = ui.FindPolicyByID(pid);
                        ui.GetCompensation(pid);
                        db.UpdatePolicy(pid, policyDeact);
                        break;
                    case "8":
                        Console.Write("ID полісу: ");
                        if (!int.TryParse(Console.ReadLine(), out int policyID))
                        {
                            Console.WriteLine("Некоректний формат ID");
                            break;
                        }

                        var policyUpd = ui.FindPolicyByID(policyID);
                        if (policyUpd != null)
                        {
                            Console.Write("Кінцева дата (yyyy-MM-dd): ");
                            // Перевірка правильності формату дати
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDateU))
                            {
                                Console.WriteLine("Некоректний формат дати");
                                break;
                            }

                            Console.Write("Страхова сума: ");
                            // Перевірка правильності формату суми
                            if (!double.TryParse(Console.ReadLine(), out double premiumU))
                            {
                                Console.WriteLine("Некоректний формат суми");
                                break;
                            }
                            policyUpd.ChangeEndDate(endDateU);
                            policyUpd.ChangeInsurancePremium(premiumU);
                            // Додаємо поліс через UI
                            ui.UpdatePolicy(policyID, policyUpd);
                            break;
                        }
                        else
                            Console.WriteLine("Поліс не знайдено");
                        break;
                    case "0":
                        // Вихід із програми
                        return;

                    default:
                        // Якщо вибір користувача не відповідає жодному пункту
                        Console.WriteLine("Неправильний вибір");
                        break;
                }

                // Порожній рядок для відділення операцій в консолі
                Console.WriteLine();
            }
        }
    }
}

