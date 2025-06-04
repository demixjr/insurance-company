
using Newtonsoft.Json;

namespace BLL
{
    /// <summary>
    /// Клас Client представляє окремого клієнта з унікальним ідентифікатором, контактною та банківською інформацією.
    /// </summary>
    public class Client
    {
        // Статичне поле, що відповідає за автоматичне присвоєння унікального ID кожному новому клієнту.
        private static int nextId = 1;

        // Унікальний ідентифікатор клієнта.
        [JsonProperty]
        private int id;

        // Прізвище клієнта.
        [JsonProperty]
        private string surname;

        // Ім'я клієнта.
        [JsonProperty]
        private string name;

        // Номер телефону клієнта.
        [JsonProperty]
        private string phone;

        // Банківський рахунок клієнта (очікується 25 цифр).
        [JsonProperty]
        private string bankNumber;

        // Конструктор класу. Присвоює клієнту унікальний ID та зберігає введені дані.
        public Client(string surname, string name, string phone, string bankNumber)
        {
            this.id = nextId++; // Присвоює унікальний ID та збільшує лічильник для наступного клієнта.
            this.surname = surname;
            this.name = name;
            this.phone = phone;
            this.bankNumber = bankNumber;
        }

        // Повертає унікальний ідентифікатор клієнта.
        public int GetID() => id;

        // Повертає прізвище клієнта.
        public string GetSurname() => surname;

        // Повертає ім’я клієнта.
        public string GetName() => name;

        // Повертає номер телефону клієнта.
        public string GetPhone() => phone;

        // Повертає банківський рахунок клієнта.
        public string GetBankNumber() => bankNumber;

        /// <summary>
        /// Повертає повну інформацію про клієнта у форматі одного рядка.
        /// </summary>
        /// <returns>
        /// Рядок з усією інформацією клієнта.
        /// </returns>

        public string GetFullInfo() =>
            $"ID: {id}, {surname} {name}\n Тел: {phone}\n Банк: {bankNumber}";
    }
}


