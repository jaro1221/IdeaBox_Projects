using System;
using System.Collections.Generic;
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

namespace IB_TaxCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        struct TaxRecord
        {
            public string Country { get; set; }
            public double Rate { get; set; }
            public string Currency { get; set; }
        }

        List<TaxRecord> TaxList = new List<TaxRecord>();

        public MainWindow()
        {
            InitializeComponent();
            string path = Directory.GetCurrentDirectory();
            path = path.Remove(path.IndexOf("\\bin\\Debug")) + "\\taxes.txt";
            LoadTaxes(path);
            
        }

        private void LoadTaxes(string path)
        {
            
            StreamReader file = new StreamReader(path);
            while(!file.EndOfStream)
            {
                string record = file.ReadLine();
                string[] data;
                data = record.Split(';');

                TaxRecord newRecord = new TaxRecord();
                newRecord.Country = data[0];
                newRecord.Rate = double.Parse(data[1]);
                newRecord.Currency = data[2];
                TaxList.Add(newRecord);

            }
            file.Close();

            foreach(TaxRecord item in TaxList)
            {
                comboBox.Items.Add(item.Country);
            }

            
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double taxValue = Math.Round(double.Parse(costTextBox.Text) * GetTaxRate(comboBox.Text), 2);
            double result = Math.Round(double.Parse(costTextBox.Text) * (1 + GetTaxRate(comboBox.Text)), 2);
            taxValueLabel.Content = taxValue + " " + GetCurrency(comboBox.Text);
            resultLabel.Content = result + " " + GetCurrency(comboBox.Text);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            taxLabel.Content = GetTaxRate(comboBox.Text)*100 + "%";
        }

        private double GetTaxRate(string text)
        {
            TaxRecord item = new TaxRecord();
            int id = 0;
            foreach(TaxRecord i in TaxList)
            {
                if (i.Country == text)
                    id = TaxList.IndexOf(i);
            }
            item = TaxList[id];
            return item.Rate;

        }
        private string GetCurrency(string text)
        {
            TaxRecord item = new TaxRecord();
            int id = 0;
            foreach (TaxRecord i in TaxList)
            {
                if (i.Country == text)
                    id = TaxList.IndexOf(i);
            }
            item = TaxList[id];
            return item.Currency;

        }
    }
}
