Public Class DocFileLoader

    Shared Function FromHash(sHash As String, Optional LoadStream As Boolean = False) As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim oDocFile As New DTODocFile
        oDocFile.Hash = sHash
        If DocFileLoader.Load(oDocFile, LoadStream) Then
            retval = oDocFile
        End If
        Return retval
    End Function

    Shared Function FromSha256(sha256 As String, Optional LoadStream As Boolean = False) As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim asin As String = ""
        Dim Sql As String = "SELECT Hash FROM DocFile WHERE Sha256='" & sha256 & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(Sql)
        If oDrd.Read Then
            asin = oDrd("Hash")
        End If
        oDrd.Close()

        If asin > "" Then
            Dim oDocFile As New DTODocFile(asin)
            If DocFileLoader.Load(oDocFile, LoadStream) Then
                retval = oDocFile
            End If
        End If

        Return retval
    End Function


    Shared Function Exists(sHash As String, ByRef DtFch As Date) As Boolean
        Dim retval As Boolean
        Dim Sql As String = "SELECT Fch FROM DocFile WHERE Hash='" & sHash & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(Sql)
        If oDrd.Read Then
            DtFch = oDrd("Fch")
            retval = True
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Stream(hash As String) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim SQL As String = "SELECT Doc, Mime FROM DocFile WHERE Hash='" & hash & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New ImageMime()
            retval.Mime = oDrd("Mime")
            retval.ByteArray = oDrd("Doc")
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Thumbnail(hash As String) As Byte()
        Dim retval As Byte() = Nothing
        Dim SQL As String = "SELECT Thumbnail FROM DocFile WHERE Hash='" & hash & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Thumbnail")) Then
                retval = oDrd("Thumbnail")
            End If
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(oDocFile As DTODocFile, Optional LoadStream As Boolean = False) As Boolean
        Dim retval As Boolean
        If oDocFile.Hash > "" Then
            Dim SQL As String
            If LoadStream Then
                SQL = "SELECT * FROM DocFile WHERE Hash='" & oDocFile.Hash & "'"
            Else
                SQL = "SELECT DocFile.Sha256, DocFile.Mime, DocFile.Size, DocFile.Width, DocFile.Height, DocFile.HRes, DocFile.VRes, DocFile.Pags, DocFile.Nom, DocFile.Obsolet, DocFile.Fch, Docfile.FchCreated FROM DocFile WHERE Hash='" & oDocFile.Hash & "'"
            End If
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Hash", oDocFile.Hash)
            If oDrd.Read Then
                With oDocFile
                    .Sha256 = oDrd("Sha256")
                    .Mime = oDrd("Mime")
                    .Length = oDrd("Size")
                    .Pags = oDrd("Pags")
                    .Size = New Size(CInt(oDrd("Width")), CInt(oDrd("Height")))
                    .HRes = oDrd("HRes")
                    .VRes = oDrd("VRes")
                    '.Thumbnail = oDrd("Thumbnail")
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Obsolet = oDrd("Obsolet")
                    .Fch = oDrd("Fch")
                    .FchCreated = oDrd("FchCreated")
                    'If LoadStream Then
                    '.Stream = oDrd("Doc")
                    'End If
                End With
                retval = True
            End If
            oDrd.Close()
        End If
        Return retval
    End Function


    Shared Function Update(oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDocFile, oTrans)
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

    Shared Sub Update(oDocFile As DTODocFile, oTrans As SqlTransaction)
        If oDocFile IsNot Nothing Then
            If oDocFile.Fch = Nothing Then oDocFile.Fch = DTO.GlobalVariables.Today()
            Dim SQL As String = "SELECT * FROM DocFile WHERE Hash='" & oDocFile.Hash & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow = Nothing
            If oTb.Rows.Count = 0 And oDocFile.Stream IsNot Nothing Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                With oDocFile
                    oRow("Hash") = .Hash
                    oRow("Sha256") = .Sha256
                    oRow("Mime") = .Mime
                    oRow("Size") = .Length
                    oRow("Pags") = .Pags
                    oRow("Width") = .Size.Width
                    oRow("Height") = .Size.Height
                    oRow("HRes") = .HRes
                    oRow("VRes") = .VRes
                End With
            Else
                oRow = oTb.Rows(0)
            End If

            If oRow IsNot Nothing Then
                With oDocFile
                    If String.IsNullOrEmpty(.Nom) Then
                        oRow("Nom") = System.DBNull.Value
                    Else
                        oRow("Nom") = Left(.Nom, 50)
                    End If
                    oRow("Fch") = .Fch
                    oRow("Obsolet") = .Obsolet
                    If .Thumbnail IsNot Nothing Then
                        oRow("Thumbnail") = .Thumbnail
                        Dim thumb140 = LegacyHelper.ImageHelper.GetThumbnailToFit(.Thumbnail, 140, 160)
                        Dim converter As ImageConverter = New ImageConverter()
                        oRow("Thumb140") = CType(converter.ConvertTo(thumb140, GetType(Byte())), Byte())
                    End If
                    If .Stream IsNot Nothing Then
                        oRow("Doc") = .Stream
                    End If
                End With

                oDA.Update(oDs)
            End If
        End If
    End Sub


    Shared Sub BackupForeignKeys(oDocFile As DTODocFile, oTrans As SqlTransaction) 'per quan hi ha una coleccio de FileDocs al mateix SrcGuid, i en volem eliminar nomes un
        If oDocFile IsNot Nothing Then
            Dim SQL As String = "UPDATE Iban SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Escriptura SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Cca SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE ProductDownload SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Pdc SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Crr SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Aeat SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE CliDoc SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Iban SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE ImportDtl SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Incidencia_DocFiles SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE Intrastat SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            SQL = "UPDATE PriceList_Supplier SET Hash=NULL WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub

    Shared Function Delete(oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDocFile, oTrans)
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

    Shared Sub Delete(oDocFile As DTODocFile, oTrans As SqlTransaction)
        DeleteLogs(oDocFile, oTrans)
        'BackupForeignKeys(oDocFile, oTrans)
        DeleteHeader(oDocFile, oTrans)
    End Sub

    Shared Sub DeleteLogs(oDocFile As DTODocFile, oTrans As SqlTransaction) 'per quan hi ha una coleccio de FileDocs al mateix SrcGuid, i en volem eliminar nomes un
        If oDocFile IsNot Nothing Then
            Dim SQL As String = "DELETE DocFileLog WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub

    Shared Sub DeleteHeader(oDocFile As DTODocFile, oTrans As SqlTransaction) 'per quan hi ha una coleccio de FileDocs al mateix SrcGuid, i en volem eliminar nomes un
        If oDocFile IsNot Nothing Then
            Dim SQL As String = "DELETE DocFile WHERE Hash='" & oDocFile.Hash & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub

    Shared Sub DeleteIfOrphan(oDocfile As DTODocFile, oTrans As SqlTransaction)
        If oDocfile IsNot Nothing Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("DELETE Docfile ")
            sb.AppendLine("FROM Docfile ")
            sb.AppendLine("LEFT OUTER JOIN Crr ON Docfile.Hash=Crr.Hash ")
            sb.AppendLine("LEFT OUTER JOIN ProductDownload ON Docfile.Hash=ProductDownload.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON Docfile.Hash=Cca.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Pdc ON Docfile.Hash=Pdc.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Aeat ON Docfile.Hash=Aeat.Hash ")
            sb.AppendLine("LEFT OUTER JOIN CliDoc ON Docfile.Hash=CliDoc.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Iban ON Docfile.Hash=Iban.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Escriptura ON Docfile.Hash=Escriptura.Hash ")
            sb.AppendLine("LEFT OUTER JOIN ImportDtl ON Docfile.Hash=ImportDtl.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Incidencia_DocFiles ON Docfile.Hash=Incidencia_DocFiles.Hash ")
            sb.AppendLine("LEFT OUTER JOIN Intrastat ON Docfile.Hash=Intrastat.Hash ")
            sb.AppendLine("LEFT OUTER JOIN PriceList_Supplier ON Docfile.Hash=PriceList_Supplier.Hash ")
            sb.AppendLine("LEFT OUTER JOIN DocfileSrc ON Docfile.Hash=DocfileSrc.Hash ")
            sb.AppendLine("WHERE Docfile.Hash='" & oDocfile.hash & "' ")
            sb.AppendLine("AND Crr.Hash IS NULL ")
            sb.AppendLine("AND ProductDownload.Hash IS NULL ")
            sb.AppendLine("AND Cca.Hash IS NULL ")
            sb.AppendLine("AND Pdc.Hash IS NULL ")
            sb.AppendLine("AND Aeat.Hash IS NULL ")
            sb.AppendLine("AND CliDoc.Hash IS NULL ")
            sb.AppendLine("AND Iban.Hash IS NULL ")
            sb.AppendLine("AND Escriptura.Hash IS NULL ")
            sb.AppendLine("AND ImportDtl.Hash IS NULL ")
            sb.AppendLine("AND Incidencia_DocFiles.Hash IS NULL ")
            sb.AppendLine("AND Intrastat.Hash IS NULL ")
            sb.AppendLine("AND PriceList_Supplier.Hash IS NULL ")
            sb.AppendLine("AND DocfileSrc.Hash IS NULL ")
            'Incidencia_DocFiles
            Dim SQL = sb.ToString
            Dim rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub


    Shared Function Srcs(oDocfile As DTODocFile) As List(Of DTODocFileSrc)
        Dim retval As New List(Of DTODocFileSrc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT " & DTODocFile.Cods.Assentament & " AS Cod, Cca.Guid, Cca.Fch, Cca.Txt AS Nom FROM Cca WHERE Cca.Hash = '" & oDocfile.hash & "' ")
        sb.AppendLine("UNION SELECT " & DTODocFile.Cods.Correspondencia & ", Crr.Guid, Crr.Fch, Crr.Dsc FROM Crr WHERE Crr.Hash = '" & oDocfile.hash & "' ")
        sb.AppendLine("UNION SELECT " & DTODocFile.Cods.Download & ", ProductDownload.Guid, NULL, VwProductNom.FullNom FROM ProductDownload INNER JOIN VwProductNom ON ProductDownload.Product = VwProductNom.Guid AND ProductDownload.Hash = '" & oDocfile.hash & "' ")
        sb.AppendLine("UNION SELECT " & DTODocFile.Cods.pdc & ", Pdc.Guid, Pdc.Fch, 'Comanda '+CAST(Pdc.Pdc AS VARCHAR)+' '+ CliGral.FullNom FROM Pdc INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid AND Pdc.Hash = '" & oDocfile.Hash & "' ")
        sb.AppendLine("UNION SELECT " & DTODocFile.Cods.mem & ", DocfileSrc.SrcGuid, Mem.Fch, 'Mem de '+CliGral.FullNom AS Nom FROM DocfileSrc INNER JOIN Mem ON DocfileSrc.SrcGuid = Mem.Guid INNER JOIN CliGral ON Mem.Contact = CliGral.Guid AND DocfileSrc.Hash = '" & oDocfile.Hash & "' ")
        'sb.AppendLine("LEFT OUTER JOIN Pdc ON Docfile.Hash=Pdc.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN Aeat ON Docfile.Hash=Aeat.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN CliDoc ON Docfile.Hash=CliDoc.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN Iban ON Docfile.Hash=Iban.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN Escriptura ON Docfile.Hash=Escriptura.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN ImportDtl ON Docfile.Hash=ImportDtl.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN Incidencia_DocFiles ON Docfile.Hash=Incidencia_DocFiles.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN Intrastat ON Docfile.Hash=Intrastat.Hash ")
        'sb.AppendLine("LEFT OUTER JOIN PriceList_Supplier ON Docfile.Hash=PriceList_Supplier.Hash ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODocFileSrc()
            With item
                .Src = New DTOBaseGuid(oDrd("Guid"))
                .Cod = oDrd("Cod")
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Log(sHash As String, oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Log(sHash, oUser, oTrans)
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

    Shared Sub Log(sHash As String, oUser As DTOUser, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DocFileLog ")
        sb.AppendLine("WHERE Hash= '" & sHash & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oTb.Rows.Add(oRow)
        oRow("Hash") = sHash
        If oUser IsNot Nothing AndAlso oUser.Guid <> Guid.Empty Then
            oRow("User") = oUser.Guid
        End If
        oDA.Update(oDs)
    End Sub

    Shared Function Logs(oDocFile As DTODocFile) As List(Of DTODocFileLog)
        Dim retval As New List(Of DTODocFileLog)
        Dim SQL As String = "SELECT DocFileLog.[User], DocFileLog.Fch, Email.adr " _
                            & "FROM DocFileLog " _
                            & "LEFT OUTER JOIN Email ON DocFileLog.[User]=Email.Guid " _
                            & "WHERE DocFileLog.Hash='" & oDocFile.Hash & "' " _
                            & "ORDER BY DocFileLog.Fch DESC"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTODocFileLog
            If Not IsDBNull(oDrd("User")) Then
                Dim oUser As New DTOUser(DirectCast(oDrd("User"), Guid))
                oUser.EmailAddress = oDrd("adr").ToString
                oItem.User = oUser
            End If
            oItem.Fch = oDrd("Fch")
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class DocFilesLoader
    Shared Function All(year As Integer) As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT [Hash], Size, Mime, Nom, FchCreated ")
        sb.AppendLine("FROM DocFile ")
        sb.AppendLine("WHERE Hash IS NOT NULL ")
        sb.AppendLine("AND Year(FchCreated)=" & year & " ")
        sb.AppendLine("ORDER BY FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDocfile As New DTODocFile(oDrd("Hash"))
            With oDocfile
                .Mime = oDrd("Mime")
                .Length = oDrd("Size")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .FchCreated = oDrd("FchCreated")
            End With

            retval.Add(oDocfile)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Delete(oDocFiles As List(Of DTODocFile), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDocFiles, oTrans)
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

    Shared Sub Delete(oDocFiles As List(Of DTODocFile), oTrans As SqlTransaction)
        If oDocFiles.Count > 0 Then
            DeleteLogs(oDocFiles, oTrans)
            'BackupForeignKeys(oDocFiles, oTrans)
            DeleteHeaders(oDocFiles, oTrans)
        End If
    End Sub

    Shared Sub DeleteLogs(oDocFiles As List(Of DTODocFile), oTrans As SqlTransaction) 'per quan hi ha una coleccio de FileDocs al mateix SrcGuid, i en volem eliminar nomes un
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE DocFileLog WHERE (")
        For Each item In oDocFiles
            If item IsNot oDocFiles.First Then sb.Append("OR ")
            sb.AppendLine("Hash = '" & item.Hash & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeaders(oDocFiles As List(Of DTODocFile), oTrans As SqlTransaction) 'per quan hi ha una coleccio de FileDocs al mateix SrcGuid, i en volem eliminar nomes un
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE DocFile WHERE (")
        For Each item In oDocFiles
            If item IsNot oDocFiles.First Then sb.Append("OR ")
            sb.AppendLine("Hash = '" & item.Hash & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class