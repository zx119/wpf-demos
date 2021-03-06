#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DragandDropDemo
{
    public class Model : NotificationObject
    {
        public Model()
        {
            TreeViewAdv1_Models = new ObservableCollection<Model>();
            TreeViewAdv2_Models = new ObservableCollection<Model>();
        }

        private string header = string.Empty;

        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
                this.RaisePropertyChanged(() => this.Header);
            }
        }

        private BitmapImage imageSource = null;

        public BitmapImage Image
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                this.RaisePropertyChanged(() => this.Image);
            }
        }

        private bool isexpanded = true;

        public bool IsExpanded
        {
            get
            {
                return isexpanded;
            }
            set
            {
                isexpanded = value;
                this.RaisePropertyChanged(() => this.IsExpanded);
            }
        }

        private ObservableCollection<Model> treeViewAdv1_models = null;

        public ObservableCollection<Model> TreeViewAdv1_Models
        {
            get
            {
                return treeViewAdv1_models;
            }
            set
            {
                treeViewAdv1_models = value;
                this.RaisePropertyChanged(() => this.TreeViewAdv1_Models);
            }
        }

        private ObservableCollection<Model> treeViewAdv2_models = null;

        public ObservableCollection<Model> TreeViewAdv2_Models
        {
            get
            {
                return treeViewAdv2_models;
            }
            set
            {
                treeViewAdv2_models = value;
                this.RaisePropertyChanged(() => this.TreeViewAdv2_Models);
            }
        }
    }
}
