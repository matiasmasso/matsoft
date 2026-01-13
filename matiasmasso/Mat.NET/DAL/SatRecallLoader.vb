Public Class SatRecallLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSatRecall
        Dim retval As DTOSatRecall = Nothing
        Dim oSatRecall As New DTOSatRecall(oGuid)
        If Load(oSatRecall) Then
            retval = oSatRecall
        End If
        Return retval
    End Function

    Shared Function fromIncidencia(oIncidencia As DTOIncidencia) As DTOSatRecall
        Dim retval As DTOSatRecall = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SatRecall.Guid ")
        sb.AppendLine("FROM SatRecall ")
        sb.AppendLine("WHERE Incidencia = '" & oIncidencia.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSatRecall(oDrd("Guid"))
            retval.Incidencia = oIncidencia
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oSatRecall As DTOSatRecall) As Boolean
        If Not oSatRecall.IsLoaded And Not oSatRecall.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SatRecall.Incidencia, SatRecall.Defect, SatRecall.PickupFrom, SatRecall.FchCustomer, SatRecall.FchManufacturer, SatRecall.PickupRef, SatRecall.CreditNum, SatRecall.CreditFch, SatRecall.Adr, SatRecall.Zip, SatRecall.Obs ")
            sb.AppendLine(", SatRecall.ContactPerson, SatRecall.Tel ")
            sb.AppendLine(", SatRecall.Mode, SatRecall.ReturnRef, SatRecall.ReturnFch, SatRecall.Carrec ")
            sb.AppendLine(", Incidencies.Id, Incidencies.Asin, Incidencies.Fch, Incidencies.ContactGuid, Incidencies.ProductGuid, Incidencies.Email, Incidencies.SerialNumber ")
            sb.AppendLine(", CliGral.FullNom, CliGral.LangId ")
            sb.AppendLine(", CliGral.RaoSocial, CliGral.NomCom ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", ART.ref AS SkuRefProveidor, ART.refprv AS SkuNomProveidor ")
            sb.AppendLine(", VwZip.* ")
            sb.AppendLine("FROM SatRecall ")
            sb.AppendLine("INNER JOIN Incidencies ON SatRecall.Incidencia = Incidencies.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Art ON Incidencies.ProductGuid = Art.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwZip ON SatRecall.Zip = VwZip.ZipGuid ")
            sb.AppendLine("WHERE SatRecall.Guid='" & oSatRecall.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oCustomer As DTOCustomer = Nothing
                If Not IsDBNull(oDrd("ContactGuid")) Then
                    oCustomer = New DTOCustomer(oDrd("ContactGuid"))
                    oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    oCustomer.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    oCustomer.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                    oCustomer.Lang = DTOLang.Factory(oDrd("LangId"))
                End If
                Dim oIncidencia As New DTOIncidencia(oDrd("Incidencia"))
                With oIncidencia
                    .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Id"))
                    .Asin = SQLHelper.GetStringFromDataReader(oDrd("Asin"))
                    .Fch = oDrd("Fch")
                    .Customer = oCustomer
                    .EmailAdr = SQLHelper.GetStringFromDataReader(oDrd("Email"))
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    .Product.Nom = DirectCast(.Product, DTOProductSku).NomLlarg
                    If Not IsDBNull(oDrd("SkuRefProveidor")) Then
                        CType(.Product, DTOProductSku).RefProveidor = oDrd("SkuRefProveidor")
                    End If
                    If Not IsDBNull(oDrd("SkuNomProveidor")) Then
                        CType(.Product, DTOProductSku).NomProveidor = oDrd("SkuNomProveidor")
                    End If
                    .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
                End With
                With oSatRecall
                    .Incidencia = oIncidencia
                    .Defect = SQLHelper.GetStringFromDataReader(oDrd("Defect"))
                    .PickupFrom = SQLHelper.GetIntegerFromDataReader(oDrd("Pickupfrom"))
                    .FchCustomer = SQLHelper.GetFchFromDataReader(oDrd("FchCustomer"))
                    .FchManufacturer = SQLHelper.GetFchFromDataReader(oDrd("FchManufacturer"))
                    .PickupRef = SQLHelper.GetStringFromDataReader(oDrd("PickupRef"))
                    .Mode = oDrd("Mode")
                    .ReturnRef = SQLHelper.GetStringFromDataReader(oDrd("ReturnRef"))
                    .ReturnFch = SQLHelper.GetFchFromDataReader(oDrd("ReturnFch"))
                    .Carrec = SQLHelper.GetStringFromDataReader(oDrd("Carrec"))
                    .CreditNum = SQLHelper.GetStringFromDataReader(oDrd("CreditNum"))
                    .CreditFch = SQLHelper.GetFchFromDataReader(oDrd("CreditFch"))
                    If Not IsDBNull(oDrd("Zip")) Then
                        .Address = New DTOAddress
                        .Address.Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .Address.Zip = SQLHelper.GetZipFromDataReader(oDrd)
                    End If
                    .ContactPerson = SQLHelper.GetStringFromDataReader(oDrd("ContactPerson"))
                    .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSatRecall.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSatRecall As DTOSatRecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSatRecall, oTrans)
            oTrans.Commit()
            oSatRecall.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oSatRecall As DTOSatRecall, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SatRecall ")
        sb.AppendLine("WHERE Guid='" & oSatRecall.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSatRecall.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSatRecall
            oRow("Incidencia") = SQLHelper.NullableBaseGuid(.Incidencia)
            oRow("Defect") = SQLHelper.NullableString(.Defect)
            oRow("Pickupfrom") = SQLHelper.NullableInt(.PickupFrom)
            oRow("FchCustomer") = SQLHelper.NullableFch(.FchCustomer)
            oRow("FchManufacturer") = SQLHelper.NullableFch(.FchManufacturer)
            oRow("PickupRef") = SQLHelper.NullableString(.PickupRef)
            oRow("Mode") = .Mode
            oRow("ReturnRef") = SQLHelper.NullableString(.ReturnRef)
            oRow("ReturnFch") = SQLHelper.NullableFch(.ReturnFch)
            oRow("Carrec") = .Carrec
            oRow("CreditNum") = SQLHelper.NullableString(.CreditNum)
            oRow("CreditFch") = SQLHelper.NullableFch(.CreditFch)
            oRow("ContactPerson") = SQLHelper.NullableString(.ContactPerson)
            oRow("Tel") = SQLHelper.NullableString(.Tel)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            If .Address IsNot Nothing Then
                oRow("Adr") = .Address.Text
                oRow("Zip") = SQLHelper.NullableBaseGuid(.Address.Zip)
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSatRecall As DTOSatRecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSatRecall, oTrans)
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


    Shared Sub Delete(oSatRecall As DTOSatRecall, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SatRecall WHERE Guid='" & oSatRecall.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class SatRecallsLoader

    Shared Function All(Optional oEmp As DTOEmp = Nothing, Optional mode As DTOSatRecall.Modes = DTOSatRecall.Modes.PerAbonar) As List(Of DTO.Models.SatRecallModel)
        Dim retval As New List(Of DTO.Models.SatRecallModel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SatRecall.Guid, SatRecall.Incidencia, SatRecall.PickupFrom, SatRecall.FchCustomer, SatRecall.FchManufacturer, SatRecall.PickupRef, SatRecall.CreditNum, SatRecall.CreditFch ")
        'sb.AppendLine("SELECT SatRecall.Guid, SatRecall.Incidencia, SatRecall.Defect, SatRecall.PickupFrom, SatRecall.FchCustomer, SatRecall.FchManufacturer, SatRecall.PickupRef, SatRecall.CreditNum, SatRecall.CreditFch ")
        sb.AppendLine(", SatRecall.ContactPerson, SatRecall.Tel ")
        'sb.AppendLine(", SatRecall.ContactPerson, SatRecall.Tel, SatRecall.Mode ")
        sb.AppendLine(", Incidencies.Id AS IncidenciaId, Incidencies.Asin AS IncidenciaAsin, Incidencies.ContactGuid, Incidencies.ProductGuid, Incidencies.SerialNumber ")
        'sb.AppendLine(", Incidencies.Id, Incidencies.ContactGuid, Incidencies.ProductGuid, Incidencies.Email, Incidencies.SerialNumber ")
        sb.AppendLine(", CliGral.FullNom ")
        'sb.AppendLine(", CliGral.FullNom, CliGral.LangId ")
        sb.AppendLine(", VwProductNom.FullNom AS ProductFullNom, VwProductNom.Cod AS ProductSourceCod ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine("FROM SatRecall ")
        sb.AppendLine("INNER JOIN Incidencies ON SatRecall.Incidencia = Incidencies.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Incidencies.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Incidencies.ProductGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Incidencies.ProductGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE SatRecall.Mode = " & mode & " ")
        sb.AppendLine("ORDER BY Incidencies.Id DESC ")


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTO.Models.SatRecallModel()
            Dim oCustomer As DTO.Models.SatRecallModel.CustomerModel = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oCustomer = New DTO.Models.SatRecallModel.CustomerModel()
                oCustomer.Guid = oDrd("ContactGuid")
                oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                'oCustomer.Lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
            End If
            Dim oProduct As New DTO.Models.SatRecallModel.ProductModel()
            With oProduct
                .Guid = oDrd("ProductGuid")
                .SourceCod = 0
                .Nom = New DTO.Models.SatRecallModel.LangText
                If IsDBNull(oDrd("SkuGuid")) Then
                    .Nom.Esp = SQLHelper.GetStringFromDataReader(oDrd("ProductFullNom"))
                Else
                    Dim ref As String = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                    Dim prvNom As String = SQLHelper.GetStringFromDataReader(oDrd("SkuPrvNom"))
                    If String.IsNullOrEmpty(ref) And String.IsNullOrEmpty(prvNom) Then
                        .Nom.Esp = SQLHelper.GetStringFromDataReader(oDrd("ProductFullNom"))
                    Else
                        If Not String.IsNullOrEmpty(ref) And Not String.IsNullOrEmpty(prvNom) Then ref += " "
                        .Nom.Esp = ref & prvNom
                    End If

                End If
            End With
            Dim oIncidencia As New DTO.Models.SatRecallModel.IncidenciaModel()
            With oIncidencia
                .Guid = oDrd("Incidencia")
                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("IncidenciaId"))
                .Asin = SQLHelper.GetStringFromDataReader(oDrd("IncidenciaAsin"))
                .Product = oProduct
                .Customer = oCustomer
                '.EmailAdr = SQLHelper.GetStringFromDataReader(oDrd("Email"))
                .SerialNumber = SQLHelper.GetStringFromDataReader(oDrd("SerialNumber"))
            End With
            With item
                .Guid = oDrd("Guid")
                .Incidencia = oIncidencia
                '.Mode = oDrd("Mode")
                '.Defect = SQLHelper.GetStringFromDataReader(oDrd("Defect"))
                .PickupFrom = SQLHelper.GetIntegerFromDataReader(oDrd("Pickupfrom"))
                .FchCustomer = SQLHelper.GetFchFromDataReader(oDrd("FchCustomer"))
                .FchManufacturer = SQLHelper.GetFchFromDataReader(oDrd("FchManufacturer"))
                .PickupRef = SQLHelper.GetStringFromDataReader(oDrd("PickupRef"))
                .CreditNum = SQLHelper.GetStringFromDataReader(oDrd("CreditNum"))
                .CreditFch = SQLHelper.GetFchFromDataReader(oDrd("CreditFch"))
                '.ContactPerson = SQLHelper.GetStringFromDataReader(oDrd("ContactPerson"))
                '.Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
