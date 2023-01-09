using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");

        ObservableCollection<ProductForList> productList = new();
        public ProductListPage()
        {
            InitializeComponent();
            IEnumerableToPL(bl.Product.getProductForList());
            ProductListView.DataContext = productList;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CategorySelector.SelectedItem is BO.Category.all)
                IEnumerableToPL(bl!.Product.getProductForList());
            else if (CategorySelector.SelectedItem is BO.Category)
                IEnumerableToPL(bl!.Product.FilterProductList(p => p.Category == (BO.Category)CategorySelector.SelectedItem));
            else if (CategorySelector.SelectedItem is "")
                IEnumerableToPL(bl!.Product.getProductForList());

        }

        private void IEnumerableToPL(IEnumerable<ProductForList> list)
        {
            productList.Clear();
            foreach (var temp in list)
                productList.Add(temp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow ep = new ProductWindow();

            ep.CategoryBox.SelectedItem = BO.Category.None;
            // ep.CategoryBox.SelectedItem.all.Visibilty = false;
            ep.IDBox.IsReadOnly = false;
            ep.Show();
        }


        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow plv = new ProductWindow(((BO.ProductForList)ProductListView.SelectedItem).ID);
            plv.InitializeComponent();
            plv.Show();
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
