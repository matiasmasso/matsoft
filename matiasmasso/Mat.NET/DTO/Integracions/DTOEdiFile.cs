namespace DTO
{
    public class DTOEdiFile
    {
        public enum IOCods
        {
            Inbox,
            Outbox
        }

        public static DTOEdiversaFile.Interlocutors Interlocutor(string gln)
        {
            DTOEdiversaFile.Interlocutors retval = DTOEdiversaFile.Interlocutors.unknown;
            if (!string.IsNullOrEmpty(gln) && gln.Length == 13)
            {
                if (gln.StartsWith(DTOEdiversaFile.PrefixBritax))
                    retval = DTOEdiversaFile.Interlocutors.britax;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixElCorteIngles))
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixElCorteInglesPt))
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixAmazon))
                    retval = DTOEdiversaFile.Interlocutors.amazon;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixCarrefour))
                    retval = DTOEdiversaFile.Interlocutors.carrefour;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixSonae))
                    retval = DTOEdiversaFile.Interlocutors.sonae;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixAlcampo))
                    retval = DTOEdiversaFile.Interlocutors.alcampo;
                else if (gln.StartsWith(DTOEdiversaFile.PrefixMiFarma))
                    retval = DTOEdiversaFile.Interlocutors.mifarma;
            }
            return retval;
        }
    }
}
