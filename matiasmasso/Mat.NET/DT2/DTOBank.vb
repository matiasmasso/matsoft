Public Class DTOBank
    Inherits DTOBaseGuid

    Property country As DTOCountry
    Property id As String
    Property raoSocial As String
    Property nomComercial As String
    Property swift As String
    Property tel As String
    Property web As String
    <JsonIgnore> Property logo As Image
    Property SEPAB2B As Boolean
    Property obsoleto As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(Optional oCountry As DTOCountry = Nothing, Optional Id As String = "") As DTOBank
        Dim retval As New DTOBank
        With retval
            .Country = oCountry
            .Id = Id
        End With
        Return retval
    End Function

    Shared Function NomComercialORaoSocial(oBank As DTOBank) As String
        Dim retval As String = ""
        If oBank IsNot Nothing Then
            If oBank.NomComercial = "" Then
                retval = oBank.RaoSocial
            Else
                retval = oBank.NomComercial
            End If
        End If
        Return retval
    End Function

    Public Function IsSepa() As Boolean
        Dim retval As Boolean
        If _Country IsNot Nothing Then
            Select Case _Country.ExportCod
                Case DTOInvoice.ExportCods.Nacional, DTOInvoice.ExportCods.Intracomunitari
                    retval = True
            End Select
        End If
        Return retval
    End Function

    Shared Function ValidateBIC(src As String) As Boolean
        Dim pattern As String = "[A-Z]{6,6}[A-Z2-9][A-NP-Z0-9]([A-Z0-9]{3,3}){0,1}"
        Dim retval As Boolean = TextHelper.RegexMatch(src, pattern)
        Return retval
    End Function
End Class