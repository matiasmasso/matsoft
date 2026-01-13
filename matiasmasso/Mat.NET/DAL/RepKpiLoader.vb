Public Class RepKpiLoader


#Region "CRUD"

    Shared Function Find(id As DTORepKpi.Ids) As DTORepKpi
        Dim retval As DTORepKpi = Nothing
        Dim oRepKpi As New DTORepKpi(id)
        If Load(oRepKpi) Then
            retval = oRepKpi
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepKpi As DTORepKpi) As Boolean
        If Not oRepKpi.IsLoaded And Not oRepKpi.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM RepKpi ")
            sb.AppendLine("WHERE id=" & oRepKpi.Id & " ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRepKpi
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                    .Unit = oDrd("Unit")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepKpi.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepKpi As DTORepKpi, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepKpi, oTrans)
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


    Shared Sub Update(oRepKpi As DTORepKpi, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepKpi ")
        sb.AppendLine("WHERE id=" & oRepKpi.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("id") = oRepKpi.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepKpi
            oRow("NomEsp") = .Nom.Esp
            oRow("NomCat") = .Nom.Cat
            oRow("NomEng") = .Nom.Eng
            oRow("NomPor") = .Nom.Por
            oRow("Unit") = .Unit
        End With

        oDA.Update(oDs)
    End Sub

#End Region

    Shared Function Factory(odrd As SqlDataReader) As DTORepKpi
        Dim retval As New DTORepKpi(odrd("Kpi"))
        With retval
            .Unit = odrd("KpiUnit")
            .Nom = SQLHelper.GetLangTextFromDataReader(odrd, "KpiEsp", "KpiCat", "KpiEng", "KpiPor")
        End With
        Return retval
    End Function

End Class

Public Class RepKpisLoader

    Shared Function All() As List(Of DTORepKpi)
        Dim retval As New List(Of DTORepKpi)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepKpi ")
        sb.AppendLine("ORDER BY NomEsp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepKpi(oDrd("id"))
            With item
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                .Unit = oDrd("Unit")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
