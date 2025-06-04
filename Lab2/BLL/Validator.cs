using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Клас відповідає за перевірку даних при вводі інформації
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Перевіряє валідність телефонного номера (10–13 цифр, можливо з + на початку).
        /// </summary>
        /// <returns>
        /// True - якщо вірно введені дані, False - якщо невірно
        /// </returns>
        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^\+?\d{10,13}$");
        }

        /// <summary>
        /// Перевіряє, що банківський номер складається з 25 цифр.
        /// </summary>
        /// <returns>
        /// True - якщо вірно введені дані, False - якщо невірно
        /// </returns>
        public static bool ValidateBankNumber(string bankNumber)
        {
            if (string.IsNullOrWhiteSpace(bankNumber)) return false;
            return Regex.IsMatch(bankNumber, @"^\d{25}$");
        }
    }
}

