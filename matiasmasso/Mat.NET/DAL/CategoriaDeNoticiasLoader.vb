Public Class CategoriaDeNoticiaLoader

    Shared Function Find(oGuid As Guid) As DTOCategoriaDeNoticia
        Dim retval As DTOCategoriaDeNoticia = Nothing
        Dim oCategoriaDeNoticia As New DTOCategoriaDeNoticia(oGuid)
        If Load(oCategoriaDeNoticia) Then
            retval = oCategoriaDeNoticia
        End If
        Return retval
    End Function

    Shared Function FromNom(sNom As String) As DTOCategoriaDeNoticia
        Dim retval As DTOCategoriaDeNoticia = Nothing
        Dim SQL As String = "SELECT * FROM CategoriaDeNoticia WHERE Nom COLLATE Latin1_General_CI_AI = @Nom"
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@Nom", sNom)
        If oDrd.Read Then
            retval = New DTOCategoriaDeNoticia(oDrd("Guid"))
            With retval
                .Nom = sNom
                If Not IsDBNull(oDrd("Excerpt")) Then
                    .Excerpt = oDrd("Excerpt")
                End If
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oCategoriaDeNoticia As DTOCategoriaDeNoticia) As Boolean
        If Not oCategoriaDeNoticia.IsLoaded And Not oCategoriaDeNoticia.IsNew Then

            Dim SQL As String = "SELECT * FROM CategoriaDeNoticia WHERE Guid=@Guid"
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oCategoriaDeNoticia.Guid.ToString())
            If oDrd.Read Then
                With oCategoriaDeNoticia
                    .Nom = oDrd("Nom")
                    If Not IsDBNull(oDrd("Excerpt")) Then
                        .Excerpt = oDrd("Excerpt")
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCategoriaDeNoticia.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCategoriaDeNoticia As DTOCategoriaDeNoticia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCategoriaDeNoticia, oTrans)
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

    Shared Sub Update(oCategoriaDeNoticia As DTOCategoriaDeNoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM CategoriaDeNoticia WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", oCategoriaDeNoticia.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCategoriaDeNoticia.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCategoriaDeNoticia
            oRow("Nom") = .Nom
            oRow("Excerpt") = .Excerpt
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCategoriaDeNoticia As DTOCategoriaDeNoticia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCategoriaDeNoticia, oTrans)
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


    Shared Sub Delete(oCategoriaDeNoticia As DTOCategoriaDeNoticia, ByRef oTrans As SqlTransaction)
        With oCategoriaDeNoticia
            Dim SQL As String = "DELETE CategoriaDeNoticia WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@GUID", oCategoriaDeNoticia.Guid.ToString())
        End With
    End Sub

End Class

Public Class CategoriasDeNoticiaLoader
    Shared Function All() As List(Of DTOCategoriaDeNoticia)
        Dim SQL As String = "SELECT Guid, Nom, Excerpt FROM CategoriaDeNoticia ORDER BY Nom"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of DTOCategoriaDeNoticia)
        Do While oDrd.Read
            Dim item As New DTOCategoriaDeNoticia(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Excerpt = oDrd("Excerpt")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ForSitemap() As List(Of DTOCategoriaDeNoticia)
        Dim retVal As New List(Of DTOCategoriaDeNoticia)

        Dim SQL As String = "SELECT CategoriaDeNoticia.Guid, CategoriaDeNoticia.Nom, Max(Noticia.FchLastEdited) AS LastFch " _
        & "FROM Noticia " _
        & "INNER JOIN NoticiaCategoria ON Noticia.Guid=NoticiaCategoria.Noticia " _
        & "INNER JOIN CategoriaDeNoticia ON NoticiaCategoria.Categoria=CategoriaDeNoticia.Guid " _
        & "GROUP BY CategoriaDeNoticia.Guid, CategoriaDeNoticia.Nom " _
        & "ORDER BY CategoriaDeNoticia.Nom, LastFch DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As New Guid(oDrd("Guid").ToString())
            Dim item As New DTOCategoriaDeNoticia(oGuid)
            With item
                .Nom = oDrd("Nom")
                .FchLastEdited = oDrd("LastFch")
            End With
            retVal.Add(item)
        Loop
        oDrd.Close()
        Return retVal
    End Function

End Class



