#region Copyright Syncfusion Inc. 2001 - 2019
//
//  Copyright Syncfusion Inc. 2001 - 2019. All rights reserved.
//
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Any infringement will be prosecuted under
//  applicable laws. 
//
#endregion
using System;
using System.Windows;
using System.Windows.Media;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System.ComponentModel;
using Syncfusion.Windows.Shared;
using System.IO;
using Syncfusion.Office;

namespace EditEquation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : ChromelessWindow
    {
        # region Constructor
        /// <summary>
        /// Window constructor
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            ImageSourceConverter img = new ImageSourceConverter();
#if NETCORE
            image1.Source = (ImageSource)img.ConvertFromString(@"..\..\..\..\..\..\..\Common\Images\DocIO\docio_header.png");
            this.Icon =(ImageSource)img.ConvertFromString(@"..\..\..\..\..\..\..\Common\Images\DocIO\sfLogo.ico");
#else
            image1.Source = (ImageSource)img.ConvertFromString(@"..\..\..\..\..\..\Common\Images\DocIO\docio_header.png");
            this.Icon = (ImageSource)img.ConvertFromString(@"..\..\..\..\..\..\Common\Images\DocIO\sfLogo.ico");
#endif 
        }
        # endregion

        # region Events
        /// <summary>
        /// Creates word document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                // Get XML file path.
                string dataPath = string.Empty;
#if NETCORE
                dataPath = @"..\..\..\..\..\..\..\Common\Data\DocIO\";
#else
                dataPath = @"..\..\..\..\..\..\Common\Data\DocIO\";
#endif
                //Opens an existing Word document
                WordDocument document = new WordDocument(dataPath+ "Mathematical Equation.docx");

                //Gets the last section in the document
                WSection section = document.LastSection;
                //Gets paragraph from Word document
                WParagraph paragraph = document.LastSection.Body.ChildEntities[3] as WParagraph;

                //Gets MathML from the paragraph
                WMath math = paragraph.ChildEntities[0] as WMath;
                //Gets the radical equation
                IOfficeMathRadical mathRadical = math.MathParagraph.Maths[0].Functions[1] as IOfficeMathRadical;
                //Gets the fraction equation in radical
                IOfficeMathFraction mathFraction = mathRadical.Equation.Functions[0] as IOfficeMathFraction;
                //Gets the n-array in fraction
                IOfficeMathNArray mathNAry = mathFraction.Numerator.Functions[0] as IOfficeMathNArray;
                //Gets the math script in n-array
                IOfficeMathScript mathScript = mathNAry.Equation.Functions[0] as IOfficeMathScript;
                //Gets the delimiter in math script
                IOfficeMathDelimiter mathDelimiter = mathScript.Equation.Functions[0] as IOfficeMathDelimiter;
                //Gets the math script in delimiter
                mathScript = mathDelimiter.Equation[0].Functions[0] as IOfficeMathScript;
                //Gets the math run element in math script
                IOfficeMathRunElement mathParaItem = mathScript.Equation.Functions[0] as IOfficeMathRunElement;
                //Modifies the math text value
                (mathParaItem.Item as WTextRange).Text = "x";

                //Gets the math bar in delimiter
                IOfficeMathBar mathBar = mathDelimiter.Equation[0].Functions[2] as IOfficeMathBar;
                //Gets the math run element in bar
                mathParaItem = mathBar.Equation.Functions[0] as IOfficeMathRunElement;
                //Modifies the math text value
                (mathParaItem.Item as WTextRange).Text = "x";

                //Gets the paragraph from Word document
                paragraph = document.LastSection.Body.ChildEntities[6] as WParagraph;
                //Gets MathML from the paragraph
                math = paragraph.ChildEntities[0] as WMath;
                //Gets the math script equation
                mathScript = math.MathParagraph.Maths[0].Functions[0] as IOfficeMathScript;
                //Gets the math run element in math script
                mathParaItem = mathScript.Equation.Functions[0] as IOfficeMathRunElement;
                //Modifies the math text value
                (mathParaItem.Item as WTextRange).Text = "x";

                //Gets the paragraph from Word document
                paragraph = document.LastSection.Body.ChildEntities[7] as WParagraph;
                //Gets MathML from the paragraph
                WMath math2 = paragraph.ChildEntities[0] as WMath;
                //Gets bar equation
                mathBar = math2.MathParagraph.Maths[0].Functions[0] as IOfficeMathBar;
                //Gets the math run element in bar
                mathParaItem = mathBar.Equation.Functions[0] as IOfficeMathRunElement;
                //Gets the math text
                (mathParaItem.Item as WTextRange).Text = "x";

                //Saving the document as .docx
                document.Save("Sample.docx", FormatType.Docx);
                document.Close();
                //Message box confirmation to view the created document.
                if (MessageBox.Show("Do you want to view the generated Word document?", "Document has been created", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    try
                    {
                        //Launching the MS Word file using the default Application.[MS Word Or Free WordViewer]
#if NETCORE
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo = new System.Diagnostics.ProcessStartInfo("Sample.docx") { UseShellExecute = true };
                        process.Start();
#else
                        System.Diagnostics.Process.Start("Sample.docx");
#endif
                        //Exit
                        this.Close();
                    }
                    catch (System.ComponentModel.Win32Exception ex)
                    {
                        MessageBox.Show("Microsoft Word Viewer or Microsoft Word is not installed in this system");
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        # endregion     
    }
}