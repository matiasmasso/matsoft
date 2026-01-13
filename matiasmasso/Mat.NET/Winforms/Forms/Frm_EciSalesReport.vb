Public Class Frm_EciSalesReport
    Private _Stat As DTOStat

    Private _AllowEvents As Boolean

    Private Async Sub Frm_EciSalesReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim exs As New List(Of Exception)
            Dim iYears = Await FEBL.EdiversaSalesReports.Years(GlobalVariables.Emp, exs)
            If exs.Count = 0 Then
                Xl_Years1.Load(iYears)
                ComboBoxUnits.SelectedIndex = 0

                _Stat = New DTOStat(DTOStat.ConceptTypes.Geo, Current.Session.Lang)
                ComboBoxConceptType.SelectedIndex = _Stat.ConceptType
                refresca()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Catch ex As Exception
            Stop
        End Try
    End Sub


    Private Sub CheckBoxProductFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProductFilter.CheckedChanged
        Xl_LookupProduct1.Visible = CheckBoxProductFilter.Checked
        If _Stat.Product IsNot Nothing Then refresca()
    End Sub

    Private Async Sub refresca()
        _Stat.Year = Xl_Years1.Value
        _Stat.Product = IIf(CheckBoxProductFilter.Checked, Xl_LookupProduct1.Product, Nothing)
        _Stat.Format = ComboBoxUnits.SelectedIndex

        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Application.DoEvents()

        _Stat.Items = Await FEBL.EdiversaSalesReport.Items(_Stat, exs)
        If exs.Count = 0 Then
            Xl_StatMonths1.Load(_Stat)
            ProgressBar1.Visible = False
            Application.DoEvents()
        Else
            ProgressBar1.Visible = False
            Application.DoEvents()
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_Years1.AfterUpdate,
        Xl_LookupProduct1.AfterUpdate,
        ComboBoxUnits.SelectedIndexChanged

        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub ComboBoxConceptType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxConceptType.SelectedIndexChanged
        _Stat.ConceptType = ComboBoxConceptType.SelectedIndex
        refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oSheet As ExcelHelper.Sheet = Xl_StatMonths1.Excel(Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class