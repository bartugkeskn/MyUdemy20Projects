using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }


        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        Db3Project20Entities db = new Db3Project20Entities();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Toplam Kategori Sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            // Toplam Ürün Sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();

            // Toplam Müşteri Sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();

            // Toplam Sipariş Sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text = orderCount.ToString();

            // Toplam Stok Sayısı
            var totalProductStockCount = db.TblProduct.Sum(x => x.ProductStock); /* Sum metodu, veritabanındaki herhangi bir sayısal sütunun toplamını veya toplamını hesaplamak için kullanılır. */
            lblProductTotalStock.Text = totalProductStockCount.ToString();

            // Ortalama Ürün Fiyatı
            var averageProductPrice = db.TblProduct.Average(x => x.ProductPrice); /* Average metodu, bir sorgudaki sayısal bir sütunun ortalamasını hesaplamak için kullanılır. */
            lblProductAveragePrice.Text = averageProductPrice.ToString() + "TL";

            //Toplam meyve stoku sayısı
            var totalProductCountByCategoryIsFurit = db.TblProduct.Where(x => x.CategoryId == 1).Sum(y => y.ProductStock);
            lblProductCountByCategoryIsFruit.Text = totalProductCountByCategoryIsFurit.ToString();

            //Gazoz isimli ürünün toplam işlem hacmi
            var totalPriceByProductNameIsGazozGetStock = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductStock).FirstOrDefault();
            var totalPriceByProductNameIsGazozGetUnitPrice = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductPrice).FirstOrDefault();
            var totalPriceByProductNameIsGazoz = totalPriceByProductNameIsGazozGetStock * totalPriceByProductNameIsGazozGetUnitPrice;
            lblTotalPriceByProductNameIsGazoz.Text = totalPriceByProductNameIsGazoz.ToString() + "TL";

            // Türkiye'den yapılan siparişler SQL Query
            var orderCountFromTurkiye = db.Database.SqlQuery<int>("Select count(*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where CustomerCountry='Türkiye')").FirstOrDefault();
            lblOrderCountFromTurkiyeBySQL.Text = orderCountFromTurkiye.ToString();

            //Türkiye'den yapılan siparişler EF metodu
            var turkishCustomerIds = db.TblCustomer.Where(x => x.CustomerCountry == "Türkiye")
                                                 .Select(y => y.CustomerId)
                                                 .ToList();
            var orderCountFromTurkiyeWithEf = db.TblOrder.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));

            lblOrderCountFromTurkiyeByEf.Text = orderCountFromTurkiyeWithEf.ToString();

        }

        private void lblProductCount_Click(object sender, EventArgs e)
        {
            
        }

        private void lblCustomerCount_Click(object sender, EventArgs e)
        {
            
        }

        private void lblOrderCount_Click(object sender, EventArgs e)
        {
            
        }
    }
}
