Public Class Menu_NewsletterSource

    Inherits Menu_Base

    Private _NewsletterSource As DTONewsletterSource


    Public Sub New(ByVal oNewsletterSource As DTONewsletterSource)
        MyBase.New()
        _NewsletterSource = oNewsletterSource
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Dim retval As ToolStripMenuItem() = Nothing
        Select Case _NewsletterSource.Cod
            Case DTONewsletterSource.Cods.News
                Dim oNoticia As DTONoticia = _NewsletterSource.Tag

            Case DTONewsletterSource.Cods.Blog
                Dim oBlogPost As DTOBlogPost = _NewsletterSource.Tag
            Case DTONewsletterSource.Cods.Events
                Dim oEvent As DTOEvento = _NewsletterSource.Tag
            Case DTONewsletterSource.Cods.Promo
                Dim oIncentiu As DTOIncentiu = _NewsletterSource.Tag
        End Select
        Return retval
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





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Select Case _NewsletterSource.Cod
            Case DTONewsletterSource.Cods.News
                Dim oNoticia As New DTONoticia(_NewsletterSource.Tag)
                Dim oFrm As New Frm_Noticia(oNoticia)
                oFrm.Show()
            Case DTONewsletterSource.Cods.Promo
                Dim oIncentiu = Await FEB2.Incentiu.Find(exs, _NewsletterSource.Tag.guid)
                If exs.Count = 0 Then
                    Dim oFrm As New Frm_Incentiu(oIncentiu)
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
        'Dim oFrm As New Frm_NewsletterSource(_NewsletterSource)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub




End Class


