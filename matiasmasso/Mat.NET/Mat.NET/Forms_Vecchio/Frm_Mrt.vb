

Public Class Frm_Mrt

    Private mMrt As Mrt
    Private mEmp as DTOEmp
    Private mDs As DataSet
    Private mAllowEvents As Boolean
    Private mDirtyBaixa As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Lin
        Fch
        Cuota
        Tipus
        Acumulat
        Saldo
    End Enum


    Public WriteOnly Property Mrt() As Mrt
        Set(ByVal Value As Mrt)
            mMrt = Value
            With mMrt
                mEmp = .Emp
                Me.Text = Me.Text & " #" & .Id
                TextBoxAltaYea.Text = .Alta.yea
                TextBoxAltaCca.Text = .Alta.Id
                DateTimePicker1.Value = .Fch
                TextBoxDsc.Text = .Nom
                Xl_Cta1.Cta = .Cta
                TextBoxTipus.Text = .Tipo
                Xl_AmtCur1.Amt = .Amt
                If .Baixa.Exists Then
                    CheckBoxBaixa.Checked = True
                    DateTimePickerBaixa.Value = .Baixa.fch
                    TextBoxBaixa.Text = .Baixa.Txt
                    ButtonCcaBaixaShow.Enabled = True
                    CheckBoxBaixa.Enabled = Not BLL.BLLApp.CcaIsBlockedYear(.Baixa.yea)
                End If
                If .Id = 0 Then ButtonOk.Enabled = True
            End With
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT lin, fch, eur as cuota, '' as tipus, CAST (0 AS NUMERIC) as acumulat, CAST(0 AS NUMERIC) as saldo " _
        & "FROM Mr2 " _
        & "WHERE emp =" & mMrt.Emp.Id & " AND " _
        & "ITM=" & mMrt.Id & " " _
        & "ORDER BY lin"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oRow As DataRow
        Dim DblAcumulat As Decimal
        Dim DblVal As Decimal = mMrt.Amt.Eur
        Dim DblCuota As Decimal
        For Each oRow In oTb.Rows
            DblCuota = oRow(Cols.Cuota)
            DblAcumulat = DblAcumulat + DblCuota
            oRow(Cols.Tipus) = Format(100 * DblCuota / DblVal, "0.00") & " %"
            oRow(Cols.Acumulat) = DblAcumulat
            oRow(Cols.Saldo) = DblVal - DblAcumulat
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Lin)
                .HeaderText = "LIN"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "DATA"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Cuota)
                .Width = 80
                .HeaderText = "CUOTA"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Tipus)
                .HeaderText = "TIPUS"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Acumulat)
                .HeaderText = "ACUMULAT"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Saldo)
                .HeaderText = "SALDO"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxDsc.TextChanged, _
        DateTimePicker1.ValueChanged, _
        TextBoxTipus.TextChanged, _
         TextBoxAltaYea.TextChanged, _
          TextBoxAltaCca.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMrt
            .Fch = DateTimePicker1.Value
            If IsNumeric(TextBoxAltaYea.Text) And IsNumeric(TextBoxAltaCca.Text) Then
                .Alta = MaxiSrvr.Cca.FromNum(mEmp, TextBoxAltaYea.Text, TextBoxAltaCca.Text)
            End If
            .Nom = TextBoxDsc.Text
            .Amt = Xl_AmtCur1.Amt
            .Cta = Xl_Cta1.Cta
            .Tipo = TextBoxTipus.Text
            .update()
        End With
        RaiseEvent AfterUpdate(mMrt, New System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Function CurrentMr2() As Mr2
        Dim oMr2 As Mr2 = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oMr2 = New Mr2(mMrt, oRow.Cells(Cols.Lin).Value)
        End If
        Return oMr2
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMr2 As Mr2 = CurrentMr2()
        If oMr2 IsNot Nothing Then
            Dim oMenu_Mr2 As New Menu_Mr2(oMr2)
            AddHandler oMenu_Mr2.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Mr2.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fch
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub CheckBoxBaixa_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.CheckedChanged
        EnableBaixa()
        mDirtyBaixa = True
    End Sub

    Private Sub EnableBaixa()
        TextBoxBaixa.Enabled = CheckBoxBaixa.Checked
        DateTimePickerBaixa.Enabled = CheckBoxBaixa.Checked
        ButtonCcaBaixaShow.Enabled = (CheckBoxBaixa.Checked And mMrt.Baixa.Exists)
    End Sub

    Private Sub ButtonCcaBaixaShow_Click(sender As Object, e As EventArgs) Handles ButtonCcaBaixaShow.Click
        Dim oFrm As New Frm_Cca(mMrt.Baixa)
        AddHandler oFrm.AfterUpdate, AddressOf onBaixaUpdated
        oFrm.Show()
    End Sub

    Private Sub onBaixaUpdated()
        If mMrt.Baixa.Exists Then
            CheckBoxBaixa.Checked = True
            DateTimePickerBaixa.Value = mMrt.Baixa.fch
            TextBoxBaixa.Text = mMrt.Baixa.Txt
            ButtonCcaBaixaShow.Enabled = True
            CheckBoxBaixa.Enabled = Not BLL.BLLApp.CcaIsBlockedYear(mMrt.Baixa.yea)
        End If
    End Sub
End Class
