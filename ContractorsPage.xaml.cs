

// Учебная практика УП.03.01. Ветка dev. Изменения для демонстрации ветвления.

using Microsoft.EntityFrameworkCore;
using StroySite.WPF.Models;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StroySite.WPF
{
    public partial class ContractorsPage : Page
    {
        public ContractorsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgContractors.ItemsSource = DBContext.GetContext().Contractors
                .Include(c => c.ContractorsType)
                .ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
                return;
            }

            var filtered = DBContext.GetContext().Contractors
                .Include(c => c.ContractorsType)
                .Where(c =>
                    c.FullName.ToLower().Contains(searchText) ||
                    c.Phone.ToLower().Contains(searchText) ||
                    c.Email.ToLower().Contains(searchText) ||
                    c.ContractorsType.TypeName.ToLower().Contains(searchText)
                )
                .ToList();

            dgContractors.ItemsSource = filtered;

            if (filtered.Count == 0)
            {
                MessageBox.Show("Ничего не найдено", "Поиск",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgContractors.SelectedItem == null)
            {
                MessageBox.Show("Выберите контрагента для редактирования",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selected = (Contractors)dgContractors.SelectedItem;
            NavigationService.Navigate(new AddEditPage(selected));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgContractors.SelectedItem == null)
            {
                MessageBox.Show("Выберите контрагента для удаления",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selected = (Contractors)dgContractors.SelectedItem;
            var result = MessageBox.Show($"Удалить '{selected.FullName}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DBContext.GetContext().Contractors.Remove(selected);
                    DBContext.GetContext().SaveChanges();
                    LoadData();
                    MessageBox.Show("Запись удалена", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}