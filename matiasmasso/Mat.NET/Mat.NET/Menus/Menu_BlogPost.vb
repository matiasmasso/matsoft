Public Class Menu_BlogPost
    Private _BlogPost As DTOBlogpost

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oBlogPost As DTOBlogpost)
        MyBase.New()
        _BlogPost = oBlogPost
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_EmailsSuscrits(), _
        MenuItem_EmailsNoSuscrits(), _
        MenuItem_CopyLinkToMailBody()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_EmailsSuscrits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar Pdf"
        oMenuItem.Image = My.Resources.clip
        AddHandler oMenuItem.Click, AddressOf Do_PostEmailsSubscribed
        Return oMenuItem
    End Function

    Private Function MenuItem_EmailsNoSuscrits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Visualitzar Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_PostEmailsNotSubscribed
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLinkToMailBody() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(_BlogPost.VirtualPath)
    End Sub


    Private Sub Do_PostEmailsNotSubscribed()
        Dim iId As Integer = _BlogPost.Id
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "exportar emails del post no suscrits al blog"
            .Filter = "documents csv (*.csv)|*.csv"
            .FileName = "emailsNoSuscritsDelPost_" & iId.ToString & ".csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SQL As String = "SELECT comment_author_email FROM OPENQUERY(WORDPRESS995,'SELECT comment_post_ID,comment_author,comment_author_email FROM wp_comments WHERE comment_approved=1 and comment_author_email>''''') " _
                                    & "WHERE comment_post_ID=@Id AND comment_author_email collate Modern_Spanish_CI_AS not in (SELECT EMAIL FROM WPSUBSCRIPTORS) GROUP BY comment_author_email ORDER BY comment_author_email"
                Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@Id", iId)
                Dim writeFile As System.IO.TextWriter = New System.IO.StreamWriter(.FileName)
                Dim iCount As Integer
                Do While oDrd.Read
                    Dim sEmail As String = oDrd("comment_author_email").ToString
                    writeFile.WriteLine(sEmail)
                    iCount += 1
                Loop
                oDrd.Close()
                writeFile.Flush()
                writeFile.Close()
                writeFile = Nothing
                MsgBox(iCount & "emails no suscrits")
            End If

        End With

    End Sub

    Private Sub Do_PostEmailsSubscribed()
        Dim iId As Integer = _BlogPost.Id
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "exportar emails del post suscrits al blog"
            .Filter = "documents csv (*.csv)|*.csv"
            .FileName = "emailsSuscritsDelPost_" & iId.ToString & ".csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SQL As String = "SELECT comment_author_email FROM OPENQUERY(WORDPRESS995,'SELECT comment_post_ID,comment_author,comment_author_email FROM wp_comments WHERE comment_approved=1 and comment_author_email>''''') " _
                                    & "WHERE comment_post_ID=@Id AND comment_author_email collate Modern_Spanish_CI_AS  in (SELECT EMAIL FROM WpSUBSCRIPTORS) " _
                                    & "GROUP BY comment_author_email ORDER BY comment_author_email"
                Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@Id", iId)
                Dim writeFile As System.IO.TextWriter = New System.IO.StreamWriter(.FileName)
                Dim iCount As Integer
                Do While oDrd.Read
                    Dim sEmail As String = oDrd("comment_author_email").ToString
                    writeFile.WriteLine(sEmail)
                    iCount += 1
                Loop
                oDrd.Close()
                writeFile.Flush()
                writeFile.Close()
                writeFile = Nothing
                MsgBox(iCount & "emails no suscrits")
            End If

        End With

    End Sub



    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.Blogpost, _BlogPost.Id.ToString)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


