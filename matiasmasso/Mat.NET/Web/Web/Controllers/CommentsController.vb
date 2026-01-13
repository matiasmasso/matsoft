Public Class CommentsController
    Inherits _MatController



    Async Function Update(model As CommentViewModel) As Threading.Tasks.Task(Of CommentViewModel.Results)
        Dim exs As New List(Of Exception)

        'model.Password = "" 'provisional

        Dim retval As CommentViewModel.Results = CommentViewModel.Results.NotSet
        If String.IsNullOrEmpty(model.EmailAddress) Then
            'email en blanc
            retval = CommentViewModel.Results.EmptyEmail
        ElseIf ContextHelper.GetUser IsNot Nothing AndAlso ContextHelper.GetUser.EmailAddress = model.EmailAddress Then
            'usuari loguejat, desem comentari
            Dim oPostComment = model.PostComment(ContextHelper.GetUser, ContextHelper.Lang)
            If Await FEB2.PostComment.Update(exs, oPostComment) Then
                Await FEB2.PostComment.EmailPendingModeration(exs, GlobalVariables.Emp, oPostComment)
                retval = CommentViewModel.Results.Success
            Else
                retval = CommentViewModel.Results.SysError
            End If
        ElseIf Not String.IsNullOrEmpty(model.Password) AndAlso Not String.IsNullOrEmpty(model.EmailAddress) Then
            'verificar email
            Dim oPasswordValidationModel = Await FEB2.User.ValidatePassword(exs, GlobalVariables.Emp, model.EmailAddress, model.Password)
            Dim oUser As DTOUser = oPasswordValidationModel.User
            Dim oPwdResult = oPasswordValidationModel.Result

            If oPwdResult = DTOUser.ValidationResults.success Then
                'obre sessio i desa el comentari
                Session("User") = oUser
                If String.IsNullOrEmpty(oUser.NickName) And String.IsNullOrEmpty(model.Nickname) Then
                    retval = CommentViewModel.Results.NicknameRequest
                Else
                    If oUser.NickName = "" And model.Nickname > "" Then
                        oUser.NickName = model.Nickname
                        Await FEB2.User.Update(exs, oUser)
                    End If
                    Dim oPostComment = model.PostComment(oUser, ContextHelper.Lang)
                    If Await FEB2.PostComment.Update(exs, oPostComment) Then
                        Await FEB2.PostComment.EmailPendingModeration(exs, GlobalVariables.Emp, oPostComment)
                        retval = CommentViewModel.Results.Success
                    Else
                        retval = CommentViewModel.Results.SysError
                    End If
                End If
            ElseIf oPwdResult = DTOUser.ValidationResults.emailNotRegistered Then
                retval = CommentViewModel.Results.EmailNotFound
            ElseIf oPwdResult = DTOUser.ValidationResults.newValidatedUser Then
                'activació Ok, demana el nickname
                If model.Nickname = "" Then
                    retval = CommentViewModel.Results.NicknameRequest
                Else
                    'desa el new User i el Comment
                    oUser = DTOUser.Factory(GlobalVariables.Emp,, model.EmailAddress)
                    With oUser
                        .nickName = model.Nickname
                        .lang = ContextHelper.Lang
                        .password = model.Password
                        .rol = New DTORol(DTORol.Ids.lead)
                        .fchCreated = Now
                        .fchActivated = Now
                        Select Case model.TargetSrc
                            Case DTOPostComment.ParentSources.Blog
                                .source = DTOUser.Sources.wpComment
                            Case DTOPostComment.ParentSources.News, DTOPostComment.ParentSources.Noticia
                                .source = DTOUser.Sources.webComment
                        End Select
                    End With
                    If Await FEB2.User.Update(exs, oUser) Then
                        Session("User") = oUser
                        Dim oPostComment = model.PostComment(oUser, ContextHelper.Lang)
                        If Await FEB2.PostComment.Update(exs, oPostComment) Then
                            Await FEB2.PostComment.EmailPendingModeration(exs, GlobalVariables.Emp, oPostComment)
                            retval = CommentViewModel.Results.Success
                        Else
                            retval = CommentViewModel.Results.SysError
                        End If
                    Else
                        retval = CommentViewModel.Results.SysError
                    End If

                End If

            ElseIf oPwdResult = DTOUser.ValidationResults.wrongPassword Then
                retval = CommentViewModel.Results.WrongPassword
            Else
                retval = CommentViewModel.Results.SysError
            End If

        ElseIf Not DTOUser.IsEmailNameAddressValid(model.EmailAddress) Then
            'email incorrecte
            retval = CommentViewModel.Results.WrongEmail
        Else
            'email valid pero Usuari no loguejat
            Dim oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, model.EmailAddress)
            If exs.Count = 0 Then
                If oUser Is Nothing Then
                    'usuari no registrat
                    retval = CommentViewModel.Results.EmailNotFound
                Else
                    'usuari registrat, demanem contrassenya
                    retval = CommentViewModel.Results.PasswordRequest

                End If
            Else
                retval = CommentViewModel.Results.SysError
            End If

        End If
        Return retval
    End Function

    Async Function PartialTree(target As Guid, targetSrc As DTOPostComment.ParentSources, take As Integer, from As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang()
        Dim oTarget As New DTOBaseGuid(target)
        Dim model = Await FEB2.PostComments.Tree(exs, oTarget, targetSrc, oLang, take, from)
        If exs.Count = 0 Then
            If from = 0 Then
                Return PartialView("_CommentsTree", model) 'retorna la secció complerta de comentaris
            Else
                Return PartialView("_CommentThreads", model) 'retorna els comentaris pelats sense header ni footer
            End If
        Else
            Return PartialView("_Error", exs)
        End If
    End Function
End Class