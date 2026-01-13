Public Class Frm_SiiLogs
    Private _X509Cert As Security.Cryptography.X509Certificates.X509Certificate2

    Private Async Sub Frm_SiiLogs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _X509Cert = Await FEB.Cert.X509Certificate2(GlobalVariables.Emp.Org, exs)
        If exs.Count = 0 Then
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub refresca()
        Dim items = AeatSii.FacturasRecibidas.Consulta(DTO.Defaults.Entornos.Produccion, _X509Cert, Current.Session.Emp.Org, CurrentYearMonth)
        Xl_SiiLogs1.Load(items)
    End Sub

    Private Function CurrentYearMonth() As DTOYearMonth
        Dim retval As DTOYearMonth = Xl_YearMonth1.YearMonth
        Return retval
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_SiiLogs1.Filter = e.Argument
    End Sub


    Private Sub Xl_YearMonth1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_YearMonth1.AfterUpdate
        refresca()
    End Sub
End Class