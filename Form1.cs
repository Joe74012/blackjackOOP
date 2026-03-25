namespace blackjackOOP
{
    public partial class Form1 : Form
    {
        int players;

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

        private void button2_Click(object sender, EventArgs e)
        {
            players = 1;
            givePlayerNames(players);
            label5.Text = players.ToString();
            button2.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            players = 4;
            givePlayerNames(players);
            label5.Text = players.ToString();
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
            players = 2;
            givePlayerNames(players);
            label5.Text = players.ToString();
            button3.BackColor = Color.Green;
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            players = 3;
            givePlayerNames(players);
            label5.Text = players.ToString();
            button4.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void givePlayerNames(int aantalSpelers)
        {
            string[] naamSpeler1 = { "Alice", "Bob", "Charlie", "Diana", "Eve" };
            string[] naamSpeler2 = { "Frank", "Grace", "Hank", "Ivan", "Julia" };
            string[] naamSpeler3 = { "Kevin", "Laura", "Mike", "Nina", "Oscar" };
            string[] naamSpeler4 = { "Paul", "Quinn", "Rachel", "Steve", "Tina" };

            Random random = new Random();

            switch (aantalSpelers)
            {
                case 1:
                    label6.Text = naamSpeler1[random.Next(0, 5)];
                    label7.Text = "";
                    label8.Text = "";
                    label9.Text = "";
                    break;
                case 2:
                    label6.Text = naamSpeler1[random.Next(0, 5)];
                    label7.Text = naamSpeler2[random.Next(0, 5)];
                    label8.Text = "";
                    label9.Text = "";
                    break;
                case 3:
                    label6.Text = naamSpeler1[random.Next(0, 5)];
                    label7.Text = naamSpeler2[random.Next(0, 5)];
                    label8.Text = naamSpeler3[random.Next(0, 5)];
                    label9.Text = "";
                    break;
                case 4:
                    label6.Text = naamSpeler1[random.Next(0, 5)];
                    label7.Text = naamSpeler2[random.Next(0, 5)];
                    label8.Text = naamSpeler3[random.Next(0, 5)];
                    label9.Text = naamSpeler4[random.Next(0, 5)];
                    break;
            }
        }
    }
}
