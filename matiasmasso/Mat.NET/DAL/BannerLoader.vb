Public Class BannerLoader

    Shared Function Find(oGuid As Guid) As DTOBanner
        Dim retval As DTOBanner = Nothing
        Dim oBanner As New DTOBanner(oGuid)
        If Load(oBanner) Then
            retval = oBanner
        End If
        Return retval
    End Function

    Shared Function Image(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Banner.Image ")
        sb.AppendLine("FROM Banner ")
        sb.AppendLine("WHERE Banner.Guid='" & oGuid.ToString & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = ImageMime.Factory(oDrd("Image"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oBanner As DTOBanner) As Boolean
        Dim retval As Boolean

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo, Banner.Lang ")
        sb.AppendLine(", Banner.Product, VwProductNom.Cod AS ProductCod, VwProductNom.FullNom AS ProductNom ")
        sb.AppendLine("FROM Banner ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Banner.Product = VwProductNom.Guid ")
        sb.AppendLine("WHERE Banner.Guid='" & oBanner.Guid.ToString & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oBanner
                .Nom = oDrd("Nom").ToString
                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                If Not IsDBNull(oDrd("Product")) Then
                    .Product = DTOProduct.Factory(oDrd("Product"), SQLHelper.GetIntegerFromDataReader(oDrd("ProductCod")), SQLHelper.GetStringFromDataReader(oDrd("ProductNom")))
                End If
                .NavigateTo = DTOUrl.Factory(oDrd("NavigateTo").ToString())
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            End With
            retval = True
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oBanner As DTOBanner, exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oBanner, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
            oTrans.Rollback()
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Protected Shared Sub Update(oBanner As DTOBanner, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Banner WHERE Guid='" & oBanner.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBanner.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBanner
            oRow("Nom") = .Nom
            oRow("FchFrom") = .FchFrom
            If .FchTo = Nothing Then
                oRow("FchTo") = System.DBNull.Value
            Else
                oRow("FchTo") = .FchTo
            End If
            oRow("NavigateTo") = .NavigateTo.RelativeUrl(DTOLang.ESP)
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("Lang") = SQLHelper.NullableLang(.lang)
            oRow("Image") = If(.Image, System.DBNull.Value)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBanner As DTOBanner, exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBanner, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
            oTrans.Rollback()
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Protected Shared Sub Delete(oBanner As DTOBanner, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Banner WHERE Guid='" & oBanner.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class BannersLoader

    Shared Function Active(oLang As DTOLang) As List(Of DTOBanner)
        Dim retval As New List(Of DTOBanner)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Banner.Guid, Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo ")
        sb.AppendLine("FROM Banner ")
        sb.AppendLine("WHERE (Banner.FchFrom IS NULL OR Banner.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (Banner.FchTo IS NULL OR Banner.FchTo>=GETDATE()) ")
        If oLang IsNot Nothing Then
            sb.AppendLine("AND (Banner.Lang IS NULL OR Banner.Lang = '" & oLang.Tag & "') ")
        End If
        sb.AppendLine("GROUP BY Banner.Guid, Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo ")
        sb.AppendLine("ORDER BY Banner.FchFrom DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanner As New DTOBanner(oDrd("Guid"))
            With oBanner
                .Nom = oDrd("Nom")
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .NavigateTo = DTOUrl.Factory(oDrd("NavigateTo").ToString())
                'If .NavigateTo.StartsWith("https://www.matiasmasso.es") Then .NavigateTo = .NavigateTo.Replace("https://www.matiasmasso.es", "")
                .lang = oLang
            End With
            retval.Add(oBanner)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ActiveHeaders(oChannel As DTODistributionChannel) As List(Of DTOBanner) 'TO DEPRECATE
        Dim retval As New List(Of DTOBanner)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Banner.Guid, Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo, Banner.Lang ")
        sb.AppendLine("FROM Banner ")
        sb.AppendLine("INNER JOIN VwProductParent ON Banner.Product = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
        sb.AppendLine("     AND ProductChannel.Cod = 0 ")
        sb.AppendLine("     AND ProductChannel.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
        sb.AppendLine("WHERE (Banner.FchFrom IS NULL OR Banner.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (Banner.FchTo IS NULL OR Banner.FchTo>=GETDATE()) ")
        sb.AppendLine("GROUP BY Banner.Guid, Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo, Banner.Lang ")
        sb.AppendLine("ORDER BY Banner.FchFrom DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanner As New DTOBanner(oDrd("Guid"))
            With oBanner
                .Nom = oDrd("Nom")
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .NavigateTo = DTOUrl.Factory(oDrd("NavigateTo").ToString())
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            End With
            retval.Add(oBanner)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(includeObsolets As Boolean) As List(Of DTOBanner)
        Dim retval As New List(Of DTOBanner)
        Dim sb As New System.Text.StringBuilder
        Dim sToday = DTO.GlobalVariables.Now().ToString("yyyyMMdd")
        'Dim sToday = String.Format(DTO.GlobalVariables.Now(), "yyyyMMdd")
        sb.AppendLine("SELECT Banner.Guid, Banner.Nom, Banner.FchFrom, Banner.FchTo, Banner.NavigateTo, Banner.Lang ")
        sb.AppendLine("FROM Banner ")
        If Not includeObsolets Then
            sb.AppendLine(String.Format("WHERE (FchFrom IS NULL OR FchFrom <= '{0}') AND (FchTo IS NULL OR FchTo >= '{0}') ", sToday))
        End If
        sb.AppendLine("ORDER BY Banner.FchFrom DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanner As New DTOBanner(oDrd("Guid"))
            With oBanner
                .Nom = oDrd("Nom")
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .NavigateTo = DTOUrl.Factory(oDrd("NavigateTo").ToString())
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            End With
            retval.Add(oBanner)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Sprite(oGuids As List(Of Guid)) As List(Of Byte())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT Banner.Image ")
        sb.AppendLine("FROM Banner ")
        sb.AppendLine("INNER JOIN @Table X ON Banner.Guid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            If Not IsDBNull(oDrd("image")) Then
                retval.Add(oDrd("Image"))
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ActiveHeaders(oUser As DTOUser) As List(Of DTOBanner)
        Dim retval As New List(Of DTOBanner)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Banner.Guid, Banner.Nom, Banner.NavigateTo, Banner.Lang ")
        sb.AppendLine("FROM Banner ")

        If oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN VwSkuNom ON ( Banner.Product = VwSkuNom.BrandGuid OR Banner.Product = VwSkuNom.CategoryGuid OR Banner.Product = VwSkuNom.SkuGuid) ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("INNER JOIN VwSkuNom ON ( Banner.Product = VwSkuNom.BrandGuid OR Banner.Product = VwSkuNom.CategoryGuid OR Banner.Product = VwSkuNom.SkuGuid) ")
                    sb.AppendLine("INNER JOIN VwCustomerSkus ON VwSkuNom.SkuGuid = VwCustomerSkus.SkuGuid AND VwCustomerSkus.Obsoleto = 0 ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("INNER JOIN VwSkuNom ON ( Banner.Product = VwSkuNom.BrandGuid OR Banner.Product = VwSkuNom.CategoryGuid OR Banner.Product = VwSkuNom.SkuGuid) ")
                    sb.AppendLine("INNER JOIN VwRepSkus ON VwSkuNom.SkuGuid = VwRepSkus.SkuGuid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
        End If

        sb.AppendLine("WHERE (Banner.FchFrom IS NULL OR Banner.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (Banner.FchTo IS NULL OR Banner.FchTo>=GETDATE()) ")

        sb.AppendLine("GROUP BY Banner.Guid, Banner.Nom, Banner.NavigateTo, Banner.FchFrom, Banner.Lang ")
        sb.AppendLine("ORDER BY Banner.FchFrom DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanner As New DTOBanner(oDrd("Guid"))
            With oBanner
                .Nom = oDrd("Nom")
                .NavigateTo = DTOUrl.Factory(oDrd("NavigateTo").ToString())
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            End With
            retval.Add(oBanner)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
