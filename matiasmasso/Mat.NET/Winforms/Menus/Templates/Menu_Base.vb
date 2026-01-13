Public Class Menu_Base
    Public Property MenuItems As List(Of ToolStripMenuItem)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        _MenuItems = New List(Of ToolStripMenuItem)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Dim retval As ToolStripMenuItem() = _MenuItems.ToArray
        Return retval
    End Function

    Public Sub AddMenuItem(oMenuItem As ToolStripMenuItem)
        _MenuItems.Add(oMenuItem)
    End Sub

    Protected Function Lang() As DTOLang
        Dim retval As DTOLang = Current.Session.Lang
        Return retval
    End Function

    Protected Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Protected Sub DeleteRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterDelete(sender, e)
    End Sub

    Protected Sub ToggleProgressBarRequest(Visible As Boolean)
        RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(Visible))
    End Sub
End Class
