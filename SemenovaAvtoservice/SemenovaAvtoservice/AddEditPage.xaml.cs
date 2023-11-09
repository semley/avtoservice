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

namespace SemenovaAvtoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServise = new Service();

        public bool a = false;

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if(SelectedService !=null)
            {
                a = true;
                _currentServise = SelectedService;
            }
                
            DataContext = _currentServise;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors=new StringBuilder();

            if(string.IsNullOrWhiteSpace(_currentServise.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            if(_currentServise.Cost==0)
            {
                errors.AppendLine("Укажите стоимость услуги");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentServise.Discount)))
            {
                errors.AppendLine("Укажите скидку");
            }
            if(_currentServise.DurationIn == 0)
            {
                errors.AppendLine("Укажите длительность услуги");
            }

            if (_currentServise.DurationIn > 240)
            {
                errors.AppendLine("Длительность не может быть больше 240 минут");
            }

            if (_currentServise.DurationIn < 0)
            {
                errors.AppendLine("Длительность не может быть меньше 0 минут");
            }

            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var allServices=Семенова_avroservisEntities.GetContext().Service.ToList();
            allServices=allServices.Where(p=>p.Title==_currentServise.Title).ToList();
            if(allServices.Count==0 || a)
            {
                if (_currentServise.ID == 0)
                    Семенова_avroservisEntities.GetContext().Service.Add(_currentServise);
                try
                {
                    Семенова_avroservisEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
            }
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }
            /*
            if (_currentServise.ID == 0 )
                Семенова_avroservisEntities.GetContext().Service.Add(_currentServise);

            try
            {
                Семенова_avroservisEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            */
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
