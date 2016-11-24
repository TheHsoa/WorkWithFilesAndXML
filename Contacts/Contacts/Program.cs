using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Contacts
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("flowCards_Card.xml"))
            {
                var doc = XDocument.Parse(reader.ReadToEnd());
                var cardElement = doc.Element("Card");
                if (cardElement == null) throw new FormatException("Can't find Card in XML");
                var contacts = cardElement.Element("Contacts")?.Elements().ToArray();
                if(contacts == null) throw new FormatException("Can't find Card/Contacts in XML");
                using (var promotionalWriter = new StreamWriter("promotionalContacts.txt"))
                {
                    using (var notPromotionalWriter = new StreamWriter("notPromotionalContacts.txt"))
                    {
                        foreach (var contact in contacts)
                        {
                            var isPromotionalAttribute = contact.Attribute("IsPromotional");

                            if (isPromotionalAttribute == null)
                                throw new FormatException($"Can't find attribute \"IsPromotional\" in Card/Contacts/{contact.Name} in XML");

                            var contactValue = contact.Attribute("Value")?.Value;

                            if (contactValue == null) throw new FormatException($"Can't find attribute \"Value\" in Card/Contacts/{contact.Name} in XML");

                            var descriptionValue = contact.Attribute("Description")?.Value ?? "";

                            if (isPromotionalAttribute.Value == "true") promotionalWriter.WriteLine($"{contactValue}[{descriptionValue}]");

                            if (isPromotionalAttribute.Value == "false") notPromotionalWriter.WriteLine($"{contactValue}[{descriptionValue}]");
                        }
                    }
                }
            }
        }
    }
}
