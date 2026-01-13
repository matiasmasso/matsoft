

Public Class Frm_WebLog
    Private Enum Cols1
        LogCode
        Qty
        LogNom
    End Enum

    Private Enum Cols2
        fch
        Guid
        adr
        nom
    End Enum

    Private mAllowEvents As Boolean

    Private Sub Frm_WebLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = "1/1/" & Today.Year
        DateTimePicker2.Value = Today.AddDays(+1)
        mAllowEvents = True
        LoadResum()
    End Sub

    Private Sub LoadResum()
        Dim sFch1 As String = Format(DateTimePicker1.Value, "yyyyMMdd") ' & "0000"
        Dim sFch2 As String = Format(DateTimePicker2.Value, "yyyyMMdd") ' & "2359"
        Dim SQL As String = "SELECT LogCode, COUNT(DISTINCT src) AS EMAILS,'' as NOM " _
        & "FROM WEBLOG2 " _
        & "WHERE Fch BETWEEN '" & sFch1 & "' AND '" & sFch2 & "' " _
        & "GROUP BY LogCode " _
        & "ORDER BY LogCode"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols1.LogCode)
                .Visible = False
            End With
            With .Columns(Cols1.Qty)
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.LogNom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols1.LogNom
                Try
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim iLogCode As Integer = oRow.Cells(Cols1.LogCode).Value
                    Dim sNom As String = [Enum].GetNames(GetType(WebLog.LogCodes))(iLogCode)
                    e.Value = sNom

                Catch ex As Exception

                End Try
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        LoadDetall()
    End Sub

    Private Function CurrentLogCode() As WebLog.LogCodes
        Dim oCode As WebLog.LogCodes
        If DataGridView1.CurrentRow IsNot Nothing Then
            oCode = DataGridView1.CurrentRow.Cells(Cols1.LogCode).Value
        End If
        Return oCode
    End Function

    Private Sub LoadDetall()
        Dim sFch1 As String = Format(DateTimePicker1.Value, "yyyyMMdd")
        Dim sFch2 As String = Format(DateTimePicker2.Value, "yyyyMMdd")
        Dim SQL As String = "SELECT  MAX(FCH) AS LASTFCH, EMAIL.Guid, EMAIL.Adr, EMAIL.Nom  " _
        & "FROM EMAIL INNER JOIN " _
        & "WEBLOG2 ON EMAIL.guid = WEBLOG2.Src " _
        & "WHERE FCH BETWEEN '" & sFch1 & "' AND '" & sFch2 & "' AND " _
        & "LOGCODE=" & CurrentLogCode() & " " _
        & "GROUP BY email.Guid, EMAIL.Adr, EMAIL.Nom " _
        & "ORDER BY LASTFCH DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView2
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols2.fch)
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols2.Guid)
                .Visible = False
            End With
            With .Columns(Cols2.adr)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.nom)
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub


    Private Function CurrentEmail() As Email
        Dim retval As Email = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols2.Guid).Value
            retval = New Email(oGuid)
        End If
        Return retval
    End Function

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Dim oFrm As New Frm_Contact_Email(CurrentEmail)
        oFrm.Show()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SplitContainer1.Enabled = False
        End If
    End Sub

    Private Sub ButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRefresh.Click
        SplitContainer1.Enabled = True
        LoadResum()
    End Sub
End Class