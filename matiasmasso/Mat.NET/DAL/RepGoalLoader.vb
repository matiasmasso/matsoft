Public Class RepGoalLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORepGoal
        Dim retval As DTORepGoal = Nothing
        Dim oRepGoal As New DTORepGoal(oGuid)
        If Load(oRepGoal) Then
            retval = oRepGoal
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepGoal As DTORepGoal) As Boolean
        If Not oRepGoal.IsLoaded And Not oRepGoal.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT RepGoal.Rep, CliRep.Abr AS Nickname, RepGoal.Kpi ")
            sb.AppendLine(", RepKpi.Unit AS KpiUnit, RepKpi.NomEsp AS KpiEsp, RepKpi.NomCat AS KpiCat, RepKpi.NomEng AS KpiEng, RepKpi.NomPor AS KpiPor")
            sb.AppendLine("FROM RepGoal ")
            sb.AppendLine("INNER JOIN CliRep ON RepGoal.Rep = CliRep.Guid ")
            sb.AppendLine("INNER JOIN RepKpi ON RepGoal.Kpi = RepKpi.Id ")
            sb.AppendLine("WHERE RepGoal.Guid='" & oRepGoal.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRepGoal
                    '.Rep = New DTORep(oDrd("Rep"))
                    '.Rep.NickName = oDrd("Nickname")
                    '.Kpi = RepKpiLoader.Factory(oDrd)
                    .Fch = oDrd("Fch")
                    '.Goal = SQLHelper.GetDecimalFromDataReader(oDrd("Goal"))
                    '.Reward = SQLHelper.GetAmtFromDataReader(oDrd("Reward"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepGoal.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepGoal As DTORepGoal, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepGoal, oTrans)
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


    Shared Sub Update(oRepGoal As DTORepGoal, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepGoal ")
        sb.AppendLine("WHERE Guid='" & oRepGoal.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRepGoal.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepGoal
            'oRow("Rep") = .Rep.Guid
            'oRow("Kpi") = .Kpi.Id
            oRow("Fch") = .Fch
            'oRow("Goal") = .Goal
            'oRow("Reward") = SQLHelper.NullableAmt(.Reward)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRepGoal As DTORepGoal, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepGoal, oTrans)
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


    Shared Sub Delete(oRepGoal As DTORepGoal, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepGoal WHERE Guid='" & oRepGoal.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RepGoalsLoader

    Shared Function All(oRep As DTORep) As List(Of DTORepGoal)
        Dim retval As New List(Of DTORepGoal)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepGoal.* ")
        sb.AppendLine("FROM RepGoal ")
        sb.AppendLine("ORDER BY NomEsp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepGoal(oDrd("Guid"))
            With item
                '.Rep = New DTORep(oDrd("Rep"))
                '.Kpi = New DTORepKpi(oDrd("Kpi"))
                .Fch = oDrd("Fch")
                '.Goal = SQLHelper.GetDecimalFromDataReader(oDrd("Goal"))
                '.Reward = SQLHelper.GetAmtFromDataReader(oDrd("Reward"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
