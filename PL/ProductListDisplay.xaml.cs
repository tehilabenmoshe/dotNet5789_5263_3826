using BO;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListDisplay.xaml
    /// </summary>
    public partial class ProductListDisplay : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");

        
        public ProductListDisplay()
        {
            InitializeComponent();
            ProductListView.ItemsSource=bl.Product.getProductForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CategorySelector.SelectedItem is BO.Category.all)
                IEnumerableToObservable(bl!.Product.getProductForList());
            else if (CategorySelector.SelectedItem is BO.Category)
                IEnumerableToObservable(bl!.Product.GetPartOfProduct(p => p.Category == (BO.Category)categorySelector.SelectedItem));
            else if (CategorySelector.SelectedItem is "")
                IEnumerableToObservable(bl!.Product.getProductForList());

    
        }

        private void IEnumerableToObservable(IEnumerable<ProductForList> listTOConvert)
        {
            ProductForList.Clear();
            foreach (var station in listTOConvert)
                productList.Add(station);
        }
    }

}
