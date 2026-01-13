Public Class DTOBanc
    Inherits DTOContact

    Property abr As String
    Property classificacio As DTOAmt
    Property disposat As DTOAmt
    Property iban As DTOIban
    Property sepaCoreIdentificador As String
    Property normaRMECedent As String
    Property comisioGestioCobr As Decimal
    Property comisioGestioCobrExentoDeIVA As Decimal
    Property comisioImpagoGestioCobro As Decimal

    Property impagComisio As Decimal
    Property impagMinim As Decimal
    Property impagMail As Decimal
    Property impagMailIva As Decimal
    Property impagMailxRebut As Decimal
    Property impagFchCondicions As Date
    Property modeCcaImpago As ModesCcaImpago

    Property saldo As DTOAmt

    Shadows Property isLoaded As Boolean

    Public Enum wellknowns
        cx
        caixaBank
        santander
        deutscheBank
    End Enum

    Public Enum ModesCcaImpago
        standard
        separaNominalDeDespeses
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function wellknown(id As DTOBanc.wellknowns) As DTOBanc
        Dim retval As DTOBanc = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOBanc.wellknowns.cx
                sGuid = "8014A3C5-7B26-4C82-8719-B0C1D0384777"
            Case DTOBanc.wellknowns.caixaBank
                sGuid = "C52FA12B-BBA1-4BD8-BB97-334DDB2B12D4"
            Case DTOBanc.wellknowns.santander
                sGuid = "A48D18C1-6F99-49CD-8AA5-E3AF3FE5DAE0"
            Case DTOBanc.wellknowns.deutscheBank
                sGuid = "27B23889-DA93-41A8-8476-926C98AE47AA"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOBanc(oGuid)
        End If
        Return retval
    End Function


    Public Function AbrOrNom() As String
        Dim retval As String = _Abr
        If retval = "" Then retval = MyBase.NomComercial
        If retval = "" Then retval = MyBase.Nom
        Return retval
    End Function
End Class
