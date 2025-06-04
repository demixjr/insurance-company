using System;

namespace BLL
{
    /// <summary>
    /// Страховий поліс для автомобіля.
    /// </summary>
    public class CarInsurancePolicy : InsurancePolicy
    {
        /// <summary>
        /// Ініціалізує поліс із клієнтом, об’єктом, датою і премією.
        /// </summary>
        public CarInsurancePolicy(Client client, string insuranceObject, DateTime endDate, double insurancePremium)
            : base(client, insuranceObject, endDate, insurancePremium)
        {
            TypeOfInsurance = "Автомобіль";
        }

        /// <summary>
        /// Обчислює суму компенсації (премія * 1.5).
        /// </summary>
        protected override double FindInsuranceAmount(double insurancePremium)
        {
            return insurancePremium * 1.5;
        }
    }
}
