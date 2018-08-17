using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDFAppDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fileDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathTextBox.Text = fileDialog.SelectedPath;
            }
        }

        private void SaveButon_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == true)
            {
                GeneratePDF(PathTextBox.Text, dialog.FileName);
            }
        }

        private void GeneratePDF(string path, string fileName)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles();

            var document = new PdfDocument();
            document.Info.Title = "List of files";

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Corbal", 10);

            int x = 30;
            int y = 20;
            int i = 1;

            foreach (FileInfo file in Files)
            {
                gfx.DrawString(
                    i + ". " + file.Name,
                    font,
                    XBrushes.Black,
                    x, i * 15 + y
                    );
                i++;
            }

            int maxY = i * 15 + y + 10;

            gfx.DrawLine(XPens.Gray, 300, 0, 300, maxY);
            gfx.DrawLine(XPens.Gray, 0, maxY, 300, maxY);

            document.Save(fileName);
            Process.Start(fileName);
        }
    }
}
