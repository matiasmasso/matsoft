

Public Class MenuItems_Mgz
    Inherits Windows.forms.MenuItem.MenuItemCollection

    Private mMgz As Mgz

    Public Sub New(ByVal oMgz As Mgz)
        MyBase.new(New MenuItem)
        mMgz = oMgz

        Add("Transmisions", New System.EventHandler(AddressOf Transmisions))
    End Sub


    Private Sub Transmisions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowTransmisions(Today.Year)
    End Sub

 


End Class
