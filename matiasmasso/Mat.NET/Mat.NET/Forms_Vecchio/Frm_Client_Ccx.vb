

Public Class Frm_Client_Ccx
    Private mHeadOffice As Client
    Private mAllowEvents As Boolean
    Private mCurrentAction As Actions = Actions.Centres

    Private Enum Actions
        Root
        Centres
        Pncs
        StpFras
        Platfs
    End Enum

    Private Enum ColsCentres
        lincod
        Id
        Nom
        Ref
        Gln
        Obsoleto
    End Enum

    Private Enum ColsPncs
        Lincod
        Guid
        Text
        Qty
    End Enum

    Private Enum ColsStpFras
        Guid
        Fra
        Fch
        Tpa
        Stp
        Qty
    End Enum

    Private Enum ColsPlatfs
        LinCod
        entregarEn
        Ref
        FchMin
        Pdd
        Fch
        Eur
        Lines
        LinesNoStk
        LinesNotEnough
        Guid
    End Enum

    Private Enum StockStatus
        NotSet
        EnoughStock
        ParciallyAvailable
        NoStock
    End Enum

    Public Sub New(ByVal oMeOrCcx As Client)
        MyBase.New()
        Me.InitializeComponent()
        mHeadOffice = oMeOrCcx.CcxOrMe
        Xl_Contact_Logo1.Contact = oMeOrCcx
        LoadTreeView()

        LoadCentres()
        SetContextMenuCentres()
        mAllowEvents = True
    End Sub

    Private Sub LoadTreeView()
        Dim oRootNode As maxisrvr.TreeNodeObj = New maxisrvr.TreeNodeObj("Grup", Actions.Root)

        TreeView1.Nodes.Add(oRootNode)

        oRootNode.Nodes.Add(New maxisrvr.TreeNodeObj("centres", Actions.Centres))
        TreeView1.SelectedNode = oRootNode.Nodes(0)

        oRootNode.Nodes.Add(New maxisrvr.TreeNodeObj("pendent d'entrega", Actions.Pncs))
        oRootNode.Nodes.Add(New maxisrvr.TreeNodeObj("producte facturat", Actions.StpFras))

        If mHeadOffice.Id = MaxiSrvr.ElCorteIngles.ID Or mHeadOffice.Id = MaxiSrvr.ECIGA.ID Then
            oRootNode.Nodes.Add(New maxisrvr.TreeNodeObj("envios per plataforma", Actions.Platfs))
        End If

        TreeView1.ExpandAll()
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        If mAllowEvents Then
            Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
            mCurrentAction = CType(oNode.Obj, Actions)

            LoadGrid()
        End If
    End Sub


#Region "Centres"

    Private Sub LoadCentres()
        Dim SQL As String = "SELECT (CASE WHEN CLICLIENT.Guid=@CliGuid THEN 0 ELSE 1 END) AS LINCOD, " _
        & "CLICLIENT.CLI,CLX.CLX,CLICLIENT.REF,CLIGRAL.GLN,CLIGRAL.OBSOLETO " _
        & "FROM CLICLIENT INNER JOIN " _
        & "CLIGRAL ON CLICLIENT.EMP=CLIGRAL.EMP AND CLICLIENT.CLI=CLIGRAL.CLI INNER JOIN " _
        & "CLX ON CLICLIENT.EMP=CLX.EMP AND CLICLIENT.CLI=CLX.CLI " _
        & "WHERE CLICLIENT.CcxGuid=@CliGuid OR CLICLIENT.Guid=@CliGuid " _
        & "ORDER BY CLIGRAL.OBSOLETO,LINCOD,CLICLIENT.REF,CLX.CLX "
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mHeadOffice.Guid.ToString)

        Dim oTb As DataTable = oDs.Tables(0)
        'Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        'oCol.SetOrdinal(ColsCentres.Ico)
        mAllowEvents = False
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
            .AllowUserToResizeColumns = True
            With .Columns(ColsCentres.lincod)
                .Visible = False
            End With
            With .Columns(ColsCentres.Id)
                .Visible = False
            End With
            With .Columns(ColsCentres.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsCentres.Ref)
                .HeaderText = "referencia"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsCentres.Gln)
                .HeaderText = "gln"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsCentres.Obsoleto)
                .Visible = False
            End With
            mAllowEvents = True
            SetContextMenuCentres()
        End With
    End Sub

    Private Function CurrentCentre() As Client
        Dim oRetVal As Client = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = MaxiSrvr.Client.FromNum(mHeadOffice.Emp, CInt(oRow.Cells(ColsCentres.Id).Value))
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenuCentres()
        Dim oContextMenuCentresStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim oItm As Client = CurrentCentre()
        If oItm IsNot Nothing Then
            oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("zoom", Nothing, AddressOf ZoomCentre))
            oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("uniformitzar logo", Nothing, AddressOf LogoUniform))
        End If
        oMenuItem = New ToolStripMenuItem("afegir...", Nothing, AddressOf AddNewCentre)
        If mHeadOffice.Obsoleto Then oMenuItem.Enabled = False
        If mHeadOffice.CcxOrMe.Id <> mHeadOffice.Id Then oMenuItem.Enabled = False
        oContextMenuCentresStrip.Items.Add(oMenuItem)
        oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf RefreshRequest))
        DataGridView1.ContextMenuStrip = oContextMenuCentresStrip
    End Sub

    Private Sub ZoomCentre(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItm As Client = CurrentCentre()
        If oItm IsNot Nothing Then
            Dim oFrm As New Frm_Contact(oItm)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub AddNewCentre(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItm As Client = mHeadOffice.NewChild

        If oItm IsNot Nothing Then
            Dim oFrm As New Frm_Contact(oItm)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub LogoUniform(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("el logo de la central es grabará a tots els centres", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim SQL As String = "UPDATE CENTRE SET CENTRE.IMG48 = CENTRAL.IMG48 FROM CLX CENTRE INNER JOIN " _
            & "CLICLIENT ON CENTRE.Guid=CLICLIENT.Guid INNER JOIN " _
            & "CLX CENTRAL ON CENTRAL.Guid=CLICLIENT.CcxGuid " _
            & "WHERE CENTRAL.Guid=@CliGuid"
            Dim i As Integer = MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mHeadOffice.Guid.ToString)
            MsgBox("actualitzats " & i.ToString & " centres")
            SetContextMenuCentres()
        End If
    End Sub

#End Region

#Region "Pncs"
    Private Sub LoadPncs()
        Dim SQL As String = "SELECT (CASE WHEN P.PDX = 0 THEN 0 ELSE 1 END) AS LINCOD, " _
        & "(CASE WHEN P.PDX = 0 THEN ART.GUID ELSE P.GUID END) AS GUID, " _
        & "(CASE WHEN P.PDX = 0 THEN TPA.DSC + '.' + STP.DSC + '.' + ART.ORD ELSE CONCEPTE END) AS CONCEPTE, " _
        & "SUM(P.QTY) AS QTY " _
        & "FROM  " _
        & "(SELECT 0 AS PDX, NULL AS GUID, Pdc.Emp, PNC.artGuid, '' AS CONCEPTE, SUM(PNC.qty) AS QTY " _
        & "FROM PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "CliClient ON CliClient.Guid = PDC.CliGuid " _
        & "WHERE        PNC.Pn2 <> 0 AND  (CliClient.CcxGuid = @CliGuid OR CliClient.Guid = @CliGuid) " _
        & "GROUP BY Pdc.Emp, PNC.artGuid " _
        & "UNION " _
        & "SELECT PDC.pdc as PDX, PDC.Guid, Pdc.Emp, PNC.artGuid, CAST(YEAR(PDC.fch) AS varchar)+CAST(MONTH(PDC.fch) AS varchar)+CAST(DAY(PDC.fch) AS varchar) + ' ' + PDC.pdd + ' ' + CliClient.ref AS CONCEPTE, " _
        & "PNC.qty " _
        & "FROM  PNC  INNER JOIN " _
        & "PDC  ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "CliClient ON CliClient.Guid = PDC.cliGuid " _
        & "WHERE PNC.Pn2 <> 0 AND (CliClient.ccxGuid = @CliGuid or CliClient.Guid = @CliGuid)) AS P INNER JOIN " _
        & "ART ON P.Emp = ART.emp AND P.artGuid = ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "TPA ON STP.Brand = TPA.Guid " _
        & "GROUP BY P.PDX, P.GUID, ART.Guid, P.CONCEPTE, TPA.DSC, STP.dsc, ART.ord, TPA.ORD, STP.ord " _
        & "ORDER BY TPA.ORD, STP.ord, ART.ord, LINCOD, CONCEPTE"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mHeadOffice.Guid.ToString)

        Dim oTb As DataTable = oDs.Tables(0)
        'Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        'oCol.SetOrdinal(ColsCentres.Ico)

        mAllowEvents = False
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
            With .Columns(ColsPncs.Lincod)
                .Visible = False
            End With
            With .Columns(ColsPncs.Guid)
                .Visible = False
            End With
            With .Columns(ColsPncs.Text)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsPncs.Qty)
                .HeaderText = "pendent"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True
        'SetContextMenuPncs()
    End Sub

    Private Function CurrentPncArt() As Art
        Dim oRetVal As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New Art(New Guid(oRow.Cells(ColsPncs.Guid).Value.ToString))
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenuPncs()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If oRow.Cells(ColsPncs.Lincod).Value = 0 Then
                'article
                Try
                    Dim oArt As Art = New Art(New Guid(oRow.Cells(ColsPncs.Guid).Value.ToString))
                    If oArt IsNot Nothing Then

                        oMenuItem = New ToolStripMenuItem("article...")
                        oContextMenu.Items.Add(oMenuItem)

                        Dim oMenuArt As New Menu_Art(oArt)
                        AddHandler oMenuArt.AfterUpdate, AddressOf RefreshRequest
                        oMenuItem.DropDownItems.AddRange(oMenuArt.Range)

                    End If

                Catch ex As Exception

                End Try
            Else
                'comanda
                Try

                    Dim oPdc As New Pdc(New Guid(oRow.Cells(ColsPncs.Guid).Value.ToString))
                    If oPdc IsNot Nothing Then

                        oMenuItem = New ToolStripMenuItem("comanda...")
                        oContextMenu.Items.Add(oMenuItem)

                        Dim oMenuPdc As New Menu_Pdc(oPdc)
                        AddHandler oMenuPdc.AfterUpdate, AddressOf RefreshRequest
                        oMenuItem.DropDownItems.AddRange(oMenuPdc.Range)

                    End If
                Catch ex As Exception

                End Try
            End If

        End If


        oContextMenu.Items.Add(New ToolStripMenuItem("excel", My.Resources.Excel_16, AddressOf Do_Excel))
        oContextMenu.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf RefreshRequest))
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ZoomPnc(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oItm As Client = CurrentCentre()
        'If oItm IsNot Nothing Then
        'Dim oFrm As New Frm_Contact(oItm)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
        'End If
    End Sub

#End Region


#Region "StpFras"
    Private Sub LoadStpFras()
        Dim SQL As String = "SELECT CAST(FRA.CcaGuid AS CHAR(36)), FRA.fra, FRA.fch, TPA.DSC AS TPANOM, STP.dsc AS STPNOM, SUM(ARC.qty) AS QTY " _
        & "FROM            ALB INNER JOIN " _
        & "CLICLIENT ON ALB.CliGuid=CLICLIENT.Guid INNER JOIN " _
        & "ARC ON ALB.Guid = ARC.AlbGuid INNER JOIN " _
        & "ART ON ARC.ArtGuid = ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "TPA ON STP.Brand = TPA.Guid LEFT OUTER JOIN " _
        & "FRA ON ALB.FraGuid = FRA.Guid " _
        & "WHERE (ALB.CliGuid=@CliGuid OR CLICLIENT.CcxGuid=@CliGuid) " _
        & "GROUP BY FRA.CCAGuid, FRA.yea, FRA.fra, FRA.fch, TPA.DSC, TPA.ORD, STP.dsc, STP.ord " _
        & "ORDER BY (CASE WHEN FRA.FRA IS NULL THEN 0 ELSE 1 END), FRA.yea DESC, FRA.fra DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mHeadOffice.Guid.ToString)

        Dim oTb As DataTable = oDs.Tables(0)
        'Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        'oCol.SetOrdinal(ColsCentres.Ico)
        mAllowEvents = False
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
            With .Columns(ColsStpFras.Guid)
                .Visible = False
            End With
            With .Columns(ColsStpFras.Fra)
                .HeaderText = "factura"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(ColsStpFras.Fch)
                .HeaderText = "data"
                .Width = 70
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsStpFras.Tpa)
                .HeaderText = "marca"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsStpFras.Stp)
                .HeaderText = "producte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsStpFras.Qty)
                .HeaderText = "unitats"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            mAllowEvents = True
        End With
    End Sub

    Private Function CurrentFra() As Fra
        Dim oRetVal As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColsStpFras.Guid).Value
            oRetVal = New Fra(oGuid)
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenuStpFras()
        Dim oContextMenuCentresStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim oItm As Client = CurrentCentre()
        If oItm IsNot Nothing Then
            oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("zoom", Nothing, AddressOf ZoomStpFra))
        End If
        oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("excel", Nothing, AddressOf Do_Excel))
        oContextMenuCentresStrip.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf RefreshRequest))
        DataGridView1.ContextMenuStrip = oContextMenuCentresStrip
    End Sub

    Private Sub ZoomStpFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItm As Fra = CurrentFra()
        If oItm IsNot Nothing Then
            Dim oFrm As New Frm_Fra(oItm)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub
#End Region


#Region "Platfs"

    Private Sub LoadPlatfs()
        Dim oMgz As DTOMgz = BLL.BLLApp.Mgz
        Dim SQL As String = "SELECT 1 AS LINCOD, PDC.entregarEn, CliClient.ref, PDC.Fch, PDC.pdd, PDC.fchMin, PDC.eur, " _
        & "COUNT(PNC.lin) AS LINES, " _
        & "SUM(CASE WHEN (ArtStock.Stock IS NULL) THEN 1 WHEN ArtStock.Stock = 0 THEN 1 ELSE 0 END) AS LINESNOSTK, " _
        & "SUM(CASE WHEN ArtStock.Stock >= dbo.fn_Pn2ToFch2(Pnc.ArtGuid,PDC.FCH) THEN 0 ELSE 1 END) AS LINESNOTENOUGH, " _
        & "PDC.GUID " _
        & "FROM            PDC INNER JOIN " _
        & "CliClient ON PDC.CliGuid = CliClient.Guid INNER JOIN " _
        & "PNC ON PDC.Guid = Pnc.PdcGuid LEFT OUTER JOIN " _
        & "ArtStock ON PNC.ArtGuid = ArtStock.ArtGuid AND ArtStock.MgzGuid =@MgzGuid " _
        & "WHERE  CliClient.CcxGuid = @CcxGuid AND (PNC.Pn2<>0) " _
        & "GROUP BY PDC.entregarEn, PDC.FchMin, PDC.pdd, PDC.fch, PDC.eur, CliClient.ref, PDC.GUID " _
        & "ORDER BY (CASE WHEN PDC.ENTREGAREN IS NULL THEN 1 ELSE 0 END), PDC.entregarEn, PDC.Fch, PDC.pdd"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@CcxGuid", mHeadOffice.Guid.ToString, "@MgzGuid", oMgz.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim i As Integer
        Dim iPlatf As Integer = -1
        Dim oRow As DataRow = Nothing
        Do Until i >= oTb.Rows.Count
            oRow = oTb.Rows(i)
            Dim iEntregarEn As Integer = 0
            If Not IsDBNull(oRow(ColsPlatfs.entregarEn)) Then
                iEntregarEn = oRow(ColsPlatfs.entregarEn)
            End If
            If iEntregarEn <> iPlatf Then
                iPlatf = iEntregarEn
                Dim oRowPlatf As DataRow = oTb.NewRow
                oRowPlatf(ColsCentres.lincod) = 0
                oRowPlatf(ColsPlatfs.entregarEn) = iEntregarEn
                oRowPlatf(ColsPlatfs.Ref) = Client.FromNum(mHeadOffice.Emp, iEntregarEn).Referencia
                oTb.Rows.InsertAt(oRowPlatf, i)
                i += 1
            End If
            i += 1
        Loop


        mAllowEvents = False
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
            With .Columns(ColsPlatfs.LinCod)
                .Visible = False
            End With
            With .Columns(ColsPlatfs.entregarEn)
                .Visible = False
            End With
            With .Columns(ColsPlatfs.Ref)
                .HeaderText = "centre destinatari"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsPlatfs.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsPlatfs.Pdd)
                .HeaderText = "comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsPlatfs.FchMin)
                .HeaderText = "dataentrega"
                .Width = 70
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsPlatfs.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsPlatfs.Lines)
                .Visible = False
            End With
            With .Columns(ColsPlatfs.LinesNoStk)
                .Visible = False
            End With
            With .Columns(ColsPlatfs.LinesNotEnough)
                .Visible = False
            End With
            With .Columns(ColsPlatfs.Guid)
                .Visible = False
            End With
            mAllowEvents = True
        End With
    End Sub


    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs
        Dim oRow As DataGridViewRow = Nothing
        If DataGridView1.SelectedRows.Count <= 1 Then
            oPdcs.Add(CurrentPdc)
        Else
            For Each oRow In DataGridView1.SelectedRows
                oPdcs.Add(New Pdc(New Guid(oRow.Cells(ColsPlatfs.Guid).Value.ToString)))
            Next
        End If
        Return oPdcs
    End Function

    Private Function CurrentPdc() As Pdc
        Dim oRetVal As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New Pdc(New Guid(oRow.Cells(ColsPlatfs.Guid).Value.ToString))
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenuPlatfs()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If oRow.Cells(ColsPncs.Lincod).Value = 1 Then
                'comanda
                Try

                    Dim oPdcs As Pdcs = CurrentPdcs()
                    If oPdcs.Count > 0 Then

                        oMenuItem = New ToolStripMenuItem("comanda...")
                        oContextMenu.Items.Add(oMenuItem)

                        Dim oMenuPdc As New Menu_Pdc(oPdcs)
                        AddHandler oMenuPdc.AfterUpdate, AddressOf RefreshRequest
                        oMenuItem.DropDownItems.AddRange(oMenuPdc.Range)

                        If mHeadOffice.Id = ElCorteIngles.ID Then
                            oMenuItem = New ToolStripMenuItem("canviar plataforma", Nothing, AddressOf Do_PlatfSend)
                            For Each oPlataforma As Contact In ElCorteIngles.Plataformas
                                oMenuItem.DropDownItems.Add(New Client(oPlataforma.Guid).Referencia, Nothing, AddressOf Do_ChangePlatform)
                            Next
                            oContextMenu.Items.Add(oMenuItem)
                        End If

                        oMenuItem = New ToolStripMenuItem("enviar (prioritat segons data comanda)", My.Resources.SquareArrowTurquesa, AddressOf Do_PlatfSend)
                        'oMenuItem.Enabled = (getStockStatus(oRow) = StockStatus.EnoughStock)
                        oContextMenu.Items.Add(oMenuItem)

                        oMenuItem = New ToolStripMenuItem("enviar (alta prioritat)", My.Resources.SquareArrowTurquesa, AddressOf Do_PlatfSendHighPriority)
                        oContextMenu.Items.Add(oMenuItem)
                    End If
                Catch ex As Exception

                End Try
            End If

        End If


        oContextMenu.Items.Add(New ToolStripMenuItem("excel", My.Resources.Excel_16, AddressOf Do_Excel))
        oContextMenu.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf RefreshRequest))
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ChangePlatform(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim sPlatformName As String = oMenuItem.Text
        For Each oPlataforma As Contact In ElCorteIngles.Plataformas
            If New Client(oPlataforma.Guid).Referencia = sPlatformName Then
                For Each oPdc As Pdc In CurrentPdcs()
                    Dim oOldPlatform As Contact = oPdc.EntregarEn 'fa saltar setitm
                    oPdc.EntregarEn = oPlataforma
                    Dim exs as New List(Of exception)
                    If Not oPdc.Update( exs) Then
                        MsgBox("error al canviar la plataforma" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                Next
            End If
        Next
        RefreshRequest()
    End Sub

    Private Sub ZoomPlatf(ByVal sender As Object, ByVal e As System.EventArgs)
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
    End Sub

    Private Sub Do_PlatfSend(ByVal sender As Object, ByVal e As System.EventArgs)

        For Each oPdc As Pdc In CurrentPdcs()
            Dim oAlb As Alb = oPdc.Deliver(, , True)
            oAlb.SetUser(BLL.BLLSession.Current.User)

            Dim exs As New List(Of Exception)
            If oAlb.Update( exs) Then
                RefreshRequest()
            Else
                MsgBox(oPdc.FullConcepte & vbCrLf & "s'ha produit un error desconegut" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Next
    End Sub

    Private Sub Do_PlatfSendHighPriority(ByVal sender As Object, ByVal e As System.EventArgs)

        For Each oPdc As Pdc In CurrentPdcs()
            Dim oAlb As Alb = oPdc.Deliver(, , True, True)
            oAlb.SetUser(BLL.BLLSession.Current.User)

            Dim exs As New List(Of Exception)
            If oAlb.Update( exs) Then
                RefreshRequest()
            Else
                MsgBox(oPdc.FullConcepte & vbCrLf & "s'ha produit un error desconegut" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Next
    End Sub

    Private Function getStockStatus(ByVal oRow As DataGridViewRow) As StockStatus
        Dim iLineCount As Integer = oRow.Cells(ColsPlatfs.Lines).Value
        Dim iLinesNoStock As Integer = oRow.Cells(ColsPlatfs.LinesNoStk).Value
        Dim iLinesNotEnough As Integer = oRow.Cells(ColsPlatfs.LinesNotEnough).Value

        Dim oRetVal As StockStatus = StockStatus.ParciallyAvailable
        If iLinesNoStock = iLineCount Then
            oRetVal = StockStatus.NoStock
        ElseIf (iLinesNoStock = 0 And iLinesNotEnough = 0) Then
            oRetVal = StockStatus.EnoughStock
        End If
        Return oRetVal
    End Function
#End Region

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case mCurrentAction
            Case Actions.Centres

            Case Actions.Pncs
                Select Case CInt(DataGridView1.Rows(e.RowIndex).Cells(ColsPncs.Lincod).Value)
                    Case 0
                        e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                        e.CellStyle.BackColor = Color.FromArgb(255, 255, 200)
                    Case Else
                        Select Case e.ColumnIndex
                            Case ColsPncs.Text
                                e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                            Case ColsPncs.Qty
                                e.CellStyle.Padding = New Padding(0, 0, 20, 0)
                        End Select
                End Select

            Case Actions.Platfs
                Select Case CInt(DataGridView1.Rows(e.RowIndex).Cells(ColsPncs.Lincod).Value)
                    Case 0
                        e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Bold)
                        e.CellStyle.BackColor = Color.FromArgb(255, 255, 200)
                    Case Else
                        Select Case e.ColumnIndex
                            Case ColsPlatfs.Pdd
                                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

                                Dim oColor As System.Drawing.Color = Color.White
                                Select Case getStockStatus(oRow)
                                    Case StockStatus.EnoughStock
                                        oColor = Color.LightGreen
                                    Case StockStatus.ParciallyAvailable
                                        oColor = Color.LightYellow
                                    Case StockStatus.NoStock
                                        oColor = maxisrvr.COLOR_NOTOK
                                End Select

                                e.CellStyle.BackColor = oColor
                        End Select
                End Select
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case mCurrentAction
            Case Actions.Centres
                ZoomCentre(sender, e)
            Case Actions.Pncs
                ZoomPnc(sender, e)
            Case Actions.StpFras
                ZoomStpFra(sender, e)
            Case Actions.Platfs
                ZoomPlatf(sender, e)
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Select Case mCurrentAction
            Case Actions.Centres
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim BlObsoleto As Boolean = (oRow.Cells(ColsCentres.Obsoleto).Value = 1)
                If BlObsoleto Then
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            Case Else
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select

    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            Select Case mCurrentAction
                Case Actions.Centres
                    SetContextMenuCentres()
                Case Actions.Pncs
                    SetContextMenuPncs()
                Case Actions.StpFras
                    SetContextMenuStpFras()
                Case Actions.Platfs
                    SetContextMenuPlatfs()
            End Select
        End If
    End Sub

    Private Sub LoadGrid()
        Select Case mCurrentAction
            Case Actions.Centres
                LoadCentres()
            Case Actions.Pncs
                LoadPncs()
            Case Actions.StpFras
                LoadStpFras()
            Case Actions.Platfs
                LoadPlatfs()
        End Select
    End Sub

    Private Sub RefreshRequest()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsCentres.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
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