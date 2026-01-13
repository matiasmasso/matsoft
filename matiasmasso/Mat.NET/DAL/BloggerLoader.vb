Public Class BloggerLoader
    Shared Function Find(oGuid As Guid) As DTOBlogger
        Dim retval As DTOBlogger = Nothing
        Dim oBlogger As New DTOBlogger(oGuid)
        If Load(oBlogger) Then
            retval = oBlogger
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBlogger As DTOBlogger) As Boolean
        If Not oBlogger.IsLoaded And Not oBlogger.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Blogger.Guid, Blogger.Title, Blogger.Url, Blogger.Author, Blogger.Logo ")
            sb.AppendLine(", Email.Guid, Email.Adr ")
            sb.AppendLine(", Bloggerpost.Guid as PostGuid, Bloggerpost.Title as PostTitle, Bloggerpost.Lang as PostLang, Bloggerpost.Url as postUrl, Bloggerpost.fch as PostFch ")
            sb.AppendLine("FROM Blogger ")
            sb.AppendLine("LEFT OUTER JOIN Email ON Blogger.Author = Email.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bloggerpost ON Blogger.Guid = Bloggerpost.Blogger ")
            sb.AppendLine("WHERE Blogger.Guid='" & oBlogger.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Bloggerpost.fch DESC")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oBlogger.IsLoaded Then
                    With oBlogger
                        .Title = oDrd("Title")
                        .Url = oDrd("Url")
                        If Not IsDBNull(oDrd("Author")) Then
                            .Author = New DTOUser(DirectCast(oDrd("Author"), Guid))
                            With .Author
                                .EmailAddress = oDrd("Adr")
                            End With
                        End If
                        If Not IsDBNull(oDrd("Logo")) Then
                            .Logo = oDrd("Logo")
                        End If
                        .Posts = New List(Of DTOBloggerPost)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("PostGuid")) Then
                    Dim oPost As New DTOBloggerPost(oDrd("PostGuid"))
                    With oPost
                        .Blogger = oBlogger
                        .Title = oDrd("PostTitle")
                        .Url = oDrd("PostUrl")
                        .Lang = SQLHelper.GetLangFromDataReader(oDrd("PostLang"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("PostFch")).Date
                    End With
                    oBlogger.Posts.Add(oPost)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oBlogger.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBlogger As DTOBlogger, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oBlogger, oTrans)
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

    Shared Sub Update(oBlogger As DTOBlogger, ByRef oTrans As SqlTransaction)
        With oBlogger
            If .Title.Length > 200 Then Throw New Exception("Title no pot ser mes llarg de 200 caracters")
            If .Url.Length > 200 Then Throw New Exception("Url no pot ser mes llarga de 200 caracters")
        End With

        Dim SQL As String = "SELECT * FROM Blogger WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBlogger.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBlogger.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBlogger
            oRow("Title") = .Title
            oRow("Url") = .Url
            If .Author Is Nothing Then
                oRow("Author") = System.DBNull.Value
            Else
                oRow("Author") = .Author.Guid
            End If
            If .Logo Is Nothing Then
                oRow("Logo") = System.DBNull.Value
            Else
                oRow("Logo") = .Logo
            End If
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oBlogger As DTOBlogger, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBlogger, oTrans)
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


    Shared Sub Delete(oBlogger As DTOBlogger, ByRef oTrans As SqlTransaction)
        With oBlogger
            Dim SQL As String = "DELETE Blogger WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBlogger.Guid.ToString())
        End With
    End Sub
End Class

Public Class BloggersLoader
    Shared Function All() As List(Of DTOBlogger)
        Dim retval As New List(Of DTOBlogger)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Blogger.Guid, Blogger.Title, Blogger.Url, Blogger.Author, Email.Guid, Email.Adr ")
        sb.AppendLine("FROM Blogger ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Blogger.Author = Email.Guid ")
        sb.AppendLine("ORDER BY Blogger.Title")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBlogger As New DTOBlogger(oDrd("Guid"))
            With oBlogger
                .Title = oDrd("Title")
                .Url = oDrd("Url")
                If Not IsDBNull(oDrd("Author")) Then
                    .Author = New DTOUser(DirectCast(oDrd("Author"), Guid))
                    With .Author
                        .EmailAddress = oDrd("Adr")
                    End With
                End If
            End With
            retval.Add(oBlogger)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class

Public Class BloggerpostLoader
    Shared Function Find(oGuid As Guid) As DTOBloggerPost
        Dim retval As DTOBloggerPost = Nothing
        Dim oPost As New DTOBloggerPost(oGuid)
        If Load(oPost) Then
            retval = oPost
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPost As DTOBloggerPost) As Boolean
        If Not oPost.IsLoaded And Not oPost.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BloggerPost.*")
            sb.AppendLine(", Blogger.Title as BloggerTitle, Blogger.Author ")
            sb.AppendLine("FROM BloggerPost ")
            sb.AppendLine("INNER JOIN Blogger ON Bloggerpost.blogger = Blogger.Guid ")
            sb.AppendLine("WHERE BloggerPost.Guid='" & oPost.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPost
                    .Blogger = New DTOBlogger(oDrd("Blogger"))
                    With .Blogger
                        .Title = oDrd("BloggerTitle")
                        If Not IsDBNull(oDrd("Author")) Then
                            .Author = New DTOUser(DirectCast(oDrd("Author"), Guid))
                        End If
                    End With
                    .Title = oDrd("Title")
                    .Url = oDrd("Url")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch")).Date
                    .HighlightFrom = SQLHelper.GetFchFromDataReader(oDrd("HighlightFrom")).Date
                    .HighlightTo = SQLHelper.GetFchFromDataReader(oDrd("HighlightTo")).Date
                    .Products = New List(Of DTOProduct)
                    .IsLoaded = True
                End With

                oDrd.Close()

                sb = New System.Text.StringBuilder
                sb.AppendLine("SELECT VwProductNom.*, BloggerpostProduct.product ")
                sb.AppendLine("FROM BloggerpostProduct ")
                sb.AppendLine("INNER JOIN VwProductNom ON BloggerpostProduct.Product = VwProductNom.Guid ")
                sb.AppendLine("WHERE BloggerpostProduct.Post='" & oPost.Guid.ToString & "' ")
                SQL = sb.ToString
                oDrd = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim oProduct As DTOProduct = SQLHelper.GetProductFromDataReader(oDrd)
                    oPost.Products.Add(oProduct)
                Loop

            End If

            oDrd.Close()

        End If



        Dim retval As Boolean = oPost.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPost As DTOBloggerPost, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oPost, oTrans)
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

    Shared Sub Update(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        UpdateHeader(oPost, oTrans)
        UpdateProducts(oPost, oTrans)
    End Sub

    Shared Sub UpdateHeader(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        With oPost
            If .Title.Length > 200 Then Throw New Exception("Title no pot ser mes llarg de 200 caracters")
            If .Url.Length > 200 Then Throw New Exception("Url no pot ser mes llarga de 200 caracters")
        End With

        Dim SQL As String = "SELECT * FROM Bloggerpost WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPost.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPost.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPost
            oRow("Blogger") = .Blogger.Guid
            oRow("Title") = .Title
            oRow("Url") = .Url
            oRow("Lang") = .Lang.Tag
            oRow("Fch") = .Fch
            If .HighlightFrom = Nothing Then
                oRow("HighlightFrom") = System.DBNull.Value
            Else
                oRow("HighlightFrom") = .HighlightFrom
            End If
            If .HighlightTo = Nothing Then
                oRow("HighlightTo") = System.DBNull.Value
            Else
                oRow("HighlightTo") = .HighlightTo
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateProducts(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        If Not oPost.IsNew Then DeleteProducts(oPost, oTrans)

        Dim SQL As String = "SELECT * FROM BloggerpostProduct WHERE Post=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPost.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oProduct As DTOProduct In oPost.Products
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Post") = oPost.Guid
            oRow("Product") = oProduct.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPost As DTOBloggerPost, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPost, oTrans)
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

    Shared Sub Delete(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        DeleteProducts(oPost, oTrans)
        DeleteHeader(oPost, oTrans)
    End Sub

    Shared Sub DeleteHeader(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        With oPost
            Dim SQL As String = "DELETE Bloggerpost WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPost.Guid.ToString())
        End With
    End Sub

    Shared Sub DeleteProducts(oPost As DTOBloggerPost, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BloggerpostProduct WHERE Post=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPost.Guid.ToString())
    End Sub
End Class

Public Class BloggerpostsLoader
    Shared Function FromProductOrParent(oProduct As DTOProduct) As List(Of DTOBloggerPost)
        Dim retval As New List(Of DTOBloggerPost)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Blogger.Guid as BloggerGuid, Blogger.Title AS BloggerTitle ")
        sb.AppendLine(", Bloggerpost.Guid as PostGuid, Bloggerpost.title AS PostTitle, Bloggerpost.Lang AS PostLang, Bloggerpost.url AS PostUrl, Bloggerpost.Fch AS PostFch ")
        sb.AppendLine("FROM BloggerpostProduct ")
        sb.AppendLine("INNER JOIN VwProductParent ON VwProductParent.Parent=BloggerpostProduct.Product ")
        sb.AppendLine("INNER JOIN Bloggerpost ON BloggerpostProduct.Post=Bloggerpost.guid ")
        sb.AppendLine("INNER JOIN Blogger ON Bloggerpost.Blogger=Blogger.guid ")
        sb.AppendLine("WHERE VwProductParent.Child='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Bloggerpost.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBlogger As New DTOBlogger(oDrd("BloggerGuid"))
            With oBlogger
                .Title = oDrd("BloggerTitle")
            End With
            Dim oPost As New DTOBloggerPost(oDrd("PostGuid"))
            With oPost
                .Blogger = oBlogger
                .Title = oDrd("PostTitle")
                .Url = oDrd("PostUrl")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("PostLang"))
                .Fch = oDrd("PostFch")
            End With
            retval.Add(oPost)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function HighlightedPosts(Optional DtFch As Date = Nothing, Optional oLang As DTOLang = Nothing) As List(Of DTOBloggerPost)
        Dim retval As New List(Of DTOBloggerPost)

        Dim sb As New System.Text.StringBuilder
        Dim sFch As String = Format(DtFch, "yyyyMMdd")
        sb.AppendLine("SELECT Blogger.Guid as BloggerGuid, Blogger.Title AS BloggerTitle ")
        sb.AppendLine(", Bloggerpost.Guid as PostGuid, Bloggerpost.title AS PostTitle, Bloggerpost.Lang AS PostLang, Bloggerpost.url AS PostUrl, Bloggerpost.Fch AS PostFch, Bloggerpost.HighlightFrom, Bloggerpost.HighlightTo ")
        sb.AppendLine("FROM Bloggerpost ")
        sb.AppendLine("INNER JOIN Blogger ON Bloggerpost.Blogger=Blogger.guid ")
        sb.AppendLine("WHERE 1=1 ")
        If DtFch <> Nothing Then
            sb.AppendLine("AND '" & sFch & "' BETWEEN HighlightFrom AND HighlightTo ")
        End If
        If oLang IsNot Nothing Then
            sb.AppendLine("AND BloggerPost.Lang = '" & oLang.Tag & "' ")
        End If
        sb.AppendLine("ORDER BY Bloggerpost.HighlightFrom DESC, Bloggerpost.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBlogger As New DTOBlogger(oDrd("BloggerGuid"))
            With oBlogger
                .Title = oDrd("BloggerTitle")
            End With
            Dim oPost As New DTOBloggerPost(oDrd("PostGuid"))
            With oPost
                .Blogger = oBlogger
                .Title = oDrd("PostTitle")
                .Url = oDrd("PostUrl")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("PostLang"))
                .Fch = oDrd("PostFch")
                .HighlightFrom = SQLHelper.GetFchFromDataReader(oDrd("HighlightFrom"))
                .HighlightTo = SQLHelper.GetFchFromDataReader(oDrd("HighlightTo"))
            End With
            retval.Add(oPost)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
