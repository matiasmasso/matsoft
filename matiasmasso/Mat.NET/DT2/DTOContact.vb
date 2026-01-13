Public Class DTOContact
    Inherits DTOArea

    Property id As Integer
    Property emp As DTOEmp
    Property nif As String
    Property nif2 As String
    Property address As DTOAddress
    Property nomComercial As String
    Property searchKey As String
    Shadows Property FullNom As String
    Property telefon As String
    Property addresses As List(Of DTOAddress)
    Property website As String
    Property displayWebsite As Boolean
    Property lang As DTOLang
    Property rol As DTORol
    Property nomAnterior As DTOContact
    Property nomNou As DTOContact
    <JsonIgnore> Property Logo As Image

    Property contactClass As DTOContactClass
    Property tels As List(Of DTOContactTel)
    Property emails As List(Of DTOUser)
    Property contactPersons As List(Of String)

    Property GLN As DTOEan

    Property botiga As Boolean
    Property obs As String
    Property obsoleto As Boolean


    Public Enum Tipus
        [NotSet]
        Proveidor
        Client
        Representant
        Personal
        Banc
    End Enum

    Public Enum FormasJuridicas
        Unknown
        PersonaFisica
        PersonaJuridica
    End Enum

    Public Enum ContactKeys
        Nom = 0
        Poblacio = 3
        Comercial = 4
        SearchKey = 26
    End Enum

    Public Enum SearchBy
        notset
        email
        tel
        adr
        nif
        SubContact
        ccc
    End Enum

    Public Enum Tabs
        General
        Client
        Proveidor
        Rep
        Staff
        Banc
    End Enum

    Public Sub New()
        MyBase.New()
        _Tels = New List(Of DTOContactTel)
        _Emails = New List(Of DTOUser)
        _ContactPersons = New List(Of String)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Tels = New List(Of DTOContactTel)
        _Emails = New List(Of DTOUser)
        _ContactPersons = New List(Of String)
    End Sub

    Shared Shadows Function factory(oEmp As DTOEmp) As DTOContact
        Dim retval As New DTOContact
        With retval
            .emp = oEmp
            .lang = DTOLang.ESP
        End With
        Return retval
    End Function

    Shared Function formaJuridica(oAddress As DTOAddress, sNif As String) As DTOContact.FormasJuridicas
        Dim retval As DTOContact.FormasJuridicas = DTOContact.FormasJuridicas.Unknown
        If DTOAddress.Country(oAddress).Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
            Dim oNif = DTONif.Factory(sNif, DTOAddress.Country(oAddress))
            If oNif.Type = DTONif.NifTypes.Juridica Then
                retval = DTOContact.FormasJuridicas.PersonaJuridica
            ElseIf oNif.Type = DTONif.NifTypes.Fisica Then
                retval = DTOContact.FormasJuridicas.PersonaFisica
            End If
        End If
        Return retval
    End Function

    Shared Function FormaJuridica(oContact As DTOContact) As DTOContact.FormasJuridicas
        Dim retval As DTOContact.FormasJuridicas = DTOContact.FormasJuridicas.Unknown
        Dim oCountry As DTOCountry = DTOAddress.Country(oContact.Address)
        If oCountry IsNot Nothing Then
            If oContact.Nif > "" Then
                retval = FormaJuridica(oContact.Address, oContact.Nif)
            End If
        End If

        If retval = DTOContact.FormasJuridicas.Unknown Then
            If oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
                If oContact.Nom.EndsWith("S.A.") Or oContact.Nom.EndsWith("S.L.") Then
                    retval = DTOContact.FormasJuridicas.PersonaJuridica
                Else
                    retval = DTOContact.FormasJuridicas.PersonaFisica
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function NomAndNomComercial(oCustomer As DTOCustomer) As String
        Dim retval As String = oCustomer.NomAndNomComercial()
        If oCustomer.Ref > "" Then
            retval = retval & " [" & oCustomer.Ref & "]"
        End If
        Return retval
    End Function

    Public Function NomAndNomComercial() As String
        Dim retval As String = ""
        If _NomComercial = "" Then
            retval = MyBase.Nom
        Else
            If MyBase.Nom > "" Then
                retval = String.Format("{0} {1}{2}{1}", MyBase.Nom, Chr(34), _NomComercial)
            Else
                retval = _NomComercial
            End If
        End If
        Return retval
    End Function

    Public Function NomComercialOrDefault() As String
        Dim retval As String = _NomComercial
        If retval = "" Then retval = MyBase.Nom
        Return retval
    End Function

    Shared Function RaoSocialONomComercial(oContact As DTOContact) As String
        Dim retval As String = ""
        If oContact IsNot Nothing Then
            retval = oContact.Nom
            If retval = "" Then retval = oContact.NomComercial
        End If
        Return retval
    End Function

    Shared Function NomComercialOrRaoSocialAndAddress(oContact As DTOContact) As String
        Dim sb As New System.Text.StringBuilder
        If oContact.NomComercial = "" Then
            sb.Append(oContact.Nom)
        Else
            sb.Append(oContact.NomComercial)
        End If
        If oContact.Address IsNot Nothing Then
            If oContact.Address.Zip IsNot Nothing Then
                If oContact.Address.Zip.Location IsNot Nothing Then
                    sb.Append("-" & oContact.Address.Zip.Location.Nom)
                End If
            End If
            sb.Append("-" & oContact.Address.Text)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function GenerateFullNom(oContact As DTOContact) As String
        Dim sb As New Text.StringBuilder
        sb.Append(oContact.NomAndNomComercial)
        sb.AppendFormat(" ({0})", DTOAddress.ClxLocation(oContact.Address))

        If TypeOf oContact Is DTOCustomer Then
            Dim oCustomer As DTOCustomer = oContact
            Dim sRef As String = oCustomer.Ref
            If sRef > "" Then
                If Not sb.ToString.Contains(sRef) Then
                    sb.Append(" [" & sRef & "]")
                End If
            End If
        End If

        Dim retval = Left(sb.ToString, 100)
        Return retval
    End Function

    Public Function nomAndAddressLines() As List(Of String)
        Dim retval As New List(Of String)
        With retval
            .Add(IIf(MyBase.nom = "", _nomComercial, MyBase.nom))
            .Add(_address.text)
            .Add(_address.zip.ZipyCit())
        End With
        Return retval
    End Function

    Public Function NomAndAddressHtml() As String
        Dim sb As New System.Text.StringBuilder

        For Each line As String In NomAndAddressLines()
            sb.AppendLine(line)
        Next
        Dim retval As String = TextHelper.ToHtml(sb.ToString)
        Return retval
    End Function

    Shared Function DistributionChannel(oContact As DTOContact) As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        If oContact.ContactClass IsNot Nothing Then
            retval = oContact.ContactClass.DistributionChannel
        End If
        Return retval
    End Function

    Public Shadows Function UrlSegment() As String
        Return MyBase.UrlSegment("contacto")
    End Function

    Public Function url(Optional AbsoluteUrl As Boolean = False) As String
        Return MmoUrl.Factory(AbsoluteUrl, "contacto", MyBase.Guid.ToString)
    End Function

    Shared Function ClaveRegimenEspecialOTrascendencia(oContact As DTOContact) As String
        Dim retval As String = ""
        If oContact.Address IsNot Nothing Then
            Select Case oContact.Address.ExportCod
                Case DTOInvoice.ExportCods.Nacional
                    retval = "01"
                Case DTOInvoice.ExportCods.Intracomunitari
                    retval = "09"
                Case DTOInvoice.ExportCods.Extracomunitari
                    retval = "13"
            End Select
        End If
        Return retval
    End Function

    Shared Function ClaveCausaExempcio(oContact As DTOContact) As String
        Dim retval As String = ""
        If oContact.Address IsNot Nothing Then
            Select Case oContact.Address.ExportCod
                Case DTOInvoice.ExportCods.Nacional
                Case DTOInvoice.ExportCods.Intracomunitari
                    retval = "E5"
                Case DTOInvoice.ExportCods.Extracomunitari
                    retval = "E2"
            End Select
        End If
        Return retval
    End Function

    Shared Function ExportCod(oContact As DTOContact) As DTOInvoice.ExportCods
        Dim retval As DTOInvoice.ExportCods = DTOAddress.ExportCod(oContact.Address)
        Select Case retval
            Case DTOInvoice.ExportCods.Intracomunitari
                Dim sNif As String = oContact.Nif
                If sNif > "" AndAlso sNif.Length > 2 Then
                    If sNif.StartsWith("ES") Then
                        retval = DTOInvoice.ExportCods.Nacional 'no residents
                    End If
                End If
        End Select
        Return retval
    End Function

    Shared Function isIVASujeto(oContact As DTOContact) As Boolean
        Dim oExportCod As DTOInvoice.ExportCods = DTOContact.ExportCod(oContact)
        Dim retval As Boolean = (oExportCod = DTOInvoice.ExportCods.Nacional)
        Return retval
    End Function


    Public Function IsElCorteIngles() As Boolean
        Dim oMembers = {DTOCustomer.wellknowns.ElCorteIngles, DTOCustomer.wellknowns.Eciga}
        Dim retval = oMembers.Any(Function(x) DTOCustomer.wellknown(x).Equals(Me))
        Return retval
    End Function

    Public Function IsPrenatal() As Boolean
        Dim oMembers = {DTOCustomer.wellknowns.Prenatal, DTOCustomer.wellknowns.PrenatalTenerife, DTOCustomer.wellknowns.PrenatalPortugal}
        Dim retval = oMembers.Any(Function(x) DTOCustomer.wellknown(x).Equals(Me))
        Return retval
    End Function

    Shared Function DefaultCtaCod(oContact As DTOContact) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcCta.Cods = DTOPgcEpgBase.Cods.NotSet
        Select Case oContact.Rol.Id
            Case DTORol.Ids.Banc
                retval = DTOPgcPlan.Ctas.bancs
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                retval = DTOPgcPlan.Ctas.Clients
            Case DTORol.Ids.Manufacturer
                retval = DTOPgcPlan.Ctas.ProveidorsEur
        End Select
        Return retval
    End Function

    Shared Function Resum(oContact As DTOContact) As String
        Dim sb As New Text.StringBuilder
        With oContact
            sb.AppendLine(oContact.Nom)
            sb.AppendLine(oContact.NomComercial)
            sb.AppendFormat("NIF: {0}", oContact.Nif)
            sb.AppendLine()
            sb.AppendLine(DTOAddress.MultiLine(oContact.Address))
        End With
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function HtmlNameAndAddress(oContact As DTOContact) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oContact.Nom)
        sb.AppendLine(DTOAddress.MultiLine(oContact.Address))
        Dim retval As String = sb.ToString.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Function Countries(oContacts As IEnumerable(Of DTOContact)) As List(Of DTOCountry)
        Return oContacts.GroupBy(Function(x) DTOAddress.Country(x.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Country(z.Address)).ToList
    End Function

    Shared Function Zonas(oContacts As IEnumerable(Of DTOContact)) As List(Of DTOZona)
        Return oContacts.GroupBy(Function(x) DTOAddress.Zona(x.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Zona(z.Address)).ToList
    End Function

    Shared Function Locations(oContacts As IEnumerable(Of DTOContact)) As List(Of DTOLocation)
        Return oContacts.GroupBy(Function(x) DTOAddress.Location(x.Address).Guid).Select(Function(y) y.First).Select(Function(z) DTOAddress.Location(z.Address)).ToList
    End Function

End Class
