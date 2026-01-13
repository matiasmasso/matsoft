Public Class WtbolSerpLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWtbolSerp
        Dim retval As DTOWtbolSerp = Nothing
        Dim oWtbolSerp As New DTOWtbolSerp(oGuid)
        If Load(oWtbolSerp) Then
            retval = oWtbolSerp
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolSerp As DTOWtbolSerp) As Boolean
        If Not oWtbolSerp.IsLoaded And Not oWtbolSerp.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM WtbolSerp ")
            sb.AppendLine("INNER JOIN VwProductNom ON WtbolSerp.Product = VwProductNom.Guid ")
            sb.AppendLine("INNER JOIN WtbolSerpItem ON WtbolSerp.Guid = WtbolSerpItem.Serp ")
            sb.AppendLine("INNER JOIN WtbolSite ON WtbolSerpItem.Site = WtbolSite.Guid ")
            sb.AppendLine("WHERE WtbolSerp.Guid='" & oWtbolSerp.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY WtbolSerpItem.Pos ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not oWtbolSerp.IsLoaded Then
                    With oWtbolSerp
                        .Session = New DTOSession(DirectCast(oDrd("Session"), Guid))
                        .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))
                        .UserAgent = SQLHelper.GetStringFromDataReader(oDrd("UserAgent"))
                        .CountryCode = SQLHelper.GetStringFromDataReader(oDrd("CountryCode"))
                        .Fch = oDrd("Fch")
                        .Product = SQLHelper.GetProductFromDataReader(oDrd)
                        .IsLoaded = True
                    End With
                End If
                Dim item As New DTOWtbolSerp.Item
                With item
                    .Pos = oDrd("Pos")
                    .Site = New DTOWtbolSite(oDrd("Site"))
                    With .Site
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .Web = oDrd("Web")
                    End With
                End With
                oWtbolSerp.Items.Add(item)
            End If
            oDrd.Close()
        End If

        Dim retval As Boolean = oWtbolSerp.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWtbolSerp As DTOWtbolSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWtbolSerp, oTrans)
            oTrans.Commit()
            oWtbolSerp.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        UpdateHeader(oWtbolSerp, oTrans)
        UpdateItems(oWtbolSerp, oTrans)
    End Sub

    Shared Sub UpdateHeader(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolSerp ")
        sb.AppendLine("WHERE Guid='" & oWtbolSerp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWtbolSerp.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWtbolSerp
            oRow("Session") = SQLHelper.NullableBaseGuid(.Session)
            oRow("Ip") = SQLHelper.NullableString(.Ip)
            oRow("UserAgent") = SQLHelper.NullableString(.UserAgent)
            oRow("CountryCode") = SQLHelper.NullableString(.CountryCode)
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("Fch") = .Fch
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        If Not oWtbolSerp.IsNew Then DeleteItems(oWtbolSerp, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WtbolSerpItem ")
        sb.AppendLine("WHERE Serp='" & oWtbolSerp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item In oWtbolSerp.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Serp") = oWtbolSerp.Guid
            oRow("Site") = SQLHelper.NullableBaseGuid(item.Site)
            oRow("Pos") = oWtbolSerp.Items.IndexOf(item)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWtbolSerp As DTOWtbolSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWtbolSerp, oTrans)
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


    Shared Sub Delete(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        DeleteItems(oWtbolSerp, oTrans)
        DeleteHeader(oWtbolSerp, oTrans)
    End Sub

    Shared Sub DeleteItems(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolSerpItem WHERE Serp='" & oWtbolSerp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oWtbolSerp As DTOWtbolSerp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolSerp WHERE Guid='" & oWtbolSerp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WtbolSerpsLoader

    Shared Function All(Optional oSite As DTOWtbolSite = Nothing) As List(Of DTOWtbolSerp)
        Dim retval As New List(Of DTOWtbolSerp)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WtbolSerp.Guid, WtbolSerp.Fch, WtbolSerp.Ip, WtbolSerp.UserAgent, WtbolSerp.CountryCode, WtbolSerpItem.Pos ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine(", WtbolSerpItem.Site, WtbolSite.Nom AS SiteNom ")
        sb.AppendLine("FROM WtbolSerp ")
        sb.AppendLine("INNER JOIN VwProductNom ON WtbolSerp.Product = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN WtbolSerpItem ON WtbolSerp.Guid = WtbolSerpItem.Serp ")
        sb.AppendLine("LEFT OUTER JOIN WtbolSite ON WtbolSerpItem.Site = WtbolSite.Guid ")
        If oSite IsNot Nothing Then
            sb.AppendLine("WHERE WtbolSerpItem.Site = '" & oSite.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY WtbolSerp.Fch DESC, WtbolSerp.Guid, WtbolSerpItem.Pos ")
        Dim SQL As String = sb.ToString
        Dim oSerp As New DTOWtbolSerp
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oSerp.Guid.Equals(oDrd("Guid")) Then
                oSerp = New DTOWtbolSerp(oDrd("Guid"))
                With oSerp
                    .Fch = oDrd("Fch")
                    .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))
                    .UserAgent = SQLHelper.GetStringFromDataReader(oDrd("UserAgent"))
                    .CountryCode = SQLHelper.GetStringFromDataReader(oDrd("CountryCode"))
                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                End With
                retval.Add(oSerp)
            End If

            If Not IsDBNull(oDrd("Site")) Then
                Dim item As New DTOWtbolSerp.Item
                With item
                    .Pos = oDrd("Pos")
                    .Site = New DTOWtbolSite(oDrd("Site"))
                    .Site.Nom = SQLHelper.GetStringFromDataReader(oDrd("SiteNom"))
                End With
                oSerp.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

