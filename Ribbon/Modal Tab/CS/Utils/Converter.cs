#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RibbonModelTab
{
    /// <summary>
    /// Represents BooltoSizeformConverter.
    /// </summary>
    public class BooltoSizeformConverter : IValueConverter
    {
        /// <summary>
        /// Method to change the size of an image.
        /// </summary>
        /// <param name="value">Specifies the value.</param>
        /// <param name="targetType">Specifies the target type.</param>
        /// <param name="parameter">Image size to be convert from bool to size.</param>
        /// <param name="culture">Specifies the culture.</param>
        /// <returns>Returns the size.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(true))
            {
                return "Large";
            }
            else
            {
                return "Small";
            }
        }

        /// <summary>
        /// Method to change back the original size.
        /// </summary>
        /// <param name="value">Size</param>
        /// <param name="targetType">Images.</param>
        /// <param name="parameter">Image size to be convert back from bool to size.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Returns the size.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class represents the data template selector.
    /// </summary>
    public class ItemDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Method which is used to select the template.
        /// </summary>
        /// <param name="item">Specifies the template item.</param>
        /// <param name="container">Template container to be select.</param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (element != null && item != null)
            {
                if (item is CustomRibbonItem && (item as CustomRibbonItem).IsSplitButton)
                {
                    return element.FindResource("Splitbutton") as DataTemplate;
                }
                else if (item is CustomRibbonItem && (item as CustomRibbonItem).HasTablePicker)
                {
                    return element.FindResource("DropDownButtonWithTablePickerUI") as DataTemplate;
                }
                else if (item is CustomRibbonItem && (item as CustomRibbonItem).IsDropDownButton)
                {
                    return element.FindResource("DropDownButton") as DataTemplate;
                }
                else if (item is CustomRibbonItem && (item as CustomRibbonItem).IsCheckBox)
                {
                    return element.FindResource("CheckBox") as DataTemplate;
                }
                else if(item is CustomRibbonItem && (item as CustomRibbonItem).IsSeparator)
                {
                    return element.FindResource("RibbonSeparator") as DataTemplate;
                }
                else
                {
                    return element.FindResource("Ribbonbutton") as DataTemplate;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Class represents the ImageConverter
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        /// <summary>
        /// Method which is used to convert the image to string format.
        /// </summary>
        /// <param name="value">Specifies the value.</param>
        /// <param name="targetType">Images which is to be convert to string.</param>
        /// <param name="parameter">Image size to be convert.</param>
        /// <param name="culture">Specifies the culture.</param>
        /// <returns>Returns the size.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string imageName = value.ToString();
                return "Images/" + imageName;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Method which is used to convert back the image to ctring format.
        /// </summary>
        /// <param name="value">Specifies the value.</param>
        /// <param name="targetType">Images which is to be convert back to string.</param>
        /// <param name="parameter">Image size to be convert.</param>
        /// <param name="culture">Specifies the culture.</param>
        /// <returns>Returns the size.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
