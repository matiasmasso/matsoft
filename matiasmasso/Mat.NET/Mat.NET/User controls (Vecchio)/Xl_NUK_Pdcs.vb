

Public Class Xl_NUK_Pdcs
    Private mPdcs As Pdcs
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCod As Cods
    Private mErr As Roche.Errors = Errors.None
    Private mAllowEvents As Boolean = False

    Public Event SelectionChange(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Cods
        NotSet
        Totes
        Pendents
        Enviades
        PerConfirmar
        Parcials
        Validades
    End Enum

    Private Enum Cols
        Yea
        Pdc
        Fch
        Clx
        amt
        VatIncluded
    End Enum

    Public Sub New(ByVal oCod As Cods, Optional ByVal oErr As Roche.Errors = Errors.None, Optional ByVal oPdcs As Pdcs = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mCod = oCod
        mErr = oErr
        mPdcs = oPdcs
        LoadGrid()
        mAllowEvents = True
    End Sub

   

    Private Sub LoadGrid()
        Dim OldAllowEvents As Boolean = mAllowEvents
        mAllowEvents = False

        Dim oTb As New DataTable

        Select Case mCod
            Case Cods.Totes
                Dim SQL As String = "SELECT PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX FROM PDC INNER JOIN " _
                & "PNC ON PDC.Guid=PNC.PdcGuid INNER JOIN " _
                & "CLX ON PDC.EMP=CLX.EMP AND PDC.CLI=CLX.CLI " _
                & "WHERE Pdc.EMP=@EMP " _
                & "GROUP BY PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX " _
                & "ORDER BY PDC.YEA DESC, PDC.PDC DESC, PDC.FCH, CLX.CLX"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MGZ", Roche.Contact.Id)
                oTb = oDs.Tables(0)
            Case Cods.Pendents, Cods.Validades
                Dim oRow As DataRow = Nothing
                With oTb.Columns
                    .Add("YEA", System.Type.GetType("System.Int32"))
                    .Add("PDC", System.Type.GetType("System.Int32"))
                    .Add("FCH", System.Type.GetType("System.DateTime"))
                    .Add("CLX", System.Type.GetType("System.String"))
                    .Add("AMT", System.Type.GetType("System.Decimal"))
                    .Add("VATINCLUDED", System.Type.GetType("System.Decimal"))
                End With

                For Each oPdc As Pdc In mPdcs
                    oRow = oTb.NewRow
                    oRow(Cols.Yea) = oPdc.Yea
                    oRow(Cols.Pdc) = oPdc.Id
                    oRow(Cols.Fch) = oPdc.Fch
                    oRow(Cols.Clx) = oPdc.Client.Clx
                    oRow(Cols.amt) = oPdc.BaseImponible(True, Roche.Mgz).Eur
                    oRow(Cols.VatIncluded) = oPdc.TotalVatIncluded(True, Roche.Mgz).Eur
                    oTb.Rows.Add(oRow)
                Next
            Case Cods.Enviades
                Dim SQL As String = "SELECT PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX FROM PDC INNER JOIN " _
                & "PNC ON PDC.Guid=PNC.PdcGuid INNER JOIN " _
                & "CLX ON PDC.EMP=CLX.EMP AND PDC.CLI=CLX.CLI " _
                & "WHERE Pdc.EMP=@EMP AND NOT PDCCONFIRM IS NULL " _
                & "GROUP BY PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX " _
                & "ORDER BY PDC.YEA DESC, PDC.PDC DESC, PDC.FCH, CLX.CLX"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MGZ", Roche.Contact.Id)
                oTb = oDs.Tables(0)
            Case Cods.PerConfirmar
                Dim SQL As String = "SELECT PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX FROM PDC INNER JOIN " _
                & "PNC ON PDC.Guid=PNC.PdcGuid INNER JOIN " _
                & "CLX ON PDC.EMP=CLX.EMP AND PDC.CLI=CLX.CLI LEFT OUTER JOIN " _
                & "ARC ON PNC.PdcGuid = ARC.PdcGuid AND PNC.lin = ARC.ln2 " _
                & "WHERE PNC.PdcConfirm IS NOT NULL AND Pdc.Emp =@EMP AND PNC.COD=2 " _
                & "GROUP BY PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX " _
                & "HAVING(COUNT(ARC.ln2)=0) " _
                & "ORDER BY PDC.yea DESC, PDC.pdc DESC"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MGZ", Roche.Contact.Id)
                oTb = oDs.Tables(0)
            Case Cods.Parcials
                Dim SQL As String = "SELECT PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX FROM PDC INNER JOIN " _
                & "PNC ON PDC.Guid=PNC.PdcGuid INNER JOIN " _
                & "CLX ON PDC.EMP=CLX.EMP AND PDC.CLI=CLX.CLI LEFT OUTER JOIN " _
                & "ARC ON PNC.PdcGuid = ARC.PdcGuid AND PNC.lin = ARC.ln2 " _
                & "WHERE PNC.PdcConfirm IS NOT NULL AND Pdc.Emp =@EMP AND PNC.COD=2 " _
                & "GROUP BY PDC.YEA, PDC.PDC, PDC.FCH, CLX.CLX " _
                & "HAVING(COUNT(PNC.lin) <> COUNT(ARC.ln2)) " _
                & "ORDER BY PDC.yea DESC, PDC.pdc DESC"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MGZ", Roche.Contact.Id)
                oTb = oDs.Tables(0)
        End Select

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
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "comanda"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "destinatari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            If mCod = Cods.Pendents Then
                With .Columns(Cols.amt)
                    .HeaderText = "Import"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.VatIncluded)
                    .HeaderText = "IVA incl."
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End If
        End With

        If oTb.Rows.Count > 0 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols.Clx)
            DataGridView1.CurrentRow.Selected = True
        End If

        SetContextMenu()
        mAllowEvents = OldAllowEvents

    End Sub


    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            Dim iPdc As Integer = DataGridView1.CurrentRow.Cells(Cols.Pdc).Value
            oPdc = Pdc.FromNum(mEmp, iYea, iPdc)
        End If
        Return oPdc
    End Function

    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer
            Dim iPdc As Integer
            Dim oPdc As Pdc = Nothing
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                iYea = oRow.Cells(Cols.Yea).Value
                iPdc = oRow.Cells(Cols.Pdc).Value
                oPdc = Pdc.FromNum(mEmp, iYea, iPdc)
                oPdcs.Add(oPdc)
            Next
        Else
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oPdcs.Add(CurrentPdc)
            End If
        End If
        Return oPdcs
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oPdcs As Pdcs = CurrentPdcs()
        If oPdcs.Count = 1 Then
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oMenuItem = New ToolStripMenuItem("comanda")
                oContextMenu.Items.Add(oMenuItem)

                Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)
            End If
        End If

        If mCod = Cods.Pendents Then
            Select Case mErr
                Case Errors.Transferencia_previa
                    oMenuItem = New ToolStripMenuItem("avisat", My.Resources.tel, AddressOf AvisarTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
                Case Errors.Transferencia_avisats
                    oMenuItem = New ToolStripMenuItem("retrocedir avis", My.Resources.UNDO, AddressOf RetrocedirAvisTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("cobrar", My.Resources.DollarOrange2, AddressOf CobrarTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
            End Select

        End If

        If mCod = Cods.Validades Then
            oMenuItem = New ToolStripMenuItem("enviar", Nothing, AddressOf SendToNuk)
            oContextMenu.Items.Add(oMenuItem)

        End If

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf DoExcel)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        LoadGrid()
        DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
        mAllowEvents = True

        If i > DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oPdc As Pdc = CurrentPdc()
        'Dim oFrm As New Frm_Pdc_Client(oPdc)
        'oFrm.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
            RaiseEvent SelectionChange(CurrentPdc, EventArgs.Empty)
        End If
    End Sub

    Private Sub DoExcel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub


    Private Sub RetrocedirAvisTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim sTit As String = "RETROCEDIR AVIS DE TRANSFERENCIA COMANDA " & oPdc.Id & " A " & oPdc.Client.Clx
        Dim sObs As String = InputBox("Observacions:", sTit, oPdc.Obs)

        oPdc.UpdateCashStatus(Pdc.CashStatuss.Pendent_de_avisar, sObs)
        RefreshRequest(Nothing, EventArgs.Empty)
    End Sub

    Private Sub AvisarTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim sTit As String = "AVIS DE TRANSFERENCIA COMANDA " & oPdc.Id & " A " & oPdc.Client.Clx
        Dim sObs As String = InputBox("Observacions:", sTit, oPdc.Obs)

        oPdc.UpdateCashStatus(Pdc.CashStatuss.avisat, sObs)
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
    End Sub

    Private Sub CobrarTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim oFrm As New Frm_Cobrament_TransferenciaPrevia(oPdc)
        AddHandler oFrm.AfterUpdate, AddressOf AfterCobroTransferencia
        oFrm.Show()
    End Sub

    Private Sub AfterCobroTransferencia(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As Frm_Cobrament_TransferenciaPrevia = sender
        Dim oPdc As Pdc = oFrm.pdc
        oPdc.UpdateCashStatus(Pdc.CashStatuss.cobrat)
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
    End Sub

    Private Sub SendToNuk()
        Dim exs as new list(Of Exception)
        Roche.Deliver( exs)
    End Sub
End Class
