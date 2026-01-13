Public Class DTORep
    Inherits DTOContact

    Property nickName As String
    Property fchAlta As Date
    Property fchBaja As Date
    Property disableLiqs As Boolean
    Property comisionStandard As Decimal
    Property comisionReducida As Decimal
    Property ivaCod As IVACods
    Property irpfCod As DTOProveidor.IRPFCods
    Property irpfCustom As Decimal
    Property raoSocialFacturacio As DTOProveidor
    <JsonIgnore> Property img48 As Image
    <JsonIgnore> Property foto As Image
    Property iban As DTOIban

    Property repProducts As List(Of DTORepProduct)



    Public Enum wellknowns
        notSet
        josep
        enric
        rosillo
    End Enum

    Public Enum IVACods
        exento
        standard
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function wellknown(id As DTORep.wellknowns) As DTORep
        Dim retval As DTORep = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTORep.wellknowns.Josep
                sGuid = "4A941105-6FDC-44C2-B2A4-267F050C41A1"
            Case DTORep.wellknowns.rosillo
                sGuid = "59A734EE-67D9-4B0D-86E6-94154CAAF733"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTORep(oGuid)
        End If
        Return retval
    End Function

    Public Function NicknameOrNom() As String
        Dim retval As String = ""
        If _NickName > "" Then
            retval = _NickName
        ElseIf MyBase.FullNom > "" Then
            retval = MyBase.FullNom
        ElseIf MyBase.Nom > "" Then
            retval = MyBase.Nom
        Else
            retval = MyBase.Guid.ToString
        End If
        Return retval
    End Function

    Shared Function IVAtipus(oRep As DTORep, Optional DtFch As Date = Nothing) As Decimal
        Dim retval As Decimal
        Select Case oRep.IvaCod
            Case DTORep.IVACods.exento
            Case DTORep.IVACods.standard
                retval = DTOTax.ClosestTipus(DTOTax.Codis.Iva_Standard, DtFch)
        End Select
        Return retval
    End Function

    Shared Function FromContact(oContact As DTOContact) As DTORep
        Dim retval As DTORep = Nothing
        If oContact Is Nothing Then
            retval = New DTORep
        Else
            retval = New DTORep(oContact.Guid)
            With retval
                .Emp = oContact.Emp
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .Nif = oContact.Nif
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
            End With
        End If
        Return retval
    End Function

    Shared Function Irpf(oRep As DTORep, Optional DtFch As Date = Nothing) As Decimal
        Dim retval As Decimal
        Dim oIrpfCod As DTOProveidor.IRPFCods = oRep.IrpfCod
        If oRep.RaoSocialFacturacio IsNot Nothing Then
            oIrpfCod = oRep.RaoSocialFacturacio.IRPF_Cod
        End If
        Select Case oIrpfCod
            Case DTOProveidor.IRPFCods.exento
            Case DTOProveidor.IRPFCods.reducido
                retval = DTOTax.ClosestTipus(DTOTax.Codis.Irpf_Professionals_Reduit, DtFch)
            Case DTOProveidor.IRPFCods.standard
                retval = DTOTax.ClosestTipus(DTOTax.Codis.Irpf_Professionals_Standard, DtFch)
            Case DTOProveidor.IRPFCods.custom
                retval = oRep.IrpfCustom
        End Select
        Return retval
    End Function

    Shared Function RaoSocialFacturacioOrSelf(oRep As DTORep) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        If oRep.RaoSocialFacturacio Is Nothing Then
            retval = DTOProveidor.FromContact(oRep)
        Else
            retval = oRep.RaoSocialFacturacio
        End If
        Return retval
    End Function
End Class
