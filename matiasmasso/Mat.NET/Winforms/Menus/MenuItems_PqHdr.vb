

Public Class MenuItems_PqHdr
    Inherits System.Windows.Forms.MenuItem.MenuItemCollection

    Private mPqHdr As PqHdr

    Public Sub New(ByVal oPqHdr As PqHdr)
        MyBase.new(New MenuItem)
        mPqHdr = oPqHdr

        Add("Zoom", New System.EventHandler(AddressOf Zoom))

    End Sub


    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowPqHdr(mPqHdr)
    End Sub

End Class
