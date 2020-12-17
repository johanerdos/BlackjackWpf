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
        public PictureBox[] playerPictures;
        public PictureBox[] dealerPictures;
        public const string imagePath = @"Cards/";
        public const string imgPathBackground = @"Pictures/";
        Card card = new Card();
        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            playerPictures = new PictureBox[2];
            dealerPictures = new PictureBox[1];

            Image imgTable = Image.FromFile(imgPathBackground + "table.jpg");

            imgTable = ResizeImage(imgTable, new Size(1000, 2000));

            this.BackgroundImage = imgTable;

            Image imgBlackChip = Image.FromFile(imgPathBackground + "chipBlack.jpg");
            imgBlackChip = ResizeImage(imgBlackChip, new Size(100, 100));

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

            for (int i = 0; i < playerHand.Count; i++)
            {
                string cardRank = playerHand[i].Rank;
                string cardSuit = playerHand[i].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                playerPictures[i] = SizeImage(newPictureBox, cardRank, cardSuit);
            }

        }

        private void CreateDealerControls(List<Card> dealerHand)
        {
            for(int i = 0; i < dealerHand.Count; i++)
            {
                string cardRank = dealerHand[i].Rank;
                string cardSuit = dealerHand[i].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                dealerPictures[i] = SizeImage(newPictureBox, cardRank, cardSuit);
            }
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
            for (int i = 0; i < 2; i++)
            {
                playerPictures[i].Left = (i * 18) + 100;
                this.Controls.Add(playerPictures[i]);
            }
        }

        private void DisplayDealerControls()
        {
            for (int i = 0; i < 1; i++)
            {
                dealerPictures[i].Left = (i * 18) + 500;
                this.Controls.Add(dealerPictures[i]);
            }
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
            int value = 0;

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

        }

        private void Bet100(object sender, EventArgs e)
        {

        }

        private void Bet20(object sender, EventArgs e)
        {

        }
    }
}
