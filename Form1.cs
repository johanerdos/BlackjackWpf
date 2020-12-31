using BlackjackWpf.DAL;
using BlackjackWpf.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace BlackjackWpf
{
    public partial class Form1 : Form
    {
        PlayerDAL pd = new PlayerDAL();
        private Random random = new Random();

        public List<PictureBox> playerPictures;
        public List<PictureBox> dealerPictures;
        public const string imagePath = @"Cards/";
        public const string imgPathBackground = @"Pictures/";
        
        List<Card> playerHand = new List<Card>();
        List<Card> dealerHand = new List<Card>();
        int bet = 0;
        public bool resetBtnClicked = false;
        public const string soundPath = @"SoundEffects/";
        Player player = new Player();

        DataTable dt = new DataTable();
        
        
        public Form1(string playerName)
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

            player = pd.FindPlayer(playerName);
            
            InitializeComponent();

            if (String.IsNullOrEmpty(betLabel.Text))
            {
                button1.Enabled = false;
            }

            button5.Enabled = false;
            button6.Enabled = false;

            dt.Columns.Add("Player Name");
            dt.Columns.Add("Player Wallet");

            DataRow dr = dt.NewRow();
            dr["Player Name"] = player.Name;
            dr["Player Wallet"] = player.Wallet;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playerHand.Count > 0 && dealerHand.Count > 0)
            {
                ResetHand(playerHand, dealerHand, playerPictures, dealerPictures);
                
                
                Thread.Sleep(2000);
            }

            if (String.IsNullOrEmpty(betLabel.Text))
            {
                button1.Enabled = false;
            }
            
            Card card = new Card();
            for(int i = 0; i < 2; i++)
            {
                
                Card c1 = card.PrintCard(random);
                playerHand.Add(c1);
            }

            Card c3 = card.PrintCard(random);
            dealerHand.Add(c3);
            int count = playerHand.Count;
            CreatePlayerControls(playerHand, count);
            CreateDealerControls(dealerHand);

            DisplayPlayerControls();
            DisplayDealerControls();

            button5.Enabled = true;
            button6.Enabled = true;
        }

        private void CreatePlayerControls(List<Card> playerHand, int count)
        {
            
            int value = 0;
            //Must be a better way of doing this but it'll do for now
            if (count == 2)
            {
                for (int i = 0; i < playerHand.Count; i++)
                {
                    string cardRank = playerHand[i].Rank;
                    string cardSuit = playerHand[i].Suit;

                    //need to fix better locations of the cards
                    var newPictureBox = new PictureBox();
                    newPictureBox.Height = 110;
                    newPictureBox.Width = 100;
                    playerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
                }
            }
            else
            {
                for (int i = 0; i < playerHand.Count; i++)
                {
                    string cardRank = playerHand[playerHand.Count - 1].Rank;
                    string cardSuit = playerHand[playerHand.Count - 1].Suit;

                    var newPictureBox = new PictureBox();
                    newPictureBox.Height = 110;
                    newPictureBox.Width = 100;
                    playerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
                }
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
                string cardRank = dealerHand[dealerHand.Count - 1].Rank;
                string cardSuit = dealerHand[dealerHand.Count - 1].Suit;

                var newPictureBox = new PictureBox();
                newPictureBox.Height = 110;
                newPictureBox.Width = 100;
                dealerPictures.Add(SizeImage(newPictureBox, cardRank, cardSuit));
            }
            foreach(Card  c in dealerHand)
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
            //int value = playerHand[0].Value + playerHand[1].Value;
            int value = 0;
            Card card = new Card();
            Card c4 = new Card("8", 8, "S");
            //Card c4 = card.PrintCard();
            playerHand.Add(c4);
            int count = playerHand.Count;
            CreatePlayerControls(playerHand, count);
            DisplayPlayerControls();

            foreach (Card c in playerHand)
            {
                value += c.Value;
            }
            if(value > 21)
            {
                Loser();
                button5.Enabled = false;
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
            button1.Enabled = true;
            
        }

        private void Bet100(object sender, EventArgs e)
        {
            bet += 100;
            betLabel.Text = bet.ToString();
            button1.Enabled = true;
        }

        private void Bet20(object sender, EventArgs e)
        {
            bet += 20;
            betLabel.Text = bet.ToString();
            button1.Enabled = true;
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
            UpdateDT();
            
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void Winner()
        {
            resultLable.Text = "Player wins";
            player.Wallet += bet * 2; 
            UpdateDT();
            
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void Loser()
        {
            resultLable.Text = "Dealer wins";
            player.Wallet -= bet;
            UpdateDT();
            
            button5.Enabled = false;
            button6.Enabled = false;
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
            button5.Enabled = true;
            button6.Enabled = true;

        }

        private int St(int dealerNumber)
        {
            dealerNumber = 0;
            
            Card card = new Card();
            Card c = card.PrintCard(random);
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
        private void UpdateDT()
        {
            dt.Clear();
            DataRow dr = dt.NewRow();
            dr["Player Name"] = player.Name;
            dr["Player Wallet"] = player.Wallet;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close this application?", "Exit", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                pd.UpdatePlayer(player.Name, player.Wallet);
                e.Cancel = false;
            }
        }
    }
}
