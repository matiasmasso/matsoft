Public Class Frm_Awards

    Private Sub Frm_Awards_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oAwards As List(Of Award) = AwardsLoader.All(True)
        Xl_Awards1.Load(oAwards, Xl_Awards.Modes.NotSet)
    End Sub

    Private Sub Xl_Awards1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Awards1.RequestToAddNew
        Dim oAward As New Award(New Contact(BLL.BLLApp.Emp))
        Dim oFrm As New Frm_Award(oAward)
        AddHandler oFrm.AfterUpdate, AddressOf onUpdated
        oFrm.Show()
    End Sub

    Private Sub OnUpdated(sender As Object, e As MatEventArgs)
        Dim oAwards As List(Of Award) = AwardsLoader.All(True)
        Xl_Awards1.Load(oAwards, Xl_Awards.Modes.NotSet)
    End Sub
End Class