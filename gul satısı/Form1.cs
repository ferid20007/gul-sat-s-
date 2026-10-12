using System.Globalization;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace gul_satısı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("col1", "Gül növü (gül adı)");
            dataGridView1.Columns.Add("col2", "Satış sayı");
            dataGridView1.Columns.Add("col3", "Qiyməti");
            dataGridView1.Columns.Add("col4", "Yekun qiyməti");

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;

        }



        private void button1_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Bütün xanaları doldurun!");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Say sıfırdan böyük tam ədəd olmalıdır!");
                textBox2.Focus();
                return;
            }

            string priceText = textBox3.Text.Trim().Replace(',', '.');

            if (!decimal.TryParse(
                priceText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal price) || price <= 0)
            {
                MessageBox.Show("Qiymət sıfırdan böyük olmalıdır!");
                textBox3.Focus();
                return;
            }

            string flowerName = textBox1.Text.Trim();
            decimal result = count * price;

            // Satışı cədvələ əlavə et
            dataGridView1.Rows.Add(
                flowerName,
                count.ToString(),
                price.ToString("F2", CultureInfo.InvariantCulture),
                result.ToString("F2", CultureInfo.InvariantCulture)
            );

            // Ümumi gəliri hesabla
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDecimal(
                    row.Cells[3].Value.ToString(),
                    CultureInfo.InvariantCulture);
            }

            // Toplam gəliri göstər
            label1.Text = "Toplam gəlir: " +
                total.ToString("F2", CultureInfo.InvariantCulture);

            // Xanaları təmizlə
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            textBox1.Focus();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult cavab = MessageBox.Show(
                "Çıxış edilsinmi?",
                "Təsdiq",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (cavab == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
