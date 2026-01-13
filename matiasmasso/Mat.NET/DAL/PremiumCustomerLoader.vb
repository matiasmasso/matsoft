Public Class PremiumCustomerLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPremiumCustomer
        Dim retval As DTOPremiumCustomer = Nothing
        Dim oPremiumCustomer As New DTOPremiumCustomer(oGuid)
        If Load(oPremiumCustomer) Then
            retval = oPremiumCustomer
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPremiumCustomer As DTOPremiumCustomer) As Boolean
        If Not oPremiumCustomer.IsLoaded And Not oPremiumCustomer.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PremiumCustomer.Codi AS PremiumCustomerCodi, PremiumCustomer.Obs ")
            sb.AppendLine(", PremiumCustomer.Customer, CliGral.FullNom ")
            sb.AppendLine(", PremiumCustomer.PremiumLine, PremiumLine.Nom ")
            sb.AppendLine(", PremiumCustomer.UsrCreated, PremiumCustomer.FchCreated, PremiumCustomer.UsrLastEdited, PremiumCustomer.FchLastEdited ")
            sb.AppendLine(", UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", UsrLastEdited.Adr AS UsrLastEditedEmailAddress, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine(", PremiumCustomer.Docfile ")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine("FROM PremiumCustomer ")
            sb.AppendLine("INNER JOIN PremiumLine ON PremiumCustomer.PremiumLine = PremiumLine.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON PremiumCustomer.Customer = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwDocFile ON PremiumCustomer.DocFile=VwDocFile.DocfileHash ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrCreated ON PremiumCustomer.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email UsrLastEdited ON PremiumCustomer.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE PremiumCustomer.Guid='" & oPremiumCustomer.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPremiumCustomer
                    .PremiumLine = New DTOPremiumLine(oDrd("PremiumLine"))
                    With .PremiumLine
                        .Nom = oDrd("Nom")
                    End With
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Codi = oDrd("PremiumCustomerCodi")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPremiumCustomer.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPremiumCustomer As DTOPremiumCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            DocFileLoader.Update(oPremiumCustomer.DocFile, oTrans)
            Update(oPremiumCustomer, oTrans)

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


    Shared Sub Update(oPremiumCustomer As DTOPremiumCustomer, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PremiumCustomer ")
        sb.AppendLine("WHERE Guid='" & oPremiumCustomer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPremiumCustomer.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPremiumCustomer
            oRow("PremiumLine") = .PremiumLine.Guid
            oRow("Customer") = .Customer.Guid
            oRow("Codi") = .Codi
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Docfile") = SQLHelper.NullableDocFile(.DocFile)
            If .UsrLog.UsrCreated Is Nothing Then .UsrLog.UsrCreated = .UsrLog.UsrLastEdited
            If .UsrLog.FchLastEdited = Nothing Then .UsrLog.FchLastEdited = DTO.GlobalVariables.Now()
            If .UsrLog.FchCreated = Nothing Then .UsrLog.FchCreated = .UsrLog.FchLastEdited
            oRow("UsrCreated") = .UsrLog.UsrCreated.Guid
            oRow("FchCreated") = .UsrLog.FchCreated
            oRow("UsrLastEdited") = .UsrLog.UsrLastEdited.Guid
            oRow("FchLastEdited") = .UsrLog.FchLastEdited
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPremiumCustomer As DTOPremiumCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPremiumCustomer, oTrans)
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


    Shared Sub Delete(oPremiumCustomer As DTOPremiumCustomer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PremiumCustomer WHERE Guid='" & oPremiumCustomer.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class PremiumCustomersLoader

    Shared Function All(oPremiumLine As DTOPremiumLine) As List(Of DTOPremiumCustomer)
        Dim retval As New List(Of DTOPremiumCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PremiumCustomer.Guid, PremiumCustomer.Codi AS PremiumCustomerCodi ")
        sb.AppendLine(", PremiumCustomer.Customer, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", CliGral.ContactClass, ContactClass.DistributionChannel ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", PremiumCustomer.FchLastEdited, PremiumCustomer.Docfile ")
        sb.AppendLine("FROM PremiumCustomer ")
        sb.AppendLine("INNER JOIN CliGral ON PremiumCustomer.Customer = CliGral.Guid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
        sb.AppendLine("INNER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON PremiumCustomer.Customer = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE PremiumCustomer.PremiumLine = '" & oPremiumLine.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwAddress.CountryISO, VwAddress.ZonaNom, VwAddress.LocationNom, CliGral.RaoSocial+CliGral.NomCom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPremiumCustomer(oDrd("Guid"))
            With item
                .PremiumLine = oPremiumLine
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                .Customer.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                .Customer.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Codi = oDrd("PremiumCustomerCodi")
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("DocFile"))
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOPremiumCustomer)
        Dim retval As New List(Of DTOPremiumCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PremiumCustomer.Guid, PremiumCustomer.Codi AS PremiumCustomerCodi ")
        sb.AppendLine(", PremiumCustomer.PremiumLine, PremiumLine.Nom ")
        sb.AppendLine(", PremiumCustomer.FchLastEdited, PremiumCustomer.Docfile ")
        sb.AppendLine("FROM PremiumCustomer ")
        sb.AppendLine("INNER JOIN PremiumLine ON PremiumCustomer.PremiumLine = PremiumLine.Guid ")
        sb.AppendLine("WHERE PremiumCustomer.Customer = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY PremiumLine.Nom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPremiumCustomer(oDrd("Guid"))
            With item
                .PremiumLine = New DTOPremiumLine(oDrd("PremiumLine"))
                With .PremiumLine
                    .Nom = oDrd("Nom")
                End With
                .Customer = oCustomer
                .Codi = oDrd("PremiumCustomerCodi")
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("DocFile"))
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
