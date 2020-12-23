using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackWpf.Model
{
    class Player
    {
        private string _name;
        private int _age;
        private double _wallet;
        private string _pwd;

        public Player()
        {

        }
        public Player(string _name, int _age, double _wallet, string _pwd)
        {
            Name = _name;
            Age = _age;
            Wallet = _wallet;
            Password = _pwd;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public double Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        public string Password
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
    }
}
