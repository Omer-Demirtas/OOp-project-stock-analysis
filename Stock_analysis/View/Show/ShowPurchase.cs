using Stock_analysis.Models;
using Stock_analysis.Repository;
using Stock_analysis.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_analysis.View.Show
{
    public partial class ShowPurchase : Form
    {
        List<Label> labels = new List<Label>();
        List<Purchase> purchases;
        IProductRepository productRepo;

        public ShowPurchase(List<Purchase> purchases, IProductRepository productRepo)
        {
            this.productRepo = productRepo;
            this.purchases = purchases;
            InitializeComponent();

            CreateUi();
        }

        private void CreateUi()
        {
            //Sütünları oluşturmak
            String[] columns = {"Ürün adı", "Alış adedi","Alış fiyatı", "Alış Tarihi"};


            //Ekran ile alakalı ayarlamalar 
            int x = 50;
            int y = 25;
            int btnSizeX = 100;
            int btnSizeY = 50;

            this.BackColor = Settings.color;

            int Winx = (btnSizeX * columns.Length + x + 50);
            int Winy = Winx;

            this.AutoScroll = true;

            this.Size = new Size(Winx, Winy);

            foreach (String column in columns)
            {
                Label label = new Label();
                label.Font = new Font(label.Font.FontFamily, 12);
                label.Text = column;
                labels.Add(label);
            }


            foreach (Purchase purchase in purchases)
            {
                Label name = new Label();
                Label amount = new Label();
                Label price = new Label();
                Label date = new Label();

                //isimleri ekleme
                //id.Text = urun.Id.ToString();
                //burada musteriRepository.getNameByID() kullanılmalı
                name.Text = productRepo.GetNameByCode(purchase.productCode);

                amount.Text = purchase.PurchaseAmount.ToString();
                date.Text = purchase.purchaseDate.ToString();
                price.Text = purchase.purchasePrice.ToString();


                //Labelları özelliklerini ayarlamak için bir listeye ekleme
                //labels.Add(id);
                labels.Add(name);
                labels.Add(amount);
                labels.Add(price);
                labels.Add(date);
            }

            //Heri biri için pozisyon ayarlamaları ve Ekrana ekleme
            foreach (Label label in labels)
            {
                label.Size = new Size(btnSizeX, btnSizeY);
                label.Location = new Point(x, y);

                x += btnSizeX;

                if (x == Winx - 50)
                {
                    y += btnSizeY;
                    x = 50;
                }
                //labelları ekrana eklme
                this.Controls.Add(label);
            }
        }

        private void ShowPurchase_Load(object sender, EventArgs e)
        {

        }
    }
}
