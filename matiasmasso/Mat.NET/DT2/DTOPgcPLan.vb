Public Class DTOPgcPlan

    Inherits DTOBaseGuid

    Property nom As String
    Property yearFrom As Integer
    Property yearTo As Integer
    Property lastPlan As DTOPgcPlan


    Public Enum Ctas
        NotSet = 0
        resultatsAnyAnterior = 120
        resultatsAnyCorrent = 130
        creditsBancaris = 170
        InmobilitzatIntangible = 203
        InmobilitzatSoftware = 206
        InmobilitzatTerrenys = 210
        InmobilitzatConstruccions = 211
        InmobilitzatInstalacionsAltres = 215
        InmobilitzatMobles = 216
        InmobilitzatHardware = 217
        InmobilitzatVehicles = 218
        InmobilitzatAltres = 219
        AmrtAcumIntangible = 2803
        AmrtAcumSoftware = 2806
        AmrtAcumConstruccions = 2811
        AmrtAcumInstalacionsAltres = 2815
        AmrtAcumMobles = 2816
        AmrtAcumHardware = 2817
        AmrtAcumVehicles = 2818
        AmrtAcumAltres = 2819
        mercancies = 30000
        mercanciesTransit = 30004
        mercanciesObsoletes = 30005
        ProveidorsEur = 4000
        ProveidorsUsd = 40040
        ProveidorsGbp = 40041
        ServeisEur = 41000
        VisasPagadas = 413
        Clients = 430
        ClientsXecsEnCartera = 4310
        ClientsXecsDescomptats = 4311
        ClientsXecsAlCobro = 4312
        impagats = 4315
        impagatsPagosACompte = 43155
        Clients_Anticips = 438
        TransferenciesDesconegudes = 43809
        DeutorsVaris = 440
        RepsCobrosClients = 44007
        VisasCobradas = 44008
        PagasTreballadors = 465
        RetencionsInteresos = 473
        Irpf = 47510
        IrpfTreballadors = 47511
        IrpfProfessionals = 47512
        IrpfLloguers = 47513
        NominaEmbargos = 47516
        NominaDeutes = 544
        caixa = 570
        bancs = 572
        BancsEfectesDescomptats = 52080
        BancsPagaresDescomptats = 52082
        IvaDeutor = 470
        IvaSoportatNacional = 47200
        IvaSoportatIntracomunitari = 47201
        IvaSoportatImportacio = 47202
        IvaRepercutitNacional = 47710
        IvaRepercutitIntracomunitari = 47711
        IvaRepercutitImportacio = 47712
        IvaReduit = 4772
        IvaSuperReduit = 4773
        IvaRecarrecEquivalencia = 4774
        IvaRecarrecReduit = 4775
        IvaRecarrecSuperReduit = 4776
        LeasingACurt = 52401
        DipositIrrevocableDeCompra = 56101
        compras = 600
        transport_internacional_aranzels = 60051
        transport_internacional_fletes = 60055
        transport_internacional_despeses = 60056
        DevolucionsDeCompres = 608
        Inventari = 6100
        lloguers = 62110
        despesesBancaries = 6260
        despesesCobrament = 6261
        despesesPagament = 6262
        despesesImpago = 6265
        ComisionsRepresentants = 6231
        interesosNegogiacioEfectes = 664
        Nomina = 6400
        Dietas = 6401
        Indemnitzacions = 641
        SegSocialDevengo = 642
        SegSocialCreditor = 476
        DiferenciesDeCanvi = 668
        DotacioAmortitzacioIntangible = 680
        DotacioAmortitzacioMaterial = 681
        InventariDesvaloritzacio = 6931
        Vendes = 700
        DevolucionsDeVendes = 708
        Indemintzacions = 75901
        Redondeos = 75903
        ImpagosRecuperacioDespeses = 75905
        IngresosDiversos = 75909
    End Enum


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class