using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JsonViewerControl
{
    /// <summary>
    /// Interaction logic for JsonViewer.xaml
    /// </summary>
    public partial class JsonViewer : UserControl
    {
        private const GeneratorStatus Generated = GeneratorStatus.ContainersGenerated;
        private DispatcherTimer timer;

        public JsonViewer()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string JsonViewerTitle { get; set; } = "Json Viewer";

        public JToken SelectedItem => this.JsonTreeView.SelectedItem as JToken;

        public void ExpandAll() => this.ToggleItems(true);

        public void CollapseAll() => this.ToggleItems(false);

        public void Load(string json)
        {
            this.JsonTreeView.ItemsSource = null;
            this.JsonTreeView.Items.Clear();

            var children = new List<JToken>();

            try
            {
                var token = JToken.Parse(json);

                if (token != null)
                {
                    children.Add(token);
                }

                this.JsonTreeView.ItemsSource = children;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the JSON string:\r\n" + ex.Message);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        private void JValue_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            var tb = sender as TextBlock;
            if (tb != null)
            {
                Clipboard.SetText(tb.Text);
            }
        }

        private void ExpandAll(object sender, RoutedEventArgs e)
        {
            this.ToggleItems(true);
        }

        private void CollapseAll(object sender, RoutedEventArgs e)
        {
            this.ToggleItems(false);
        }

        private void ToggleItems(bool isExpanded)
        {
            if (this.JsonTreeView.Items.IsEmpty)
            {
                return;
            }

            var prevCursor = this.Cursor;

            // System.Windows.Controls.DockPanel.Opacity = 0.2;
            // System.Windows.Controls.DockPanel.IsEnabled = false;
            this.Cursor = Cursors.Wait;
            this.timer = new DispatcherTimer(
                TimeSpan.FromMilliseconds(50),
                DispatcherPriority.Normal,
                (sender, e) =>
                {
                    this.ToggleItems(this.JsonTreeView, this.JsonTreeView.Items, isExpanded);

                    // System.Windows.Controls.DockPanel.Opacity = 1.0;
                    // System.Windows.Controls.DockPanel.IsEnabled = true;
                    this.timer.Stop();
                    this.Cursor = prevCursor;
                },
                Application.Current.Dispatcher);

            this.timer.Start();
        }

        private void ToggleItems(ItemsControl parentContainer, ItemCollection items, bool isExpanded)
        {
            var itemGen = parentContainer.ItemContainerGenerator;
            if (itemGen.Status == Generated)
            {
                this.Recurse(items, isExpanded, itemGen);
            }
            else
            {
                itemGen.StatusChanged += (sender, e) =>
                {
                    this.Recurse(items, isExpanded, itemGen);
                };
            }
        }

        private void Recurse(ItemCollection items, bool isExpanded, ItemContainerGenerator itemGen)
        {
            if (itemGen.Status != Generated)
            {
                return;
            }

            foreach (var item in items)
            {
                var tvi = itemGen.ContainerFromItem(item) as TreeViewItem;
                tvi.IsExpanded = isExpanded;
                this.ToggleItems(tvi, tvi.Items, isExpanded);
            }
        }
    }
}
