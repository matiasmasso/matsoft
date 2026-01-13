Public Class Xl_FtpExplorer
    Private _Ftp As BLL.FTPclient
    Private _LoadedNodes As List(Of String)

    Public Event NodeMouseClick(sender As Object, e As MatEventArgs)
    Public Event NodeMouseDoubleClick(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oFtpClient As BLL.FTPclient) 'Optional sRootUrl As String = "")
        _Ftp = oFtpClient
        '_Ftp = New FTPclient(sRootUrl)
        _LoadedNodes = New List(Of String)
        Dim oNode As TreeNode = TreeView1.Nodes.Add(_Ftp.Hostname)
    End Sub

    Private Sub LoadNode(oParentNode As TreeNode)
        Dim sUrl As String = GetUrlFromNode(oParentNode)
        Dim oFtpDirectory As BLL.FTPdirectory = _Ftp.ListDirectoryDetail(sUrl)
        For Each sNom As String In _Ftp.Directoris(sUrl)
            oParentNode.Nodes.Add(sNom)
        Next
    End Sub

    Private Function GetUrlFromNode(oNode As TreeNode) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oNode.Text)
        Do Until oNode.Parent Is Nothing
            oNode = oNode.Parent
            sb.Insert(0, oNode.Text & "/")
        Loop
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim sUrl As String = GetUrlFromNode(e.Node)
        RaiseEvent NodeMouseClick(Me, New MatEventArgs(sUrl))
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Cursor = Cursors.WaitCursor
        Dim sUrl As String = GetUrlFromNode(e.Node)
        If Not _LoadedNodes.Exists(Function(x) x = sUrl) Then
            _LoadedNodes.Add(sUrl)
            RaiseEvent NodeMouseDoubleClick(Me, New MatEventArgs(sUrl))
            LoadNode(e.Node)
            e.Node.Expand()
        End If
        Cursor = Cursors.Default
    End Sub
End Class
