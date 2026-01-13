Public Class Frm_PostComments

    Private _AllowEvents As Boolean

    Private Enum Cols
        Fch
        User
        Post
        Text
    End Enum

    Private Sub Frm_PostComments_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBoxStatus.SelectedIndex = PostComment.StatusEnum.Pendent
        LoadGrid()
        _AllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim oStatus As PostComment.StatusEnum = CurrentStatus()
        Dim oDataSource As PostComments = CommentsLoader.All(oStatus)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("Fch", "Data")
            .Columns.Add("User", "Usuari")
            .Columns.Add("Post", "Post")
            .Columns.Add("Text", "Text")
            .DataSource = oDataSource
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Fch)
                .DataPropertyName = "Fch"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            With .Columns(Cols.User)
                .DataPropertyName = "Nickname"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(Cols.Post)
                .DataPropertyName = "ParentTitle"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(Cols.Text)
                .DataPropertyName = "Text"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            SetContextMenu()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Post

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oItem As PostComment = CurrentItem()

        If oItem IsNot Nothing Then
            Dim oMenu As New Menu_PostComment(oItem)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
        End If

        oContextMenu.Items.Add("Afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("Refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentStatus() As PostComment.StatusEnum
        Dim retval As PostComment.StatusEnum = ComboBoxStatus.SelectedIndex
        Return retval
    End Function

    Private Function CurrentItem() As PostComment
        Dim retval As PostComment = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = DataGridView1.CurrentRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub ComboBoxStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxStatus.SelectedIndexChanged
        LoadGrid()
    End Sub


    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oItem As New PostComment(Guid.NewGuid)
        'Dim oFrm As New Frm_PostComment(oItem)
        'AddHandler oFrm.AfterUpdate, AddressOf OnNewItemAdded
        'oFrm.Show()
    End Sub

    Private Sub OnNewItemAdded(sender As Object, e As EventArgs)
        'Dim oItem As PostComment = sender
        'Dim oItems As PriceListItems_Customer = DataGridView1.DataSource
        'oItems.Add(oItem)
        'RefreshRequest(sender, e)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oItem As PostComment = CurrentItem()
        Dim oFrm As New Frm_PostComment(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

End Class