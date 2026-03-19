namespace blackjackOOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Card card1 = new Card(Rank.ACE, Suit.HEARTS);
            Card card2 = new Card(Rank.TWO, Suit.HEARTS);
            label1.Text = card1.ToString() + " " + card2.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "OK";
        }


        private void label2_Load(object sender, EventArgs e)
        {
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            label2.Top = (this.ClientSize.Height - label2.Height) / 3;
        }

        private void button3_Load(object sender, EventArgs e)
        {
            button3.BackColor = Color.Green;
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterPanels();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterPanels();
        }

        private void CenterPanels()
        {
            flowLayoutPanel1.Left = (this.ClientSize.Width - flowLayoutPanel1.Width) / 2;
            flowLayoutPanel1.Top = (this.ClientSize.Height - flowLayoutPanel1.Height) / 2;
            flowLayoutPanel2.Left = (this.ClientSize.Width - flowLayoutPanel3.Width) / 2;
            flowLayoutPanel2.Top = flowLayoutPanel1.Top - flowLayoutPanel3.Height + 15;
            flowLayoutPanel3.Left = (this.ClientSize.Width - flowLayoutPanel3.Width) / 2;
            flowLayoutPanel3.Top = flowLayoutPanel1.Top - flowLayoutPanel3.Height - 15;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Green;
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }
    }
}
