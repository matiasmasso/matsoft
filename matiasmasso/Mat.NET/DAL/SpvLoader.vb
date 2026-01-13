Public Class SpvLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSpv
        Dim retval As DTOSpv = Nothing
        Dim oSpv As New DTOSpv(oGuid)
        If Load(oSpv) Then
            retval = oSpv
        End If
        Return retval
    End Function

    Shared Function FromId(oEmp As DTOEmp, iYear As Integer, iId As Integer) As DTOSpv
        Dim retval As DTOSpv = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, FchAvis ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Yea=" & iYear & " ")
        sb.AppendLine("AND Id=" & iId & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSpv(oDrd("Guid"))
            With retval
                .Emp = oEmp
                .Id = iId
                .FchAvis = oDrd("FchAvis")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oSpv As DTOSpv) As Boolean
        If Not oSpv.IsLoaded And Not oSpv.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Spv.*, CliGral.FullNom AS CustomerFullNom, CliGral.RaoSocial, CliGral.LangId, VwAddress.*, VwSkuNom.*, VwTelDefault.TelNum ")
            sb.AppendLine(", SpvIn.Fch AS SpvInFch, SpvIn.Id AS SpvInId, SpvIn.Expedicio, SpvIn.Bultos AS SpvInBultos, SpvIn.Kg AS SpvInKg, SpvIn.Obs AS SpvInObs ")
            sb.AppendLine(", Email.Adr AS UsrRegisterAdr, Email.NickName AS UsrRegisterNickName ")
            sb.AppendLine(", Incidencies.Id AS IncidenciaId, Incidencies.Asin AS IncidenciaAsin, Incidencies.Fch AS IncidenciaFch ")
            sb.AppendLine(", Alb.Alb, Alb.Fch AS AlbFch, Alb.Facturable AS AlbFacturable ")
            sb.AppendLine("FROM Spv ")
            sb.AppendLine("LEFT OUTER JOIN VwTelDefault ON Spv.CliGuid = VwTelDefault.Contact ")
            sb.AppendLine("LEFT OUTER JOIN Email ON Spv.UsrRegisterGuid = Email.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SpvIn ON Spv.SpvIn = SpvIn.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Incidencies ON Spv.Incidencia = Incidencies.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON Spv.AlbGuid= Alb.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON Spv.Zip = VwAddress.ZipGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON (Spv.ProductGuid = VwSkuNom.SkuGuid OR VwSkuNom.CategoryGuid = Spv.ProductGuid OR VwSkuNom.BrandGuid = Spv.ProductGuid) ")
            sb.AppendLine("WHERE Spv.Guid='" & oSpv.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oLang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
                Dim oSpvIn As DTOSpvIn = Nothing
                If Not IsDBNull(oDrd("SpvIn")) Then
                    oSpvIn = New DTOSpvIn(oDrd("SpvIn"))
                    With oSpvIn
                        .id = SQLHelper.GetIntegerFromDataReader(oDrd("SpvInId"))
                        .fch = SQLHelper.GetFchFromDataReader(oDrd("SpvInFch"))
                        .expedicio = SQLHelper.GetStringFromDataReader(oDrd("Expedicio"))
                        .bultos = SQLHelper.GetIntegerFromDataReader(oDrd("SpvInBultos"))
                        .kg = SQLHelper.GetIntegerFromDataReader(oDrd("SpvInKg"))
                        .obs = SQLHelper.GetStringFromDataReader(oDrd("SpvInObs"))
                    End With
                End If
                With oSpv
                    .emp = New DTOEmp(oDrd("Emp"))
                    .id = CInt(oDrd("Id"))
                    Dim oCur As DTOCur = DTOCur.Factory(oDrd("Cur").ToString())
                    .fchAvis = oDrd("FCHAVIS")

                    If Not IsDBNull(oDrd("CliGuid")) Then
                        .customer = New DTOCustomer(oDrd("CliGuid"))
                        .customer.FullNom = oDrd("CustomerFullNom")
                        .customer.nom = IIf(oDrd("RaoSocial") = "", .customer.FullNom, oDrd("RaoSocial"))
                        .customer.lang = oLang
                        .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        If .nom = "" Then .nom = .customer.nom
                        If IsDBNull(oDrd("Tel")) Then
                            .customer.telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum")) 'el telefon per defecte del client
                        Else
                            .customer.telefon = SQLHelper.GetStringFromDataReader(oDrd("Tel")) 'el que ha escrit el client per aquesta incidencia
                        End If
                    End If

                    .address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .product = SQLHelper.GetProductFromDataReader(oDrd)
                    If TypeOf .product Is DTOProductSku Then
                        .product.Nom = DirectCast(.product, DTOProductSku).nomLlarg
                    ElseIf TypeOf .product Is DTOProductCategory Then
                        .product.Nom = String.Format("{0} {1}", DirectCast(.product, DTOProductCategory).brand.nom.Tradueix(oLang), .product.Nom.Tradueix(oLang))
                    End If
                    .serialNumber = SQLHelper.GetStringFromDataReader(oDrd("Serial"))
                    .ManufactureDate = SQLHelper.GetStringFromDataReader(oDrd("ManufactureDate"))
                    .obsClient = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .obsTecnic = SQLHelper.GetStringFromDataReader(oDrd("OBSTECNIC"))
                    .garantia = oDrd("GARANTIA")
                    .fchAvis = oDrd("FCHAVIS")
                    .fchRead = IIf(IsDBNull(oDrd("FCHREAD")), Date.MinValue, oDrd("FCHREAD"))
                    .solicitaGarantia = oDrd("SOLICITAGARANTIA")
                    .obsClient = SQLHelper.GetStringFromDataReader(oDrd("OBS"))
                    .labelEmailedTo = SQLHelper.GetStringFromDataReader(oDrd("LabelEmailedTo"))
                    .spvIn = oSpvIn

                    If Not IsDBNull(oDrd("Incidencia")) Then
                        .incidencia = New DTOIncidencia(DirectCast(oDrd("Incidencia"), Guid))
                        With .incidencia
                            .Num = SQLHelper.GetIntegerFromDataReader(oDrd("IncidenciaId"))
                            .Asin = SQLHelper.GetStringFromDataReader(oDrd("IncidenciaAsin"))
                            .Fch = SQLHelper.GetFchFromDataReader(oDrd("IncidenciaFch"))
                        End With
                    End If

                    .contacto = SQLHelper.GetStringFromDataReader(oDrd("CONTACTO"))
                    .sRef = SQLHelper.GetStringFromDataReader(oDrd("SREF"))
                    If Not IsDBNull(oDrd("UsrRegisterGuid")) Then
                        .usrRegister = New DTOUser(DirectCast(oDrd("UsrRegisterGuid"), Guid))
                        .usrRegister.emailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterAdr"))
                        .usrRegister.nom = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                        .usrRegister.nickName = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                    End If
                    If Not IsDBNull(oDrd("USRTECNICGUID")) Then
                        .usrTecnic = New DTOUser(DirectCast(oDrd("USRTECNICGUID"), Guid))
                    End If
                    .valJob = DTOAmt.Factory(oDrd("VALJOB"), oCur.tag, oDrd("VALJOB"))
                    .valMaterial = DTOAmt.Factory(oDrd("VALSPARES"), oCur.tag, oDrd("VALSPARES"))
                    .valEmbalatje = DTOAmt.Factory(oDrd("VALEMBALATJE"), oCur.tag, oDrd("VALEMBALATJE"))
                    .valPorts = DTOAmt.Factory(oDrd("VALPORTS"), oCur.tag, oDrd("VALPORTS"))

                    If Not IsDBNull(oDrd("AlbGuid")) Then
                        .delivery = New DTODelivery(oDrd("AlbGuid"))
                        With .delivery
                            .id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                            .fch = SQLHelper.GetFchFromDataReader(oDrd("AlbFch"))
                            .facturable = SQLHelper.GetBooleanFromDatareader(oDrd("AlbFacturable"))
                        End With
                    End If

                    'OutOfSpvIn (retirar de pendents de entrada; no arribará mai)
                    If Not IsDBNull(oDrd("UsrOutOfSpvInGuid")) Then
                        .usrOutOfSpvIn = New DTOUser(DirectCast(oDrd("UsrOutOfSpvInGuid"), Guid))
                    End If
                    .fchOutOfSpvIn = SQLHelper.GetFchFromDataReader(oDrd("FchOutOfSpvIn"))
                    .obsOutOfSpvIn = SQLHelper.GetStringFromDataReader(oDrd("ObsOutOfSpvIn"))

                    'OutOfSpvOut (retirar de pendents de sortir, no sortirá mai)
                    If Not IsDBNull(oDrd("UsrOutOfSpvOutGuid")) Then
                        .usrOutOfSpvOut = New DTOUser(DirectCast(oDrd("UsrOutOfSpvOutGuid"), Guid))
                    End If
                    .fchOutOfSpvOut = SQLHelper.GetFchFromDataReader(oDrd("FchOutOfSpvOut"))
                    .obsOutOfSpvOut = SQLHelper.GetStringFromDataReader(oDrd("ObsOutOfSpvOut"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSpv.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSpv As DTOSpv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSpv, oTrans)
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


    Shared Sub Update(oSpv As DTOSpv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("WHERE Guid='" & oSpv.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSpv.Guid
            oRow("Emp") = oSpv.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        If oSpv.Id = 0 Then oSpv.Id = LastId(oSpv, oTrans) + 1

        With oSpv

            oRow("Id") = .Id
            oRow("FchAvis") = .FchAvis
            oRow("Yea") = .FchAvis.Year

            oRow("CliGuid") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Adr") = SQLHelper.NullableString(.Address.Text)
            oRow("Zip") = SQLHelper.NullableBaseGuid(.address.Zip)

            If .product Is Nothing Then
                oRow("ProductGuid") = System.DBNull.Value
            Else
                oRow("ProductGuid") = SQLHelper.NullableBaseGuid(.product)
            End If
            oRow("Serial") = SQLHelper.NullableString(.serialNumber)
            oRow("ManufactureDate") = SQLHelper.NullableString(.ManufactureDate)
            oRow("Obs") = SQLHelper.NullableString(.ObsClient)
            oRow("OBSTECNIC") = SQLHelper.NullableString(.ObsTecnic)
            oRow("GARANTIA") = .Garantia
            oRow("FCHREAD") = SQLHelper.NullableFch(.FchRead)
            oRow("SOLICITAGARANTIA") = .SolicitaGarantia
            oRow("LabelEmailedTo") = SQLHelper.NullableString(.LabelEmailedTo)
            oRow("SpvIn") = SQLHelper.NullableBaseGuid(.SpvIn)
            oRow("CONTACTO") = SQLHelper.NullableString(.Contacto)
            oRow("SREF") = .sRef
            oRow("UsrRegisterGuid") = SQLHelper.NullableBaseGuid(.UsrRegister)
            oRow("UsrTecnicGuid") = SQLHelper.NullableBaseGuid(.UsrTecnic)
            If .ValJob Is Nothing Then
                oRow("ValJob") = 0
            Else
                oRow("ValJob") = SQLHelper.NullableAmt(.ValJob)
            End If
            If .ValMaterial Is Nothing Then
                oRow("ValSpares") = 0
            Else
                oRow("ValSpares") = SQLHelper.NullableAmt(.ValMaterial)
            End If
            If .ValEmbalatje Is Nothing Then
                oRow("ValEmbalatje") = 0
            Else
                oRow("ValEmbalatje") = SQLHelper.NullableAmt(.ValEmbalatje)
            End If
            If .ValPorts Is Nothing Then
                oRow("ValPorts") = 0
            Else
                oRow("ValPorts") = SQLHelper.NullableAmt(.ValPorts)
            End If
            If .ValJob IsNot Nothing AndAlso .ValJob.Cur IsNot Nothing Then
                oRow("Cur") = .ValJob.Cur.Tag
            Else
                oRow("Cur") = DTOApp.Current.Cur.Tag
            End If
            oRow("AlbGuid") = SQLHelper.NullableBaseGuid(.Delivery)
            oRow("Incidencia") = SQLHelper.NullableBaseGuid(.Incidencia)

            'OutOfSpvIn (retirar de pendents de entrada; no arribará mai)
            oRow("UsrOutOfSpvInGuid") = SQLHelper.NullableBaseGuid(.UsrOutOfSpvIn)
            oRow("FchOutOfSpvIn") = SQLHelper.NullableFch(.FchOutOfSpvIn)
            oRow("ObsOutOfSpvIn") = SQLHelper.NullableString(.ObsOutOfSpvIn)

            'OutOfSpvOut (retirar de pendents de sortir, no sortirá mai)
            oRow("UsrOutOfSpvOutGuid") = SQLHelper.NullableBaseGuid(.UsrOutOfSpvOut)
            oRow("FchOutOfSpvOut") = SQLHelper.NullableFch(.FchOutOfSpvOut)
            oRow("ObsOutOfSpvOut") = SQLHelper.NullableString(.ObsOutOfSpvOut)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastId(oSpv As DTOSpv, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Id AS LastId FROM Spv " _
        & "WHERE Emp=" & oSpv.Emp.Id & " AND Yea=@Yea " _
        & "ORDER BY Id DESC"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Yea", Year(oSpv.FchAvis))
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

    Shared Function Delete(oSpv As DTOSpv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSpv, oTrans)
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


    Shared Sub Delete(oSpv As DTOSpv, ByRef oTrans As SqlTransaction)
        RemoveFromIncidencias(oSpv, oTrans)
        DeleteSpv(oSpv, oTrans)
    End Sub

    Shared Sub RemoveFromIncidencias(oSpv As DTOSpv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Incidencies SET SpvGuid=NULL, CodiTancament=NULL, FchClose=NULL WHERE SpvGuid='" & oSpv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteSpv(oSpv As DTOSpv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Spv WHERE Guid='" & oSpv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SpvsLoader

    Shared Function Headers(oEmp As DTOEmp, Optional customer As DTOCustomer = Nothing, Optional onlyOpen As Boolean = False, Optional year As Integer = 0) As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Spv.Guid, Spv.Id, Spv.fchAvis ")
        sb.AppendLine(", Spv.FchRead, Spv.SpvIn, Spv.AlbGuid ")
        sb.AppendLine(", Spv.ProductGuid, VwProductNom.* ")
        sb.AppendLine(", Spv.UsrOutOfSpvInGuid, Spv.UsrOutOfSpvOutGuid ")
        sb.AppendLine(", Spv.CliGuid, CliGral.FullNom AS CustomerFullNom ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Spv.ProductGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON Spv.AlbGuid = Alb.Guid ")
        sb.AppendLine("WHERE Spv.Emp = " & oEmp.Id & " ")

        If onlyOpen Then
            sb.AppendLine("AND Spv.AlbGuid IS NULL AND Spv.UsrOutOfSpvOutGuid IS NULL ")
        End If
        If customer IsNot Nothing Then
            sb.AppendLine(" AND Spv.CliGuid = '" & customer.Guid.ToString & "' ")
        End If
        If year > 0 Then
            sb.AppendLine(" AND Spv.Yea=" & year & " ")
        End If
        sb.AppendLine("ORDER BY Spv.Yea DESC, Spv.Id DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpv(oDrd("Guid"))
            With item
                .emp = oEmp
                .id = oDrd("Id")
                .fchAvis = oDrd("FchAvis")
                .fchRead = SQLHelper.GetFchFromDataReader(oDrd("FchRead"))
                .customer = New DTOCustomer(oDrd("CliGuid"))
                .customer.FullNom = oDrd("CustomerFullNom")
                .product = SQLHelper.GetProductFromDataReader(oDrd)
                If Not IsDBNull(oDrd("SpvIn")) Then
                    .spvIn = New DTOSpvIn(oDrd("SpvIn"))
                End If
                If Not IsDBNull(oDrd("AlbGuid")) Then
                    .delivery = New DTODelivery(oDrd("AlbGuid"))
                End If
                .product = SQLHelper.GetProductFromDataReader(oDrd)
                If Not IsDBNull(oDrd("UsrOutOfSpvInGuid")) Then
                    .usrOutOfSpvIn = New DTOUser(oDrd("UsrOutOfSpvInGuid"))
                End If
                If Not IsDBNull(oDrd("UsrOutOfSpvOutGuid")) Then
                    .usrOutOfSpvOut = New DTOUser(oDrd("UsrOutOfSpvOutGuid"))
                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oSpvIn As DTOSpvIn) As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Spv.Guid, Spv.Id, Spv.FchAvis, Spv.CliGuid, Spv.ProductGuid ")
        sb.AppendLine(", CliGral.FullNom AS CustomerFullNom ")
        sb.AppendLine(", VwAddress.*, VwSkuNom.* ")
        sb.AppendLine(", Spv.UsrRegisterGuid, Email.Adr AS UsrRegisterAdr, Email.NickName AS UsrRegisterNickName ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON Spv.CliGuid = VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON (Spv.ProductGuid = VwSkuNom.SkuGuid OR VwSkuNom.CategoryGuid = Spv.ProductGuid OR VwSkuNom.BrandGuid = Spv.ProductGuid) ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Spv.UsrRegisterGuid = Email.Guid ")
        sb.AppendLine("WHERE SpvIn='" & oSpvIn.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpv(oDrd("Guid"))
            With item
                .Emp = oSpvIn.Emp
                .Id = oDrd("Id")
                .FchAvis = oDrd("FchAvis")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .customer.FullNom = oDrd("CustomerFullNom")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Product = SQLHelper.GetProductFromDataReader(oDrd)

                If Not IsDBNull(oDrd("UsrRegisterGuid")) Then
                    .UsrRegister = New DTOUser(DirectCast(oDrd("UsrRegisterGuid"), Guid))
                    .UsrRegister.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterAdr"))
                    .UsrRegister.Nom = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                    .UsrRegister.NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Spv.Guid, Spv.Id, Spv.FchAvis, Spv.FchRead, Spv.ProductGuid, Spv.SpvIn, Spv.FchOutOfSpvIn, Alb.Guid as AlbGuid, Spv.FchOutOfSpvOut ")
        sb.AppendLine(", Spv.Serial, Spv.ManufactureDate, Spv.Contacto, Spv.sRef, Spv.Obs, Spv.SolicitaGarantia, Spv.Garantia ")
        sb.AppendLine(", VwProductNom.FullNom AS ProductNom, SpvIn.Fch AS SpvInFch, Alb.Alb AS DeliveryAlb, Alb.Fch AS DeliveryFch ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN VwProductNom ON Spv.ProductGuid=VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SpvIn ON Spv.SpvIn = SpvIn.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON Spv.AlbGuid = Alb.Guid ")
        sb.AppendLine("WHERE Spv.CliGuid='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY FchAvis DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpv(oDrd("Guid"))
            With item
                .Emp = oCustomer.Emp
                .Id = oDrd("Id")
                .FchAvis = oDrd("FchAvis")
                .FchRead = oDrd("FchRead")
                .Customer = oCustomer
                .Product = New DTOProduct(oDrd("ProductGuid"))
                .Product.Nom = oDrd("ProductNom")
                .sRef = SQLHelper.GetStringFromDataReader(oDrd("sRef"))
                .Contacto = SQLHelper.GetStringFromDataReader(oDrd("Contacto"))
                .ObsClient = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .SolicitaGarantia = oDrd("SolicitaGarantia")
                .Garantia = oDrd("Garantia")
                If Not IsDBNull(oDrd("SpvIn")) Then
                    .SpvIn = New DTOSpvIn(oDrd("SpvIn"))
                    .SpvIn.Fch = oDrd("SpvInFch")
                End If
                If Not IsDBNull(oDrd("AlbGuid")) Then
                    .Delivery = New DTODelivery(oDrd("AlbGuid"))
                    .Delivery.Id = oDrd("DeliveryAlb")
                    .Delivery.Fch = oDrd("DeliveryFch")
                End If
                .FchOutOfSpvIn = SQLHelper.GetFchFromDataReader(oDrd("FchOutOfSpvIn"))
                .FchOutOfSpvOut = SQLHelper.GetFchFromDataReader(oDrd("FchOutOfSpvOut"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ArrivalPending(oEmp As DTOEmp) As List(Of DTOSpv)

        Dim retval As New List(Of DTOSpv)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Spv.Guid, Spv.Id, Spv.FchAvis, Spv.ProductGuid, Spv.CliGuid ")
        sb.AppendLine(", CliGral.FullNom AS CustomerFullNom ")
        sb.AppendLine(", Spv.Adr, VwZip.* ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine(", Spv.UsrRegisterGuid, Email.Adr AS UsrRegisterAdr, Email.NickName AS UsrRegisterNickName ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwZip ON Spv.Zip = VwZip.ZipGuid ")
        sb.AppendLine("INNER JOIN VwProductNom ON Spv.ProductGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Spv.UsrRegisterGuid = Email.Guid ")
        sb.AppendLine("WHERE Spv.SpvIn IS NULL ")
        sb.AppendLine("AND Spv.AlbGuid IS NULL ")
        sb.AppendLine("AND FchOutOfSpvIn IS NULL ")
        sb.AppendLine("AND FchOutOfSpvOut IS NULL ")
        sb.AppendLine("ORDER BY Spv.Yea DESC, Spv.Id DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpv(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Id = oDrd("Id")
                .FchAvis = oDrd("FchAvis")

                If Not IsDBNull(oDrd("CliGuid")) Then
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .customer.FullNom = oDrd("CustomerFullNom")
                    .customer.Nom = oDrd("CustomerFullNom")
                End If

                .Address = New DTOAddress
                With .Address
                    .Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                    .Zip = SQLHelper.GetZipFromDataReader(oDrd)
                End With
                .Product = SQLHelper.GetProductFromDataReader(oDrd)

                If Not IsDBNull(oDrd("UsrRegisterGuid")) Then
                    .UsrRegister = New DTOUser(DirectCast(oDrd("UsrRegisterGuid"), Guid))
                    .UsrRegister.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterAdr"))
                    .UsrRegister.Nom = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                    .UsrRegister.NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function NotRead(oEmp As DTOEmp, ByRef oSpvs As List(Of DTOSpv), exs As List(Of Exception)) As Boolean
        Dim DtFchRead As DateTime = DTO.GlobalVariables.Now()
        Dim retval As Boolean = SetFchRead(oEmp, DtFchRead, exs)
        If retval Then
            oSpvs = ReadInFch(oEmp, DtFchRead)
        End If
        Return retval
    End Function

    Shared Function ReadInFch(oEmp As DTOEmp, DtFchRead As Date) As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)

        Dim sFch As String = Format(DtFchRead, "yyyyMMdd HH:mm:ss") 'DtFchRead.ToString(System.Globalization.CultureInfo.InvariantCulture)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Spv.Guid, Spv.Id, Spv.FchAvis, Spv.FchRead, Spv.ProductGuid, Spv.SpvIn, Spv.FchOutOfSpvIn, Spv.FchOutOfSpvOut ")
        sb.AppendLine(", Spv.Serial, Spv.ManufactureDate, Spv.Contacto, Spv.sRef, Spv.Obs, Spv.SolicitaGarantia, Spv.Garantia ")
        sb.AppendLine(", Spv.CliGuid, CliGral.FullNom AS CustomerFullNom ")
        sb.AppendLine(", Spv.Incidencia, Incidencies.Id AS IncidenciaNum, Incidencies.Asin AS IncidenciaAsin ")
        sb.AppendLine(", Spv.ProductGuid, VwProductNom.BrandNom as BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine(", Spv.UsrRegisterGuid, Email.Adr AS UsrRegisterAdr, Email.NickName AS UsrRegisterNickName ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Spv.ProductGuid=VwProductNom.Guid ")
        'sb.AppendLine("LEFT OUTER JOIN Tpa ON Spv.ProductGuid=Tpa.Guid ")
        'sb.AppendLine("LEFT OUTER JOIN Stp ON Spv.ProductGuid=Stp.Guid ")
        'sb.AppendLine("LEFT OUTER JOIN Art ON Spv.ProductGuid=Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Incidencies ON Spv.Incidencia=Incidencies.Guid ")
        'sb.AppendLine("INNER JOIN VwAddress ON Spv.Zip = VwAddress.ZipGuid ")
        'sb.AppendLine("INNER JOIN VwSkuNom ON (Spv.ProductGuid = VwSkuNom.SkuGuid OR VwSkuNom.CategoryGuid = Spv.ProductGuid OR VwSkuNom.BrandGuid = Spv.ProductGuid) ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Spv.UsrRegisterGuid = Email.Guid ")
        sb.AppendLine("WHERE Spv.Emp = " & oEmp.Id & " AND Spv.FchRead='" & sFch & "' ")
        sb.AppendLine("ORDER BY Spv.FchAvis DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpv(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Id = oDrd("Id")
                .FchAvis = oDrd("FchAvis")
                .FchRead = oDrd("FchRead")

                If Not IsDBNull(oDrd("CliGuid")) Then
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .customer.FullNom = oDrd("CustomerFullNom")
                    .customer.Nom = oDrd("CustomerFullNom")
                End If

                .Address = SQLHelper.GetAddressFromDataReader(oDrd)

                If Not IsDBNull(oDrd("ProductGuid")) Then
                    If Not IsDBNull(oDrd("SkuNomLlarg")) Then
                        .product = New DTOProductSku(oDrd("ProductGuid"))
                        CType(.product, DTOProductSku).Nom.Esp = oDrd("SkuNomLlarg")
                    ElseIf Not IsDBNull(oDrd("CategoryNom")) Then
                        .product = New DTOProductCategory(oDrd("ProductGuid"))
                        CType(.product, DTOProductCategory).Nom.Esp = oDrd("CategoryNom")
                    ElseIf Not IsDBNull(oDrd("BrandNom")) Then
                        .Product = New DTOProductBrand(oDrd("ProductGuid"))
                        CType(.product, DTOProductBrand).Nom.Esp = oDrd("BrandNom")
                    End If
                End If

                If Not IsDBNull(oDrd("Incidencia")) Then
                    .incidencia = New DTOIncidencia(oDrd("Incidencia"))
                    .incidencia.Num = oDrd("IncidenciaNum")
                    .incidencia.Asin = oDrd("IncidenciaAsin")
                End If

                If Not IsDBNull(oDrd("UsrRegisterGuid")) Then
                    .UsrRegister = New DTOUser(DirectCast(oDrd("UsrRegisterGuid"), Guid))
                    .UsrRegister.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterAdr"))
                    .UsrRegister.Nom = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                    .UsrRegister.NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrRegisterNickname"))
                End If

                .sRef = SQLHelper.GetStringFromDataReader(oDrd("sRef"))
                .Contacto = SQLHelper.GetStringFromDataReader(oDrd("Contacto"))
                .ObsClient = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .SolicitaGarantia = oDrd("SolicitaGarantia")
                .Garantia = oDrd("Garantia")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SetFchRead(oEmp As DTOEmp, DtFchRead As Date, exs As List(Of Exception)) As Boolean
        Dim sFch As String = DtFchRead.ToString(System.Globalization.CultureInfo.InvariantCulture)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET Spv.FchRead ='" & sFch & "' ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN CliGral ON Spv.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Spv.FchRead IS NULL ")
        Dim SQL As String = sb.ToString
        Dim rc As Integer = SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
End Class
