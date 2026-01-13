Public Class WtbolCtrLoader

    Shared Function Delete(oWtbolCtr As DTOWtbolCtr, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWtbolCtr, oTrans)
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


    Shared Sub Delete(oWtbolCtr As DTOWtbolCtr, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WtbolCtr WHERE Guid='" & oWtbolCtr.Guid.ToString & "' "
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Log(oLandingPage As DTOWtbolLandingPage, sIp As String, exs As List(Of Exception)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO WtbolCtr (Product, Site, Ip) ")
        If oLandingPage.Product Is Nothing Then
            sb.AppendLine("VALUES('" & Guid.Empty.ToString & "' ")
        Else
            sb.AppendLine("VALUES('" & oLandingPage.Product.Guid.ToString & "' ")
        End If
        sb.AppendLine(", '" & oLandingPage.Site.Guid.ToString & "' ")
        sb.AppendLine(", '" & Left(sIp, 15) & "')")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
End Class

Public Class WtbolCtrsLoader
    Shared Function All(Optional Site As DTOWtbolSite = Nothing, Optional FchFrom As Date = Nothing, Optional FchTo As Date = Nothing) As List(Of DTOWtbolCtr)
        Dim retval As New List(Of DTOWtbolCtr)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT WtbolCtr.Guid AS CtrGuid, WtbolCtr.Fch, WtbolCtr.Ip ")
        sb.AppendLine(", WtbolCtr.Site, WtbolSite.Nom AS SiteNom ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine("FROM WtbolCtr ")
        sb.AppendLine("INNER JOIN WtbolSite ON WtbolCtr.Site = WtbolSite.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom ON WtbolCtr.Product = VwProductNom.Guid ")
        sb.AppendLine("WHERE 1 = 1 ")
        If Site IsNot Nothing Then
            sb.AppendLine("AND WtbolCtr.Site ='" & Site.Guid.ToString & "' ")
        End If
        If FchFrom <> Nothing Then
            sb.AppendLine("AND WtbolCtr.Fch >='" & Format(FchFrom, "yyyyMMdd") & "' ")
        End If
        If FchTo <> Nothing Then
            sb.AppendLine("AND WtbolCtr.Fch <='" & Format(FchTo, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("ORDER BY Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWtbolCtr(oDrd("CtrGuid"))
            With item
                .Fch = oDrd("Fch")
                .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))
                .Site = New DTOWtbolSite(oDrd("Site"))
                .Site.Nom = SQLHelper.GetStringFromDataReader(oDrd("SiteNom"))
                .Product = SQLHelper.GetProductFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        Return retval
    End Function
End Class