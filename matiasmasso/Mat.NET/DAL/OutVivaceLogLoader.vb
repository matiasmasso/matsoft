Public Class OutVivaceLogLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOOutVivaceLog
        Dim retval As DTOOutVivaceLog = Nothing
        Dim oOutVivaceLog As New DTOOutVivaceLog(oGuid)
        If Load(oOutVivaceLog) Then
            retval = oOutVivaceLog
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oOutVivaceLog As DTOOutVivaceLog) As Boolean
        If Not oOutVivaceLog.IsLoaded And Not oOutVivaceLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT OutVivaceLog.Fch, OutVivaceLog.FchCreated ")
            sb.AppendLine(", OutVivaceExpedicio.Guid AS Expedicio, OutVivaceLog.FchCreated ")
            sb.AppendLine("FROM OutVivaceLog ")
            sb.AppendLine("LEFT OUTER JOIN OutVivaceExpedicio ON OutVivaceLog.Guid = OutVivaceExpedicio.Log ")
            sb.AppendLine("LEFT OUTER JOIN OutVivaceCarrec ExpCarrec ON OutVivaceExpedicio.Guid = ExpCarrec.Parent ")
            sb.AppendLine("LEFT OUTER JOIN OutVivaceAlbExp ON OutVivaceExpedicio.Guid = OutVivaceAlbExp.Exp ")
            sb.AppendLine("LEFT OUTER JOIN Alb ON OutVivaceAlbExp.Alb = Alb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN OutVivaceCarrec AlbCarrec ON Alb.Guid = AlbCarrec.Parent ")
            sb.AppendLine("WHERE Guid='" & oOutVivaceLog.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oExpedicio As New DTOOutVivaceLog.expedicion
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not oOutVivaceLog.IsLoaded Then
                    With oOutVivaceLog
                        .fecha = oDrd("Fch")
                        .fchCreated = oDrd("FchCreated")
                        .IsLoaded = True
                    End With
                End If
                If Not oExpedicio.Guid.Equals(oDrd("Expedicio")) Then
                    oExpedicio = New DTOOutVivaceLog.expedicion(oDrd("Expedicio"))
                    With oExpedicio
                        .vivace = SQLHelper.GetStringFromDataReader(oDrd("id"))
                        .bultos = SQLHelper.GetIntegerFromDataReader(oDrd("bultos"))
                        .m3 = SQLHelper.GetDecimalFromDataReader(oDrd("m3"))
                        .kg = SQLHelper.GetDecimalFromDataReader(oDrd("kg"))
                        .transpnif = SQLHelper.GetStringFromDataReader(oDrd("transpnif"))
                        .albaranes = New List(Of DTOOutVivaceLog.albaran)
                    End With
                End If

            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oOutVivaceLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oOutVivaceLog As DTOOutVivaceLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oOutVivaceLog, oTrans)
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

    Shared Sub Update(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        'DeleteAlbs(oOutVivaceLog, oTrans)
        'DeleteExpedicions(oOutVivaceLog, oTrans)
        UpdateLog(oOutVivaceLog, oTrans)
        UpdateExpedicions(oOutVivaceLog, oTrans)
        UpdateAlbs(oOutVivaceLog, oTrans)
        UpdateCarrecs(oOutVivaceLog, oTrans)
    End Sub


    Shared Sub UpdateLog(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OutVivaceLog ")
        sb.AppendLine("WHERE Guid='" & oOutVivaceLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oOutVivaceLog.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oOutVivaceLog
            oRow("Fch") = .fecha
        End With

        oDA.Update(oDs)
    End Sub
    Shared Sub UpdateExpedicions(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        For Each item As DTOOutVivaceLog.expedicion In oOutVivaceLog.expediciones
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM OutVivaceExpedicio ")
            sb.AppendLine("WHERE Guid='" & item.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = item.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            With item
                oRow("id") = .vivace
                oRow("bultos") = .bultos
                oRow("m3") = .m3
                oRow("kg") = .kg
                oRow("transpnif") = .transpnif
                oRow("log") = oOutVivaceLog.Guid
            End With

            oDA.Update(oDs)
        Next
    End Sub
    Shared Sub UpdateAlbs(value As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        DeleteAlbs(value, oTrans)
        For Each exp As DTOOutVivaceLog.expedicion In value.expediciones
            For Each alb As DTOOutVivaceLog.albaran In exp.albaranes
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT * ")
                sb.AppendLine("FROM OutVivaceAlbExp ")
                sb.AppendLine("WHERE Alb='" & alb.guid.ToString & "' ")
                Dim SQL As String = sb.ToString

                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                Dim oRow As DataRow
                If oTb.Rows.Count = 0 Then
                    oRow = oTb.NewRow
                    oTb.Rows.Add(oRow)
                    oRow("alb") = alb.guid
                Else
                    oRow = oTb.Rows(0)
                End If

                oRow("expedicio") = exp.Guid

                oDA.Update(oDs)
            Next

        Next
    End Sub
    Shared Sub UpdateCarrecs(value As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        DeleteCarrecs(value, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OutVivaceCarrec ")
        sb.AppendLine("WHERE Guid IS NULL")
        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each exp As DTOOutVivaceLog.expedicion In value.expediciones
            For Each carrec As DTOOutVivaceLog.cargo In exp.cargos
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                With carrec
                    oRow("Guid") = .Guid
                    oRow("parent") = exp.Guid
                    oRow("qty") = .unidades
                    oRow("ref") = .codigo
                    oRow("price") = .precio
                End With
            Next
            For Each alb As DTOOutVivaceLog.albaran In exp.albaranes
                If alb.cargos IsNot Nothing Then
                    For Each carrec As DTOOutVivaceLog.cargo In alb.cargos
                        Dim oRow As DataRow = oTb.NewRow
                        oTb.Rows.Add(oRow)
                        With carrec
                            oRow("Guid") = .Guid
                            oRow("parent") = exp.Guid
                            oRow("qty") = .unidades
                            oRow("ref") = .codigo
                            oRow("price") = .precio
                        End With
                    Next

                End If
            Next
        Next
        oDA.Update(oDs)

    End Sub

    Shared Sub DeleteCarrecs(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE OutVivaceCarrec ")
        sb.AppendLine("FROM OutVivaceCarrec ")
        sb.AppendLine("LEFT OUTER JOIN OutVivaceAlbExp ")
        sb.AppendLine("LEFT OUTER JOIN OutVivaceExpedicio ON (OutVivaceAlbExp.Expedicio = OutVivaceExpedicio.Guid ")
        sb.AppendLine("WHERE OutVivaceExpedicio.Log='" & oOutVivaceLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        'SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteAlbs(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE OutVivaceAlbExp ")
        sb.AppendLine("FROM OutVivaceAlbExp ")
        sb.AppendLine("INNER JOIN OutVivaceExpedicio ON OutVivaceAlbExp.Expedicio = OutVivaceExpedicio.Guid ")
        sb.AppendLine("WHERE OutVivaceExpedicio.Log='" & oOutVivaceLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteExpedicions(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE OutVivaceExpedicio ")
        sb.AppendLine("WHERE Log='" & oOutVivaceLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Delete(oOutVivaceLog As DTOOutVivaceLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oOutVivaceLog, oTrans)
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


    Shared Sub Delete(oOutVivaceLog As DTOOutVivaceLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE OutVivaceLog WHERE Guid='" & oOutVivaceLog.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region



End Class

Public Class OutVivaceLogsLoader

    Shared Function All() As List(Of DTOOutVivaceLog)
        Dim retval As New List(Of DTOOutVivaceLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OutVivaceLog ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOOutVivaceLog(oDrd("Guid"))
            With item
                '.Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class