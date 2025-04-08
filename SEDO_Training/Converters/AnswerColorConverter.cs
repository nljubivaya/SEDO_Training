using Avalonia.Data.Converters;
using Avalonia.Media;
using SEDO_Training.Models;
using SEDO_Training.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDO_Training.Converters
{
    internal class AnswerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedAnswerIndex && parameter is int correctAnswerIndex)
            {
                if (selectedAnswerIndex == correctAnswerIndex)
                {
                    return Brushes.Green; // Correct answer
                }
                else
                {
                    return Brushes.Red; // Incorrect answer
                }
            }
            return Brushes.Black; // No answer selected
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
