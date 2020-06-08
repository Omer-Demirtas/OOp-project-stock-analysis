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

namespace Stock_analysis.View.Show
{
    public partial class ShowCustomers : Form
    {
        private List<Customer> customers;
        private List<Label> labels = new List<Label>();
        private List<int> idler = new List<int>();
        private int indis;
        private bool editable = true;
        private List<TextBox> tbs = new List<TextBox>();
        private static int imgSize = 18;
        
        private ICustomerRepository customerRepo;
        private ISaleRepository saleRepo;

        private static Image imgSave = Image.FromFile(Settings.directory + "save.png");
        private Bitmap saveImg = new Bitmap(imgSave, new Size(imgSize, imgSize));

        private static Image imgEdit = Image.FromFile(Settings.directory + "icons8-edit-128.png");
        private Bitmap editImg = new Bitmap(imgEdit , new Size(imgSize, imgSize));

        public void Save(object sender, EventArgs e)
        {
            bool delete = true;
            //indis -1 ilk surun
            foreach (TextBox tb in tbs)
            {
                if (tb.Text != "")
                {
                    delete = false;
                }
            }
            if (delete)
            {

                customerRepo.Delete(idler[(indis - 5) / 4]);
                saleRepo.DeleteByCustomerId(idler[(indis - 5) / 4]);
 
                editable = true;


                this.Controls.Remove(labels[indis - 1]);
                for (int i = 0; i < 3; i++)
                {
                    this.Controls.Remove(labels[indis + i]);
                    this.Controls.Remove(tbs[i]);
                }
            }
            else
            {
                String name  = tbs[0].Text.ToUpper();
                String surname = tbs[1].Text.ToUpper();
                String telNo = tbs[2].Text;
                int id = idler[(indis - 5) / 4];

                String[] ozellikler = { name, surname, telNo };

                Customer customer = new Customer(id, name, surname, telNo);


                customerRepo.Update(customer);


                editable = true;

                //Çöpleri temizleme
                for (int i = 0; i < 3; i++)
                {
                    labels[indis + i].Visible = true;
                    this.Controls.Remove(tbs[i]);
                    labels[indis + i].Text = ozellikler[i];
                }
                
                //kullanıcıların bilgilerini değiştirme
                tbs.RemoveRange(0, tbs.Count);


                labels[indis - 1].Click -= Save;
                labels[indis - 1].Click += Edit;

                //resmi eski haline gelitrme

                labels[indis - 1].Image = editImg;
                labels[indis - 1].ImageAlign = ContentAlignment.TopLeft;
            }
        }

        public ShowCustomers(List<Customer> customers, ICustomerRepository customerRepo, ISaleRepository saleRepo)
        {
            this.saleRepo = saleRepo;
            this.customers = customers;
            this.customerRepo = customerRepo;

            InitializeComponent();

            CreateUi();
        }
        private void Edit(object sender, EventArgs e)
        {
            if (editable)
            {
                editable = false;

                Label label = (sender as Label);
                indis = 0;

                foreach (Label lb in labels)
                {
                    indis++;
                    if (label.Equals(lb))
                    {
                        //eşitlik durunda kendin sonraki butonları textbox yapacak
                        for (int j = 0; j < 3; j++)
                        {
                            int i = indis + j;

                            Label l = labels[i];
                            labels[i].Visible = false;

                            TextBox tb = new TextBox();

                            tb.Size = l.Size;
                            tb.Text = l.Text;
                            tb.Location = l.Location;

                            tbs.Add(tb);
                            this.Controls.Add(tb);
                        }

                        labels[indis - 1].Click -= Edit;
                        labels[indis - 1].Click += Save;

                        labels[indis - 1].Image = saveImg;
                        labels[indis - 1].ImageAlign = ContentAlignment.TopLeft;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("aynı anda tek üye düzenliyebilirsiniz");
            }
        }

        private void CreateUi()
        {
            //Sütünları oluşturmak
            String[] coulmns = { "#", "ad", "soyad", "tel" };

            int x = 50;
            int y = 0;
            int btnSizeX = 100;
            int btnSizeY = 50;

            int Winx = (btnSizeX * (coulmns.Length)) + x + 50;
            int Winy = Winx;

            this.AutoScroll = true;
            this.BackColor = Color.White;
            this.Size = new Size(Winx, Winy);


            foreach (String coulmn in coulmns)
            {
                Label label = new Label();
                label.Text = coulmn;
                labels.Add(label);
            }


            foreach (Customer customer in customers)
            {
                // Sütünlar :id ,Musteri_id, urun_id , Satis_tarihi , satis_Adet

                Label id = new Label();
                Label name = new Label();
                Label surname = new Label();
                Label telNo = new Label();


                //isimleri ekleme
                //id.Text = musteri.Id.ToString();
                //burada musteriRepository.getNameByID() kullanılmalı
                name.Text = customer.name;
                surname.Text = customer.surName;
                telNo.Text = customer.telNo;

                id.Click += Edit;
                
                id.Image = editImg;
                id.ImageAlign = ContentAlignment.TopLeft;
                //basılan tuşun id sini bulam için idler diye bit liste oluştrdum

                idler.Add(customer.id);


                //Labelları özelliklerini ayarlamak için bir listeye ekleme
                labels.Add(id);
                labels.Add(name);
                labels.Add(surname);
                labels.Add(telNo);
            }

            //Heri biri için pozisyon ayarlamaları ve Ekrana ekleme
            foreach (Label label in labels)
            {
                label.Size = new Size(btnSizeX, btnSizeY);
                label.Location = new Point(x, y);
                label.Font = new Font(label.Font.FontFamily, 10);
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

        private void ShowCustomers_Load(object sender, EventArgs e)
        {

        }
    }
}
