

Public Class Frm_MgzSched
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mMgzSched As MgzSched
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Hora
        Minute
        Clx
    End Enum

    Private Sub Frm_MgzSched_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MonthCalendar1.SetDate(Today)
        LoadGrid()
    End Sub

    Private Sub MonthCalendar1_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected

        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim DtFch As Date = MonthCalendar1.SelectionStart
        Dim iYea As Integer = DtFch.Year
        Dim iMes As Integer = DtFch.Month
        Dim iDay As Integer = DtFch.Day

        Dim SQL As String = "SELECT MGZSCHED.ID, DATEPART(hh,FCH) AS HORA, DATEPART(mi,FCH) AS MINUT,CLX " _
        & "FROM MGZSCHED INNER JOIN " _
        & "CLX ON MGZSCHED.EMP=CLX.EMP AND MGZSCHED.PRV=CLX.CLI WHERE " _
        & "MGZSCHED.EMP=" & mEmp.Id & " AND " _
        & "MGZSCHED.YEA=" & iYea & " AND " _
        & "DATEPART(M,FCH)=" & iMes & " AND " _
        & "DATEPART(d,FCH)=" & iDay & " " _
        & "ORDER BY FCH"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With DataGridView1
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Width = 50
                .HeaderText = "registre"
            End With
            With .Columns(Cols.Hora)
                .Width = 50
                .HeaderText = "hora"
            End With
            With .Columns(Cols.Minute)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mMgzSched = CurrentItm()
        refresca()
    End Sub

    Private Function CurrentItm() As MgzSched
        Dim oMgzSched As MgzSched = Nothing
        Dim iYea As Integer = MonthCalendar1.SelectionStart.Year
        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim iId As Integer = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            oMgzSched = New MgzSched(mEmp, iYea, iId)
        End If
        Return oMgzSched
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Hora
                Dim iMin As Integer = DataGridView1.Rows(e.RowIndex).Cells(Cols.Minute).Value
                Dim s As String = Format(CInt(e.Value), "00") & ":" & Format(iMin, "00")
                e.Value = s
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        mMgzSched = CurrentItm()
        REFRESCA()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs)

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DtFch As Date = MonthCalendar1.SelectionStart
        Dim DtFchTime As New DateTime(DtFch.Year, DtFch.Month, DtFch.Day, CInt(TextBoxHora.Text), CInt(TextBoxMin.Text), 0)
        With mMgzSched
            .Fch = DtFchTime
            .Proveidor = New Proveidor(Xl_ContactPrv.Contact.Guid)
            .m3 = TextBoxM3.Text
            .bultos = TextBoxBultos.Text
            .palets = TextBoxPalets.Text
            .Obs = TextBoxObs.Text
            .Update()
        End With
        'GroupBoxDetail.Visible = False
        LoadGrid()
        ClearItm()
    End Sub

    Private Sub ClearItm()
        mAllowEvents = False
        Xl_ContactPrv.Clear()
        TextBoxHora.Text = "00"
        TextBoxMin.Text = "00"
        TextBoxM3.Text = "0"
        TextBoxBultos.Text = "0"
        TextBoxPalets.Text = "00"
        TextBoxObs.Text = ""
        mAllowEvents = True
        ButtonOk.Enabled = False
    End Sub

    Private Sub refresca()
        mAllowEvents = False
        If mMgzSched Is Nothing Then
            ClearItm()
        Else
            With mMgzSched
                TextBoxFch.Text = .Fch.ToShortDateString
                TextBoxHora.Text = Format(.Fch.Hour, "00")
                TextBoxMin.Text = Format(.Fch.Minute, "00")
                If .Proveidor Is Nothing Then
                    Xl_ContactPrv.Contact = Nothing
                Else
                    Xl_ContactPrv.Contact = .Proveidor
                End If
                TextBoxM3.Text = .m3
                TextBoxBultos.Text = .bultos
                TextBoxPalets.Text = .palets
                TextBoxObs.Text = .Obs
                'GroupBoxDetail.Visible = True
            End With
        End If
        mAllowEvents = True
        ButtonOk.Enabled = False
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        mMgzSched.delete()
        ClearItm()
        'GroupBoxDetail.Visible = False
        LoadGrid()
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        mMgzSched = New MgzSched(mEmp, MonthCalendar1.SelectionStart.Year, 0)
        mMgzSched.Fch = MonthCalendar1.SelectionStart.ToShortDateString
        refresca()
        'GroupBoxDetail.Visible = True
    End Sub
End Class