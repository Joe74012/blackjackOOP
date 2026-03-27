namespace blackjackOOP
{

    public partial class Form1 : Form
    {
        enum GameState
        {
            START,
            ROUND,
            SETUP
        }

        GameState currentGameState = GameState.SETUP;
        int ingevoerdGetal;
        int players;

        public Form1()
        {
            InitializeComponent();
            Card card1 = new Card(Rank.ACE, Suit.HEARTS);
            Card card2 = new Card(Rank.TWO, Suit.HEARTS);
            label1.Text = card1.ToString() + " " + card2.Value.ToString();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int getal))
            {
                ingevoerdGetal = getal;
            }
            switch (currentGameState)
            {
                case GameState.START:
                    currentGameState = GameState.ROUND;
                    break;
                case GameState.ROUND:
                    currentGameState = GameState.SETUP;
                    break;
                case GameState.SETUP:
                    //amount of players and decks function call
                    if (players != 0)
                    {
                        if (ingevoerdGetal > 0)
                        {
                            startScene scherm2 = new startScene();
                            scherm2.WindowState = FormWindowState.Maximized;
                            scherm2.Show();
                            this.Hide();
                            currentGameState = GameState.START;
                        }
                    }
                    break;
            }
            button1.Text = currentGameState.ToString();
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
            button2.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            players = 4;
            givePlayerNames(players);
            button5.BackColor = Color.Green;
            button3.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button4.BackColor = Color.FromArgb(255, 128, 0);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            players = 2;
            givePlayerNames(players);
            button3.BackColor = Color.Green;
            button4.BackColor = Color.FromArgb(255, 128, 0);
            button2.BackColor = Color.FromArgb(255, 128, 0);
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            players = 3;
            givePlayerNames(players);
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

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // teken wordt genegeerd
            }
        }
    }
}
