Public Class Xl_Pdc_LineItems
    Private mPdc As Pdc = Nothing
    Private mMgz As DTOMgz = Nothing
    Private mFch As Date
    Private mAllowEvents As Boolean
    Private mNukTpaId As Integer = 104
    Private mTb As DataTable = Nothing
    Private mSkipMinPack As Boolean 'per quan partim la quantitat en dues linies de preus diferents
    Private mMenuItemEditPreus As New ToolStripMenuItem("editar preus i descomptes", Nothing, AddressOf Do_EditPreus)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        ArtId
        ArtGuid
        ArtNom
        Qty
        Ico
        Pvp
        SenseCarrec
        Dto
        Amt
        Pendent
        ArtStk
        ArtPn2
        ArtPrevisio
        Pack
        Rep
        Com
        PdcConfirm
        Promo
        Tpa
        Lin
    End Enum

    Public WriteOnly Property Fch As Date
        Set(ByVal value As Date)
            mFch = value
        End Set
    End Property

    Public WriteOnly Property Pdc() As Pdc
        Set(ByVal value As Pdc)
            mPdc = value
            If mPdc.Mgz Is Nothing Then
                mPdc.Mgz = BLL.BLLApp.Mgz
            Else
                If mPdc.Mgz.Guid.Equals(Guid.Empty) Then
                    mPdc.Mgz = BLL.BLLApp.Mgz
                End If
            End If
            mMgz = mPdc.Mgz

            Refresca()
        End Set
    End Property

    Public WriteOnly Property AllowEvents() As Boolean
        Set(ByVal value As Boolean)
            mAllowEvents = value
        End Set
    End Property

    Public ReadOnly Property Itms() As LineItmPncs
        Get
            Return GetItmsFromGrid()
        End Get
    End Property

    Public ReadOnly Property ExistPendents() As Boolean
        Get
            Dim RetVal As Boolean = False
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If Not IsDBNull(oRow.Cells(Cols.Pendent).Value) Then
                    If oRow.Cells(Cols.Pendent).Value <> 0 Then
                        RetVal = True
                    End If
                End If
            Next
            Return RetVal
        End Get
    End Property

    Private Sub Refresca()
        LoadGrid()
        ToolStripStatusLabelUsr.Text = BLLUser.LogText(mPdc.UsrCreated, mPdc.UsrLastEdited, mPdc.FchCreated, mPdc.FchLastEdited)
        CalculaTotals()
    End Sub

    Private Sub LoadGrid()
        mTb = GetTableFromPdc(mPdc)
        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.4 '1.3
            End With
            .DataSource = mTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = False
            .AllowUserToAddRows = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.ArtId)
                .Visible = False
            End With
            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "quant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 18 ' 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With
            With .Columns(Cols.Pvp)
                .HeaderText = "preu"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.SenseCarrec)
                .Visible = False
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "dte"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Pendent)
                If mPdc.Id = 0 Then
                    .Visible = False
                Else
                    .HeaderText = "pendent"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 50
                    .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .ReadOnly = True
                End If
            End With
            With .Columns(Cols.ArtStk)
                .HeaderText = "stock"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.ArtPn2)
                .HeaderText = "clients"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.ArtPrevisio)
                .HeaderText = "previsió"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns(Cols.Pack)
                .Visible = False
            End With
            With .Columns(Cols.Rep)
                .Visible = False
            End With
            With .Columns(Cols.Com)
                .Visible = False
            End With
            With .Columns(Cols.PdcConfirm)
                .Visible = False
            End With
            With .Columns(Cols.Promo)
                .Visible = False
            End With
            With .Columns(Cols.Tpa)
                .Visible = False
            End With
            With .Columns(Cols.Lin)
                .Visible = False
            End With
        End With
        mAllowEvents = BlOldEvents
    End Sub

    Private Function GetItmsFromGrid() As LineItmPncs
        Dim oItms As New LineItmPncs()
        Dim oItm As LineItmPnc = Nothing
        Dim iLastLineNumber As Integer = 0
        Dim iCurrentLineNumber As Integer = 0
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not oRow.IsNewRow Then

                iCurrentLineNumber = 0
                If Not IsDBNull(oRow.Cells(Cols.Lin).Value) Then
                    iCurrentLineNumber = oRow.Cells(Cols.Lin).Value
                End If

                If iCurrentLineNumber > 0 Then
                    iLastLineNumber = iCurrentLineNumber
                Else
                    iLastLineNumber += 1
                    iCurrentLineNumber = iLastLineNumber
                End If

                oItm = GetItmFromRow(oRow, iCurrentLineNumber)
                oItms.Add(oItm)
            End If
        Next
        Return oItms
    End Function

    Private Function GetItmFromRow(ByVal oRow As DataGridViewRow, Optional ByVal iCurrentLineNumber As Integer = 0) As LineItmPnc
        If iCurrentLineNumber = 0 Then
            If Not IsDBNull(oRow.Cells(Cols.Lin).Value) Then
                iCurrentLineNumber = oRow.Cells(Cols.Lin).Value
            End If
        End If

        Dim oItm As New LineItmPnc(CType(oRow.Cells(Cols.Guid).Value, Guid))
        With oItm
            .Lin = iCurrentLineNumber
            .Art = GetArtFromDataGridViewRow(oRow)
            If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
                .Qty = oRow.Cells(Cols.Qty).Value
            End If
            If Not IsDBNull(oRow.Cells(Cols.Pendent).Value) Then
                .Pendent = oRow.Cells(Cols.Pendent).Value
            End If
            If Not IsDBNull(oRow.Cells(Cols.Pvp).Value) Then
                .Preu = BLLApp.GetAmt(CDec(oRow.Cells(Cols.Pvp).Value))
            End If
            If Not IsDBNull(oRow.Cells(Cols.SenseCarrec).Value) Then
                .Carrec = Not oRow.Cells(Cols.SenseCarrec).Value
            End If
            If Not IsDBNull(oRow.Cells(Cols.Dto).Value) Then
                .dto = oRow.Cells(Cols.Dto).Value
            End If
            If Not IsDBNull(oRow.Cells(Cols.Rep).Value) Then
                If CType(oRow.Cells(Cols.Rep).Value, Guid).Equals(Guid.Empty) Then
                    .RepCom = Nothing
                Else
                    Dim oRep As New Rep(CType(oRow.Cells(Cols.Rep).Value, Guid))
                    Dim DcCom As Decimal = 0
                    If Not IsDBNull(oRow.Cells(Cols.Com).Value) Then
                        DcCom = oRow.Cells(Cols.Com).Value
                    End If
                    .RepCom = New RepCom(oRep, DcCom)
                End If
            Else
                .RepCom = Nothing
            End If
            If IsDBNull(oRow.Cells(Cols.PdcConfirm).Value) Then
                .PdcConfirm = Nothing
            Else
                .PdcConfirm = New PdcConfirm(New Guid(oRow.Cells(Cols.PdcConfirm).Value.ToString))
            End If

        End With
        Return oItm
    End Function

    Private Function GetArtFromDataGridViewRow(ByVal oRow As DataGridViewRow) As Art
        Dim retval As Art = Nothing
        If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
            Dim oGuid As Guid = oRow.Cells(Cols.ArtGuid).Value
            Dim ArtId As Integer = oRow.Cells(Cols.ArtId).Value
            Dim ArtNom As String = oRow.Cells(Cols.ArtNom).Value
            retval = New Art(oGuid)
            With retval
                .Emp = mPdc.Emp
                .Id = ArtId
                .Nom_ESP = ArtNom
            End With
        End If
        Return retval
    End Function

    Private Function GetTableFromPdc(ByVal oPdc As Pdc) As DataTable
        Dim oTb As DataTable = CreateTable()
        Dim oRow As DataRow = Nothing
        For Each oItm As LineItmPnc In oPdc.Itms
            oRow = oTb.NewRow
            FeedRowFromItm(oRow, oItm)
            oTb.Rows.Add(oRow)
        Next
        Return oTb
    End Function

    Private Sub FeedRowFromItm(ByRef oRow As DataRow, ByVal oItm As LineItmPnc)
        Dim oArt As Art = oItm.Art
        If oArt.Id > 0 Then
            Dim oDimensions As ArtDimensions = oArt.Dimensions.SelfOrInherited
            Dim iPack As Integer = IIf(oDimensions.InnerPack > 1 And oDimensions.ForzarInnerPack, oDimensions.InnerPack, 1)

            oRow(Cols.ArtId) = oArt.Id
            oRow(Cols.ArtGuid) = oArt.Guid
            oRow(Cols.ArtNom) = oArt.Nom_ESP
            oRow(Cols.Pack) = iPack
            oRow(Cols.Tpa) = oArt.Stp.Tpa.Id
        End If

        oRow(Cols.Guid) = oItm.Guid
        oRow(Cols.Qty) = oItm.Qty
        'oRow(Cols.Ico) = oDaus
        oRow(Cols.Pvp) = oItm.Preu.Eur
        oRow(Cols.SenseCarrec) = Not oItm.Carrec
        oRow(Cols.Dto) = oItm.dto
        oRow(Cols.Amt) = oItm.Amt.Eur
        oRow(Cols.Pendent) = oItm.Pendent
        oRow(Cols.ArtStk) = oArt.Stk
        oRow(Cols.ArtPn2) = oArt.Pn2
        oRow(Cols.ArtPrevisio) = oArt.PrevisionsText
        If oItm.RepCom IsNot Nothing Then
            oRow(Cols.Rep) = oItm.RepCom.Rep.Guid
            oRow(Cols.Com) = oItm.RepCom.ComisioPercent
        End If
        If oItm.PdcConfirm Is Nothing Then
            oRow(Cols.PdcConfirm) = System.Guid.Empty
        Else
            oRow(Cols.PdcConfirm) = oItm.PdcConfirm.Guid
        End If
        oRow(Cols.Lin) = oItm.Lin
    End Sub

    Private Sub FeedRowFromArt(ByRef oRow As DataGridViewRow, ByVal oArt As Art)
        If oArt Is Nothing Then
            oRow.Cells(Cols.ArtId).Value = 0
            oRow.Cells(Cols.ArtGuid).Value = System.DBNull.Value
            oRow.Cells(Cols.ArtNom).Value = ""
            oRow.Cells(Cols.Qty).Value = 0
            oRow.Cells(Cols.Pvp).Value = 0
            oRow.Cells(Cols.SenseCarrec).Value = False
            oRow.Cells(Cols.Dto).Value = 0
            oRow.Cells(Cols.Amt).Value = 0
            oRow.Cells(Cols.Pendent).Value = 0
            oRow.Cells(Cols.ArtStk).Value = 0
            oRow.Cells(Cols.ArtPn2).Value = 0
            oRow.Cells(Cols.ArtPrevisio).Value = ""
            oRow.Cells(Cols.Pack).Value = 0
            oRow.Cells(Cols.Rep).Value = System.Guid.Empty
            oRow.Cells(Cols.Com).Value = 0
            oRow.Cells(Cols.Tpa).Value = 0
            oRow.Cells(Cols.PdcConfirm).Value = System.Guid.Empty
        Else
            Dim oRepGuid As Guid
            Dim DcComisioPercent As Decimal = 0
            Dim oRepCom As RepCom = RepCom.FromCliProduct(mPdc.Client, oArt, mPdc.Fch)
            If oRepCom IsNot Nothing Then
                oRepGuid = oRepCom.Rep.Guid
                DcComisioPercent = oRepCom.ComisioPercent
            End If

            Dim oPreu As DTOAmt = GetPreu(oArt)
            Dim oDimensions As ArtDimensions = oArt.Dimensions.SelfOrInherited
            Dim iPack As Integer = IIf(oDimensions.InnerPack > 1 And oDimensions.ForzarInnerPack, oDimensions.InnerPack, 1)

            oRow.Cells(Cols.Guid).Value = Guid.NewGuid
            oRow.Cells(Cols.Rep).Value = oRepGuid
            oRow.Cells(Cols.Com).Value = DcComisioPercent
            oRow.Cells(Cols.ArtGuid).Value = oArt.Guid
            oRow.Cells(Cols.ArtId).Value = oArt.Id
            oRow.Cells(Cols.ArtNom).Value = oArt.Nom_ESP
            If oPreu IsNot Nothing Then
                oRow.Cells(Cols.Pvp).Value = oPreu.Val
            End If
            oRow.Cells(Cols.Dto).Value = mPdc.Client.CcxOrMe.DtoArt(oArt)
            oRow.Cells(Cols.SenseCarrec).Value = False
            oRow.Cells(Cols.ArtStk).Value = oArt.Stk(mMgz)
            oRow.Cells(Cols.ArtPn2).Value = oArt.Pn2
            oRow.Cells(Cols.ArtPrevisio).Value = oArt.PrevisionsText()
            oRow.Cells(Cols.Pack).Value = iPack
            oRow.Cells(Cols.Tpa).Value = oArt.Stp.Tpa.Id
            DataGridView1.Refresh()
        End If
        CalculaImports(oRow)
    End Sub

    Private Function GetPreu(ByVal oArt As Art) As DTOAmt
        Dim RetVal As DTOAmt = BLLApp.EmptyAmt
        Select Case mPdc.Cod
            Case DTO.DTOPurchaseOrder.Codis.Client
                RetVal = oArt.Tarifa(mPdc.Client.CcxOrMe, mFch)
            Case DTO.DTOPurchaseOrder.Codis.Proveidor
                RetVal = oArt.Cost
        End Select
        Return RetVal
    End Function

    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable()
        With oTb.Columns
            .Add("Guid", System.Type.GetType("System.Guid"))
            .Add("ARTID", System.Type.GetType("System.Int32"))
            .Add("ARTGUID", System.Type.GetType("System.Guid"))
            .Add("ARTNOM", System.Type.GetType("System.String"))
            .Add("QTY", System.Type.GetType("System.Int32"))
            .Add("ICO", System.Type.GetType("System.Byte[]"))
            .Add("PVP", System.Type.GetType("System.Decimal"))
            .Add("SENSECARREC", System.Type.GetType("System.Boolean"))
            .Add("DTO", System.Type.GetType("System.Decimal"))
            .Add("AMT", System.Type.GetType("System.Decimal"))
            .Add("PN2", System.Type.GetType("System.Int32"))
            .Add("ARTSTK", System.Type.GetType("System.Int32"))
            .Add("ARTPN2", System.Type.GetType("System.Int32"))
            .Add("ARTPREVISIO", System.Type.GetType("System.String"))
            .Add("PACK", System.Type.GetType("System.Int32"))
            .Add("REP", System.Type.GetType("System.Guid"))
            .Add("COM", System.Type.GetType("System.Decimal"))
            .Add("PDCCONFIRM", System.Type.GetType("System.String"))
            .Add("PROMO", System.Type.GetType("System.Guid"))
            .Add("TPA", System.Type.GetType("System.Int32"))
            .Add("LIN", System.Type.GetType("System.Int32"))
        End With
        Return oTb
    End Function


    Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        If mAllowEvents Then
            Dim dgc As DataGridViewCell = TryCast(DataGridView1.Item(e.ColumnIndex, e.RowIndex), DataGridViewCell)

            If dgc IsNot Nothing AndAlso dgc.ReadOnly Then
                Dim PreviousAllowEvents As Boolean = mAllowEvents
                mAllowEvents = False
                SendKeys.Send("{Tab}")
                mAllowEvents = PreviousAllowEvents
            End If
        End If
    End Sub

    Private Function CurrentLine() As LineItmPnc
        Dim oLine As LineItmPnc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                oLine = New LineItmPnc(CType(oRow.Cells(Cols.Guid).Value, Guid))
                oLine.SetItm()
                oLine.Art = oArt
                oLine.Qty = oRow.Cells(Cols.Qty).Value
            End If
        End If
        Return oLine
    End Function

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                oArt = GetArtFromDataGridViewRow(oRow)
            End If
        End If
        Return oArt
    End Function

    Private Function CurrentCarrec() As Boolean
        Dim RetVal As Boolean = True
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.SenseCarrec).Value) Then
                RetVal = Not oRow.Cells(Cols.SenseCarrec).Value
            End If
        End If
        Return RetVal
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Try
            Dim oItm As LineItmPnc = GetItmFromRow(DataGridView1.CurrentRow)
            If oItm.Art IsNot Nothing Then
                Dim oMenu_Art As New Menu_Art(oItm.Art, mMgz)
                oMenuItem = New ToolStripMenuItem("article...")
                oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)
                oContextMenu.Items.Add(oMenuItem)

                oMenuItem = New ToolStripMenuItem("representant")
                oContextMenu.Items.Add(oMenuItem)

                Dim s As String = ""
                If oItm.RepCom IsNot Nothing Then
                    s = String.Format("{0:P} {1}", oItm.RepCom.ComisioPercent / 100, oItm.RepCom.Rep.Abr)
                End If
                oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s))

                oMenuItem.DropDownItems.Add(New ToolStripMenuItem("reasignar comisions", Nothing, AddressOf Do_ResetReps))

            End If

            oMenuItem = New ToolStripMenuItem("sense carrec", Nothing, AddressOf Do_SenseCarrec)
            oMenuItem.Checked = Not CurrentCarrec()
            oContextMenu.Items.Add(oMenuItem)

            oContextMenu.Items.Add(mMenuItemEditPreus)

        Catch ex As Exception

        End Try


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ResetReps(sender As Object, e As System.EventArgs)
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                Dim oArt As Art = GetArtFromDataGridViewRow(oRow)

                Dim oRepGuid As Guid
                Dim DcRepComCom As Decimal = 0
                Dim sRepComAbr As String = ""
                Dim oRepCom As RepCom = RepCom.FromCliProduct(mPdc.Client, oArt, mPdc.Fch)
                If oRepCom IsNot Nothing Then
                    oRepGuid = oRepCom.Rep.Guid
                    DcRepComCom = oRepCom.ComisioPercent
                    sRepComAbr = oRepCom.Rep.Abr
                End If

                oRow.Cells(Cols.Rep).Value = oRepGuid
                oRow.Cells(Cols.Com).Value = DcRepComCom
                Dim s As String = String.Format("{0:P} {1} {2}", DcRepComCom / 100, sRepComAbr, oArt.Nom_ESP)
                If s.Length > 70 Then s = s.Substring(0, 70)
                sb.AppendLine(s)
            End If
        Next

        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        MsgBox(sb.ToString)
    End Sub


    Private Sub Do_EditPreus(ByVal sender As Object, ByVal e As System.EventArgs)
        mMenuItemEditPreus.Checked = Not mMenuItemEditPreus.Checked
        With DataGridView1
            .Columns(Cols.Pvp).ReadOnly = Not mMenuItemEditPreus.Checked
            .Columns(Cols.Dto).ReadOnly = Not mMenuItemEditPreus.Checked
        End With
    End Sub

    Private Sub Do_SenseCarrec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        With oRow.Cells(Cols.SenseCarrec)
            .Value = Not oRow.Cells(Cols.SenseCarrec).Value
            .Style.BackColor = GetPvpBackColor(oRow.Index)
        End With
        oMenuItem.Checked = Not oMenuItem.Checked
        CalculaImports(oRow)
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                e.Value = GetCellIcon(e.RowIndex)
            Case Cols.Pvp
                e.CellStyle.BackColor = GetPvpBackColor(e.RowIndex)
            Case Cols.Qty
                If mPdc.IsNew Then
                    e.CellStyle.BackColor = GetStockBackColor(e.RowIndex)
                End If
            Case Cols.Pendent
                If Not mPdc.IsNew Then
                    e.CellStyle.BackColor = GetStockBackColor(e.RowIndex)
                End If
                'Case Cols.Pvp
                'Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                'If oRow.Cells(Cols.SenseCarrec).Value Then
                'e.CellStyle.BackColor = maxisrvr.COLOR_NOTOK
                'End If
        End Select
    End Sub

    Private Function GetCellIcon(ByVal iRowIndex As Integer) As System.Drawing.Image
        Dim oImage As Image = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
        If IsDBNull(oRow.Cells(Cols.Pack).Value) Then
            oImage = My.Resources.empty
        Else
            oImage = GetDaus(oRow.Cells(Cols.Pack).Value)
        End If
        Return oImage
    End Function

    Private Function GetStockBackColor(ByVal iRowIndex As Integer) As System.Drawing.Color
        Dim oColor As System.Drawing.Color = DataGridView1.DefaultCellStyle.BackColor
        Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
        If Not IsDBNull(oRow.Cells(Cols.ArtId).Value) Then
            If Not IsDBNull(oRow.Cells(Cols.Pendent).Value) Then
                If oRow.Cells(Cols.Pendent).Value > 0 Then
                    Dim iArt As Integer = oRow.Cells(Cols.ArtId).Value
                    If iArt > 0 Then
                        Dim iCurrentCell As Integer = IIf(mPdc.IsNew, Cols.Qty, Cols.Pendent)
                        Dim iQty As Integer = 0
                        If Not IsDBNull(oRow.Cells(iCurrentCell).Value) Then
                            iQty = oRow.Cells(iCurrentCell).Value
                        End If

                        Dim iTpa As Integer = oRow.Cells(Cols.Tpa).Value
                        If iTpa = mNukTpaId Then
                            oColor = Color.LightGray
                        Else
                            'Dim iPnc As Integer = 0
                            Dim iStk As Integer = 0
                            Dim iPn2 As Integer = 0

                            'If Not IsDBNull(oRow.Cells(Cols.Pendent).Value) Then iPnc = oRow.Cells(Cols.Pendent).Value
                            If Not IsDBNull(oRow.Cells(Cols.ArtStk).Value) Then iStk = oRow.Cells(Cols.ArtStk).Value
                            If Not IsDBNull(oRow.Cells(Cols.ArtPn2).Value) Then iPn2 = oRow.Cells(Cols.ArtPn2).Value

                            If iStk <= 0 Then
                                oColor = Color.LightSalmon
                            ElseIf iStk >= (iQty + iPn2) Then
                                oColor = Color.LightGreen
                            Else
                                oColor = Color.Yellow
                            End If
                        End If
                    End If
                End If
            Else
                oColor = Color.White
            End If
        End If
        Return oColor
    End Function

    Private Function GetPvpBackColor(ByVal iRowIndex As Integer) As System.Drawing.Color
        Dim oColor As System.Drawing.Color = DataGridView1.DefaultCellStyle.BackColor
        Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
        If Not IsDBNull(oRow.Cells(Cols.SenseCarrec).Value) Then
            If oRow.Cells(Cols.SenseCarrec).Value = True Then
                oColor = MaxiSrvr.COLOR_NOTOK
            End If
        End If
        Return oColor
    End Function

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mAllowEvents Then

            Select Case e.ColumnIndex
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    oRow.Cells(Cols.Qty).Style.BackColor = GetStockBackColor(e.RowIndex)

                Case Cols.ArtNom
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim iQty As Integer = 0
                    If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
                        iQty = oRow.Cells(Cols.Qty).Value
                    End If
                    oRow.Cells(Cols.ArtNom).Style.BackColor = GetStockBackColor(e.RowIndex)
                    If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                        Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                        If oArt.Obsoleto Then
                            oRow.Cells(Cols.ArtNom).Style.BackColor = Color.LightGray
                        End If
                    End If
            End Select

        End If
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mAllowEvents Then
            Dim oArrayErrs As New ArrayList
            Dim sWarn As String = ""

            Select Case e.ColumnIndex
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    sWarn = CheckInteger(e.FormattedValue)
                    If sWarn > "" Then
                        oArrayErrs.Add(sWarn)
                    ElseIf e.FormattedValue > "" Then
                        sWarn = CheckMaxUnits(oRow, e.FormattedValue)
                        If sWarn > "" Then oArrayErrs.Add(sWarn)

                        If Not mSkipMinPack Then
                            sWarn = CheckTheRightPack(oRow, e.FormattedValue)
                            If sWarn > "" Then oArrayErrs.Add(sWarn)
                        End If
                    End If

                Case Cols.ArtNom
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    sWarn = CheckPermissionToChangeArt(oRow, e.FormattedValue)
                    If sWarn > "" Then oArrayErrs.Add(sWarn)
            End Select

            If oArrayErrs.Count > 0 Then
                Dim sB As New System.Text.StringBuilder
                For Each s As String In oArrayErrs
                    sB.AppendLine(s)
                Next
                MsgBox(sB.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                oRow.Cells(e.ColumnIndex).Selected = True
                e.Cancel = True
            End If
        End If
    End Sub

    Private Function CheckInteger(ByVal sSource As String) As String
        Dim sRetVal As String = ""
        If IsNumeric(sSource) Then
            If CDec(sSource) <> CInt(sSource) Then
                sRetVal = "no s'admeten decimals"
            End If
        Else
            If sSource > "" Then
                sRetVal = "nomes s'admeten valors numerics"
            End If
        End If
        Return sRetVal
    End Function

    Private Function CheckMaxUnits(ByVal oRow As DataGridViewRow, ByVal iNewQty As Integer) As String
        Dim sRetVal As String = ""

        If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
            Dim oArt As Art = GetArtFromDataGridViewRow(oRow)

            Dim iOldQty As Integer = 0
            If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
                iOldQty = oRow.Cells(Cols.Qty).Value
            End If

            If iNewQty <> iOldQty Then
                Dim BlWarn As Boolean = True
                Dim iMax As Integer = oArt.MaxUnitsOrdable
                If iMax < 0 Then iMax = 0
                Dim iExtraQtyRequested As Integer = iNewQty - iOldQty
                If iExtraQtyRequested > iMax Then
                    If iMax > 0 Then
                        sRetVal = "Article fora de producció. No es poden demanar més de " & iMax.ToString & " unitats"
                    Else
                        sRetVal = "Article esgotat i fora de producció. Ja no s'admeten comandes"
                    End If
                End If
            End If

        End If

        Return sRetVal
    End Function

    Private Function CheckTheRightPack(ByVal oRow As DataGridViewRow, ByVal iNewQty As Integer) As String
        Dim sRetVal As String = ""

        Dim iOldQty As Integer = 0
        If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
            iOldQty = oRow.Cells(Cols.Qty).Value
        End If


        If iNewQty <> iOldQty Then
            Dim iPendent As Integer = 0
            If Not IsDBNull(oRow.Cells(Cols.Pendent).Value) Then
                iPendent = oRow.Cells(Cols.Pendent).Value
            End If

            Dim iQtyRequested As Integer = iNewQty - iOldQty + iPendent
            Dim iPack As Integer = CInt(oRow.Cells(Cols.Pack).Value)
            If iPack > 1 Then
                Dim iMod As Integer = iQtyRequested Mod iPack
                If iMod <> 0 Then
                    Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                    If Not oArt.Dimensions.SelfOrInherited.AllowUserToFraccionarInnerPack Then
                        sRetVal = "la quantitat pendent ha de ser multiplo de " & iPack.ToString
                    End If
                End If
            End If
        End If

        Return sRetVal
    End Function

    Private Function CheckPermissionToChangeArt(ByVal oRow As DataGridViewRow, ByVal sNewValue As String) As String
        Dim sRetVal As String = ""
        If Not IsDBNull(oRow.Cells(Cols.Lin).Value) Then
            Dim iLin As Integer = oRow.Cells(Cols.Lin).Value
            If iLin > 0 Then
                Dim sOldValue As String = oRow.Cells(Cols.ArtNom).Value
                If sNewValue <> sOldValue Then
                    'check sortides
                    Dim oLineItmPnc As New LineItmPnc(CType(oRow.Cells(Cols.Guid).Value, Guid))
                    If oLineItmPnc.SortidesExist Then
                        sRetVal = "no es pot rectificar aquesta columna. Ja han sortit articles!"
                    End If

                End If
            End If
        End If



        Return sRetVal
    End Function

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim iPendents As Integer = GetPendents(oRow)
                    oRow.Cells(Cols.Pendent).Value = iPendents
                    CalculaImports(oRow)
                Case Cols.Pvp, Cols.Dto
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    CalculaImports(oRow)
                Case Cols.ArtNom
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim BlPreviousAllowEvents As Boolean = mAllowEvents
                    mAllowEvents = False
                    Dim sKey As String = oRow.Cells(Cols.ArtNom).Value

                    Dim oSku As DTOProductSku = Finder.FindSku(sKey, BLL.BLLApp.Mgz)
                    If oSku Is Nothing Then
                        FeedRowFromArt(oRow, Nothing)
                    Else
                        Dim oArt As New Art(oSku.Guid)
                        If mPdc.Client.IsArtAllowed(oSku) Then
                            FeedRowFromArt(oRow, oArt)
                            oRow.Cells(Cols.ArtNom).Value = oArt.Nom_ESP
                        ElseIf mPdc.Client.CcxOrMe.IsArtAllowed(oSku) Then
                            FeedRowFromArt(oRow, oArt)
                            oRow.Cells(Cols.ArtNom).Value = oArt.Nom_ESP
                        Else
                            MsgBox(oArt.Nom_ESP & vbCrLf & "Aquest client té bloquejat aquest article", MsgBoxStyle.Exclamation, "MAT.NET")
                            My.Computer.Keyboard.SendKeys("+{TAB}")

                        End If
                    End If
                    'Application.DoEvents()
                    mAllowEvents = BlPreviousAllowEvents
                    SetContextMenu()
            End Select
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)

        End If
    End Sub

    Private Function GetPendents(ByVal oRow As DataGridViewRow) As Integer
        Dim RetVal As Integer = 0

        Dim iNewQty As Integer = oRow.Cells(Cols.Qty).Value

        Dim iLin As Integer = 0
        If Not IsDBNull(oRow.Cells(Cols.Lin).Value) Then
            iLin = oRow.Cells(Cols.Lin).Value
        End If

        If iLin = 0 Then
            RetVal = iNewQty
        Else
            'get old value
            Dim oLineItmPnc As New LineItmPnc(CType(oRow.Cells(Cols.Guid).Value, Guid))
            oLineItmPnc.SetItm()
            Dim iOldQty As Integer = oLineItmPnc.Qty
            Dim iOldPnc As Integer = oLineItmPnc.Pendent
            RetVal = iNewQty - iOldQty + iOldPnc
        End If
        Return RetVal
    End Function

    Private Sub CalculaImports(ByVal oRow As DataGridViewRow)
        Dim iQty As Integer = 0
        Dim DcPvp As Decimal = 0
        Dim DcDto As Decimal = 0
        Dim BlCarrec As Boolean = True
        Dim DcAmt As Decimal = 0

        If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then iQty = oRow.Cells(Cols.Qty).Value
        If Not IsDBNull(oRow.Cells(Cols.Pvp).Value) Then DcPvp = oRow.Cells(Cols.Pvp).Value
        If Not IsDBNull(oRow.Cells(Cols.Dto).Value) Then DcDto = oRow.Cells(Cols.Dto).Value
        If Not IsDBNull(oRow.Cells(Cols.SenseCarrec).Value) Then BlCarrec = Not oRow.Cells(Cols.SenseCarrec).Value

        If BlCarrec Then
            DcAmt = Math.Round(iQty * DcPvp * (100 - DcDto) / 100, 2)
        End If

        oRow.Cells(Cols.Amt).Value = DcAmt
        CalculaTotals()
    End Sub

    Private Sub CalculaTotals()
        Dim DcTot As Decimal = 0
        Dim DcEur As Decimal = 0
        Dim DcNuk As Decimal = 0
        Dim iTpa As Integer = 0
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not IsDBNull(oRow.Cells(Cols.Amt).Value) And Not IsDBNull(oRow.Cells(Cols.Tpa).Value) Then
                If CBool(oRow.Cells(Cols.SenseCarrec).Value) = False Then
                    DcEur = oRow.Cells(Cols.Amt).Value
                    DcTot += DcEur
                    iTpa = oRow.Cells(Cols.Tpa).Value
                    If iTpa = mNukTpaId Then DcNuk += DcEur
                End If
            End If
        Next

        Dim s As String = ""
        If DcTot <> 0 Then
            s = "total " & Format(DcTot, "#,##0.00 €")
            If DcNuk <> 0 Then s = s & "  (VIVACE " & Format(DcTot - DcNuk, "#,##0.00 €") & " / NUK " & Format(DcNuk, "#,##0.00 €") & ")"
        End If

        ToolStripStatusLabelTot.Text = s
    End Sub

    Private Function GetDaus(ByVal iPack As Integer) As Image
        Dim oRetVal As Image = Nothing
        Select Case iPack
            Case 2
                oRetVal = My.Resources.dau2_17x17
            Case 3
                oRetVal = My.Resources.dau3_17x17
            Case 4
                oRetVal = My.Resources.dau4_17x17
            Case 5
                oRetVal = My.Resources.dau5_17x17
            Case Is > 5
                oRetVal = My.Resources.dau6_17x17
            Case Else
                oRetVal = My.Resources.empty
        End Select
        Return oRetVal
    End Function


    Private Sub DataGridView1_RowValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) ' Handles DataGridView1.RowValidating
        If mAllowEvents Then
            If e.RowIndex = DataGridView1.Rows.Count - 2 Then
                If DataGridView1.IsCurrentRowDirty Then '================ en proves
                    CheckArtWiths()
                    CheckFreeUnits()
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        If mAllowEvents Then
            If e.RowIndex = DataGridView1.Rows.Count - 2 Then
                ' If DataGridView1.IsCurrentRowDirty Then '================ en proves
                CheckArtWiths()
                CheckFreeUnits()
                'End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If mAllowEvents Then
            Dim iRows As Integer = mTb.Rows.Count
            RaiseEvent AfterUpdate(Me, EventArgs.Empty)
            iRows = mTb.Rows.Count
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub CheckFreeUnits()
        'nomes actua si estem afegint una linia nova
        Dim oRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Count - 2)
        'Get main art
        If IsNumeric(oRow.Cells(Cols.ArtId).Value) Then
            If HasCarrec(oRow) And Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
                Dim iQty As Integer = CInt(oRow.Cells(Cols.Qty).Value)
                If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then

                    Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                    Dim oProduct As New Product(oArt)
                    Dim oIncentius As Incentius = oProduct.Incentius(MaxiSrvr.Incentiu.Cods.FreeUnits, mPdc.Fch)
                    If oIncentius.Count > 0 Then

                        'troba l'incentiu que li proporciona mes unitats sense carrec
                        'dins que les unitats amb carrec no passin de les demanades
                        Dim oBestIncentiu As Incentiu = Nothing
                        Dim iBestSenseCarrec As Integer = 0
                        Dim iBestAmbCarrec As Integer = iQty

                        For Each oIncentiu As Incentiu In oIncentius
                            Dim iAmbCarrec As Integer = 0
                            Dim iSenseCarrec As Integer = 0
                            Dim iMinPack As Integer = oArt.Dimensions.InnerPackObligatori
                            If oIncentiu.GetFreeUnits(iMinPack, iQty, iAmbCarrec, iSenseCarrec) Then
                                If iSenseCarrec > iBestSenseCarrec Then
                                    oBestIncentiu = oIncentiu
                                    iBestSenseCarrec = iSenseCarrec
                                    iBestAmbCarrec = iAmbCarrec
                                End If
                            End If
                        Next

                        'actua si cal
                        If iBestSenseCarrec > 0 Then
                            If iBestAmbCarrec <> iQty Then
                                modificaCurrentQtyTo(iBestAmbCarrec, oRow, oBestIncentiu)
                            End If
                            afegeixSenseCarrec(iBestSenseCarrec, oArt, oBestIncentiu)
                        End If

                    End If
                End If
            End If
        End If

    End Sub

    Private Function HasCarrec(ByVal oRow As DataGridViewRow) As Boolean
        Dim retVal As Boolean = False
        If Not IsDBNull(oRow.Cells(Cols.Pvp).Value) Then
            Dim DcEur As Decimal = CDec(oRow.Cells(Cols.Pvp).Value)
            If DcEur <> 0 Then
                Dim BlSenseCarrec As Boolean = CBool(oRow.Cells(Cols.SenseCarrec).Value)
                retVal = Not BlSenseCarrec
            End If
        End If
        Return retVal
    End Function

    Private Sub modificaCurrentQtyTo(ByVal iQty As Integer, ByVal oRow As DataGridViewRow, ByVal oIncentiu As Incentiu)
        oRow.Cells(Cols.Qty).Value = iQty
        oRow.Cells(Cols.Promo).Value = oIncentiu.Guid
    End Sub

    Private Sub afegeixSenseCarrec(ByVal iQty As Integer, ByVal oArt As Art, ByVal oIncentiu As Incentiu)
        Dim oDataRow As DataRow = mTb.NewRow
        Dim oTpa As Tpa = oArt.Stp.Tpa
        'Dim oRepcom As RepCom = mPdc.Client.GetRepCom(oTpa, mPdc.Fch)

        Dim oRepGuid As Guid = System.Guid.Empty
        Dim DcComisioPercent As Decimal = 0
        Dim oRepCom As RepCom = RepCom.FromCliProduct(mPdc.Client, oArt, mPdc.Fch)
        If oRepCom IsNot Nothing Then
            oRepGuid = oRepCom.Rep.Guid
            DcComisioPercent = oRepCom.ComisioPercent
        End If


        'Dim oArtStk As ArtStk = mPdc.Mgz.ArtStk(oArt)
        'Dim oDimensions As ArtDimensions = oArt.Dimensions.SelfOrInherited
        'Dim iPack As Integer = IIf(oDimensions.InnerPack > 1 And oDimensions.ForzarInnerPack, oDimensions.InnerPack, 1)

        oDataRow(Cols.Rep) = oRepGuid
        oDataRow(Cols.Com) = DcComisioPercent
        oDataRow(Cols.ArtId) = oArt.Id
        oDataRow(Cols.ArtGuid) = oArt.Guid
        oDataRow(Cols.ArtNom) = oArt.Nom_ESP
        oDataRow(Cols.Qty) = iQty
        oDataRow(Cols.Pendent) = iQty
        oDataRow(Cols.Pvp) = 0
        oDataRow(Cols.Dto) = 0
        oDataRow(Cols.SenseCarrec) = True
        oDataRow(Cols.ArtStk) = oArt.Stk(mPdc.Mgz)
        oDataRow(Cols.ArtPn2) = oArt.Pn2
        oDataRow(Cols.ArtPrevisio) = oArt.PrevisionsText()
        oDataRow(Cols.Pack) = 0
        oDataRow(Cols.Tpa) = oArt.Stp.Tpa.Id
        oDataRow(Cols.Promo) = oIncentiu.Guid
        mTb.Rows.Add(oDataRow)

        Application.DoEvents()
        'DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.RowCount - 2).Cells(Cols.ArtNom)
        SendKeys.Send("{DOWN}")
    End Sub


    Private Sub CheckArtWiths()
        'nomes actua si estem afegint una linia nova
        Dim oRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Count - 2)
        'Get main art
        If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
            If Not IsDBNull(oRow.Cells(Cols.Qty).Value) Then
                Dim oMainArt As Art = GetArtFromDataGridViewRow(oRow)
                If Not oMainArt.Virtual Then
                    Dim oArtWiths As ArtWiths = oMainArt.ArtWiths
                    If oArtWiths.Count > 0 Then
                        Dim iQty As Integer = oRow.Cells(Cols.Qty).Value
                        For Each oItm As ArtWith In oArtWiths
                            Dim oDataRow As DataRow = mTb.NewRow
                            Dim oArt As Art = oItm.Child
                            Dim oTpa As Tpa = oArt.Stp.Tpa
                            'Dim oRepcom As RepCom = mPdc.Client.GetRepCom(oTpa, mPdc.Fch)

                            Dim oRepGuid As Guid
                            Dim DcComisioPercent As Decimal = 0
                            Dim oRepCom As RepCom = RepCom.FromCliProduct(mPdc.Client, oArt, mPdc.Fch)
                            If oRepCom IsNot Nothing Then
                                oRepGuid = oRepCom.Rep.Guid
                                DcComisioPercent = oRepCom.ComisioPercent
                            End If

                            'Dim oArtStk As ArtStk = mPdc.Mgz.ArtStk(oArt)
                            Dim oDimensions As ArtDimensions = oArt.Dimensions.SelfOrInherited
                            Dim iPack As Integer = IIf(oDimensions.InnerPack > 1 And oDimensions.ForzarInnerPack, oDimensions.InnerPack, 1)

                            oDataRow(Cols.Rep) = oRepGuid
                            oDataRow(Cols.Com) = DcComisioPercent
                            oDataRow(Cols.ArtId) = oArt.Id
                            oDataRow(Cols.ArtGuid) = oArt.Guid
                            oDataRow(Cols.ArtNom) = oArt.Nom_ESP
                            oDataRow(Cols.Qty) = iQty * oItm.Qty
                            oDataRow(Cols.Pendent) = iQty * oItm.Qty
                            oDataRow(Cols.Pvp) = GetPreu(oArt).Val
                            oDataRow(Cols.Dto) = mPdc.Client.CcxOrMe.DtoArt(oArt)
                            oDataRow(Cols.SenseCarrec) = False
                            oDataRow(Cols.ArtStk) = oArt.Stk(mPdc.Mgz)
                            oDataRow(Cols.ArtPn2) = oArt.Pn2
                            oDataRow(Cols.ArtPrevisio) = oArt.PrevisionsText()
                            oDataRow(Cols.Pack) = iPack
                            oDataRow(Cols.Tpa) = oArt.Stp.Tpa.Id
                            mTb.Rows.Add(oDataRow)
                        Next
                    End If
                End If
            End If
        End If

    End Sub


    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Pvp
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub


End Class
