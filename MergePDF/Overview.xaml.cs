using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MergePDF
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : UserControl
    {
        public List<string> filesToMerge { get; set; }
        private Helper helper;

        public Overview()
        {
            InitializeComponent();
            helper = new Helper();
            // disable button while file count is less than two
            btn_merge.IsEnabled = false;
            filesToMerge = new List<string>();
            // set itemsource 
            listview.MouseDoubleClick += Listview_MouseDoubleClick;
            listview.ItemsSource = filesToMerge;
            ((INotifyCollectionChanged)listview.Items).CollectionChanged += Overview_CollectionChanged;
        }

        // remove item on double click 
        private void Listview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (filesToMerge.Contains((string)listview.SelectedItem))
            {
                filesToMerge.Remove((string)listview.SelectedItem);
                listview.Items.Refresh();
            }
        }

        private void Overview_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (listview.Items.Count > 1)
            {
                btn_merge.IsEnabled = true;
            }
            else
            {
                btn_merge.IsEnabled = false;
            }
        }

        private void btn_merge_Click(object sender, RoutedEventArgs e)
        {
            // initalize savedialog
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                RestoreDirectory = true,
                Title = "MergePDF - Datei speichern",
                DefaultExt = "pdf",
                Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*",
                FilterIndex = 1,
                CheckPathExists = true
            };

            // temporary save nullable dialogresult
            Nullable<bool> dialogresult = sfd.ShowDialog();
            if (dialogresult == true)
            {
                helper.savePdf(helper.mergePdf(helper.getPdfDocuments(filesToMerge)), sfd.FileName);
                if (checkbox.IsChecked == true)
                {
                    Process.Start(sfd.FileName);
                }
            }
        }
    }
}
