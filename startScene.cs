using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
        public startScene(int ingevoerdGetal, int aantalPlayers)
        {
            InitializeComponent();
            deck deck = new deck(ingevoerdGetal);
            deck.Shuffle();
            label1.Text = "Aantal kaarten: " + deck.Cards.Count;
            this.players = aantalPlayers;
            label2.Text = "Spelers: " + aantalPlayers.ToString();
            givePlayerNames(aantalPlayers);
            DealCards(deck, aantalPlayers);
        }

        private void DealCards(deck deck, int aantalSpelers)
        {
            Label[] nameLabels = { label4, label5, label6, label7 };
            Label[] cardLabels = { label8, label9, label10, label11 };

            int cardIndex = 0;

            for (int i = 0; i < aantalSpelers; i++)
            {
                Card card1 = deck.Cards[cardIndex++];
                Card card2 = deck.Cards[cardIndex++];

                string playerName = nameLabels[i].Text;
                cardLabels[i].Text = $"{playerName}: {card1} | {card2}";
            }
        }
    }
}
