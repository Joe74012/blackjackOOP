namespace blackjackOOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            card1 = new card();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "OK";
        }
    }
}
