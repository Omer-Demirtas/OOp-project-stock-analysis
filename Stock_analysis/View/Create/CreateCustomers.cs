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
    public partial class CreateCustomers :  Form
    {
        private static List<Label> labels = new List<Label>();
        private static List<TextBox> textBoxes = new List<TextBox>();
        private ICustomerRepository customerRepo;

        public void Save(object sender, EventArgs e)
        {
            if (textBoxes[0].Text.Trim() == "" || textBoxes[1].Text.Trim() == "" ||
                textBoxes[2].Text.Trim() == "")
            {
                MessageBox.Show("Herhangi bir yer boş olmamalı");
            }
            else if (textBoxes[2].Text.Length > 11)
            {
                MessageBox.Show("Telefon 11 Haneli olmalı");
            }
            else
            {
                String name = textBoxes[0].Text.Trim().ToUpper();
                String surname = textBoxes[1].Text.Trim().ToUpper();
                String telNo = textBoxes[2].Text.Trim();

                Customer musteri = new Customer(name, surname, telNo);

                customerRepo.Create(musteri);
                
                this.Close();
            }

        }

        public CreateCustomers(ICustomerRepository customerRepo)
        {
            this.customerRepo = customerRepo;
            InitializeComponent();

            String[] MusteriOzellikler = { "ad", "soyad", "Telefon numarası" };

            int x = 25;
            int y = 50;

            //yükseklik
            int sizeY = 50;
            int sizeX = 150;

            //Özellik sayısına göre yüksekliği belirliyoruz
            int Winx = 450;
            int Winy = (MusteriOzellikler.Length * sizeY * 3) + 100;

            //Save Button
            Button btn = new Button();
            btn.Text = "Kaydet";
            btn.Size = new Size(60, 30);
            btn.Location = new Point((Winx / 2) - 40, Winy - 80);
            btn.Click += Save;

            this.Controls.Add(btn);

            //Windows Settings
            this.BackColor = Settings.color;
            this.Size = new Size(Winx, Winy);


            foreach (String text in MusteriOzellikler)
            {
                Label l = new Label();
                TextBox tb = new TextBox();
                labels.Add(l);
                textBoxes.Add(tb);
            }

            for (int i = 0; i < 3; i++)
            {
                labels[i].Location = new Point(x, y + i * 100);
                textBoxes[i].Location = new Point(x, y + i * 100 + sizeY);

                labels[i].Text = MusteriOzellikler[i];
                labels[i].Font = new Font(labels[i].Font.FontFamily, 12);
                labels[i].Size = new Size(sizeX, sizeY);

                textBoxes[i].Size = new Size(sizeX * 2, sizeY - 40);


                this.Controls.Add(labels[i]);
                this.Controls.Add(textBoxes[i]);

                y += sizeY;
            }
        }
        private void CreateCustomers_Load(object sender, EventArgs e)
        {

        }
    }
}
