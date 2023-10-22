using System;
using System.Collections.Generic;
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

namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
            var area = MainChart.ChartAreas.Add("MainArea");
            var legend = MainChart.Legends.Add("Main legend");
            area.AxisX.Interval = 1;
            area.AxisY.Interval = 1;
        }

        private void DGenerateChart_Click(object sender, RoutedEventArgs e)
        {
            var startDate = DPStartDate.SelectedDate;
            var endDate = DPEndDate.SelectedDate.Value.Date;
            if (!startDate.HasValue)
            {
                MessageBox.Show("Выберите начальную дату.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainChart.Series.Clear();
            foreach(var delivery in App.db.DeliveryType)
            {
                var seria = MainChart.Series.Add($"#{delivery.Name}");
                var chartData = App.db.Order.ToList()
                    .Where(o => o.Date >= startDate.Value.Date && o.Date <=
           endDate)
                    .GroupBy(o => o.DeliveryTypeId)
                    .ToDictionary(key => key.Key, value => value.Count());
                seria.Points.DataBindXY(chartData.Keys, chartData.Values);
                seria.BorderWidth = 5;
                seria.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
           
            

           
           
            
            
        }
    }
}
