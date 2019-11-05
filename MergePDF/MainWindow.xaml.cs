using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace MergePDF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Overview overview;
        public MainWindow()
        {
            overview = new Overview();
            InitializeComponent();
        }

        private void dropzone_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            // check if left mousebutton is pressed set dragdrop effects
            if (rec != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(rec, rec.Fill.ToString(), DragDropEffects.Copy);
            }
        }


        private void dropzone_Drop(object sender, DragEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            if (rec != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // save all files pathes to array and after to list include file type check                
                string[] tmpfiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in tmpfiles)
                {
                    if (s.Contains(".pdf"))
                    {
                        overview.filesToMerge.Add(s);
                        overview.listview.Items.Refresh();
                    }
                }
            }
            addOverviewToWindow();
        }


        private void dropzone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TODO:
            // - check if list count is larger than 0 before adding overview

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.RestoreDirectory = true;
            ofd.Title = "MergePDF - Datei öffnen";
            ofd.DefaultExt = "pdf";
            ofd.Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = true;
            //show dialog
            ofd.ShowDialog();

            foreach (string file in ofd.FileNames)
            {
                overview.filesToMerge.Add(file);
                overview.listview.Items.Refresh();
            }

            if (overview.filesToMerge.Count > 0)
            {
                addOverviewToWindow();
            }


            //MessageBox.Show("implement me, i'm mouse down");
        }

        private void addOverviewToWindow()
        {
            //TODO: 
            // - cleanup and outsource it to events and maybe a whole control
            // - textbox for destitnation?
            // - outsource every functionality to Overview

            //edit dropzone property

            if (!maingrid.Children.Contains(overview))
            {
                dropzone.Height = 380;
                maingrid.Children.Add(overview);
                Grid.SetColumn(overview, 1);
                Grid.SetRow(overview, 1);
            }
        }
    }
}
