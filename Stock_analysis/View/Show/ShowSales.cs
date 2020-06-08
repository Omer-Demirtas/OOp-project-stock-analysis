using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stock_analysis.Models;
using Stock_analysis.Repository;

namespace Stock_analysis.View.Show
{
    public partial class ShowSales : Form
    {
        private List<Sale> sales;
        private List<Label> labels = new List<Label>();
        private List<int> idler = new List<int>();
        private int indis;
        private bool editable = true;
        private List<TextBox> tbs = new List<TextBox>();
        private int imgSize = 18;

        private static Image imgEdit = Image.FromFile(Settings.directory + "icons8-edit-128.png");
        private Bitmap editImg = new Bitmap(imgEdit, new Size(24, 24));

        private static Image imgSave = Image.FromFile(Settings.directory + "icons8-edit-128.png");
        private Bitmap saveImg = new Bitmap(imgSave, new Size(24, 24));

        private ICustomerRepository customerRepo;
        private IProductRepository productRepo;
        private ISaleRepository saleRepo;

        //düzenlemeye yapmayı kontrol 
        //tıklanan satırın sırası
        //Düzenleme yapmak için kullanılacak fonksiyon

        public void CreateUi()
        {
            String[] columns = {"Müşteri", "ürün", "Tarih", "Adet" ,"Kazanç"};

            int btnSizeX = 100;
            int btnSizeY = 50;

            int Winx = (btnSizeX * columns.Length + btnSizeX);
            int Winy = 500;
            int x = 75;
            int y = 50;

            this.AutoScroll = true;
            this.BackColor = Settings.color;
            this.Size = new Size(Winx, Winy);

            //Sütünları oluşturmak
            foreach (String column in columns)
            {
                Label label = new Label();
                label.Text = column;
                labels.Add(label);

                //ekrana yazırma işlemleri 
            }

            foreach (Sale sale in sales)
            {
                // Sütünlar :id ,Musteri_id, urun_id , Satis_tarihi , satis_Adet

                Label customer = new Label();
                Label product = new Label();
                Label date = new Label();
                Label amount = new Label();
                Label price = new Label();

                //isimleri ekleme
                //burada musteriRepository.getNameByID() kullanılmalı
                customer.Text = customerRepo.GetNameById(sale.customerId);

                product.Text = productRepo.GetNameByCode(sale.productCode);
                
                date.Text = sale.saleDate.ToString();
                amount.Text = sale.saleCount.ToString();

                price.Text = sale.salesProfit.ToString();

                labels.Add(customer);
                labels.Add(product);
                labels.Add(date);
                labels.Add(amount);
                labels.Add(price);
            }

            //Heri biri için pozisyon ayarlamaları ve Ekrana ekleme
            foreach (Label label in labels)
            {
                label.Size = new Size(btnSizeX, btnSizeY);
                label.Location = new Point(x, y);
                label.Font = new Font(label.Font.FontFamily, 12);
                x += btnSizeX;

                if (x == Winx - 25)
                {
                    y += btnSizeY;
                    x = 75;
                }
                //labelları ekrana eklme
                this.Controls.Add(label);
            }
        }
        public ShowSales(List<Sale> sales, ICustomerRepository customerRepo, IProductRepository productRepo
        )
        {
            this.productRepo = productRepo;
            this.sales = sales;
            this.customerRepo = customerRepo;

            InitializeComponent();

            CreateUi();
        }

        private void ShowSales_Load(object sender, EventArgs e)
        {

        }
    }
}
