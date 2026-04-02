using StroySite.WPF.Models;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StroySite.WPF
{
    public partial class AddEditPage : Page
    {
        private Contractors _currentContractor;
        private bool _isEditMode;

        // Конструктор для добавления
        public AddEditPage()
        {
            InitializeComponent();
            _currentContractor = new Contractors();
            _isEditMode = false;
        }

        // Конструктор для редактирования
        public AddEditPage(Contractors contractor)
        {
            InitializeComponent();
            _currentContractor = contractor;
            _isEditMode = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTypes();

            if (_isEditMode)
                LoadContractorData();
        }

        private void LoadTypes()
        {
            cmbType.ItemsSource = DBContext.GetContext().ContractorsTypes.ToList();
            cmbType.DisplayMemberPath = "TypeName";
            cmbType.SelectedValuePath = "Id";
        }

        private void LoadContractorData()
        {
            txtFullName.Text = _currentContractor.FullName;
            txtPhone.Text = _currentContractor.Phone;
            txtEmail.Text = _currentContractor.Email;
            txtAddress.Text = _currentContractor.Address;
            cmbType.SelectedValue = _currentContractor.TypeId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите наименование", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип контрагента", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Заполняем объект
            _currentContractor.FullName = txtFullName.Text.Trim();
            _currentContractor.Phone = txtPhone.Text.Trim();
            _currentContractor.Email = txtEmail.Text.Trim();
            _currentContractor.Address = txtAddress.Text.Trim();
            _currentContractor.TypeId = (int)cmbType.SelectedValue;

            try
            {
                if (!_isEditMode)
                {
                    // Добавление новой записи
                    DBContext.GetContext().Contractors.Add(_currentContractor);
                }

                DBContext.GetContext().SaveChanges();

                MessageBox.Show("Данные сохранены", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на предыдущую страницу
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}