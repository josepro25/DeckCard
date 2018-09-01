using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DeckCard
{
    class MGCImport
    {
        public List<DexCard> cards;
        BinaryReader br;
        String fileMagic;
        int cardN;


        private DexCard readIndex()
        {
            int offset = br.ReadInt32();
           // MessageBox.Show("Going to offset " + offset);

            Int64 warpZone = br.BaseStream.Position;
            br.BaseStream.Seek(offset, SeekOrigin.Begin);

            String data;

            br.ReadBytes(2);
       
            int dl = br.ReadInt16();
            //MessageBox.Show("Length " + dl);
            data = new String(br.ReadChars(dl));
           // MessageBox.Show("Data " + data.Trim());
            br.BaseStream.Seek(warpZone,SeekOrigin.Begin);

            char cr = br.ReadChar();
           // MessageBox.Show(cr+"");

            String title = new String(br.ReadChars(47));
           // MessageBox.Show("Length " + title);
            

            DexCard dc = new DexCard(title.Trim(new char[]{'\0','\r','\n',' '}));

            dc.StoreRTF(data);

            dc.sanitize();
            

            return dc;
        }

        public MGCImport(String filename)
        {
            br = new BinaryReader(new FileStream(filename, FileMode.Open));
            cards = new List<DexCard>();

            fileMagic = new string(br.ReadChars(3));

            if (fileMagic.CompareTo("MGC") != 0)
                MessageBox.Show("Sorry, this version of Cardfile isn't supported yet!");

            else
            {

                cardN = br.ReadInt32();

                br.ReadBytes(4);

                for (int i = 0; i < cardN; i++)
                    cards.Add(readIndex());

                MessageBox.Show(cardN + " cards imported!");
            }

            br.Close();

           

        }

        
    }
}
