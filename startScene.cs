using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blackjackOOP
{
    public partial class startScene : Form
    {
        private int players;
        private List<Player> botPlayers = new List<Player>();
        private List<List<PictureBox>> playerBoxes = new List<List<PictureBox>>();
        private deck[] currentDecks; //shoe
        private Card dealerCard1;
        private Card dealerCard2;
        private bool isShuffled = false;
        private bool gameStarted = false;
        private PlayerSetup playerSetup;

        private Random rnd = new Random();
        public startScene(ShoeSetup shoeSetup, PlayerSetup playerSetup)
        {
            InitializeComponent();
            this.playerSetup = playerSetup;
            this.players = playerSetup.AantalSpelers;

            currentDecks = shoeSetup.Decks.ToArray();

            int totaal = currentDecks.Sum(d => d.Cards.Count);
            label1.Text = "Aantal kaarten: " + totaal;
            label2.Text = "Spelers: " + playerSetup.AantalSpelers;

            label4.Text = playerSetup.Namen.Count > 0 ? playerSetup.Namen[0] : "";
            label5.Text = playerSetup.Namen.Count > 1 ? playerSetup.Namen[1] : "";
            label6.Text = playerSetup.Namen.Count > 2 ? playerSetup.Namen[2] : "";
            label7.Text = playerSetup.Namen.Count > 3 ? playerSetup.Namen[3] : "";

            buttonStart.Visible = false;
            buttonStart.Enabled = false;
            buttonDeal.Visible = false;
            buttonDeal.Enabled = false;
            buttonReveal.Visible = false;
            buttonReveal.Enabled = false;
        }

        private Card DealFromShoe()
        {
            deck gekozenDeck;

            do
            {
                gekozenDeck = currentDecks[rnd.Next(currentDecks.Length)];
            }
            while (gekozenDeck.Cards.Count == 0);

            return gekozenDeck.Deal();
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

        private void RevealDealerCard()
        {
            pictureBoxDealer2.Image = getCardImage(dealerCard2);
        }

        private async Task DealCards(int aantalSpelers)
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

                Card card1 = DealFromShoe();
                player.Hand.Add(card1);
                cardBoxes[i, 0].Image = getCardImage(card1);
                playerBoxes.Add(new List<PictureBox> { cardBoxes[i, 0] });
                await Task.Delay(500);

                Card card2 = DealFromShoe();
                player.Hand.Add(card2);
                cardBoxes[i, 1].Image = getCardImage(card2);
                playerBoxes[i].Add(cardBoxes[i, 1]);
                await Task.Delay(250);
            }

            dealerCard1 = DealFromShoe();
            pictureBoxDealer1.Image = getCardImage(dealerCard1);
            await Task.Delay(250);

            dealerCard2 = DealFromShoe();
            pictureBoxDealer2.Image = getCardBackImage();
            await Task.Delay(250);

            int totaal = currentDecks.Sum(d => d.Cards.Count);
            label1.Text = "Aantal kaarten: " + totaal;

            gameStarted = true;
            buttonStart.Visible = true;
            buttonStart.Enabled = true;
        }

        private async Task PlayBotTurns(List<Player> players)
        {
            string log = "";

            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                string action = player.BotDecide();

                if (action == "hit")
                {
                    Card newCard = DealFromShoe();
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

        private async void buttonShuffle_Click(object sender, EventArgs e)
        {
            buttonShuffle.Enabled = false;
            buttonShuffle.Visible = false;
            labelShuffle.Text = "Shuffling...";

            for (int i = 0; i < 15; i++)
            {
                int totaal = currentDecks.Sum(d => d.Cards.Count);
                Card randomCard = currentDecks[rnd.Next(currentDecks.Length)].Cards[0];
                pictureBoxDealer1.Image = getCardImage(randomCard);
                pictureBoxDealer2.Image = getCardImage(randomCard);
                await Task.Delay(80);
            }

            foreach (deck d in currentDecks)
            {
                d.Shuffle();
            }

            pictureBoxDealer1.Image = null;
            pictureBoxDealer2.Image = null;

            labelShuffle.Text = "Shuffled!";
            await Task.Delay(800);
            labelShuffle.Text = "";

            isShuffled = true;
            buttonDeal.Visible = true;
            buttonDeal.Enabled = true;
        }

        private async void buttonDeal_Click(object sender, EventArgs e)
        {
            if (!isShuffled)
            {
                MessageBox.Show("You must shuffle first!");
                return;
            }

            buttonDeal.Enabled = false;
            buttonDeal.Visible = false;

            await DealCards(players);

            buttonStart.Visible = true;
            buttonStart.Enabled = true;
        }

        private void buttonReveal_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                MessageBox.Show("Deal the cards first!");
                return;
            }

            buttonReveal.Enabled = false;
            buttonReveal.Visible = false;
            RevealDealerCard();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (!gameStarted)
            {
                MessageBox.Show("Deal the cards first!");
                return;
            }

            buttonStart.Visible = false;
            buttonStart.Enabled = false;

            await PlayBotTurns(botPlayers);

            buttonReveal.Enabled = true;
            buttonReveal.Visible = true;
        }
    }
}