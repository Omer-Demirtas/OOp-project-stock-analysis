using Stock_analysis.Models;
using Stock_analysis.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_analysis.View
{
    public partial class Createpurchase : Form
    {
        private IProductRepository productRepo;
        private IPurchaseRepository purchaseRepo;

        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();
        private bool IsSave = false;

        public void Save(object sender, EventArgs e)
        {
            IsSave = true;
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text.Trim() == "")
                {
                    IsSave = false;
                    MessageBox.Show("Boş alan olmamalı");
                }
            }
            if (IsSave)//kaydedilebilir
            {
                DateTime date;
                try
                {
                    date = DateTime.Parse(textBoxes[5].Text);
                }
                catch
                {
                    date = DateTime.Now;
                }

                double purchasePrice = double.Parse(textBoxes[2].Text);
                int purcheseAmount = int.Parse(textBoxes[1].Text);

                //Yeni tür bir ürün eklenyiot demektir
                if (productRepo.GetCodeByName(textBoxes[0].Text.ToUpper()) == 0)
                {
                    int code = productRepo.GetLastCode() + 1;
                    Product product = new Product(code,textBoxes[0].Text.ToUpper(),
                                    purcheseAmount, purchasePrice * purcheseAmount);

                    Purchase purchase = new Purchase(code, purcheseAmount, purchasePrice,DateTime.Now);

                    productRepo.Create(product);

                    //purchase e yeni bir save etmemiz gerek 

                    purchaseRepo.Create(purchase);


                    this.Close();
                }

                /*girilen ürün önceden eklenmiş bir ürün ise  önceki ürünün alış
                 ürünün update işlemini yapıp update işlemi önceki stok bililerinin değiştirilmesi*/
                else
                {
                    int code = productRepo.GetCodeByName(textBoxes[0].Text.ToUpper());

                    //Database'den gelen ürün
                    Product productDB = productRepo.GetByCode(code);

                    Product product = new Product(code, textBoxes[0].Text.ToUpper(),
                        purcheseAmount,purchasePrice);

                    productDB.purcheseAmount += product.purcheseAmount;
                    productDB.purchasePrice += (product.purcheseAmount * product.purchasePrice);

                    productRepo.Update(productDB);

                    //purchase e yeni bir save etmemiz gerek 
                    Purchase purchase = new Purchase(code, purcheseAmount, purchasePrice, DateTime.Now);

                    purchaseRepo.Create(purchase);

                    this.Close();
                }

            }

        }


        public Createpurchase(IProductRepository productRepo, IPurchaseRepository purchaseRepo)
        {
            this.productRepo = productRepo;
            this.purchaseRepo = purchaseRepo;
            InitializeComponent();

            CreateUi();

        }

        public void CreateUi()
        {
            String[] urunOzellikler = {"Ürün adı",
                "Ürün adeti","Ürün Alış(TL)", "Alş Tarih"};

            int x = 25;
            int y = 50;

            //yükseklik
            int sizeY = 50;
            int sizeX = 100;

            int Winx = 400;
            int Winy = (urunOzellikler.Length * sizeY * 2) + 100;

            //Save Button
            Button btn = new Button();
            btn.Text = "Kaydet";
            btn.Size = new Size(60, 30);
            btn.Location = new Point((Winx / 2) - 40, Winy - 70);
            btn.Click += Save;

            this.Controls.Add(btn);

            //Windows Settings
            this.BackColor = Color.White;
            this.Size = new Size(Winx, Winy);
            this.BackColor = Settings.color;
            this.AutoScroll = true;

            foreach (String text in urunOzellikler)
            {
                Label l = new Label();
                TextBox tb = new TextBox();

                labels.Add(l);
                textBoxes.Add(tb);
            }

            for (int i = 0; i < urunOzellikler.Length; i++)
            {
                labels[i].Location = new Point(x + x, y + i * 50);
                textBoxes[i].Location = new Point(x + x, y + i * 50 + sizeY);

                labels[i].Text = urunOzellikler[i];
                labels[i].Font = new Font(labels[i].Font.FontFamily, 12);
                labels[i].Size = new Size(sizeX, sizeY);

                textBoxes[i].Size = new Size(sizeX * 3, sizeY);
                if (i == 3)
                {
                    textBoxes[i].Text = "Tarih : GG-AA-YYYY şeklinde olmalı \n Eğer bu bölüme dokunmaz iseniz tarih otamatik olarak günü ntarihi atılır";
                }
                this.Controls.Add(labels[i]);
                this.Controls.Add(textBoxes[i]);

                y += sizeY;
            }
        }

        private void CreateProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
