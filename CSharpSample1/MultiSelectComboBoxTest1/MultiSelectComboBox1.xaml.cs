using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MultiSelectComboBoxTest1
{
    /// <summary>
    /// Interaction logic for MultiSelectComboBox.xaml
    /// https://www.codeproject.com/Articles/563862/Multi-Select-ComboBox-in-WPF
    /// </summary>
    public partial class MultiSelectComboBox1 : UserControl
    {
        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IList),
                typeof(MultiSelectComboBox1),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnItemsSourceChanged)
                )
            );
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion

        #region SelectedItems
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems",
                typeof(IList),
                typeof(MultiSelectComboBox1),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnSelectedItemsChanged)
                )
            );
        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
        #endregion

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new UIPropertyMetadata(string.Empty)
            );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region DefaultText
        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register(
                "DefaultText",
                typeof(string),
                typeof(MultiSelectComboBox1),
                new UIPropertyMetadata(string.Empty)
            );
        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        #endregion


        private ObservableCollection<Node> NodeList { get; set; } = new ObservableCollection<Node>();


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiSelectComboBox1()
        {
            InitializeComponent();
        }
        #endregion

        #region イベント
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox1 control = (MultiSelectComboBox1)d;
            control.DisplayInControl();
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox1 control = (MultiSelectComboBox1)d;
            control.SelectNodes();
            control.SetText();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;

            if (clickedBox.Content != null && clickedBox.Content.ToString() == "All")
            {
                if (clickedBox.IsChecked.HasValue && clickedBox.IsChecked.Value)
                {
                    foreach (var node in NodeList)
                    {
                        node.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var node in NodeList)
                    {
                        node.IsSelected = false;
                    }
                }
            }
            else
            {
                var selectedCount = NodeList.Count(s => s.IsSelected && s.Title != "All");
                var node = NodeList.FirstOrDefault(i => i.Title == "All");
                if (node != null)
                {
                    node.IsSelected = selectedCount == NodeList.Count - 1;
                }
            }
            SetSelectedItems();
            SetText();
        }
        #endregion


        #region Methods
        private void SelectNodes()
        {
            if (SelectedItems == null)
            {
                return;
            }

            foreach (var item in SelectedItems)
            {
                var node = NodeList.FirstOrDefault(i => i.Title == item.ToString());
                if (node != null)
                {
                    node.IsSelected = true;
                }
            }
        }

        private void SetSelectedItems()
        {
            SelectedItems.Clear();

            foreach (var node in NodeList)
            {
                if (!node.IsSelected || node.Title == "All")
                {
                    continue;
                }
                if (ItemsSource.Count <= 0)
                {
                    continue;
                }

                var source = ItemsSource.Cast<object>().ToList();
                SelectedItems.Add(source.FirstOrDefault(i => i.ToString() == node.Title));
            }
        }

        private void DisplayInControl()
        {
            NodeList.Clear();

            if (this.ItemsSource.Count > 0)
            {
                NodeList.Add(new Node("All"));
            }

            foreach (var item in this.ItemsSource)
            {
                NodeList.Add(new Node(item.ToString()));
            }

            MultiSelectCombo.ItemsSource = NodeList;
        }

        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in NodeList)
                {
                    if (s.IsSelected == true && s.Title == "All")
                    {
                        displayText = new StringBuilder().Append("All");
                        break;
                    }
                    else if (s.IsSelected == true && s.Title != "All")
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }
                this.Text = displayText.ToString().TrimEnd(new char[] { ',' });
            }
            // set DefaultText if nothing else selected
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.DefaultText;
            }
        }
        #endregion


        /// <summary>
        /// ノードクラス
        /// </summary>
        private class Node : INotifyPropertyChanged
        {
            #region Properties
            public string Title
            {
                get
                {
                    return _title;
                }
                set
                {
                    _title = value;
                    NotifyPropertyChanged(nameof(Title));
                }
            }
            private string _title;

            public bool IsSelected
            {
                get
                {
                    return _isSelected;
                }
                set
                {
                    _isSelected = value;
                    NotifyPropertyChanged(nameof(IsSelected));
                }
            }
            private bool _isSelected;
            #endregion

            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="title"></param>
            public Node(string title) => Title = title;
        }
    }
}
