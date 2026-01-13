Public Class PostCommentLoader
    Shared Function Find(oGuid As Guid) As DTOPostComment
        Dim retval As DTOPostComment = Nothing
        Dim oPostComment As New DTOPostComment(oGuid)
        If Load(oPostComment) Then
            retval = oPostComment
        End If
        Return retval

    End Function

    Shared Function Load(oPostComment As DTOPostComment) As Boolean
        If Not oPostComment.IsLoaded Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT C1.Fch AS C1Fch, C1.[User] AS C1User, C1.Text AS C1Text ")
            sb.AppendLine(", C1.Parent AS C1Parent, C1.ParentSource AS C1ParentSource, C1.AnswerRoot AS C1AnswerRoot ")
            sb.AppendLine(", C1.Lang AS C1Lang, C1.FchApproved AS C1FchApproved, C1.FchDeleted AS C1FchDeleted ")
            sb.AppendLine(", C3.Guid AS C3Guid, C3.Fch AS C3Fch, C3.[User] AS C3User, C3.Text AS C3Text ")
            sb.AppendLine(", C1User.Adr AS C1UserEmail, C1User.Nom AS C1UserNom, C1User.Cognoms AS C1UserCognoms, C1User.Nickname AS C1UserNickname, C1User.Rol AS C1UserRol ")
            sb.AppendLine(", C1User.BirthYea AS C1UserBirthYea, C1User.Location AS C1UserLocationGuid, C1UserLocation.Nom AS C1UserLocationNom, C1User.FchCreated AS C1UserFchCreated, C1User.Tel AS C1UserTel ")
            sb.AppendLine(", C3User.Adr AS C3UserEmail, C3User.Nom AS C3UserNom, C3User.Nickname AS C3UserNickname ")
            sb.AppendLine(", VwLangText.Esp AS TitleEsp, VwLangText.Cat AS TitleCat, VwLangText.Eng AS TitleEng, VwLangText.Por AS TitlePor, VwLangText.Src AS LangTextSrc")
            sb.AppendLine("FROM PostComment C1 ")                                       'original
            sb.AppendLine("LEFT OUTER JOIN PostComment C3 ON C1.Answering = C3.Guid ")  'Answering source
            sb.AppendLine("LEFT OUTER JOIN Email C1User ON C1.[User] = C1User.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Location C1UserLocation ON C1User.Location = C1UserLocation.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email C3User ON C3.[User] = C3User.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText ON C1.Parent = VwLangText.Guid AND (VwLangText.Src=" & DTOLangText.Srcs.ContentTitle & " OR VwLangText.Src=" & DTOLangText.Srcs.BlogTitle & ") ")
            sb.AppendLine("WHERE C1.Guid='" & oPostComment.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oUser = New DTOUser(oDrd("C1User"))
                With oUser
                    .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("C1UserEmail"))
                    .nickName = SQLHelper.GetStringFromDataReader(oDrd("C1UserNickname"))
                    .nom = SQLHelper.GetStringFromDataReader(oDrd("C1UserNom"))
                    .cognoms = SQLHelper.GetStringFromDataReader(oDrd("C1UserCognoms"))
                    .lang = SQLHelper.GetLangFromDataReader(oDrd("C1Lang"))
                    If Not IsDBNull(oDrd("C1UserRol")) Then
                        .rol = New DTORol(oDrd("C1UserRol"))
                    End If
                    .tel = SQLHelper.GetStringFromDataReader(oDrd("C1UserTel"))
                    .birthYear = SQLHelper.GetIntegerFromDataReader(oDrd("C1UserBirthYea"))
                    .fchCreated = SQLHelper.GetFchFromDataReader(oDrd("C1UserFchCreated"))
                    If Not IsDBNull(oDrd("C1UserLocationGuid")) Then
                        .location = New DTOLocation(oDrd("C1UserLocationGuid"))
                        .location.nom = SQLHelper.GetStringFromDataReader(oDrd("C1UserLocationNom"))
                    End If
                End With

                With oPostComment
                    .Parent = DirectCast(oDrd("C1Parent"), Guid)
                    .ParentTitle = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    Dim oSrc As DTOLangText.Srcs = oDrd("LangTextSrc")
                    Select Case oSrc
                        Case DTOLangText.Srcs.ContentTitle
                            .ParentSource = DTOPostComment.ParentSources.Noticia
                        Case DTOLangText.Srcs.BlogTitle
                            .ParentSource = DTOPostComment.ParentSources.Blog
                    End Select
                    .User = oUser
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("C1Lang"))
                    .Fch = CDate(oDrd("C1Fch"))
                    .Text = oDrd("C1Text").ToString
                    .FchApproved = SQLHelper.GetFchFromDataReader(oDrd("C1FchApproved"))
                    .FchDeleted = SQLHelper.GetFchFromDataReader(oDrd("C1FchDeleted"))
                    If Not IsDBNull(oDrd("C3Guid")) Then
                        .Answering = New DTOPostComment(DirectCast(oDrd("C3Guid"), Guid))
                        With .Answering
                            .User = New DTOUser(oDrd("C3User"))
                            With .User
                                .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("C3UserEmail"))
                                .NickName = SQLHelper.GetStringFromDataReader(oDrd("C3UserNickname"))
                                .Nom = SQLHelper.GetStringFromDataReader(oDrd("C3UserNom"))
                            End With
                            .Fch = CDate(oDrd("C3Fch"))
                            .Text = SQLHelper.GetStringFromDataReader(oDrd("C3Text"))
                        End With
                    End If
                    If Not IsDBNull(oDrd("C1AnswerRoot")) Then
                        .AnswerRoot = New DTOPostComment(DirectCast(oDrd("C1AnswerRoot"), Guid))
                    End If
                    .IsLoaded = True
                End With

            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPostComment.IsLoaded
        Return retval
    End Function


    Shared Function Update(oComment As DTOPostComment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oComment, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oComment As DTOPostComment, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM PostComment WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", oComment.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oComment.Guid
            oRow("Parent") = oComment.Parent
            oRow("ParentSource") = oComment.ParentSource
        Else
            oRow = oTb.Rows(0)
        End If

        With oComment
            oRow("Fch") = .Fch
            oRow("User") = SQLHelper.NullableBaseGuid(.User)
            oRow("Lang") = SQLHelper.NullableLang(.Lang)
            oRow("Text") = .Text
            oRow("Answering") = SQLHelper.NullableBaseGuid(.Answering)
            oRow("AnswerRoot") = SQLHelper.NullableBaseGuid(.AnswerRoot)
            oRow("FchApproved") = SQLHelper.NullableFch(.FchApproved)
            oRow("FchDeleted") = SQLHelper.NullableFch(.FchDeleted)
        End With
        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oComment As DTOPostComment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oComment, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oComment As DTOPostComment, ByRef oTrans As SqlTransaction)
        With oComment
            Dim SQL As String = "DELETE PostComment WHERE Guid='" & oComment.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End With
    End Sub


End Class

Public Class PostCommentsLoader
    Shared Function All(oStatus As DTOPostComment.StatusEnum, Optional oParentGuid As Guid = Nothing) As List(Of DTOPostComment)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PostComment.* ")
        sb.AppendLine(", Email.Nom, Email.Cognoms, Email.Nickname, Email.adr, Email.Rol  ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(" FROM PostComment ")
        sb.AppendLine("INNER JOIN Email ON PostComment.[User]=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Noticia ON PostComment.Parent=Noticia.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        Dim SQLWHERE As String = ""
        Select Case oStatus
            Case DTOPostComment.StatusEnum.Aprobat
                SQLWHERE = " PostComment.FchApproved IS NOT NULL AND PostComment.FchDeleted IS NULL "
            Case DTOPostComment.StatusEnum.Pendent
                SQLWHERE = " PostComment.FchApproved IS NULL AND PostComment.FchDeleted IS NULL "
            Case DTOPostComment.StatusEnum.Eliminat
                SQLWHERE = " PostComment.FchDeleted IS NOT NULL "
            Case DTOPostComment.StatusEnum.NotSet
        End Select

        If Not oParentGuid.Equals(Guid.Empty) Then
            If SQLWHERE > "" Then SQLWHERE = SQLWHERE & " AND "
            SQLWHERE = SQLWHERE & "PostComment.Parent='" & oParentGuid.ToString & "' "

            'ONLY TEST--------------------------------------------------------------------------------------------------------
            'SQLWHERE = SQLWHERE & " and eMAIL.Nickname='Angi' "
        End If

        If SQLWHERE > "" Then
            sb.AppendLine(" WHERE " & SQLWHERE & " ")
        End If

        sb.AppendLine("ORDER BY PostComment.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim retval As New List(Of DTOPostComment)
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("User"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
                .Rol = New DTORol(oDrd("Rol"))
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                .NickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
            End With

            Dim oGuid As Guid = oDrd("Guid")
            Dim oItem As New DTOPostComment(oGuid)
            With oItem
                Try
                    If Not IsDBNull(oDrd("Parent")) Then
                        .Parent = oDrd("Parent")
                    End If
                    .ParentSource = oDrd("ParentSource")
                    .ParentTitle = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")

                    .Fch = oDrd("Fch")
                    .Text = oDrd("Text").ToString
                    .User = oUser
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))

                    If Not IsDBNull(oDrd("Answering")) Then
                        .Answering = New DTOPostComment(DirectCast(oDrd("Answering"), Guid))
                    End If

                    If Not IsDBNull(oDrd("AnswerRoot")) Then
                        .AnswerRoot = New DTOPostComment(DirectCast(oDrd("AnswerRoot"), Guid))
                    End If

                    If Not IsDBNull(oDrd("FchApproved")) Then
                        .FchApproved = CDate(oDrd("FchApproved"))
                    End If

                    If Not IsDBNull(oDrd("FchDeleted")) Then
                        .FchApproved = CDate(oDrd("FchDeleted"))
                    End If

                Catch ex As Exception
                    Stop
                End Try
            End With

            retval.Add(oItem)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function ForFeed(fchFrom As Date, oDomain As DTOWebDomain) As List(Of DTOPostComment)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PostComment.* ")
        sb.AppendLine(", Email.Nom, Email.Cognoms, Email.Nickname, Email.adr, Email.Rol  ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(" FROM PostComment ")
        sb.AppendLine("INNER JOIN Email ON PostComment.[User]=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Noticia ON PostComment.Parent=Noticia.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("WHERE PostComment.FchApproved IS NOT NULL AND PostComment.FchDeleted IS NULL ")

        sb.AppendLine("AND PostComment.Fch > '" & fchFrom.ToString("yyyyMMdd") & "' ")
        sb.AppendLine("ORDER BY PostComment.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim retval As New List(Of DTOPostComment)
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("User"), Guid))
            With oUser
                .emailAddress = oDrd("Adr")
                .rol = New DTORol(oDrd("Rol"))
                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                .nickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
            End With

            Dim oGuid As Guid = oDrd("Guid")
            Dim oItem As New DTOPostComment(oGuid)
            With oItem
                .Parent = DirectCast(oDrd("Parent"), Guid)
                .ParentSource = oDrd("ParentSource")
                .ParentTitle = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")

                .Fch = oDrd("Fch")
                .Text = oDrd("Text").ToString
                .User = oUser
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))

                If Not IsDBNull(oDrd("Answering")) Then
                    .Answering = New DTOPostComment(DirectCast(oDrd("Answering"), Guid))
                End If

                If Not IsDBNull(oDrd("AnswerRoot")) Then
                    .AnswerRoot = New DTOPostComment(DirectCast(oDrd("AnswerRoot"), Guid))
                End If

                If Not IsDBNull(oDrd("FchApproved")) Then
                    .FchApproved = CDate(oDrd("FchApproved"))
                End If

                If Not IsDBNull(oDrd("FchDeleted")) Then
                    .FchApproved = CDate(oDrd("FchDeleted"))
                End If
            End With

            retval.Add(oItem)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function TreeModel(oTarget As DTOBaseGuid, oLang As DTOLang, Optional take As Integer = 0, Optional from As Integer = 0, Optional includeComment As DTOPostComment = Nothing) As DTOPostComment.TreeModel
        Dim retval As New DTOPostComment.TreeModel
        With retval
            .Target = oTarget
            .Take = take
            .From = from
        End With

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT C1.Guid AS C1Guid, C1.Fch AS C1Fch, C1.[User] AS C1User, C1.Text AS C1Text ")
        sb.AppendLine(", C2.Guid AS C2Guid, C2.Fch AS C2Fch, C2.[User] AS C2User, C2.Text AS C2Text ")
        sb.AppendLine(", C3.Guid AS C3Guid, C3.Fch AS C3Fch, C3.[User] AS C3User ")
        sb.AppendLine(", C1User.Adr AS C1UserEmail, C1User.Nom AS C1UserNom, C1User.Nickname AS C1UserNickname ")
        sb.AppendLine(", C2User.Adr AS C2UserEmail, C2User.Nom AS C2UserNom, C2User.Nickname AS C2UserNickname ")
        sb.AppendLine(", C3User.Adr AS C3UserEmail, C3User.Nom AS C3UserNom, C3User.Nickname AS C3UserNickname ")
        sb.AppendLine("FROM PostComment C1 ")
        sb.AppendLine("LEFT OUTER JOIN PostComment C2 ON C1.Guid = C2.AnswerRoot AND C2.FchApproved IS NOT NULL ")
        sb.AppendLine("LEFT OUTER JOIN PostComment C3 ON C2.Answering = C3.Guid ")
        sb.AppendLine("INNER JOIN Email C1User ON C1.[User] = C1User.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email C2User ON C2.[User] = C2User.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email C3User ON C3.[User] = C3User.Guid ")
        sb.AppendLine("WHERE C1.Parent='" & oTarget.Guid.ToString() & "' ")
        sb.AppendLine("AND C1.AnswerRoot IS NULL ")
        sb.AppendLine("AND C1.FchApproved IS NOT NULL ")
        sb.AppendLine("AND C1.FchDeleted IS NULL ")

        If oLang.Tag = "POR" Then
            sb.AppendLine("AND C1.Lang ='POR' ")
        Else
            sb.AppendLine("AND (C1.Lang ='ESP' OR C1.Lang='CAT' OR C1.Lang = 'ENG') ")
        End If
        'sb.AppendLine("ORDER BY C1.FCH DESC, C1.Guid, C2.Fch DESC ")
        sb.AppendLine("ORDER BY C1.FCH DESC, C1.Guid, C2.Fch ")
        Dim SQL = sb.ToString

        Dim oComment As New DTOPostComment()
        Dim includedCommentFound As Boolean = False
        Dim item As DTOPostComment.TreeModel.Item = Nothing
        Dim oUser As DTOUser = Nothing
        Dim inRange As Boolean
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oComment.Guid.Equals(oDrd("C1Guid")) Then
                oComment = New DTOPostComment(oDrd("C1Guid"))

                If includeComment Is Nothing Then
                    Dim afterRange = retval.Items.Count >= take
                    Dim beforeRange = retval.RootItemsCount < from
                    inRange = beforeRange = False And afterRange = False
                Else
                    inRange = Not includedCommentFound
                    If oComment.Guid.Equals(includeComment.Guid) Then
                        includedCommentFound = True
                        'stop on next run when all current thread has been read
                    End If
                End If

                If inRange Then
                    With oComment
                        .User = New DTOUser(oDrd("C1User"))
                        With .User
                            .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("C1UserEmail"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("C1UserNickname"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("C1UserNom"))
                        End With
                        .Fch = oDrd("C1Fch")
                        .Text = oDrd("C1Text")
                        .AnswerRoot = New DTOPostComment(oDrd("C1Guid"))
                    End With
                    Dim threadRoot = DTOPostComment.TreeModel.Item.Factory(oComment)
                    retval.Items.Add(threadRoot)
                End If
                retval.RootItemsCount += 1
            End If

            If inRange = True Then
                If Not IsDBNull(oDrd("C2Guid")) Then
                    Dim oChild = New DTOPostComment(oDrd("C2Guid"))
                    If includeComment IsNot Nothing AndAlso oChild.Guid.Equals(includeComment.Guid) Then
                        includedCommentFound = True
                    End If
                    With oChild
                        .User = New DTOUser(oDrd("C2User"))
                        With .User
                            .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("C2UserEmail"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("C2UserNickname"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("C2UserNom"))
                        End With
                        .Fch = oDrd("C2Fch")
                        .Text = oDrd("C2Text")
                        .AnswerRoot = New DTOPostComment(oDrd("C1Guid"))

                        If Not IsDBNull(oDrd("C3Guid")) Then
                            .Answering = New DTOPostComment(oDrd("C3Guid"))
                            With .Answering
                                .User = New DTOUser(oDrd("C3User"))
                                With .User
                                    .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("C3UserEmail"))
                                    .nickName = SQLHelper.GetStringFromDataReader(oDrd("C3UserNickname"))
                                    .nom = SQLHelper.GetStringFromDataReader(oDrd("C3UserNom"))
                                End With
                                .Fch = oDrd("C3Fch")
                            End With
                        End If

                        Dim threadChild = DTOPostComment.TreeModel.Item.Factory(oChild)
                        retval.Items.Last.Items.Add(threadChild)
                    End With
                End If
            End If
        Loop

        oDrd.Close()

        Return retval

    End Function

End Class
