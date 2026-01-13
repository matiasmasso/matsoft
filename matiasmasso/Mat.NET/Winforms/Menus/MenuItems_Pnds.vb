

Public Class MenuItems_Pnds
    Inherits System.Windows.Forms.MenuItem.MenuItemCollection

    Private mPnds As List(Of Pnd)

    Public Sub New(ByVal oPnds As List(Of Pnd))
        MyBase.new(New MenuItem)
        mPnds = oPnds
        AddItms()
    End Sub

    Public Sub New(ByVal oPnd As Pnd)
        MyBase.new(New MenuItem)
        mPnds = New List(Of Pnd)
        mPnds.Add(oPnd)
        AddItms()
    End Sub

    Private Sub AddItms()
        Dim oZoomItm As MenuItem = Add("Zoom", New System.EventHandler(AddressOf Zoom))
        If mPnds.Count > 1 Then oZoomItm.Enabled = False

        'Add("Imprimir", New System.EventHandler(AddressOf Print))
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Pnd(mPnds(0).ToDTO)
        oFrm.Show()
    End Sub
End Class
