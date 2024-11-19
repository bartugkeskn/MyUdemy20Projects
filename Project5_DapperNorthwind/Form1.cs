using Dapper;
using Project5_DapperNorthwind.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5_DapperNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        // Dapper'da da ado.netteki gibi queryler ile veritabanı işlemleri yapılıyor.

        SqlConnection connection = new SqlConnection("Server=DESKTOP-9MRKRIG;initial catalog=Db5Project20;integrated security=true");
        private async void btnCategoryList_Click(object sender, EventArgs e)
        {
            string query = "select * from Categories";
            var values = await connection.QueryAsync<ResultCategoryDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string query = "insert into Categories (CategoryName, Description) values (@p1,@p2)";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtCategoryName.Text);
            parameters.Add("@p2", txtCategoryDescription.Text);
            await connection.ExecuteAsync(query, parameters);
        }

        private async void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from Categories where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId", txtCategoryId.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Silinme işlemi başarılı");
        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            string query = "update Categories set CategoryName=@categoryName, Description=@description where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", txtCategoryName.Text);
            parameters.Add("@description", txtCategoryDescription.Text);
            parameters.Add("@categoryId", txtCategoryId);
            await connection.ExecuteAsync(query, parameters);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Kategori sayısı
            string query1 = "select count(*) from Categories";
            var categoryCount = await connection.ExecuteScalarAsync<int>(query1);
            lblCategoryCount.Text = "Toplam Kategori Sayısı : " + categoryCount;

            // Ürün Sayısı
            string query2 = "select count(*) from Products";
            var productCount = await connection.ExecuteScalarAsync<int>(query2);
            lblProductCount.Text = "Toplam Ürün Sayısı : " + productCount;

            // Ortalama Ürün Stok Sayısı
            string query3 = "select avg(UnitsInStock) from Products";
            var avgProductStock = await connection.ExecuteScalarAsync<int>(query3);
            lblAvgProductStock.Text = "Ortalama Ürün Stok Sayısı : " + avgProductStock;

            // Deniz Ürünleri Toplam Fiyatı
            string query4 = "select Sum(UnitPrice) from Products where CategoryId=(select CategoryId from Categories where CategoryName='SeaFood')";
            var totalSeaFoodPrice = await connection.ExecuteScalarAsync<int>(query4);
            lblSeafoodProductTotalPrice.Text = "Deniz Ürünleri Toplam Fiyatı : " + totalSeaFoodPrice;
        }
    }
}
