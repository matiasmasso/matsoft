

Public Class Frm_Fra
    Private mFra As Fra
    Private mDs As New DataSet
    Private mFontAlb As New Font(Me.Font, FontStyle.Bold)
    Private mFontPdc As New Font(Me.Font, FontStyle.Italic)
    Private INDENT As String = New String(Chr(32), 4)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Codi
        AlbIdx
        ItmIdx
        text
        Qty
        Pvp
        Dto
        Amt
    End Enum

    Private Enum LineCodis
        Alb
        Pdc
        Spv
        Itm
    End Enum

    Public Sub New(oFra As Fra)
        MyBase.New()
        Me.InitializeComponent()
        mFra = oFra
        With mFra
            Me.Text = "FACTURA " & .Id
            Xl_Contact1.Contact = .Client
            DateTimePicker1.Value = .Fch
            TextBoxFpg.Text = .Fpg
            TextBoxOb1.Text = .Ob1
            TextBoxOb2.Text = .Ob2
            TextBoxOb3.Text = .Ob3
            'TextBoxPuntsQty.Text = .PuntsQty
            'TextBoxPuntsTipus.Text = .PuntsTipus
            'TextBoxPuntsBase.Text = .PuntsBase.CurFormat
            TextBoxSum.Text = .Suma.Formatted
            TextBoxDto.Text = .DtoPct & "%"
            TextBoxDpp.Text = .DppPct & "%"

            For Each oQuota As maxisrvr.IvaBaseQuota In .IvaBaseQuotas
                Select Case oQuota.Iva.Codi
                    Case DTO.DTOTax.Codis.Iva_Standard
                        TextBoxBaseIvaStd.Text = oQuota.Base.CurFormat
                        TextBoxIVApct.Text = oQuota.Iva.Tipus & "%"
                        TextBoxIVAStdeur.Text = oQuota.QuotaIva.CurFormat
                        If .Client.REQ Then
                            TextBoxReqPct.Text = oQuota.Iva.RecarrecEquivalencia.Tipus & "%"
                            TextBoxIVAReqEur.Text = oQuota.QuotaReq.CurFormat
                        End If
                    Case DTO.DTOTax.Codis.Iva_Reduit
                        TextBoxBaseIvaReduit.Text = oQuota.Base.CurFormat
                        TextBoxIVAredPct.Text = oQuota.Iva.Tipus & "%"
                        TextBoxIVAredEur.Text = oQuota.QuotaIva.CurFormat
                    Case DTO.DTOTax.Codis.Iva_SuperReduit
                        'TextBoxBaseIvaSuperReduit.Text = oQuota.Base.CurFormat
                End Select
            Next

            Dim DcTotal As Decimal = .Total.Val
            If DcTotal <> 0 Then TextBoxTot.Text = .Total.CurFormat
            .CalcTotals()
            If DcTotal <> .Total.Val Then
                PictureBoxWarn.Visible = True
                Dim oToolTip As New ToolTip()
                oToolTip.SetToolTip(PictureBoxWarn, "total " & .Total.CurFormat)
                ButtonOk.Enabled = True
            End If
        End With
        LoadGrid()

        Xl_FraRepComLiquidables1.Fra = mFra
    End Sub



    Private Sub LoadGrid()
        Dim oTb As New DataTable()
        mDs.Tables.Add(oTb)
        oTb.Columns.Add("CODI", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ALBIDX", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ITMIDX", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("TEXT", System.Type.GetType("System.String"))
        oTb.Columns.Add("QTY", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("PREU", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("DTO", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("AMT", System.Type.GetType("System.Decimal"))
        Dim AlbIdx As Integer
        Dim ItmIdx As Integer
        Dim oItm As LineItmArc

        Dim oLastPdc As New Pdc(mFra.Client.Emp, DTOPurchaseOrder.Codis.client)
        Dim oLastSpv As New Spv(System.Guid.NewGuid)


        For AlbIdx = 0 To mFra.Albs.Count - 1
            oTb.Rows.Add(GetDataRowFromAlb(AlbIdx))
            For ItmIdx = 0 To mFra.Albs(AlbIdx).Itms.Count - 1
                oItm = mFra.Albs(AlbIdx).Itms(ItmIdx)
                If oItm.Alb.Cod = DTOPurchaseOrder.Codis.client Then
                    If Not (oLastPdc.Equals(oItm.Pnc.Pdc)) Then
                        oLastPdc = oItm.Pnc.Pdc
                        If oLastPdc.Id > 0 Then
                            oTb.Rows.Add(GetDataRowFromPdc(AlbIdx, ItmIdx))
                        End If
                    End If
                End If
                If mFra.Albs(AlbIdx).Cod = DTOPurchaseOrder.Codis.reparacio Then
                    If Not (oLastSpv.Equals(oItm.Spv)) Then
                        oLastSpv = oItm.Spv
                        If oLastSpv.Id > 0 Then
                            SetDataRowsFromSpv(AlbIdx, ItmIdx)
                        End If
                    End If
                End If
                oTb.Rows.Add(GetDataRowFromItm(AlbIdx, ItmIdx))
            Next
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
            .AllowDrop = False

            With .Columns(Cols.Codi)
                .Visible = False
            End With
            With .Columns(Cols.AlbIdx)
                .Visible = False
            End With
            With .Columns(Cols.ItmIdx)
                .Visible = False
            End With

            With .Columns(Cols.text)
                .HeaderText = "concepte"
                .Width = 350
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Qty)
                .HeaderText = "quant."
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###;-#,###;#"
            End With
            With .Columns(Cols.Pvp)
                .HeaderText = "preu"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00;-#,###0.00;#"
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "dte."
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00;-#,###0.00;#"
            End With
        End With
    End Sub

    Private Function GetDataRowFromAlb(ByVal AlbIdx As Integer) As DataRow
        Dim oRow As DataRow = mDs.Tables(0).NewRow
        Dim oAlb As Alb = mFra.Albs(AlbIdx)

        oRow(Cols.Codi) = LineCodis.Alb
        oRow(Cols.AlbIdx) = AlbIdx
        oRow(Cols.ItmIdx) = 0
        oRow(Cols.Codi) = LineCodis.Alb
        oRow(Cols.text) = "albará " & oAlb.Id & " del " & oAlb.Fch
        oRow(Cols.Qty) = 0
        oRow(Cols.Pvp) = 0
        oRow(Cols.Dto) = 0
        oRow(Cols.Amt) = 0
        Return oRow
    End Function

    Private Function GetDataRowFromPdc(ByVal AlbIdx As Integer, ByVal ItmIdx As Integer) As DataRow
        Dim oRow As DataRow = mDs.Tables(0).NewRow
        Dim oAlb As Alb = mFra.Albs(AlbIdx)
        Dim oItm As LineItmArc = oAlb.Itms(ItmIdx)
        Dim oPdc As Pdc = oItm.Pnc.Pdc

        oRow(Cols.Codi) = LineCodis.Pdc
        oRow(Cols.AlbIdx) = AlbIdx
        oRow(Cols.ItmIdx) = ItmIdx
        oRow(Cols.text) = INDENT & "comanda " & oPdc.Text & " del " & oPdc.Fch & " (" & oPdc.Id & ")"
        oRow(Cols.Qty) = 0
        oRow(Cols.Pvp) = 0
        oRow(Cols.Dto) = 0
        oRow(Cols.Amt) = 0
        Return oRow
    End Function

    Private Sub SetDataRowsFromSpv(ByVal AlbIdx As Integer, ByVal ItmIdx As Integer)
        Dim oRow As DataRow = Nothing
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oAlb As Alb = mFra.Albs(AlbIdx)
        Dim oItm As LineItmArc = oAlb.Itms(ItmIdx)
        Dim oSpv = oItm.Spv



        Dim oSpvTextArray As ArrayList = oSpv.TextArray
        For i As Integer = 0 To oSpvTextArray.Count - 1
            oRow = oTb.NewRow
            oRow(Cols.Codi) = LineCodis.Spv
            oRow(Cols.AlbIdx) = AlbIdx
            oRow(Cols.ItmIdx) = ItmIdx
            If i = 0 Then
                oRow(Cols.text) = INDENT & oSpvTextArray(i).ToString.TrimEnd
            Else
                oRow(Cols.text) = INDENT & INDENT & oSpvTextArray(i).ToString.TrimEnd
            End If
            oRow(Cols.Qty) = 0
            oRow(Cols.Pvp) = 0
            oRow(Cols.Dto) = 0
            oRow(Cols.Amt) = 0
            oTb.Rows.Add(oRow)
        Next

    End Sub

    Private Function GetDataRowFromItm(ByVal AlbIdx As Integer, ByVal ItmIdx As Integer) As DataRow
        Dim oRow As DataRow = mDs.Tables(0).NewRow
        Dim oAlb As Alb = mFra.Albs(AlbIdx)
        Dim oItm As LineItmArc = oAlb.Itms(ItmIdx)
        oRow(Cols.Codi) = LineCodis.Itm
        oRow(Cols.AlbIdx) = AlbIdx
        oRow(Cols.ItmIdx) = ItmIdx
        oRow(Cols.text) = INDENT & INDENT & oItm.Art.Nom_ESP
        oRow(Cols.Qty) = oItm.Qty
        oRow(Cols.Pvp) = oItm.Preu.Val
        oRow(Cols.Dto) = oItm.Dto
        oRow(Cols.Amt) = oItm.Amt.Val
        Return oRow
    End Function


    Private Function CurrentCodi() As LineCodis
        Return DataGridView1.CurrentRow.Cells(Cols.Codi).Value
    End Function

    Private Function CurrentAlb() As Alb
        Dim AlbIdx As Integer = DataGridView1.CurrentRow.Cells(Cols.AlbIdx).Value
        Return mFra.Albs(AlbIdx)
    End Function

    Private Function CurrentItm() As LineItmArc
        Dim ItmIdx As Integer = DataGridView1.CurrentRow.Cells(Cols.ItmIdx).Value
        Return CurrentAlb.Itms(ItmIdx)
    End Function

    Private Function CurrentPdc() As Pdc
        Return CurrentItm.Pnc.Pdc
    End Function

    Private Function CurrentSpv() As spv
        Return CurrentItm.Spv
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        Select Case CurrentCodi()
            Case LineCodis.Alb
                oMenuItem = New ToolStripMenuItem("albará...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Alb As New Menu_Alb(CurrentAlb)
                AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Alb.Range)

            Case LineCodis.Pdc
                oMenuItem = New ToolStripMenuItem("albará...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Alb As New Menu_Alb(CurrentAlb)
                AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Alb.Range)

                oMenuItem = New ToolStripMenuItem("comanda...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Pdc As New Menu_Pdc(CurrentPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

            Case LineCodis.Spv
                oMenuItem = New ToolStripMenuItem("albará...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Alb As New Menu_Alb(CurrentAlb)
                AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Alb.Range)

                oMenuItem = New ToolStripMenuItem("comanda...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Pdc As New Menu_Pdc(CurrentPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

                oMenuItem = New ToolStripMenuItem("reparació...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Spv As New Menu_Spv(CurrentSpv)
                AddHandler oMenu_Spv.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Spv.Range)

            Case LineCodis.Itm
                oMenuItem = New ToolStripMenuItem("albará...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Alb As New Menu_Alb(CurrentAlb)
                AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Alb.Range)

                oMenuItem = New ToolStripMenuItem("comanda...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Pdc As New Menu_Pdc(CurrentPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

                oMenuItem = New ToolStripMenuItem("article...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Art As New Menu_Art(CurrentItm.Art)
                AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)
        End Select

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim Codi As LineCodis = CType(oRow.Cells(Cols.Codi).Value, LineCodis)
        Select Case Codi
            Case LineCodis.Alb
                e.CellStyle.Font = mFontAlb
            Case LineCodis.Pdc, LineCodis.Spv
                e.CellStyle.Font = mFontPdc
        End Select
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.text

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
            MsgBox("no hi han factures registrades!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case CurrentCodi()
            Case LineCodis.Alb
                Dim oFrm As New Frm_AlbNew2(CurrentAlb)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                With oFrm
                    .Show()
                End With

            Case LineCodis.Pdc
                Dim oItm As Pdc = CurrentPdc()
                If oItm IsNot Nothing Then
                    Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oItm.Guid)
                    Dim exs As New List(Of Exception)
                    If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                        UIHelper.WarnError(exs)
                    Else
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    End If
                End If

            Case LineCodis.Itm
                Dim oFrm As New Frm_Art(CurrentItm.Art)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ToolStripButtonPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonPdf.Click
        UIHelper.ShowStream(mFra.Cca.DocFile)
    End Sub


    Private Sub TextBoxIVAeur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxIVAStdeur.TextChanged

    End Sub
    Private Sub TextBoxIVApct_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxIVApct.TextChanged

    End Sub

    Private Sub ButtonCancel_Click1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        If mFra.Update( exs) Then
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar la factura")
        End If
    End Sub
End Class