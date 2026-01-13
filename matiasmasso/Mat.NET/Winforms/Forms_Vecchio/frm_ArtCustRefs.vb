

Public Class frm_ArtCustRefs
    Private mAllowEvents As Boolean
    Private mArt As Art = Nothing
    Private mClient As Client = Nothing
    Private mMode As Modes = Modes.NotSet

    Private Enum Cols
        ArtId
        CliGuid
        ArtGuid
        Nom
        Ref
    End Enum

    Private Enum Modes
        NotSet
        PerArt
        PerCli
    End Enum

    Public Sub New(oArt As Art)
        MyBase.New()
        Me.InitializeComponent()
        mArt = oArt
        mMode = Modes.PerArt
        refresca()
    End Sub

    Public Sub New(oClient As Client)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oClient
        mMode = Modes.PerCli
        refresca()
    End Sub

    Private Sub refresca()
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = ""
        Dim oGuid As Guid
        Dim sNomHeader As String = ""

        If mMode = Modes.PerArt Then
            sNomHeader = "client"
            oGuid = mArt.Guid
            SQL = "SELECT c.cLI,X.CliGuid, X.ArtGuid, C.RaoSocial, X.Ref " _
                                & "FROM ArtCustRef X INNER JOIN CliGral C ON X.CliGuid=C.Guid " _
                                & "WHERE X.ArtGuid=@Guid " _
                                & "ORDER BY C.RaoSocial"
        ElseIf mMode = Modes.PerCli Then
            sNomHeader = "producte"
            oGuid = mClient.Guid
            SQL = "SELECT P.ArtiD, X.CliGuid, X.ArtGuid, P.Nom, X.Ref " _
                                & "FROM ArtCustRef X INNER JOIN Product P ON X.ArtGuid=P.Guid " _
                                & "WHERE X.CliGuid=@Guid " _
                                & "ORDER BY P.Nom"
        End If

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Guid", oGuid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.CliGuid)
                .Visible = False
            End With

            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With

            With .Columns(Cols.ArtId)
                .HeaderText = "n/ref."
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(Cols.Nom)
                .HeaderText = sNomHeader
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Ref)
                .HeaderText = "referència"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
            End With
        End With
    End Sub

    Private Function CurrentItm() As ArtCustRef
        Dim oItm As ArtCustRef = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCliGuid As Guid = CType(oRow.Cells(Cols.CliGuid).Value, Guid)
            Dim oArtGuid As Guid = CType(oRow.Cells(Cols.ArtGuid).Value, Guid)
            Dim oClient As New Client(oCliGuid)
            Dim oArt As New Art(oArtGuid)
            Dim sRef As String = oRow.Cells(Cols.Ref).Value
            oItm = New ArtCustRef(oClient, oArt, sRef)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As ArtCustRef = CurrentItm()
        If oItm IsNot Nothing Then
            If mMode = Modes.PerArt Then
                oMenuItem = New ToolStripMenuItem("contacte", My.Resources.binoculares, AddressOf Zoom)
                oContextMenuStrip.Items.Add(oMenuItem)

                Dim oContactMenu As New Menu_Contact(oItm.Client)
                oMenuItem.DropDownItems.AddRange(oContactMenu.Range)

                oMenuItem = New ToolStripMenuItem("cataleg sencer", Nothing, AddressOf Do_CliCustRefs)
                oContextMenuStrip.Items.Add(oMenuItem)
            ElseIf mMode = Modes.PerCli Then
                oMenuItem = New ToolStripMenuItem("article", My.Resources.binoculares, AddressOf Zoom)
                oContextMenuStrip.Items.Add(oMenuItem)

                Dim oMenuArt As New Menu_Art(oItm.Art)
                oMenuItem.DropDownItems.AddRange(oMenuArt.Range)
            End If


        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("Excel", My.Resources.Excel, AddressOf Do_Excel))
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oArtCustRef As New ArtCustRef(mClient, mArt)
        Dim oFrm As New Frm_ArtCustRef(oArtCustRef)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oArtCustRef As ArtCustRef = CurrentItm()
        Dim oFrm As New Frm_ArtCustRef(oArtCustRef)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CliCustRefs()
        Dim oArtCustRef As ArtCustRef = CurrentItm()
        Dim oFrm As New frm_ArtCustRefs(oArtCustRef.Client)
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As ArtCustRef = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.Ref & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim BlSuccess As Boolean = oItm.Delete
            If BlSuccess Then
                RefreshRequest(Nothing, EventArgs.Empty)
            Else
                MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Ref
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class