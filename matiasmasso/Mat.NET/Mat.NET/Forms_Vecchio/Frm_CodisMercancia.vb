Public Class Frm_CodisMercancia
    Private mAllowEvents As Boolean
    Private mMode As Modes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Dsc
    End Enum

    Public Enum Modes
        NotSet
        ForSelection
        ForBrowsing
    End Enum

    Public Sub New(Optional ByVal oMode As Modes = Modes.ForBrowsing)
        MyBase.new()
        InitializeComponent()
        mMode = oMode
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT * FROM CODISMERCANCIA ORDER BY ID"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

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
            With .Columns(Cols.Id)
                .HeaderText = "codi"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
            End With
            With .Columns(Cols.Dsc)
                .HeaderText = "descripció"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Function CurrentItm() As maxisrvr.CodiMercancia
        Dim oItm As maxisrvr.CodiMercancia = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oItm = New maxisrvr.CodiMercancia(oRow.Cells(Cols.Id).Value)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As maxisrvr.CodiMercancia = CurrentItm()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", Nothing, AddressOf Zoom))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", Nothing, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case mMode
            Case Modes.ForSelection
                RaiseEvent AfterSelect(CurrentItm, EventArgs.Empty)
                Me.Close()
            Case Else
                Zoom()
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCodiMercancia As maxisrvr.CodiMercancia = maxisrvr.CodiMercancia.Empty
        Dim oFrm As New Frm_CodiMercancia(oCodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            '.CodiMercancia = oCodiMercancia
            .Show()
        End With
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_CodiMercancia(CurrentItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id
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