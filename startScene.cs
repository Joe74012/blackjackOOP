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

        private Image getCardImage(Card card)
        {
            string cardName = card.getImage();
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Cards");
            folderPath = Path.GetFullPath(folderPath);
            string[] files = Directory.GetFiles(folderPath, $"*{cardName}");
            if (files.Length > 0)
                return Image.FromFile(files[0]);
            return null;
        }

        private void DealCards(deck deck, int aantalSpelers)
        {
            Label[] nameLabels = { label4, label5, label6, label7 };

            PictureBox[,] cardBoxes = {
                { pictureBox1, pictureBox2 },
                { pictureBox3, pictureBox4 },
                { pictureBox5, pictureBox6 },
                { pictureBox7, pictureBox8 }
            };

            int cardIndex = 0;
            for (int i = 0; i < aantalSpelers; i++)
            {
                Card card1 = deck.Cards[cardIndex++];
                Card card2 = deck.Cards[cardIndex++];

                cardBoxes[i, 0].Image = getCardImage(card1);
                cardBoxes[i, 1].Image = getCardImage(card2);
            }
        }
    }
}
