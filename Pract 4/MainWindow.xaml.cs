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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Practos4
{
    public class Record
    {
        public string name {get; set;}
        public string recordtype  { get; set; }
        public double amountofmoney { get; set; }
        public bool deduction { get; set; }
        public DateTime date { get; set; }

        public Record() { }
        public Record(string name, string recordtype, double amountofmoney, bool deduction, DateTime date) {
            this.name = name;
            this.recordtype = recordtype;
            this.amountofmoney = amountofmoney;
            this.deduction = deduction;
            this.date = date;
        }
    }

    public class AllData
    {
        public List<Record> records;
        public List<string> recordTypes;
        public List<DateTime> dates;
        
        public AllData()
        {
            records = new List<Record>();
            dates = new List<DateTime>();
            recordTypes = new List<string>();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public AllData appData;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            appData = new AllData();

            if (File.Exists("alldata.xml"))
            {
                TextReader reader = null;
                var readSerializer = new XmlSerializer(typeof(AllData));
                reader = new StreamReader("alldata.xml");
                appData = (AllData)readSerializer.Deserialize(reader);
            }
            else
            {
                appData.records.Add(new Record("Семчишин Илья Витальевич", "Доход от инвестиций", 16000, false, new DateTime(2022, 12, 30)));
                appData.records.Add(new Record("Узунов Степан Федорович", "Доход от продажи криптовалюты", 40000, false, new DateTime(2023, 04, 01)));
                appData.records.Add(new Record("Корсунов Роман Владимирович", "Доход от продажи видеокарт " 50000, false, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Перевезенцев Матвей Валерьевич", "Налоговый вычет", 17800, true, new DateTime(2023, 01, 01)));
                appData.records.Add(new Record("Семчишин Степан Витальевич", "Налоговый вычет", 15900, true, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Мелехов Антон Вадимович", "Доход от работодателя", 19000, false, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Семчишин Виталий Степанович", "Доход от крупных инвестиций ", 36000, false, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Иванов Алексей Антонович", "Налоговый вычет", 130000, true, new DateTime(2023, 04, 01)));
                appData.records.Add(new Record("Ваганова Наталья Сергеевна", "Вычет из Гос.Преприятия", 40500, true, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Кирсанов Егор Владимирович", "Доход от вклада в банк ", 38000, false, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Солдатов Вячеслав Павлович", "Налоговый вычет из-за неуплаты долгов", 43900, true, new DateTime(2022, 12, 05)));
                appData.records.Add(new Record("Сайдашев Тимофей Вадимович", "Вычет из-за неуплаты кредита", 22000, true, new DateTime(2023, 01, 01)));
                appData.records.Add(new Record("Борисова Елена Владимировна", "Доход от пенсии ", 18000, false, new DateTime(2022, 12, 05)));


                //---------------------------------      

                for (int i = 0; i < appData.records.Count; i++)
                {
                    if (!appData.dates.Contains(appData.records[i].date))
                        appData.dates.Add(appData.records[i].date);
                }

                //---------------------------------


                appData.recordTypes.Add("Налоговый вычет");
                appData.recordTypes.Add("Доход от инвестиций");
                appData.recordTypes.Add("Доход от продажи криптовалюты");
                appData.recordTypes.Add("Доход от продажи видеокарт");
                appData.recordTypes.Add("Доход от работодателя ");
                appData.recordTypes.Add("Доход от крупных инвестиций");
                appData.recordTypes.Add("Налоговый Вычет");
                appData.recordTypes.Add("Вычет из Гос.Предприятия");
                appData.recordTypes.Add("Доход от вклада в банк");
                appData.recordTypes.Add("Налоговый вычет из-за неуплаты долгов");
                appData.recordTypes.Add("Доход от пенсии");
                appData.recordTypes.Add("Вычет из-за неуплаты кредита");



                TextWriter writer = null;
                var serializer = new XmlSerializer(typeof(AllData));
                writer = new StreamWriter("alldata.xml", false);
                serializer.Serialize(writer, appData);
            }


            Razer.Items.Clear();
            Razer.ItemsSource = appData.records;
            RecordDatePicker.ItemsSource = appData.dates;
            DGCbColumn.ItemsSource = appData.recordTypes;

        }

        private void Razer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RecordDatePicker.SelectedIndex = -1;
            Razer.ItemsSource = appData.records;
        }

        private void RecordDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecordDatePicker.SelectedIndex != -1)
                Razer.ItemsSource = appData.records.Where(a => a.date == (DateTime)RecordDatePicker.SelectedValue).ToList();
        }
    }
}
