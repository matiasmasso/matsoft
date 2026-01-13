using System;
using System.Collections.Generic;

namespace DTO.Integracions.Vivace
{
    public class DeliveryTracking
    {
        public DateTime date { get; set; }
        public string sender { get; set; }
        public DTOJsonLog Log { get; set; }
        public List<Shipment> shipments { get; set; }
    }

    public class Shipment
    {
        public string delivery { get; set; } //(el nostre numero de albarà)",
        public int packages { get; set; } //(bultos)",
        public int palets { get; set; } //(numero de palets)”,
        public string forwarder { get; set; } //(nif del transportista)",
        public string tracking { get; set; } //(num.de seguiment del transportista)",
        public int cubicKg { get; set; } //(kg cubicats)",
        public Decimal weight { get; set; } //(pes real, amb punt decimal)",
        public decimal volume { get; set; } //(en metres cubics i punt decimal)",
        public decimal cost { get; set; } //(estimació aprox de cost en euros, amb punt decimal)",

        public List<Item> items { get; set; }
    }

    public class Item
    {
        public int package { get; set; } //(numero de bulto)",
        public string SSCC { get; set; }
        public string packaging { get; set; } //(codi embalatje)",
        public int length { get; set; } //(llarg, en mm)",
        public int width { get; set; } //(ample, en mm)",
        public int height { get; set; } //(alt, en mm)",



    }
}
