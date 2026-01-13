Imports LegacyHelper

Public Class GalleryItemLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOGalleryItem
        Dim retval As DTOGalleryItem = Nothing
        Dim oGalleryItem As New DTOGalleryItem(oGuid)
        If Load(oGalleryItem) Then
            retval = oGalleryItem
        End If
        Return retval
    End Function

    Shared Function ImageMime(ByRef oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Image, Mime ")
        sb.AppendLine("FROM Gallery ")
        sb.AppendLine("WHERE Guid='" & oGuid.ToString() & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New ImageMime
            retval.ByteArray = oDrd("Image")
            Dim oMime = oDrd("Mime")
            If oMime <> MimeCods.NotSet Then retval.Mime = oMime
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function ThumbnailMime(ByRef oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Thumbnail, Mime ")
        sb.AppendLine("FROM Gallery ")
        sb.AppendLine("WHERE Guid='" & oGuid.ToString() & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New ImageMime
            retval.ByteArray = oDrd("Thumbnail")
            Dim oMime = oDrd("Mime")
            If oMime <> MimeCods.NotSet Then retval.Mime = oMime
        End If

        oDrd.Close()
        Return retval
    End Function


    Shared Function Load(ByRef oGalleryItem As DTOGalleryItem) As Boolean
        If Not oGalleryItem.IsLoaded And Not oGalleryItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Hash, Nom, Width, Height, Mime, Kb, FchCreated ")
            sb.AppendLine("FROM Gallery ")
            sb.AppendLine("WHERE Guid='" & oGalleryItem.Guid.ToString() & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oGalleryItem
                    .Hash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
                    .Nom = oDrd("Nom").ToString
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .Mime = oDrd("Mime")
                    If .Mime = MimeCods.NotSet Then .Mime = MimeCods.Jpg
                    .Size = oDrd("Kb")
                    .FchCreated = oDrd("FchCreated")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oGalleryItem.IsLoaded
        Return retval
    End Function

    Shared Function FromHash(hash As String) As DTOGalleryItem
        Dim retval As DTOGalleryItem = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Nom, Width, Height, Mime, Kb, FchCreated ")
        sb.AppendLine("FROM Gallery ")
        sb.AppendLine("WHERE Hash='" & hash & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOGalleryItem(oDrd("Guid"))
            With retval
                .Guid = oDrd("Guid")
                .Hash = hash
                .Nom = oDrd("Nom").ToString
                .Width = oDrd("Width")
                .Height = oDrd("Height")
                .Mime = oDrd("Mime")
                If .Mime = MimeCods.NotSet Then .Mime = MimeCods.Jpg
                .Size = oDrd("Kb")
                .FchCreated = oDrd("FchCreated")
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oGalleryItem As DTOGalleryItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oGalleryItem, oTrans)
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


    Shared Sub Update(oGalleryItem As DTOGalleryItem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Gallery ")
        sb.AppendLine("WHERE Guid='" & oGalleryItem.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oGalleryItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        If String.IsNullOrEmpty(oGalleryItem.Hash) Then
            oGalleryItem.Hash = MatHelperStd.CryptoHelper.HashMD5(oGalleryItem.Image)
        End If

        With oGalleryItem
            Dim oImage = LegacyHelper.ImageHelper.FromBytes(.Image)
            .Thumbnail = ImageHelper.GetThumbnailToFill(oImage, DTOGalleryItem.THUMBNAIL_WIDTH, DTOGalleryItem.THUMBNAIL_HEIGHT).Bytes

            oRow("Hash") = .Hash
            oRow("Nom") = .Nom
            oRow("Image") = .Image
            oRow("Thumbnail") = .Thumbnail
            oRow("Height") = .Height
            oRow("Width") = .Width
            oRow("Mime") = .Mime
            oRow("Kb") = .Size
            oRow("FchCreated") = .FchCreated
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oGalleryItem As DTOGalleryItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oGalleryItem, oTrans)
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


    Shared Sub Delete(oGalleryItem As DTOGalleryItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Gallery WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oGalleryItem.Guid.ToString())
    End Sub

#End Region

End Class

Public Class GalleryItemsLoader

    Shared Function All(Optional sSearchKey As String = "") As List(Of DTOGalleryItem)
        Dim retval As New List(Of DTOGalleryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Hash, Nom, Width, Height, Mime, Kb, FchCreated ")
        sb.AppendLine("FROM Gallery ")
        If sSearchKey > "" Then
            sb.AppendLine("WHERE Nom LIKE '%" & sSearchKey & "%' ")
        End If
        sb.AppendLine("ORDER BY FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOGalleryItem(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom").ToString
                .Hash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
                .Width = oDrd("Width")
                .Height = oDrd("Height")
                .Mime = oDrd("Mime")
                If .Mime = MimeCods.NotSet Then .Mime = MimeCods.Jpg
                .Size = oDrd("Kb")
                .FchCreated = oDrd("FchCreated")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
