﻿using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDO_Training.Converters
{
    internal class ImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? new Bitmap(AssetLoader.Open(new Uri("avares://SEDO_Training/Assets/courses/plug.png"))) : new Bitmap(AssetLoader.Open(new Uri($"avares://SEDO_Training/Assets/courses/{value}")));
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
