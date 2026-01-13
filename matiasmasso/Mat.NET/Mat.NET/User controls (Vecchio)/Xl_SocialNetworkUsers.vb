

Public Class Xl_SocialNetworkUsers
    Private mEmail As Email
    Private mAllowEvents As Boolean

    Private Enum Cols
        Network
        UserId
        Image
        UserName
        FirstName
        LastName
    End Enum

 
    Public WriteOnly Property Email As Email
        Set(value As Email)
            mEmail = value
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Network, UserId, Image, UserName, FirstName, LastName FROM SocialNetworkUsers WHERE Email like @Email"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@email", mEmail.Adr)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            .DataSource = oTb
            .AllowUserToAddRows = False
            .Columns(0).Width = 48
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .RowTemplate.Height = 48
            .ReadOnly = True
            With .Columns(Cols.Network)
                .Visible = False
            End With
            With .Columns(Cols.UserId)
                .Visible = False
            End With
            With .Columns(Cols.Image)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 48
            End With
            With .Columns(Cols.UserName)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.FirstName)
                .Visible = False
            End With
            With .Columns(Cols.LastName)
                .Visible = False
            End With
        End With

        SetContextMenu()
    End Sub


    Private Function CurrentUser() As SocialNetworkUser
        Dim retval As SocialNetworkUser = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oNetwork As SocialNetworkUser.Networks = CType(oRow.Cells(Cols.Network).Value, SocialNetworkUser.Networks)
            Dim sUserId As String = oRow.Cells(Cols.UserId).Value.ToString
            retval = New SocialNetworkUser(oNetwork, sUserId)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem
        Dim oNSUser As SocialNetworkUser = CurrentUser()

        If oNSUser IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.iExplorer, AddressOf Do_Zoom)
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem = New ToolStripMenuItem("perfil", My.Resources.iExplorer, AddressOf Do_ViewProfile)
            oContextMenu.Items.Add(oMenuItem)
        End If

        oMenuItem = New ToolStripMenuItem("afegir")
        oContextMenu.Items.Add(oMenuItem)
        For Each oNetwork As SocialNetworkUser.Networks In [Enum].GetValues(GetType(SocialNetworkUser.Networks))
            If oNetwork <> SocialNetworkUser.Networks.NotSet Then
                oMenuItem.DropDownItems.Add(oNetwork.ToString, Nothing, AddressOf Add_SocialNetwork)
            End If
        Next

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Image
                If IsDBNull(e.Value) Then
                    e.Value = My.Resources.empty
                End If
            Case Cols.UserName
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim sNetWork As String = CType(oRow.Cells(Cols.Network).Value, SocialNetworkUser.Networks).ToString
                Dim sUsername As String = e.Value
                Dim sName As String = oRow.Cells(Cols.FirstName).Value & " " & oRow.Cells(Cols.LastName).Value
                e.Value = sNetWork & vbCrLf & sUsername & vbCrLf & sName
                e.CellStyle.WrapMode = DataGridViewTriState.True
        End Select
    End Sub

    Private Sub Do_Zoom(sender As Object, e As System.EventArgs)
        Dim oNSUser As SocialNetworkUser = CurrentUser()
        Dim oFrm As New Frm_SocialNetworkUser(oNSUser)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ViewProfile(sender As Object, e As System.EventArgs)
        Dim oNSUser As SocialNetworkUser = CurrentUser()
        UIHelper.ShowHtml(oNSUser.Url)
    End Sub

    Private Sub Add_SocialNetwork(sender As Object, e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim sNetworkName As String = oMenuItem.Text
        Dim oNetwork As SocialNetworkUser.Networks
        [Enum].TryParse(Of SocialNetworkUser.Networks)(oMenuItem.Text, oNetwork)
        Dim oNSUser As New SocialNetworkUser(mEmail, oNetwork)


        Dim oFrm As New Frm_SocialNetworkUser(oNSUser)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            SetContextMenu()
        End If
    End Sub

    Public Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index
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
