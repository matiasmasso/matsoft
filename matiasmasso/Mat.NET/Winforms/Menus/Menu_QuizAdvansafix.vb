Public Class Menu_QuizAdvansafix

    Inherits Menu_Base

    Private _QuizAdvansafix As DTOQuizAdvansafix

    Public Sub New(ByVal oQuizAdvansafix As DTOQuizAdvansafix)
    MyBase.New()
    _QuizAdvansafix = oQuizAdvansafix
End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Email(),
        MenuItem_SignUp(),
        MenuItem_CopyLink(),
        MenuItem_Delete()})
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

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email"
        AddHandler oMenuItem.Click, AddressOf Do_Email
        Return oMenuItem
    End Function

    Private Function MenuItem_SignUp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Formulari web"
        AddHandler oMenuItem.Click, AddressOf Do_SignUp
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a Formulari web"
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_QuizAdvansafix(_QuizAdvansafix)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _QuizAdvansafix.Customer.FullNom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLQuizAdvansafix.Delete(_QuizAdvansafix, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_SignUp()
        Dim oUser As DTOUser = GetUser()

        If oUser Is Nothing Then
            UIHelper.WarnError("Client sense emails registrats")
        Else
            Dim sUrl As String = BLL.BLLQuizAdvansafix.Url(oUser, True)
            UIHelper.ShowHtml(sUrl)
        End If
    End Sub
    Private Sub Do_CopyLink()
        Dim oUser As DTOUser = GetUser()

        If oUser Is Nothing Then
            UIHelper.WarnError("Client sense emails registrats")
        Else
            Dim sUrl As String = BLL.BLLQuizAdvansafix.Url(oUser, True)
            UIHelper.CopyLink(sUrl)
        End If
    End Sub

    Private Sub Do_Email()
        Dim oUser As DTOUser = GetUser()

        If oUser Is Nothing Then
            UIHelper.WarnError("Client sense emails registrats")
        Else
            Dim oNoticia As DTONoticia = BLL.BLLNoticia.Find(New Guid("62D8E579-D123-4130-8910-17E788FA9EE4"))
            Dim sUrl As String = BLL.BLLNoticia.UrlMailingBody(oNoticia, True)
            Dim exs As New List(Of Exception)
            If Not MatOutlook.NewMessage(oUser.EmailAddress, "", , "La Advansafix baja de precio (Requiere acción por su parte)", , sUrl, , exs) Then
                UIHelper.WarnError(exs, "error al redactar missatge. Verificar " & sUrl)
            End If
        End If
    End Sub

    Private Function GetUser() As DTOUser
        Dim retval As DTOUser = _QuizAdvansafix.LastUser
        If retval Is Nothing Then
            Dim oUsers As List(Of DTOUser) = BLL.BLLUsers.All(_QuizAdvansafix.Customer)
            If oUsers.Count > 0 Then
                retval = oUsers(0)
            End If
        End If
        Return retval
    End Function

End Class