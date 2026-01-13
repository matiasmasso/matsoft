
Imports System.Data
Imports System.Data.SqlClient

Public Class Frm_PdcFchCreated

    Private mAllowEvents As Boolean
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Hora
        sum
        pct
        w1
        w2
        w3
        w4
        w5
        w6
        w7
    End Enum

    Private Enum ColsSrc
        Chk
        Src
        Nom
        Sum
        Pct
    End Enum

    Private Sub Frm_PdcFchCreated_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DtFch As DateTime = Today
        NumericUpDownYea.Value = DtFch.Year
        DateTimePicker1.Value = New Date(DtFch.Year, 1, 1)
        DateTimePicker2.Value = DtFch
        DateTimePicker1.MinDate = New Date(DtFch.Year, 1, 1)
        DateTimePicker1.MaxDate = DateTimePicker2.Value
        DateTimePicker2.MinDate = DateTimePicker1.Value
        DateTimePicker2.MaxDate = New Date(DtFch.Year, 12, 31)
        LoadSrcs()
        PreloadGrid()
        LoadGrid()
        mAllowEvents = True
    End Sub



    Private Sub PreloadGrid()
        With DataGridView1.Columns(Cols.Hora)
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.BackColor = Color.LightGray
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        With DataGridView1.Columns(Cols.sum)
            .DefaultCellStyle.Format = "#,###"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.BackColor = Color.LightGray
            .Width = 45
        End With

        With DataGridView1.Columns(Cols.pct)
            .DefaultCellStyle.Format = "0%;-0%;#"
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.BackColor = Color.LightGray
            .Width = 40
        End With

        For j As Integer = Cols.w1 To Cols.w7
            With DataGridView1.Columns(j)
                .DefaultCellStyle.Format = "#,###"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
        Next

        DataGridView1.Rows.Add("suma", 0, 0, 0, 0, 0, 0, 0, 0)
        DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.LightGray

        DataGridView1.Rows.Add("quota", 0, 0, 0, 0, 0, 0, 0, 0)
        DataGridView1.Rows(1).DefaultCellStyle.Format = "0%;-0%;#"
        DataGridView1.Rows(1).DefaultCellStyle.BackColor = Color.LightGray

        For i As Integer = 0 To 23
            DataGridView1.Rows.Add(Format(i, "00"), 0, 0, 0, 0, 0, 0, 0, 0)
        Next
    End Sub

    Private Sub LoadGrid()
        For i As Integer = 0 To 25
            For j As Integer = Cols.sum To Cols.w7
                DataGridView1.Rows(i).Cells(j).Value = 0
            Next
        Next

        Dim SQL As String = "SELECT DATEPART(Hh, FchCreated) AS HORA"

        SQL = SQL & ", COUNT(PDC) AS TOT, 0 AS PCT "

        For i As Integer = 1 To 7
            SQL = SQL & ", SUM(CASE WHEN DATEPART(W, fchCreated) =" & i.ToString & " THEN 1 ELSE 0 END) AS W" & i.ToString & " "
        Next



        SQL = SQL & "FROM PDC " _
        & "WHERE Emp =@EMP AND " _
        & "yea =@YEA AND " _
        & "COD=2 AND " _
        & "FCHCREATED BETWEEN @FCH1 AND @FCH2 "


        Dim s As String = ""

        For i As Integer = 0 To DataGridViewSrc.Rows.Count - 1
            If ItemChecked(i) Then
                If s > "" Then s = s & " OR "
                s = s & " SRC=" & DataGridViewSrc.Rows(i).Cells(ColsSrc.Src).Value & " "
            End If
        Next

        If s > "" Then
            SQL = SQL & " AND (" & s & ") "
        Else
            Exit Sub
        End If

        SQL = SQL & "GROUP BY DATEPART(Hh, FchCreated) " _
        & "ORDER BY HORA"

        Dim oRow As DataGridViewRow
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", CurrentYea, "@FCH1", Fch1, "@FCH2", Fch2)
        Do While oDrd.Read
            oRow = DataGridView1.Rows(CInt(oDrd(Cols.Hora)) + 2)
            For iCol As Integer = Cols.w1 To Cols.w7
                oRow.Cells(iCol).Value = CInt(oDrd(iCol))
            Next
        Loop
        oDrd.Close()

        Dim iSum As Integer = 0
        Dim iTot As Integer = 0
        Dim iDias As Integer = DateDiff(DateInterval.Day, Fch1, Fch2)
        Dim iWeeks As Decimal = iDias / 7
        CheckBoxAverage.Text = "valors en promig diari (" & Math.Round(iWeeks, 0) & " setmanes)"
        For j As Integer = Cols.w1 To Cols.w7
            iSum = 0
            For i As Integer = 2 To 25
                If CheckBoxAverage.Checked Then
                    DataGridView1.Rows(i).Cells(j).Value = DataGridView1.Rows(i).Cells(j).Value / iWeeks
                End If
                iSum += DataGridView1.Rows(i).Cells(j).Value
            Next
            DataGridView1.Rows(0).Cells(j).Value = iSum
            iTot += iSum
        Next

        'fila suma total
        For j As Integer = Cols.w1 To Cols.w7
            DataGridView1.Rows(0).Cells(Cols.sum).Value += DataGridView1.Rows(0).Cells(j).Value
        Next

        'fila percentatjes per dia
        For j As Integer = Cols.w1 To Cols.w7
            DataGridView1.Rows(1).Cells(j).Value = DataGridView1.Rows(0).Cells(j).Value / iTot
        Next
        DataGridView1.Rows(1).Cells(Cols.pct).Value = 1


        'columna percentatje per hora
        For i As Integer = 2 To 25
            iSum = 0
            For j As Integer = Cols.w1 To Cols.w7
                iSum += DataGridView1.Rows(i).Cells(j).Value
            Next
            DataGridView1.Rows(i).Cells(Cols.sum).Value = iSum
        Next

        For i As Integer = 2 To 25
            DataGridView1.Rows(i).Cells(Cols.pct).Value = DataGridView1.Rows(i).Cells(Cols.sum).Value / iTot
        Next



    End Sub

    Private Function ItemChecked(ByVal idx As Integer) As Boolean
        Dim oRow As DataGridViewRow = DataGridViewSrc.Rows(idx)
        Dim oCell As DataGridViewCell = oRow.Cells(ColsSrc.Chk)
        Dim RetVal As Boolean = oCell.Value
        Return RetVal
    End Function

    Private Sub LoadSrcs()
        Dim SQL As String = "SELECT CAST(0 AS BIT) AS CHK, SRC, '' AS NOM, COUNT(PDC) as SUM, CAST(0 AS DECIMAL) AS PCT FROM PDC WHERE " _
        & "EMP=@EMP AND " _
        & "YEA=@YEA AND " _
        & "COD=2 AND " _
        & "FCHCREATED BETWEEN @FCH1 AND @FCH2 " _
        & "GROUP BY SRC " _
        & "ORDER BY COUNT(PDC) desc"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", CurrentYea, "@FCH1", Fch1, "@FCH2", Fch2)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oRow As DataRow
        Dim iTot As Integer
        Dim oSrc As DTOPurchaseorder.Sources
        Dim oEnumType As Type = GetType(DTOPurchaseorder.Sources)
        For Each oRow In oTb.Rows
            oSrc = oRow(ColsSrc.Src)
            Dim sNom As String = [Enum].Parse(oEnumType, oSrc).ToString
            oRow(ColsSrc.Nom) = sNom.Replace("_", " ")
            iTot += oRow(ColsSrc.Sum)
        Next

        Dim iCount As Integer
        Dim DecPct As Decimal
        For Each oRow In oTb.Rows
            iCount = oRow(ColsSrc.Sum)
            DecPct = iCount / iTot
            oRow(ColsSrc.Pct) = DecPct
        Next

        oRow = oTb.NewRow
        oRow(ColsSrc.Chk) = False
        oRow(ColsSrc.Src) = 0
        oRow(ColsSrc.Nom) = "tot"
        oRow(ColsSrc.Sum) = 0
        oRow(ColsSrc.Sum) = iTot
        oRow(ColsSrc.Pct) = 1
        oTb.Rows.InsertAt(oRow, 0)

        With DataGridViewSrc
            .DataSource = oTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect

            With .Columns(ColsSrc.Chk)
                .Width = 20
            End With
            With .Columns(ColsSrc.Src)
                .Visible = False
            End With
            With .Columns(ColsSrc.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsSrc.Sum)
                .Width = 45
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsSrc.Pct)
                .Width = 35
                .DefaultCellStyle.Format = "0%;-0%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Rows(0).DefaultCellStyle.BackColor = Color.LightGray
        End With

    End Sub

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Function Fch1() As Date
        Return DateTimePicker1.Value
    End Function

    Private Function Fch2() As Date
        Return DateTimePicker2.Value
    End Function


    Private Sub DataGridViewSrc_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewSrc.CellValueChanged
        Select Case e.ColumnIndex
            Case ColsSrc.Chk
                If mAllowEvents Then
                    If e.RowIndex = 0 Then CheckAll()
                    LoadGrid()
                End If
        End Select
    End Sub

    Private Sub DataGridViewSrc_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewSrc.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridViewSrc.CurrentCell.ColumnIndex
            Case ColsSrc.Chk
                DataGridViewSrc.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub CheckAll()
        mAllowEvents = False
        For Each oRow As DataGridViewRow In DataGridViewSrc.Rows
            oRow.Cells(ColsSrc.Chk).Value = DataGridViewSrc.Rows(0).Cells(ColsSrc.Chk).Value
        Next
        mAllowEvents = True
    End Sub

    Private Sub NumericUpDownYea_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownYea.ValueChanged
        If mAllowEvents Then

            Dim iYea As Integer = NumericUpDownYea.Value
            Dim DtFch1 As New Date(iYea, 1, 1)
            Dim DtFch2 As New Date(iYea, 12, 31)
            If iYea = Today.Year Then DtFch2 = Today
            mAllowEvents = False
            DateTimePicker1.MinDate = DateTimePicker.MinimumDateTime
            DateTimePicker1.MaxDate = DateTimePicker.MaximumDateTime
            DateTimePicker2.MinDate = DateTimePicker.MinimumDateTime
            DateTimePicker2.MaxDate = DateTimePicker.MaximumDateTime
            DateTimePicker1.Value = DtFch1
            DateTimePicker2.Value = DtFch2
            DateTimePicker1.MinDate = New Date(iYea, 1, 1)
            DateTimePicker2.MaxDate = New Date(iYea, 12, 31)
            DateTimePicker1.MaxDate = DateTimePicker2.Value
            DateTimePicker2.MinDate = DateTimePicker1.Value
            mAllowEvents = True
            LoadGrid()
        End If
    End Sub

    Private Sub DateTimePicker_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
    DateTimePicker1.Validating, DateTimePicker2.Validating
        Dim iYea As Integer = NumericUpDownYea.Value
        Dim oControl As DateTimePicker = sender
        If oControl.Value.Year <> iYea Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DateTimePicker_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePicker1.ValueChanged, DateTimePicker2.ValueChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub CheckBoxAverage_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAverage.CheckedChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub
End Class