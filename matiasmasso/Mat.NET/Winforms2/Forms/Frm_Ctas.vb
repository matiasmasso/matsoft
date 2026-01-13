Public Class Frm_Ctas
    Private _Filters As List(Of String)
    Private _Value As DTOSumasYSaldos
    Private _DefaultValue As DTOSumasYSaldosItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional DtFch As Date = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        If DtFch = Nothing Then
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
        Else
            DateTimePicker1.Value = DtFch
        End If
    End Sub

    Public Sub New(oFilters As IEnumerable(Of String))
        MyBase.New()
        Me.InitializeComponent()
        _Filters = oFilters.ToList()
        DateTimePicker1.Value = DTO.GlobalVariables.Today()
    End Sub

    Private Async Sub Frm_SumasYSaldos_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = DTO.GlobalVariables.Today()
        Await refresca()
    End Sub

    Private Sub Xl_SumasYSaldos1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SumasYSaldos1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub


    Private Async Sub Xl_SumasYSaldos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SumasYSaldos1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = DateTimePicker1.Value
        ProgressBar1.Visible = True
        _Value = Await FEB.SumasYSaldos.All(exs, Current.Session.Emp, DtFch)
        If _Filters IsNot Nothing Then
            _Value.items = FilteredItems()
        End If
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_SumasYSaldos1.Load(_Value, _DefaultValue, _SelectionMode)
            ButtonReload.Enabled = False
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function FilteredItems() As List(Of DTOSumasYSaldosItem)
        Dim retval As New List(Of DTOSumasYSaldosItem)
        For Each item In _Value.items
            Dim ctaId As String = item.id
            If _Filters.Any(Function(x) ctaId.StartsWith(x)) Then
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 3 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_SumasYSaldos1.Filter = sSearchKey
        Else
            Xl_SumasYSaldos1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Async Sub ButtonReload_Click(sender As Object, e As EventArgs) Handles ButtonReload.Click
        Await refresca()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ButtonReload.Enabled = True
    End Sub

    Private Sub ToolStripMenuItem3D_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3D.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, _Value, DTOPgcCta.Digits.Digits3, Nothing)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ToolStripMenuItem4D_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4D.Click
        Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, _Value, DTOPgcCta.Digits.Digits4, Nothing)
        UIHelper.SaveExcelDialog(oSheet, Nothing, "Desar balanç de sumes i saldos a 4 digits")
    End Sub

    Private Sub ToolStripMenuItem5D_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5D.Click
        Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, _Value, DTOPgcCta.Digits.Digits5, Nothing)
        UIHelper.SaveExcelDialog(oSheet, Nothing, "Desar balanç de sumes i saldos a 5 digits")
    End Sub

    Private Sub ToolStripMenuItemFull_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemFull.Click
        Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, _Value, DTOPgcCta.Digits.Full, Nothing)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DescuadresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescuadresToolStripMenuItem.Click
        Dim oExercici = DTOExercici.FromYear(Current.Session.Emp, DateTimePicker1.Value.Year)
        Dim oFrm As New Frm_CcaDescuadres(oExercici)
        AddHandler oFrm.RequestToRefresh, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_SumasYSaldos1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_SumasYSaldos1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub
End Class