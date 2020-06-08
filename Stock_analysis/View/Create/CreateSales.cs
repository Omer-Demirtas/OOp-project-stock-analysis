using Stock_analysis.Models;
using Stock_analysis.Repository;
using System;
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
    public partial class CreateSales : Form
    {
        private ISaleRepository saleRepo;
        private ICustomerRepository customerRepo;
        private IProductRepository productRepo;

        private List<Label> labels = new List<Label>();
        private List<TextBox> textBoxes = new List<TextBox>();

        public void Save(object sender, EventArgs e)
        {
            if (textBoxes[0].Text != "" && textBoxes[1].Text != "" &&
            textBoxes[2].Text != "" && textBoxes[3].Text != "")
            {
                DateTime date;
                String productName = textBoxes[1].Text.ToUpper();
                String customerName = textBoxes[0].Text.ToUpper();

                int code = productRepo.GetCodeByName(productName);

                int customerId = customerRepo.GetIdByName(customerName);
                int salesCount = int.Parse(textBoxes[2].Text);
                int salesPrice = int.Parse(textBoxes[3].Text);
                
                try
                {
                    date = DateTime.Parse(textBoxes[4].Text);
                }
                catch
                {
                    date = DateTime.Now;
                }

                //Müşterinin veya ürünün olup olmadığını kontrol etme
                if (code == 0 || customerId == 0)
                {
                    MessageBox.Show("Müşteri adı veya Ürün adı hatalı");
                }
                else
                {
                    Models.Sale sale = new Models.Sale(customerId, code, date, salesCount, (salesPrice * salesCount));

                    saleRepo.Create(sale);

                    //Db'den eski product'ı çekip değerleri güncellenmeli

                    Product productDB = productRepo.GetByName(productName);

                    //Stokları ve ödenen parayı azaltma
                    productDB.purchasePrice -= (salesPrice * salesCount);
                    productDB.purcheseAmount -= salesCount;

                    productRepo.Update(productDB);

                    this.Close();

                }
            }

        }
        public CreateSales(ISaleRepository saleRepo, ICustomerRepository customerRepo
            , IProductRepository productRepo)
        {
            this.saleRepo = saleRepo;
            this.customerRepo = customerRepo;
            this.productRepo = productRepo;

            InitializeComponent();

            CreateUi();
        }

        private void CreateUi()
        {
            String[] SatisOzellikler = { "Müşteri", "ürün", "Adet", "Satış Fiyat" ,"Tarih" };

            int x = 25;
            int y = 25;

            //yükseklik
            int sizeY = 50;
            int sizeX = 100;

            int Winx = 400;
            int Winy = (SatisOzellikler.Length * sizeY * 2) + 100;

            //Save Button
            Button btn = new Button();
            btn.Text = "Kaydet";
            btn.Size = new Size(80, 40);
            btn.Location = new Point((Winx / 2) - 40, Winy - 70);
            btn.Click += Save;

            this.Controls.Add(btn);

            //Windows Settings
            this.BackColor = Settings.color;
            this.Size = new Size(Winx, Winy);





            foreach (String text in SatisOzellikler)
            {
                Label l = new Label();
                TextBox tb = new TextBox();
                labels.Add(l);
                textBoxes.Add(tb);
            }

            for (int i = 0; i < SatisOzellikler.Length; i++)
            {
                labels[i].Location = new Point(x + x, y + i * 50);
                textBoxes[i].Location = new Point(x + x, y + i * 50 + sizeY);

                labels[i].Text = SatisOzellikler[i];
                labels[i].Font = new Font(labels[i].Font.FontFamily, 14);
                labels[i].Size = new Size(sizeX, sizeY);

                textBoxes[i].Size = new Size(sizeX * 3, sizeY - 60);
                if (i == SatisOzellikler.Length - 1)
                {
                    textBoxes[i].Text = "Tarih : GG-AA-YYYY şeklinde olmalı, eğer herhangi bir tarih yazmaz iseniz güncel tarihiniz eklenir";
                }
                this.Controls.Add(labels[i]);
                this.Controls.Add(textBoxes[i]);

                y += sizeY;
            }
        }
        private void CreateSales_Load(object sender, EventArgs e)
        {

        }
    }
}
