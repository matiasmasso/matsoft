Public Class SearchMiscLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSearchMisc
        Dim retval As DTOSearchMisc = Nothing
        Dim oSearchMisc As New DTOSearchMisc(oGuid)
        If Load(oSearchMisc) Then
            retval = oSearchMisc
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSearchMisc As DTOSearchMisc) As Boolean
        If Not oSearchMisc.IsLoaded And Not oSearchMisc.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Noticia.*, Keyword.Value as Tag ")
            sb.AppendLine("FROM Noticia ")
            sb.AppendLine("LEFT OUTER JOIN Keyword ON Keyword.SrcGuid = Noticia.Guid ")
            sb.AppendLine("WHERE Noticia.Guid='" & oSearchMisc.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

            Do While oDrd.Read
                If Not oSearchMisc.IsLoaded Then
                    With oSearchMisc
                        .Fch = oDrd("Fch")
                        .UrlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
                        .Title = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                        .Excerpt = SQLHelper.GetLangTextFromDataReader(oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                        .Src = oDrd("Cod")
                        .Keywords = New List(Of String)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Tag")) Then
                    oSearchMisc.Keywords.Add(oDrd("Tag"))
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oSearchMisc.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSearchMisc As DTOSearchMisc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSearchMisc, oTrans)
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

    Shared Sub Update(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        UpdateHeader(oSearchMisc, oTrans)
    End Sub

    Shared Sub UpdateHeader(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("WHERE Guid='" & oSearchMisc.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSearchMisc.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSearchMisc
            oRow("Cod") = DTONoticiaBase.Srcs.Misc
            oRow("Fch") = .Fch
            oRow("UrlFriendlySegment") = SQLHelper.NullableString(.UrlFriendlySegment)
            oRow("TitleEsp") = SQLHelper.NullableLangText(.Title, DTOLang.ESP)
            oRow("TitleCat") = SQLHelper.NullableLangText(.Title, DTOLang.CAT)
            oRow("TitleEng") = SQLHelper.NullableLangText(.Title, DTOLang.ENG)
            oRow("TitlePor") = SQLHelper.NullableLangText(.Title, DTOLang.POR)
            oRow("ExcerptEsp") = SQLHelper.NullableLangText(.Excerpt, DTOLang.ESP)
            oRow("ExcerptCat") = SQLHelper.NullableLangText(.Excerpt, DTOLang.CAT)
            oRow("ExcerptEng") = SQLHelper.NullableLangText(.Excerpt, DTOLang.ENG)
            oRow("ExcerptPor") = SQLHelper.NullableLangText(.Excerpt, DTOLang.POR)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateTags(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        DeleteTags(oSearchMisc, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Keyword ")
        sb.AppendLine("WHERE SrcGuid='" & oSearchMisc.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each sKeyword As String In oSearchMisc.Keywords
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("SrcGuid") = oSearchMisc.Guid
            'oRow("SrcCod") = Keyword.S
            oRow("Value") = sKeyword
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSearchMisc As DTOSearchMisc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSearchMisc, oTrans)
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

    Shared Sub Delete(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        DeleteTags(oSearchMisc, oTrans)
        DeleteHeader(oSearchMisc, oTrans)
    End Sub

    Shared Sub DeleteHeader(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Noticia WHERE Guid='" & oSearchMisc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteTags(oSearchMisc As DTOSearchMisc, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Keyword WHERE Keyword.SrcGuid ='" & oSearchMisc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SearchMiscsLoader

    Shared Function All() As List(Of DTOSearchMisc)
        Dim retval As New List(Of DTOSearchMisc)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.*, Keyword.Value as Tag ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN Keyword ON Keyword.SrcGuid = Noticia.Guid ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim item As New DTOSearchMisc()
        Do While oDrd.Read
            If Not item.Guid.Equals(oDrd("Guid")) Then
                item = New DTOSearchMisc(oDrd("Guid"))
                With item
                    .Fch = oDrd("Fch")
                    .UrlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
                    .Title = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    .Excerpt = SQLHelper.GetLangTextFromDataReader(oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                    .Src = oDrd("Cod")
                    .Keywords = New List(Of String)
                End With
            End If
            If Not IsDBNull(oDrd("Tag")) Then
                item.Keywords.Add(oDrd("Tag"))
            End If
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
