Public Class DTOBaseGuidNumFch
    Inherits DTOBaseGuid

    Property num As Integer
    Property fch As Date

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Formatted(value As DTOBaseGuidNumFch) As String
        Dim retval As String = Format(value.Fch.Year, "0000") & "." & Format(value.Num, "00000")
        Return retval
    End Function
End Class
