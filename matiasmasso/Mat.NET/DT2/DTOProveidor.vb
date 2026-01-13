Public Class DTOProveidor
    Inherits DTOContact

    Property DefaultCtaCarrec As DTOPgcCta
    Property Cur As DTOCur
    Property CommercialMargin As DTOCommercialMargin
    Property IRPF_Cod As IRPFCods
    Property PaymentTerms As DTOPaymentTerms
    Property IncoTerm As Incoterms
    Property CodStk As DTOProductBrand.CodStks


    Shadows Property IsLoaded As Boolean


    Public Enum IRPFCods
        exento
        standard
        reducido
        custom
    End Enum

    Public Enum Incoterms
        NotSet
        CIF
        DAP
        DDP
        EXW
        FOB
    End Enum

    Public Enum wellknowns
        Roemer
        FourMoms
        Mayborn
        Inglesina
    End Enum

    Public Sub New()
        MyBase.New()
        _PaymentTerms = New DTOPaymentTerms
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _PaymentTerms = New DTOPaymentTerms
    End Sub


    Shared Function FromContact(oContact As DTOContact) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        If oContact Is Nothing Then
            retval = New DTOProveidor
        ElseIf TypeOf oContact Is DTOProveidor Then
            retval = CType(oContact, DTOProveidor)
        Else
            retval = New DTOProveidor(oContact.Guid)
            With retval
                .Emp = oContact.Emp
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .FullNom = oContact.FullNom
                .Nif = oContact.Nif
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
                .Cur = DTOCur.Eur
            End With
        End If
        Return retval
    End Function


    Shared Function wellknown(id As DTOProveidor.wellknowns) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOProveidor.wellknowns.Roemer
                sGuid = "47C3A677-89C3-4B5E-86A4-25434CE415D5"
            Case DTOProveidor.wellknowns.Inglesina
                sGuid = "A80228D2-0308-42E7-8405-48F4713E4413"
            Case DTOProveidor.wellknowns.FourMoms
                sGuid = "CBA06D6C-8DB5-45FF-9927-FC284441366C"
            Case DTOProveidor.wellknowns.Mayborn
                sGuid = "DB65A3B7-B25A-4728-81F6-D69A3BDBEAAD"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOProveidor(oGuid)
        End If
        Return retval
    End Function

    Shared Function IRPF(oProveidor As DTOProveidor, DtFch As Date) As Decimal
        Select Case oProveidor.IRPF_Cod
            Case DTOProveidor.IRPFCods.reducido
                Dim retval As Decimal = DTOTax.Closest(DTOTax.Codis.Irpf_Professionals_Reduit, DtFch).Tipus
                Return retval
            Case DTOProveidor.IRPFCods.standard
                Dim retval As Decimal = DTOTax.Closest(DTOTax.Codis.Irpf_Professionals_Standard, DtFch).Tipus
                Return retval
            Case Else
                Return 0
        End Select
    End Function

    Shared Function GetCommercialMargin(Optional oProveidor As DTOProveidor = Nothing) As DTOCommercialMargin
        Dim retval As New DTOCommercialMargin
        With retval
            .CostToRetail = 155
        End With
        Return retval
    End Function

End Class
