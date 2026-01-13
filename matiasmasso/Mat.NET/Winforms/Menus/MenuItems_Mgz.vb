

Public Class MenuItems_Mgz
    Inherits System.Windows.Forms.MenuItem.MenuItemCollection

    Private mMgz As Mgz

    Public Sub New(ByVal oMgz As Mgz)
        MyBase.new(New MenuItem)
        mMgz = oMgz

        Add("Transmisions", New System.EventHandler(AddressOf Transmisions))
    End Sub


    Private Sub Transmisions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Transmisions
        oFrm.Show()
    End Sub

 


End Class
