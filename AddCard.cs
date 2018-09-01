using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DeckCard
{
    public partial class Add_Card : Form
    {
        public string newCardName = null;

        public Add_Card(String title)
        {
            InitializeComponent();
            this.Text = title;
        }

        private void Add_Card_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCardName = this.textBox1.Text;
            this.Close();
        }
    }

}