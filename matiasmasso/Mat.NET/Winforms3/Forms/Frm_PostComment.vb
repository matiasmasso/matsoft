Public Class Frm_PostComment
    Private _Comment As DTOPostComment
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oComment As DTOPostComment)
        InitializeComponent()
        _Comment = oComment

    End Sub

    Private Async Sub Frm_PostComment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        With ComboBoxStatus.Items
            .Add("(tria un estat)")
            .Add("pendent de moderador")
            .Add("aprobat")
            .Add("descartat")
        End With
        If FEB.PostComment.Load(exs, _Comment) Then

            With _Comment
                Select Case .ParentSource
                    Case DTOPostComment.ParentSources.Blog
                        Dim oBlogPost = Await FEB.BlogPost.Find(exs, .Parent)
                        If oBlogPost IsNot Nothing Then TextBoxSource.Text = oBlogPost.Title.Tradueix(Current.Session.Lang)
                    Case DTOPostComment.ParentSources.Noticia, DTOPostComment.ParentSources.News
                        Dim oNoticia = Await FEB.Noticia.Find(exs, .Parent)
                        If oNoticia IsNot Nothing Then TextBoxSource.Text = oNoticia.Title.Tradueix(Current.Session.Lang)
                End Select
                ComboBoxStatus.SelectedIndex = .Status()
                TextBoxSource.Text = .ParentTitle.Tradueix(Current.Session.Lang)
                TextBoxComment.Text = .Text
                With .User
                    TextBoxNickname.Text = .nickName
                    TextBoxEmail.Text = .emailAddress
                    TextBoxNom.Text = .nom
                    TextBoxCognoms.Text = .cognoms
                    TextBoxLocation.Text = .FullLocation(.lang)
                    TextBoxTel.Text = .tel
                    TextBoxBirth.Text = .FullBirthAge(Current.Session.Lang)
                    TextBoxLog.Text = .FullLogText(Current.Session.Lang)
                End With
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            ComboBoxStatus.SelectedIndexChanged,
             TextBoxComment.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If Await Save() Then
            Me.Close()
        End If
    End Sub

    Private Async Sub ButtonReply_Click(sender As Object, e As EventArgs) Handles ButtonReply.Click
        If ButtonOk.Enabled Then
            If Not Await Save() Then Exit Sub
        End If

        Dim oFrm As New frm_PostCommentAnswer(_Comment)
        oFrm.show()
        Me.Close()
    End Sub

    Private Async Function Save() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim oNewStatus As DTOPostComment.StatusEnum = ComboBoxStatus.SelectedIndex
        If oNewStatus = DTOPostComment.StatusEnum.NotSet Then
            exs = {New Exception("cal triar un estat del desplegable")}.ToList()
        Else
            With _Comment
                .Text = TextBoxComment.Text

                Select Case oNewStatus
                    Case DTOPostComment.StatusEnum.Aprobat
                        .FchDeleted = Nothing
                        .FchApproved = DTO.GlobalVariables.Now()
                    Case DTOPostComment.StatusEnum.Eliminat
                        .FchDeleted = DTO.GlobalVariables.Now()
                        .FchApproved = Nothing
                    Case DTOPostComment.StatusEnum.Pendent
                        .FchDeleted = Nothing
                        .FchApproved = Nothing
                End Select

            End With

            UIHelper.ToggleProggressBar(PanelButtons, True)
            Await FEB.PostComment.Update(exs, _Comment)
            UIHelper.ToggleProggressBar(PanelButtons, False)
        End If

        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Comment))
        Else
            UIHelper.WarnError(exs)
        End If

        Return exs.Count = 0
    End Function
End Class