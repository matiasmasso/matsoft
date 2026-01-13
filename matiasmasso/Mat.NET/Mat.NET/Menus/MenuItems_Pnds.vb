

Public Class MenuItems_Pnds
    Inherits Windows.forms.MenuItem.MenuItemCollection

    Private mPnds As Pnds

    Public Sub New(ByVal oPnds As Pnds)
        MyBase.new(New MenuItem)
        mPnds = oPnds
        AddItms()
    End Sub

    Public Sub New(ByVal oPnd As Pnd)
        MyBase.new(New MenuItem)
        mPnds = New Pnds
        mPnds.Add(oPnd)
        AddItms()
    End Sub

    Private Sub AddItms()
        Dim oZoomItm As MenuItem = Add("Zoom", New System.EventHandler(AddressOf Zoom))
        If mPnds.Count > 1 Then oZoomItm.Enabled = False

        'Add("Imprimir", New System.EventHandler(AddressOf Print))
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowContactPnd(mPnds(0))
    End Sub
End Class
