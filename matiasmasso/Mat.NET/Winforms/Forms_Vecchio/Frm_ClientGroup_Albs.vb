
Imports System.Drawing

Public Class Frm_ClientGroup_Albs

    Private mDs As DataSet
    Private mClient As Client
    Private mEmp as DTOEmp
    Private mOcultaFacturats As Boolean = False
    Private mAllowEvents As Boolean

    Private Enum Cols
        Yea
        Id
        Fch
        Eur
        Ref
        Cash
        CashIco
        Transm
        Fra
        Trp
        Usr
        Cod
    End Enum

    Public Sub New(ByVal oClient As Client, Optional ByVal OcultaFacturats As Boolean = False)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oClient
        mEmp = mClient.Emp
        mOcultaFacturats = OcultaFacturats
        Me.Text = "ALBARANS DE SORTIDA PER " & mClient.Clx
        If mOcultaFacturats Then
            Me.Text = Me.Text & " PENDENTS DE FACTURAR"
        End If
        Refresca()
        mAllowEvents = True
    End Sub


    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ALB.YEA, ALB.ALB, ALB.FCH, ALB.EUR, CLICLIENT.REF, " _
        & "ALB.CASHCOD, Transm.Transm, Fra.FRA, TRP.ABR, " _
        & "(CASE WHEN Email.Nickname > '' THEN eMAIL.nICKNAME ELSE Email.adr END) AS USR, " _
        & "ALB.COD " _
        & "FROM Alb " _
        & "INNER JOIN CliClient ON ALB.CliGuid = CliClient.Guid " _
        & "LEFT OUTER JOIN Trp ON ALB.TrpGuid=Trp.Guid " _
        & "LEFT OUTER JOIN Transm ON ALB.TransmGuid=Transm.Guid " _
        & "LEFT OUTER JOIN Email ON ALB.UsrCreatedGuid=Email.Guid " _
        & "LEFT OUTER JOIN FRA ON ALB.FraGuid = Fra.Guid " _
        & "WHERE (ALB.CliGuid=@CliGuid OR CliClient.CcxGuid=@CliGuid) AND " _
        & "ALB.COD<>1 "
        If mOcultaFacturats Then
            SQL = SQL & " AND FraGuid IS NULL AND FACTURABLE=1 AND EUR<>0 "
        End If
        SQL = SQL & "ORDER BY ALB.YEA DESC, ALB.alb DESC"

        mDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@CliGuid", mClient.Guid.ToString)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oCol As DataColumn = oTb.Columns.Add("CASHICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.CashIco)
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
            With .Columns(Cols.Id)
                .HeaderText = "Comanda"
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
            With .Columns(Cols.Ref)
                .HeaderText = "Destinació"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Cash)
                .Visible = False
            End With
            With .Columns(Cols.CashIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Transm)
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
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
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
                    Case DTO.DTOCustomer.CashCodes.credit
                        e.Value = My.Resources.empty
                End Select
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
        Dim oFrm As New Frm_AlbNew2(CurrentAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
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
            Case DTOPurchaseOrder.Codis.reparacio
                PaintGradientRowBackGround(e, Color.Pink)
            Case DTOPurchaseOrder.Codis.traspas
                PaintGradientRowBackGround(e, Me.BackColor)
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

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

End Class