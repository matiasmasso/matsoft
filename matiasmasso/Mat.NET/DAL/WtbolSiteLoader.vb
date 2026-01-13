Public Class WtbolSiteLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWtbolSite
        Dim retval As DTOWtbolSite = Nothing
        Dim oWtbolSite As New DTOWtbolSite(oGuid)
        If Load(oWtbolSite) Then
            retval = oWtbolSite
        End If
        Return retval
    End Function

    Shared Function FromMerchantId(MerchantId As String) As DTOWtbolSite
        Dim retval As DTOWtbolSite = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WtbolSite.Guid ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("WHERE WtbolSite.MerchantId='" & MerchantId & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOWtbolSite(oDrd("Guid"))
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oWtbolSite As DTOWtbolSite) As Boolean
        If Not oWtbolSite.IsLoaded And Not oWtbolSite.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WtbolSite.*, CliGral.FullNom ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", VwWtbolLandingPage.LandingPage, VwWtbolLandingPage.Url, VwWtbolLandingPage.LandingPageFchCreated ")
            sb.AppendLine(", VwWtbolLandingPage.Stock, VwWtbolLandingPage.Price, VwWtbolLandingPage.StockFchCreated, VwWtbolLandingPage.Status AS LandingPageStatus ")
            sb.AppendLine("FROM WtbolSite ")
            sb.AppendLine("INNER JOIN CliGral ON WtbolSite.Customer=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwWtbolLandingPage ON WtbolSite.Guid = VwWtbolLandingPage.Site ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON VwWtbolLandingPage.Product=VwProductNom.Guid ")
            sb.AppendLine("WHERE WtbolSite.Guid='" & oWtbolSite.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oWtbolSite
                    If Not .IsLoaded Then
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .Web = oDrd("Web")
                        .Customer = New DTOCustomer(oDrd("Customer"))
                        .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                        .MerchantId = SQLHelper.GetStringFromDataReader(oDrd("MerchantId"))
                        .ContactNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                        .ContactEmail = SQLHelper.GetStringFromDataReader(oDrd("ContactEmail"))
                        .ContactTel = SQLHelper.GetStringFromDataReader(oDrd("ContactTel"))
                        .Active = oDrd("Active")
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                        .LandingPages = New List(Of DTOWtbolLandingPage)
                        .IsLoaded = True
                    End If
                End With

                If Not IsDBNull(oDrd("LandingPage")) Then
                    Dim item As New DTOWtbolLandingPage(oDrd("LandingPage"))
                    With item
                        .Site = oWtbolSite
                        .Product = SQLHelper.GetProductFromDataReader(oDrd)
                        If TypeOf .Product Is DTOProductSku Then
                            Dim oSku As DTOProductSku = .Product
                            oSku.Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                            oSku.RRPP = SQLHelper.GetAmtFromDataReader(oDrd("Price"))
                            If oWtbolSite.FchLastStocks = Nothing Then
                                If Not IsDBNull(oDrd("StockFchCreated")) Then
                                    oWtbolSite.FchLastStocks = oDrd("StockFchCreated")
                                End If
                            End If
                        End If
                        .Uri = New Uri(oDrd("Url"))
                        .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("LandingPageFchCreated"))
                        .Status = oDrd("LandingPageStatus")
                    End With
                    oWtbolSite.LandingPages.Add(item)
                End If
            Loop

            oDrd.Close()

            SQL = "select max(FchCreated) as LastFch from wtbolstock where site='" & oWtbolSite.Guid.ToString() & "'"
            oDrd = SQLHelper.GetDataReader(SQL)
            oDrd.Read()
            If Not IsDBNull(oDrd("LastFch")) Then
                oWtbolSite.FchLastStocks = oDrd("LastFch")
            End If
            oDrd.Close()

            End If



            Dim retval As Boolean = oWtbolSite.IsLoaded
        Return retval
    End Function

    Shared Function Logo(oSite As DTOWtbolSite) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WtbolSite.Logo ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("WHERE WtbolSite.Guid='" & oSite.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Logo")
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oWtbolSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWtbolSite, oTrans)
            If oWtbolSite.LandingPages IsNot Nothing Then
                UpdateLandingPages(oWtbolSite, oTrans)
            End If
            oTrans.Commit()
            oWtbolSite.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Function Update(model As DTO.Models.Wtbol.Site, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(model, oTrans)
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


    Shared Sub Update(oWtbolSite As DTOWtbolSite, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("WHERE Guid='" & oWtbolSite.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWtbolSite.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWtbolSite
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Web") = .Web
            oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("MerchantId") = SQLHelper.NullableString(.MerchantId)
            oRow("ContactNom") = SQLHelper.NullableString(.ContactNom)
            oRow("ContactEmail") = SQLHelper.NullableString(.ContactEmail)
            oRow("ContactTel") = SQLHelper.NullableString(.ContactTel)
            oRow("Logo") = .Logo
            oRow("Active") = .Active
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub
    Shared Sub Update(model As DTO.Models.Wtbol.Site, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("WHERE Guid='" & model.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = model.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With model
            oRow("Web") = .Website
            oRow("ContactNom") = SQLHelper.NullableString(.ContactNom)
            oRow("ContactEmail") = SQLHelper.NullableString(.ContactEmail)
            oRow("ContactTel") = SQLHelper.NullableString(.ContactTel)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateLandingPages(oSite As DTOWtbolSite, oTrans As SqlTransaction)
        DeleteLandingPages(oSite, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolLandingPage ")
        sb.AppendLine("WHERE Site = '" & oSite.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each value In oSite.LandingPages
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With value
                oRow("Site") = SQLHelper.NullableBaseGuid(oSite)
                oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
                oRow("Url") = .Uri.AbsoluteUri
                oRow("FchCreated") = .FchCreated
            End With
        Next
        oDA.Update(oDs)

    End Sub

    Shared Function Delete(oWtbolSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWtbolSite, oTrans)
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


    Shared Sub Delete(oWtbolSite As DTOWtbolSite, ByRef oTrans As SqlTransaction)
        DeleteStocks(oWtbolSite, oTrans)
        DeleteLandingPages(oWtbolSite, oTrans)
        DeleteSite(oWtbolSite, oTrans)
    End Sub

    Shared Sub DeleteSite(oWtbolSite As DTOWtbolSite, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolSite WHERE Guid='" & oWtbolSite.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteLandingPages(oWtbolSite As DTOWtbolSite, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolLandingPage WHERE Site='" & oWtbolSite.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteStocks(oWtbolSite As DTOWtbolSite, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolStock WHERE Site='" & oWtbolSite.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function UpdateStock(exs As List(Of Exception), oStock As DTOWtbolStock) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolStock ")
        sb.AppendLine("WHERE Site='" & oStock.Site.Guid.ToString & "' ")
        sb.AppendLine("AND Sku='" & oStock.Sku.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Site") = oStock.Site.Guid
                oRow("Sku") = oStock.Sku.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("Stock") = oStock.Stock
            oRow("FchCreated") = DTO.GlobalVariables.Now()

            oDA.Update(oDs)
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
    Shared Function UpdateLandingPage(exs As List(Of Exception), oLandingPage As Newtonsoft.Json.Linq.JObject) As Boolean
        Dim retval As Boolean = False
        Dim oSite = oLandingPage("Site")
        Dim oSiteGuid = New Guid(oSite("Guid").ToString())
        Dim oProduct = oLandingPage("Product")
        Dim oProductGuid = New Guid(oProduct("Guid").ToString())
        Dim oUri = oLandingPage("Uri")
        Dim url = oUri("OriginalString")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolLandingPage ")
        sb.AppendLine("WHERE Site='" & oSiteGuid.ToString & "' ")
        sb.AppendLine("AND Product='" & oProductGuid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Site") = oSiteGuid
                oRow("Product") = oProductGuid
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("Url") = url
            oDA.Update(oDs)
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

    Shared Function DeleteLandingPage(exs As List(Of Exception), oLandingPage As Newtonsoft.Json.Linq.JObject) As Boolean
        Dim retval As Boolean = False
        Dim oSite = oLandingPage("Site")
        Dim oSiteGuid = New Guid(oSite("Guid").ToString())
        Dim oProduct = oLandingPage("Product")
        Dim oProductGuid = New Guid(oProduct("Guid").ToString())

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE WtbolLandingPage ")
        sb.AppendLine("WHERE Site='" & oSiteGuid.ToString & "' ")
        sb.AppendLine("AND Product='" & oProductGuid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

End Class

Public Class WtbolSitesLoader

    Shared Function All() As List(Of DTOWtbolSite)
        Dim retval As New List(Of DTOWtbolSite)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WtbolSite.Guid, WtbolSite.Nom, WtbolSite.Web, WtbolSite.MerchantId, WtbolSite.Customer, WtbolSite.Active, CliGral.FullNom, WtbolSite.ContactEmail ")
        sb.AppendLine(", X.S0, X.S1, X.S2 ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("LEFT OUTER JOIN ( ")

        sb.AppendLine("SELECT WtbolLandingPage.Site ")
        sb.AppendLine(", SUM(CASE WHEN WtbolLandingPage.Status = 0 THEN 1 ELSE 0 END) AS S0 ")
        sb.AppendLine(", SUM(CASE WHEN WtbolLandingPage.Status = 1 THEN 1 ELSE 0 END) AS S1 ")
        sb.AppendLine(", SUM(CASE WHEN WtbolLandingPage.Status = 2 THEN 1 ELSE 0 END) AS S2 ")
        sb.AppendLine("FROM WtbolLandingPage ")
        sb.AppendLine("GROUP BY WtbolLandingPage.Site ) X ON WtbolSite.Guid = X.Site ")

        sb.AppendLine("INNER JOIN CliGral ON WtbolSite.Customer=CliGral.Guid ")
        sb.AppendLine("ORDER BY WtbolSite.Nom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWtbolSite(oDrd("Guid"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Web = oDrd("Web")
                .MerchantId = SQLHelper.GetStringFromDataReader(oDrd("MerchantId"))
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .ContactEmail = SQLHelper.GetStringFromDataReader(oDrd("ContactEmail"))
                .LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Pending) = SQLHelper.GetIntegerFromDataReader(oDrd("S0"))
                .LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Approved) = SQLHelper.GetIntegerFromDataReader(oDrd("S1"))
                .LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Denied) = SQLHelper.GetIntegerFromDataReader(oDrd("S2"))
                .Active = oDrd("Active")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
