Public Class DTOTaskLog
    Inherits DTOBaseGuid

    Property Fch As DateTimeOffset
    Property ResultCod As DTOTask.ResultCods
    Property ResultMsg As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory() As DTOTaskLog
        Dim retval As New DTOTaskLog
        With retval
            .ResultCod = DTOTask.ResultCods.Running
            .Fch = DateTimeOffset.Now
        End With
        Return retval
    End Function
End Class
