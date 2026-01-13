Public Class MediaResourceLoader


#Region "CRUD"

    Shared Function Find(guid As Guid) As DTOMediaResource
        Dim retval As DTOMediaResource = Nothing
        Dim oMediaResource As New DTOMediaResource(guid)
        If Load(oMediaResource) Then
            retval = oMediaResource
        End If
        Return retval
    End Function

    Shared Function FromHash(hash As String) As DTOMediaResource
        Dim retval As DTOMediaResource = Nothing
        Dim SQL = "SELECT Guid FROM MediaResource WHERE Hash = '" & hash & "'"
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then retval = New DTOMediaResource(oDrd("Guid"))
        oDrd.Close()
        If retval IsNot Nothing Then Load(retval)
        Return retval
    End Function

    Shared Function Load(ByRef oMediaResource As DTOMediaResource) As Boolean
        If Not oMediaResource.IsLoaded And Not oMediaResource.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT  MediaResource.Hash, MediaResource.Ord, MediaResource.Nom, MediaResource.Mime, MediaResource.Size, MediaResource.Pags, MediaResource.Width, MediaResource.Height ")
            sb.AppendLine(", MediaResource.Lang, MediaResource.LangSet ")
            sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
            sb.AppendLine(", MediaResource.HRes, MediaResource.VRes, MediaResource.Cod, MediaResource.Obsoleto ")
            sb.AppendLine(", MediaResource.FchCreated, MediaResource.UsrCreated, UsrCreated.Nom ")
            sb.AppendLine(", MediaResource.FchLastEdited, MediaResource.UsrLastEdited, UsrLastEdited.Nom ")
            sb.AppendLine("FROM MediaResource ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText ON MediaResource.Guid = VwLangText.Guid AND VwLangText.Src = " & CInt(DTOLangText.Srcs.MediaResource) & " ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrCreated ON MediaResource.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrLastEdited ON MediaResource.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE MediaResource.Guid='" & oMediaResource.Guid.ToString() & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oMediaResource
                    .Hash = oDrd("Hash")
                    .Ord = oDrd("Ord")
                    .Nom = oDrd("Nom")
                    .Mime = oDrd("Mime")
                    .Length = oDrd("Size")
                    .Pags = oDrd("Pags")
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .HRes = oDrd("HRes")
                    .VRes = oDrd("VRes")
                    .Cod = oDrd("Cod")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    .LangSet = New DTOLang.Set(oDrd("LangSet"))
                    SQLHelper.LoadLangTextFromDataReader(.Description, oDrd)
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .Obsolet = oDrd("Obsoleto")
                    .IsLoaded = True
                End With
            End If
            oDrd.Close()

            sb = New System.Text.StringBuilder
            sb.AppendLine("SELECT MediaResourceTarget.Target ")
            sb.AppendLine("FROM MediaResourceTarget ")
            sb.AppendLine("WHERE MediaResourceTarget.Parent ='" & oMediaResource.Guid.ToString() & "' ")
            SQL = sb.ToString
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oProduct As New Models.Base.GuidNom(oDrd("Target"))
                oMediaResource.Products.Add(oProduct)
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = oMediaResource.IsLoaded
        Return retval
    End Function


    Shared Function Thumbnail(ByRef oMediaResource As DTOMediaResource) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Thumbnail ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("WHERE Guid='" & oMediaResource.Guid.ToString() & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Thumbnail")
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oMediaResource As DTOMediaResource, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMediaResource, oTrans)
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

    Shared Sub Update(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        DeleteItems(oMediaResource, oTrans)
        UpdateHeader(oMediaResource, oTrans)
        UpdateItems(oMediaResource, oTrans)
        LangTextLoader.Update(oMediaResource.Description, oTrans)
    End Sub

    Shared Sub UpdateHeader(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("WHERE Guid ='" & oMediaResource.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With oMediaResource
                oRow("Guid") = .Guid
                oRow("Ord") = LastOrd(oTrans) + 1
            End With
        Else
            oRow = oTb.Rows(0)
            oRow("Ord") = oMediaResource.Ord
        End If

        With oMediaResource
            oRow("Hash") = .Hash
            oRow("Ord") = .Ord
            oRow("Mime") = .Mime
            oRow("Size") = .Length
            oRow("Pags") = .Pags
            oRow("Width") = .Width
            oRow("Height") = .Height
            oRow("HRes") = .HRes
            oRow("VRes") = .VRes
            oRow("Nom") = .Nom
            oRow("Cod") = .Cod
            oRow("Lang") = SQLHelper.NullableLang(.Lang)
            oRow("LangSet") = .LangSet.Value
            SQLHelper.SetUsrLog(.UsrLog, oRow)
            oRow("Obsoleto") = .Obsolet

            If .Thumbnail IsNot Nothing AndAlso .Thumbnail.Length > 0 Then
                oRow("Thumbnail") = .Thumbnail
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastOrd(ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Ord) AS LastOrd ")
        sb.AppendLine("FROM MediaResource ")
        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastOrd")) Then
                retval = CInt(oRow("LastOrd"))
            End If
        End If
        Return retval
    End Function

    Shared Sub UpdateItems(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM MediaResourceTarget ")
        sb.AppendLine("WHERE Parent ='" & oMediaResource.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oProduct In oMediaResource.Products
            Dim oRow = oTb.NewRow
            oRow("Parent") = oMediaResource.Guid
            oRow("Target") = oProduct.Guid
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oMediaResource As DTOMediaResource, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMediaResource, oTrans)
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

    Shared Sub Delete(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        LangTextLoader.Delete(oMediaResource.Description, oTrans)
        DeleteItems(oMediaResource, oTrans)
        DeleteHeader(oMediaResource, oTrans)
    End Sub

    Shared Sub DeleteItems(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE MediaResourceTarget WHERE Parent ='" & oMediaResource.Guid.ToString() & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oMediaResource As DTOMediaResource, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE MediaResource WHERE Guid ='" & oMediaResource.Guid.ToString() & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class MediaResourcesLoader

    Shared Function All(oProduct As DTOProduct) As List(Of DTOMediaResource)
        Dim retval As New List(Of DTOMediaResource)

        Dim sb As New System.Text.StringBuilder
        Dim sbWhere As New System.Text.StringBuilder
        sb.AppendLine("SELECT MediaResource.Guid, MediaResource.Hash, MediaResource.Ord, MediaResource.Nom, MediaResource.Mime, MediaResource.Size, MediaResource.Pags, MediaResource.Width, MediaResource.Height ")
        sb.AppendLine(", MediaResource.Lang, MediaResource.LangSet ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine(", MediaResource.HRes, MediaResource.VRes, MediaResource.Cod, MediaResource.FchCreated, MediaResource.Obsoleto ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("INNER JOIN MediaResourceTarget ON MediaResource.Guid=MediaResourceTarget.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON MediaResource.Guid = VwLangText.Guid AND VwLangText.Src = " & CInt(DTOLangText.Srcs.MediaResource) & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductParent Parents ON MediaResourceTarget.Target = Parents.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwProductParent Children ON MediaResourceTarget.Target = Children.Child ")
        sb.AppendLine("LEFT OUTER JOIN Art ON Children.Child = Art.Guid ")
        sb.AppendLine("WHERE (Parents.Child='" & oProduct.Guid.ToString & "' OR Children.Parent='" & oProduct.Guid.ToString & "') ")
        sb.AppendLine("AND (Art.Obsoleto IS NULL OR Art.Obsoleto = 0) ")
        sb.AppendLine("AND (Art.NoWeb IS NULL OR Art.NoWeb = 0) ")
        sb.AppendLine("GROUP BY MediaResource.Guid, MediaResource.Hash, MediaResource.Ord, MediaResource.Nom, MediaResource.Mime, MediaResource.Size, MediaResource.Pags, MediaResource.Width, MediaResource.Height ")
        sb.AppendLine(", MediaResource.Lang, MediaResource.LangSet ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine(", MediaResource.HRes, MediaResource.VRes, MediaResource.Cod, MediaResource.FchCreated, MediaResource.Obsoleto ")
        sb.AppendLine("ORDER BY MediaResource.Cod, MediaResource.FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMediaResource(oDrd("Guid"))
            With item
                .Hash = oDrd("Hash")
                .Guid = oDrd("Guid")
                .Ord = oDrd("Ord")
                .Nom = oDrd("Nom")
                .Mime = oDrd("Mime")
                .Length = oDrd("Size")
                .Pags = oDrd("Pags")
                .Width = oDrd("Width")
                .Height = oDrd("Height")
                .HRes = oDrd("HRes")
                .VRes = oDrd("VRes")
                .Cod = oDrd("Cod")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("LangSet"))
                SQLHelper.LoadLangTextFromDataReader(.Description, oDrd)
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                .Obsolet = oDrd("Obsoleto") <> 0
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ProductSpecific(oProduct As DTOProduct) As List(Of DTOMediaResource)
        Dim retval As New List(Of DTOMediaResource)

        Dim sb As New System.Text.StringBuilder
        Dim sbWhere As New System.Text.StringBuilder
        sb.AppendLine("SELECT MediaResource.* ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("INNER JOIN MediaResourceTarget ON MediaResource.Guid = MediaResourceTarget.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON MediaResource.Guid = VwLangText.Guid AND VwLangText.Src = " & CInt(DTOLangText.Srcs.MediaResource) & " ")
        sb.AppendLine("WHERE MediaResourceTarget.Target='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY MediaResource.Cod, MediaResource.Ord DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMediaResource(oDrd("Guid"))
            Try
                With item
                    .Hash = oDrd("Hash")
                    .Ord = oDrd("Ord")
                    .Nom = oDrd("Nom")
                    .Mime = oDrd("Mime")
                    .Length = oDrd("Size")
                    .Pags = oDrd("Pags")
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .HRes = oDrd("HRes")
                    .VRes = oDrd("VRes")
                    .Cod = oDrd("Cod")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    .LangSet = New DTOLang.Set(oDrd("LangSet"))
                    SQLHelper.LoadLangTextFromDataReader(.Description, oDrd)
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .Obsolet = oDrd("Obsoleto")
                End With
            Catch ex As Exception
                'Stop
            End Try
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SpriteImages(oGuids As List(Of Guid)) As List(Of Byte())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx INT NOT NULL, ")
        sb.AppendLine("	      Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx, Guid) ")

        Dim idx As Integer = 0
        For Each guid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, guid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT MediaResource.Thumbnail ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("INNER JOIN @Table X ON MediaResource.Guid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            Dim oThumbnail = oDrd("Thumbnail")
            retval.Add(oThumbnail)
        Loop
        oDrd.Close()

        Return retval
    End Function



    Shared Function Delete(exs As List(Of Exception), oMediaResources As DTOMediaResource.Collection) As Boolean
        Dim retval As Boolean
        Dim sc As New System.Text.StringBuilder
        sc.AppendLine("DECLARE @Table TABLE( ")
        sc.AppendLine("	     Guid uniqueidentifier NOT NULL ")
        sc.AppendLine("        ) ")
        sc.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each item In oMediaResources
            sc.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sc.AppendFormat("('{0}') ", item.Guid.ToString())
            idx += 1
        Next

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(sc.ToString)
        sb.AppendLine("DELETE MediaResourceTarget ")
        sb.AppendLine("FROM MediaResourceTarget ")
        sb.AppendLine("INNER JOIN @Table X ON MediaResourceTarget.Parent = X.Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)


        sb = New System.Text.StringBuilder
        sb.AppendLine(sc.ToString)
        sb.AppendLine("DELETE MediaResource ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("INNER JOIN @Table X ON MediaResource.Guid = X.Guid ")
        SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        retval = exs.Count = 0
        Return retval
    End Function

    Shared Function ExistsFromProductOrChildren(oProduct As DTOProduct) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 MediaResource.Guid ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("INNER JOIN MediaResourceTarget ON MediaResource.Guid = MediaResourceTarget.Parent ")
        sb.AppendLine("INNER JOIN VwProductParent ON MediaResourceTarget.Target = VwProductParent.Child ")
        sb.AppendLine("WHERE VwProductParent.Parent='" & oProduct.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        retval = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function MissingResources(filenames As List(Of String)) As List(Of DTOMediaResource)
        Dim retval As New List(Of DTOMediaResource)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      HashValue VARCHAR(24) NOT NULL")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(HashValue) ")

        Dim idx As Integer = 0
        For Each filename In filenames
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", DTOMediaResource.HashFromFilename(filename))
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT MediaResource.Guid, MediaResource.Hash, MediaResource.Nom, Width, Height, Size, Mime ")
        sb.AppendLine("FROM MediaResource ")
        sb.AppendLine("LEFT OUTER JOIN MediaResourceTarget ON MediaResource.Guid = MediaResourceTarget.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON MediaResourceTarget.Target = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN @Table X ON MediaResource.[Hash] = X.HashValue ")
        sb.AppendLine("WHERE X.HashValue IS NULL AND VwProductNom.Obsoleto=0 ")
        sb.AppendLine("ORDER BY BrandNom, CategoryNom, SkuNom")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMediaResource(oDrd("Guid"))
            With item
                .Hash = oDrd("Hash")
                .Nom = oDrd("Nom")
                .Width = SQLHelper.GetIntegerFromDataReader(oDrd("Width"))
                .Height = SQLHelper.GetIntegerFromDataReader(oDrd("Height"))
                .Length = oDrd("Size")
                .Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class