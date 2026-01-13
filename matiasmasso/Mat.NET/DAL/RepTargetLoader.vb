Public Class RepTargetLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORepTarget
        Dim retval As DTORepTarget = Nothing
        Dim oRepTarget As New DTORepTarget(oGuid)
        If Load(oRepTarget) Then
            retval = oRepTarget
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepTarget As DTORepTarget) As Boolean
        If Not oRepTarget.IsLoaded And Not oRepTarget.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT RepTarget.Rep, CliRep.Abr AS Nickname, RepTarget.Kpi, RepTarget.Active ")
            sb.AppendLine(", RepKpi.Unit AS KpiUnit, RepKpi.NomEsp AS KpiEsp, RepKpi.NomCat AS KpiCat, RepKpi.NomEng AS KpiEng, RepKpi.NomPor AS KpiPor")
            sb.AppendLine("FROM RepTarget ")
            sb.AppendLine("INNER JOIN CliRep ON RepTarget.Rep = CliRep.Guid ")
            sb.AppendLine("INNER JOIN RepKpi ON RepTarget.Kpi = RepKpi.Id ")
            sb.AppendLine("WHERE RepTarget.Guid='" & oRepTarget.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRepTarget
                    .Rep = New DTORep(oDrd("Rep"))
                    .Rep.NickName = oDrd("Nickname")
                    .Kpi = RepKpiLoader.Factory(oDrd)
                    .Target = SQLHelper.GetDecimalFromDataReader(oDrd("Target"))
                    .Reward = SQLHelper.GetAmtFromDataReader(oDrd("Reward"))
                    .Active = oDrd("Active")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepTarget.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepTarget As DTORepTarget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepTarget, oTrans)
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


    Shared Sub Update(oRepTarget As DTORepTarget, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM RepTarget ")
        sb.AppendLine("WHERE Guid='" & oRepTarget.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRepTarget.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepTarget
            oRow("Rep") = .Rep.Guid
            oRow("Kpi") = .Kpi.Id
            oRow("Target") = .Target
            oRow("Reward") = SQLHelper.NullableAmt(.Reward)
            oRow("Active") = .Active
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRepTarget As DTORepTarget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepTarget, oTrans)
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


    Shared Sub Delete(oRepTarget As DTORepTarget, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepTarget WHERE Guid='" & oRepTarget.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RepTargetsLoader

    Shared Function All(oRep As DTORep) As List(Of DTORepTarget)
        Dim retval As New List(Of DTORepTarget)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepTarget.Guid, RepTarget.Rep, CliRep.Abr AS Nickname, RepTarget.Kpi, RepTarget.Active ")
        sb.AppendLine(", RepKpi.Unit AS KpiUnit, RepKpi.NomEsp AS KpiEsp, RepKpi.NomCat AS KpiCat, RepKpi.NomEng AS KpiEng, RepKpi.NomPor AS KpiPor")
        sb.AppendLine("FROM RepTarget ")
        sb.AppendLine("INNER JOIN CliRep ON RepTarget.Rep = CliRep.Guid ")
        sb.AppendLine("INNER JOIN RepKpi ON RepTarget.Kpi = RepKpi.Id ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepTarget(oDrd("Guid"))
            With item
                .Kpi = RepKpiLoader.Factory(oDrd)
                .Target = SQLHelper.GetDecimalFromDataReader(oDrd("Target"))
                .Reward = SQLHelper.GetAmtFromDataReader(oDrd("Reward"))
                .Active = oDrd("Active")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
