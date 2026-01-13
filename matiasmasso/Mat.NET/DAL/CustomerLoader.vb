Public Class CustomerLoader
    Shared Function Find(oGuid As Guid) As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        Dim oCustomer As New DTOCustomer(oGuid)
        If Load(oCustomer) Then
            retval = oCustomer
        End If
        Return retval

    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim SQL As String = "SELECT Guid FROM CliClient WHERE Guid='" & oContact.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function


    Shared Function Load(ByRef oCustomer As DTOCustomer) As Boolean

        If Not oCustomer.IsLoaded Then
            Dim oPaymentTerms As New DTOPaymentTerms

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp, CliClient.* ")
            sb.AppendLine(",  CliGral.RaoSocial, CliGral.NomCom, CliGral.Rol, CliGral.LangId, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.GLN ")
            sb.AppendLine(", CliGral.ContactClass, ContactClass.DistributionChannel ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", VwTel.TelNum ")
            sb.AppendLine(", DeliveryPlatform.FullNom AS DeliveryPlatformNom ")
            sb.AppendLine(", Holding.Nom AS HoldingNom, CustomerCluster.Nom AS CustomerClusterNom ")
            sb.AppendLine(", PortsCondicions.Guid AS PortsCondicionsGuid, PortsCondicions.Nom AS PortsCondicionsNom, PortsCondicions.PdcMinVal AS PortsCondicionsPdcMinVal, PortsCondicions.Fee AS PortsCondicionsFee, PortsCondicions.Cod AS PortsCondicionsCod ")
            sb.AppendLine("FROM CliClient ")
            sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliClient.Guid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwTel ON CliClient.Guid = VwTel.Contact ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS DeliveryPlatform ON CliClient.DeliveryPlatform = DeliveryPlatform.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Holding ON CliClient.Holding = Holding.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CustomerCluster ON CliClient.CustomerCluster = CustomerCluster.Guid ")
            sb.AppendLine("LEFT OUTER JOIN PortsCondicions ON PortsCondicions.Guid = CliClient.PortsCondicions ")
            sb.AppendLine("WHERE CliClient.Guid='" & oCustomer.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then

                With oCustomer
                    If .Emp Is Nothing Then
                        If Not IsDBNull(oDrd("Emp")) Then
                            .Emp = New DTOEmp(oDrd("Emp"))
                        End If
                    End If
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                        If Not IsDBNull(oDrd("DistributionChannel")) Then
                            .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        End If
                    End If
                    .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    '.Nif = SQLHelper.GetStringFromDataReader(oDrd("Nif"))
                    '.Nif2 = SQLHelper.GetStringFromDataReader(oDrd("NIF2"))
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                    .Rol = New DTORol(oDrd("Rol"))
                    .Lang = DTOLang.Factory(oDrd("LangId").ToString())
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
                    .Iva = oDrd("IVA")
                    .Req = oDrd("Req")
                    .PortsCod = oDrd("Ports")
                    If Not IsDBNull(oDrd("PortsCondicionsGuid")) Then
                        .PortsCondicions = New DTOPortsCondicio(oDrd("PortsCondicionsGuid"))
                        With .PortsCondicions
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("PortsCondicionsNom"))
                            .PdcMinVal = SQLHelper.GetAmtFromDataReader(oDrd("PortsCondicionsPdcMinVal"))
                            .Fee = SQLHelper.GetAmtFromDataReader(oDrd("PortsCondicionsFee"))
                            .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("PortsCondicionsCod"))
                        End With
                    End If
                    .FpgIndependent = oDrd("FPGINDEPENDENT")
                    .CashCod = oDrd("CashCod")
                    .WebAtlasPriority = oDrd("WebAtlasPriority") = 1
                    .NoWeb = oDrd("NoWeb")
                    .NoRep = oDrd("NoRep")
                    .OrdersToCentral = oDrd("OrdersToCentral")
                    .NoIncentius = oDrd("NoIncentius")
                    .NoRaffles = oDrd("NoRaffles")
                    .AlbValorat = oDrd("AlbVal")
                    .HorarioEntregas = SQLHelper.GetStringFromDataReader(oDrd("HorarioEntregas"))
                    .WarnAlbs = SQLHelper.GetStringFromDataReader(oDrd("Warning"))
                    .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .FraPrintMode = oDrd("FraPrintMode")
                    .MostrarEANenFactura = oDrd("MostrarEANenFactura")
                    '.Cluster = SQLHelper.GetIntegerFromDataReader(oDrd("Cluster"))
                    .PaymentTerms = New DTOPaymentTerms
                    With .PaymentTerms
                        If IsDBNull(oDrd("Cfp")) Then
                            oCustomer.CashCod = DTOCustomer.CashCodes.transferenciaPrevia
                        Else
                            .Cod = oDrd("Cfp")
                            .Months = oDrd("Mes")
                        End If
                        .PaymentDays = GetPaymentDaysFromDataReader(oDrd("PaymentDays"))
                        .Vacaciones = DecodedVacacions(oDrd("Vacaciones"))
                        Dim oIbans = IbansLoader.All(oCustomer.Emp, oCustomer, -1, OnlyVigent:=True, oStatus:=DTOIban.StatusEnum.all, oCod:=DTOIban.Cods.client)
                        If oIbans.Count > 0 Then
                            .Iban = oIbans.First
                        End If
                    End With
                    If Not IsDBNull(oDrd("Holding")) Then
                        .Holding = New DTOHolding(oDrd("Holding"))
                        .Holding.Nom = SQLHelper.GetStringFromDataReader(oDrd("HoldingNom"))
                    End If
                    If Not IsDBNull(oDrd("CustomerCluster")) Then
                        .CustomerCluster = New DTOCustomerCluster(oDrd("CustomerCluster"))
                        .CustomerCluster.Nom = SQLHelper.GetStringFromDataReader(oDrd("CustomerClusterNom"))
                    End If

                    If Not IsDBNull(oDrd("DeliveryPlatform")) Then
                        .DeliveryPlatform = New DTOCustomerPlatform(oDrd("DeliveryPlatform"))
                        .DeliveryPlatform.FullNom = SQLHelper.GetStringFromDataReader(oDrd("DeliveryPlatformNom"))
                    End If

                    .ExportCod = oDrd("ExportCod")
                    .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("IncoTerm"))

                    If Not IsDBNull(oDrd("CcxGuid")) Then
                        .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                    End If
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                End With
            End If
            oDrd.Close()

            If oCustomer.Ccx IsNot Nothing Then
                Load(oCustomer.Ccx)
            End If

            oCustomer.IsLoaded = True
        End If

        Return oCustomer.IsLoaded
    End Function

    Shared Function DistributionChannel(oCustomer As DTOCustomer) As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT ContactClass.DistributionChannel ")
        sb.AppendLine("From CliGral ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON CliGral.Guid = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "'")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTODistributionChannel(oDrd("DistributionChannel"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetPaymentDaysFromDataReader(sEncodedPaymentDays As Object) As List(Of Integer)
        Dim retval As New List(Of Integer)
        If Not IsDBNull(sEncodedPaymentDays) Then
            For i As Integer = 1 To 31
                Dim tmp As String = sEncodedPaymentDays.Substring(i - 1, 1)
                If tmp = "1" Then retval.Add(i)
            Next
        End If
        Return retval
    End Function

    Shared Function NullablePaymentDays(oPaymentDays As List(Of Integer)) As Object
        Dim retval As Object = System.DBNull.Value
        If oPaymentDays IsNot Nothing Then
            Dim src As New String("0", 31)
            For Each i As Integer In oPaymentDays
                src = src.Remove(i - 1, 1).Insert(i - 1, "1")
            Next
            retval = src
        End If
        Return retval
    End Function

    Shared Function Update(oCustomer As DTOCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCustomer, oTrans)

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

    Shared Sub Update(oCustomer As DTOCustomer, ByRef oTrans As SqlTransaction)
        'ContactLoader.Update(oCustomer, oTrans)
        UpdateHeader(oCustomer, oTrans)
        If oCustomer.TarifaDtos IsNot Nothing Then
            UpdateTarifaDtos(oCustomer, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oCustomer As DTOCustomer, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliClient ")
        sb.AppendLine("WHERE Guid='" & oCustomer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCustomer.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCustomer
            oRow("IVA") = .Iva
            oRow("Req") = .Req
            oRow("Ports") = .PortsCod
            oRow("PortsCondicions") = SQLHelper.NullableBaseGuid(.PortsCondicions)
            oRow("CashCod") = .CashCod
            oRow("OrdersToCentral") = .OrdersToCentral
            oRow("WebAtlasPriority") = IIf(.Webatlaspriority, 1, 0)
            oRow("NoWeb") = .NoWeb
            oRow("NoRep") = .NoRep
            oRow("NoIncentius") = .NoIncentius
            oRow("NoRaffles") = .NoRaffles
            oRow("AlbVal") = .AlbValorat
            oRow("Warning") = SQLHelper.NullableString(.WarnAlbs)
            oRow("HorarioEntregas") = SQLHelper.NullableString(.HorarioEntregas)
            oRow("SuProveedorNum") = .SuProveedorNum
            oRow("CcxGuid") = SQLHelper.NullableBaseGuid(.Ccx)
            oRow("Ref") = .Ref
            oRow("FraPrintMode") = .FraPrintMode
            oRow("MostrarEANenFactura") = .MostrarEANenFactura
            oRow("DeliveryPlatform") = SQLHelper.NullableBaseGuid(.DeliveryPlatform)
            oRow("FpgIndependent") = .FpgIndependent
            'oRow("Cluster") = .Cluster
            oRow("Holding") = SQLHelper.NullableBaseGuid(.Holding)
            oRow("CustomerCluster") = SQLHelper.NullableBaseGuid(.CustomerCluster)
            If .PaymentTerms IsNot Nothing Then
                With .PaymentTerms
                    oRow("Cfp") = .Cod
                    oRow("Mes") = .Months
                    oRow("PaymentDays") = NullablePaymentDays(.PaymentDays)
                    oRow("Vacaciones") = EncodedVacacions(.Vacaciones)
                End With
            End If
            oRow("ExportCod") = .ExportCod
            oRow("Incoterm") = SQLHelper.NullableIncoterm(.Incoterm)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateTarifaDtos(oCustomer As DTOCustomer, ByRef oTrans As SqlTransaction)
        DeleteTarifaDtos(oCustomer, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CustomerDto ")
        sb.AppendLine("WHERE Customer='" & oCustomer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOCustomerTarifaDto In oCustomer.TarifaDtos
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Customer") = oCustomer.Guid
            oRow("Product") = SQLHelper.NullableBaseGuid(item.Product)
            oRow("Dto") = item.Dto
            oRow("Fch") = item.Fch
            oRow("Obs") = SQLHelper.NullableString(item.Obs)
        Next

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCustomer As DTOCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomer, oTrans)
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


    Shared Sub Delete(oCustomer As DTOContact, ByRef oTrans As SqlTransaction)
        DeleteTarifaDtos(oCustomer, oTrans)
        DeleteHeader(oCustomer, oTrans)
    End Sub

    Shared Sub DeleteHeader(oCustomer As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliClient WHERE Guid='" & oCustomer.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteTarifaDtos(oCustomer As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CustomerDto WHERE Customer='" & oCustomer.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function FromGln(ByRef oGln As DTOEan) As DTOCustomer
        Dim retval As DTOCustomer = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, VwCcxOrMe.Ccx, CliClient.SuProveedorNum ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON CliGral.Guid = VwCcxOrMe.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliClient.Guid = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE CliGral.Gln='" & oGln.Value & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOCustomer(oDrd("Guid"))
            retval.Ccx = New DTOCustomer(oDrd("Ccx"))
            retval.SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
            retval.GLN = oGln
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function IsGroup(oContact As DTOContact) As Boolean
        Dim retval As Boolean
        Dim SQL As String = "SELECT TOP 1 Guid FROM CliClient WHERE CcxGuid='" & oContact.Guid.ToString & "' "
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        retval = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function EFrasEnabled(oContact As DTOContact) As Boolean
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        Dim SQL As String = "SELECT EMAIL.Adr " _
        & "FROM            SSCEMAIL INNER JOIN " _
        & "EMAIL ON SSCEMAIL.Email = EMAIL.guid INNER JOIN " _
        & "EMAIL_CLIS ON EMAIL.Guid = EMAIL_CLIS.emailGuid INNER JOIN " _
        & "VwCcxOrMe ON EMAIL_CLIS.ContactGuid = VwCcxOrMe.Ccx " _
        & "WHERE (SSCEMAIL.SscGuid = '" & oSubscription.Guid.ToString & "') AND " _
        & "(VwCcxOrMe.Guid =@Guid) AND " _
        & "Email.BadMailGuid IS NULL AND " _
        & "(EMAIL.Obsoleto = 0)"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(sql, "@Guid", oContact.Guid.ToString())
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function Centres(oCcx As DTOCustomer) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid, CliGral.FullNom, CliClient.Ref, CliGral.Gln, CliGral.Obsoleto ")
        sb.AppendLine("FROM CliClient ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE (CliClient.CcxGuid ='" & oCcx.Guid.ToString & "' OR CliClient.Guid ='" & oCcx.Guid.ToString & "') ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, (CASE WHEN CliClient.CcxGuid IS NULL THEN 0 ELSE 1 END), CliClient.Ref, CliGral.FullNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("Guid"))
            With item
                .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DecodedVacacions(oSrc As Object) As List(Of DTOVacacion)
        Dim retval As New List(Of DTOVacacion)
        If oSrc IsNot Nothing Then
            If Not IsDBNull(oSrc) Then
                Dim Srcs As String() = oSrc.ToString.Split(vbCrLf)
                For Each src As String In Srcs
                    Dim segments As String() = src.Split(",")
                    If segments.Count = 6 Then
                        Dim item As New DTOVacacion
                        With item
                            .MonthDayFrom = New DTOMonthDay(segments(DTOVacacion.Segments.FromMes), segments(DTOVacacion.Segments.FromDia))
                            .MonthDayTo = New DTOMonthDay(segments(DTOVacacion.Segments.UntilMes), segments(DTOVacacion.Segments.UntilDia))
                            .MonthDayResult = New DTOMonthDay(segments(DTOVacacion.Segments.ForwardMes), segments(DTOVacacion.Segments.ForwardDia))
                        End With
                        retval.Add(item)
                    End If
                Next
            End If
        End If
        Return retval
    End Function

    Shared Function EncodedVacacions(oVacacions As List(Of DTOVacacion)) As Object
        Dim retval As Object = System.DBNull.Value
        If oVacacions IsNot Nothing Then
            If oVacacions.Count > 0 Then
                Dim sb As New Text.StringBuilder
                For Each item As DTOVacacion In oVacacions
                    Dim line As String = String.Format("{0},{1},{2},{3},{4},{5}", item.MonthDayFrom.Month, item.MonthDayFrom.Day, item.MonthDayTo.Month, item.MonthDayTo.Day, item.MonthDayResult.Month, item.MonthDayResult.Day)
                    sb.AppendLine(line)
                Next
                retval = sb.ToString
            End If
        End If
        Return retval
    End Function

    Shared Function EFrasEnabled(oCustomer As DTOCustomer) As Boolean
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT SscEmail.Email ")
        sb.AppendLine("FROM SscEmail ")
        sb.AppendLine("INNER JOIN Email ON SscEmail.Email = Email.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid = Email_Clis.EmailGuid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Email_Clis.ContactGuid = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE SscEmail.SscGuid = '" & oSubscription.Guid.ToString() & "' ")
        sb.AppendLine("AND VwCcxOrMe.Guid ='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        sb.AppendLine("AND Email.Obsoleto = 0 ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class CustomersLoader

    Shared Function Children(ccx As DTOCustomer) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT CliClient.Guid, CliClient.Ref, CliGral.FullNom, CliGral.Obsoleto ")
        sb.Append("FROM CliClient ")
        sb.Append("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.Append("WHERE CliClient.CcxGuid = '" & ccx.Guid.ToString & "' ")
        sb.Append("ORDER BY CliClient.Ref, CliGral.FullNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("Guid"))
            With item
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromUser(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT EMAIL_CLIS.ContactGuid, CliGral.RaoSocial, CliGral.NomCom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.Append(", CliAdr.Adr, CliAdr.Zip, Zip.ZipCod, Zip.Location ")
        sb.Append(", Location.Nom as LocationNom, Location.Zona, Zona.Nom as ZonaNom, Zona.Country ")
        sb.Append(", Country.ISO, Country.Nom_ESP AS CountryNom ")
        sb.Append(", CliClient.Ref, CliClient.NoRep, CliClient.NoIncentius, CliClient.NoRaffles, CliClient.OrdersToCentral, CliClient.CcxGuid, CliClient.Dto ")
        sb.Append("FROM EMAIL_CLIS ")
        sb.Append("INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid ")
        sb.Append("INNER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
        sb.Append("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.Append("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.Append("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.Append("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.Append("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.Append("WHERE EMAIL_CLIS.EmailGuid='" & oUser.Guid.ToString & "' ")
        sb.Append("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
        sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
        sb.AppendLine(") ")
        sb.Append("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oCountry As New DTOCountry(oDrd("Country"))
            With oCountry
                .ISO = oDrd("ISO")
                .LangNom.Esp = oDrd("CountryNom")
            End With
            Dim oZona As New DTOZona(oDrd("Zona"))
            With oZona
                .Country = oCountry
                .Nom = oDrd("ZonaNom")
            End With
            Dim oLocation As New DTOLocation(oDrd("Location"))
            With oLocation
                .Zona = oZona
                .Nom = oDrd("LocationNom")
            End With
            Dim oZip As New DTOZip(oDrd("Zip"))
            With oZip
                .Location = oLocation
                .ZipCod = oDrd("ZipCod")
            End With
            Dim oAddress As New DTOAddress
            With oAddress
                .Text = oDrd("Adr")
                .Zip = oZip
            End With

            Dim oCustomer = New DTOCustomer(oDrd("ContactGuid"))
            With oCustomer
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .Address = oAddress
                .NoRep = oDrd("NoRep")
                .OrdersToCentral = oDrd("OrdersToCentral")
                .NoIncentius = oDrd("NoIncentius")
                .NoRaffles = oDrd("NoRaffles")
                If Not IsDBNull(oDrd("CcxGuid")) Then
                    .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                End If
            End With

            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function RaonsSocialsWithInvoices(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NIF, CliGral.NifCod, CliGral.NIF2, CliGral.Nif2Cod ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN Fra ON Fra.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid=@Guid AND CliGral.Guid NOT IN (SELECT Guid FROM CliClient WHERE CcxGuid IS NOT NULL) ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, CliGral.NIF, CliGral.NifCod, CliGral.NIF2, CliGral.Nif2Cod ")
        sb.AppendLine("ORDER BY MAX(Fra.Fch) DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString())
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("Guid"))
            With item
                .Nom = oDrd("RaoSocial")
                '.Nif = oDrd("Nif")
                '.Nif2 = SQLHelper.GetStringFromDataReader(oDrd("Nif2"))
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

