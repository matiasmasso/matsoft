Imports System.Text
Imports System.Text.RegularExpressions
Imports DAL.Integracions.Edi

Public Class TestLoader

    Shared Function InvRpt() As Boolean
        Dim Edis = InvRptsLoader.All()
        Dim exs As New List(Of Exception)
        For i As Integer = 5480 To Edis.Count - 1
            InvRptLoader.Update(exs, Edis(i))
        Next
        Return True
    End Function


    Shared Function BuildHashes() As Boolean
        Dim exs As New List(Of Exception)
        Dim oUsers As New List(Of DTOUser)
        Dim SQL = "SELECT Guid, Adr, Pwd FROM Email WHERE Hash IS NULL"
        SQL = SQL & " AND (Adr = 'ingrid@bitti.es' OR Adr='matias@matiasmasso.es')"
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(oDrd("Guid"))
            oUser.EmailAddress = oDrd("Adr")
            oUser.Password = oDrd("Pwd")
            oUsers.Add(oUser)
        Loop
        oDrd.Close()
        Dim count = oUsers.Count



        Dim Cancel As Boolean = False
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Dim done As Integer
        Try
            For Each oUser In oUsers
                If Cancel = True Then Exit For
                SQL = "UPDATE Email SET Hash = '" & oUser.HashPassword() & "' WHERE Guid = '" & oUser.Guid.ToString() & "'"
                Dim i = SQLHelper.ExecuteNonQuery(SQL, oTrans)
                done += 1
                'If done > 100000 Then Cancel = True
            Next
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return exs.Count = 0


    End Function
    Shared Function GeoCode() As Boolean
        Dim sql = "select Guid, Cli, FullNom, FchCreated from cligral left outer join CliAdr on CliGral.Guid = CliAdr.SrcGuid WHERE cLIgRAL.eMP=1 AND CliAdr.Cod=1 and Latitud=0 and CliAdr.adr > '' order by cli desc"
        Dim oDrd = SQLHelper.GetDataReader(sql)
        Dim oGuids As New List(Of Guid)
        Do While oDrd.Read
            oGuids.Add(New Guid(oDrd("Guid").ToString()))
        Loop
        oDrd.Close()
        For Each oGuid In oGuids
            Dim oContact = ContactLoader.Find(oGuid)
            Dim googleText As String = DTOAddress.GoogleText(oContact.Address)
            Dim exs As New List(Of Exception)
            Dim oGeoCoordenadas = GoogleMapsHelper.GeoCode(exs, googleText)
            If exs.Count = 0 Then
                oContact.Address.Coordenadas = oGeoCoordenadas
                ContactLoader.Update(oContact, exs)
                If exs.Count > 0 Then Stop
            Else
                Stop
            End If
        Next
        Return True
    End Function

    Shared Function SwitchCategoryPlugin() As Boolean
        Dim oGuids As New List(Of Guid)
        'Dim SQL = "select PKey from langtext where text like '%https://www.matiasmasso.es/plugin/skucolors/%'"
        Dim SQL = "select PKey from langtext where text like '%https://www.matiasmasso.es/plugin/RelatedProducts'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oGuids.Add(oDrd("PKey"))
        Loop
        oDrd.Close()

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = Nothing
        For Each oGuid As Guid In oGuids
            Try
                Dim i = oGuids.IndexOf(oGuid)
                Dim sb = New System.Text.StringBuilder
                sb.AppendLine("SELECT PKey, Text ")
                sb.AppendLine("FROM LangText ")
                sb.AppendLine("WHERE PKey='" & oGuid.ToString & "' ")
                SQL = sb.ToString

                oTrans = oConn.BeginTransaction

                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                If oTb.Rows.Count = 0 Then
                    Stop
                    Throw New Exception("row not found")
                Else
                    Dim oRow As DataRow = oTb.Rows(0)
                    oRow("Text") = AmendedText(oRow("Text"))
                    oDA.Update(oDs)
                    oTrans.Commit()
                End If
            Catch ex As Exception
                oTrans.Rollback()
            End Try
        Next
        Return True

    End Function
    Shared Function SwitchProductTextH2ToH1() As Boolean
        Dim oGuids As New List(Of Guid)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("select PKey from langtext where Src=30 and text like '%<h2>%'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oGuids.Add(oDrd("PKey"))
        Loop
        oDrd.Close()

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = Nothing
        For Each oGuid As Guid In oGuids
            Try
                Dim i = oGuids.IndexOf(oGuid)
                sb = New System.Text.StringBuilder
                sb.AppendLine("SELECT PKey, Text ")
                sb.AppendLine("FROM LangText ")
                sb.AppendLine("WHERE PKey='" & oGuid.ToString & "' ")
                SQL = sb.ToString

                oTrans = oConn.BeginTransaction

                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                If oTb.Rows.Count = 0 Then
                    Stop
                    Throw New Exception("row not found")
                Else
                    Dim oRow As DataRow = oTb.Rows(0)
                    oRow("Text") = AmendedText(oRow("Text"))
                    oDA.Update(oDs)
                    oTrans.Commit()
                End If
            Catch ex As Exception
                oTrans.Rollback()
            End Try
        Next
        Return True
    End Function

    Private Shared Function AmendedText(src As String) As String
        Dim pattern = String.Format("<iframe src='https://www.matiasmasso.es/plugin/skucolors/{0}' style='border:0;' width='100%' height='205px'></iframe>", GuidHelper.RegexPattern)
        Dim matches As MatchCollection = Regex.Matches(src, pattern)
        Dim sb As StringBuilder = New StringBuilder(src)

        'replace plugin markups by their expanded html code
        For Each match As Match In matches
            Dim guid = New Guid(match.Groups("Guid").Value)
            Dim spare = DTOProductPlugin.Snippet(guid.ToString(), DTOProductPlugin.Modes.Collection)
            sb = sb.Replace(match.Value, spare)
        Next

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Shared Function AmendedText2(src As String) As String

        'replace first h2 by h1
        Dim retval As String = src
        Dim idx = src.IndexOf("<h2>", StringComparison.InvariantCultureIgnoreCase)
        retval = retval.Substring(0, idx) & "<h1>" & retval.Substring(idx + 4)
        idx = src.IndexOf("</h2>", StringComparison.InvariantCultureIgnoreCase)
        retval = retval.Substring(0, idx) & "</h1>" & retval.Substring(idx + 5)

        'replace all h3 by h2
        retval = retval.Replace("h3>", "h2>")
        Return retval
    End Function
End Class
