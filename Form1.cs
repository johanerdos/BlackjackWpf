using BlackjackWpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace BlackjackWpf
{
    public partial class Form1 : Form
    {

        Player player = new Player("Admin", 22, 2000);
        public List<PictureBox> playerPictures;
        public List<PictureBox> dealerPictures;
        public const string imagePath = @"Cards/";
        public const string imgPathBackground = @"Pictures/";
        Card card = new Card();
        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();
        int bet = 0;
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            playerPictures = new List<PictureBox>();
            dealerPictures = new List<PictureBox>();

            Image imgTable = Image.FromFile(imgPathBackground + "table.jpg");

            imgTable = ResizeImage(imgTable, new Size(1000, 2000));

            this.BackgroundImage = imgTable;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Card c1 = new Card("J", 10, "S");
            Card c2 = new Card("Q", 10, "C");
            Card c3 = card.PrintCard();
            playerHand.Add(c1);
            playerHand.Add(c2);
            dealerHand.Add(c3);
            CreatePlayerControls(playerHand);
            CreateDealerControls(dealerHand);
            DisplayPlayerControls();
            DisplayDealerControls();
        }

        private void CreatePlayerControls(List<Card> playerHand)
        {
            int value = 0;
            for (int i = 0; i < playerHand.Count; i++)
            {
                string cardRank = playerHand[i].Rank;
                string cardSuit = playerHand[i].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                playerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
            }
            foreach (Card c in playerHand)
            {
                value += c.Value;
            }
            label1.Text = value.ToString();

        }

        private void CreateDealerControls(List<Card> dealerHand)
        {
            int value = 0;

            for (int i = 0; i < dealerHand.Count; i++)
            {
                string cardRank = dealerHand[i].Rank;
                string cardSuit = dealerHand[i].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                dealerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
            }
            foreach (Card c in dealerHand)
            {
                value += c.Value;
            }
            label2.Text = value.ToString();

        }

        public PictureBox SizeImage(PictureBox pb, string cardRank, string cardSuit)
        {

            string concatPath = cardRank + cardSuit;

            Image img = Image.FromFile(imagePath + concatPath + ".jpg");
            img = ResizeImage(img, new Size(pb.Height, pb.Width));
            pb.Image = img;
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Top = 100;

            return pb;
        }

        public static Image ResizeImage(Image image, Size size)
        {
            return (Image)(new Bitmap(image, size));

        }
        private void DisplayPlayerControls()
        {
            for (int i = 0; i < playerHand.Count; i++)
            {
                playerPictures[i].Left = (i * 18) + 100;
                this.Controls.Add(playerPictures[i]);
            }
            CheckScore(playerHand, dealerHand);
        }

        private void DisplayDealerControls()
        {
            for (int i = 0; i < dealerHand.Count; i++)
            {
                dealerPictures[i].Left = (i * 18) + 500;
                this.Controls.Add(dealerPictures[i]);
            }
            CheckScore(playerHand, dealerHand);
        }

        private void Hit(object sender, EventArgs e)
        {
            int value = 0;
            Card c4 = card.PrintCard();
            playerHand.Add(c4);
            CreatePlayerControls(playerHand);
            DisplayPlayerControls();

            foreach (Card c in playerHand)
            {
                value += c.Value;
            }
            label1.Text = value.ToString();

        }

        private void Stay(object sender, EventArgs e)
        {
            int value = dealerHand[0].Value;

            Card c5 = card.PrintCard();
            dealerHand.Add(c5);
            CreateDealerControls(dealerHand);
            DisplayDealerControls();

            foreach (Card c in dealerHand)
            {
                value += c.Value;
            }
            label2.Text = value.ToString();

        }

        private void Bet50(object sender, EventArgs e)
        {
            bet += 50;
            betLabel.Text = bet.ToString();
        }

        private void Bet100(object sender, EventArgs e)
        {
            bet += 100;
            betLabel.Text = bet.ToString();
        }

        private void Bet20(object sender, EventArgs e)
        {
            bet += 20;
            betLabel.Text = bet.ToString();
        }

        private void CheckScore(List<Card> playerHand, List<Card> dealerHand)
        {
            int playerVal = 0;
            int dealerVal = 0;
            foreach (Card c in playerHand)
            {
                playerVal += c.Value;
            }

            foreach (Card ca in dealerHand)
            {
                dealerVal += ca.Value;
            }

            if (playerVal == 21)
            {
                BlackJack();
            }
            else if (playerVal > 21)
            {
                Loser();
            }
            else if (dealerVal > 21)
            {
                Winner();
            }
            else if (playerVal > dealerVal && dealerVal < 21 && playerVal < 21)
            {
                Winner();
            }
            else if (playerVal < dealerVal && playerVal < 21 && dealerVal < 21)
            {
                Loser();
            }


        }

        private void BlackJack()
        {
            resultLable.Text = "You got blackjack!";
            player.Wallet = bet * (bet / 2);
        }

        private void Winner()
        {
            resultLable.Text = "Player wins";
            player.Wallet += bet;
        }

        private void Loser()
        {
            resultLable.Text = "Dealer wins";
            player.Wallet -= bet;
        }

        private void ResetHand(List<Card> playerHand, List<Card> dealerHand)
        {
            for(int i = 0; i < playerHand.Count; i++)
            {
                playerHand.RemoveAt(i);

                for(int j = 0; i < dealerHand.Count; i++)
                {
                    dealerHand.RemoveAt(i);
                }
            }
        }
    }
}
