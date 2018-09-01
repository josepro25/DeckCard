using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;

namespace DeckCard
{
    public partial class Form1 : Form
    {

        private DexCard currentCard;


        public Form1()
        {
            InitializeComponent();
            currentCard = null;

            richTextBox1.ContextMenu = contextMenu1;
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }




        private void Form1_Load(object sender, EventArgs e)
        {
            //listBox1.Items.Add(new DexCard("TEST"));
            // listBox1.Items.Add(new DexCard("TEST2"));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                splitContainer2.Visible = true;
                if (currentCard != null)
                {
                    currentCard.rtfData = richTextBox1.Rtf;
                }

                richTextBox1.Rtf = ((DexCard)listBox1.SelectedItem).rtfData;
                currentCard = (DexCard)listBox1.SelectedItem;
            }
            else
            {

                splitContainer2.Visible = false;
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Card cardAdd = new Add_Card("Create New Card");
            cardAdd.ShowDialog();

            if (cardAdd.newCardName != null)
                listBox1.Items.Add(new DexCard(cardAdd.newCardName));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DexCard rename = ((DexCard)listBox1.SelectedItem);

                Add_Card newName = new Add_Card("Rename Card");
                newName.ShowDialog();

                if (newName.newCardName != null)
                    rename.name = newName.newCardName;
                else
                    return;

                int oldIndex = listBox1.SelectedIndex;

                listBox1.Items.Remove(rename);
                listBox1.Items.Insert(oldIndex, rename);

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //listBox1.SelectedIndex = 0;

            if (listBox1.SelectedItem != null)
            {
                splitContainer2.Visible = true;
                if (currentCard != null)
                {
                    currentCard.rtfData = richTextBox1.Rtf;
                }

            }

            DialogResult dr = saveFileDialog1.ShowDialog();

            String path = "default.dex";
            try
            {

                if (saveFileDialog1.FileName != null && dr == DialogResult.OK)
                    path = saveFileDialog1.FileName;

                CardHive hive = new CardHive();

                foreach (Object o in listBox1.Items)
                {
                    DexCard card = (DexCard)o;
                    hive.Add(card);
                }

                hive.SerializeToXML(path);
            }
            catch (Exception de)
            {


            }


        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                CardHive ins = CardHive.DeserializeFromXML(openFileDialog1.FileName);

                listBox1.Items.Clear();



                foreach (DexCard kard in ins)
                {
                    listBox1.Items.Add(kard);


                }


            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            splitContainer2.Visible = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Font selFont = richTextBox1.SelectionFont;
          Font newFont = null;
            if (!(selFont.Style == FontStyle.Bold))
                 newFont = new Font(selFont.FontFamily, selFont.Size, FontStyle.Bold);
            else
                 newFont = new Font(selFont.FontFamily, selFont.Size, FontStyle.Regular);


            richTextBox1.SelectionFont = newFont;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font selFont = richTextBox1.SelectionFont;
            Font newFont = null;
            if (!(selFont.Style == FontStyle.Italic))
                newFont = new Font(selFont.FontFamily, selFont.Size, FontStyle.Italic);
            else
                newFont = new Font(selFont.FontFamily, selFont.Size, FontStyle.Regular);

            richTextBox1.SelectionFont = newFont;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will probably be removed, unless someone still has his 16-bit CARDFILE.exe files.");

            DialogResult dr = openFileDialog2.ShowDialog();

            if (dr == DialogResult.OK)
            {
                MGCImport mgci = new MGCImport(openFileDialog2.FileName);

                foreach (DexCard dcx in mgci.cards)
                {
                    listBox1.Items.Add(dcx);
                }
                

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }
    }

}