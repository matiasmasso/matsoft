Public Class Frm_EdiversaSalesReports
    Private _EdiversaSalesReports As List(Of DTOEdiversaSalesReport)
    Private _allowEvents As Boolean

    Private Async Sub Frm_EdiversaSalesReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Await LoadYears(exs) Then
            If Await LoadData(exs) Then
                LoadCustomers()
                LoadHeaders()
                LoadDetails()
                ProgressBar1.Visible = False
                _allowEvents = True
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _allowEvents Then
            Dim exs As New List(Of Exception)
            If Await LoadData(exs) Then
                LoadCustomers()
                LoadHeaders()
                LoadDetails()
                ProgressBar1.Visible = False
                _allowEvents = True
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ComboBoxCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCustomer.SelectedIndexChanged
        If _allowEvents Then
            _allowEvents = False
            LoadHeaders()
            LoadDetails()
            _allowEvents = True
        End If
    End Sub

    Private Sub Xl_EdiversaSalesReportsHeaders1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_EdiversaSalesReportsHeaders1.ValueChanged
        If _allowEvents Then
            LoadDetails()
        End If
    End Sub

    Private Async Function LoadYears(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim years = Await FEB.EdiversaSalesReports.Years(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Xl_Years1.Load(years)
            retval = True
        End If
        Return retval
    End Function

    Private Async Function LoadData(exs As List(Of Exception)) As Task(Of Boolean)
        _EdiversaSalesReports = Await FEB.EdiversaSalesReports.All(exs, GlobalVariables.Emp, Xl_Years1.Value)
        Return exs.Count = 0
    End Function

    Private Sub LoadCustomers()
        Dim oCustomers = _EdiversaSalesReports.
            GroupBy(Function(x) x.Customer.Guid).
            Select(Function(y) y.First.Customer).
            OrderBy(Function(z) z.Nom).
            ToList

        With ComboBoxCustomer
            .DisplayMember = "nom"
            .DataSource = oCustomers
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadHeaders()
        Dim oHeaders = _EdiversaSalesReports.Where(Function(x) x.Customer.Guid.Equals(CurrentCustomer().Guid)).ToList
        Xl_EdiversaSalesReportsHeaders1.Load(oHeaders)
    End Sub

    Private Sub LoadDetails()
        Dim value = Xl_EdiversaSalesReportsHeaders1.Value
        Xl_EdiversaSalesReportItems1.Load(value)
    End Sub

    Private Function CurrentCustomer() As DTOCustomer
        Return ComboBoxCustomer.SelectedItem
    End Function



End Class