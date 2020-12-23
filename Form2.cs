using BlackjackWpf.DAL;
using BlackjackWpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackjackWpf
{
    public partial class Form2 : Form
    {
        public Form1 f1 = new Form1();
        public Form2()
        {

            f1.Hide();
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            PlayerDAL pd = new PlayerDAL();
            string name = textBox1.Text;
            int age = int.Parse(textBox2.Text);
            double wallet = double.Parse(textBox3.Text);
            string password = textBox5.Text;

            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                textBox7.Text = "CapsLock is on";
            }

            if (!name.Equals("") || age.Equals("") || !wallet.Equals("") || !password.Equals(""))
            {
                
                pd.AddPlayer(name, age, wallet, password);
            }
            else
            {
                textBox1.Text = "please fill out the form.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox6.Multiline = false;
            
            PlayerDAL pd = new PlayerDAL();
            string name = textBox4.Text;
            string pwd = textBox6.Text;
            
            if(!name.Equals("") || !pwd.Equals(""))
            {
                
                Player p = pd.LogInPlayer(name, pwd);
                if(p != null)
                {
                    
                    f1.Show();
                    
                    
                }
                else
                {
                    textBox7.Text = "wrong password or username";
                }

            }
            else
            {
                textBox7.Text = "please fill out form";
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        
    }
}
