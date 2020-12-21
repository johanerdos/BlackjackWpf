using BlackjackWpf.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace BlackjackWpf
{
    public partial class Form1 : Form
    {
        
        Player player = new Player("Admin", 22, 2000);
        public List<PictureBox> playerPictures;
        public List<PictureBox> dealerPictures;
        public const string imagePath = @"Cards/";
        public const string imgPathBackground = @"Pictures/";
        
        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();
        int bet = 0;
        public bool resetBtnClicked = false;
        public const string soundPath = @"SoundEffects/";

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

            //SoundPlayer sp = new SoundPlayer(soundPath + "lounge.wav");
            //sp.Load();
            //sp.Play();


            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playerHand.Count > 0 && dealerHand.Count > 0)
            {
                ResetHand(playerHand, dealerHand, playerPictures, dealerPictures);
                
                
                Thread.Sleep(2000);
            }
            Card card = new Card();
            Card c1 = card.PrintCard();
            Card c2 = card.PrintCard();
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
                string cardRank = playerHand[playerHand.Count - 1].Rank;
                string cardSuit = playerHand[playerHand.Count - 1].Suit;

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
   
            for (int i = 0; i < dealerHand.Count; i++)
            {
                string cardRank = dealerHand[dealerHand.Count - 1].Rank;
                string cardSuit = dealerHand[dealerHand.Count - 1].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                dealerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
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

            for (int i = 0; i < playerHand.Count; i++)
            {
                playerPictures[i].Left = (i * 18) + 100;
                this.Controls.Add(playerPictures[i]);
            }

        }

        private void DisplayDealerControls()
        {

            for (int i = 0; i < dealerHand.Count; i++)
            {
                dealerPictures[i].Left = (i * 18) + 500;
                this.Controls.Add(dealerPictures[i]);
            }
            
        }

        private void Hit(object sender, EventArgs e)
        {
            //detta måste fixas
            int value = playerHand[0].Value + playerHand[1].Value;
            Card card = new Card();
            //Card c4 = new Card("4", 4, "S");
            Card c4 = card.PrintCard();
            playerHand.Add(c4);
            CreatePlayerControls(playerHand);
            DisplayPlayerControls();

            foreach (Card c in playerHand)
            {
                value += c.Value;
            }
            if(value > 21)
            {
                Loser();
            }

            label1.Text = value.ToString();

        }

        private void Stay(object sender, EventArgs e)
        {
            
            int value = 0;
            foreach(Card c in dealerHand)
            {
                value += c.Value;
               
            }
            while(value < 17)
            {
                
                value = St(value);
                if(value > 21)
                {
                    Winner();
                    break;
                }
            }
            label2.Text = value.ToString();
            CheckScore();
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

        private void CheckScore()
        {
            int playerVal = int.Parse(label1.Text);
            int dealerVal = int.Parse(label2.Text);

            if (playerVal == 21)
            {
                BlackJack();
                
            }
            if (playerVal > 21)
            {
                Loser();
            }
            if (dealerVal > 21)
            {
                Winner();
            }
            if (playerVal > dealerVal)
            {
                if(playerVal < 21)
                {
                    if(dealerVal < 21)
                    {
                        Winner();
                    }
                }

            }
            if (playerVal < dealerVal)
            {
                if(playerVal < 21)
                {
                    if(dealerVal < 21)
                    {
                        Loser();
                    }
                }
                
                
            }
           
        }

        private void BlackJack()
        {
            resultLable.Text = "You got blackjack!";
            player.Wallet += bet + (bet / 2);
            walletLbl.Text = player.Wallet.ToString();
            bet = 0;
        }

        private void Winner()
        {
            resultLable.Text = "Player wins";
            player.Wallet += bet * 2;
            walletLbl.Text = player.Wallet.ToString();
            bet = 0;
        }

        private void Loser()
        {
            resultLable.Text = "Dealer wins";
            player.Wallet -= bet;
            walletLbl.Text = player.Wallet.ToString();
            bet = 0;
        }

        private void ResetHand(List<Card> playerHand, List<Card> dealerHand, List<PictureBox> playerPictures, List<PictureBox> dealerPictures)
        {

            playerHand.Clear();
            dealerHand.Clear();

            for (int i = 0; i < playerPictures.Count; i++)
            {
                this.Controls.Remove(playerPictures[i]);

                for (int j = 0; j < dealerPictures.Count; j++)
                {
                    this.Controls.Remove(dealerPictures[j]);
                }
            }

            playerPictures.Clear();
            dealerPictures.Clear();
            DisplayPlayerControls();
            DisplayDealerControls();

            betLabel.Text = "";
            label1.Text = "";
            resultLable.Text = "";
            label2.Text = "";

        }

        private int St(int dealerNumber)
        {
            dealerNumber = 0;
            //Card c = new Card("8", 8, "S");
            Card card = new Card();
            Card c = card.PrintCard();
            dealerHand.Add(c);
            CreateDealerControls(dealerHand);
            Thread.Sleep(1000);
            DisplayDealerControls();

            foreach (Card ca in dealerHand)
            {
                dealerNumber += ca.Value;
            }


            return dealerNumber;
        }
    }
}
