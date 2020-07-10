#region Copyright Syncfusion Inc. 2001 - 2020
// Copyright Syncfusion Inc. 2001 - 2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Markup;
using Syncfusion.Windows.Tools.Controls;
using Syncfusion.Windows.Tools;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using Syncfusion.Windows.Shared;
using System.Globalization;
using System.Windows.Threading;
using System.Windows.Interop;
using Syncfusion.Pdf.Parsing;
using System.Drawing;
using Syncfusion.Windows.PdfViewer;
namespace RibbonSample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : RibbonWindow
    {
        # region Constructor
        public Window1()
        {
            InitializeComponent();
            ImageSourceConverter converter = new ImageSourceConverter();
            this.Icon = (ImageSource)converter.ConvertFromString(GetFullTemplatePath("pdf viewer.png", true));
            this.ShowHelpButton = false;
           
          
        }

        # endregion

        # region Events


        /// <summary>
        /// Loads PDF on load and initializes controls in custom toolbar.
        /// </summary>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyRibbon.BackStageButton.Visibility = Visibility.Collapsed;
            file_tab.LauncherButton.Visibility = Visibility.Collapsed;
            Navigation.LauncherButton.Visibility = Visibility.Collapsed;
            Zoom.LauncherButton.Visibility = Visibility.Collapsed;
            Size.LauncherButton.Visibility = Visibility.Collapsed;
            find_text.LauncherButton.Visibility = Visibility.Collapsed;
#if NETCORE
			pdfViewerControl1.ReferencePath = @"..\..\..\..\..\..\..\Common\Data\PDF";
#else
			pdfViewerControl1.ReferencePath = @"..\..\..\..\..\..\Common\Data\PDF";
#endif
            pdfViewerControl1.CurrentPageChanged += pdfViewerControl1_CurrentPageChanged;
            lblTotalPageCount.Text = pdfViewerControl1.PageCount.ToString();
            txtCurrentPageIndex.Text = "1";
            if (pdfViewerControl1.LoadedDocument == null)
            {
                FitWidth.IsEnabled = false;
                FitPage.IsEnabled = false;
                Last.IsEnabled = false;
                First.IsEnabled = false;
                Previous.IsEnabled = false;
                Next.IsEnabled = false;
                Save.IsEnabled = false;
                Print1.IsEnabled = false;
                ZoomIn.IsEnabled = false;
                ZoomOut.IsEnabled = false;
                txtCurrentPageIndex.IsEnabled = false;
                ZoomComboBox.IsEnabled = false;
                FindText.IsEnabled = false;
                pageSeparator.IsEnabled = false;
                lblTotalPageCount.IsEnabled = false;
                ZoomPercentage.IsEnabled = false;
            }            
        }

         /// <summary>
        /// Handles file open in custom toolbar.
        /// </summary>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pdf Files (.pdf)|*.pdf";
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                LoadDocument(dialog.FileName);
                txtCurrentPageIndex.Text = "1";
                ribbonwindow.Title = dialog.SafeFileName;
                ZoomComboBox.SelectedIndex = 2;
                FitWidth.IsEnabled = true;
                FitPage.IsEnabled = true;
                Last.IsEnabled = true;
                First.IsEnabled = true;
                Previous.IsEnabled = true;
                Next.IsEnabled = true;
                Save.IsEnabled = true;
                Print1.IsEnabled = true;
                ZoomIn.IsEnabled = true;
                ZoomOut.IsEnabled = true;
                txtCurrentPageIndex.IsEnabled = true;
                ZoomComboBox.IsEnabled = true;
                FindText.IsEnabled = true;
                pageSeparator.IsEnabled = true;
                lblTotalPageCount.IsEnabled = true;
                ZoomPercentage.IsEnabled = true;
            }
           
            pdfViewerControl1.CurrentPageChanged += pdfViewerControl1_CurrentPageChanged;
        }
        void pdfViewerControl1_CurrentPageChanged(object sender, EventArgs args)
        {
            txtCurrentPageIndex.Text = pdfViewerControl1.CurrentPageIndex.ToString();
             if (pdfViewerControl1.CurrentPage == 1)
             {
                 First.IsEnabled = false;
                 Previous.IsEnabled = false;

             }
             else
             {
                 First.IsEnabled = true;
                 Previous.IsEnabled = true;

             }
             if (pdfViewerControl1.CurrentPage == pdfViewerControl1.LoadedDocument.Pages.Count)
             {
                 Last.IsEnabled = false;

                 Next.IsEnabled = false;
             }
             else
             {
                 Last.IsEnabled = true;

                 Next.IsEnabled = true;
             }
        }

        /// <summary>
        /// Handles zoom selection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Zoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ZoomComboBox.Text != "" && ZoomComboBox.Items.Contains(ZoomComboBox.SelectedItem))
            {
                pdfViewerControl1.ZoomTo(int.Parse((ZoomComboBox.SelectedItem as ComboBoxItem).Content.ToString()));
                FitWidth.IsEnabled = true;
                FitPage.IsEnabled = true;
                ZoomOut.IsEnabled = true;
                ZoomIn.IsEnabled = true;
                if (int.Parse((ZoomComboBox.SelectedItem as ComboBoxItem).Content.ToString()) <= 50)
                {
                    ZoomOut.IsEnabled = false;
                    ZoomIn.IsEnabled = true;
                }
                if (int.Parse((ZoomComboBox.SelectedItem as ComboBoxItem).Content.ToString()) >= 400)
                {
                    ZoomOut.IsEnabled = true;
                    ZoomIn.IsEnabled = false;
                }

            }

        }
        /// <summary>
        /// Handles fitpage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FitPage_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewerControl1.LoadedDocument != null)
            {
                pdfViewerControl1.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitPage;
                ZoomComboBox.Text = pdfViewerControl1.ZoomPercentage.ToString();
                ZoomIn.IsEnabled = true;
                ZoomOut.IsEnabled = true;
                FitWidth.IsEnabled = true;
                FitPage.IsEnabled = false;
            }

        }
        /// <summary>
        /// Handles fitwidth.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FitWidth_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewerControl1.LoadedDocument != null)
            {
                pdfViewerControl1.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
                ZoomComboBox.Text = pdfViewerControl1.ZoomPercentage.ToString();
                ZoomIn.IsEnabled = true;
                ZoomOut.IsEnabled = true;
                FitWidth.IsEnabled = false;
                FitPage.IsEnabled = true;
            
            }
        }
        /// <summary>
        /// Handles save operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (pdfViewerControl1.LoadedDocument != null)
            {
                PdfLoadedDocument ldoc = pdfViewerControl1.LoadedDocument;
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Files (*.pdf)|*.pdf";                

                if (save.ShowDialog() == true)
                {
                    FileInfo saveFi = new FileInfo(save.FileName);
                    if (save.FileName != string.Empty)
                    {

                        pdfViewerControl1.LoadedDocument.Save(save.FileName);

                    }
                }
            }
        }
        /// <summary>
        /// Handles text search bar dispaly operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void FindText_Click(object sender, RoutedEventArgs e)
        {
              pdfViewerControl1.ShowTextSearchBar();
        }

     
        # endregion

        #region Helper methods
        private void LoadDocument(string fileName)
        {
            pdfViewerControl1.Load(fileName);
            lblTotalPageCount.Text = pdfViewerControl1.PageCount.ToString();
        }

        /// <summary>
        /// Gets the full path of the PDF template or image.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="image">True if image</param>
        /// <returns>Path of the file</returns>
        private string GetFullTemplatePath(string fileName, bool image)
        {
#if NETCORE
            string fullPath = @"..\..\..\..\..\..\..\Common\";
#else
            string fullPath = @"..\..\..\..\..\..\Common\";
#endif
            string folder = image ? "Images" : "Data";

            return string.Format(@"{0}{1}\PDF\{2}", fullPath, folder, fileName);
        }
        #endregion



    }
}