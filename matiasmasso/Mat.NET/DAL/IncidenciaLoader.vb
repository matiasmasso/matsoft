Public Class IncidenciaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOIncidencia
        Dim retval As DTOIncidencia = Nothing
        Dim oIncidencia As New DTOIncidencia(oGuid)
        If Load(oIncidencia) Then
            retval = oIncidencia
        End If
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, num As String) As DTOIncidencia
        Dim retval As DTOIncidencia = Nothing

        Dim SQL As String = ""
        If num.IsNumericStrict() Then
            SQL = "SELECT Guid FROM Incidencies WHERE Emp=" & oEmp.Id & " AND Id=" & num & " "
        Else
            SQL = "SELECT Guid FROM Incidencies WHERE Asin='" & num & "'"
        End If

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oGuid As Guid = oDrd("Guid")
                retval = New DTOIncidencia(oGuid)
            End If
            oDrd.Close()
            Return retval
    End Function

    Shared Function Load(ByRef oIncidencia As DTOIncidencia) As Boolean
        If Not oIncidencia.IsLoaded And Not oIncidencia.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Incidencies.Id, Incidencies.Asin, Incidencies.Cod, Incidencies.CodiApertura, Incidencies.CodiTancament, Incidencies.Fch, Incidencies.FchClose ")
            sb.AppendLine(", Incidencies.ContactType, Incidencies.ContactGuid, Incidencies.Emp, Incidencies.SpvGuid, Incidencies.ProductGuid, Incidencies.Obs ")
            sb.AppendLine(", Incidencies.Email, Incidencies.Person, Incidencies.Tel, Incidencies.SerialNumber, Incidencies.ManufactureDate, Incidencies.sRef, Incidencies.Procedencia, Incidencies.BoughtFrom, Incidencies.FchCompra ")
            sb.AppendLine(", Incidencies.FchCreated, Incidencies.FchLastEdited ")
            sb.AppendLine(", Incidencies.UsrCreated, UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", Incidencies.UsrLastEdited, UsrLastEdited.Adr AS UsrLastEditedEmailAddress, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine(", CodiApertura.Esp AS AperturaEsp, CodiApertura.Cat AS AperturaCat, CodiApertura.Eng AS AperturaEng, CodiApertura.Por AS AperturaPor ")
            sb.AppendLine(", CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
            sb.AppendLine(", CodiTancament.ReposicionParcial AS TancamentReposicionParcial, CodiTancament.ReposicionTotal AS TancamentReposicionTotal ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", Incidencia_DocFiles.Cod as DocCod, Incidencia_DocFiles.Hash ")
            sb.AppendLine(", CliGral.FullNom AS CustomerFullNom, CliGral.RaoSocial, CliGral.NomCom, CliGral.LangId ")
            sb.AppendLine(", Spv.Id AS SpvId, Spv.FchAvis AS SpvFch ")
            sb.AppendLine(", VwAddress.* ") ', BF.Thumbnail, BF.Doc ")
            sb.AppendLine(", BF.Mime, BF.Size, BF.Pags, BF.Width, BF.Height ") ', BF.Thumbnail, BF.Doc ")
            sb.AppendLine("FROM Incidencies ")
            sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiApertura ON Incidencies.CodiApertura = CodiApertura.Guid ")
            sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON Incidencies.ContactGuid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Spv ON Incidencies.Guid = Spv.Incidencia ")
            sb.AppendLine("LEFT OUTER JOIN Incidencia_DocFiles ON Incidencies.Guid = Incidencia_DocFiles.Incidencia ")
            sb.AppendLine("LEFT OUTER JOIN DocFile BF ON Incidencia_DocFiles.Hash = BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrCreated ON Incidencies.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrLastEdited ON Incidencies.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE Incidencies.Guid='" & oIncidencia.Guid.ToString & "'")
            sb.AppendLine("ORDER BY Incidencia_DocFiles.Cod, Incidencia_DocFiles.Hash ")


            oIncidencia.docFileImages = New List(Of DTODocFile)
            oIncidencia.purchaseTickets = New List(Of DTODocFile)

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oIncidencia
                    Dim sHash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
                    If sHash.isNotEmpty() Then
                        If Not oIncidencia.Attachments.Any(Function(x) x.hash = sHash) Then 'evita duplicar imatges en cas de varies reparacions per incidencia
                            Dim oDocfile As DTODocFile = New DTODocFile(sHash)
                            With oDocfile
                                .Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                                .Pags = SQLHelper.GetIntegerFromDataReader(oDrd("Pags"))
                                If Not IsDBNull(oDrd("Size")) Then
                                    .Length = oDrd("Size")
                                End If
                                .Size = New System.Drawing.Size(SQLHelper.GetIntegerFromDataReader(oDrd("Width")), SQLHelper.GetIntegerFromDataReader(oDrd("Height")))
                            End With
                            Select Case DirectCast(oDrd("DocCod"), DTOIncidencia.AttachmentCods)
                                Case DTOIncidencia.AttachmentCods.imatge
                                    .docFileImages.Add(oDocfile)
                                Case DTOIncidencia.AttachmentCods.ticket
                                    .purchaseTickets.Add(oDocfile)
                                Case DTOIncidencia.AttachmentCods.video
                                    .videos.Add(oDocfile)
                            End Select
                        End If
                    End If

                    If Not .IsLoaded Then
                        .Num = oDrd("Id")
                        .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                        .src = oDrd("Cod")

                        If Not IsDBNull(oDrd("CodiApertura")) Then
                            .codi = New DTOIncidenciaCod(oDrd("CodiApertura"))
                            .codi.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "AperturaEsp", "AperturaCat", "AperturaEng", "AperturaPor")
                        End If

                        If Not IsDBNull(oDrd("CodiTancament")) Then
                            .tancament = New DTOIncidenciaCod(oDrd("CodiTancament"))
                            .tancament.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "TancamentEsp", "TancamentCat", "TancamentEng", "TancamentPor")
                            .tancament.reposicionParcial = SQLHelper.GetIntegerFromDataReader(oDrd("TancamentReposicionParcial"))
                            .tancament.reposicionTotal = SQLHelper.GetIntegerFromDataReader(oDrd("TancamentReposicionTotal"))
                        End If

                        .fch = oDrd("Fch")
                        .fchClose = SQLHelper.GetFchFromDataReader(oDrd("FchClose"))

                        .contactType = oDrd("ContactType")

                        If Not IsDBNull(oDrd("ContactGuid")) Then
                            .customer = New DTOCustomer(oDrd("ContactGuid"))
                            With .customer
                                .emp = New DTOEmp(oDrd("Emp"))
                                .lang = DTOLang.Factory(oDrd("LangId"))
                                .FullNom = SQLHelper.GetStringFromDataReader(oDrd("CustomerFullNom"))
                                .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                            End With
                        End If

                        If Not IsDBNull(oDrd("SpvGuid")) Then
                            .spv = New DTOSpv(oDrd("SpvGuid"))
                            With .spv
                                .id = oDrd("SpvId")
                                .fchAvis = SQLHelper.GetFchFromDataReader(oDrd("SpvFch"))
                            End With
                        End If

                        .product = SQLHelper.GetProductFromDataReader(oDrd)
                        .description = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .emailAdr = SQLHelper.GetStringFromDataReader(oDrd("Email"))
                        .contactPerson = SQLHelper.GetStringFromDataReader(oDrd("Person"))
                        .tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                        .serialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
                        .ManufactureDate = SQLHelper.GetStringFromDataReader(oDrd("ManufactureDate"))
                        .customerRef = SQLHelper.GetStringFromDataReader(oDrd("sRef"))
                        .procedencia = oDrd("Procedencia")
                        .FchCompra = SQLHelper.GetNullableFchFromDataReader(oDrd("FchCompra"))
                        .BoughtFrom = SQLHelper.GetStringFromDataReader(oDrd("BoughtFrom"))
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)

                        .IsLoaded = True

                    End If
                End With
            Loop
            oDrd.Close()

            Dim loggedTracking = oIncidencia.UsrLog.Tracking()

            oIncidencia.Trackings = New DTOTracking.Collection
            oIncidencia.Trackings.Add(loggedTracking)
            oIncidencia.Trackings.AddRange(TrackingsLoader.All(oIncidencia))
        End If

        Dim retval As Boolean = oIncidencia.IsLoaded
        Return retval
    End Function



    Shared Function SpriteImages(ByRef oIncidencia As DTOIncidencia) As List(Of Byte())
        Dim retval As New List(Of Byte())
        If Not oIncidencia.IsLoaded And Not oIncidencia.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BF.Thumbnail ")
            sb.AppendLine("FROM Incidencies ")
            sb.AppendLine("LEFT OUTER JOIN Incidencia_DocFiles ON Incidencies.Guid = Incidencia_DocFiles.Incidencia ")
            sb.AppendLine("LEFT OUTER JOIN DocFile BF ON Incidencia_DocFiles.Hash = BF.Hash ")
            sb.AppendLine("WHERE Incidencies.Guid='" & oIncidencia.Guid.ToString & "'")
            sb.AppendLine("ORDER BY Incidencia_DocFiles.Cod, Incidencia_DocFiles.Hash ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If IsDBNull(oDrd("Thumbnail")) Then
                    Dim oThumbnail = New System.Drawing.Bitmap(150, 150)
                    retval.Add(oThumbnail.Bytes())
                Else
                    retval.Add(oDrd("Thumbnail"))
                End If

            Loop
            oDrd.Close()
        End If

        Return retval
    End Function

    Shared Function Update(oIncidencia As DTOIncidencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oIncidencia, oTrans)
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

    Shared Sub Update(oIncidencia As DTOIncidencia, ByRef oTrans As SqlTransaction)
        UpdateHeader(oIncidencia, oTrans)
        UpdateDocFiles(oIncidencia, oTrans)
    End Sub

    Shared Sub UpdateHeader(oIncidencia As DTOIncidencia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Incidencies WHERE Guid='" & oIncidencia.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oIncidencia.Guid
        Else
            oRow = oTb.Rows(0)
        End If


        With oIncidencia
            If .num = 0 Then .num = LastId(oIncidencia.customer.Emp, oTrans) + 1
            oRow("Id") = .Num
            oRow("Asin") = SQLHelper.NullableString(.Asin)
            oRow("COD") = .src
            oRow("CodiApertura") = SQLHelper.NullableBaseGuid(.codi)
            oRow("CodiTancament") = SQLHelper.NullableBaseGuid(.tancament)

            oRow("FCH") = .fch
            If .fchClose > Date.MinValue Then
                oRow("FCHCLOSE") = .fchClose
            Else
                oRow("FCHCLOSE") = System.DBNull.Value
            End If


            oRow("CONTACTTYPE") = .contactType
            Select Case .contactType
                Case DTOIncidencia.ContactTypes.professional
                    oRow("ContactGuid") = .customer.Guid
                    oRow("EMP") = .customer.Emp.Id
                    'oRow("CONTACT") = .customer.Id 'To deprecate
                Case DTOIncidencia.ContactTypes.consumidor
                    'oRow("CONSUMIDOR") = mConsumidor.Id
            End Select

            oRow("SpvGuid") = SQLHelper.NullableBaseGuid(.spv)
            oRow("ProductGuid") = SQLHelper.NullableBaseGuid(.product)

            oRow("OBS") = .description
            oRow("EMAIL") = SQLHelper.NullableString(.emailAdr)
            oRow("PERSON") = SQLHelper.NullableString(.contactPerson)
            oRow("TEL") = SQLHelper.NullableString(.tel)
            oRow("serialNumber") = SQLHelper.NullableString(.serialNumber)
            oRow("ManufactureDate") = SQLHelper.NullableString(.ManufactureDate)
            oRow("SREF") = SQLHelper.NullableString(.customerRef)
            oRow("Procedencia") = .Procedencia
            oRow("FchCompra") = SQLHelper.NullableFch2(.FchCompra)
            oRow("BoughtFrom") = SQLHelper.NullableString(.BoughtFrom)
            oRow("UsrLastEdited") = SQLHelper.NullableBaseGuid(.UsrLog.UsrLastEdited)
            If .IsNew Then
                oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrLog.usrLastEdited)
            Else
                oRow("FchLastEdited") = DTO.GlobalVariables.Now()
            End If

        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateDocFiles(oIncidencia As DTOIncidencia, oTrans As SqlTransaction)
        If oIncidencia.docFileImages IsNot Nothing Then

            DeleteMissingDocFiles(oIncidencia, oTrans)

            Dim SQL As String = "SELECT * FROM Incidencia_DocFiles WHERE Incidencia='" & oIncidencia.Guid.ToString & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oExistingDocFiles As New List(Of DTODocFile)
            For Each oRow In oTb.Rows
                oExistingDocFiles.Add(New DTODocFile(oRow("Hash")))
            Next

            Dim oAttachmentsToAdd = oIncidencia.Attachments.Where(Function(x) oExistingDocFiles.All(Function(y) y.hash <> x.hash)) 'retira els que ja hi son
            oAttachmentsToAdd = oAttachmentsToAdd.Where(Function(q) q.stream IsNot Nothing) 'retira els buits
            Dim oUniqueAttachmentsToAdd = oAttachmentsToAdd.GroupBy(Function(x) x.hash).Select(Function(y) y.First).Select(Function(z) z).ToList 'retira els duplicats

            For Each oDocfile In oUniqueAttachmentsToAdd.Where(Function(x) oIncidencia.DocFileImages.Any(Function(y) y.Hash = x.Hash))
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)

                oRow("Incidencia") = oIncidencia.Guid
                oRow("Cod") = DTOIncidencia.AttachmentCods.imatge
                oRow("Hash") = oDocfile.Hash
                If oDocfile.Stream IsNot Nothing Then
                    DocFileLoader.Update(oDocfile, oTrans)
                End If
            Next

            For Each oDocfile In oUniqueAttachmentsToAdd.Where(Function(x) oIncidencia.Videos.Any(Function(y) y.Hash = x.Hash))
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)

                oRow("Incidencia") = oIncidencia.Guid
                oRow("Cod") = DTOIncidencia.AttachmentCods.video
                oRow("Hash") = oDocfile.Hash
                If oDocfile.Stream IsNot Nothing Then
                    DocFileLoader.Update(oDocfile, oTrans)
                End If
            Next


            For Each oDocfile In oUniqueAttachmentsToAdd.Where(Function(x) oIncidencia.purchaseTickets.Any(Function(y) y.hash = x.hash))
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Incidencia") = oIncidencia.Guid
                oRow("Cod") = DTOIncidencia.AttachmentCods.ticket
                oRow("Hash") = oDocfile.hash
                If oDocfile.stream IsNot Nothing Then
                    DocFileLoader.Update(oDocfile, oTrans)
                End If
            Next

            oDA.Update(oDs)
        End If

    End Sub

    Shared Function Delete(oIncidencia As DTOIncidencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = Nothing
        Dim oTrans As SqlTransaction = Nothing
        Try
            Load(oIncidencia)

            oConn = SQLHelper.SQLConnection(True)
            oTrans = oConn.BeginTransaction
            DeleteDocFiles(oIncidencia, oTrans)
            Delete(oIncidencia, oTrans)
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


    Shared Sub DeleteMissingDocFiles(oIncidencia As DTOIncidencia, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE Incidencia_DocFiles ")
        sb.AppendLine("WHERE Incidencia='" & oIncidencia.Guid.ToString & "' ")
        For Each oDocfile In oIncidencia.Attachments
            sb.AppendLine("AND Incidencia_DocFiles.Hash <>'" & oDocfile.Hash & "' ")
        Next

        Dim SQL2 As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL2, oTrans)


        sb = New Text.StringBuilder
        sb.AppendLine("DELETE DocFile ")
        sb.AppendLine("FROM Incidencia_DocFiles ")
        sb.AppendLine("INNER JOIN DocFile On Incidencia_DocFiles.Hash=DocFile.Hash ")
        sb.AppendLine("WHERE Incidencia_DocFiles.Incidencia ='" & oIncidencia.Guid.ToString & "' ")
        For Each oDocfile In oIncidencia.Attachments
            sb.AppendLine("AND Incidencia_DocFiles.Hash <>'" & oDocfile.hash & "' ")
        Next
        Dim SQL1 As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL1, oTrans)


    End Sub

    Shared Sub DeleteDocFiles(oIncidencia As DTOIncidencia, ByRef oTrans As SqlTransaction)

        If oIncidencia.Attachments.Count > 0 Then
            'build query
            Dim sb = New Text.StringBuilder
            SB.AppendLine("DECLARE @Table TABLE( ")
            sb.AppendLine("	    Hash VARCHAR(50) NOT NULL ")
            sb.AppendLine("        ) ")
            sb.AppendLine("INSERT INTO @Table(Hash) ")

            Dim idx As Integer = 0
            For Each oDocfile In oIncidencia.DocFileImages
                sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
                sb.AppendFormat("('{0}') ", oDocfile.Hash)
                idx += 1
            Next

            Dim SqlHashes = sb.ToString()

            'delete attachments
            Dim SQL1 As String = "DELETE Incidencia_DocFiles WHERE Incidencia='" & oIncidencia.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL1, oTrans)

            'delete logs
            sb = New Text.StringBuilder(SqlHashes)
            sb.AppendLine("DELETE DocFileLog ")
            sb.AppendLine("FROM DocFileLog INNER JOIN @Table X ON DocFileLog.Hash = X.Hash ")
            Dim SQL2 As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL2, oTrans)

            'delete docfiles
            sb = New Text.StringBuilder(SqlHashes)
            sb.AppendLine("DELETE DocFile ")
            sb.AppendLine("FROM Docfile INNER JOIN @Table X ON DocFile.Hash = X.Hash ")
            Dim SQL3 As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL3, oTrans)

        End If

    End Sub

    Shared Sub Delete(oIncidencia As DTOIncidencia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Incidencies WHERE Guid='" & oIncidencia.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function LastId(oEmp As DTOEmp, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Incidencies.Id AS LastId FROM Incidencies ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Incidencies.Id DESC")

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function

#End Region

    Shared Function Catalog(oLang As DTOLang, Optional emp As DTOEmp = Nothing, Optional contacts As List(Of DTOContact) = Nothing) As DTOCatalog
        'Torna els productes consumits per un client susceptibles de incidencia
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder


        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine("FROM VwSkuNom ")

        If contacts Is Nothing Then
            sb.AppendLine("WHERE VwSkuNom.Emp = " & Emp.Id & " ")
            sb.AppendLine("AND VwSkuNom.BrandObsoleto = 0 ")
            sb.AppendLine("AND VwSkuNom.CategoryCodi <= " & DTOProductCategory.Codis.accessories & " ")
        Else
            sb.AppendLine("INNER JOIN Arc ON VwSkuNom.SkuGuid = Arc.ArtGuid ")
            sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
            sb.AppendLine("INNER JOIN VwCcxOrMe ON (Alb.CliGuid = VwCcxOrMe.Ccx  or Alb.CliGuid=VwCcxOrMe.Guid) ")

            sb.AppendLine("WHERE VwSkuNom.BrandObsoleto = 0 ")
            sb.AppendLine("AND VwSkuNom.CategoryCodi <= " & DTOProductCategory.Codis.accessories & " ")
            sb.AppendLine("AND (")

            Dim FirstRec As Boolean = True
            For Each oContact As DTOContact In contacts
                If FirstRec Then
                    FirstRec = False
                Else
                    sb.Append("OR ")
                End If
                sb.Append("VwCcxOrMe.Guid = '" & oContact.Guid.ToString & "' ")
            Next

            sb.AppendLine(")")
        End If
        sb.AppendLine("GROUP BY VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNom, VwSkuNom.SkuNom")

        Dim SQL As String = sb.ToString
        Dim oBrand As New DTOCatalog.Brand
        Dim oCategory As New DTOCatalog.Category

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOCatalog.Brand(oDrd("BrandGuid"), SQLHelper.GetStringFromDataReader(oDrd("BrandNom")))
                retval.Add(oBrand)
            End If

            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                Dim oCategoryLangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                oCategory = New DTOCatalog.Category(oDrd("CategoryGuid"), oCategoryLangNom.Tradueix(oLang))
                oBrand.Categories.Add(oCategory)
            End If

            Dim oSkuLangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNom", "SkuNomCat", "SkuNomEng", "SkuNomPor")
            Dim oSku As New DTOCatalog.Sku(oDrd("SkuGuid"), oSkuLangNom.Tradueix(oLang))
            oCategory.Skus.Add(oSku)
        Loop

        oDrd.Close()
        Return retval
    End Function



End Class

Public Class IncidenciasLoader

    Shared Function Model(oRequest As Models.IncidenciesModel.Request) As Models.IncidenciesModel
        Dim retval As New Models.IncidenciesModel
        Dim oUser = UserLoader.Find(oRequest.UserGuid)
        Dim oProductGuids As New HashSet(Of Guid)
        Dim oCodisAperturaGuids As New HashSet(Of Guid)
        Dim oCodisTancamentGuids As New HashSet(Of Guid)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id, Incidencies.Asin, Incidencies.Fch, Incidencies.FchClose, Incidencies.Cod ")
        sb.AppendLine(", SUBSTRING(CAST(Incidencies.Obs AS VARCHAR), 0, 40) AS Description ")
        sb.AppendLine(", Incidencies.ContactGuid, CliGral.FullNom ")
        sb.AppendLine(", Incidencies.SerialNumber, Incidencies.ManufactureDate ")
        sb.AppendLine(", Incidencies.CodiApertura, CodiApertura.Esp AS AperturaEsp, CodiApertura.Cat AS AperturaCat, CodiApertura.Eng AS AperturaEng, CodiApertura.Por AS AperturaPor ")
        sb.AppendLine(", Incidencies.CodiTancament, CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
        sb.AppendLine(", Incidencies.ProductGuid, VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.Cod AS ProductCod ")
        sb.AppendLine(", Incidencies.FchLastEdited, Incidencies.UsrLastEdited, VwUsrNickname.Nom AS UsrLastEditedNickname ")
        sb.AppendLine(", Docs.Images, Docs.Tickets, Docs.Videos ")
        sb.AppendLine("FROM Incidencies ")
        If oUser.Rol.id = DTORol.Ids.manufacturer Then
            sb.AppendLine("INNER JOIN Email_Clis ON VwProductNom.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oRequest.UserGuid.ToString() & "' ")
        End If
        sb.AppendLine("INNER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid=CliGral.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Incidencia ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.imatge) & " THEN 1 ELSE 0 END) AS Images ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.ticket) & " THEN 1 ELSE 0 END) AS Tickets ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.video) & " THEN 1 ELSE 0 END) AS Videos ")
        sb.AppendLine("FROM Incidencia_DocFiles GROUP BY Incidencia) Docs ON Incidencies.Guid=Docs.Incidencia ")

        sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiApertura ON Incidencies.CodiApertura = CodiApertura.Guid ")
        sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname ON Incidencies.UsrLastEdited = VwUsrNickname.Guid ")

        sb.AppendLine(" WHERE Incidencies.Emp = " & oUser.Emp.Id & " ")
        If oRequest.Year > 0 Then
            sb.AppendLine("AND YEAR(Incidencies.Fch)=" & oRequest.Year & " ")
        End If
        If oRequest.OnlyOpen Then
            sb.AppendLine("AND FchClose IS NULL ")
        End If
        If oRequest.CustomerGuid <> Nothing Then
            sb.AppendLine(" AND Incidencies.ContactGuid = '" & oRequest.CustomerGuid.ToString() & "' ")
        End If
        If oRequest.ProductGuid <> Nothing Then
            sb.AppendLine(" AND Incidencies.ProductGuid = '" & oRequest.ProductGuid.ToString() & "' ")
        End If

        sb.AppendLine(" ORDER BY Incidencies.Id DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProductGuid = SQLHelper.GetGuidFromDataReader(oDrd("ProductGuid"))
            If Not oProductGuids.Contains(oProductGuid) Then
                Dim oProduct = ProductLoader.NewProduct(oDrd("ProductCod"), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                retval.Catalog.AddProduct(oProduct)
                oProductGuids.Add(oProductGuid)
            End If

            Dim oCodiAperturaGuid As Guid = SQLHelper.GetGuidFromDataReader(oDrd("CodiApertura"))
            If oCodiAperturaGuid <> Nothing AndAlso Not oCodisAperturaGuids.Contains(oCodiAperturaGuid) Then
                Dim oLangText = SQLHelper.GetLangTextFromDataReader(oDrd, "AperturaEsp", "AperturaCat", "AperturaEng", "AperturaPor")
                Dim oCodiApertura = DTOGuidNom.Compact.Factory(oCodiAperturaGuid, oLangText.Tradueix(oUser.Lang))
                retval.CodisApertura.Add(oCodiApertura)
                oCodisAperturaGuids.Add(oCodiAperturaGuid)
            End If

            Dim oCodiTancamentGuid As Guid = SQLHelper.GetGuidFromDataReader(oDrd("CodiTancament"))
            If oCodiTancamentGuid <> Nothing AndAlso Not oCodisTancamentGuids.Contains(oCodiTancamentGuid) Then
                Dim oLangText = SQLHelper.GetLangTextFromDataReader(oDrd, "TancamentEsp", "TancamentCat", "TancamentEng", "TancamentPor")
                Dim oCodiTancament = DTOGuidNom.Compact.Factory(oCodiTancamentGuid, oLangText.Tradueix(oUser.Lang))
                retval.CodisTancament.Add(oCodiTancament)
                oCodisTancamentGuids.Add(oCodiTancamentGuid)
            End If

            Dim oCustomer As DTOGuidNom.Compact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oCustomer = DTOGuidNom.Compact.Factory(oDrd("ContactGuid"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                If Not retval.Customers.Any(Function(x) x.Guid.Equals(oDrd("ContactGuid"))) Then
                    retval.Customers.Add(oCustomer)
                End If
            End If

            Dim oItem As New Models.IncidenciesModel.Item
            With oItem
                .Guid = oDrd("Guid")
                .Num = oDrd("Id")
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                .Fch = oDrd("Fch")
                .FchClose = SQLHelper.GetFchFromDataReader(oDrd("FchClose"))
                .Src = oDrd("Cod")
                .ManufactureDate = SQLHelper.GetStringFromDataReader(oDrd("ManufactureDate"))
                .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))

                If oCustomer IsNot Nothing Then .CustomerGuid = oCustomer.Guid
                If oProductGuid <> Nothing Then .ProductGuid = oProductGuid
                If oCodiTancamentGuid <> Nothing Then .CodTancament = oCodiTancamentGuid
                If oCodiAperturaGuid <> Nothing Then .CodApertura = oCodiAperturaGuid

                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Description"))
                .HasImg = SQLHelper.GetIntegerFromDataReader(oDrd("Images"))
                .HasTicket = SQLHelper.GetIntegerFromDataReader(oDrd("Tickets"))
                .HasVideo = SQLHelper.GetIntegerFromDataReader(oDrd("Videos"))
                .UserNom = SQLHelper.GetStringFromDataReader(oDrd("UsrLastEditedNickname"))
                .FchLastEdited = SQLHelper.GetFchFromDataReader(oDrd("FchLastEdited"))
            End With
            retval.Items.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Compact(oTitular As DTOBaseGuid, oTitularCod As DTOEnums.TitularCods) As List(Of DTOIncidencia.Compact)
        Dim retval As New List(Of DTOIncidencia.Compact)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id, Incidencies.Asin, Incidencies.Fch, Incidencies.ContactGuid ")
        sb.AppendLine(", Incidencies.sRef, Incidencies.CodiTancament ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.Cod AS ProductCod ")
        sb.AppendLine(", Docs.Images, Docs.Tickets, Docs.Videos ")
        sb.AppendLine(", CliGral.FullNom, CliGral.LangId AS CustomerLang ")
        sb.AppendLine("FROM Incidencies ")
        Select Case oTitularCod
            Case DTOEnums.TitularCods.User
                sb.AppendLine("INNER JOIN Email_Clis ON Incidencies.ContactGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oTitular.Guid.ToString() & "' ")
                'Case DTOEnums.TitularCods.Contact
                'sb.AppendLine("WHERE Incidencies.ContactGuid = '" & oTitular.Guid.ToString() & "'")
        End Select
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Incidencia ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.imatge) & " THEN 1 ELSE 0 END) AS Images ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.ticket) & " THEN 1 ELSE 0 END) AS Tickets ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.video) & " THEN 1 ELSE 0 END) AS Videos ")
        sb.AppendLine("FROM Incidencia_DocFiles GROUP BY Incidencia) Docs ON Incidencies.Guid=Docs.Incidencia ")
        Select Case oTitularCod
            'Case DTOEnums.TitularCods.User
                'sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oTitular.Guid.ToString() & "'")
            Case DTOEnums.TitularCods.Contact
                sb.AppendLine("WHERE Incidencies.ContactGuid = '" & oTitular.Guid.ToString() & "'")
        End Select
        sb.AppendLine("ORDER BY Incidencies.Id DESC")

        Dim oLang As DTOLang = DTOLang.ESP()

        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct = SQLHelper.GetProductFromDataReader(oDrd)
            Dim oItem As New DTOIncidencia.Compact()
            With oItem
                .Guid = oDrd("Guid")
                .Num = oDrd("Id")
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                .Fch = oDrd("Fch")
                .Contact = DTOGuidNom.Compact.Factory(oDrd("ContactGuid"), oDrd("FullNom"))
                If oProduct IsNot Nothing Then
                    Select Case oTitularCod
                        Case DTOEnums.TitularCods.User
                            oLang = CType(oTitular, DTOUser).Lang
                        Case DTOEnums.TitularCods.Contact
                            oLang = SQLHelper.GetLangFromDataReader(oDrd("CustomerLang"))
                    End Select

                    .Product = oProduct.ToGuidNom(oLang)
                End If
                .SRef = SQLHelper.GetStringFromDataReader(oDrd("sRef"))
                .ExistTickets = SQLHelper.GetIntegerFromDataReader(oDrd("Tickets")) > 0
                .ExistImages = SQLHelper.GetIntegerFromDataReader(oDrd("Images")) > 0
                .ExistVideos = SQLHelper.GetIntegerFromDataReader(oDrd("Videos")) > 0
                .IsOpen = IsDBNull(oDrd("CodiTancament"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Headers(oUser As DTOUser, Optional oContact As DTOContact = Nothing) As List(Of DTOIncidencia)
        Dim retval As New List(Of DTOIncidencia)
        Dim oRol As New DTORol(DTORol.Ids.cliFull)
        If oUser IsNot Nothing Then oRol = oUser.Rol

        Dim sb As New System.Text.StringBuilder
        Select Case oRol.id
            Case DTORol.Ids.manufacturer
                sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id, Incidencies.Asin, Incidencies.Fch, Incidencies.FchClose, Incidencies.ProductGuid, Incidencies.Cod ")
                sb.AppendLine(", Incidencies.ContactType, Incidencies.ContactGuid ")
                sb.AppendLine(", Incidencies.Person, Incidencies.Tel, Incidencies.Email ")
                sb.AppendLine(", Incidencies.sRef, Incidencies.Procedencia, Incidencies.FchCompra, Incidencies.Obs, Incidencies.SerialNumber, Incidencies.ManufactureDate ")
                sb.AppendLine(", Incidencies.CodiApertura, CodiApertura.Esp AS AperturaEsp, CodiApertura.Cat AS AperturaCat, CodiApertura.Eng AS AperturaEng, CodiApertura.Por AS AperturaPor ")
                sb.AppendLine(", Incidencies.CodiTancament, CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
                sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.Cod AS ProductCod ")
                sb.AppendLine(", Docs.Images, Docs.Tickets, Docs.Videos ")
                sb.AppendLine(", CliGral.FullNom ")
                sb.AppendLine("FROM Incidencies ")
                sb.AppendLine("INNER JOIN VwProductParent ON Incidencies.ProductGuid = VwProductParent.Child ")
                sb.AppendLine("INNER JOIN Tpa ON Tpa.Guid = VwProductParent.Parent ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = Tpa.Proveidor AND Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiApertura ON Incidencies.CodiApertura = CodiApertura.Guid ")
                sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
                sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
                sb.AppendLine("LEFT OUTER JOIN (SELECT Incidencia ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.imatge) & " THEN 1 ELSE 0 END) AS Images ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.ticket) & " THEN 1 ELSE 0 END) AS Tickets ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.video) & " THEN 1 ELSE 0 END) AS Videos ")
                sb.AppendLine("FROM Incidencia_DocFiles GROUP BY Incidencia) Docs ON Incidencies.Guid=Docs.Incidencia ")
                sb.AppendLine("WHERE Incidencies.Emp='" & oUser.Emp.Id & "'")
                If oContact IsNot Nothing Then
                    sb.AppendLine("AND Incidencies.ContactGuid='" & oContact.Guid.ToString & "'")
                End If

                sb.AppendLine("ORDER BY Incidencies.Id DESC")

            Case Else
                sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id, Incidencies.Asin, Incidencies.Fch, Incidencies.FchClose, Incidencies.ProductGuid, Incidencies.Cod ")
                sb.AppendLine(", Incidencies.ContactType, Incidencies.ContactGuid ")
                sb.AppendLine(", Incidencies.Person, Incidencies.Tel, Incidencies.Email ")
                sb.AppendLine(", Incidencies.sRef, Incidencies.Procedencia, Incidencies.FchCompra, Incidencies.Obs, Incidencies.SerialNumber, Incidencies.ManufactureDate ")
                sb.AppendLine(", Incidencies.CodiApertura, CodiApertura.Esp AS AperturaEsp, CodiApertura.Cat AS AperturaCat, CodiApertura.Eng AS AperturaEng, CodiApertura.Por AS AperturaPor ")
                sb.AppendLine(", Incidencies.CodiTancament, CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
                sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.Cod AS ProductCod ")
                sb.AppendLine(", Docs.Images, Docs.Tickets, Docs.Videos ")
                sb.AppendLine(", CliGral.FullNom ")
                sb.AppendLine("FROM Incidencies ")
                sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiApertura ON Incidencies.CodiApertura = CodiApertura.Guid ")
                sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
                sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
                sb.AppendLine("LEFT OUTER JOIN (SELECT Incidencia ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.imatge) & " THEN 1 ELSE 0 END) AS Images ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.ticket) & " THEN 1 ELSE 0 END) AS Tickets ")
                sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.video) & " THEN 1 ELSE 0 END) AS Videos ")
                sb.AppendLine("FROM Incidencia_DocFiles GROUP BY Incidencia) Docs ON Incidencies.Guid=Docs.Incidencia ")
                sb.AppendLine("WHERE Incidencies.Emp='" & oUser.Emp.Id & "'")
                If oContact IsNot Nothing Then
                    sb.AppendLine("AND Incidencies.ContactGuid='" & oContact.Guid.ToString & "'")
                End If
                sb.AppendLine("ORDER BY Incidencies.Id DESC")
        End Select
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIncidencia(DirectCast(oDrd("Guid"), Guid))
            With item
                .Num = oDrd("Id")
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                .src = oDrd("Cod")
                If Not IsDBNull(oDrd("CodiApertura")) Then
                    .Codi = New DTOIncidenciaCod(oDrd("CodiApertura"))
                    .Codi.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "AperturaEsp", "AperturaCat", "AperturaEng", "AperturaPor")
                End If

                If Not IsDBNull(oDrd("CodiTancament")) Then
                    .Tancament = New DTOIncidenciaCod(oDrd("CodiTancament"))
                    .Tancament.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "TancamentEsp", "TancamentCat", "TancamentEng", "TancamentPor")
                End If

                .Fch = oDrd("Fch")
                .FchClose = SQLHelper.GetFchFromDataReader(oDrd("FchClose"))

                .ContactType = oDrd("ContactType")

                If Not IsDBNull(oDrd("ContactGuid")) Then
                    .Customer = New DTOCustomer(oDrd("ContactGuid"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If

                .ContactPerson = SQLHelper.GetStringFromDataReader(oDrd("Person"))
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .EmailAdr = SQLHelper.GetStringFromDataReader(oDrd("Email"))
                .CustomerRef = SQLHelper.GetStringFromDataReader(oDrd("sRef"))
                .Procedencia = SQLHelper.GetStringFromDataReader(oDrd("Procedencia"))
                .FchCompra = SQLHelper.GetNullableFchFromDataReader(oDrd("FchCompra"))
                .Description = SQLHelper.GetStringFromDataReader(oDrd("Obs"))

                If Not IsDBNull(oDrd("ProductCod")) Then
                    .Product = ProductLoader.NewProduct(oDrd("ProductCod"), oDrd("BrandGuid"), oDrd("BrandNom"), oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                End If

                .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
                .ManufactureDate = SQLHelper.GetStringFromDataReader(oDrd("ManufactureDate"))

                If Not IsDBNull(oDrd("Images")) Then
                    .ExistImages = (CInt(oDrd("Images")) > 0)
                End If
                If Not IsDBNull(oDrd("Tickets")) Then
                    .ExistTickets = (CInt(oDrd("Tickets")) > 0)
                End If
                If Not IsDBNull(oDrd("Videos")) Then
                    Dim videosCount As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Videos"))
                    .ExistVideos = videosCount > 0
                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Sub LoadQuery(ByRef oQuery As DTOIncidenciaQuery)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id,Incidencies.Asin,  Incidencies.Fch, Incidencies.FchClose, SUBSTRING(CAST(Incidencies.Obs AS VARCHAR), 0, 40) AS Description ")
        sb.AppendLine(", Incidencies.ContactGuid, Incidencies.Procedencia, Incidencies.FchCompra, CliGral.FullNom ")
        sb.AppendLine(", Incidencies.CodiApertura, CodiApertura.Esp AS AperturaEsp, CodiApertura.Cat AS AperturaCat, CodiApertura.Eng AS AperturaEng, CodiApertura.Por AS AperturaPor ")
        sb.AppendLine(", Incidencies.CodiTancament, CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
        sb.AppendLine(", Incidencies.ProductGuid, VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.Cod AS ProductCod, Incidencies.SerialNumber, Incidencies.ManufactureDate ")
        sb.AppendLine(", Incidencies.UsrLastEdited, UsrLastEdited.Adr AS UsrLastEditedAdr, UsrLastEdited.Nickname AS UsrLastEditedNickname, Incidencies.FchLastEdited ")
        sb.AppendLine(", Docs.Images, Docs.Tickets, Docs.Videos ")
        'sb.AppendLine(", SUM(CASE WHEN Incidencia_DocFiles.Cod=1 THEN 1 ELSE 0 END) AS Imgs ")
        sb.AppendLine("FROM Incidencies ")
        sb.AppendLine("INNER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
        If oQuery.Product IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent ON Incidencies.ProductGuid = VwProductParent.Child AND VwProductParent.Parent = '" & oQuery.Product.Guid.ToString & "' ")
        End If
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid=CliGral.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Incidencia ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.imatge) & " THEN 1 ELSE 0 END) AS Images ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.ticket) & " THEN 1 ELSE 0 END) AS Tickets ")
        sb.AppendLine(", SUM( CASE WHEN Cod=" & CInt(DTOIncidencia.AttachmentCods.video) & " THEN 1 ELSE 0 END) AS Videos ")
        sb.AppendLine("FROM Incidencia_DocFiles GROUP BY Incidencia) Docs ON Incidencies.Guid=Docs.Incidencia ")

        sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiApertura ON Incidencies.CodiApertura = CodiApertura.Guid ")
        sb.AppendLine("LEFT OUTER JOIN IncidenciesCods AS CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email UsrLastEdited ON Incidencies.UsrLastEdited = UsrLastEdited.Guid ")
        sb.AppendLine("WHERE Incidencies.Emp=" & oQuery.emp.Id & " ")
        sb.AppendLine("AND YEAR(Incidencies.Fch)<=" & oQuery.Year & " ")

        If oQuery.IncludeClosed Then
            sb.AppendLine("AND (Incidencies.FchClose IS NULL OR YEAR(Incidencies.FchClose)=" & oQuery.Year & ") ")
        Else
            sb.AppendLine("AND Incidencies.FchClose IS NULL ")
        End If
        If oQuery.Src <> DTOIncidencia.Srcs.notSet Then
            sb.AppendLine(" AND Incidencies.Cod=" & CInt(DTOIncidencia.Srcs.Producte).ToString & " ")
        End If
        If oQuery.Tancament IsNot Nothing Then
            sb.AppendLine(" AND Incidencies.CodiTancament = '" & oQuery.Tancament.Guid.ToString & "' ")
        End If
        If oQuery.Codi IsNot Nothing Then
            sb.AppendLine(" AND Incidencies.CodiApertura = '" & oQuery.Codi.Guid.ToString & "' ")
        End If
        If oQuery.Customer IsNot Nothing Then
            sb.AppendLine(" AND Incidencies.ContactGuid = '" & oQuery.Customer.Guid.ToString & "' ")
        End If
        If oQuery.Manufacturer IsNot Nothing Then
            sb.AppendLine("AND VwProductNom.Proveidor = '" & oQuery.Manufacturer.Guid.ToString & "' ")
        End If

        sb.AppendLine(" ORDER BY Incidencies.Id DESC")

        oQuery.result = New List(Of DTOIncidencia)

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read


            Dim oIncidencia As New DTOIncidencia(DirectCast(oDrd("Guid"), Guid))
            With oIncidencia
                .Num = oDrd("Id")
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                .Fch = oDrd("Fch")

                If Not IsDBNull(oDrd("CodiApertura")) Then
                    .Codi = New DTOIncidenciaCod(oDrd("CodiApertura"))
                    .Codi.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "AperturaEsp", "AperturaCat", "AperturaEng", "AperturaPor")
                End If

                If Not IsDBNull(oDrd("CodiTancament")) Then
                    .Tancament = New DTOIncidenciaCod(oDrd("CodiTancament"))
                    .Tancament.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "TancamentEsp", "TancamentCat", "TancamentEng", "TancamentPor")
                End If

                .Procedencia = oDrd("Procedencia")
                .FchCompra = SQLHelper.GetNullableFchFromDataReader(oDrd("FchCompra"))
                If Not IsDBNull(oDrd("FchClose")) Then
                    .FchClose = oDrd("FchClose")
                End If

                If oQuery.Customer Is Nothing Then
                    If Not IsDBNull(oDrd("ContactGuid")) Then
                        .Customer = New DTOCustomer(oDrd("ContactGuid"))
                        .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    End If
                Else
                    .Customer = oQuery.Customer
                End If

                If Not IsDBNull(oDrd("ProductCod")) Then
                    .Product = ProductLoader.NewProduct(oDrd("ProductCod"), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                End If
                .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
                .ManufactureDate = SQLHelper.GetStringFromDataReader(oDrd("ManufactureDate"))

                If Not IsDBNull(oDrd("Description")) Then
                    .Description = oDrd("Description")
                End If
                .ExistImages = SQLHelper.GetIntegerFromDataReader(oDrd("Images"))
                .ExistTickets = SQLHelper.GetIntegerFromDataReader(oDrd("Tickets"))
                .ExistVideos = SQLHelper.GetIntegerFromDataReader(oDrd("Videos"))
                If Not IsDBNull(oDrd("UsrLastEdited")) Then
                    .UsrLog.UsrLastEdited = New DTOUser(DirectCast(oDrd("UsrLastEdited"), Guid))
                    .UsrLog.UsrLastEdited.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrLastEditedAdr"))
                    .UsrLog.UsrLastEdited.NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrLastEditedNickName"))
                    .UsrLog.FchLastEdited = SQLHelper.GetFchFromDataReader(oDrd("FchLastEdited"))
                End If
            End With
            oQuery.result.Add(oIncidencia)
        Loop
        oDrd.Close()
    End Sub


    Shared Function CodisDeTancament() As List(Of DTOIncidenciaCod)
        Dim retval As New List(Of DTOIncidenciaCod)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Incidencies.CodiTancament ")
        sb.AppendLine(", CodiTancament.Esp AS TancamentEsp, CodiTancament.Cat AS TancamentCat, CodiTancament.Eng AS TancamentEng, CodiTancament.Por AS TancamentPor ")
        sb.AppendLine("FROM Incidencies ")
        sb.AppendLine("INNER JOIN IncidenciesCods CodiTancament ON Incidencies.CodiTancament = CodiTancament.Guid ")
        sb.AppendLine("GROUP BY Incidencies.CodiTancament ")
        sb.AppendLine(", CodiTancament.Esp, CodiTancament.Cat, CodiTancament.Eng, CodiTancament.Por ")
        sb.AppendLine("ORDER BY TancamentEsp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCodi As New DTOIncidenciaCod(oDrd("CodiTancament"))
            oCodi.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "TancamentEsp", "TancamentCat", "TancamentEng", "TancamentPor")
            retval.Add(oCodi)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Reposicions(oEmp As DTOEmp, iYea As Integer) As List(Of DTOIncidencia)
        Dim retval As New List(Of DTOIncidencia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Incidencies.Guid, Incidencies.Id, Incidencies.Asin, Incidencies.Fch ")
        sb.AppendLine(", Incidencies.ContactGuid, CliGral.FullNom ")
        sb.AppendLine(", Incidencies.ProductGuid AS SkuGuid, VwSkuNom.BrandGuid, VwSkuNom.CategoryGuid, VwSkuNom.BrandNom, VwSkuNom.CategoryNom, VwSkuNom.SkuNomLlarg ")
        sb.AppendLine(", VwSkuCost.Price, VwSkuCost.Discount_OnInvoice ")
        sb.AppendLine("FROM Incidencies ")
        sb.AppendLine("INNER JOIN CliGral ON Incidencies.ContactGuid= CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Incidencies.ProductGuid= VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN IncidenciesCods ON Incidencies.CodiTancament=IncidenciesCods.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuCost ON Incidencies.ProductGuid = VwSkuCost.SkuGuid ")
        sb.AppendLine("WHERE Incidencies.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND YEAR(Incidencies.Fch)=" & iYea & " ")
        sb.AppendLine("AND IncidenciesCods.ReposicionTotal = 1 ")
        sb.AppendLine("ORDER BY Incidencies.Id ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTOIncidencia(DirectCast(oDrd("Guid"), Guid))
            With item
                .Num = oDrd("Id")
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("ContactGuid"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
                Dim oSku As DTOProductSku = .Product
                If Not oSku Is Nothing Then
                    Dim DtBrut As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Price"))
                    Dim dto As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Discount_OnInvoice"))
                    oSku.Cost = DTOAmt.Factory(DtBrut * (100 - dto) / 100)
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Ratios(FchFrom As Date, FchTo As Date) As List(Of Tuple(Of DTOProductCategory, Integer, Integer))
        Dim retval As New List(Of Tuple(Of DTOProductCategory, Integer, Integer))
        Dim sFchFrom As String = Format(FchFrom, "yyyyMMdd")
        Dim sFchTo As String = Format(FchTo, "yyyyMMdd")
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, Count(Incidencies.Guid) AS Incs, X.Qty AS Sales ")
        sb.AppendLine("FROM incidencies ")
        sb.AppendLine("INNER JOIN VwProductNom ON Incidencies.ProductGuid=VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Art.Category, SUM(Arc.Qty) AS Qty FROM Arc INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid INNER JOIN Art ON Arc.ArtGuid = Art.Guid WHERE Arc.Cod>49 AND (Alb.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "') GROUP BY  Art.Category) X ON VwProductNom.CategoryGuid = X.Category ")
        sb.AppendLine("WHERE Incidencies.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "' ")
        sb.AppendLine("GROUP BY VwProductNom.BrandGuid, VwProductNom.CategoryGuid, VwProductNom.BrandNom, VwProductNom.CategoryNom, X.Qty ")
        sb.AppendLine("ORDER BY 100*Count(Incidencies.Guid)/ X.Qty DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(oDrd("BrandGuid"))
            SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            Dim oCategory As DTOProductCategory
            If IsDBNull(oDrd("CategoryGuid")) Then
                oCategory = New DTOProductCategory()
                With oCategory
                    .Brand = oBrand
                    .Nom.Esp = "(sense definir)"
                End With
            Else
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                With oCategory
                    .Brand = oBrand
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNom", "CategoryNom", "CategoryNom", "CategoryNom")
                End With
            End If
            Dim item As New Tuple(Of DTOProductCategory, Integer, Integer)(oCategory, SQLHelper.GetIntegerFromDataReader(oDrd("Incs")), SQLHelper.GetIntegerFromDataReader(oDrd("Sales")))

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function withVideos() As List(Of Guid)
        Dim retval As New List(Of Guid)
        Dim SQL As String = "SELECT Incidencia FROM Incidencia_DocFiles where cod=2 GROUP BY Incidencia "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Incidencia")
            retval.Add(oGuid)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
