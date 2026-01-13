namespace DTO
{
    public class DTODefault
    {
        public DTOEmp emp { get; set; }
        public Codis cod { get; set; }
        public string value { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public enum Codis
        {
            notSet,
            mgz,
            org,
            min347,
            lastBlockedCcaYea,
            creditProtocol_MaxImpagatsIndex,
            minAlbDate,
            bancNominaTransfers,
            SEURECB,
            emailTransmisioVivace,
            AECOCroot,
            ECI_Weekday,
            // GrandesCuentas = 12
            paidPortsCanarias = 13,
            paidPortsPeninsula,
            paidPortsAndorra,
            paidPortsMarroc,
            ECI_Agrupar,
            ECI_Depto,
            granEmpresaDesde,
            hiResUrl,
            hiResPath,
            despesesImpago,
            bancToReceiveTransfers,
            lastBalanceQuadrat,
            bancTpv,
            sermepaTpvProductionEnvironment,
            sermepaTpvTestingEnvironment,
            despesesImpagoMinim,
            matschedLogLevel,
            eDiversaPath,
            notifyVtosDays,
            taller,
            matschedTaskInterval,
            dtoTarifa // Descompte basic sobre Pvp totes les marques i canals per calcular la tarifa professional
            ,
            spvTrp,
            urlIntrastat
        }

        public enum MailingTemplates
        {
            notSet,
            password,
            blogpost,
            noticia,
            activationRequest,
            commentPendingModeration,
            commentAnswered,
            emailAddressVerification,
            incidencia,
            raffleParticipation,
            deliveryConfirmation,
            raffleWinnerCongrats,
            mailVenciment,
            raffleDistributorNotification,
            quizConfirmationPromoDualFix // deprecated
    ,
            quizConfirmationConsumerFair // deprecated
    ,
            quizConfirmationPromoFisher1 // deprecated
    ,
            quizConfirmationPromoFisher // deprecated
    ,
            repPurchaseOrder,
            quizConfirmationAdvansafix, // deprecated
            customerPurchaseOrder,
            storeLocator,
            bankTransferReminder
        }
    }
}
