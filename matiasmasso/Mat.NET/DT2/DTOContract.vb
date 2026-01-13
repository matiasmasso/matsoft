Public Class DTOContract
    Inherits DTOBaseGuid

    Property codi As DTOContractCodi
    Property nom As String
    Property contact As DTOContact = Nothing
    Property num As String
    Property fchFrom As Date
    Property fchTo As Date
    Property privat As Boolean = False
    Property docFile As DTODocFile


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FullText(oContract As DTOContract) As String
        Dim sb As New System.Text.StringBuilder
        With oContract
            If .Num > "" Then sb.Append(.Num & " ")
            sb.Append(.fchFrom.ToShortDateString & " ")
            sb.Append(.Contact.NomComercialOrDefault())
        End With
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function ContactNom() As String
        Dim retval As String = ""
        If _Contact IsNot Nothing Then
            retval = _Contact.FullNom
        End If
        Return retval
    End Function
End Class
