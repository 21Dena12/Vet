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
using Vet.Classes;
using Vet.DBModel;

namespace Vet.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private DBEntities _dbContext;

        public ReportsPage()
        {
            InitializeComponent();
            _dbContext = ConnectionClass.VetClinicContext;
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            ChartArea.Items.Clear();

            var reportData = _dbContext.Appointments
                .GroupBy(a => a.Animals.ClientID)
                .Select(g => new { ClientID = g.Key, Count = g.Count() })
                .ToList();

            double maxValue = reportData.Max(r => r.Count);
            double chartHeight = 300;

            foreach (var data in reportData)
            {
                var client = _dbContext.Clients.FirstOrDefault(c => c.ClientID == data.ClientID);
                if (client != null)
                {
                    double barHeight = (data.Count / maxValue) * chartHeight;

                    var bar = new Rectangle
                    {
                        Width = 50,
                        Height = barHeight,
                        Fill = Brushes.SteelBlue,
                        Margin = new Thickness(10)
                    };

                    ChartArea.Items.Add(new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                        {
                            bar,
                            new TextBlock
                            {
                                Text = $"{client.Users.FullName}: {data.Count}",
                                TextAlignment = TextAlignment.Center
                            }
                        }
                    });
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
