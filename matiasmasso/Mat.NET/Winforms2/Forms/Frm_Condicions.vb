Public Class Frm_Condicions

    Private Sub Frm_Condicions_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Condicios1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Condicions1.RequestToAddNew
        Dim oCondicio As New DTOCondicio
        Dim oFrm As New Frm_Condicio(oCondicio)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Condicios1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Condicions1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oCondicions = Await FEB.Condicions.Headers(exs)
        If exs.Count = 0 Then
            Xl_Condicions1.Load(oCondicions)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class