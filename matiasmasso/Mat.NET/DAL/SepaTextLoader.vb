Public Class SepaTextLoader
    Shared Function Update(oSepaText As DTOSepaText, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSepaText, oTrans)
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


    Shared Sub Update(oSepaText As DTOSepaText, ByRef oTrans As SqlTransaction)
        DeleteLangs(oSepaText, oTrans)
        UpdateLangs(oSepaText, oTrans)
    End Sub
    Shared Sub UpdateLangs(oSepaText As DTOSepaText, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Sepa_Texts ")
        sb.AppendLine("WHERE id=" & oSepaText.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        UpdateLang(oTb, oSepaText, DTOLang.ESP)
        UpdateLang(oTb, oSepaText, DTOLang.CAT)
        UpdateLang(oTb, oSepaText, DTOLang.ENG)
        UpdateLang(oTb, oSepaText, DTOLang.POR)
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateLang(oTb As DataTable, item As DTOSepaText, oLang As DTOLang)
        If item.LangText.Text(oLang) > "" Then
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("id") = item.Id
            oRow("Lang") = oLang.Tag
            oRow("Text") = item.LangText.Text(oLang)
        End If
    End Sub

    Shared Sub DeleteLangs(oSepaText As DTOSepaText, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Sepa_Texts WHERE id=" & oSepaText.Id & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
Public Class SepaTextsLoader

    Shared Function All() As List(Of DTOSepaText)
        Dim retval As New List(Of DTOSepaText)
        Dim SQL As String = "SELECT Lang, Id, Text FROM SEPA_TEXTS WHERE (Lang = 'ESP' or LANG = 'CAT' OR LANG='ENG' OR LANG = 'POR') ORDER BY Id, Lang"
        Dim item As New DTOSepaText()
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If oDrd("Id") <> item.Id Then
                item = New DTOSepaText(oDrd("Id"))
                retval.Add(item)
            End If
            Dim oLang As DTOLang = DTOLang.Factory(oDrd("Lang"))
            item.LangText.SetText(oLang, SQLHelper.GetStringFromDataReader(oDrd("Text")))
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
