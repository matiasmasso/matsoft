

Public Class Frm_Escriptures

    Private Sub Frm_Escriptures_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Escriptures1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Escriptures1.RequestToAddNew
        Dim oEscriptura As DTOEscriptura = DTOEscriptura.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_Escriptura(oEscriptura)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Escriptures1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Escriptures1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oEscriptures = Await FEB2.Escripturas.AllAsync(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Xl_Escriptures1.Load(oEscriptures)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Escriptures1.filter = e.Argument
    End Sub
End Class