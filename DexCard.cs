using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

    public class DexCard
    {
        public String rtfData;
        public String name;
        public DateTime made;

        private RichTextBox rtb = new RichTextBox();

        public void StoreRTF(String text)
        {
            rtb.Text = text;
            rtfData = rtb.Rtf;
        }

        public void sanitize()
        {

            if (rtfData.IndexOf('\0') > 1)
                rtfData = rtfData.Substring(0, rtfData.IndexOf('\0'));
            if (name.IndexOf('\0') > 1)
                name = name.Substring(0, name.IndexOf('\0'));
         
        }

        public DexCard()
        {

        }

        public DexCard(String name)
        {
            this.name = name;
            made = DateTime.UtcNow;

            
            rtfData = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fcharset0 Arial;}}{\\*\\generator Msftedit 5.41.15.1515;}\\viewkind4\\uc1\\pard\\f0\\fs20\\par}";
        }

        public override String ToString()
        {
            return name;
        }


    }
