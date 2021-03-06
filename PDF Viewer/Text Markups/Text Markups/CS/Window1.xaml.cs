#region Copyright Syncfusion Inc. 2001-2016.
// Copyright Syncfusion Inc. 2001-2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Syncfusion.Windows.Shared;

namespace TextMarkups
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : ChromelessWindow
    {
        # region Constructor
        public Window1()
        {
            InitializeComponent();
        }
        # endregion

        # region Events
        /// <summary>
        /// Occurs when the window is loaded
        /// </summary>
        private void Pdfviewer1_DocumentLoaded(object sender, EventArgs args)
        {
            pdfviewer1.GotoPage(3);
        }
        #endregion
    }
}
