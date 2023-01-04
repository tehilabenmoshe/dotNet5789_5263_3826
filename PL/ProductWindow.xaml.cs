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
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        public ProductWindow()
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            UpdateButton.Visibility = Visibility.Hidden;
        }

        public ProductWindow(int ID) //reciving the id of the product
        {
            
            InitializeComponent();
            BO.Product temp = new BO.Product();
            temp = bl.Product.GetProductbyIdForManager(ID); //temp=product
            
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategoryBox.SelectedItem=temp!.Category; 
            NameBox.Text = temp!.Name.ToString();
            IDBox.Text = temp!.ID.ToString();
            PriceBox.Text = temp!.Price.ToString();
            InStockBox.Text = temp!.InStock.ToString();

            AddBox.Visibility = Visibility.Hidden;

        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            //if (CategoryBox.SelectedItem is "")
            //    CategoryBox.SelectedItem = BO.Category.none;

            //if (CategoryBox.SelectedItem is BO.Category.all)
            //    IEnumerableToPL(bl!.Product.getProductForList());
            //else if (CategoryBox.SelectedItem is BO.Category)
            //    IEnumerableToPL(bl!.Product.FilterProductList(p => p.Category == (BO.Category)CategoryBox.SelectedItem));
            //else if (CategoryBox.SelectedItem is "")
            //    IEnumerableToPL(bl!.Product.getProductForList());

        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }

        private void CategoryBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product temp = new BO.Product();
                //copy the reciving data to the product in bo
                temp.ID = int.Parse(IDBox.Text);
                temp.Name = NameBox.Text;
                temp.Price = double.Parse(PriceBox.Text);
                temp.InStock = int.Parse(InStockBox.Text);
                if ((BO.Category)CategoryBox.SelectedItem == BO.Category.None|| (BO.Category)CategoryBox.SelectedItem == BO.Category.all)
                    throw new Exception("Please Select Category");
                temp.Category = (BO.Category)CategoryBox.SelectedItem;
                //add the product to bo 
                bl!.Product.AddProduct(temp);
                MessageBox.Show("The product successfully added");
                Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                BO.Product temp = new BO.Product();
                //copy the reciving data to the product in bo
                temp.ID = int.Parse(IDBox.Text);
                temp.Name = NameBox.Text;
                temp.Price = double.Parse(PriceBox.Text);
                temp.InStock = int.Parse(InStockBox.Text);
                temp.Category = (BO.Category)CategoryBox.SelectedItem;
                //add the product to bo 
                bl!.Product.UpdateDetailProduct(temp);
                MessageBox.Show("The product successfully update");
                Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
