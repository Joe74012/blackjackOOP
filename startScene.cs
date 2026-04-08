using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blackjackOOP
{
    public partial class startScene : Form
    {
        private int kaarten;
        private int players;
        int kaartPerSpeler;
        private Card dealerCard1;
        private Card dealerCard2;
        private List<Player> botPlayers = new List<Player>();
        private List<List<PictureBox>> playerBoxes = new List<List<PictureBox>>();
        private deck currentDeck;

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
            currentDeck = new deck(ingevoerdGetal);
            currentDeck.Shuffle();
            label1.Text = "Aantal kaarten: " + currentDeck.Cards.Count;
            this.players = aantalPlayers;
            label2.Text = "Spelers: " + aantalPlayers.ToString();
            givePlayerNames(aantalPlayers);
        }

        private async Task StartGame(deck deck, int aantalPlayers)
        {
            await DealCards(deck, aantalPlayers);
            await PlayBotTurns(deck, botPlayers);
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

        private Image getCardBackImage()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Cards");
            folderPath = Path.GetFullPath(folderPath);
            string backPath = Path.Combine(folderPath, "card_back.png");
            if (File.Exists(backPath))
                return Image.FromFile(backPath);
            return null;
        }

        private Image rotateImage(Image image)
        {
            Bitmap rotated = new Bitmap(image);
            rotated.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return rotated;
        }

        private void RevealDealerCard()
        {
            pictureBoxDealer2.Image = getCardImage(dealerCard2);
        }

        private async Task DealCards(deck deck, int aantalSpelers)
        {
            Label[] nameLabels = { label4, label5, label6, label7 };

            PictureBox[,] cardBoxes = {
                { pictureBox1, pictureBox2 },
                { pictureBox3, pictureBox4 },
                { pictureBox5, pictureBox6 },
                { pictureBox7, pictureBox8 }
            };

            botPlayers.Clear();
            playerBoxes.Clear();

            for (int i = 0; i < aantalSpelers; i++)
            {
                Player player = new Player(nameLabels[i].Text);
                botPlayers.Add(player);

                Card card1 = deck.Deal();
                player.Hand.Add(card1);
                cardBoxes[i, 0].Image = getCardImage(card1);
                playerBoxes.Add(new List<PictureBox> { cardBoxes[i, 0] });
                await Task.Delay(1000);

                Card card2 = deck.Deal();
                player.Hand.Add(card2);
                cardBoxes[i, 1].Image = getCardImage(card2);
                playerBoxes[i].Add(cardBoxes[i, 1]);
                await Task.Delay(1000);
            }

            dealerCard1 = deck.Deal();
            pictureBoxDealer1.Image = getCardImage(dealerCard1);
            await Task.Delay(1000);

            dealerCard2 = deck.Deal();
            pictureBoxDealer2.Image = getCardBackImage();
            await Task.Delay(1000);

            label1.Text = "Aantal kaarten: " + deck.Cards.Count;
        }

        private async Task PlayBotTurns(deck deck, List<Player> players)
        {
            string log = "";

            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                string action = player.BotDecide();

                if (action == "hit")
                {
                    Card newCard = deck.Deal();
                    player.Hand.Add(newCard);

                    PictureBox lastBox = playerBoxes[i][playerBoxes[i].Count - 1];
                    PictureBox newBox = new PictureBox();
                    newBox.SizeMode = PictureBoxSizeMode.Zoom;
                    newBox.Size = lastBox.Size;
                    newBox.Location = new Point(lastBox.Location.X + lastBox.Width + 5, lastBox.Location.Y);
                    newBox.Image = getCardImage(newCard);
                    this.Controls.Add(newBox);
                    playerBoxes[i].Add(newBox);

                    log += $"{player.Name} hits → got {newCard}\n";
                    await Task.Delay(1000);
                }
                else if (action == "stand")
                {
                    player.IsStanding = true;
                    log += $"{player.Name} stands with {player.HandValue()}\n";
                    await Task.Delay(1000);
                }

                labelLog.Text = log;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RevealDealerCard();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DealCards(currentDeck, players);
        }
    }
}