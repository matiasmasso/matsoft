

Public Class MenuItems_Mrt
    Inherits Windows.forms.MenuItem.MenuItemCollection

    Private mMrt As Mrt

    Public Sub New(ByVal oMrt As Mrt)
        MyBase.new(New MenuItem)
        mMrt = oMrt

        Add("Zoom", New System.EventHandler(AddressOf Zoom))

    End Sub


    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowMrt(mMrt)
    End Sub

End Class
