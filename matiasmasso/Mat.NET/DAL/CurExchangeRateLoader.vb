Public Class CurExchangeRateLoader
    Public Enum Directions
        NextDate
        PreviousDate
    End Enum

    Shared Function Closest(oCur As DTOCur, DtFch As Date, Optional Direction As Directions = Directions.PreviousDate) As DTOCurExchangeRate
        Dim retval As DTOCurExchangeRate = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Fch, Rate ")
        sb.AppendLine("FROM CurExchangeRate ")
        sb.AppendLine("WHERE ISO='" & oCur.Tag & "' ")

        If Direction = Directions.PreviousDate Then
            sb.AppendLine("AND Fch<='" & Format(DtFch, "yyyyMMdd") & "' ")
            sb.AppendLine("ORDER BY Fch DESC")
        Else
            sb.AppendLine("AND Fch>='" & Format(DtFch, "yyyyMMdd") & "' ")
            sb.AppendLine("ORDER BY Fch")
        End If
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = DTOCurExchangeRate.Factory(oDrd("Fch"), oDrd("Rate"))
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class CurExchangeRatesLoader

    Shared Function LastRates(Optional DtFch As Date = Nothing) As DTOCur.Collection
        Dim retval As New DTOCur.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fch, ISO, Rate FROM CurExchangeRate ")
        If DtFch = Nothing Then
            sb.AppendLine("WHERE Fch IN (SELECT TOP 1 Fch FROM CurExchangeRate ORDER BY Fch DESC) ")
        Else
            sb.AppendLine("WHERE Fch ='" & Format(DtFch, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("ORDER BY ISO ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCur As DTOCur = DTOCur.Factory(oDrd("ISO"))
            If oCur IsNot Nothing Then
                oCur.ExchangeRate = DTOCurExchangeRate.Factory(oDrd("Fch"), oDrd("Rate"))
            End If
            retval.Add(oCur)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Save(values As DTOCur.Collection, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Save(values, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(New Exception("Error al actualitzar el canvi de divises"))
            exs.Add(ex)
            'Catch ex2 As sql
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Save(values As DTOCur.Collection, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CurExchangeRate ")
        sb.AppendLine("WHERE Fch=GETDATE() ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In values
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With item
                oRow("ISO") = .Tag
                oRow("Fch") = .ExchangeRate.Fch
                oRow("Rate") = .ExchangeRate.Rate
            End With
        Next

        oDA.Update(oDs)
    End Sub

End Class
