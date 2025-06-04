using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL;

namespace Lab2
{
    /// <summary>
    /// Клас, що відповідає за взаємодію з користувачем і керування клієнтами та страховими полісами.
    /// </summary>
    public class UI
    {
        private DataBase db;
        private List<Client> clients;
        private List<InsurancePolicy> policies;

        /// <summary>
        /// Ініціалізує новий екземпляр класу UI з базою даних і завантажує дані клієнтів та полісів.
        /// </summary>
        /// <param name="database">Об'єкт бази даних для збереження і завантаження інформації.</param>
        public UI(DataBase database)
        {
            db = database;
            clients = db.LoadClientsFromDB().ToList();
            policies = db.LoadInsurancePoliciesFromDB().ToList();
        }

        /// <summary>
        /// Додає нового клієнта після перевірки валідності введених даних.
        /// </summary>
        /// <param name="surname">Прізвище клієнта.</param>
        /// <param name="name">Ім'я клієнта.</param>
        /// <param name="phone">Телефонний номер клієнта.</param>
        /// <param name="bankNumber">Банківський номер клієнта.</param>
        public void AddClient(string surname, string name, string phone, string bankNumber)
        {
            if(!Validator.ValidatePhone(phone) && !Validator.ValidateBankNumber(bankNumber))
            {
                Console.WriteLine("Невірний телефон та банківський рахунок");
                return;
            }
            else if (!Validator.ValidatePhone(phone))
            {
                Console.WriteLine("Невірний телефон");
                return;
            }
           else if(!Validator.ValidateBankNumber(bankNumber))
            {
                Console.WriteLine("Невірний банківський рахунок");
                return;
            }

            var client = new Client(surname, name, phone, bankNumber);
            clients.Add(client);
            db.AddClient(client);
            Console.WriteLine($"Додано клієнта: {client.GetFullInfo()}");
        }

        /// <summary>
        /// Додає страховий поліс для існуючого клієнта.
        /// </summary>
        /// <param name="clientID">Ідентифікатор клієнта, для якого оформлюється поліс.</param>
        /// <param name="typeOfInsurance">Тип страхування (car, realestate, health).</param>
        /// <param name="insuranceObject">Об'єкт страхування.</param>
        /// <param name="endDate">Кінцева дата дії полісу.</param>
        /// <param name="insurancePremium">Страхова сума.</param>
        public void AddPolicy(int clientID, int typeOfInsurance, string insuranceObject, DateTime endDate, double insurancePremium)
        {
            var client = clients.FirstOrDefault(c => c.GetID() == clientID);
            if (client == null)
            {
                Console.WriteLine("Клієнта не знайдено.");
                return;
            }

            InsurancePolicy policy = null;

            switch (typeOfInsurance)
            {
                case 1:
                    policy = new CarInsurancePolicy(client, insuranceObject, endDate, insurancePremium);
                    break;
                case 2:
                    policy = new RealEstateInsurancePolicy(client, insuranceObject, endDate, insurancePremium);
                    break;
                case 3:
                    policy = new HealthInsurancePolicy(client, insuranceObject, endDate, insurancePremium);
                    break;
                default:
                    policy = null;
                    break;
            }

            if (policy == null)
            {
                Console.WriteLine("Невірний тип");
                return;
            }

            policies.Add(policy);
            db.AddInsurancePolicy(policy);
            Console.WriteLine($"Поліс додано {policy.GetInfo()}");
        }

        /// <summary>
        /// Пошук страхового полісу за його унікальним ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор полісу.</param>
        /// <returns>Поліс або null, якщо не знайдено.</returns>
        public InsurancePolicy FindPolicyByID(int id) => policies.FirstOrDefault(p => p.GetID() == id);

        /// <summary>
        /// Пошук усіх полісів за прізвищем клієнта (без урахування регістру).
        /// </summary>
        /// <param name="surname">Прізвище клієнта.</param>
        /// <returns>Список полісів, що відповідають прізвищу.</returns>
        public List<InsurancePolicy> FindPolicyBySurname(string surname) =>
            policies.Where(p => p.client.GetSurname().Equals(surname, StringComparison.OrdinalIgnoreCase)).ToList();

        /// <summary>
        /// Оновлює інформацію про страховий поліс за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор полісу для оновлення.</param>
        /// <param name="updatedPolicy">Оновлений об'єкт полісу.</param>
        public void UpdatePolicy(int id, InsurancePolicy updatedPolicy)
        {
            var index = policies.FindIndex(p => p.GetID() == id);
            if (index != -1)
            {
                policies[index] = updatedPolicy;
                db.UpdatePolicy(id, updatedPolicy);
                Console.WriteLine("Поліс оновлено.");
            }
            else
            {
                Console.WriteLine("Поліс не знайдено.");
            }
        }

        /// <summary>
        /// Виконує виплату компенсації за полісом за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор полісу.</param>
        public void GetCompensation(int id)
        {
            var policy = FindPolicyByID(id);
            if (policy == null)
            {
                Console.WriteLine("Поліс не знайдено.");
                return;
            }
            policy.PayCompensation();
            db.UpdatePolicy(id, policy);
        }

        /// <summary>
        /// Відображає інформацію про всіх клієнтів у консолі.
        /// </summary>
        public void ViewAllClients()
        {
            foreach (var c in clients)
                Console.WriteLine(c.GetFullInfo());
        }

        /// <summary>
        /// Відображає інформацію про всі страхові поліси у консолі.
        /// </summary>
        public void ViewAllPolicies()
        {
            foreach (var p in policies)
                Console.WriteLine(p.GetInfo());
        }
    }
}
