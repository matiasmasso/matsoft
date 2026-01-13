Public Class DTODeliveryTraspas
    Inherits DTOBaseGuid
    Property Emp As DTOEmp
    Property Id As Integer
    Property Fch As Date
    Property MgzFrom As DTOMgz
    Property MgzTo As DTOMgz
    Property Items As List(Of DTODeliveryItem)
    Property UsrLog As DTOUsrLog

    Public Sub New()
        MyBase.New
        _Items = New List(Of DTODeliveryItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTODeliveryItem)
    End Sub

    Shared Function Factory(oUser As DTOUser, oMgzFrom As DTOMgz, oMgzTo As DTOMgz, Optional DtFch As Date = Nothing) As DTODeliveryTraspas
        If DtFch = Nothing Then DtFch = DateTime.Today
        Dim retval As New DTODeliveryTraspas
        With retval
            .Emp = oUser.Emp
            .MgzFrom = oMgzFrom
            .MgzTo = oMgzTo
            .Fch = DtFch
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function
End Class
