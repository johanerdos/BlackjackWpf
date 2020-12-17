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

        public Player()
        {

        }
        public Player(string _name, int _age, double _wallet)
        {
            Name = _name;
            Age = _age;
            Wallet = _wallet;
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
    }
}
