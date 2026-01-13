
Public Class Frm_Ftp
    Private mRootUrl As String = ""

    Public Sub New(ByVal sUrl As String, ByVal sUserName As String, ByVal sPwd As String)
        MyBase.New()
        Me.InitializeComponent()

        mRootUrl = sUrl
        TextBoxUrl.Text = sUrl
        TextBoxUserName.Text = sUserName
        TextBoxPwd.Text = sPwd
        LoadTree()
    End Sub

    Private Sub ButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConnect.Click
        LoadTree()
    End Sub

    Private Sub LoadTree()
        Cursor = Cursors.WaitCursor
        TreeView1.Nodes.Clear()
        Dim sUrl As String = TextBoxUrl.Text
        Dim sUserName As String = TextBoxUserName.Text
        Dim sPwd As String = TextBoxPwd.Text
        Dim oArrayList As ArrayList = maxisrvr.FtpOld.ListDirectory(sUserName, sPwd, sUrl)
        For Each s As String In oArrayList
            TreeView1.Nodes.Add(s)
        Next
        Cursor = Cursors.Default
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Dim sExt As String = System.IO.Path.GetExtension(e.Node.Text)
        If sExt = "" Then
            TextBoxUrl.Text = System.IO.Path.Combine(mRootUrl, e.Node.Text)
            LoadTree()
        Else
            Dim oDlg As New SaveFileDialog()
            With oDlg
                .Title = "FTP DOWNLOAD"
                .FileName = System.IO.Path.GetFileName(e.Node.Text)
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim sSrcFile As String = System.IO.Path.Combine(mRootUrl, e.Node.Text)
                    maxisrvr.FtpOld.Download(.FileName, sSrcFile, TextBoxUserName.Text, TextBoxPwd.Text)
                End If
            End With
        End If
    End Sub
End Class