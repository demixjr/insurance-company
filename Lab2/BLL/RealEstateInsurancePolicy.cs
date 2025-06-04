using System;

namespace BLL
{
    /// <summary>
    /// Страховий поліс для нерухомості.
    /// </summary>
    public class RealEstateInsurancePolicy : InsurancePolicy
    {
        /// <summary>
        /// Ініціалізує поліс з клієнтом, об'єктом, датою закінчення та премією.
        /// </summary>
        public RealEstateInsurancePolicy(Client client, string insuranceObject, DateTime endDate, double insurancePremium)
            : base(client, insuranceObject, endDate, insurancePremium)
        {
            TypeOfInsurance = "Нерухомість";
        }

        /// <summary>
        /// Обчислює суму компенсації (премія * 1.2).
        /// </summary>
        protected override double FindInsuranceAmount(double insurancePremium)
        {
            return insurancePremium * 1.2;
        }
    }
}
