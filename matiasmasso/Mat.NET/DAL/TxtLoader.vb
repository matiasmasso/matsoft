Public Class TxtLoader

#Region "CRUD"

    Shared Function Find(oId As DTOTxt.Ids) As DTOTxt
        Dim retval As DTOTxt = Nothing
        Dim oTxt As New DTOTxt(oId)
        If Load(oTxt) Then
            retval = oTxt
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTxt As DTOTxt) As Boolean
        If Not oTxt.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Txt ")
            sb.AppendLine("WHERE id=" & oTxt.Id & "")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTxt
                    .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ESP", "CAT", "ENG", "POR")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTxt.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTxt As DTOTxt, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTxt, oTrans)
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


    Shared Sub Update(oTxt As DTOTxt, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Txt ")
        sb.AppendLine("WHERE Id=" & oTxt.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = oTxt.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oTxt
            oRow("Esp") = SQLHelper.NullableLangText(.LangText, DTOLang.ESP)
            oRow("Cat") = SQLHelper.NullableLangText(.LangText, DTOLang.CAT)
            oRow("Eng") = SQLHelper.NullableLangText(.LangText, DTOLang.ENG)
            oRow("Por") = SQLHelper.NullableLangText(.LangText, DTOLang.POR)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTxt As DTOTxt, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTxt, oTrans)
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


    Shared Sub Delete(oTxt As DTOTxt, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Txt WHERE Id=" & oTxt.Id & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TxtsLoader

    Shared Function All() As List(Of DTOTxt)
        Dim retval As New List(Of DTOTxt)

        For Each Id As Integer In [Enum].GetValues(GetType(DTOTxt.Ids))
            Dim item As New DTOTxt(Id)
            retval.Add(item)
        Next

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Txt ")
        sb.AppendLine("ORDER BY Id")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOTxt = retval.FirstOrDefault(Function(x) x.Id = oDrd("Id"))
            If item IsNot Nothing Then
                With item
                    .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ESP", "CAT", "ENG", "POR")
                    .IsLoaded = True
                End With
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
