Public Class DTOContractCodi
    Inherits DTOBaseGuid

    Public Enum wellknowns
        notSet
        reps
    End Enum

    Property nom As String
    Property amortitzable As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function wellknown(id As DTOContractCodi.wellknowns) As DTOContractCodi
        Dim retval As DTOContractCodi = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOContractCodi.wellknowns.Reps
                sGuid = "53FA6E40-89BA-4485-8715-F6EB2FCB990E"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOContractCodi(oGuid)
        End If
        Return retval
    End Function
End Class
