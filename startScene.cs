using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blackjackOOP
{
    public partial class startScene : Form
    {
        // Variabelen opslaan in startScene
        private int kaarten;
        private int players;
        int kaartPerSpeler;

        // Constructor uitbreiden met parameters

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
                    label4.Text = naamSpeler1[random.Next(0, 5)];
                    label5.Text = "";
                    label6.Text = "";
                    label7.Text = "";
                    break;
                case 2:
                    label4.Text = naamSpeler1[random.Next(0, 5)];
                    label5.Text = naamSpeler2[random.Next(0, 5)];
                    label6.Text = "";
                    label7.Text = "";
                    break;
                case 3:
                    label4.Text = naamSpeler1[random.Next(0, 5)];
                    label5.Text = naamSpeler2[random.Next(0, 5)];
                    label6.Text = naamSpeler3[random.Next(0, 5)];
                    label7.Text = "";
                    break;
                case 4:
                    label4.Text = naamSpeler1[random.Next(0, 5)];
                    label5.Text = naamSpeler2[random.Next(0, 5)];
                    label6.Text = naamSpeler3[random.Next(0, 5)];
                    label7.Text = naamSpeler4[random.Next(0, 5)];
                    break;
            }
        }
        public startScene(int aantalKaarten, int aantalPlayers)
        {
            InitializeComponent();
            kaartPerSpeler = aantalKaarten / aantalPlayers;
            this.kaarten = aantalKaarten;
            this.players = aantalPlayers;
            label1.Text = "kaarten: "+aantalKaarten.ToString();
            label2.Text = "spelers: " + aantalPlayers.ToString();
            label3.Text = "kaarten per speler: " + kaartPerSpeler.ToString();
            givePlayerNames(aantalPlayers);
        }

    }
}
