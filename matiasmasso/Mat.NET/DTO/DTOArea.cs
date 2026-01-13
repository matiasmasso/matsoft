using Newtonsoft.Json.Linq;
using System;



namespace DTO
{
    public class DTOArea : DTOGuidNom
    {
        public Cods Cod { get; set; }

        public enum Cods
        {
            NotSet,
            Country,
            Zona,
            Location,
            Zip,
            Adr,
            Contact,
            Comarca
        }

        public enum SelectModes
        {
            Browse,
            SelectAny,
            SelectCountry,
            SelectZona,
            SelectLocation,
            SelectZip
        }

        public DTOArea() : base()
        {
        }

        public DTOArea(Guid oGuid, string sNom = "") : base(oGuid, sNom)
        {
        }

        public static DTOArea fromJObject(JObject jArea)
        {
            DTOArea retval = null;
            DTOArea oArea = jArea.ToObject<DTOArea>();
            switch (oArea.Cod)
            {
                case DTOArea.Cods.Contact:
                    retval = jArea.ToObject<DTOContact>();
                    break;
                case DTOArea.Cods.Adr:
                    //retval = jArea.ToObject<DTOAddress>();
                    break;
                case DTOArea.Cods.Zip:
                    retval = jArea.ToObject<DTOZip>();
                    break;
                case DTOArea.Cods.Location:
                    retval = jArea.ToObject<DTOLocation>();
                    break;
                case DTOArea.Cods.Comarca:
                    retval = jArea.ToObject<DTOComarca>();
                    break;
                case DTOArea.Cods.Zona:
                    retval = jArea.ToObject<DTOZona>();
                    break;
                case DTOArea.Cods.Country:
                    retval = jArea.ToObject<DTOCountry>();
                    DTOCountry oCountry = (DTOCountry)retval;
                    oCountry.LangNom.Esp = oCountry.Nom;
                    break;
                default:
                    break;
            }
            return retval;
        }

        public static DTOArea fromObject(object oObject)
        {
            DTOArea retval = null;
            if (oObject != null)
            {
                if (oObject.GetType().IsSubclassOf(typeof(DTOArea)))
                    retval = (DTOArea)oObject;
                else
                {
                    DTOArea oArea = (DTOArea)oObject;
                    switch (oArea.Cod)
                    {
                        case DTOArea.Cods.Contact:
                            {
                                retval = (DTOContact)oObject;
                                break;
                            }

                        case DTOArea.Cods.Adr:
                            {
                                //retval = (DTOAddress)oObject;
                                break;
                            }

                        case DTOArea.Cods.Zip:
                            {
                                retval = (DTOZip)oObject;
                                break;
                            }

                        case DTOArea.Cods.Location:
                            {
                                retval = (DTOLocation)oObject;
                                break;
                            }

                        case DTOArea.Cods.Zona:
                            {
                                retval = (DTOZona)oObject;
                                break;
                            }

                        case DTOArea.Cods.Comarca:
                            {
                                retval = (DTOComarca)oObject;
                                break;
                            }

                        case DTOArea.Cods.Country:
                            {
                                retval = (DTOCountry)oObject;
                                break;
                            }

                        default:
                            {
                                retval = oArea;
                                break;
                            }
                    }
                }
            }
            return retval;
        }
        public Cods getCod()
        {
            Cods retval = Cods.NotSet;
            if (Cod == Cods.NotSet)
            {
                if (this is DTOCountry)
                    retval = Cods.Country;
                else if (this is DTOZona)
                    retval = Cods.Zona;
                else if (this is DTOLocation)
                    retval = Cods.Location;
                else if (this is DTOContact)
                    retval = Cods.Contact;
            }
            else
                retval = Cod;
            return retval;
        }

        public static DTOArea Factory(Guid oGuid, DTOArea.Cods oCod, string sNom = "")
        {
            DTOArea retval = new DTOArea(oGuid);
            {
                var withBlock = retval;
                withBlock.Cod = oCod;
                withBlock.Nom = sNom;
            }
            return retval;
        }

        public static string nomOrDefault(DTOArea oArea)
        {
            string retval = "";
            if (oArea != null)
            {
                if (oArea is DTOCountry)
                    retval = ((DTOCountry)oArea).LangNom.Esp;
                else if (oArea is DTOZona)
                {
                    DTOZona oZona = (DTOZona)oArea;
                    retval = oZona.Nom;
                }
                else if (oArea is DTOLocation)
                    retval = ((DTOLocation)oArea).Nom;
                else if (oArea is DTOContact)
                    retval = ((DTOContact)oArea).FullNom;
            }
            return retval;
        }

        public static string FullNom(DTOArea oArea, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string retval = "";
            if (oArea is DTOCountry)
                retval = ((DTOCountry)oArea).LangNom.Tradueix(oLang);
            else if (oArea is DTOZona)
            {
                DTOZona oZona = (DTOZona)oArea;
                retval = oZona.FullNom(oLang);
            }
            else if (oArea is DTOLocation)
            {
                DTOLocation oLocation = (DTOLocation)oArea;
                retval = oLocation.FullNom(oLang);
            }
            else if (oArea is DTOZip)
            {
                DTOZip oZip = (DTOZip)oArea;
                retval = DTOZip.FullNomSegmented(oZip, oLang);
            }
            else if (oArea is DTOContact)
                retval = ((DTOContact)oArea).FullNom;
            return retval;
        }

        public static string FullNomSegmented(DTOArea oArea, DTOLang oLang)
        {
            string retval = "";
            if (oArea is DTOCountry)
            {
                DTOCountry oCountry = (DTOCountry)oArea;
                retval = DTOCountry.NomTraduit(oCountry, oLang);
            }
            else if (oArea is DTOZona)
            {
                DTOZona oZona = (DTOZona)oArea;
                retval = oZona.FullNomSegmented(oLang);
            }
            else if (oArea is DTOLocation)
            {
                DTOLocation oLocation = (DTOLocation)oArea;
                retval = DTOLocation.FullNomSegmented(oLocation, oLang);
            }
            else if (oArea is DTOZip)
            {
                DTOZip oZip = (DTOZip)oArea;
                retval = DTOZip.FullNomSegmented(oZip, oLang);
            }
            else if (oArea is DTOContact)
                retval = ((DTOContact)oArea).FullNom;
            else
                retval = oArea.Nom;
            return retval;
        }

        public static string FullNomSegmentedReversed(DTOArea oArea, DTOLang oLang)
        {
            string retval = "";
            if (oArea is DTOCountry)
            {
                DTOCountry oCountry = (DTOCountry)oArea;
                retval = DTOCountry.NomTraduit(oCountry, oLang);
            }
            else if (oArea is DTOZona)
            {
                DTOZona oZona = (DTOZona)oArea;
                retval = oZona.FullNomSegmentedReversed(oLang);
            }
            else if (oArea is DTOLocation)
            {
                DTOLocation oLocation = (DTOLocation)oArea;
                retval = DTOLocation.FullNomSegmentedReversed(oLocation, oLang);
            }
            else if (oArea is DTOZip)
            {
                DTOZip oZip = (DTOZip)oArea;
                retval = DTOZip.FullNomSegmentedReversed(oZip, oLang);
            }
            else if (oArea is DTOContact)
                retval = ((DTOContact)oArea).FullNom;
            else if (oArea is DTOArea)
                retval = oArea.Nom;
            return retval;
        }

        public static bool isEsp(DTOArea oArea)
        {
            bool retval = false;
            if (oArea != null)
            {
                DTOCountry oEsp = DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain);

                if (oArea is DTOCountry)
                    retval = ((DTOCountry)oArea).Equals(oEsp);
                else if (oArea is DTOZona)
                {
                    DTOZona oZona = (DTOZona)oArea;
                    retval = isEsp(oZona.Country);
                }
                else if (oArea is DTOLocation)
                {
                    DTOLocation oLocation = (DTOLocation)oArea;
                    retval = isEsp(oLocation.Zona);
                }
                else if (oArea is DTOZip)
                {
                    DTOZip oZip = (DTOZip)oArea;
                    retval = isEsp(oZip.Location);
                }
            }

            return retval;
        }

        public static DTOCountry Country(DTOArea oArea)
        {
            DTOCountry retval = null;
            if (oArea != null)
            {
                if (oArea is DTOCountry)
                    retval = (DTOCountry)oArea;
                else if (oArea is DTOZona)
                {
                    DTOZona oZona = (DTOZona)oArea;
                    retval = oZona.Country;
                }
                else if (oArea is DTOLocation)
                {
                    DTOLocation oLocation = (DTOLocation)oArea;
                    if (oLocation.Zona != null)
                        retval = oLocation.Zona.Country;
                }
                else if (oArea is DTOZip)
                {
                    DTOZip oZip = (DTOZip)oArea;
                    if (oZip.Location != null)
                    {
                        if (oZip.Location.Zona != null)
                            retval = oZip.Location.Zona.Country;
                    }
                }
            }
            return retval;
        }

        public static DTOAreaProvincia provincia(DTOArea oArea)
        {
            DTOAreaProvincia retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oArea != null)
            {
                if (oArea is DTOAreaProvincia)
                    retval = (DTOAreaProvincia)oArea;
                else if (oArea is DTOZona)
                {
                    DTOZona oZona = (DTOZona)oArea;
                    retval = oZona.Provincia;
                }
                else if (oArea is DTOLocation)
                {
                    DTOLocation oLocation = (DTOLocation)oArea;
                    if (oLocation.Zona != null)
                        retval = oLocation.Zona.Provincia;
                }
                else if (oArea is DTOZip)
                {
                    DTOZip oZip = (DTOZip)oArea;
                    if (oZip.Location != null)
                    {
                        if (oZip.Location.Zona != null)
                            retval = oZip.Location.Zona.Provincia;
                    }
                }
            }
            return retval;
        }


        public static DTOZona zona(DTOArea oArea)
        {
            DTOZona retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oArea != null)
            {
                if (oArea is DTOZona)
                    retval = (DTOZona)oArea;
                else if (oArea is DTOLocation)
                {
                    DTOLocation oLocation = (DTOLocation)oArea;
                    retval = oLocation.Zona;
                }
                else if (oArea is DTOZip)
                {
                    DTOZip oZip = (DTOZip)oArea;
                    if (oZip.Location != null)
                        retval = oZip.Location.Zona;
                }
            }
            return retval;
        }

        public static DTOLocation location(DTOArea oArea)
        {
            DTOLocation retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oArea != null)
            {
                if (oArea is DTOLocation)
                    retval = (DTOLocation)oArea;
                else if (oArea is DTOZip)
                {
                    DTOZip oZip = (DTOZip)oArea;
                    retval = oZip.Location;
                }
            }
            return retval;
        }

        public static DTOZip zip(DTOArea oArea)
        {
            DTOZip retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oArea != null)
            {
                if (oArea is DTOZip)
                    retval = (DTOZip)oArea;
            }
            return retval;
        }

        public static DTOArea toArea(DTOArea oArea)
        {
            DTOArea retval = null;
            if (oArea != null)
            {
                if (oArea is DTOCountry)
                {
                    retval = new DTOArea(oArea.Guid, ((DTOCountry)oArea).LangNom.Esp);
                    retval.Cod = Cods.Country;
                }
                else if (oArea is DTOZona)
                {
                    retval = new DTOArea(oArea.Guid, ((DTOZona)oArea).Nom);
                    retval.Cod = Cods.Zona;
                }
                else if (oArea is DTOLocation)
                {
                    retval = new DTOArea(oArea.Guid, ((DTOLocation)oArea).Nom);
                    retval.Cod = Cods.Location;
                }
                else if (oArea is DTOZip)
                {
                    retval = new DTOArea(oArea.Guid, ((DTOZip)oArea).ZipCod);
                    retval.Cod = Cods.Zip;
                }
                else
                    retval = oArea;
            }
            return retval;
        }

        public string urlCustomersSegment()
        {
            return base.urlSegment("area/areacustomers");
        }
    }
}
