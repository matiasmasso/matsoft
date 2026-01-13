Public Class CurLoader

#Region "CRUD"


    Shared Function Update(oCur As DTOCur, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCur, oTrans)
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


    Shared Sub Update(oCur As DTOCur, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Cur ")
        sb.AppendLine("WHERE Tag='" & oCur.Tag.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Tag") = oCur.Tag
        Else
            oRow = oTb.Rows(0)
        End If

        With oCur
            oRow("Decimals") = .Decimals
            oRow("Symbol") = .Symbol
            oRow("Obsoleto") = .Obsoleto
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCur As DTOCur, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCur, oTrans)
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


    Shared Sub Delete(oCur As DTOCur, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cur WHERE Tag='" & oCur.Tag.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CursLoader

    Shared Function All() As DTOCur.Collection
        Dim retval As New DTOCur.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cur.Tag, Cur.Decimals, Cur.Symbol, Cur.Obsoleto ")
        sb.AppendLine(", VwCurExchange.Fch, VwCurExchange.Rate ")
        sb.AppendLine("FROM Cur ")
        sb.AppendLine("LEFT OUTER JOIN VwCurExchange ON Cur.tag = VwCurExchange.Tag ")
        sb.AppendLine("ORDER BY Cur.Tag")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item = New DTOCur()
            With item
                .Tag = oDrd("Tag")
                .Decimals = SQLHelper.GetIntegerFromDataReader(oDrd("Decimals"))
                .Symbol = SQLHelper.GetStringFromDataReader(oDrd("Symbol"))
                .Obsoleto = oDrd("Obsoleto")

                Select Case item.Tag
                    Case "EUR"
                        .ExchangeRate = DTOCurExchangeRate.Factory(DTO.GlobalVariables.Today(), 1)
                    Case "ESP"
                        .ExchangeRate = DTOCurExchangeRate.Factory(DTO.GlobalVariables.Today(), 166.386)
                    Case Else

                        If Not IsDBNull(oDrd("Rate")) Then
                            .ExchangeRate = DTOCurExchangeRate.Factory(oDrd("Fch"), oDrd("Rate"))
                        End If
                End Select

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

