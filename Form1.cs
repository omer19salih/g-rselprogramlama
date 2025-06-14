using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace görsel2
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; // Aktif dosyanın yolu
        public ToolTip toolTip = new ToolTip(); // ToolTip nesnesi, kullanıcıya bilgi sağlamak için kullanılır.

        public Form1()
        {
            ConfigureUI(); // UI'yi yapılandırmak için çağrılır
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler (şu an boş)
        }

        // UI yapılandırma metodu
        private void ConfigureUI()
        {
            toolTip.IsBalloon = true; // Tooltip'in balon şeklinde görünmesini sağlar
            toolTip.ToolTipIcon = ToolTipIcon.Info; // Tooltip ikonunu bilgilendirme olarak ayarlar

            // Form özelliklerini ayarla
            this.Text = "Özgün Windows Forms Uygulaması"; // Başlık
            this.Width = 800; // Genişlik
            this.Height = 500; // Yükseklik
            this.StartPosition = FormStartPosition.CenterScreen; // Formun ekranda ortalanarak açılmasını sağlar
            this.BackColor = Color.LightGray; // Form arka plan rengini ayarlar

            // TabControl oluştur
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill; // TabControl'ün tüm alanı doldurmasını sağlar
            tabControl.Appearance = TabAppearance.FlatButtons; // Sekmelerin düz buton şeklinde görünmesini sağlar
            tabControl.SizeMode = TabSizeMode.Fixed; // Sekme boyutlarının sabit olmasını sağlar
            tabControl.ItemSize = new Size(120, 40); // Sekme boyutlarını ayarlar
            tabControl.Font = new Font("Arial", 10, FontStyle.Bold); // Sekme fontunu ayarlar
            tabControl.Padding = new Point(10, 5); // Sekmeler arasındaki boşluğu ayarlar

            // Sekmeleri oluştur
            TabPage page1 = new TabPage("🏠 Ana Sayfa");
            page1.BackColor = Color.Blue; // Sayfa arka plan rengini ayarla
            TextBox textBox1 = new TextBox() { Dock = DockStyle.Fill, Multiline = true }; // Metin kutusu oluştur
            page1.Controls.Add(textBox1); // TextBox'ı sayfaya ekle
            toolTip.SetToolTip(textBox1, "Ana sayfa sekmesi"); // Tooltip ekle

            TabPage page2 = new TabPage("ℹ️ Hakkında");
            page2.BackColor = Color.LightBlue; // Sayfa arka plan rengini ayarla
            Label aboutLabel = new Label() { Dock = DockStyle.Fill, Text = GetAboutContent(), Font = new Font("Arial", 12), Padding = new Padding(10), TextAlign = ContentAlignment.TopLeft }; // Etiket oluştur
            page2.Controls.Add(aboutLabel); // Etiketi sayfaya ekle
            toolTip.SetToolTip(aboutLabel, "Hakkımızda  sayfa sekmesi"); // Tooltip ekle

            TabPage page3 = new TabPage("❓ Yardım");
            page3.BackColor = Color.LightGreen; // Sayfa arka plan rengini ayarla
            Label helpLabel = new Label() { Dock = DockStyle.Fill, Text = GetHelpContent(), Font = new Font("Arial", 12), Padding = new Padding(10), TextAlign = ContentAlignment.TopLeft }; // Etiket oluştur
            page3.Controls.Add(helpLabel); // Etiketi sayfaya ekle
            toolTip.SetToolTip(helpLabel, "Yardım sayfa sekmesi"); // Tooltip ekle

            // Sekmeleri TabControl'a ekle
            tabControl.TabPages.Add(page1);
            tabControl.TabPages.Add(page2);
            tabControl.TabPages.Add(page3);

            // Menü Strip (Üst Menü) oluştur
            MenuStrip menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.White; // Menü strip arka plan rengini ayarla
            menuStrip.Font = new Font("Arial", 10, FontStyle.Bold); // Menü fontunu ayarla

            // Menü öğeleri oluştur
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("📂 Dosya");
            ToolStripMenuItem editMenu = new ToolStripMenuItem("✏️ Düzen");
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("❓ Yardım");

            // Dosya menüsüne alt öğeler ekle
            fileMenu.DropDownItems.Add("📄 Yeni", null, NewFile); // Yeni dosya oluştur
            fileMenu.DropDownItems.Add("📂 Aç", null, OpenFile); // Dosya aç
            fileMenu.DropDownItems.Add("💾 Kaydet", null, SaveFile); // Dosyayı kaydet
            fileMenu.DropDownItems.Add("🚪 Çıkış", null, (s, e) => Application.Exit()); // Uygulamadan çık

            // Düzen menüsüne alt öğeler ekle
            editMenu.DropDownItems.Add("✂️ Kes", null, CutText); // Metin kesme
            editMenu.DropDownItems.Add("📋 Kopyala", null, CopyText); // Metin kopyalama
            editMenu.DropDownItems.Add("📌 Yapıştır", null, PasteText); // Metin yapıştırma

            // Yardım menüsüne alt öğeler ekle
            helpMenu.DropDownItems.Add("ℹ️ Hakkında");
            helpMenu.DropDownItems.Add("📖 Kullanım Kılavuzu");

            // Menüleri menü strip'e ekle
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(helpMenu);

            // Sağ tıklama menüsü (ContextMenuStrip) oluştur
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("✂️ Kes", null, CutText); // Metin kesme
            contextMenu.Items.Add("📋 Kopyala", null, CopyText); // Metin kopyalama
            contextMenu.Items.Add("📌 Yapıştır", null, PasteText); // Metin yapıştırma
            contextMenu.Items.Add("❌ Sil"); // Silme seçeneği

            // Sağ tıklama menüsünü formda etkinleştir
            this.ContextMenuStrip = contextMenu;

            // Kontrolleri form üzerine ekle
            this.Controls.Add(tabControl);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Menü strip'in üstte görünmesini sağla
            menuStrip.Dock = DockStyle.Top;
        }

        // Hakkında sayfasının içeriği
        private string GetAboutContent()
        {
            return "Bu uygulama, Windows Forms kullanarak temel dosya yönetimi ve metin düzenleme işlemleri gerçekleştirmenize olanak tanır.\n" +
                   "Geliştiricinin amacı, kullanıcıların metin dosyaları üzerinde işlem yaparken kolaylık sağlamaktır.\n\n" +
                   "Özellikler:\n" +
                   "- Dosya açma, kaydetme ve yeni dosya oluşturma\n" +
                   "- Metin düzenleme (Kes, Kopyala, Yapıştır)\n" +
                   "- Yardım ve Hakkında sayfaları\n\n" +
                   "Uygulama, sade bir arayüzle kullanıcı dostu bir deneyim sunmayı amaçlamaktadır.";
        }

        // Yardım sayfasının içeriği
        private string GetHelpContent()
        {
            return "Kullanım Adımları:\n\n" +
                   "1. Yeni Dosya Oluşturma: 'Dosya' menüsünden 'Yeni' seçeneğine tıklayarak yeni bir dosya oluşturabilirsiniz.\n" +
                   "2. Dosya Açma: 'Dosya' menüsünden 'Aç' seçeneğine tıklayarak bilgisayarınızda bulunan bir dosyayı açabilirsiniz.\n" +
                   "3. Dosyayı Kaydetme: 'Dosya' menüsünden 'Kaydet' seçeneğine tıklayarak yapılan değişiklikleri kaydedebilirsiniz.\n" +
                   "4. Metin Düzenleme: 'Düzen' menüsünden kesme, kopyalama ve yapıştırma işlemleri yapılabilir.\n" +
                   "5. Yardım ve Hakkında: Uygulama hakkında bilgi almak veya kullanım kılavuzunu görmek için 'Yardım' menüsünden ilgili seçeneklere tıklayabilirsiniz.\n\n" +
                   "Eğer herhangi bir sorunuz olursa, lütfen bizimle iletişime geçin.  Web Developer  Salih Ömer Uyar salihomeruyar@gmail.com   ";
        }

        // Yeni dosya oluşturma
        private void NewFile(object sender, EventArgs e)
        {
            currentFilePath = string.Empty;
            foreach (TabPage tabPage in ((TabControl)this.Controls[0]).TabPages)
            {
                if (tabPage.Text == "🏠 Ana Sayfa")
                {
                    ((TextBox)tabPage.Controls[0]).Clear(); // Ana sayfadaki metni temizle
                }
            }
        }

        // Dosya açma
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                string content = File.ReadAllText(currentFilePath);
                foreach (TabPage tabPage in ((TabControl)this.Controls[0]).TabPages)
                {
                    if (tabPage.Text == "🏠 Ana Sayfa")
                    {
                        ((TextBox)tabPage.Controls[0]).Text = content; // İçeriği textBox'a yükle
                    }
                }
            }
        }

        // Dosyayı kaydetme
        private void SaveFile(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
            }

            string content = ((TextBox)((TabControl)this.Controls[0]).SelectedTab.Controls[0]).Text;
            File.WriteAllText(currentFilePath, content); // İçeriği dosyaya kaydet
        }

        // Metni kesme
        private void CutText(object sender, EventArgs e)
        {
            TextBox activeTextBox = (TextBox)((TabControl)this.Controls[0]).SelectedTab.Controls[0];
            if (activeTextBox != null && activeTextBox.SelectedText.Length > 0)
            {
                Clipboard.SetText(activeTextBox.SelectedText); // Seçilen metni panoya kopyala
                activeTextBox.SelectedText = string.Empty; // Seçilen metni sil
            }
        }

        // Metni kopyalama
        private void CopyText(object sender, EventArgs e)
        {
            TextBox activeTextBox = (TextBox)((TabControl)this.Controls[0]).SelectedTab.Controls[0];
            if (activeTextBox != null && activeTextBox.SelectedText.Length > 0)
            {
                Clipboard.SetText(activeTextBox.SelectedText); // Seçilen metni panoya kopyala
            }
        }

        // Metni yapıştırma
        private void PasteText(object sender, EventArgs e)
        {
            TextBox activeTextBox = (TextBox)((TabControl)this.Controls[0]).SelectedTab.Controls[0];
            if (activeTextBox != null)
            {
                activeTextBox.Paste(); // Panodaki metni yapıştır
            }
        }

        /*1. Form1 Yapıcı Metodu (Constructor)
 ConfigureUI() metodunu çağırarak kullanıcı arayüzünü yapılandırır. Bu metod, formun başlatılmasında gerekli olan tüm UI elemanlarını (menüler, sekmeler, ipuçları, vb.) oluşturur.
 2. Form1_Load
 Form yüklendiğinde yapılan işlemler burada yer alıyor, ancak bu metod şu anda boş.
 3. UI Yapılandırma (ConfigureUI Metodu)
 ToolTip: Kullanıcıya bilgi vermek amacıyla, sekmeler ve kontroller için tooltipler (ipucu metinleri) ayarlanır.
 Form Özellikleri: Formun başlık, boyut, başlangıç pozisyonu ve arka plan rengi gibi özellikler belirlenir.
 TabControl: Üç sekme oluşturulur:
 Ana Sayfa: Bir TextBox içerir, bu kutuda dosya içeriği düzenlenebilir.
 Hakkında: Uygulama hakkında bilgi veren bir Label içerir.
 Yardım: Kullanım kılavuzunu içeren bir Label içerir.
 MenuStrip: Üst menü çubuğu oluşturulur. Menüde üç ana başlık vardır:
 Dosya: Yeni dosya oluşturma, dosya açma, kaydetme ve çıkış seçenekleri içerir.
 Düzen: Kesme, kopyalama, yapıştırma işlemleri içerir.
 Yardım: Hakkında ve kullanım kılavuzunu içeren seçenekler bulunur.
 ContextMenuStrip: Sağ tıklama menüsü oluşturulur, bu menüde kesme, kopyalama, yapıştırma ve silme işlemleri bulunur.
 4. İçerik Metodları
 GetAboutContent(): Hakkında sayfasının içeriğini döndüren metot.
 GetHelpContent(): Yardım sayfasının içeriğini döndüren metot.
 5. Dosya İşlemleri
 NewFile: Yeni bir dosya oluşturulması için, TextBox içeriğini temizler ve dosya yolunu sıfırlar.
 OpenFile: Bir dosya seçilir ve içeriği TextBox'a yüklenir.
 SaveFile: Eğer dosya yolunu belirtmemişse, kullanıcıdan bir dosya yolu seçmesi istenir ve dosya kaydedilir.
 6. Metin Düzenleme İşlemleri
 CutText: Seçilen metni panoya kopyalar ve TextBox'tan siler.
 CopyText: Seçilen metni panoya kopyalar.
 PasteText: Panodaki metni TextBox'a yapıştırır.  */
    }
}
