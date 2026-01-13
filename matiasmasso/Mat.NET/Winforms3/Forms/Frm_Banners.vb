Public Class Frm_Banners
    Private showObsolets As Boolean = False

    Private Async Sub Frm_Banners_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Refresca()
    End Sub

    Private Sub Xl_Banners1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Banners1.RequestToAddNew
        Dim oBanner As New DTOBanner
        oBanner.FchFrom = DTO.GlobalVariables.Today()
        Dim oFrm As New Frm_Banner(oBanner)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Async Sub Refresca(sender As Object, e As MatEventArgs)
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oBanners = Await FEB.Banners.AllWithThumbnails(exs, ObsoletsToolStripMenuItem.Checked)
        If exs.Count = 0 Then
            Xl_Banners1.Load(oBanners)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Banners1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Banners1.AfterUpdate
        Await Refresca()
    End Sub

    Private Async Sub ObsoletsToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles ObsoletsToolStripMenuItem.CheckedChanged
        Await Refresca()
    End Sub
End Class