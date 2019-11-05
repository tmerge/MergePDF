using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace MergePDF
{
    /// <summary>
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : System.Windows.Controls.UserControl
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
            listview.ItemsSource = filesToMerge;
            ((INotifyCollectionChanged)listview.Items).CollectionChanged += Overview_CollectionChanged;
        }

        private void Overview_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //TODO:
            // - enable button after more than one element is in list   

            btn_merge.IsEnabled = true;
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
            if (sfd.ShowDialog() == DialogResult.OK)
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
