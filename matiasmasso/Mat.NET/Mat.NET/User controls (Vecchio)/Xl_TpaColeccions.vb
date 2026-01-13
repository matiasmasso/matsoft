

Public Class Xl_TpaColeccions
    Private mAllowEvents As Boolean
    Private mTpa As Tpa
    Private mSelectionMode As BLL.Defaults.SelectionModes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Cols
        Guid
        Nom
        Obsoleto
    End Enum

    Public WriteOnly Property SelectionMode As BLL.Defaults.SelectionModes
        Set(value As BLL.Defaults.SelectionModes)
            mSelectionMode = value
        End Set
    End Property

    Public WriteOnly Property Tpa As Tpa
        Set(value As Tpa)
            If value IsNot Nothing Then
                mTpa = value
                LoadGrid()
                SetContextMenu()
                mAllowEvents = True
            End If
        End Set
    End Property

    Public ReadOnly Property Coleccion As Coleccion
        Get
            Dim retval As Coleccion = Nothing
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            If oRow IsNot Nothing Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New Coleccion(oGuid)
            End If
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT GUID, NOM, OBSOLETO FROM COLECCION " _
                            & "WHERE EMP=@EMP AND TPA=@TPA " _
                            & "ORDER BY NOM"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mTpa.emp.Id, "@TPA", mTpa.Id)
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
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With
        End With
    End Sub

    Private Function CurrentItm() As Coleccion
        Dim oItm As Coleccion = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As System.Guid = oRow.Cells(Cols.Guid).Value
            oItm = New Coleccion(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As Coleccion = CurrentItm()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("web", Nothing, AddressOf Do_Web))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            onProductSelect(Me, EventArgs.Empty)
        Else
            Zoom()
        End If
    End Sub

    Private Sub onProductSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterSelect(sender, EventArgs.Empty)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oColeccion As New Coleccion
        'oColeccion.Tpa = mTpa
        'Dim oFrm As New Frm_Coleccion(oColeccion)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Zoom()
        'Dim oFrm As New Frm_Coleccion(CurrentItm())
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_Web()
        Dim sUrl As String = New Product(CurrentItm).Url(True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridView1
        Dim iFirstVisibleCell As Integer = Cols.Nom

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
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
