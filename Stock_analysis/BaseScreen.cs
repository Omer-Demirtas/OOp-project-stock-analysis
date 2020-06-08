using Stock_analysis.Models;
using Stock_analysis.Repository;
using Stock_analysis.View;
using Stock_analysis.View.Show;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_analysis
{
    
    public partial class BaseScreen : Form
    {
        private ICustomerRepository customerRepo;
        private ISaleRepository saleRepo;
        private IProductRepository productRepo;
        private IPurchaseRepository purchaseRepo;

        private List<Label> labels = new List<Label>();
        private int p1x;

        //COmpanent
        private Timer timer = new Timer();
        private Panel panel1;
        private Panel panel2;

        //Slider da yön için
        private bool right = false;
        private bool left = false;

        public BaseScreen(ICustomerRepository customerRepo, ISaleRepository saleRepo,
            IProductRepository productRepo, IPurchaseRepository purchaseRepo)
        {
            this.purchaseRepo = purchaseRepo;
            this.customerRepo = customerRepo;
            this.saleRepo = saleRepo;
            this.productRepo = productRepo;
            
            InitializeComponent();

            Settings.directory = System.IO.Directory.GetCurrentDirectory() + @"\images\" ;

            CreateUi();
        }

        
        private void BtnEvent(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                String Text = (sender as Button).Text;

                switch (Text)
                {
                    case "Ürünler":

                        ShowProducts sp = new ShowProducts(productRepo.GetAll());
                        sp.Show();
                        break;

                    case "Müşteriler":
                        ShowCustomers sc = new ShowCustomers(customerRepo.GetAll(), customerRepo, saleRepo);
                        sc.Show();
                        break;

                    //Tüm Satışları Çekmek için
                    case "Satışlar":
                        ShowSales ss = new ShowSales(saleRepo.GetAll(),customerRepo, productRepo);
                        ss.Show();
                        break;
                    case "Alımlar":
                        ShowPurchase sP = new ShowPurchase(purchaseRepo.GetAll(), productRepo);
                        sP.Show();
                        break;

                }
            }
            else
            {
                //burada ekrandaki göresellerin tıklandığındaki işlevi
                Label label = (sender as Label);

                if (label.Equals(labels[2]))
                {
                    CreateCustomers cc = new CreateCustomers(customerRepo);
                    cc.ShowDialog();
                }
                else if (label.Equals(labels[1]))
                {
                    Createpurchase cp = new Createpurchase(productRepo, purchaseRepo);
                    cp.ShowDialog();
                }
                else if (label.Equals(labels[0]))
                {
                    CreateSales cs = new CreateSales(saleRepo, customerRepo, productRepo);
                    cs.ShowDialog();
                }
            }
        }

        public void Slider(object sender, System.EventArgs e)
        {
            if (right)
            {
                p1x += 5;
                if (p1x >= 300)
                {
                    timer.Enabled = false;
                    right = false;
                    left = true;
                }
                else
                {
                    panel2.Location = new Point((p1x - 300), 0);
                    panel1.Location = new Point(p1x, 0);
                }
            }
            else if (left)
            {
                p1x -= 5;
                if (p1x <= 100)
                {
                    timer.Enabled = false;
                    right = true;
                    left = false;
                }
                else
                {
                    panel2.Location = new Point((p1x - 300), 0);
                    panel1.Location = new Point(p1x, 0);
                }
            }
        }
        //Görüntüyü oluşturan kompanentlerinin kurulması
        private void CreateUi()
        {
            //ekran ayarlamaları 

            this.Size = new Size(Settings.winX, Settings.winY);

            //panel1 sol bar
            //panel2 ana ekran

            panel1 = new Panel();
            panel2 = new Panel();

            panel1.MouseEnter += panel1_MouseEnter;
            panel2.MouseEnter += panel2_MouseEnter;

            this.Controls.Add(panel1);
            this.Controls.Add(panel2);

            //Sol Menüdeki buttonlar
            int btny = 100;
            List<String> BtnTexts = new List<string>()
            {"Ürünler", "Müşteriler", "Satışlar", "Alımlar"};

            foreach (String text in BtnTexts)
            {
                Button btn = new Button();
                btn.Text = text;
                btn.Size = new Size(300, 50);
                btn.Location = new Point(0, btny);
                btn.Font = new Font(btn.Font.FontFamily, 14);
                panel2.Controls.Add(btn);
                btny += 50;
                btn.Click += BtnEvent;

            }

            //Slider için Timer Ayarları
            timer.Interval = 1;
            timer.Tick += Slider;
            timer.Enabled = false;

            p1x = 100;

            panel1.Location = new Point(p1x, 0);
            panel1.Size = new Size((Settings.winX - 100), Settings.winY);

            panel2.Location = new Point((p1x - 300), 0);
            panel2.Size = new Size(300, Settings.winY);

            //Satıi müşteri ve ürün ekleme butonları
            String[] resimAnlam = { "Satış oluitur", "Ürün Ekle", "Müşteri Ekle" };
            String[] resimler = { "addSales.png", "addProdycts.png", "addUser.png" };
            int i = 0;
            foreach (String resim in resimler)
            {
                Label l = new Label();
                Label la = new Label();

                la.Size = new Size(100, 50);
                l.Size = new Size(50, 50);
                la.Location = new Point(((50 + i * 100)), Settings.winY - ((2 * l.Size.Height) + 20));
                l.Location = new Point(((50 + i * 100)), Settings.winY - ((2 * l.Size.Height) + 80));
                l.Click += BtnEvent;
                Image image = Image.FromFile(Settings.directory + resim);
                Bitmap img = new Bitmap(image, l.Size);
                l.Image = img;
                la.Text = resimAnlam[i];

                labels.Add(l);
                //labels.Add(la);

                panel1.Controls.Add(la);
                panel1.Controls.Add(l);
                i++;
            }
        }

        //farenin elrandaki haraketine göre ekrandaki menüyüa çmak
        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            right = true;
            timer.Enabled = true;
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            timer.Enabled = true;
            right = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
