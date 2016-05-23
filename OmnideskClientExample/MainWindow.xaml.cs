using System;
using System.Windows;
using OmnideskRestClient;

namespace TryOmnidesk
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OmnideskClient OmnideskClient = new OmnideskClient("https://2gis.omnidesk.ru", "omniuser@2gis.ru", "a23e0675b2d2c755810922e1b");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var issues = OmnideskClient.GetCases(Int32.Parse(pageTextBox.Text), Int32.Parse(countTextBox.Text));

            int i = 0;
            foreach (var issue in issues)
            {
                textBox.Text += ++i + " " + issue.case_number + " " +issue.case_id 
                    + " " + issue.subject + " " + issue.created_at + " " + issue.updated_at
                    + System.Environment.NewLine;
            }
        }

        private void getIssueButton_Click(object sender, RoutedEventArgs e)
        {
            var issue = OmnideskClient.GetCase(Int32.Parse(idTextBox.Text));
            textBox.Text = issue.case_number + System.Environment.NewLine
                           + issue.subject + System.Environment.NewLine
                           + issue.channel;
        }

        private void getLablesButton_Click(object sender, RoutedEventArgs e)
        {
            var lables = OmnideskClient.GetLables(Int32.Parse(countTextBox.Text), Int32.Parse(pageTextBox.Text));

            int i = 0;
            foreach (var lable in lables)
            {
                textBox.Text += ++i + " " + lable.label_id + " " + lable.label_title + System.Environment.NewLine;
            }
        }
    }
}

