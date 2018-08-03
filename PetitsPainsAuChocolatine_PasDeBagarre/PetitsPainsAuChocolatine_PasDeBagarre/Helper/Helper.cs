using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PetitsPainsAuChocolatine_PasDeBagarre
{
    public static class Helper
    {
        /// <summary>
        /// Format a string as lowercase, expect the first char as uppercase
        /// </summary>
        /// <param name="input">The string to format</param>
        /// <returns>The formatted string</returns>
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Cannot be null");
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }

        /// <summary>
        /// Parse a stringified date into a DateTime?
        /// </summary>
        /// <param name="dateAsString">The stringified date</param>
        /// <returns>The date as nullable <see cref="DateTime">DateTime</see></returns>
        public static DateTime? ParseDate(this string dateAsString)
        {
            return DateTime.ParseExact(dateAsString, Constants.DATETIME_UI_FORMAT, CultureInfo.CurrentCulture) as DateTime?;
        }

        /// <summary>
        /// Format a DateTime? as following : 01 janvier 1970
        /// </summary>
        /// <param name="date">The date to format</param>
        /// <returns>The formatted date</returns>
        public static DateTime? FormatDate(this DateTime? date)
        {
            return Convert.ToDateTime(date.Value.ToString(Constants.DATETIME_UI_FORMAT)) as DateTime?;
        }

        /// <summary>
        /// Get the next given day from a given start date
        /// </summary>
        /// <param name="start">The start date</param>
        /// <param name="day">The desired next day</param>
        /// <returns></returns>
        public static DateTime? GetNextWeekday(DateTime? start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.Value.DayOfWeek + 7) % 7;
            return start.Value.AddDays(daysToAdd);
        }

        public static void OperationMessage(this TextBox textBox, string message)
        {
            textBox.Text = message;
        }

        public static void SuccessOperationMessage(this TextBox textBox, string message)
        {
            textBox.Foreground = Brushes.ForestGreen;
            textBox.Text = message;
        }

        public static void FailureOperationMessage(this TextBox textBox, string message)
        {
            textBox.Foreground = Brushes.DarkRed;
            textBox.Text = message;
        }
    }
}
