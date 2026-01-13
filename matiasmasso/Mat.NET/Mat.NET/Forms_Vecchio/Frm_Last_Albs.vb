
Imports System.Drawing

Public Class Frm_Last_Albs
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsYeas As DataSet
    Private mAllowEvents As Boolean
    Private mCod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet

    Private Enum Cols
        Guid
        Id
        Fch
        Clx
        Eur
        Cash
        CashIco
        Facturable
        FacturableIco
        Transm
        Fra
        Usr
        Cod
        Trp
        RetencioCod
    End Enum

    Private Sub Frm_Last_Albs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetYeas()
        Refresca()
        EnableYeaButtons()
        mAllowEvents = True
    End Sub

    Public WriteOnly Property Cod() As DTOPurchaseOrder.Codis
        Set(ByVal value As DTOPurchaseOrder.Codis)
            mCod = value
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Alb.Guid,Alb.ALB,Alb.FCH,Clx.CLX,EUR+PT2,CASHCOD,FACTURABLE, (CASE WHEN TRANSM=0 THEN '' ELSE CAST(TRANSM AS VARCHAR) END) AS STRANSM, FRA ")
        'sb.AppendLine(",(CASE WHEN USR.LOGIN IS NULL THEN (CASE WHEN ALB.USRCREATED=ALB.CLI THEN '(client)' ELSE CAST(ALB.USRCREATED AS VARCHAR) END) ELSE USR.LOGIN END) AS USR ")
        sb.AppendLine(", (CASE WHEN Email.Nickname IS NULL THEN (CASE WHEN Email.adr IS NULL THEN '' ELSE Email.Adr END) ELSE Email.Nickname END) AS USR ")
        sb.AppendLine(",COD,TRP.ABR, ALB.RETENCIOCOD ")
        sb.AppendLine("FROM alb ")
        sb.AppendLine("INNER JOIN CLX ON ALB.CliGuid=CLX.Guid ")
        sb.AppendLine("LEFT JOIN TRP ON ALB.TrpGuid=TRP.Guid ")
        'sb.AppendLine("LEFT OUTER JOIN EMPUSR ON ALB.UsrCreatedGuid=EMPUSR.ContactGuid ")
        'sb.AppendLine("LEFT OUTER JOIN USR ON EmpUsr.UsrGuid = Usr.Guid ")
        sb.AppendLine("LEFT OUTER JOIN EMAIL ON ALB.UsrCreatedGuid= Email.Guid ")
        sb.AppendLine("WHERE ALB.EMP=@Emp ")
        sb.AppendLine("AND ALB.YEA=@Yea ")
        If mCod <> DTOPurchaseOrder.Codis.NotSet Then
            sb.AppendLine("AND ALB.COD=" & CInt(mCod) & " ")
        End If
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
            Case Else
                sb.AppendLine("AND ALB.COD<>" & DTOPurchaseOrder.Codis.Proveidor & " ")
        End Select
        sb.AppendLine("ORDER BY ALB DESC")

        Dim SQL As String = sb.ToString
        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Emp", App.Current.emp.Id, "@Yea", CurrentYea)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("CASHICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.CashIco)

        Dim oCol2 As DataColumn = oTb.Columns.Add("FacturableICO", System.Type.GetType("System.Byte[]"))
        oCol2.SetOrdinal(Cols.FacturableIco)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Albará"
                .Width = 45
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
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cash)
                .Visible = False
            End With
            With .Columns(Cols.CashIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Facturable)
                .Visible = False
            End With
            With .Columns(Cols.FacturableIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Transm)
                .HeaderText = "Transm"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Trp)
                .HeaderText = "Transport"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Cod)
                .Visible = False
            End With
            With .Columns(Cols.RetencioCod)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub SetYeas()
        Dim SQL As String = "SELECT YEA FROM ALB " _
        & "WHERE EMP=" & mEmp.Id & " " _
        & "GROUP BY YEA " _
        & "ORDER BY YEA DESC"

        mDsYeas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsYeas.Tables(0)
        Dim oRow As DataRow
        With ToolStripComboBoxYea
            .BeginUpdate()
            If oTb.Rows.Count = 0 Then
                .Items.Add(Today.Year)
            Else
                For Each oRow In oTb.Rows
                    .Items.Add(oRow("YEA"))
                Next
            End If
            .EndUpdate()
            .SelectedIndex = 0
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            Dim iNum As Integer = oRow.Cells(Cols.Id).Value
            oAlb = New Alb(oGuid)
            oAlb.Yea = CurrentYea()
            oAlb.Id = oRow.Cells(Cols.Id).Value
            oAlb.Fch = CDate(oRow.Cells(Cols.Fch).Value)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim retval As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oAlb As New Alb(oGuid)
                oAlb.Yea = CurrentYea()
                oAlb.Id = oRow.Cells(Cols.Id).Value
                oAlb.Fch = CDate(oRow.Cells(Cols.Fch).Value)
                retval.Add(oAlb)
            Next
            retval.Sort()
        Else
            Dim oAlb As Alb = CurrentAlb()
            If oAlb IsNot Nothing Then
                retval.Add(CurrentAlb)
            End If
        End If
        Return retval
    End Function

    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlbs As Albs = CurrentAlbs()

        If oAlbs.Count > 0 Then
            Dim oMenu_Alb As New Menu_Alb(oAlbs)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Alb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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



    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Transm
                If e.Value = "" Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Select Case CType(oRow.Cells(Cols.RetencioCod).Value, DTODelivery.RetencioCods)
                        Case DTODelivery.RetencioCods.Transferencia
                            e.Value = "TRANSF"
                            e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                        Case DTODelivery.RetencioCods.VISA
                            e.Value = "VISA"
                            e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                    End Select
                End If
            Case Cols.CashIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.Cash).Value, DTO.DTOCustomer.CashCodes)
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        e.Value = My.Resources.DollarBlue
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa
                        e.Value = My.Resources.DollarOrange2
                    Case Else
                        e.Value = My.Resources.empty
                End Select
            Case Cols.FacturableIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If CBool(oRow.Cells(Cols.Facturable).Value) Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.NoPark
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowAlb()
    End Sub

    Private Sub ShowAlb()
        Dim oFrm As New Frm_AlbNew2(CurrentAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowAlb()
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Cod).Value, DTOPurchaseOrder.Codis)
        Select Case oCod
            Case DTOPurchaseOrder.Codis.client
                Dim DblEur As Decimal = oRow.Cells(Cols.Eur).Value
                If DblEur < 0 Then
                    PaintGradientRowBackGround(e, Color.LightSalmon)
                Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            Case DTOPurchaseOrder.Codis.proveidor
                PaintGradientRowBackGround(e, Color.GreenYellow)
                'oRow.DefaultCellStyle.BackColor = Color.GreenYellow
            Case DTOPurchaseOrder.Codis.reparacio
                PaintGradientRowBackGround(e, Color.Pink)
                'oRow.DefaultCellStyle.BackColor = Color.LightPink
            Case DTOPurchaseOrder.Codis.traspas
                PaintGradientRowBackGround(e, Me.BackColor)
                'oRow.DefaultCellStyle.BackColor = Me.BackColor
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            EnableYeaButtons()
            Refresca()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub
End Class