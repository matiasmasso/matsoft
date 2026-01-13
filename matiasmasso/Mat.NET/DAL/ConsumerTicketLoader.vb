Public Class ConsumerTicketLoader

    Shared Function Find(oGuid As Guid) As DTOConsumerTicket
        Dim retval As DTOConsumerTicket = Nothing
        Dim oConsumerTicket As New DTOConsumerTicket(oGuid)
        If Load(oConsumerTicket) Then
            retval = oConsumerTicket
        End If
        Return retval
    End Function

    Shared Function Find(oMarketPlace As DTOMarketPlace, orderId As String) As DTOConsumerTicket
        Dim retval As DTOConsumerTicket = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ConsumerTicket.Guid ")
        sb.AppendLine("FROM ConsumerTicket ")
        sb.AppendLine("WHERE ConsumerTicket.MarketPlace='" & oMarketPlace.Guid.ToString & "' ")
        sb.AppendLine("AND ConsumerTicket.OrderNum='" & orderId & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOConsumerTicket(oDrd("Guid"))
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oConsumerTicket As DTOConsumerTicket) As Boolean
        If Not oConsumerTicket.IsLoaded And Not oConsumerTicket.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ConsumerTicket.* ")
            sb.AppendLine(", MarketPlace.Nom AS MarketPlaceNom ")
            sb.AppendLine(", UsrTrackingNotified.Nom AS UsrTrackingNotifiedNom ")
            sb.AppendLine(", UsrDelivered.Nom AS UsrDeliveredNom ")
            sb.AppendLine(", UsrReviewRequest.Nom AS UsrReviewRequestNom ")
            sb.AppendLine(", Spv.Id AS SpvId ")
            sb.AppendLine(", VwDeliveryTrackingTrp.Tracking ")
            sb.AppendLine(", VwDeliveryTrackingTrp.TrpGuid ")
            sb.AppendLine(", VwDeliveryTrackingTrp.TrpNom ")
            sb.AppendLine(", VwDeliveryTrackingTrp.Alb ")
            sb.AppendLine(", Cca.Cca AS CcaId, Cca.Fch as CcaFch ")

            sb.AppendLine(", TicketAdr.Adr AS TicketAdrAdr, TicketAdr.ZipGuid AS TicketAdrZipGuid, TicketAdr.ZipCod AS TicketAdrZipCod, TicketAdr.LocationGuid AS TicketAdrLocationGuid, TicketAdr.LocationNom AS TicketAdrLocationNom ")
            sb.AppendLine(", TicketAdr.ProvinciaGuid AS TicketAdrProvinciaGuid, TicketAdr.ZonaGuid AS TicketAdrZonaGuid, TicketAdr.ProvinciaNom AS TicketAdrProvinciaNom, TicketAdr.CountryGuid AS TicketAdrCountryGuid, TicketAdr.CountryEsp AS TicketAdrCountryEsp, TicketAdr.ExportCod AS TicketAdrExportCod ")

            sb.AppendLine(", FraAdr.Adr AS FraAdrAdr, FraAdr.ZipGuid AS FraAdrZipGuid, FraAdr.ZipCod AS FraAdrZipCod, FraAdr.LocationGuid AS FraAdrLocationGuid, FraAdr.LocationNom AS FraAdrLocationNom ")
            sb.AppendLine(", FraAdr.ProvinciaGuid AS FraAdrProvinciaGuid, FraAdr.ZonaGuid AS FraAdrZonaGuid, FraAdr.ProvinciaNom AS FraAdrProvinciaNom, FraAdr.CountryGuid AS FraAdrCountryGuid, FraAdr.CountryEsp AS FraAdrCountryEsp, FraAdr.ExportCod AS FraAdrExportCod ")

            sb.AppendLine("FROM ConsumerTicket ")
            sb.AppendLine("LEFT OUTER JOIN VwAddressBase TicketAdr ON ConsumerTicket.Guid = TicketAdr.SrcGuid AND TicketAdr.Cod = " & DTOAddress.Codis.Fiscal & " ")
            sb.AppendLine("LEFT OUTER JOIN VwAddressBase FraAdr ON ConsumerTicket.Guid = FraAdr.SrcGuid And FraAdr.Cod = " & DTOAddress.Codis.FraConsumidor & " ")
            sb.AppendLine("LEFT OUTER JOIN Spv ON ConsumerTicket.Spv=Spv.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON ConsumerTicket.Cca=Cca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwDeliveryTrackingTrp ON ConsumerTicket.Delivery = VwDeliveryTrackingTrp.AlbGuid ")
            sb.AppendLine("LEFT OUTER JOIN MarketPlace ON ConsumerTicket.MarketPlace = MarketPlace.Guid ")

            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrTrackingNotified ON ConsumerTicket.UsrTrackingNotified=UsrTrackingNotified.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrDelivered ON ConsumerTicket.UsrDelivered=UsrDelivered.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrReviewRequest ON ConsumerTicket.UsrReviewRequest=UsrReviewRequest.Guid ")

            sb.AppendLine("WHERE ConsumerTicket.Guid='" & oConsumerTicket.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oConsumerTicket
                    .Id = oDrd("Id")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    .Fch = oDrd("Fch")
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Cognom1 = SQLHelper.GetStringFromDataReader(oDrd("CogNom1"))
                    .Cognom2 = SQLHelper.GetStringFromDataReader(oDrd("CogNom2"))
                    .Nif = SQLHelper.GetStringFromDataReader(oDrd("Nif"))
                    .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd, AdrField:="TicketAdrAdr", ZipGuidField:="TicketAdrZipGuid", ZipCodField:="TicketAdrZipCod", LocationGuidField:="TicketAdrLocationGuid", LocationNomField:="TicketAdrLocationNom", ProvinciaGuidField:="TicketAdrProvinciaGuid",
                                                                  ZonaGuidField:="TicketAdrZonaGuid", ProvinciaNomField:="TicketAdrProvinciaNom", CountryGuidField:="TicketAdrCountryGuid", CountryEspField:="TicketAdrCountryEsp", ExportCodField:="TicketAdrExportCod")
                    .Address.Codi = DTOAddress.Codis.Fiscal
                    .FraNom = SQLHelper.GetStringFromDataReader(oDrd("FraNom"))
                    .FraAddress = SQLHelper.GetAddressFromDataReader(oDrd, AdrField:="FraAdrAdr", ZipGuidField:="FraAdrZipGuid", ZipCodField:="FraAdrZipCod", LocationGuidField:="FraAdrLocationGuid", LocationNomField:="FraAdrLocationNom", ProvinciaGuidField:="FraAdrProvinciaGuid",
                                                                  ZonaGuidField:="FraAdrZonaGuid", ProvinciaNomField:="FraAdrProvinciaNom", CountryGuidField:="FraAdrCountryGuid", CountryEspField:="FraAdrCountryEsp", ExportCodField:="FraAdrExportCod")
                    .FraAddress.Codi = DTOAddress.Codis.FraConsumidor
                    .Op = oDrd("Op")
                    If Not IsDBNull(oDrd("PurchaseOrder")) Then
                        .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                    End If
                    If Not IsDBNull(oDrd("Spv")) Then
                        .Spv = New DTOSpv(oDrd("Guid")) With {
                            .id = oDrd("SpvId")
                        }
                    End If
                    If Not IsDBNull(oDrd("Delivery")) Then
                        .Delivery = New DTODelivery(oDrd("Delivery"))
                        .Delivery.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                        .Delivery.Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                        If Not IsDBNull(oDrd("TrpGuid")) Then
                            .Delivery.Transportista = New DTOTransportista(oDrd("TrpGuid"))
                            .Delivery.Transportista.Abr = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                        End If
                    End If

                    If Not IsDBNull(oDrd("Cca")) Then
                        .Cca = New DTOCca(oDrd("Cca"))
                        With .Cca
                            .Id = oDrd("CcaId")
                            .Fch = oDrd("CcaFch")
                        End With
                    End If

                    If Not IsDBNull(oDrd("MarketPlace")) Then
                        .MarketPlace = New DTOMarketPlace(oDrd("MarketPlace"))
                        .MarketPlace.Nom = SQLHelper.GetStringFromDataReader(oDrd("MarketPlaceNom"))
                    End If
                    .OrderId = SQLHelper.GetStringFromDataReader(oDrd("OrderNum"))

                    .FchTrackingNotified = SQLHelper.GetFchFromDataReader(oDrd("FchTrackingNotified"))
                    .FchDelivered = SQLHelper.GetFchFromDataReader(oDrd("FchDelivered"))
                    .FchReviewRequest = SQLHelper.GetFchFromDataReader(oDrd("FchReviewRequest"))

                    If Not IsDBNull(oDrd("UsrTrackingNotified")) Then
                        .UsrTrackingNotified = New DTOGuidNom(oDrd("UsrTrackingNotified"), SQLHelper.GetStringFromDataReader(oDrd("UsrTrackingNotifiedNom")))
                    End If
                    If Not IsDBNull(oDrd("UsrDelivered")) Then
                        .UsrDelivered = New DTOGuidNom(oDrd("UsrDelivered"), SQLHelper.GetStringFromDataReader(oDrd("UsrDeliveredNom")))
                    End If
                    If Not IsDBNull(oDrd("UsrReviewRequest")) Then
                        .UsrReviewRequest = New DTOGuidNom(oDrd("UsrReviewRequest"), SQLHelper.GetStringFromDataReader(oDrd("UsrReviewRequestNom")))
                    End If

                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Select Case oConsumerTicket.Op
            Case DTOPurchaseOrder.Codis.reparacio
                oConsumerTicket.Spv = SpvLoader.Find(oConsumerTicket.Guid)
            Case Else
                Dim oPurchaseOrder = PurchaseOrderLoader.Find(oConsumerTicket.PurchaseOrder.Guid)
                If oPurchaseOrder IsNot Nothing Then
                    oConsumerTicket.PurchaseOrder = PurchaseOrderLoader.Find(oConsumerTicket.PurchaseOrder.Guid)
                End If
        End Select

        If oConsumerTicket.Delivery IsNot Nothing Then
            DeliveryLoader.Load(oConsumerTicket.Delivery)
        End If

        Dim retval As Boolean = oConsumerTicket.IsLoaded
        Return retval
    End Function

    Shared Function FromDelivery(oDelivery As DTODelivery) As DTOConsumerTicket
        Dim retval As DTOConsumerTicket = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ConsumerTicket.*, VwAddress.* ")
        sb.AppendLine("FROM ConsumerTicket ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON ConsumerTicket.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE ConsumerTicket.Delivery ='" & oDelivery.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOConsumerTicket(oDrd("Guid"))
            With retval
                .Id = oDrd("Id")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .Fch = oDrd("Fch")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognom1 = SQLHelper.GetStringFromDataReader(oDrd("CogNom1"))
                .Cognom2 = SQLHelper.GetStringFromDataReader(oDrd("CogNom2"))
                .Nif = SQLHelper.GetStringFromDataReader(oDrd("Nif"))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oConsumerTicket As DTOConsumerTicket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            If oConsumerTicket.IsNew Then
                PurchaseOrderLoader.Update(oConsumerTicket.PurchaseOrder, oTrans)
                DeliveryLoader.Update(oConsumerTicket.Delivery, oTrans)
            End If

            Update(oConsumerTicket, oTrans)

            If oConsumerTicket.IsNew Then
                Dim oPlan = PgcPlanLoader.FromYear(DTO.GlobalVariables.Today().Year)
                Dim oAllCtas = PgcCtasLoader.All(oPlan)
                If oConsumerTicket.Cca IsNot Nothing Then

                    oConsumerTicket.CompleteCca(oAllCtas)
                    CcaLoader.Update(oConsumerTicket.Cca, oTrans)
                End If
            End If

            oConsumerTicket.Address.Src = oConsumerTicket
            oConsumerTicket.Address.Codi = DTOAddress.Codis.Fiscal
            AddressLoader.Update(oConsumerTicket.Address, oTrans)
            If oConsumerTicket.FraAddress IsNot Nothing Then
                oConsumerTicket.FraAddress.Src = oConsumerTicket
                oConsumerTicket.FraAddress.Codi = DTOAddress.Codis.FraConsumidor
                AddressLoader.Update(oConsumerTicket.FraAddress, oTrans)
            End If

            oTrans.Commit()
            oConsumerTicket.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oConsumerTicket As DTOConsumerTicket, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ConsumerTicket ")
        sb.AppendLine("WHERE Guid='" & oConsumerTicket.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString


        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            If oConsumerTicket.Id = 0 Then oConsumerTicket.Id = NextId(oConsumerTicket.PurchaseOrder.emp, Year(oConsumerTicket.Fch), oTrans)
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oConsumerTicket.Guid
            oRow("Emp") = oConsumerTicket.PurchaseOrder.Emp.Id
            oRow("Id") = oConsumerTicket.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oConsumerTicket
            If .Lang Is Nothing Then .Lang = DTOLang.ESP
            oRow("Lang") = .Lang.Tag
            oRow("Fch") = .Fch
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Cognom1") = SQLHelper.NullableString(.Cognom1)
            oRow("Cognom2") = SQLHelper.NullableString(.Cognom2)
            oRow("Nif") = SQLHelper.NullableString(.Nif)
            oRow("FraNom") = SQLHelper.NullableString(.FraNom)
            oRow("Tel") = SQLHelper.NullableString(.Tel)
            oRow("EmailAddress") = SQLHelper.NullableString(.EmailAddress)
            oRow("PurchaseOrder") = SQLHelper.NullableBaseGuid(.PurchaseOrder)
            oRow("Spv") = SQLHelper.NullableBaseGuid(.Spv)
            oRow("Delivery") = SQLHelper.NullableBaseGuid(.Delivery)
            oRow("Cca") = SQLHelper.NullableBaseGuid(.Cca)
            oRow("MarketPlace") = SQLHelper.NullableBaseGuid(.MarketPlace)
            oRow("OrderNum") = SQLHelper.NullableString(.OrderId)
            oRow("Op") = .Op


            oRow("FchTrackingNotified") = SQLHelper.NullableFch(.FchTrackingNotified)
            oRow("FchDelivered") = SQLHelper.NullableFch(.FchDelivered)
            oRow("FchReviewRequest") = SQLHelper.NullableFch(.FchReviewRequest)
            oRow("UsrTrackingNotified") = SQLHelper.NullableBaseGuid(.UsrTrackingNotified)
            oRow("UsrDelivered") = SQLHelper.NullableBaseGuid(.UsrDelivered)
            oRow("UsrReviewRequest") = SQLHelper.NullableBaseGuid(.UsrReviewRequest)

            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function NextId(oEmp As DTOEmp, year As Integer, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 1
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Id) AS LastId ")
        sb.AppendLine("FROM ConsumerTicket ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND YEAR(Fch)=" & year & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId")) + 1
            End If
        End If
        Return retval
    End Function

    Shared Function Delete(oConsumerTicket As DTOConsumerTicket, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Load(oConsumerTicket)
            CcaLoader.Delete(oConsumerTicket.Cca, oTrans)
            If oConsumerTicket.Delivery IsNot Nothing Then DeliveryLoader.Delete(oConsumerTicket.Delivery, oTrans)
            If oConsumerTicket.PurchaseOrder IsNot Nothing Then PurchaseOrderLoader.Delete(oConsumerTicket.PurchaseOrder, oTrans)
            AddressesLoader.Delete(oConsumerTicket, oTrans)
            Delete(oConsumerTicket, oTrans)
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


    Shared Sub Delete(oConsumerTicket As DTOConsumerTicket, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ConsumerTicket WHERE Guid='" & oConsumerTicket.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class ConsumerTicketsLoader

    Shared Function All(year As Integer, Optional oEmp As DTOEmp = Nothing, Optional marketPlace As DTOMarketPlace = Nothing) As List(Of DTOConsumerTicket)
        Dim retval As New List(Of DTOConsumerTicket)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ConsumerTicket.*, VwAddress.*, MarketPlace.Nom AS MarketPlaceNom ")
        sb.AppendLine(", Pdc.Pdc, Spv.Id AS SpvId, Fra.Fra, Fra.Serie ")
        sb.AppendLine(", Alb.Alb, Alb.Fch AS AlbFch, Alb.Cod AS AlbCod, Alb.Eur AS AlbEur, Alb.FraGuid ")
        sb.AppendLine(", Cca.Cca AS CcaId, Cca.Fch as CcaFch ")
        sb.AppendLine(", VwDeliveryTrackingTrp.Tracking ")
        sb.AppendLine("FROM ConsumerTicket ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON ConsumerTicket.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN MarketPlace ON ConsumerTicket.MarketPlace=MarketPlace.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON ConsumerTicket.PurchaseOrder=Pdc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Spv ON ConsumerTicket.Spv=Spv.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON ConsumerTicket.Delivery=Alb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid=Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON ConsumerTicket.Cca=Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwDeliveryTrackingTrp ON ConsumerTicket.Delivery = VwDeliveryTrackingTrp.AlbGuid ")
        sb.AppendLine("WHERE YEAR(ConsumerTicket.Fch)=" & year & " ")
        If oEmp IsNot Nothing Then
            sb.AppendLine("AND ConsumerTicket.Emp=" & oEmp.Id & " ")
        End If
        If marketPlace IsNot Nothing Then
            sb.AppendLine("AND ConsumerTicket.MarketPlace='" & marketPlace.Guid.ToString() & "' ")
        End If
        sb.AppendLine("ORDER BY ConsumerTicket.Id DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOConsumerTicket(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Fch = oDrd("Fch")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognom1 = SQLHelper.GetStringFromDataReader(oDrd("CogNom1"))
                .Cognom2 = SQLHelper.GetStringFromDataReader(oDrd("CogNom2"))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Address.codi = DTOAddress.Codis.Fiscal
                .Op = oDrd("Op")

                .FchTrackingNotified = SQLHelper.GetFchFromDataReader(oDrd("FchTrackingNotified"))
                .FchDelivered = SQLHelper.GetFchFromDataReader(oDrd("FchDelivered"))
                .FchReviewRequest = SQLHelper.GetFchFromDataReader(oDrd("FchReviewRequest"))

                If Not IsDBNull(oDrd("MarketPlace")) Then
                    .MarketPlace = New DTOMarketPlace(oDrd("MarketPlace"))
                    .MarketPlace.Nom = SQLHelper.GetStringFromDataReader(oDrd("MarketPlaceNom"))
                End If

                If Not IsDBNull(oDrd("PurchaseOrder")) Then
                    .PurchaseOrder = New DTOPurchaseOrder(oDrd("PurchaseOrder"))
                    .PurchaseOrder.num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                End If
                If Not IsDBNull(oDrd("SpvId")) Then
                    .Spv = New DTOSpv(oDrd("Spv"))
                    .Spv.id = oDrd("SpvId")
                End If
                If Not IsDBNull(oDrd("Delivery")) Then
                    .Delivery = New DTODelivery(oDrd("Delivery"))
                    .Delivery.id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                    .Delivery.fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                    .Delivery.cod = SQLHelper.GetIntegerFromDataReader(oDrd("AlbCod"))
                    .Delivery.Import = SQLHelper.GetAmtFromDataReader(oDrd("AlbEur"))
                    .Delivery.Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                    If Not IsDBNull(oDrd("Fra")) Then
                        .Delivery.invoice = New DTOInvoice(oDrd("FraGuid"))
                        .Delivery.invoice.serie = oDrd("Serie")
                        .Delivery.invoice.num = oDrd("Fra")
                    End If
                End If
                If Not IsDBNull(oDrd("Cca")) Then
                    .Cca = New DTOCca(oDrd("Cca"))
                    With .Cca
                        .id = SQLHelper.GetIntegerFromDataReader(oDrd("CcaId"))
                        .fch = SQLHelper.GetFchFromDataReader(oDrd("CcaFch"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

