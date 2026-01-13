Public Class Frm_InvoicesReceived

    Private _Values As List(Of DTOInvoiceReceived)
    Private _DefaultValue As DTOInvoiceReceived
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOInvoiceReceived = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_InvoicesReceived_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        Await refresca()
    End Sub

    Private Sub Xl_InvoicesReceived1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_InvoicesReceived1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_InvoicesReceived1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_InvoicesReceived1.RequestToAddNew
        Dim oInvoiceReceived As New DTOInvoiceReceived
        Dim oFrm As New Frm_InvoiceReceived(oInvoiceReceived)
        oFrm.Show()
    End Sub

    Private Async Sub Xl_InvoicesReceived1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_InvoicesReceived1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Function CurrentYear() As Integer
        Return Xl_Years1.Value
    End Function

    Private Function CurrentProveidor() As DTOGuidNom
        Dim retval As DTOGuidNom = Nothing
        If ComboBoxProveidors.SelectedIndex > 0 Then
            retval = ComboBoxProveidors.SelectedItem
        End If
        Return retval
    End Function

    Private Sub ComboBoxProveidors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxProveidors.SelectedIndexChanged
        Dim oProveidor = CurrentProveidor()
        If oProveidor Is Nothing Then
            Xl_InvoicesReceived1.ClearFilter()
        Else
            Xl_InvoicesReceived1.Filter = oProveidor.Nom
        End If
    End Sub

    Private Sub LoadYears()
        Dim years As IEnumerable(Of Integer) = Enumerable.Range(start:=2020, count:=DTO.GlobalVariables.Today().Year - 2020 + 1).OrderByDescending(Function(x) x).ToList()
        Xl_Years1.Load(years)
    End Sub

    Private Async Sub Xl_Years_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub

    Private Sub LoadProveidors()
        Dim oProveidors = _Values.
            GroupBy(Function(x) x.Proveidor.Guid).
            Select(Function(y) y.First.Proveidor).
            OrderBy(Function(z) z.Nom).
            ToList()
        oProveidors.Insert(0, DTOGuidNom.Factory(Guid.Empty, "(tots els proveidors)"))
        With ComboBoxProveidors
            .DataSource = oProveidors
            .DisplayMember = "Nom"
            .SelectedIndex = 0
        End With
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Values = Await FEB.InvoicesReceived.All(exs, CurrentYear())
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            LoadProveidors()
            Xl_InvoicesReceived1.Load(_Values, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Sub Xl_InvoicesReceived1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_InvoicesReceived1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub

    Private Sub NovaFacturaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NovaFacturaToolStripMenuItem.Click
        Dim oFrm As New Frm_InvoiceReceived_Factory()
        AddHandler oFrm.afterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class