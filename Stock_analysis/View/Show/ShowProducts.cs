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

namespace Stock_analysis.View.Show
{
    public partial class ShowProducts : Form
    {
        List<Label> labels = new List<Label>();
        List<Product> products;

        public ShowProducts(List<Product> products)
        {
            this.products = products;
            InitializeComponent();

            CreateUi();
        }

        private void CreateUi()
        {
            //Sütünları oluşturmak
            String[] sutunlar = {"Ürün adı", "Ürün kodu",
                "Stok adeti", "Alış Fiyatı"};


            //Ekran ile alakalı ayarlamalar 
            int x = 50;
            int y = 25;
            int btnSizeX = 100;
            int btnSizeY = 50;

            this.BackColor = Settings.color;

            int Winx = (btnSizeX * sutunlar.Length + x);
            int Winy = Winx;

            this.AutoScroll = true;

            this.Size = new Size(Winx, Winy);

            foreach (String sutun in sutunlar)
            {
                Label label = new Label();
                label.Font = new Font(label.Font.FontFamily, 12);
                label.Text = sutun;
                labels.Add(label);
            }


            foreach (Product product in products)
            {
                // Sütünlar :id ,Musteri_id, urun_id , Satis_tarihi , satis_Adet

                //Label id = new Label();
                Label name = new Label();
                Label code = new Label();
                Label amount = new Label();
                Label price = new Label();

                //isimleri ekleme
                //id.Text = urun.Id.ToString();
                //burada musteriRepository.getNameByID() kullanılmalı
                name.Text = product.name;
                code.Text = product.code.ToString();
                amount.Text = product.purcheseAmount.ToString();
                price.Text = product.purchasePrice.ToString();


                //Labelları özelliklerini ayarlamak için bir listeye ekleme
                //labels.Add(id);
                labels.Add(name);
                labels.Add(code);
                labels.Add(amount);
                labels.Add(price);
            }

            //Heri biri için pozisyon ayarlamaları ve Ekrana ekleme
            foreach (Label label in labels)
            {
                label.Size = new Size(btnSizeX, btnSizeY);
                label.Location = new Point(x, y);

                x += btnSizeX;

                if (x == Winx)
                {
                    y += btnSizeY;
                    x = 50;
                }
                //labelları ekrana eklme
                this.Controls.Add(label);
            }
        }

        private void ShowProducts_Load(object sender, EventArgs e)
        {

        }
    }
}
