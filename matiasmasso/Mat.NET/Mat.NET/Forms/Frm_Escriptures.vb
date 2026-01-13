

Public Class Frm_Escriptures

    Private Sub Frm_Escriptures_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Escriptures1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Escriptures1.RequestToAddNew
        Dim oEscriptura As DTOEscriptura = BLL.BLLEscriptura.newEscriptura
        Dim oFrm As New Frm_Escriptura(oEscriptura)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Escriptures1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Escriptures1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oEscriptures As List(Of DTOEscriptura) = BLL.BLLEscriptures.All
        Xl_Escriptures1.Load(oEscriptures)
    End Sub
End Class