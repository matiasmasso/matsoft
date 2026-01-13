
Imports System.Data

Public Class Frm_Fiscal_Mod145
    Private mStaff As DTOStaff = Nothing
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Baja
        Cli
        Abr
        Fch
        Hash
    End Enum

    Public Sub New(Optional oStaff As DTOStaff = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mStaff = oStaff
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CLIDOCS.guid, (CASE WHEN BAJA IS NULL THEN 0 ELSE 1 END) AS BAJA, CliStaff.Guid, CliStaff.Abr, CLIDOCS.fch, CliDocs.Hash " _
        & "FROM            CliStaff " _
        & "LEFT OUTER JOIN CLIDOCS ON CliStaff.Guid = CLIDOCS.Contact AND CLIDOCS.src = 8 "

        If mStaff IsNot Nothing Then
            SQL = SQL & " WHERE CliDocs.Contact='" & mStaff.Guid.ToString & "' "
        End If

        SQL = SQL & "GROUP BY CLIDOCS.guid, CliStaff.Guid, CliStaff.Abr, CliStaff.Baja, CLIDOCS.fch, CliDocs.Hash " _
        & "ORDER BY CliStaff.Baja, CliStaff.Abr, CLIDOCS.fch DESC"


        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
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

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Hash)
                .Visible = False
            End With

            With .Columns(Cols.Baja)
                .Visible = False
            End With

            With .Columns(Cols.Cli)
                .Visible = False
            End With

            With .Columns(Cols.Abr)
                .HeaderText = "empleat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With

        End With
    End Sub

    Private Function CurrentItm() As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = New DTODocFile(oRow.Cells(Cols.Guid).Value.ToString)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(Cols.Guid).Value)
            If oGuid <> Nothing Then
                'oMenuItem = New ToolStripMenuItem("model 145")
                'Dim oDropDownMenu As New Menu_Bigfile(New BigFileSrc(DTODocFile.Cods.CliDoc, oGuid))
                'oMenuItem.DropDownItems.AddRange(oDropDownMenu.Range)
                'oMenuItem.DropDownItems.Add("afegir", Nothing, AddressOf Do_AddNew)

                'oContextMenuStrip.Items.Add(oMenuItem)
                'oContextMenuStrip.Items.Add("-")
            End If
            Dim oStaff As New DTOStaff(oRow.Cells(Cols.Cli).Value)
            Dim oMenu As New Menu_Contact(oStaff)
            oContextMenuStrip.Items.AddRange(oMenu.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Select Case CBool(oRow.Cells(Cols.Baja).Value)
            Case True
                e.CellStyle.BackColor = Color.LightGray
            Case False
                e.CellStyle.BackColor = Color.White
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(Cols.Guid).Value)
            If oGuid <> Nothing Then
                Zoom()
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Zoom()
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            'Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(Cols.Guid).Value)
            'Dim oBigFile As New BigFileSrc(DTODocFile.Cods.CliDoc, oGuid)
            'Dim oFrm As New Frm_BigFile(oBigFile)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If
    End Sub

    Private Sub RefreshRequest()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Abr
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

    Private Sub Do_AddNew()
        Dim oCliDoc As New DTOContactDoc()
        oCliDoc.Contact = mStaff
        oCliDoc.Fch = Today
        oCliDoc.Type = DTOContactDoc.Types.Model_145
        Dim oFrm As New Frm_ContactDoc(oCliDoc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

End Class