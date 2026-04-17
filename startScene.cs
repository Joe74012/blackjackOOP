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
        private ShoeSetup shoeSetup;
        private DealSetup dealSetup;
        private Dealer dealer;
        private ScoreSetup scoreSetup;
        private bool isShuffled = false;
        private bool gameStarted = false;
        private PlayerSetup playerSetup;

        private Random rnd = new Random();

        public startScene(ShoeSetup shoeSetup, PlayerSetup playerSetup)
        {
            InitializeComponent();
            this.playerSetup = playerSetup;
            this.shoeSetup = shoeSetup;
            this.players = playerSetup.AantalSpelers;
            this.scoreSetup = new ScoreSetup();

            label1.Text = "Aantal kaarten: " + shoeSetup.TotaalKaarten();
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
            buttonHit.Visible = false;
            buttonHit.Enabled = false;
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
            pictureBoxDealer2.Image = getCardImage(dealSetup.DealerKaart2);
        }

        private async Task DealCards(int aantalSpelers)
        {
            PictureBox[,] cardBoxes = {
                { pictureBox1, pictureBox2 },
                { pictureBox3, pictureBox4 },
                { pictureBox5, pictureBox6 },
                { pictureBox7, pictureBox8 }
            };

            dealSetup = new DealSetup(playerSetup, shoeSetup);
            botPlayers = dealSetup.Spelers;
            playerBoxes.Clear();

            dealer = new Dealer();
            dealer.Hand.Add(dealSetup.DealerKaart1);
            dealer.Hand.Add(dealSetup.DealerKaart2);

            for (int i = 0; i < botPlayers.Count; i++)
            {
                cardBoxes[i, 0].Image = getCardImage(botPlayers[i].Hand[0]);
                playerBoxes.Add(new List<PictureBox> { cardBoxes[i, 0] });
                await Task.Delay(500);

                cardBoxes[i, 1].Image = getCardImage(botPlayers[i].Hand[1]);
                playerBoxes[i].Add(cardBoxes[i, 1]);
                await Task.Delay(250);
            }

            pictureBoxDealer1.Image = getCardImage(dealSetup.DealerKaart1);
            await Task.Delay(250);

            pictureBoxDealer2.Image = getCardBackImage();
            await Task.Delay(250);

            label1.Text = "Aantal kaarten: " + shoeSetup.TotaalKaarten();

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
                    Card newCard = shoeSetup.DealKaart();
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

            buttonHit.Visible = true;
            buttonHit.Enabled = true;
        }

        private async void buttonShuffle_Click(object sender, EventArgs e)
        {
            buttonShuffle.Enabled = false;
            buttonShuffle.Visible = false;
            labelShuffle.Text = "Shuffling...";

            shoeSetup.Shuffle();

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
            buttonDeal.Enabled = false;
            buttonDeal.Visible = false;

            try
            {
                await DealCards(players);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                buttonDeal.Enabled = true;
                buttonDeal.Visible = true;
            }

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

        private void buttonHit_Click(object sender, EventArgs e)
        {
            if (dealer.MoetHitten())
            {
                scoreSetup.GeefPunten();
                labelLog.Text += $"Dealer hit +1 punt (score: {scoreSetup.Punten})";
            }
            else
            {
                scoreSetup.GeefStrafpunten();
                labelLog.Text += $"Dealer had niet moeten hitten -1 punt (score: {scoreSetup.Punten})";
            }

            Card newCard = shoeSetup.DealKaart();
            dealer.Hand.Add(newCard);

            PictureBox newBox = new PictureBox();
            newBox.SizeMode = PictureBoxSizeMode.Zoom;
            newBox.Size = pictureBoxDealer2.Size;
            newBox.Location = new Point(pictureBoxDealer2.Location.X + pictureBoxDealer2.Width + 5, pictureBoxDealer2.Location.Y);
            newBox.Image = getCardImage(newCard);
            this.Controls.Add(newBox);

            buttonHit.Enabled = false;
            buttonHit.Visible = false;
            buttonReveal.Enabled = true;
            buttonReveal.Visible = true;
        }
    }
}