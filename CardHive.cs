using System;
using System.Collections.Generic;

using System.Text;
using System.Xml.Serialization;
using System.IO;


  public   class CardHive : List<DexCard>
    {

        public static CardHive DeserializeFromXML(String path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CardHive));
            TextReader textReader = new StreamReader(path);
            CardHive cards;
            cards = (CardHive)deserializer.Deserialize(textReader);
            textReader.Close();

            return cards;
        }

        public void SerializeToXML(String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CardHive));
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }
    }

