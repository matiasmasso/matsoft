

Public Class Frm_Intrastat_Old
    Private _Intrastat As Intrastat

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        ImportacioGuid
        GrpVal
        GrpIco
        ChkVal
        ChkIco
        Yea
        Id
        Fch
        Amt
        Prv
        Clx
        Guid
    End Enum

    Private Enum Grps
        Empty
        Plus
        Minus
    End Enum

    Private Enum Chks
        Unchecked
        Grayed
        Checked
    End Enum


    Public Sub New(oIntrastat As DTOIntrastat)
        MyBase.New()
        Me.InitializeComponent()
        _Intrastat = New Intrastat(oIntrastat.Guid)
    End Sub


    Public Sub New(oIntrastat As Intrastat)
        MyBase.New()
        Me.InitializeComponent()
        _Intrastat = oIntrastat
    End Sub

    Private Sub Frm_Intrastat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _Intrastat.IsNew Then
            Me.Text = "NOU FORMULARI INTRASTAT"
        Else
            IntrastatLoader.Load(_Intrastat)
            Me.Text = "INTRASTAT " & _Intrastat.Mes.ToString & "/" & _Intrastat.Yea.ToString
            ButtonDel.Enabled = True
        End If
        LoadFch()
        LoadGrid()
        SetTotals()
    End Sub

    Private Sub LoadGrid()
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp

        Dim SQL As String = ""
        Dim oDs As DataSet
        Dim oTb As DataTable

        If _Intrastat.IsNew Then
            SQL = "SELECT IMPORTDTL.HeaderGuid, 0 AS GRP, 0 AS CHK, IMPORTDTL.Yea, IMPORTDTL.Id, CCA.fch, SUM(CCB.eur) As DTOAmt, IMPORTHDR.PrvGuid, CCA.txt, IMPORTDTL.GUID, ImportDtl.Intrastat " _
            & "FROM IMPORTDTL INNER JOIN " _
            & "IMPORTHDR ON IMPORTDTL.EMP=IMPORTHDR.EMP AND IMPORTDTL.YEA=IMPORTHDR.YEA AND IMPORTDTL.ID=IMPORTHDR.ID INNER JOIN " _
            & "CCA ON IMPORTDTL.Guid = CCA.Guid INNER JOIN " _
            & "CCB ON Ccb.CcaGuid = Cca.Guid " _
            & "LEFT OUTER JOIN Country P ON IMPORTHDR.PAISORIGEN LIKE P.ISO " _
            & "INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid AND PgcCta.Id LIKE '6%' " _
            & "WHERE IMPORTDTL.INTRASTAT IS NULL AND P.CEE=1 AND IMPORTDTL.SRCCOD=" & DTOImportacioItem.SourceCodes.Fra & " " _
            & "GROUP BY IMPORTDTL.HeaderGuid, IMPORTDTL.Yea, IMPORTDTL.Id, IMPORTHDR.PrvGuid, CCA.txt, CCA.fch, IMPORTDTL.GUID, ImportDtl.Intrastat " _
            & "ORDER BY IMPORTDTL.Yea DESC, IMPORTDTL.Id DESC, CCA.txt"
            oDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", BLLApp.Emp.Id)
            oTb = oDs.Tables(0)
        Else
            SQL = "SELECT IMPORTDTL.HeaderGuid, 0 AS GRP, 2 AS CHK, IMPORTDTL.Yea, IMPORTDTL.Id, CCA.fch, SUM(CCB.eur) As DTOAmt, '" & System.Guid.Empty.ToString & "' AS PrvGuid, CCA.txt, IMPORTDTL.GUID " _
            & "FROM IMPORTDTL INNER JOIN " _
            & "CCA ON IMPORTDTL.Guid = CCA.Guid INNER JOIN " _
            & "CCB ON Ccb.CcaGuid = Cca.Guid " _
            & "INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid AND PgcCta.Id LIKE '6%' " _
            & "WHERE IMPORTDTL.INTRASTAT = '" & _Intrastat.Guid.ToString & "' " _
            & "GROUP BY IMPORTDTL.HeaderGuid, IMPORTDTL.Yea, IMPORTDTL.Id, CCA.txt, CCA.fch, IMPORTDTL.GUID " _
            & "ORDER BY IMPORTDTL.Yea DESC, IMPORTDTL.Id DESC, CCA.txt"
            oDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", BLLApp.Emp.Id, "@GUID", _Intrastat.Guid.ToString)
            oTb = oDs.Tables(0)


        End If


        'afegeix icono GRP
        Dim oColGrp As DataColumn = oTb.Columns.Add("GRPICO", System.Type.GetType("System.Byte[]"))
        oColGrp.SetOrdinal(Cols.GrpIco)

        'afegeix icono CHK
        Dim oColChk As DataColumn = oTb.Columns.Add("CHKICO", System.Type.GetType("System.Byte[]"))
        oColChk.SetOrdinal(Cols.ChkIco)

        Dim oHdr As New Importacio(Guid.NewGuid)
        Dim oRow As DataRow
        Dim oHdrRow As DataRow
        Dim i As Integer
        Do While i < oTb.Rows.Count
            oRow = oTb.Rows(i)
            Dim oImportacioGuid As Guid = oRow(Cols.ImportacioGuid)
            If Not oHdr.Guid.Equals(oImportacioGuid) Then
                oHdr = ImportacioLoader.Find(oImportacioGuid)
                oHdrRow = oTb.NewRow
                oHdrRow(Cols.GrpVal) = 1
                oHdrRow(Cols.ChkVal) = IIf(_Intrastat Is Nothing, Chks.Unchecked, Chks.Checked)
                oHdrRow(Cols.Yea) = oHdr.Yea
                oHdrRow(Cols.Id) = oHdr.Id
                oHdrRow(Cols.Fch) = oHdr.Fch
                If oHdr.Amt IsNot Nothing Then
                    oHdrRow(Cols.Amt) = oHdr.Amt.Eur
                End If
                If oHdr.Proveidor IsNot Nothing Then
                    oHdrRow(Cols.Prv) = oHdr.Proveidor.Guid
                    oHdrRow(Cols.Clx) = oHdr.Proveidor.Clx
                End If
                oTb.Rows.InsertAt(oHdrRow, i)
            End If
            i += 1
        Loop

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = 15 ' DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.ImportacioGuid)
                .Visible = False
            End With
            With .Columns(Cols.GrpVal)
                .Visible = False
            End With
            With .Columns(Cols.GrpIco)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.ChkVal)
                .Visible = False
            End With
            With .Columns(Cols.ChkIco)
                If _Intrastat.IsNew Then
                    .HeaderText = ""
                    .Width = 20
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.SelectionBackColor = Color.White
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Remesa"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Prv)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Guid)
                .Visible = False
            End With

        End With

        Dim gRow As DataGridViewRow
        For Each gRow In DataGridView1.Rows
            If CType(gRow.Cells(Cols.GrpVal).Value, Grps) = Grps.Empty Then
                gRow.DefaultCellStyle.BackColor = Color.LightGray
                gRow.Visible = False
            End If
        Next
    End Sub

    Private Sub LoadFch()
        Dim oLang As DTOLang = BLL.BLLApp.Lang
        For i As Integer = 1 To 12
            ComboBoxMes.Items.Add(oLang.MesAbr(i))
        Next

        If _Intrastat.IsNew Then
            Dim DtFch As Date = Today.AddMonths(-1)
            TextBoxYea.Text = DtFch.Year
            ComboBoxMes.SelectedIndex = DtFch.Month - 1
        Else
            TextBoxYea.Text = _Intrastat.Yea
            ComboBoxMes.SelectedIndex = _Intrastat.Mes - 1
        End If
    End Sub

    Private Function CurrentYea() As Integer
        Dim RetVal As Integer
        If IsNumeric(TextBoxYea.Text) Then
            RetVal = CInt(TextBoxYea.Text)
        End If
        Return RetVal
    End Function

    Private Function CurrentMes() As Integer
        Dim RetVal As Integer = ComboBoxMes.SelectedIndex + 1
        Return RetVal
    End Function

    Private Function CurrentImportacio() As Importacio
        Dim oImportacio As Importacio = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
            Dim iId As Integer = oRow.Cells(Cols.Id).Value
            If GuidHelper.IsGuid(oRow.Cells(Cols.ImportacioGuid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.ImportacioGuid).Value
                oImportacio = New Importacio(oGuid)
            End If
        End If
        Return oImportacio
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.GrpIco
                Dim iGrpVal As Integer = DataGridView1.Rows(e.RowIndex).Cells(Cols.GrpVal).Value
                Select Case iGrpVal
                    Case 0
                        e.Value = My.Resources.empty
                    Case 1
                        e.Value = My.Resources.PLUS
                    Case 2
                        e.Value = My.Resources.minus
                End Select
            Case Cols.ChkIco
                Dim iChkVal As Integer = DataGridView1.Rows(e.RowIndex).Cells(Cols.ChkVal).Value
                Select Case iChkVal
                    Case 0
                        e.Value = My.Resources.UnChecked13
                    Case 1
                        e.Value = My.Resources.CheckedGrayed13
                    Case 2
                        e.Value = My.Resources.Checked13
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_RowHeightChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.RowHeightChanged
        If DataGridView1.RowTemplate.Height <> DataGridView1.Font.Height * 1.3 Then
            DataGridView1.RowTemplate.Height = DataGridView1.Font.Height * 1.3
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        'SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.ChkVal, Cols.ChkIco
                SetTotals()
                'RaiseEvent Changed(Me, New System.EventArgs)
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.ChkVal, Cols.ChkIco
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub SetTotals()
        Dim iCount As Integer
        Dim DcImport As Decimal
        Dim s As String = ""
        Dim oRow As DataGridViewRow = Nothing
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            oRow = DataGridView1.Rows(i)
            If CType(oRow.Cells(Cols.ChkVal).Value, Chks) = Chks.Checked Then
                If CType(oRow.Cells(Cols.GrpVal).Value, Grps) = Grps.Empty Then
                    DcImport += DataGridView1.Rows(i).Cells(Cols.Amt).Value
                Else
                    iCount += 1
                End If
            End If
        Next
        Select Case iCount
            Case 0
                s = "(no hi ha cap remesa seleccionada)"
                ButtonOk.Enabled = False
            Case 1
                s = "1 remesa per import de " & Format(DcImport, "#,##0.00")
                ButtonOk.Enabled = True
            Case Else
                s = iCount.ToString & " remeses per import de " & Format(DcImport, "#,##0.00")
                ButtonOk.Enabled = True
        End Select
        LabelStatus.Text = s
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If _Intrastat.IsNew Then
            _Intrastat = New Intrastat(BLL.BLLApp.Emp)
        End If

        With _Intrastat
            .Yea = CurrentYea()
            .Mes = CurrentMes()
            With .ImportacioItems
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    Dim BlLineItm As Boolean = CType(DataGridView1.Rows(i).Cells(Cols.GrpVal).Value, Grps) = Grps.Empty
                    Dim BlChecked As Boolean = DataGridView1.Rows(i).Cells(Cols.ChkVal).Value
                    If BlChecked And BlLineItm Then
                        Dim oHeader As New Importacio(DataGridView1.Rows(i).Cells(Cols.ImportacioGuid).Value)
                        Dim oGuid As Guid = DataGridView1.Rows(i).Cells(Cols.Guid).Value
                        Dim oItem As New ImportacioItem(oHeader, DTOImportacioItem.SourceCodes.Fra, oGuid)
                        .Add(oItem)
                    End If
                Next
            End With
        End With

        Dim exs as New List(Of exception)
        If IntrastatLoader.Update(_Intrastat, exs) Then
            RaiseEvent AfterUpdate(_Intrastat, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar l'Intrastat")
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.GrpIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.GrpVal).Value, Grps)
                    Case Grps.Plus
                        oRow.Cells(Cols.GrpVal).Value = Grps.Minus
                        For i As Integer = e.RowIndex + 1 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(Cols.Yea).Value = oRow.Cells(Cols.Yea).Value And DataGridView1.Rows(i).Cells(Cols.Id).Value = oRow.Cells(Cols.Id).Value Then
                                DataGridView1.Rows(i).Visible = True
                            Else
                                Exit For
                            End If
                        Next
                    Case Grps.Minus
                        oRow.Cells(Cols.GrpVal).Value = Grps.Plus
                        For i As Integer = e.RowIndex + 1 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(i).Cells(Cols.Yea).Value = oRow.Cells(Cols.Yea).Value And DataGridView1.Rows(i).Cells(Cols.Id).Value = oRow.Cells(Cols.Id).Value Then
                                DataGridView1.Rows(i).Visible = False
                            Else
                                Exit For
                            End If
                        Next
                End Select

            Case Cols.ChkIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim OldStatus As Chks = oRow.Cells(Cols.ChkVal).Value
                Dim NewStatus As Chks = IIf(OldStatus = Chks.Checked, Chks.Unchecked, Chks.Checked)
                oRow.Cells(Cols.ChkVal).Value = NewStatus
                Select Case CType(oRow.Cells(Cols.GrpVal).Value, Grps)
                    Case Grps.Plus, Grps.Minus
                        'set NewStatus to Children
                        SetChildrenStatus(oRow, NewStatus)
                    Case Else
                        'set new status to parent
                        Dim oRowParent As DataGridViewRow = ParentRow(oRow)
                        UpdateParentRow(oRowParent)
                End Select
                DataGridView1.Refresh()
        End Select
    End Sub

    Private Function ParentRow(ByVal oRow As DataGridViewRow) As DataGridViewRow
        Dim RetVal As DataGridViewRow = Nothing
        Dim oGrid As DataGridView = oRow.DataGridView
        For i As Integer = oRow.Index - 1 To 0 Step -1
            If CType(DataGridView1.Rows(i).Cells(Cols.GrpVal).Value, Grps) <> Grps.Empty Then
                If DataGridView1.Rows(i).Cells(Cols.Yea).Value = oRow.Cells(Cols.Yea).Value And DataGridView1.Rows(i).Cells(Cols.Id).Value = oRow.Cells(Cols.Id).Value Then
                    RetVal = DataGridView1.Rows(i)
                Else
                    Exit For
                End If
            End If
        Next
        Return RetVal
    End Function

    Private Sub UpdateParentRow(ByVal oParentRow As DataGridViewRow)
        Dim oChkStatus As Chks = Chks.Unchecked
        Dim UnCheckCount As Integer = 0
        Dim CheckCount As Integer = 0
        Dim DcEurOk As Decimal = 0
        Dim DcEurKo As Decimal = 0

        Dim oGrid As DataGridView = oParentRow.DataGridView
        For i As Integer = oParentRow.Index + 1 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(Cols.Yea).Value = oParentRow.Cells(Cols.Yea).Value And DataGridView1.Rows(i).Cells(Cols.Id).Value = oParentRow.Cells(Cols.Id).Value Then
                If CType(DataGridView1.Rows(i).Cells(Cols.ChkVal).Value, Chks) = Chks.Checked Then
                    CheckCount += 1
                    DcEurOk += DataGridView1.Rows(i).Cells(Cols.Amt).Value
                Else
                    UnCheckCount += 1
                    DcEurKo += DataGridView1.Rows(i).Cells(Cols.Amt).Value
                End If
            Else
                Exit For
            End If
        Next


        If CheckCount = 0 Then
            oChkStatus = Chks.Unchecked
            oParentRow.Cells(Cols.Amt).Value = DcEurKo
        ElseIf UnCheckCount = 0 Then
            oChkStatus = Chks.Checked
            oParentRow.Cells(Cols.Amt).Value = DcEurOk
        Else
            oChkStatus = Chks.Grayed
            oParentRow.Cells(Cols.Amt).Value = DcEurOk
        End If

        oParentRow.Cells(Cols.ChkVal).Value = oChkStatus

    End Sub


    Private Sub SetChildrenStatus(ByVal oRowParent As DataGridViewRow, ByVal oStatus As Chks)
        For i As Integer = oRowParent.Index + 1 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(Cols.Yea).Value = oRowParent.Cells(Cols.Yea).Value And DataGridView1.Rows(i).Cells(Cols.Id).Value = oRowParent.Cells(Cols.Id).Value Then
                DataGridView1.Rows(i).Cells(Cols.ChkVal).Value = oStatus
            Else
                Exit For
            End If
        Next
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oImportacio As DTOImportacio = BLLImportacio.Find(CurrentImportacio.Guid)
        If oImportacio IsNot Nothing Then
            Dim oFrm As New Frm_Importacio(oImportacio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
        SetTotals()
    End Sub

    Private Sub ButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonXls.Click
        Dim oIntrastat As DTOIntrastat = BLLIntrastat.Find(_Intrastat.Guid)
        Dim oSheet As DTOExcelSheet = BLLIntrastat.ExcelImport(oIntrastat)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("retrocedim l'intrastat?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            _Intrastat.Delete()
            RaiseEvent AfterUpdate(Nothing, MatEventArgs.Empty)
            MsgBox("intrastat retrocedit", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub
End Class