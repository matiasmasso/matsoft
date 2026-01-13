Public Class Menu_Noticia
    Inherits Menu_Base

    Private _Noticia As DTONoticiaBase

    Public Sub New(ByVal oNoticia As DTONoticiaBase)
        MyBase.New()
        _Noticia = oNoticia
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_CopyDirectLink())
        MyBase.AddMenuItem(MenuItem_CopyMailLink())
        MyBase.AddMenuItem(MenuItem_Mailing())
        MyBase.AddMenuItem(MenuItem_ProLeads())
        MyBase.AddMenuItem(MenuItem_Logs())
    End Sub

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyDirectLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç directe"
        AddHandler oMenuItem.Click, AddressOf Do_CopyDirectLink
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

    Private Function MenuItem_ProLeads() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "llistat professionals"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ProLeads
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

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim url = FEB2.Noticia.UrlFriendly(_Noticia, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_CopyDirectLink()
        Dim sUrl = FEB2.UrlHelper.Factory(True, "news", _Noticia.Guid.ToString())
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_CopyMailLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.Noticia, _Noticia.Guid.ToString())
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_Mailing()
        Dim sUrl As String = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.Noticia, _Noticia.Guid.ToString())
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Async Sub Do_ProLeads()
        Dim exs As New List(Of Exception)
        If FEB2.Noticia.Load(exs, _Noticia) Then
            Dim oBrands As New List(Of DTOProductBrand)
            If DirectCast(_Noticia, DTONoticia).Product IsNot Nothing Then
                Dim oBrand = FEB2.Product.Brand(exs, DirectCast(_Noticia, DTONoticia).Product)
                oBrands.Add(oBrand)
            End If
            Dim oChannels As List(Of DTODistributionChannel)
            If _Noticia.Professional Then
                oChannels = _Noticia.DistributionChannels
            Else
                oChannels = Await FEB2.DistributionChannels.Headers(GlobalVariables.Emp, Current.Session.Lang, exs)
            End If

            If exs.Count = 0 Then
                Dim oFrm As New Frm_MailingPros(oChannels, oBrands)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Logs()
        Dim exs As New List(Of Exception)
        Dim sTitle As String = "Logs de la noticia '" & _Noticia.Title.Tradueix(DTOLang.CAT) & "'"
        Dim oLogs = Await FEB2.WebLogBrowses.All(exs, _Noticia)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_WebLogs(oLogs, sTitle)
            AddHandler oFrm.RequestToRefresh, AddressOf refreshLogs
            AddHandler oFrm.RequestToReset, AddressOf resetLogs
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refreshLogs(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oFrm As Frm_WebLogs = sender
        Dim oLogs = Await FEB2.WebLogBrowses.All(exs, _Noticia) ' NoticiaLoader.WebLogs(_Noticia)
        If exs.Count = 0 Then
            oFrm.RefreshLogs(oLogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub resetLogs()
        'BLL_WebLogs.reset(_Noticia.Guid)
        'RefreshRequest()
    End Sub

End Class



