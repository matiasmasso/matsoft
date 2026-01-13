

Public Class MenuItems_Epg
    Inherits Windows.forms.MenuItem.MenuItemCollection

    Private mEpg As Epg

    Public Sub New(ByVal oEpg As Epg)
        MyBase.new(New MenuItem)
        mEpg = oEpg

        Add("Zoom", New System.EventHandler(AddressOf Zoom))

    End Sub


    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowEpg(mEpg)
    End Sub

End Class
