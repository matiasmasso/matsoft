Public Class Menu_Noticia
    Private _Noticia As Noticia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oNoticia As Noticia)
        MyBase.New()
        _Noticia = oNoticia
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_Web(), _
                                         MenuItem_CopyMailLink(), _
                                         MenuItem_Mailing(), _
                                         MenuItem_Logs() _
        })
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyMailLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a Mailing"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_CopyMailLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Mailing() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mailing"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Mailing
        Return oMenuItem
    End Function

    Private Function MenuItem_Logs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Logs"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Logs
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Noticia(_Noticia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(BLL_Noticia.Url(_Noticia, True))
    End Sub

    Private Sub Do_CopyMailLink(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim sUrl As String =BLL.BLLMailing.BodyUrl(Templates., sEmail) _Noticia.BodyUrlForMailing()
        Dim sUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.Noticia, _Noticia.Guid.ToString)

        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_Mailing()
        Dim sUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.Noticia, _Noticia.Guid.ToString)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_Logs()
        Dim sTitle As String = "Logs de la noticia '" & _Noticia.Title & "'"
        Dim oLogs As List(Of DTOWebLogBrowse) = BLL.BLLWebLogBrowses.All(_Noticia) ' NoticiaLoader.WebLogs(_Noticia)
        Dim oFrm As New Frm_WebLogs(oLogs, sTitle)
        AddHandler oFrm.RequestToRefresh, AddressOf refreshLogs
        AddHandler oFrm.RequestToReset, AddressOf resetLogs
        oFrm.Show()
    End Sub

    Private Sub refreshLogs(sender As Object, e As MatEventArgs)
        Dim oFrm As Frm_WebLogs = sender
        Dim oLogs As List(Of DTOWebLogBrowse) = BLL.BLLWebLogBrowses.All(_Noticia) ' NoticiaLoader.WebLogs(_Noticia)
        oFrm.RefreshLogs(oLogs)
    End Sub

    Private Sub resetLogs()
        BLL_WebLogs.reset(_Noticia.Guid)
        RefreshRequest()
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

End Class



