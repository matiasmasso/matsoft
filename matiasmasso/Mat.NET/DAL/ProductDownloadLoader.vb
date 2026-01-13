Public Class ProductDownloadLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOProductDownload
        Dim retval As DTOProductDownload = Nothing
        Dim oProductDownload As New DTOProductDownload(oGuid)
        If Load(oProductDownload) Then
            retval = oProductDownload
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProductDownload As DTOProductDownload) As Boolean
        If Not oProductDownload.IsLoaded And Not oProductDownload.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ProductDownload.Guid, ProductDownload.Src, ProductDownload.Lang, ProductDownload.LangSet ")
            sb.AppendLine(", ProductDownload.PublicarAlConsumidor, ProductDownload.PublicarAlDistribuidor, ProductDownload.Obsoleto ")
            sb.AppendLine(", DownloadTarget.target ")

            sb.AppendLine(", VwProductNom.Guid AS ProductGuid, VwProductNom.Cod AS ProductCod ")
            sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom ")
            sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
            sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")

            sb.AppendLine(", Vehicle_Flota.Guid AS VehicleGuid, Vehicle_Flota.Matricula ")

            sb.AppendLine(", VwDocfile.* ")

            sb.AppendLine("FROM ProductDownload ")
            sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
            sb.AppendLine("INNER JOIN VwDocFile ON ProductDownload.Hash = VwDocFile.DocfileHash ")

            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON DownloadTarget.Target = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Flota ON DownloadTarget.Target = Vehicle_Flota.Guid ")

            sb.AppendLine("WHERE ProductDownload.Guid='" & oProductDownload.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oProductDownload.IsLoaded Then
                    With oProductDownload
                        .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                        .Src = oDrd("Src")
                        .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                        .LangSet = New DTOLang.Set(oDrd("Langset"))
                        .PublicarAlConsumidor = oDrd("PublicarAlConsumidor")
                        .PublicarAlDistribuidor = oDrd("PublicarAlDistribuidor")
                        .Obsoleto = oDrd("Obsoleto")
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("target")) Then
                    Dim oTarget = New DTOBaseGuidCodNom(oDrd("Target"))
                    If Not IsDBNull(oDrd("ProductGuid")) Then
                        Dim oProduct = SQLHelper.GetProductFromDataReader(oDrd)
                        oTarget.Cod = oDrd("ProductCod")
                        oTarget.Nom = oProduct.FullNom()
                    End If
                    If Not IsDBNull(oDrd("VehicleGuid")) Then
                        Dim oVehicle As New DTOVehicle(oDrd("VehicleGuid"))
                        oVehicle.Matricula = SQLHelper.GetStringFromDataReader(oDrd("Matricula"))
                        oTarget.Cod = DTOBaseGuidCodNom.Cods.Vehicle
                        oTarget.Nom = String.Format("Vehicle matricula {0}", oVehicle.Matricula)
                    End If
                    oProductDownload.Targets.Add(oTarget)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oProductDownload.IsLoaded
        Return retval
    End Function

    Shared Function Update(oProductDownload As DTOProductDownload, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProductDownload, oTrans)
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

    Shared Sub Update(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oProductDownload.DocFile, oTrans)
        UpdateHeader(oProductDownload, oTrans)
        UpdateItems(oProductDownload, oTrans)
    End Sub

    Shared Sub UpdateHeader(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("WHERE Guid='" & oProductDownload.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProductDownload.Guid
        Else
            oRow = oTb.Rows(0)
        End If


        With oProductDownload
            DocFileLoader.Update(.DocFile, oTrans)

            'oRow("Product") = .Target.Guid
            oRow("Src") = .Src
            oRow("Lang") = SQLHelper.NullableLang(.Lang)
            oRow("Langset") = .LangSet.Value
            oRow("PublicarAlConsumidor") = .PublicarAlConsumidor
            oRow("PublicarAlDistribuidor") = .PublicarAlDistribuidor
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
            oRow("Obsoleto") = .Obsoleto
        End With

        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateItems(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        If Not oProductDownload.IsNew Then DeleteItems(oProductDownload, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DownloadTarget ")
        sb.AppendLine("WHERE Download='" & oProductDownload.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oProductDownload.Targets
            Dim oRow = oTb.NewRow
            oRow("Download") = oProductDownload.Guid
            oRow("Target") = item.Guid
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProductDownload As DTOProductDownload, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProductDownload, oTrans)
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

    Shared Sub Delete(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        DeleteItems(oProductDownload, oTrans)
        DeleteHeader(oProductDownload, oTrans)
    End Sub

    Shared Sub DeleteItems(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE DownloadTarget WHERE Download='" & oProductDownload.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oProductDownload As DTOProductDownload, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ProductDownload WHERE Guid='" & oProductDownload.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ProductDownloadsLoader

    Shared Function All() As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ProductDownload.Guid, ProductDownload.Target, ProductDownload.Lang, ProductDownload.LangSet ")
        sb.AppendLine(", ProductDownload.PublicarAlConsumidor, ProductDownload.PublicarAlDistribuidor, ProductDownload.Obsoleto ")
        sb.AppendLine(", VwProductNom.Guid AS ProductGuid, VwProductNom.Cod AS ProductCod ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom ")
        sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
        sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine(", DocFile.Hash AS DocfileHash, Docfile.Mime AS DocfileMime, Docfile.Size AS DocfileSize ")
        sb.AppendLine(", Docfile.Width AS DocfileWidth, Docfile.Height AS DocfileHeight ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
        sb.AppendLine("INNER JOIN VwProductNom ON DownloadTarget.Target = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN DocFile ON ProductDownload.Hash = DocFile.Hash ")
        sb.AppendLine("ORDER BY ProductDownload.Src, ProductDownload.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductDownload(oDrd("Guid"))
            With item
                .Target = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"))
                .DocFile = New DTODocFile(oDrd("Hash"))
                .Src = oDrd("Src")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("Langset"))
                .PublicarAlConsumidor = oDrd("PublicarAlConsumidor")
                .PublicarAlDistribuidor = oDrd("PublicarAlDistribuidor")
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromProductOrChildren(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Count(DocFileLog.Fch) AS LogCount, ProductDownload.Guid, DownloadTarget.Target As Product ")
        sb.AppendLine(", ProductDownload.Lang, ProductDownload.LangSet,ProductDownload.Src,ProductDownload.Obsoleto,ProductDownload.Hash ")
        sb.AppendLine(", ProductDownload.PublicarAlConsumidor,ProductDownload.PublicarAlDistribuidor ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine(", BF.Size, BF.Mime, BF.Pags, BF.Width, BF.Height, BF.Hres, BF.Vres, BF.Nom, BF.Obsolet ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
        sb.AppendLine("INNER JOIN VwProductNom ON DownloadTarget.Target = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON DownloadTarget.Target=VwProductParent.Child ")
        sb.AppendLine("INNER JOIN DocFile BF ON ProductDownload.Hash=BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("LEFT OUTER JOIN DocFileLog ON ProductDownload.Hash = DocFileLog.Hash ")
        sb.AppendLine("WHERE VwProductParent.Parent ='" & oProduct.Guid.ToString & "' ")

        If Not IncludeObsoletos Then
            sb.AppendLine("AND ProductDownload.Obsoleto = 0 ")
        End If
        If OnlyConsumerEnabled Then
            sb.AppendLine("AND ProductDownload.PublicarAlConsumidor <>0 ")
        End If
        If oSrc <> DTOProductDownload.Srcs.NotSet Then
            sb.AppendLine("AND ProductDownload.Src = " & CInt(oSrc).ToString & " ")
        End If
        sb.AppendLine("GROUP BY ProductDownload.Guid,DownloadTarget.Target,ProductDownload.PublicarAlConsumidor,ProductDownload.PublicarAlDistribuidor ")
        sb.AppendLine(", ProductDownload.Lang, ProductDownload.LangSet, ProductDownload.Src,ProductDownload.Obsoleto,ProductDownload.Hash ")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine(", BF.Size, BF.Mime, BF.Pags, BF.Width, BF.Height, BF.Hres, BF.Vres, BF.Nom, BF.Obsolet, ProductDownload.Ord ")
        sb.AppendLine("ORDER BY VwProductNom.CategoryNom ") 'ProductDownload.Src, ProductDownload.Ord")
        Dim SQL As String = sb.ToString


        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDocfile As DTODocFile = New DTODocFile(oDrd("Hash").ToString())
            With oDocfile
                .Length = oDrd("Size")
                .Size = New Size(CInt(oDrd("Width")), CInt(oDrd("Height")))
                .Mime = oDrd("Mime")
                .Pags = oDrd("Pags")
                .HRes = oDrd("HRes")
                .VRes = oDrd("VRes")
                .Obsolet = oDrd("Obsolet")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .LogCount = SQLHelper.GetIntegerFromDataReader(oDrd("LogCount"))
            End With

            Dim oTarget As DTOProduct
            If oProduct.Guid.Equals(oDrd("Product")) Then
                oTarget = oProduct
            Else
                oTarget = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"))
            End If

            Dim oProductDownload As New DTOProductDownload(DirectCast(oDrd("Guid"), Guid))
            With oProductDownload
                .Target = oTarget
                .DocFile = oDocfile
                .Src = oDrd("Src")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("Langset"))
                .PublicarAlConsumidor = oDrd("PublicarAlConsumidor")
                .PublicarAlDistribuidor = oDrd("PublicarAlDistribuidor")
                .Obsoleto = oDrd("Obsoleto")
                .IsLoaded = True
            End With
            retval.Add(oProductDownload)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromProductOrParent(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Count(DocFileLog.Fch) AS LogCount, ProductDownload.Guid, ProductDownload.Product ")
        sb.AppendLine(", ProductDownload.Lang, ProductDownload.LangSet,ProductDownload.Src,ProductDownload.Obsoleto,ProductDownload.Hash ")
        sb.AppendLine(", ProductDownload.PublicarAlConsumidor,ProductDownload.PublicarAlDistribuidor ")

        sb.AppendLine(", VwProductNom.Guid AS ProductGuid, VwProductNom.Cod AS ProductCod ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom ")
        sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
        sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")

        sb.AppendLine(", BF.Size, BF.Mime, BF.Pags, BF.Width, BF.Height, BF.Hres, BF.Vres, BF.Nom, BF.Obsolet, BF.Fch AS DocFileFchCreated ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
        sb.AppendLine("INNER JOIN VwProductNom ON DownloadTarget.Target = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON DownloadTarget.target=VwProductParent.Parent ")
        sb.AppendLine("INNER JOIN DocFile BF ON ProductDownload.Hash=BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("LEFT OUTER JOIN DocFileLog ON ProductDownload.Hash = DocFileLog.Hash ")
        sb.AppendLine("WHERE VwProductParent.Child ='" & oProduct.Guid.ToString & "' ")

        If Not IncludeObsoletos Then
            sb.AppendLine("AND ProductDownload.Obsoleto = 0 ")
        End If
        If OnlyConsumerEnabled Then
            sb.AppendLine("AND ProductDownload.PublicarAlConsumidor <>0 ")
        End If
        If oSrc <> DTOProductDownload.Srcs.NotSet Then
            sb.AppendLine("AND ProductDownload.Src = " & CInt(oSrc).ToString & " ")
        End If

        sb.AppendLine("GROUP BY ProductDownload.Guid,ProductDownload.Product,ProductDownload.PublicarAlConsumidor,ProductDownload.PublicarAlDistribuidor ")
        sb.AppendLine(", ProductDownload.Lang, ProductDownload.LangSet,ProductDownload.Src,ProductDownload.Obsoleto,ProductDownload.Hash ")
        sb.AppendLine(", VwProductNom.Guid , VwProductNom.Cod ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom ")
        sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
        sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine(", BF.Size, BF.Mime, BF.Pags, BF.Width, BF.Height, BF.Hres, BF.Vres, BF.Nom, BF.Obsolet, ProductDownload.Ord, BF.Fch ")
        sb.AppendLine("ORDER BY ProductDownload.Src, ProductDownload.Ord, BF.Fch DESC")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDocfile As DTODocFile = New DTODocFile(oDrd("Hash").ToString())
            With oDocfile
                .length = oDrd("Size")
                .size = New Size(CInt(oDrd("Width")), CInt(oDrd("Height")))
                .mime = oDrd("Mime")
                .pags = oDrd("Pags")
                .hRes = oDrd("HRes")
                .vRes = oDrd("VRes")
                .fch = oDrd("DocFileFchCreated")
                .obsolet = oDrd("Obsolet")
                If Not IsDBNull(oDrd("Nom")) Then
                    .nom = oDrd("Nom")
                End If
                If Not IsDBNull(oDrd("LogCount")) Then
                    .logCount = oDrd("LogCount")
                End If
            End With

            Dim oProductTarget = SQLHelper.GetProductFromDataReader(oDrd)
            Dim oTarget As New DTOBaseGuidCodNom(oDrd("ProductGuid"))
            oTarget.cod = oDrd("ProductCod")
            oTarget.nom = oProduct.FullNom()

            Dim oProductDownload As New DTOProductDownload(DirectCast(oDrd("Guid"), Guid))
            With oProductDownload
                .DocFile = oDocfile
                .Src = oDrd("Src")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("Langset"))
                .PublicarAlConsumidor = oDrd("PublicarAlConsumidor")
                .PublicarAlDistribuidor = oDrd("PublicarAlDistribuidor")
                .Obsoleto = oDrd("Obsoleto")

                .Targets.Add(oTarget)
            End With
            retval.Add(oProductDownload)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function ExistsFromProductOrParent(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As Boolean
        Dim SQL As String = "SELECT TOP 1 ProductDownload.Guid " _
        & "FROM ProductDownload " _
        & "INNER JOIN VwProductParent ON ProductDownload.Product=VwProductParent.Parent " _
        & "WHERE VwProductParent.Child='" & oProduct.Guid.ToString & "' "


        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 ProductDownload.Guid ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
        sb.AppendLine("INNER JOIN VwProductParent ON DownloadTarget.target=VwProductParent.Parent ")
        sb.AppendLine("WHERE VwProductParent.Child='" & oProduct.Guid.ToString & "' ")

        If Not IncludeObsoletos Then
            sb.AppendLine("AND ProductDownload.Obsoleto = 0 ")
        End If
        If OnlyConsumerEnabled Then
            sb.AppendLine("AND ProductDownload.PublicarAlConsumidor <>0 ")
        End If
        If oSrc <> DTOProductDownload.Srcs.NotSet Then
            sb.AppendLine("AND ProductDownload.Src = " & CInt(oSrc).ToString & " ")
        End If

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oSrc As DTOProductDownload.Srcs) As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)
        Dim SQL As String = "SELECT Count(ProductDownload.Guid) AS FileCount, Tpa.Guid, BrandNom.Esp AS BrandNom " _
        & "FROM ProductDownload " _
        & "INNER JOIN VwProductParent ON ProductDownload.Product=VwProductParent.Child " _
        & "INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid " _
        & "INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 " _
        & "WHERE ProductDownload.Src=@Src " _
        & "AND ProductDownload.Obsoleto = 0 " _
        & "AND ProductDownload.PublicarAlConsumidor <> 0 " _
        & "AND Tpa.Obsoleto = 0 " _
        & "GROUP BY Tpa.Guid, BrandNom.Esp, Tpa.Ord " _
        & "ORDER BY Tpa.Ord"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "Src", CInt(oSrc))
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(DirectCast(oDrd("Guid"), Guid))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")

            Dim item As New DTOProductDownload
            With item
                .Target = oBrand
                .FileCount = CInt(oDrd("FileCount"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oTarget As DTOBaseGuid, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.notSet) As List(Of DTOProductDownload)
        Dim retval As New List(Of DTOProductDownload)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Log.Counter, ProductDownload.Guid, ProductDownload.Lang, ProductDownload.LangSet, ProductDownload.Src, ProductDownload.Hash ")
        sb.AppendLine(", DocFile.Mime, DocFile.Nom, DocFile.Size, DocFile.Width, DocFile.Height, DocFile.HRes, DocFile.VRes, DocFile.Pags, DocFile.Obsolet, DocFile.Fch, DocFile.FchCreated ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download ")
        sb.AppendLine("INNER JOIN DocFile ON ProductDownload.Hash=DocFile.Hash ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT DocFileLog.Hash, COUNT(DocFileLog.[User]) AS Counter FROM DocFileLog GROUP BY DocFileLog.Hash) Log ON ProductDownload.Hash = Log.Hash ")
        sb.AppendLine("WHERE DownloadTarget.Target='" & oTarget.Guid.ToString & "' ")
        If oSrc <> DTOProductDownload.Srcs.notSet Then
            sb.AppendLine("AND DownloadTarget.Src=" & CInt(oSrc) & " ")
        End If
        sb.AppendLine("ORDER BY DocFile.Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDocFile As New DTODocFile(oDrd("Hash"))
            With oDocFile
                .mime = oDrd("Mime")
                .length = oDrd("Size")
                .pags = oDrd("Pags")
                .Size = New System.Drawing.Size(CInt(oDrd("Width")), CInt(oDrd("Height")))
                .hRes = oDrd("HRes")
                .vRes = oDrd("VRes")
                .obsolet = oDrd("Obsolet")
                .fch = oDrd("Fch")
                .fchCreated = oDrd("FchCreated")
                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                If Not IsDBNull(oDrd("Counter")) Then
                    .logCount = oDrd("Counter")
                End If

            End With

            Dim item As New DTOProductDownload(oDrd("Guid"))
            With item
                .target = oTarget
                .src = oDrd("Src")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("Langset"))
                .DocFile = oDocFile
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ProductModels(oSrc As DTOProductDownload.Srcs, oLang As DTOLang) As DTOProductDownload.ProductModel
        Dim retval As New DTOProductDownload.ProductModel(oSrc)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT ProductDownload.Guid, ProductDownload.Product, ProductDownload.Hash, Log.Counter ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom ")
        sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
        sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine(", DocFile.Mime, Docfile.Nom, Docfile.Pags, Docfile.Size, Docfile.Width, Docfile.Height ")
        sb.AppendLine("FROM ProductDownload ")
        sb.AppendLine("INNER JOIN DownloadTarget ON ProductDownload.Guid = DownloadTarget.Download  ")
        sb.AppendLine("INNER JOIN VwProductNom ON DownloadTarget.Target = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid = Tpa.Guid ")
        sb.AppendLine("INNER JOIN Docfile ON ProductDownload.Hash = Docfile.Hash ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT DocFileLog.Hash, COUNT(DocFileLog.[User]) AS Counter FROM DocFileLog GROUP BY DocFileLog.Hash) Log ON ProductDownload.Hash = Log.Hash ")
        sb.AppendLine("WHERE ProductDownload.Obsoleto = 0 ")
        sb.AppendLine("AND VwProductNom.Obsoleto = 0 ")
        sb.AppendLine("AND Tpa.Enliquidacio=0 ")
        sb.AppendLine("AND ProductDownload.Src = " & CInt(oSrc) & " ")

        Select Case oLang.id
            Case DTOLang.Ids.ESP, DTOLang.Ids.POR
                sb.AppendLine("AND (ProductDownload.Lang IS NULL OR ProductDownload.Lang = '" & oLang.Tag & "' ) ")
            Case Else
                sb.AppendLine("AND (ProductDownload.Lang IS NULL OR ProductDownload.Lang = 'ESP' OR ProductDownload.Lang = '" & oLang.Tag & "' ) ")
        End Select

        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDocFile As New DTODocFile(oDrd("Hash"))
            With oDocFile
                .mime = oDrd("Mime")
                .length = oDrd("Size")
                .pags = oDrd("Pags")
                .size = New Size(oDrd("Width"), oDrd("Height"))
            End With
            Dim file As New DTOProductDownload.ProductModel.File()
            With file
                .Guid = oDrd("Guid")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                If Not IsDBNull(oDrd("BrandGuid")) Then
                    .BrandGuid = oDrd("BrandGuid")
                    .BrandNom = oDrd("BrandNom")
                    If Not IsDBNull(oDrd("CategoryGuid")) Then
                        .CategoryGuid = oDrd("CategoryGuid")
                        .CategoryNom = oDrd("CategoryNom")
                        If Not IsDBNull(oDrd("SkuGuid")) Then
                            .SkuGuid = oDrd("SkuGuid")
                            .SkuNom = oDrd("SkuNom")
                        End If
                    End If
                End If
                .DownloadUrl = oDocFile.downloadUrl()
                .ThumbnailUrl = oDocFile.ThumbnailUrl()
                .Features = oDocFile.Features(True)
                .LogCount = SQLHelper.GetIntegerFromDataReader(oDrd("Counter"))
            End With
            retval.Files.Add(file)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

