using System;
using Newtonsoft.Json;
namespace BLL
{
    /// <summary>
    /// Абстрактний клас страхового полісу.
    /// Зберігає базову інформацію про поліс та клієнта.
    /// </summary>
    public abstract class InsurancePolicy
    {
        private static int nextId = 1; // Автоінкремент ID полісу
        [JsonProperty]
        protected int id;
        [JsonProperty]
        protected int clientID;
        [JsonProperty]
        public Client client;
        [JsonProperty]
        public string TypeOfInsurance { get; protected set; }
        [JsonProperty]
        protected string insuranceObject;
        [JsonProperty]
        protected DateTime startDate;
        [JsonProperty]
        protected DateTime endDate;
        [JsonProperty]
        protected double insuranceAmount;
        [JsonProperty]
        protected double insurancePremium;
        [JsonProperty]
        protected bool status; // true - активний, false - неактивний

        /// <summary>
        /// Конструктор ініціалізує основні поля полісу.
        /// </summary>
        public InsurancePolicy(Client client, string insuranceObject, DateTime endDate, double insurancePremium)
        {
            this.id = nextId++;
            this.client = client;
            this.clientID = client.GetID();
            this.TypeOfInsurance = "Undefined";
            this.insuranceObject = insuranceObject;
            this.startDate = DateTime.Now;
            this.endDate = endDate;
            this.insurancePremium = insurancePremium;
            this.insuranceAmount = FindInsuranceAmount(insurancePremium);
            this.status = true;
        }

        /// <summary>
        /// Обчислення суми компенсації на основі премії (реалізується у нащадках).
        /// </summary>
        protected abstract double FindInsuranceAmount(double insurancePremium);

        public int GetID() => id;
        public int GetClientID() => clientID;
        public double GetInsuranceAmount() => insuranceAmount;
        public bool GetStatus() => status;

        /// <summary>
        /// Повертає інформацію про поліс у вигляді рядка.
        /// </summary>
        public string GetInfo()
        {
            return $"Поліс ID: {id}, Клієнт: {client.GetSurname()} {client.GetName()}\n Тип: {TypeOfInsurance}, Об'єкт: {insuranceObject}\n " +
                   $"Початок: {startDate.ToShortDateString()}\n Кінець: {endDate.ToShortDateString()}\n Премія: {insurancePremium}\nВиплата: {insuranceAmount}\n " +
                   $"Статус: {(status ? "Активний" : "Неактивний")}";
        }

        /// <summary>
        /// Деактивує поліс.
        /// </summary>
        public void DeactivateInsurancePolicy()
        {
            status = false;
        }

        /// <summary>
        /// Змінює дату закінчення дії полісу.
        /// </summary>
        public void ChangeEndDate(DateTime newEndDate)
        {
            endDate = newEndDate;
        }

        /// <summary>
        /// Змінює розмір страхової премії та оновлює суму компенсації.
        /// </summary>
        public void ChangeInsurancePremium(double newPremium)
        {
            insurancePremium = newPremium;
            insuranceAmount = FindInsuranceAmount(newPremium);
        }

        /// <summary>
        /// Метод виплати компенсації (виводить повідомлення в консоль).
        /// </summary>
        public virtual void PayCompensation()
        {
            DeactivateInsurancePolicy();
            Console.WriteLine($"Виплачено компенсацію у розмірі {insuranceAmount} клієнту {client.GetSurname()} {client.GetName()} на рахунок {client.GetBankNumber()}");
        }
    }
}
