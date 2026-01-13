

Public Class Xl_Prestec
    Private mPrestec As Prestec
    Private mAllowEvents As Boolean
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Fch
        Quota
        Acumulat
        Pendent
        Cca
    End Enum

    Public Property Prestec() As Prestec
        Get
            With DataGridView1
                If (mPrestec IsNot Nothing) And (.Rows.Count > 0) Then
                    mPrestec.Fch = DateTimePicker1.Value
                    mPrestec.Nominal = Xl_AmtNominal.Amt
                    Dim oRow As DataGridViewRow = Nothing
                    Dim DtFch As Date = Date.MinValue
                    Dim DcEur As Decimal = 0
                    Dim oQuotes As New PrestecQuotes
                    Dim oCca As Cca = Nothing
                    Dim oCcaGuid As System.Guid
                    For i As Integer = 1 To DataGridView1.Rows.Count - 1
                        oRow = DataGridView1.Rows(i)
                        DtFch = oRow.Cells(Cols.Fch).Value
                        If DtFch > Date.MinValue Then
                            DcEur = oRow.Cells(Cols.Quota).Value
                            If IsDBNull(oRow.Cells("CCA").Value) Then
                                oCcaGuid = System.Guid.Empty
                            Else
                                oCcaGuid = New Guid(oRow.Cells("CCA").Value.ToString)
                            End If
                            oCca = New Cca(oCcaGuid)
                            oQuotes.Add(New PrestecQuota(DtFch, New maxisrvr.Amt(DcEur), oCca))
                        End If
                    Next
                    mPrestec.Quotes = oQuotes
                End If
            End With
            Return mPrestec
        End Get
        Set(ByVal value As Prestec)
            mPrestec = value
            If mPrestec.Fch = Date.MinValue Then
                DateTimePicker1.Value = Today
            Else
                DateTimePicker1.Value = mPrestec.Fch
            End If
            If mPrestec.Nominal Is Nothing Then
                Xl_AmtNominal.Amt = New maxisrvr.Amt
            Else
                Xl_AmtNominal.Amt = mPrestec.Nominal
            End If
            LoadGrid()
            mAllowEvents = True
        End Set
    End Property

    Public Sub LoadGrid()
        Dim oTb As DataTable = CreateTable()
        Dim oRow As DataRow = Nothing
        Dim i As Integer = 0
        Dim DcQuota As Decimal = 0
        Dim DcAcumulat As Decimal = 0

        Dim DcPendent As Decimal = Xl_AmtNominal.Amt.Eur

        oRow = oTb.NewRow
        oRow(Cols.Id) = i
        oRow(Cols.Fch) = mPrestec.Fch
        oRow(Cols.Quota) = 0
        oRow(Cols.Acumulat) = 0
        oRow(Cols.Pendent) = DcPendent
        oTb.Rows.Add(oRow)

        For Each oQuota As PrestecQuota In mPrestec.Quotes
            i += 1
            DcQuota = oQuota.Amt.Eur
            DcAcumulat += DcQuota
            DcPendent = DcPendent - DcQuota
            oRow = oTb.NewRow
            oRow(Cols.Id) = i
            oRow(Cols.Fch) = oQuota.Fch
            oRow(Cols.Quota) = DcQuota
            oRow(Cols.Acumulat) = DcAcumulat
            oRow(Cols.Pendent) = DcPendent
            If oQuota.Cca Is Nothing Then
                oRow(Cols.Cca) = System.Guid.Empty
            Else
                oRow(Cols.Cca) = oQuota.Cca.Guid
            End If
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .HeaderText = "quota"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .ReadOnly = True
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Quota)
                .HeaderText = "amortitzacio"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Acumulat)
                .HeaderText = "acumulat"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Pendent)
                .HeaderText = "pendent"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Cca)
                .Visible = False
            End With
        End With

    End Sub

    Public Function CreateTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Id", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("Fch", System.Type.GetType("System.DateTime")))
            .Add(New DataColumn("Quota", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Acumulat", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Pendent", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Cca", System.Type.GetType("System.String")))
        End With
        Return oTb
    End Function


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Quota
                    Recalcula(e.RowIndex)
                    RaiseEvent Changed(Nothing, EventArgs.Empty)
                Case Cols.Pendent

                    RaiseEvent Changed(Nothing, EventArgs.Empty)
            End Select
        End If
    End Sub

    Private Sub Recalcula(ByVal iRowIdx As Integer)
        Dim oRow As DataGridViewRow = DataGridView1.Rows(0)
        Dim DcNominal As Decimal = oRow.Cells(Cols.Pendent).Value
        oRow = DataGridView1.Rows(iRowIdx)
        Select Case iRowIdx
            Case 0
            Case 1
                Dim DcQuota As Decimal = oRow.Cells(Cols.Quota).Value
                Dim DcAcumulat As Decimal = DcQuota
                Dim DcPendent As Decimal = DcNominal - DcAcumulat
                oRow.Cells(Cols.Acumulat).Value = DcAcumulat
                oRow.Cells(Cols.Pendent).Value = DcPendent
            Case Else
                Dim oPreviousRow As DataGridViewRow = DataGridView1.Rows(iRowIdx - 1)
                Dim DcPreviousAcumulat = oPreviousRow.Cells(Cols.Acumulat).Value
                Dim DcQuota As Decimal = oRow.Cells(Cols.Quota).Value
                Dim DcAcumulat As Decimal = DcPreviousAcumulat + DcQuota
                Dim DcPendent As Decimal = DcNominal - DcAcumulat
                oRow.Cells(Cols.Acumulat).Value = DcAcumulat
                oRow.Cells(Cols.Pendent).Value = DcPendent
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then

        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If IsDBNull(oRow.Cells(Cols.Cca).value) Then
                oMenuItem = New ToolStripMenuItem("pagar de quota", Nothing, AddressOf PayQuota)
            Else
                oMenuItem = New ToolStripMenuItem("consultar quota", Nothing, AddressOf ZoomQuota)
            End If
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub PayQuota(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ZoomQuota(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    '=============================tabstop columns

    Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        Dim dgc As DataGridViewCell = TryCast(DataGridView1.Item(e.ColumnIndex, e.RowIndex), DataGridViewCell)
        If dgc IsNot Nothing AndAlso dgc.ReadOnly Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub DataGridView1_DefaultValuesNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.DefaultValuesNeeded
        If DataGridView1.Rows.Count > 1 Then
            Dim oRow As DataGridViewRow = e.Row
            Dim oLastRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Count - 2)
            Dim iLastId As Integer = CInt(oLastRow.Cells(Cols.Id).Value)
            Dim DtLastFch As Date = CDate(oLastRow.Cells(Cols.Fch).Value)
            oRow.Cells(Cols.Id).Value = iLastId + 1
            oRow.Cells(Cols.Fch).Value = DtLastFch.AddMonths(1)
        End If
    End Sub
End Class
