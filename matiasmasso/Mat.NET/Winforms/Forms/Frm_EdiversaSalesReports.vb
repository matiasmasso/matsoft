Public Class Frm_EdiversaSalesReports
    Private _EdiversaSalesReports As List(Of DTOEdiversaSalesReport)
    Private _allowEvents As Boolean

    Private Async Sub Frm_EdiversaSalesReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim years = Await FEB2.EdiversaSalesReports.Years(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Xl_Years1.Load(years)
            Await refresca()
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _EdiversaSalesReports = Await FEB2.EdiversaSalesReports.all(exs, GlobalVariables.Emp, Xl_Years1.Value)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Dim oCustomers = _EdiversaSalesReports.
            GroupBy(Function(x) x.Customer.Guid).
            Select(Function(y) y.First.Customer).
            OrderBy(Function(z) z.nom).
            ToList

            loadCustomers(oCustomers)

            Dim oHeaders = _EdiversaSalesReports.Where(Function(x) x.Customer.Equals(ComboBoxCustomer.SelectedItem)).ToList
            Xl_EdiversaSalesReportsHeaders1.Load(oHeaders)


            Dim value = Xl_EdiversaSalesReportsHeaders1.Value
            Xl_EdiversaSalesReportItems1.Load(value)
            _allowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Private Sub loadCustomers(oCustomers As List(Of DTOCustomer))
        With ComboBoxCustomer
            .DisplayMember = "nom"
            .DataSource = oCustomers
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub Xl_EdiversaSalesReportsHeaders1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_EdiversaSalesReportsHeaders1.ValueChanged
        If _allowEvents Then
            Dim value = Xl_EdiversaSalesReportsHeaders1.Value
            Xl_EdiversaSalesReportItems1.Load(value)
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _allowEvents Then
            Await refresca()
        End If
    End Sub

    Private Sub ComboBoxCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCustomer.SelectedIndexChanged
        If _allowEvents Then
            _allowEvents = False
            Dim oHeaders = _EdiversaSalesReports.Where(Function(x) x.Customer.Equals(ComboBoxCustomer.SelectedItem)).ToList
            Xl_EdiversaSalesReportsHeaders1.Load(oHeaders)

            Dim value = Xl_EdiversaSalesReportsHeaders1.Value
            Xl_EdiversaSalesReportItems1.Load(value)
            _allowEvents = True
        End If
    End Sub
End Class