

Public Class Xl_Gir_SelBancs
    Public Event NextAllowed()
    Public Event NextForbidden()

    Private mCsbs As Csbs
    Private mCsas As New Csas
    Private mDsCsas As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet
    Private mFileFormat As DTOCsa.FileFormats
    Private mTotCsas As DataRow
    Private mAllowEvents As Boolean

    Private Enum Cols
        Chk
        Banc
        Abr
        Tot
        Min
        Max
        Clasificacio
        Disponible
        CheckPropi
        TotPropi
        CheckGrup
        TotGrup
        CheckAltres
        TotAltres
        Tarifa
    End Enum

    Public Sub LoadCsbs(oCsbs As Csbs, oFileFormat As DTOCsa.FileFormats)
        mCsbs = oCsbs
        mFileFormat = oFileFormat
        LoadCsas()
        Distribueix()
    End Sub

    Public ReadOnly Property Csas() As Csas
        Get
            Return mCsas
        End Get
    End Property

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Chk

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        Distribueix()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub LoadCsas()
        mCsas = New Csas
        Dim oCod As Contact.Tipus = Contact.Tipus.Banc

        Dim SQL As String = "SELECT CAST (0 as bit) AS CHECKED, " _
        & "CliBnc.cli, CliBnc.Abr, " _
        & "CAST(0 as MONEY) as TOT, " _
        & "CAST(0 AS MONEY) as MIN, " _
        & "CAST(0 AS MONEY) as MAX, " _
        & "CliBnc.classificacio, " _
        & "CAST(0 AS MONEY) AS DISPONIBLE, " _
        & "CAST (1 AS BIT) AS CHECKPROPIS, " _
        & "CAST(0 AS MONEY) AS TOTPROPIS, " _
        & "CAST (1 AS BIT) AS CHECKGRUP, " _
        & "CAST(0 AS MONEY) AS TOTGRUP, " _
        & "CAST (1 AS BIT) AS CHECKALTRES, " _
        & "CAST(0 AS MONEY) AS TOTALTRES, " _
        & "DTOTARIFA " _
        & "FROM CliBnc INNER JOIN " _
        & "IBAN ON CliBnc.Guid = IBAN.ContactGuid AND IBAN.COD = @COD AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) " _
        & "WHERE CLIBNC.EMP=@EMP AND " _
        & "CliBnc.classificacio > 0 AND " _
        & "IBAN.CCC LIKE 'ES%' AND " _
        & "CliBnc.actiu = 1 AND " _
        & "IBAN.COD=@COD " _
        & "ORDER BY CliBnc.Ord"

        mDsCsas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@COD", CInt(oCod).ToString)
        Dim oCsa As Csa
        Dim oBanc As Banc
        Dim DblDisponible As Decimal
        Dim oTb As DataTable = mDsCsas.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            oBanc = MaxiSrvr.Banc.FromNum(mEmp, oRow(Cols.Banc))
            DblDisponible = oBanc.Norma58Disponible.Eur
            oRow(Cols.Chk) = (DblDisponible > 0)
            oRow(Cols.Min) = 0
            oRow(Cols.Max) = DblDisponible
            oRow(Cols.Disponible) = DblDisponible

            oCsa = New Csa(oBanc, mFileFormat, DTO.DTOCsa.Types.AlDescompte)
            With oCsa
                '.Banc = oBanc
                .fch = Today
                .Andorra = False
                .Condicions = oBanc.Norma58Tarifa
                .descomptat = True
            End With
            mCsas.Add(oCsa)
        Next

        mTotCsas = oTb.NewRow
        oTb.Rows.Add(mTotCsas)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.Banc)
                .Visible = False
            End With
            With .Columns(Cols.Abr)
                .HeaderText = "banc"
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "Girat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Min)
                .HeaderText = "Min"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Max)
                .HeaderText = "Max"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Clasificacio)
                .HeaderText = "Límit"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Disponible)
                .HeaderText = "Disponible"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.CheckPropi)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.TotPropi)
                .HeaderText = "Propis"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.CheckGrup)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.TotGrup)
                .HeaderText = "Grup"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.CheckAltres)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.TotAltres)
                .HeaderText = "Altres"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Tarifa)
                .HeaderText = "tarifa"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With

            With LastRow()
                .DefaultCellStyle.BackColor = Color.LightGray
                .Cells(Cols.Chk).ReadOnly = True
                .Cells(Cols.CheckPropi).ReadOnly = True
                .Cells(Cols.CheckGrup).ReadOnly = True
                .Cells(Cols.CheckAltres).ReadOnly = True
            End With

        End With
    End Sub

    Private Sub Distribueix()
        mAllowEvents = False

        'propis
        Dim oCsa As Csa
        Dim oCsbs As Csbs = mCsbs.Clone
        Dim sBankId As String
        Dim i As Integer
        Dim j As Integer
        Dim oRow As DataGridViewRow
        Dim oRowCsa As DataGridViewRow
        Dim DblEur As Decimal

        'abuida remeses
        For j = 0 To mCsas.Count - 1
            mCsas(j).csbs = New Csbs
        Next

        'posa a zero els totals de cada remesa
        For Each oRow In DataGridView1.Rows
            With oRow
                '.Cells(cols.
                .Cells(Cols.TotPropi).Value = 0
                .Cells(Cols.TotGrup).Value = 0
                .Cells(Cols.TotAltres).Value = 0
                .Cells(Cols.Tot).Value = 0
            End With
        Next

        Dim CsaIdx As Integer = 0
        For i = oCsbs.Count - 1 To 0 Step -1
            DblEur = oCsbs(i).Amt.Eur
            sBankId = BLL.BLLIban.BankId(oCsbs(i).Iban)
            For j = 0 To mCsas.Count - 1
                oRowCsa = DataGridView1.Rows(CsaIdx)
                oCsa = mCsas(CsaIdx)
                CsaIdx = CsaIdx + 1
                If CsaIdx > mCsas.Count - 1 Then CsaIdx = 0
                If oRowCsa.Cells(Cols.Chk).Value And oRowCsa.Cells(Cols.CheckPropi).Value Then
                    If sBankId = BLL.BLLIban.BankId(oCsa.Banc.Iban) Then
                        If oRowCsa.Cells(Cols.Tot).Value + DblEur <= oRowCsa.Cells(Cols.Max).Value Then
                            oCsa.csbs.Add(oCsbs(i))
                            oRowCsa.Cells(Cols.TotPropi).Value += DblEur
                            oRowCsa.Cells(Cols.Tot).Value += DblEur
                            oCsbs.RemoveAt(i)
                            Exit For
                        End If
                    End If
                End If
            Next
        Next

        'grup
        Dim oBank As DTOBank
        For i = oCsbs.Count - 1 To 0 Step -1
            DblEur = oCsbs(i).Amt.Eur
            sBankId = BLL.BLLIban.BankId(oCsbs(i).Iban)
            For j = 0 To mCsas.Count - 1
                oRowCsa = DataGridView1.Rows(CsaIdx)
                oCsa = mCsas(CsaIdx)
                CsaIdx = CsaIdx + 1
                If CsaIdx > mCsas.Count - 1 Then CsaIdx = 0
                If oRowCsa.Cells(Cols.Chk).Value And oRowCsa.Cells(Cols.CheckGrup).Value Then
                    If oRowCsa.Cells(Cols.Tot).Value + DblEur <= oRowCsa.Cells(Cols.Max).Value Then
                        'For Each oBank In oCsa.Banc.Iban.BankBranch.Bank.Group.Banks
                        'If sBankId = oBank.Id Then
                        'oCsa.csbs.Add(oCsbs(i))
                        'oRowCsa.Cells(Cols.TotGrup).Value += DblEur
                        'oRowCsa.Cells(Cols.Tot).Value += DblEur
                        'oCsbs.RemoveAt(i)
                        'Exit For
                        'End If
                        'Next
                    End If
                End If
            Next
        Next

        'altres
        For i = oCsbs.Count - 1 To 0 Step -1
            'sBankId = oCsbs(i).Iban.Bank.Id
            DblEur = oCsbs(i).Amt.Eur
            For j = 0 To mCsas.Count - 1
                oRowCsa = DataGridView1.Rows(CsaIdx)
                oCsa = mCsas(CsaIdx)
                CsaIdx = CsaIdx + 1
                If CsaIdx > mCsas.Count - 1 Then CsaIdx = 0
                If oRowCsa.Cells(Cols.Chk).Value And oRowCsa.Cells(Cols.CheckAltres).Value Then
                    If oRowCsa.Cells(Cols.Tot).Value + DblEur <= oRowCsa.Cells(Cols.Max).Value Then
                        oCsa.csbs.Add(oCsbs(i))
                        oRowCsa.Cells(Cols.TotAltres).Value += DblEur
                        oRowCsa.Cells(Cols.Tot).Value += DblEur
                        oCsbs.RemoveAt(i)
                        Exit For
                    End If
                End If
            Next
        Next

        If oCsbs.Count > 0 Then
            Dim oCsb As Csb
            DblEur = 0
            For Each oCsb In oCsbs
                DblEur += oCsb.Amt.Eur
            Next
            MsgBox("excedit per " & oCsbs.Count & " efectes i " & Format(DblEur, "#,###.00") & "Eur !", MsgBoxStyle.Exclamation)
        End If

        Dim DblTot As Decimal = 0
        Dim DblMin As Decimal = 0
        Dim DblMax As Decimal = 0
        Dim DblCls As Decimal = 0
        Dim DblDsp As Decimal = 0
        For Each oRow In DataGridView1.Rows
            If oRow Is LastRow() Then
                With oRow
                    .Cells(Cols.Tot).Value = DblTot
                    .Cells(Cols.Min).Value = DblMin
                    .Cells(Cols.Max).Value = DblMax
                    .Cells(Cols.Clasificacio).Value = DblCls
                    .Cells(Cols.Disponible).Value = DblDsp
                End With
            Else
                With oRow
                    DblTot += .Cells(Cols.Tot).Value
                    DblMin += .Cells(Cols.Min).Value
                    DblMax += .Cells(Cols.Max).Value
                    DblCls += .Cells(Cols.Clasificacio).Value
                    DblDsp += .Cells(Cols.Disponible).Value
                End With
            End If
        Next

        mAllowEvents = True
    End Sub

    Private Function LastRow() As DataGridViewRow
        Return DataGridView1.Rows(DataGridView1.Rows.Count - 1)
    End Function

    Private Function CurrentBanc() As Banc
        Dim oBanc As Banc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing And oRow IsNot LastRow() Then
            oBanc = MaxiSrvr.Banc.FromNum(mEmp, oRow.Cells(Cols.Banc).Value)
        End If
        Return oBanc
    End Function

    Private Sub DataGridView1_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Select Case e.RowIndex
            Case LastRow.Index
                Select Case e.ColumnIndex
                    Case Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
                        Dim newRect As New Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, _
                                    e.CellBounds.Width - 4, e.CellBounds.Height - 4)
                        Dim backColorBrush As New SolidBrush(e.CellStyle.BackColor)
                        Dim gridBrush As New SolidBrush(Me.DataGridView1.GridColor)
                        Dim gridLinePen As New Pen(gridBrush)

                        Try

                            ' Erase the cell.
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds)

                            ' Draw the grid lines (only the right and bottom lines;
                            ' DataGridView takes care of the others).
                            'e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, _
                            'e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, _
                            'e.CellBounds.Bottom - 1)
                            'e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, _
                            'e.CellBounds.Top, e.CellBounds.Right - 1, _
                            'e.CellBounds.Bottom)

                            ' Draw the inset highlight box.
                            ' e.Graphics.DrawRectangle(Pens.Blue, newRect)

                            ' Draw the text content of the cell, ignoring alignment.
                            'If Not (e.Value Is Nothing) Then
                            'e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, _
                            'Brushes.Crimson, e.CellBounds.X + 2, e.CellBounds.Y + 2, _
                            'StringFormat.GenericDefault)
                            'End If
                            e.Handled = True

                        Finally
                            gridLinePen.Dispose()
                            gridBrush.Dispose()
                            backColorBrush.Dispose()
                        End Try
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Contact(CurrentBanc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenuBanc()
    End Sub

    Private Sub SetContextMenuBanc()
        Dim oContextMenu As New ContextMenuStrip
        Dim oBanc As Banc = CurrentBanc()

        If oBanc IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oBanc)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Min, Cols.Max, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
                If mAllowEvents Then
                    Distribueix()
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

End Class
