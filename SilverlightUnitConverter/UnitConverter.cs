// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;
using Cureos.Measures;

namespace SilverlightUnitConverter
{
    /// <summary>
    /// Conversion of amount given in one physical unit to another specified physical unit
    /// </summary>
    /// <remarks>
    /// MultiBinding support is not provided out-of-the-box in Silverlight 4 and earlier. This implementation
    /// is using support code developed by Colin Eberhart, and the implementation and usage is displayed in 
    /// detail on the following web page:
    /// http://www.scottlogic.co.uk/blog/colin/2010/08/silverlight-multibinding-updated-adding-support-for-elementname-and-twoway-binding/
    /// </remarks>
    public class UnitConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double fromAmount;
            IUnit fromUnit, toUnit;
            if (values.Length == 3 && Double.TryParse(values[0].ToString(), NumberStyles.Number, culture, out fromAmount) &&
                (fromUnit = values[1] as IUnit) != null && (toUnit = values[2] as IUnit) != null)
            {
                return toUnit.AmountFromStandardUnitConverter(fromUnit.AmountToStandardUnitConverter(fromAmount));
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}