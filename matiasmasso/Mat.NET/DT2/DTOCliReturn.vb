Public Class DTOCliReturn
    Inherits DTOBaseGuid

    Property Customer As DTOCustomer
    Property Mgz As DTOMgz
    Property Fch As Date
    Property RefMgz As String
    Property Bultos As Integer
    Property Entrada As DTODelivery
    Property Auth As String
    Property Obs As String
    Property UsrLog As DTOUsrLog

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oUser As DTOUser) As DTOCliReturn
        Dim retval As New DTOCliReturn
        With retval
            .Mgz = oUser.Emp.Mgz
            .Auth = TextHelper.RandomString(6).ToUpper
            .Bultos = 1
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function
End Class
