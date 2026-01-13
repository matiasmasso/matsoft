Public Class DTODefault
    Property Emp As DTOEmp
    Property Cod As Codis
    Property Value As String

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Enum Codis
        NotSet
        Mgz
        Org
        Min347
        LastBlockedCcaYea
        CreditProtocol_MaxImpagatsIndex
        MinAlbDate
        BancNominaTransfers
        SEURECB
        EmailTransmisioVivace
        AECOCroot
        ECI_Weekday
        'GrandesCuentas = 12
        PaidPortsCanarias = 13
        PaidPortsPeninsula
        PaidPortsAndorra
        PaidPortsMarroc
        ECI_Agrupar
        ECI_Depto
        GranEmpresaDesde
        HiResUrl
        HiResPath
        DespesesImpago
        BancToReceiveTransfers
        LastBalanceQuadrat
        BancTpv
        SermepaTpvProductionEnvironment
        SermepaTpvTestingEnvironment
        DespesesImpagoMinim
        MatschedLogLevel
        eDiversaPath
        NotifyVtosDays
        Taller
        MatschedTaskInterval
        DtoTarifa 'Descompte basic sobre Pvp totes les marques i canals per calcular la tarifa professional 
        SpvTrp
        UrlIntrastat
    End Enum

    Public Enum MailingTemplates
        NotSet
        Password
        Blogpost
        Noticia
        ActivationRequest
        CommentPendingModeration
        CommentAnswered
        EmailAddressVerification
        Incidencia
        RaffleParticipation
        DeliveryConfirmation
        RaffleWinnerCongrats
        MailVenciment
        RaffleDistributorNotification
        QuizConfirmationPromoDualFix 'deprecated
        QuizConfirmationConsumerFair 'deprecated
        QuizConfirmationPromoFisher1 'deprecated
        QuizConfirmationPromoFisher 'deprecated
        RepPurchaseOrder
        QuizConfirmationAdvansafix
        CustomerPurchaseOrder
        StoreLocator
    End Enum
End Class
