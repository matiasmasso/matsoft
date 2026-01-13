

Public Class Xl_NUK_Albs
    Private mAllowEvents As Boolean
    Private mRoche As contact = roche.contact

    Public Event SelectionChange(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Warn
        IcoWarn
        YeaP
        AlbP
        Desadv
        Fch
        Fra
        YeaC
        AlbC
        Clx
        CashCod
        IcoCash
        Eur
    End Enum

    Public Sub New()
        MyBase.new()
        InitializeComponent()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT 0 AS WARN, ALBP.YEA AS YEAP, ALBP.alb AS PALB, " _
        & "(CASE WHEN ALBP.DelegatNum IS NULL THEN ALBC.DelegatNum ELSE ALBP.DelegatNum END) AS DESADV, ALBP.fch, " _
        & "(CASE WHEN FRAPRV.Num IS NULL THEN 0 ELSE FRAPRV.Num END) AS FRA, " _
        & "ALBC.YEA AS YEAC, ALBC.alb AS CALB, CLX.clx, ALBC.CashCod, ALBC.eur + ALBC.Pt2 As DTOAmt " _
        & "FROM CLX INNER JOIN " _
        & "ALB AS ALBC ON CLX.Emp = ALBC.Emp AND CLX.cli = ALBC.cli AND ALBC.Cod = 2 FULL OUTER JOIN " _
        & "ALB AS ALBP ON ALBC.Emp = ALBP.Emp AND ALBC.DelegatNum = ALBP.DelegatNum AND ALBC.DelegatContact = ALBP.DelegatContact AND ALBP.Cod = 1 LEFT OUTER JOIN " _
        & "FRAPRVALBS ON FRAPRVALBS.AlbGuid = ALBP.Guid LEFT OUTER JOIN " _
        & "FRAPRV ON FRAPRVALBS.FraGuid = FRAPRV.Guid " _
        & "WHERE ALBC.DelegatContact =@ROCHE AND ALBC.Emp =@EMP " _
        & "ORDER BY DESADV DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mRoche.Emp.Id, "@ROCHE", mRoche.Id)
        Dim oTb As DataTable = ods.tables(0)

        Dim oColIcoWarn As DataColumn = oTb.Columns.Add("WARNICO", System.Type.GetType("System.Byte[]"))
        oColIcoWarn.SetOrdinal(Cols.IcoWarn)

        Dim oColIcoCash As DataColumn = oTb.Columns.Add("CASHICO", System.Type.GetType("System.Byte[]"))
        oColIcoCash.SetOrdinal(Cols.IcoCash)

        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Warn)
                .Visible = False
            End With
            With .Columns(Cols.IcoWarn)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.YeaP)
                .Visible = False
            End With
            With .Columns(Cols.AlbP)
                Select Case BLL.BLLSession.Current.User.Rol.id
                    Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.Accounts
                        .HeaderText = "entrada"
                        .Width = 60
                    Case Else
                        .Visible = False
                End Select
            End With
            With .Columns(Cols.Desadv)
                .HeaderText = "desadv"
                .Width = 70
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 70
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.YeaC)
                .Visible = False
            End With
            With .Columns(Cols.AlbC)
                .HeaderText = "sortida"
                .Width = 60
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.CashCod)
                .Visible = False
            End With
            With .Columns(Cols.IcoCash)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = BlOldEvents

        SetContextMenu()
        RaiseEvent SelectionChange(CurrentAlb, EventArgs.Empty)
    End Sub

    Private Function CurrentRow() As DataGridViewRow
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Return oRow
    End Function

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = CurrentAlb(DTOPurchaseOrder.Codis.proveidor)
        If oAlb Is Nothing Then oAlb = CurrentAlb(DTOPurchaseOrder.Codis.client)
        Return oAlb
    End Function

    Private Function CurrentAlb(ByVal oCod As DTOPurchaseOrder.Codis) As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer
            Dim iAlb As Integer
            If oCod = DTOPurchaseOrder.Codis.client Then
                iYea = oRow.Cells(Cols.YeaC).Value
                iAlb = oRow.Cells(Cols.AlbC).Value
            Else
                If Not IsDBNull(oRow.Cells(Cols.AlbP).Value) Then
                    iYea = oRow.Cells(Cols.YeaP).Value
                    iAlb = oRow.Cells(Cols.AlbP).Value
                End If
            End If
            oAlb = MaxiSrvr.Alb.FromNum(mRoche.Emp, iYea, iAlb)
        End If
        Return oAlb
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Select Case BLL.BLLSession.Current.User.Rol.id
                Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.Accounts
                    oMenuItem = New ToolStripMenuItem("entrada", My.Resources.candau, AddressOf ShowEntrada)
                    oMenuItem.Enabled = Not IsDBNull(CurrentRow.Cells(Cols.AlbP).Value)
                    oContextMenu.Items.Add(oMenuItem)
            End Select

            oMenuItem = New ToolStripMenuItem("sortida", Nothing, AddressOf ShowSortida)
            oMenuItem.Enabled = Not IsDBNull(CurrentRow.Cells(Cols.AlbC).Value)
            oContextMenu.Items.Add(oMenuItem)

            If BLL.BLLSession.Current.User.Rol.id = Rol.Ids.SuperUser Then
                oMenuItem = New ToolStripMenuItem("redacta entrada", My.Resources.candau, AddressOf RedactaEntrada)
                oMenuItem.Enabled = IsDBNull(CurrentRow.Cells(Cols.AlbP).Value)
                oContextMenu.Items.Add(oMenuItem)
            End If
        End If

        oMenuItem = New ToolStripMenuItem("refresca", Nothing, AddressOf LoadGrid)
        oContextMenu.Items.Add(oMenuItem)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub ShowEntrada(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim oAlb As Alb = CurrentAlb(DTOPurchaseOrder.Codis.proveidor)
            Dim oFrm As New Frm_AlbNew2(oAlb)
            oFrm.Show()
        End If
    End Sub

    Private Sub ShowSortida(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim oAlb As Alb = CurrentAlb(DTOPurchaseOrder.Codis.client)
            Dim oFrm As New Frm_AlbNew2(oAlb)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoWarn
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.AlbP).Value) Then
                    e.Value = My.Resources.warn
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.IcoCash
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.CashCod).Value, DTO.DTOCustomer.CashCodes)
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        e.Value = My.Resources.DollarBlue
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa
                        e.Value = My.Resources.DollarOrange2
                    Case DTO.DTOCustomer.CashCodes.credit
                        e.Value = My.Resources.empty
                End Select
        End Select

    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            Dim oAlb As Alb = CurrentAlb(DTOPurchaseOrder.Codis.proveidor)
            If oAlb Is Nothing Then oAlb = CurrentAlb(DTOPurchaseOrder.Codis.client)
            SetContextMenu()
            RaiseEvent SelectionChange(oAlb, EventArgs.Empty)
        End If
    End Sub

    Private Sub RedactaEntrada(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'comanda i albará a proveidor a partir de albará de client
        Dim oCliAlb As Alb = CurrentAlb(DTOPurchaseOrder.Codis.client)

        Dim sCliPdc As String = ""
        Try
            Dim oCliPdc As Pdc = oCliAlb.Itms(0).Pnc.Pdc
            sCliPdc = oCliPdc.Id.ToString & " "
        Catch ex As Exception
        End Try

        Dim oProveidor As New Proveidor(Roche.Contact.Guid)
        Dim oPdc As Pdc = oProveidor.NewPdc(oCliAlb.Fch, "via EDI per " & sCliPdc & " " & oCliAlb.Nom)
        oPdc.Source = DTOPurchaseorder.Sources.edi

        For Each oItm As LineItmArc In oCliAlb.Itms
            Dim oArt As Art = oItm.Art
            Dim oTarifa As DTOAmt = oItm.Art.Cost
            Dim oPncLin As New LineItmPnc(oPdc)
            With oPncLin
                .Qty = oItm.Qty
                .Art = oArt

                Dim oCliPnc As LineItmPnc = oItm.Pnc
                If (oCliPnc.Preu.Eur = 0 Or Not oCliPnc.Carrec) Then
                    .Preu = BLLApp.EmptyAmt
                Else
                    .Preu = oTarifa
                End If

            End With
            oPdc.Itms.Add(oPncLin)
        Next

        Dim exs as New List(Of exception)
        oPdc.Update(exs)


        '3.- graba entrada mercaderia
        Dim oRow As DataGridViewRow = CurrentRow()
        Dim sDesadv As String = oRow.Cells(Cols.Desadv).Value
        Dim oAlbPrv As Alb = oPdc.Deliver()
        oAlbPrv.SetUser(BLL.BLLSession.Current.User)

        With oAlbPrv
            .Fch = oCliAlb.Fch
            .DelegatNum = sDesadv
            .DelegatContact = Roche.Contact
            .Update(exs)
        End With

        RefreshRequest(sender, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Desadv
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
End Class
