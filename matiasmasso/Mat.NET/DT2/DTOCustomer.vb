Public Class DTOCustomer
    Inherits DTOContact

    Property discountOnRRPP As Decimal
    Property iVA As Boolean
    Property req As Boolean
    Property portsCod As PortsCodes
    Property portsCondicio As PortsCondicions
    Property cashCod As CashCodes
    Property tarifa As Tarifas
    Property globalDto As Decimal
    Property productDtos As List(Of DTOCliProductDto)
    Property tarifaDtos As List(Of DTOCustomerTarifaDto)
    Property noWeb As Boolean
    Property webatlaspriority As Boolean
    Property noRep As Boolean
    Property noRaffles As Boolean
    Property ordersToCentral As Boolean
    Property noIncentius As Boolean
    Property eShopOnly As Boolean
    Property deliveryPlatform As DTOCustomerPlatform
    Property ccx As DTOCustomer 'Invoice destination
    Property ref As String 'Label to distinguish between outlets of same organization
    Property albValorat As Boolean
    Property warnAlbs As String
    Property paymentTerms As DTOPaymentTerms
    Property fpgIndependent As Boolean

    Property horarioEntregas As String

    Property incoterm As DTOProveidor.Incoterms
    Property suProveedorNum As String

    Property mostrarEANenFactura As Boolean
    Property fraPrintMode As FraPrintModes
    Property channel As DTOContactClass

    Property customerCluster As DTOCustomerCluster
    Property cluster As Clusters
    Property holding As DTOHolding


    Shadows Property isLoaded As Boolean

    Public Enum wellknowns
        NotSet
        ElCorteIngles
        Eciga
        Amazon
        Sonae
        Carrefour
        Prenatal
        PrenatalPortugal
        PrenatalTenerife
        MiFarma
        Bebitus
        JavierBayon
    End Enum

    Public Enum CodsAlbsXFra
        NotSet
        UnaSolaFraMensual
        JuntarAlbs
        JuntarAlbsPetits
        FraPerAlbara
    End Enum

    Public Enum PortsCodes
        NotSet
        Pagats
        Deguts
        Reculliran
        Altres
        EntregatEnMa
    End Enum

    Public Enum PortsCondicions
        SegunZona
        PeninsulaBalears
        Andorra
        Canaries
        ResteDelMon
        eCom
        Portugal
    End Enum

    Public Enum CashCodes
        NotSet
        credit
        Reembols
        TransferenciaPrevia
        Visa
        Diposit
    End Enum

    Public Enum CreditStatus
        Suficient
        Excedit
        Caducat
        Retirat
        Impagats
        NeverLogged
    End Enum

    Public Enum Tarifas
        NotSet
        Standard
        Virtual
        FiftyFifty
        Pvp
    End Enum

    Public Enum FraPrintModes
        NotSet
        NoPrint
        Printer
        Email
        Edi
    End Enum

    Public Enum Clusters
        NotSet
        A
        B
        C
        D
        E
        F
    End Enum

    Public Enum GroupLevels
        [Single] 'Una rao social amb un sol punt de venda
        Chain    'Una rao social multiples punts de venda
        Holding  'multiples raons socials cada una amb els seus punts de venda
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub



    Shared Function FromContact(oContact As DTOContact) As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        If oContact Is Nothing Then
            retval = New DTOCustomer
        ElseIf TypeOf oContact Is DTOCustomer Then
            retval = oContact
        Else
            retval = New DTOCustomer(oContact.Guid)
            With retval
                .Emp = oContact.Emp
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .SearchKey = oContact.SearchKey
                .FullNom = oContact.FullNom
                .Nif = oContact.Nif
                .Nif2 = oContact.Nif2
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
                .NomAnterior = oContact.NomAnterior
                .NomNou = oContact.NomNou
                .Website = oContact.Website
                .DisplayWebsite = oContact.DisplayWebsite
                .Botiga = oContact.Botiga
                .Logo = oContact.Logo
                .GLN = oContact.GLN
                .Telefon = oContact.Telefon
                .Tels = oContact.Tels
                .ContactPersons = oContact.ContactPersons
                .Obsoleto = oContact.Obsoleto
                .Obs = oContact.Obs
            End With
        End If
        Return retval
    End Function

    Shared Function wellknown(id As DTOCustomer.wellknowns) As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case wellknowns.ElCorteIngles
                sGuid = "1850CA50-B514-404E-BD5C-3C33B7A6D3BF"
            Case wellknowns.Eciga
                sGuid = "4A590843-E1E7-4550-9375-B42FCC917A24"
            Case wellknowns.Amazon
                sGuid = "BDAC8F45-D3E7-47D7-8229-889FBA4543E1"
            Case wellknowns.Carrefour
                sGuid = "21DAC56A-F152-48CE-B357-6A8508520622"
            Case wellknowns.Prenatal
                sGuid = "44684614-0437-4FFB-B59E-D0B1392F819F"
            Case wellknowns.PrenatalPortugal
                sGuid = "E59C399A-A9BD-4D17-9729-ACA9FF88A7A4"
            Case wellknowns.PrenatalTenerife
                sGuid = "4779EE3D-5876-4065-B4FD-6D1F09D655AA"
            Case wellknowns.MiFarma
                sGuid = "35D515BA-585D-458A-9126-C713A5B26F58"
            Case wellknowns.Bebitus
                sGuid = "B6613C73-A857-401C-8F86-B6597378EA88"
            Case wellknowns.JavierBayon
                sGuid = "6901C741-9554-46BC-B4BA-8696929D2454"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOCustomer(oGuid)
        End If
        Return retval
    End Function

    Public Function CcxOrMe() As DTOCustomer
        Dim retval As DTOCustomer = If(_Ccx, Me)
        Return retval
    End Function



    Shared Function RefOrNomComercial(oCustomer As DTOCustomer) As String
        Dim retval As String = oCustomer.Ref
        If retval = "" Then retval = oCustomer.NomComercial
        If retval = "" Then retval = oCustomer.Nom
        Return retval
    End Function

    Shared Function NomNifAndAddress(oCustomer As DTOCustomer, exs As List(Of Exception)) As String
        Dim sb As New Text.StringBuilder
        If oCustomer.Nom = "" Then
            exs.Add(New Exception("falta la rao social"))
        Else
            sb.AppendLine(oCustomer.Nom)
        End If
        If oCustomer.Nif = "" Then
            exs.Add(New Exception("falta el Nif"))
        Else
            sb.AppendLine("NIF:" & oCustomer.Nif)
        End If
        If oCustomer.Address Is Nothing Then
            exs.Add(New Exception("falta la adreça fiscal"))
        Else
            sb.AppendLine(oCustomer.Address.Text)
            sb.AppendLine(DTOAddress.ZipyCit(oCustomer.Address))
            Dim oCountry As DTOCountry = DTOAddress.Country(oCustomer.Address)
            If oCountry.UnEquals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
                sb.AppendLine(oCountry.langNom.Esp)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SuggestedIVA(oContact As DTOContact) As Boolean
        Dim oExportCod As DTOInvoice.ExportCods = DTOContact.ExportCod(oContact)
        Dim retval As Boolean = (oExportCod = DTOInvoice.ExportCods.Nacional)
        Return retval
    End Function

    Shared Function SuggestedReq(oContact As DTOContact) As Boolean
        Dim retval As Boolean = False
        If DTOCustomer.SuggestedIVA(oContact) Then
            Dim oFormaJuridica = DTOContact.FormaJuridica(oContact)
            retval = (oFormaJuridica = DTOContact.FormasJuridicas.PersonaFisica)
        End If
        Return retval
    End Function

    Shared Function DistributionChannels(oCustomers As IEnumerable(Of DTOContact)) As List(Of DTODistributionChannel)
        Dim oNoChannel As New DTODistributionChannel(New Guid)
        oNoChannel.LangText.Esp = "(sense canal)"
        Dim oNoClass As New DTOContactClass(New Guid)
        oNoClass.Nom.Esp = "(sense classificar)"
        oNoClass.DistributionChannel = oNoChannel
        For Each oContact In oCustomers.Where(Function(x) x.ContactClass Is Nothing)
            oContact.ContactClass = oNoClass
        Next
        Return oCustomers.GroupBy(Function(x) x.ContactClass.DistributionChannel.Guid).Select(Function(y) y.First).Select(Function(z) z.ContactClass.DistributionChannel).ToList
    End Function

    Shared Function ContactClasses(oCustomers As IEnumerable(Of DTOContact)) As List(Of DTOContactClass)
        Return oCustomers.GroupBy(Function(x) x.ContactClass.Guid).Select(Function(y) y.First).Select(Function(z) z.ContactClass).ToList
    End Function

End Class

