//using Avalonia.Data.Converters;
//using System;
//using System.Globalization;
//using System.Collections.Generic;
//using System.Linq;

//using System.Text;
//using System.Windows;
//using System.Threading.Tasks;

//namespace SEDO_Training.Converters
//{
//    internal class RoleToVisibilityConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            try
//            {
//                if (value == null) return Visibility.Collapsed;

//                int role = System.Convert.ToInt32(value);
//                int targetRole = int.Parse(parameter.ToString());

//                return role == targetRole ? Visibility.Visible : Visibility.Collapsed;
//            }
//            catch (Exception)
//            {
//                return Visibility.Collapsed; // Обработка ошибок преобразования
//            }

//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
