Public Class PremiumLineLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPremiumLine
        Dim retval As DTOPremiumLine = Nothing
        Dim oPremiumLine As New DTOPremiumLine(oGuid)
        If Load(oPremiumLine) Then
            retval = oPremiumLine
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPremiumLine As DTOPremiumLine) As Boolean
        If Not oPremiumLine.IsLoaded And Not oPremiumLine.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PremiumLine.Nom, PremiumLine.Fch, PremiumLine.Codi ")
            sb.AppendLine(", VwCategoryNom.* ")
            sb.AppendLine("FROM PremiumLine ")
            sb.AppendLine("LEFT OUTER JOIN PremiumProduct ON PremiumLine.Guid = PremiumProduct.PremiumLine ")
            sb.AppendLine("LEFT OUTER JOIN VwCategoryNom ON PremiumProduct.Product = VwCategoryNom.CategoryGuid ")
            sb.AppendLine("WHERE PremiumLine.Guid='" & oPremiumLine.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY VwCategoryNom.BrandOrd, VwCategoryNom.CategoryOrd ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oPremiumLine.IsLoaded Then
                    With oPremiumLine
                        .Nom = oDrd("Nom")
                        .fch = oDrd("Fch")
                        .Codi = oDrd("Codi")
                        .products = New List(Of DTOProductCategory)
                        .IsLoaded = True
                    End With
                End If
                Dim oCategory As DTOProductCategory = SQLHelper.GetProductFromDataReader(oDrd)
                If oCategory IsNot Nothing Then oPremiumLine.products.Add(oCategory)
            Loop
            oDrd.Close()
        End If


        Dim retval As Boolean = oPremiumLine.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPremiumLine As DTOPremiumLine, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPremiumLine, oTrans)
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


    Shared Sub Update(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        UpdateHeader(oPremiumLine, oTrans)
        UpdateProducts(oPremiumLine, oTrans)
    End Sub

    Shared Sub UpdateProducts(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        If oPremiumLine.Products.Count > 0 Then DeleteProducts(oPremiumLine, oTrans)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PremiumLine, Product, Ord ")
        sb.AppendLine("FROM PremiumProduct ")
        sb.AppendLine("WHERE PremiumLine='" & oPremiumLine.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim idx As Integer
        For Each item As DTOProductCategory In oPremiumLine.Products
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("PremiumLine") = oPremiumLine.Guid
            oRow("Product") = item.Guid
            oRow("Ord") = idx
            idx += 1
        Next

        oDA.Update(oDs)

    End Sub

    Shared Sub UpdateHeader(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PremiumLine ")
        sb.AppendLine("WHERE Guid='" & oPremiumLine.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPremiumLine.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPremiumLine
            oRow("Nom") = .nom
            oRow("Fch") = .fch
            oRow("Codi") = .Codi
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPremiumLine As DTOPremiumLine, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPremiumLine, oTrans)
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


    Shared Sub Delete(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        DeleteCustomers(oPremiumLine, oTrans)
        DeleteProducts(oPremiumLine, oTrans)
        DeleteHeader(oPremiumLine, oTrans)
    End Sub

    Shared Sub DeleteCustomers(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PremiumCustomer WHERE PremiumLine='" & oPremiumLine.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteProducts(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PremiumProduct WHERE PremiumLine='" & oPremiumLine.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
    Shared Sub DeleteHeader(oPremiumLine As DTOPremiumLine, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PremiumLine WHERE Guid='" & oPremiumLine.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function EmailRecipients(oPremiumLine As DTOPremiumLine) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT        X.guid, X.Adr, X.Lang, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, VwAddress.* ")
        sb.AppendLine(", CliGral.ContactClass, ContactClass.Esp AS ContactClassNom, ContactClass.DistributionChannel, DistributionChannel.NomEsp AS DistributionChannelNom ")
        sb.AppendLine("FROM (SELECT    Email.guid, Email.Adr, Email.Lang, VwCcxOrMe.Ccx ")
        sb.AppendLine("FROM             PremiumCustomer ")
        sb.AppendLine("                 INNER JOIN VwCcxOrMe ON PremiumCustomer.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwCcxOrMe.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.guid ")
        sb.AppendLine("WHERE        (PremiumCustomer.PremiumLine = '" & oPremiumLine.Guid.ToString & "') ")
        sb.AppendLine("AND Email.Obsoleto = 0 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        sb.AppendLine("AND Email.Privat = 0 ")
        sb.AppendLine("AND Email.NoNews = 0 ")
        sb.AppendLine("AND PremiumCustomer.Codi = 1 ")
        sb.AppendLine("GROUP BY Email.guid, Email.Adr, Email.Lang, VwCcxOrMe.Ccx) X ")
        sb.AppendLine("INNER JOIN CliGral ON X.Ccx = CliGral.Guid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
        sb.AppendLine("INNER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON X.Ccx = VwAddress.SrcGuid ")
        sb.AppendLine("ORDER BY VwAddress.CountryISO, VwAddress.ZonaNom, VwAddress.LocationNom, CliGral.RaoSocial+CliGral.NomCom, CliGral.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oContact As New DTOContact
        Dim oContactClass As New DTOContactClass
        Dim oDistributionChannel As New DTODistributionChannel
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("SrcGuid")) Then
                If Not oContactClass.Guid.Equals(oDrd("ContactClass")) Then
                    If Not oDistributionChannel.Guid.Equals(oDrd("DistributionChannel")) Then
                        oDistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        oDistributionChannel.LangText.Esp = oDrd("DistributionChannelNom")
                    End If
                    oContactClass = New DTOContactClass(oDrd("ContactClass"))
                    With oContactClass
                        .DistributionChannel = oDistributionChannel
                        .Nom.Esp = oDrd("DistributionChannelNom")
                    End With
                End If
                oContact = New DTOContact(oDrd("SrcGuid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .FullNom = oDrd("FullNom")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .ContactClass = oContactClass
                End With
            End If
            Dim item As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            With item
                .EmailAddress = oDrd("Adr")
                .Lang = DTOLang.Factory(oDrd("Lang"))
                .Contact = oContact
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProduct(oProduct As DTOProduct) As DTOPremiumLine
        Dim retval As DTOPremiumLine = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 PremiumProduct.PremiumLine ")
        sb.AppendLine("FROM PremiumProduct ")
        sb.AppendLine("INNER JOIN VwProductParent ON PremiumProduct.Product = VwProductParent.Parent ")
        sb.AppendLine("WHERE VwProductParent.Child = '" & oProduct.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPremiumLine(oDrd("PremiumLine"))
        End If
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class PremiumLinesLoader

    Shared Function All() As List(Of DTOPremiumLine)
        Dim retval As New List(Of DTOPremiumLine)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PremiumLine ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPremiumLine(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

