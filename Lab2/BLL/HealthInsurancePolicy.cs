using System;

namespace BLL
{
    /// <summary>
    /// Страховий поліс для здоров’я.
    /// </summary>
    public class HealthInsurancePolicy : InsurancePolicy
    {
        /// <summary>
        /// Ініціалізує поліс із клієнтом, об’єктом, датою і премією.
        /// </summary>
        public HealthInsurancePolicy(Client client, string insuranceObject, DateTime endDate, double insurancePremium)
            : base(client, insuranceObject, endDate, insurancePremium)
        {
            TypeOfInsurance = "Здоров’я";
        }

        /// <summary>
        /// Обчислює суму компенсації (премія * 1.8).
        /// </summary>
        protected override double FindInsuranceAmount(double insurancePremium)
        {
            return insurancePremium * 1.8;
        }
    }
}
