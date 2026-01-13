Public Class Frm_Kpis
    Private _Balance As DTOBalance
    Private _Comandes As List(Of DTOKpi)
    Private _Impagats As List(Of DTOKpi)
    Private _yearFrom As Integer

    Public Enum Cods
        Marge_de_maniobra
        Comandes
        Impagats
    End Enum

    Private Sub Frm_Kpis_Load(sender As Object, e As EventArgs) Handles Me.Load
        _yearFrom = Today.Year - 2
        Xl_StatGraph1.Load(_yearFrom)
        loadCheckedListbox()
    End Sub


    Private Async Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oBalance = Await Balance(exs)
        If exs.Count = 0 Then
            Dim oBook = _Balance.ExcelBook(Xl_StatGraph1.Kpis, Current.Session.Lang)
            If Not UIHelper.ShowExcel(oBook, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadMargeDeManiobra() As Task
        Dim exs As New List(Of Exception)
        Dim oKpis As New List(Of DTOKpi)

        ProgressBar1.Visible = True
        Cursor = Cursors.WaitCursor
        Dim oBalance = Await Balance(exs)
        Cursor = Cursors.Default
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            oKpis.Add(oBalance.getKpi(DTOKpi.Ids.Activo_Corriente))
            oKpis.Add(oBalance.getKpi(DTOKpi.Ids.Pasivo_Corriente))
            Xl_StatGraph1.AddKpis(oKpis)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadComandes() As Task
        Dim exs As New List(Of Exception)
        Dim oKpis As New List(Of DTOKpi)

        ProgressBar1.Visible = True
        Cursor = Cursors.WaitCursor
        Dim oComandes = Await Comandes(exs)
        Cursor = Cursors.Default
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_StatGraph1.AddKpis(oComandes)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadImpagats() As Task
        Dim exs As New List(Of Exception)
        Dim oKpis As New List(Of DTOKpi)

        ProgressBar1.Visible = True
        Cursor = Cursors.WaitCursor
        Dim oImpagats = Await Impagats(exs)
        Cursor = Cursors.Default
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_StatGraph1.AddKpis(oImpagats.Where(Function(x) x.Id = DTOKpi.Ids.Efectes_Impagats).ToList)
            Xl_StatGraph1.AddKpis(oImpagats.Where(Function(x) x.Id = DTOKpi.Ids.Index_Impagats).ToList)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Function Balance(exs As List(Of Exception)) As Task(Of DTOBalance)
        If _Balance Is Nothing Then
            _Balance = Await FEB2.Balance.Tree(exs, GlobalVariables.Emp, Today.Year - 2)
        End If
        Return _Balance
    End Function

    Private Async Function Comandes(exs As List(Of Exception)) As Task(Of List(Of DTOKpi))
        If _Comandes Is Nothing Then
            _Comandes = Await FEB2.PurchaseOrderItems.Kpis(exs, GlobalVariables.Emp, Today.Year - 2)
        End If
        Return _Comandes
    End Function

    Private Async Function Impagats(exs As List(Of Exception)) As Task(Of List(Of DTOKpi))
        If _Impagats Is Nothing Then
            _Impagats = Await FEB2.Impagats.Kpis(exs, GlobalVariables.Emp, Today.Year - 2)
            Dim indexImpagats = DTOKpi.Factory(DTOKpi.Ids.Index_Impagats)
            Dim oRange = DTOYearMonth.Range(New DTOYearMonth(Today.Year - 2, 1), DTOYearMonth.current)
            Dim paids = _Impagats.First(Function(x) x.Id = DTOKpi.Ids.Efectes_Vençuts)
            Dim unpaids = _Impagats.First(Function(x) x.Id = DTOKpi.Ids.Efectes_Impagats)
            Dim unPaidsIdx = DTOKpi.Factory(DTOKpi.Ids.Index_Impagats)
            For Each item In oRange
                Dim unPaid = unpaids.YearMonths.FirstOrDefault(Function(x) x.Equals(item))
                If unPaid IsNot Nothing Then
                    Dim Paid = paids.YearMonths.FirstOrDefault(Function(x) x.Equals(item))
                    unPaidsIdx.AddYearMonth(item.year, item.month, 100 * unPaid.Eur / (unPaid.Eur + Paid.Eur))
                End If
            Next
            _Impagats.Add(unPaidsIdx)
        End If
        Return _Impagats
    End Function

    Private Sub loadCheckedListbox()
        For Each value As Cods In [Enum].GetValues(GetType(Cods))
            CheckedListBox1.Items.Add(value, False)
        Next
    End Sub

    Private Async Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Select Case e.NewValue
            Case CheckState.Checked
                Select Case e.Index
                    Case Cods.Marge_de_maniobra
                        Await LoadMargeDeManiobra()
                    Case Cods.Comandes
                        Await LoadComandes()
                    Case Cods.Impagats
                        Await LoadImpagats()
                End Select
            Case CheckState.Unchecked
        End Select
    End Sub
End Class