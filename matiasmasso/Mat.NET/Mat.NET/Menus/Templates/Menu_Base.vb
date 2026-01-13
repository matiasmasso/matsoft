Public Class Menu_Base
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Protected Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
