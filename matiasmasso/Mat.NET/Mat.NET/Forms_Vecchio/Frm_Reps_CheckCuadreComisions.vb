
Imports System.Data.SqlClient

Public Class Frm_Reps_CheckCuadreComisions
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mClient As Client
    Private LOCKEDICON As Byte() = maxisrvr.GetByteArrayFromImg(My.Resources.candado)
    Private EMPTYICON As Byte() = maxisrvr.GetByteArrayFromImg(My.Resources.empty)

    Private Enum Cols
        Yea
        Pdc
        Lin
        Cli
        CliNom
        Fch
        ArtGuid
        ArtNom
        ArcRep
        ArcCom
        ArcRepNom
        ZonRep
        ZonCom
        ZonRepNom
    End Enum

    Public Sub New(Optional oCli As Client = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oCli
        Me.Show()
    End Sub

    Private Sub Frm_Reps_CheckCuadreComisions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()

        Dim SQL As String = "SELECT ARC.ye2, ARC.PDC, ARC.ln2, PDC.cli, CLX.Clx, PDC.fch, Art.guid, ART.Myd, ARC.rep, ARC.Com, '' as ArcRepNom, 0 as ZonRep, 0 as ZonCom, '' as ZonRepNom " _
        & "FROM            ARC INNER JOIN " _
        & "ART ON ARC.ArtGuid=ART.Guid INNER JOIN " _
        & "ALB ON ARC.AlbGuid = ALB.Guid INNER JOIN  " _
        & "PDC ON ARC.PdcGuid = PDC.PdcGuid LEFT OUTER JOIN  " _
        & "CLX ON PDC.emp = CLX.Emp AND PDC.Cli = CLX.Cli LEFT OUTER JOIN  " _
        & "RPS ON ALB.Emp = RPS.Emp AND ALB.yef = RPS.yef AND ALB.fra = RPS.fra   " _
        & "WHERE PDC.Emp =@Emp AND (RPS.Id IS NULL) AND (PDC.cod = 2) AND (PDC.yea > 2011) " _
        & "ORDER BY ALB.yea DESC, ALB.alb DESC, ARC.lin DESC"


        If mClient IsNot Nothing Then
            SQL = SQL & " AND PDC.CLI=" & mClient.Id.ToString
        End If


        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        With ProgressBar1
            .Visible = True
            .Maximum = oTb.Rows.Count
            .Value = 0
        End With
        Application.DoEvents()

        For i As Integer = oTb.Rows.Count - 1 To 0 Step -1
            ProgressBar1.Increment(1)
            Application.DoEvents()

            Dim oRow As DataRow = oTb.Rows(i)
            Dim CliId As Integer = CInt(oRow("Cli"))
            Dim DtFch As Date = CDate(oRow("Fch"))
            Dim oArtGuid As Guid = CType(oRow("Guid"), Guid)
            Dim iRep As Integer = CInt(oRow("Rep"))
            Dim DcCom As Decimal = CDec(oRow("Com"))
            Dim iPdc As Integer = CInt(oRow("Pdc"))

            Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, CliId)
            Dim oClient As new Client(oContact.Guid)
            Dim oProduct As New Product(oArtGuid)
            Dim oRepCom As RepCom = RepProductLoader.GetRepCom(oClient, oProduct, DtFch)


            Dim iZonRep As Integer = 0
            Dim DcZonCom As Decimal = 0
            Dim sZonRepNom As String = ""
            If oRepCom IsNot Nothing Then
                iZonRep = oRepCom.Rep.Id
                sZonRepNom = oRepCom.Rep.Abr
                DcZonCom = oRepCom.ComisioPercent
            End If

            If iZonRep = iRep And DcZonCom = DcCom Then
                oTb.Rows.RemoveAt(i)
            Else
                Dim oArcRep As Rep = MaxiSrvr.Rep.FromNum(mEmp, iRep)
                oRow("ArcRepNom") = oArcRep.Abr

                oRow("ZonRep") = iZonRep
                oRow("ZonRepNom") = sZonRepNom
                oRow("ZonCom") = DcZonCom
            End If
        Next

        ProgressBar1.Visible = False
        Application.DoEvents()

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
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With
            With .Columns(Cols.Lin)
                .Visible = False
            End With
            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.CliNom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 150
            End With
            With .Columns(Cols.ArcRep)
                .Visible = False
            End With
            With .Columns(Cols.ArcCom)
                .HeaderText = "comisió"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.ArcRepNom)
                .HeaderText = "rep comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            With .Columns(Cols.ZonRep)
                .Visible = False
            End With
            With .Columns(Cols.ZonCom)
                .HeaderText = "comisió"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.ZonRepNom)
                .HeaderText = "rep zona"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
        End With

    End Sub


    Private Sub ReAssignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReAssignToolStripMenuItem.Click
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Reassign(oRow.Cells(Cols.Yea).Value, oRow.Cells(Cols.Pdc).Value, oRow.Cells(Cols.Lin).Value, oRow.Cells(Cols.ZonRep).Value, oRow.Cells(Cols.ZonCom).Value)
        Next
        LoadGrid()
    End Sub

    Private Sub Reassign(ByVal iYea As Integer, ByVal iPdc As Integer, ByVal iLin As Integer, ByVal iNewRep As Integer, ByVal DcNewCom As Decimal)
        Dim sCom As String = DcNewCom.ToString
        sCom = sCom.Replace(",", ".")

        Dim SQL As String = "UPDATE PNC " _
        & "SET REP=@REP, COM=@COM " _
        & "WHERE EMP=@EMP AND YEA=@YEA AND PDC=@PDC AND LIN=@LIN"
        Dim RetVal1 As String = maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", iYea, "@PDC", iPdc, "@LIN", iLin, "@REP", iNewRep, "@COM", sCom)

        SQL = "UPDATE ARC " _
        & "SET REP=@REP, COM=@COM " _
        & "WHERE EMP=@EMP AND YE2=@YEA AND PDC=@PDC AND LN2=@LIN "
        Dim RetVal2 As String = maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@YEA", iYea, "@PDC", iPdc, "@LIN", iLin, "@REP", iNewRep, "@COM", sCom)

    End Sub

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
            Dim iPdc As Integer = oRow.Cells(Cols.Pdc).Value
            oPdc = Pdc.FromNum(mEmp, iYea, iPdc)
        End If
        Return oPdc
    End Function

    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        Dim oPdc As Pdc = CurrentPdc()
        If oPdc IsNot Nothing Then
            Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
            Dim exs As New List(Of Exception)
            If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                UIHelper.WarnError(exs)
            Else
                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                oFrm.Show()
            End If
        End If
    End Sub

    Private Sub RefrescaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefrescaToolStripMenuItem.Click
        LoadGrid()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExcelToolStripMenuItem.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oPdc As Pdc = CurrentPdc()

        'oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Do_Zoom)
        oContextMenuStrip.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub
End Class