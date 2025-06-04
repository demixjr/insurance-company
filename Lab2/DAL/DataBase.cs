using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.IO;
using Newtonsoft.Json;

namespace DAL
{
    /// <summary>
    /// Клас для роботи з базою даних клієнтів та страхових полісів.
    /// Виконує додавання, пошук, оновлення і завантаження даних з файлів JSON.
    /// </summary>
    public class DataBase
    {
        private List<Client> clients = new List<Client>();
        private List<InsurancePolicy> policies = new List<InsurancePolicy>();

        private string pathForClients = @"D:\\нау\\2 курс\\кдпз\\Lab2\\DAL\\jsonFiles\\clients.json";
        private string pathForInsurancePolicies = @"D:\\нау\\2 курс\\кдпз\\Lab2\\DAL\\jsonFiles\\policies.json";

        /// <summary>
        /// Ініціалізує базу даних шляхами до файлів клієнтів та полісів.
        /// </summary>
        /// <param name="clientsPath">Шлях до файлу з клієнтами.</param>
        /// <param name="policiesPath">Шлях до файлу зі страховими полісами.</param>
        public DataBase()
        {

            // Якщо файлу клієнтів немає — створити з порожнім масивом
            if (!File.Exists(pathForClients))
            {
                File.WriteAllText(pathForClients, "");
            }

            // Якщо файлу полісів немає — створити з порожнім масивом
            if (!File.Exists(pathForInsurancePolicies))
            {
                File.WriteAllText(pathForInsurancePolicies, "");
            }
        }


        /// <summary>
        /// Завантажує список клієнтів з JSON-файлу.
        /// </summary>
        /// <returns>Список клієнтів</returns>
        public List<Client> LoadClientsFromDB()
        {
            string json = File.ReadAllText(pathForClients);
            clients = JsonConvert.DeserializeObject<List<Client>>(json) ?? new List<Client>();
            return clients;
        }

        /// <summary>
        /// Завантажує список страхових полісів з JSON-файлу з урахуванням поліморфізму.
        /// </summary>
        /// <returns>Список страхових полісів</returns>
        public List<InsurancePolicy> LoadInsurancePoliciesFromDB()
        {
            string json = File.ReadAllText(pathForInsurancePolicies);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto  // Для коректної десеріалізації спадкоємців
            };
            policies = JsonConvert.DeserializeObject<List<InsurancePolicy>>(json, settings) ?? new List<InsurancePolicy>();
            return policies;
        }

        /// <summary>
        /// Зберігає список клієнтів у JSON-файл.
        /// </summary>
        public void SaveClientsToDB()
        {
            string json = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(pathForClients, json);
        }

        /// <summary>
        /// Зберігає список страхових полісів у JSON-файл з урахуванням поліморфізму.
        /// </summary>
        public void SavePoliciesToDB()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(policies, settings);
            File.WriteAllText(pathForInsurancePolicies, json);
        }

        /// <summary>
        /// Додає нового клієнта до списку і зберігає оновлені дані у файл.
        /// </summary>
        /// <param name="client">Об'єкт клієнта</param>
        public void AddClient(Client client)
        {
            clients.Add(client);
            SaveClientsToDB();
        }

        /// <summary>
        /// Додає новий страховий поліс до списку і зберігає оновлені дані у файл.
        /// </summary>
        /// <param name="policy">Об'єкт страхового полісу</param>
        public void AddInsurancePolicy(InsurancePolicy policy)
        {
            policies.Add(policy);
            SavePoliciesToDB();
        }

        /// <summary>
        /// Оновлює страховий поліс за індентифікатором і зберігає зміни у файл.
        /// </summary>
        /// <param name="id">ID полісу для оновлення</param>
        /// <param name="updatedPolicy">Оновлений об'єкт полісу</param>
        public void UpdatePolicy(int id, InsurancePolicy updatedPolicy)
        {
            var index = policies.FindIndex(p => p.GetID() == id);
            if (index >= 0)
            {
                policies[index] = updatedPolicy;
                SavePoliciesToDB();
            }
        }
    }
}
    
