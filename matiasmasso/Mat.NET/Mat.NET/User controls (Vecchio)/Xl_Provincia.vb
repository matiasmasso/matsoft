

Public Class Xl_Provincia
    Private mProvincia As provincia
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Provincia() As Provincia
        Get
            Return mProvincia
        End Get
        Set(ByVal value As Provincia)
            mProvincia = value
            If mProvincia Is Nothing Then
                TextBox1.Text = ""
            Else
                TextBox1.Text = mProvincia.Nom
            End If
        End Set
    End Property

    Private Sub ToolStripMenuItemZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemZoom.Click
        Dim oFrm As New Frm_Provincia(mProvincia)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mProvincia = DirectCast(sender, Provincia)
        TextBox1.Text = mProvincia.Nom
    End Sub

    Private Sub ButtonSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
        'Dim oFrm As New Frm_Provincias(mProvincia, Frm_Provincias.Modes.Selection)
        'AddHandler oFrm.afterupdate, AddressOf UpdateRequest
        'oFrm.show()
    End Sub

    Private Sub UpdateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mProvincia = DirectCast(sender, Provincia)
        TextBox1.Text = mProvincia.Nom
        RaiseEvent AfterUpdate(mProvincia, EventArgs.Empty)
    End Sub
End Class
