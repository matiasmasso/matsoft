Public Class CustomerTarifaDtoLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCustomerTarifaDto
        Dim retval As DTOCustomerTarifaDto = Nothing
        Dim oCustomerDto As New DTOCustomerTarifaDto(oGuid)
        If Load(oCustomerDto) Then
            retval = oCustomerDto
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCustomerDto As DTOCustomerTarifaDto) As Boolean
        If Not oCustomerDto.IsLoaded And Not oCustomerDto.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT X.Guid, X.Src, X.SrcGuid, X.SrcNom, X.Fch, X.Dto, X.Product, X.Obs ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine("FROM ( ")
            sb.AppendLine("SELECT CustomerDto.Guid, " & CInt(DTOCustomerTarifaDto.Srcs.client) & " AS Src, CustomerDto.Customer AS SrcGuid, CliGral.FullNom AS SrcNom, CustomerDto.Fch, CustomerDto.Dto, CustomerDto.Product, CAST(CustomerDto.Obs AS VARCHAR) AS Obs ")
            sb.AppendLine("FROM CustomerDto ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON CustomerDto.Customer=CliGral.Guid ")
            sb.AppendLine("UNION ")
            sb.AppendLine("SELECT ChannelDto.Guid, " & CInt(DTOCustomerTarifaDto.Srcs.Canal) & ", ChannelDto.Channel, DistributionChannel.NomEsp, ChannelDto.Fch, ChannelDto.Dto, ChannelDto.Product, CAST(ChannelDto.Obs AS VARCHAR) AS Obs ")
            sb.AppendLine("FROM ChannelDto ")
            sb.AppendLine("LEFT OUTER JOIN DistributionChannel ON ChannelDto.Channel=DistributionChannel.Guid ")
            sb.AppendLine(") X ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON X.Product=VwProductNom.Guid ")
            sb.AppendLine("WHERE X.Guid='" & oCustomerDto.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCustomerDto
                    .Src = oDrd("Src")
                    Select Case .Src
                        Case DTOCustomerTarifaDto.Srcs.Client
                            Dim oCustomer As New DTOCustomer(oDrd("SrcGuid"))
                            oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("SrcNom"))
                            .CustomerOrChannel = oCustomer
                        Case DTOCustomerTarifaDto.Srcs.Canal
                            Dim oChannel As New DTODistributionChannel(oDrd("SrcGuid"))
                            oChannel.LangText.Esp = SQLHelper.GetStringFromDataReader(oDrd("SrcNom"))
                            .CustomerOrChannel = oChannel
                    End Select
                    .Fch = oDrd("Fch")
                    .Dto = oDrd("Dto")
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCustomerDto.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCustomerDto As DTOCustomerTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCustomerDto, oTrans)
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


    Shared Sub Update(oCustomerDto As DTOCustomerTarifaDto, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")

        If oCustomerDto.Src = DTOCustomerTarifaDto.Srcs.Client Then
            sb.AppendLine("FROM CustomerDto ")
        Else
            sb.AppendLine("FROM ChannelDto ")
        End If
        sb.AppendLine("WHERE Guid='" & oCustomerDto.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCustomerDto.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCustomerDto
            If oCustomerDto.Src = DTOCustomerTarifaDto.Srcs.Client Then
                oRow("Customer") = .CustomerOrChannel.Guid
            Else
                oRow("Channel") = .CustomerOrChannel.Guid
            End If
            oRow("Fch") = .Fch
            oRow("Dto") = .Dto
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCustomerDto As DTOCustomerTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomerDto, oTrans)
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


    Shared Sub Delete(oCustomerDto As DTOCustomerTarifaDto, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CustomerDto WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCustomerDto.Guid.ToString())
    End Sub

#End Region

    Shared Function FromSkuAndCustomerOrChannel(oSku As DTOProductSku, oCustomer As DTOCustomer) As Decimal
        Dim retval As Decimal = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 X.CustOrChannel, VwProductParent.ParentCod, X.Dto ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("SELECT 1 AS CustOrChannel, VwCcxOrMe.Guid AS Customer, VwChannelDto.Product, VwChannelDto.Dto ")
        sb.AppendLine("FROM VwCcxOrMe ")
        sb.AppendLine("LEFT OUTER JOIN VwContactChannel ON VwCcxOrMe.Ccx = VwContactChannel.Contact ")
        sb.AppendLine("LEFT OUTER JOIN VwChannelDto ON VwContactChannel.Channel = VwChannelDto.Channel ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT 0 AS CustOrChannel, VwCcxOrMe.Guid AS Customer, VwCustomerDto.Product, VwCustomerDto.Dto ")
        sb.AppendLine("FROM VwCustomerDto INNER JOIN VwCcxOrMe ON VwCustomerDto.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine(") X INNER JOIN VwProductParent ON X.Product IS NULL OR X.Product = VwProductParent.Parent AND VwProductParent.Child = '" & oSku.Guid.ToString() & "' ")
        sb.AppendLine("WHERE X.Customer = '" & oCustomer.Guid.ToString() & "' ")
        sb.AppendLine("GROUP BY X.CustOrChannel, VwProductParent.ParentCod, X.Dto ")
        sb.AppendLine("ORDER BY CustOrChannel, ParentCod DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class CustomerTarifaDtosLoader

    'Gets customer product discount over retail price
    Shared Function ForCustomerBasket(oCustomer As DTOCustomer) As Dictionary(Of String, Decimal)
        Dim retval As New Dictionary(Of String, Decimal)

        'select last discounts assigned to customer headquarters
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerDto.Product, VwCustomerDto.Dto ")
        sb.AppendLine("FROM VwCustomerDto ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON VwCustomerDto.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid='" & oCustomer.Guid.ToString() & "' ")
        Dim oDrd = SQLHelper.GetDataReader(sb.ToString)
        Do While oDrd.Read
            retval.Add(CType(oDrd("Product"), Guid).ToString(), oDrd("Dto"))
        Loop
        oDrd.Close()

        'select default channel discounts for products with no discounts
        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT VwChannelDto.Product, VwChannelDto.Dto ")
        sb.AppendLine("FROM VwChannelDto ")
        sb.AppendLine("INNER JOIN VwContactChannel ON VwChannelDto.Channel=VwContactChannel.Channel ")
        sb.AppendLine("WHERE VwContactChannel.Contact='" & oCustomer.Guid.ToString() & "' ")
        oDrd = SQLHelper.GetDataReader(sb.ToString)
        Do While oDrd.Read
            Dim oGuid As Guid = If(IsDBNull(oDrd("Product")), Guid.Empty, oDrd("Product"))
            If Not retval.ContainsKey(oGuid.ToString()) Then
                retval.Add(oGuid.ToString(), oDrd("Dto"))
            End If
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOCustomerTarifaDto)
        Dim retval As New List(Of DTOCustomerTarifaDto)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CustomerDto.Guid, CustomerDto.Fch, CustomerDto.Dto, CustomerDto.Obs, CliGral.FullNom ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine("FROM CustomerDto ")
        sb.AppendLine("INNER JOIN CliGral ON CustomerDto.Customer=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON CustomerDto.Customer=VwCcxOrMe.Ccx ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON CustomerDto.Product=VwProductNom.Guid ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid ='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerTarifaDto(DirectCast(oDrd("Guid"), Guid))
            With item
                .CustomerOrChannel = oCustomer
                DirectCast(.CustomerOrChannel, DTOCustomer).FullNom = oDrd("FullNom")
                .Src = DTOCustomerTarifaDto.Srcs.client
                .Fch = oDrd("Fch")
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
                If Not IsDBNull(oDrd("Obs")) Then
                    .Obs = oDrd("Obs")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oChannel As DTODistributionChannel) As List(Of DTOCustomerTarifaDto)
        Dim retval As New List(Of DTOCustomerTarifaDto)
        If oChannel IsNot Nothing Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ChannelDto.* ")
            sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine("FROM ChannelDto ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ChannelDto.Product=VwProductNom.Guid ")
            sb.AppendLine("WHERE ChannelDto.Channel='" & oChannel.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Fch DESC")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOCustomerTarifaDto(DirectCast(oDrd("Guid"), Guid))
                With item
                    .CustomerOrChannel = oChannel
                    .Src = DTOCustomerTarifaDto.Srcs.canal
                    .Fch = oDrd("Fch")
                    .Dto = oDrd("Dto")
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    If Not IsDBNull(oDrd("Obs")) Then
                        .Obs = oDrd("Obs")
                    End If
                End With
                retval.Add(item)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

End Class

