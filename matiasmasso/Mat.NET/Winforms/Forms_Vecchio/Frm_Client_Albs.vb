
Imports System.Drawing

Public Class Frm_Client_Albs
    Private _Contact As DTOContact

    Private mDs As DataSet
    Private mClient As Client
    Private mEmp As DTOEmp
    Private mLastMouseDownRectangle As System.Drawing.Rectangle
    Private mAllowEvents As Boolean

    Private Enum Cols
        Yea
        transm
        Id
        Fch
        Eur
        Cash
        CashIco
        Justificante
        JustificanteIco
        FchJustificante
        Fra
        Trp
        Usr
        Cod
    End Enum

    Public Sub New(oContact As DTOContact)
        MyBase.New
        ' This call is required by the designer.
        InitializeComponent()
        _Contact = oContact
        BLLContact.Load(oContact)
        Me.Text = String.Format("Albarans de sortida per {0}", oContact.FullNom)
        Refresca()
    End Sub

    Public Sub New()
        MyBase.New
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    Public WriteOnly Property Client() As Client
        Set(ByVal Value As Client)
            mClient = Value
            mEmp = mClient.Emp
            _Contact = BLLContact.Find(mClient.Guid)

            Me.Text = "ALBARANS DE SORTIDA PER " & mClient.Clx
            Refresca()
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property ToBeInvoicedOnly() As Boolean
        Set(ByVal value As Boolean)
            CheckBoxHideInvoiced.Checked = value
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ALB.YEA, (CASE WHEN Transm.Transm IS NULL THEN 0 ELSE Transm.Transm END) AS TransmId, ALB.ALB, ALB.FCH, ALB.EUR+PT2," _
        & "ALB.CASHCOD, ALB.JUSTIFICANTE,ALB.FCHJUSTIFICANTE, fra.FRA, TRP.ABR, " _
        & "(CASE WHEN Email.Nickname IS NULL THEN Email.Adr ELSE Email.Nickname END) AS USR, " _
        & "ALB.COD " _
        & "FROM Alb " _
        & "LEFT OUTER JOIN TRP ON ALB.TrpGuid=TRP.Guid " _
        & "LEFT OUTER JOIN Fra ON ALB.FraGuid=Fra.Guid " _
        & "LEFT OUTER JOIN Transm ON ALB.TransmGuid=Transm.Guid " _
        & "LEFT OUTER JOIN Email ON ALB.UsrCreatedGuid=Email.Guid " _
        & "WHERE ALB.CliGuid='" & _Contact.Guid.ToString & "' AND " _
        & "ALB.COD<>1 "

        ' "LEFT OUTER JOIN USR ON EmpUsr.UsrGuid = Usr.Guid " _


        If CheckBoxHideInvoiced.Checked Then
            SQL = SQL & " AND FACTURABLE=1 AND Fra.Guid IS NULL "
        End If
        SQL = SQL & "ORDER BY ALB.YEA DESC, ALB.alb DESC"

        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oCol As DataColumn = oTb.Columns.Add("CASHICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.CashIco)
        Dim oCol2 As DataColumn = oTb.Columns.Add("JUSTIFICANTEICO", System.Type.GetType("System.Byte[]"))
        oCol2.SetOrdinal(Cols.JustificanteIco)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.transm)
                .HeaderText = "transm"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Id)
                .HeaderText = "albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
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
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
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
            With .Columns(Cols.Justificante)
                .Visible = False
            End With
            With .Columns(Cols.FchJustificante)
                .Visible = False
            End With
            With .Columns(Cols.JustificanteIco)
                .HeaderText = "js"
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.transm)
                .HeaderText = "Transm"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Trp)
                .HeaderText = "Transport"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Cod)
                .Visible = False
            End With
        End With
    End Sub

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            oAlb = MaxiSrvr.Alb.FromNum(BLL.BLLApp.Emp, iYea, LngId)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim oAlbs As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim IntYea As Integer
            Dim LngId As Integer
            Dim oAlb As Alb
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                LngId = oRow.Cells(Cols.Id).Value
                IntYea = oRow.Cells(Cols.Yea).Value
                oAlb = MaxiSrvr.Alb.FromNum(mEmp, IntYea, LngId)
                oAlbs.Add(oAlb)
            Next
            oAlbs.Sort()
        Else
            Dim oAlb As Alb = CurrentAlb()
            If oAlb IsNot Nothing Then
                oAlbs.Add(CurrentAlb)
            End If
        End If
        Return oAlbs
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlbs As Albs = CurrentAlbs()
        Dim oDeliveries As New List(Of DTODelivery)
        For Each oAlb As Alb In oAlbs
            Dim oDelivery As New DTODelivery(oAlb.Guid)
            oDelivery.Id = oAlb.Id
            oDeliveries.Add(oDelivery)
        Next

        If oDeliveries.Count > 0 Then
            Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
            AddHandler oMenu_Delivery.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Delivery.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        Refresca()

        DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
        mAllowEvents = True

        If i > DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
        End If
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.CashIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Cod).Value, DTOPurchaseOrder.Codis)
                Select Case CType(oRow.Cells(Cols.Cash).Value, DTO.DTOCustomer.CashCodes)
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        e.Value = My.Resources.DollarBlue
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa
                        e.Value = My.Resources.DollarOrange2
                    Case DTO.DTOCustomer.CashCodes.credit, DTO.DTOCustomer.CashCodes.Diposit
                        e.Value = My.Resources.empty
                End Select
            Case Cols.JustificanteIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As Alb.JustificanteCodes = CType(oRow.Cells(Cols.Justificante).Value, Alb.JustificanteCodes)
                Select Case oCod
                    Case Alb.JustificanteCodes.None
                        e.Value = My.Resources.empty
                    Case Alb.JustificanteCodes.Solicitado
                        e.Value = My.Resources.Outlook_16
                    Case Alb.JustificanteCodes.Recibido
                        e.Value = My.Resources.Ok
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        Select Case e.ColumnIndex
            Case Cols.CashIco
                If e.RowIndex >= 0 And e.RowIndex < DataGridView1.Rows.Count Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    If oRow IsNot Nothing Then
                        Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Cod).Value, DTOPurchaseOrder.Codis)
                        Select Case CType(oRow.Cells(Cols.Cash).Value, DTO.DTOCustomer.CashCodes)
                            Case DTO.DTOCustomer.CashCodes.Reembols
                                e.ToolTipText = "contra reembolsament"
                            Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                                e.ToolTipText = "transferencia previa"
                            Case DTO.DTOCustomer.CashCodes.Visa
                                e.ToolTipText = "tarja de crèdit"
                            Case DTO.DTOCustomer.CashCodes.credit
                                e.ToolTipText = "enviat a crèdit"
                        End Select
                    End If
                End If
            Case Cols.JustificanteIco
                If e.RowIndex >= 0 And e.RowIndex < DataGridView1.Rows.Count Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    If oRow IsNot Nothing Then
                        Dim oCod As Alb.JustificanteCodes = CType(oRow.Cells(Cols.Justificante).Value, Alb.JustificanteCodes)
                        Select Case oCod
                            Case Alb.JustificanteCodes.None
                            Case Alb.JustificanteCodes.Solicitado
                                Dim DtFch As DateTime = oRow.Cells(Cols.FchJustificante).Value
                                e.ToolTipText = "justificant demanat en data " & DtFch.ToShortDateString
                            Case Alb.JustificanteCodes.Recibido
                                Dim DtFch As DateTime = oRow.Cells(Cols.FchJustificante).Value
                                e.ToolTipText = "justificant rebut en data " & DtFch.ToShortDateString
                        End Select
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowAlb()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowAlb()
            e.Handled = True
        End If
    End Sub

    Private Sub ShowAlb()
        Dim oAlb As Alb = CurrentAlb()
        Dim oFrm As New Frm_AlbNew2(oAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub


    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    DataGridView1.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                    Dim oAlb As Alb = MaxiSrvr.Alb.FromNum(mEmp, CInt(oRow.Cells(Cols.Yea).Value), CLng(oRow.Cells(Cols.Id).Value))
                    sender.DoDragDrop(oAlb, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Cod).Value, DTOPurchaseOrder.Codis)
        Select Case oCod
            Case DTOPurchaseOrder.Codis.Client
                Dim DblEur As Decimal = oRow.Cells(Cols.Eur).Value
                If DblEur < 0 Then
                    PaintGradientRowBackGround(e, Color.LightSalmon)
                Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            Case DTOPurchaseOrder.Codis.Proveidor
                PaintGradientRowBackGround(e, Color.GreenYellow)
            Case DTOPurchaseOrder.Codis.Reparacio
                PaintGradientRowBackGround(e, Color.Pink)
            Case DTOPurchaseOrder.Codis.Traspas
                PaintGradientRowBackGround(e, Me.BackColor)
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle(
            0, e.RowBounds.Top,
            Me.DataGridView1.Columns.GetColumnsWidth(
            DataGridViewElementStates.Visible) -
            Me.DataGridView1.HorizontalScrollingOffset + 1,
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush(
        rowBounds,
        oColor,
        oBgColor,
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub

    Private Sub CheckBoxHideInvoiced_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxHideInvoiced.Click
        If mClient IsNot Nothing Then
            Refresca()
        End If
    End Sub
End Class