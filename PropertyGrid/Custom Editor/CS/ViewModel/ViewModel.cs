#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Syncfusion.Windows.PropertyGrid;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;

namespace CustomEditorDemo
{
    public class ViewModel : INotifyPropertyChanged
    {
        PropertyGrid pgrid;
        public ViewModel()
        {
            loadedCommand = new DelegateCommand<object>(WindowLoaded);
        }

        private ICommand loadedCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadedCommand
        {
            get
            {
                return loadedCommand;
            }
        }

        public void WindowLoaded(object param)
        {
            pgrid = param as PropertyGrid;
            HideProperties(typeof(ButtonAdv));
            pgrid.DefaultPropertyPath = "SmallIcon";
            CustomEditor upDownEditor = new CustomEditor() { HasPropertyType = true, PropertyType = typeof(double), Editor = new UpDownEditor() };
            CustomEditor brusheditor = new CustomEditor() { HasPropertyType = true, PropertyType = typeof(Brush), Editor = new BrushEditor() };
            pgrid.CustomEditorCollection.Add(brusheditor);
            CustomEditor imgeditor = new CustomEditor() { HasPropertyType = true, PropertyType = typeof(ImageSource), Editor = new ImageEditor() };
            pgrid.CustomEditorCollection.Add(upDownEditor);
            pgrid.CustomEditorCollection.Add(imgeditor);
            pgrid.RefreshPropertygrid();
        }

        private void HideProperties(Type type)
        {
            PropertyDescriptorCollection p = (PropertyDescriptorCollection)TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor item in p)
            {
                if (item.PropertyType != typeof(Brush) && item.PropertyType != typeof(double))
                {
                    if (item.Name != "SmallIcon")
                    {
                        pgrid.HidePropertiesCollection.Add(item.Name);
                    }
                }
                else if (item.Name == "Foreground")
                {
                    pgrid.HidePropertiesCollection.Add(item.Name);
                }
            }

        }
    }
}