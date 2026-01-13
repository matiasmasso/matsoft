Public Class TrackingLoader

    Shared Function Find(oGuid As Guid) As DTOTracking
        Dim retval As DTOTracking = Nothing
        Dim oTracking As New DTOTracking(oGuid)
        If Load(oTracking) Then
            retval = oTracking
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTracking As DTOTracking) As Boolean
        If Not oTracking.IsLoaded And Not oTracking.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Tracking.Cod, Tracking.Target, Tracking.Obs ")
            sb.AppendLine(", Tracking.FchCreated, Tracking.UsrCreated, UsrCreated.Nom AS UsrCreatedNom ")
            sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
            sb.AppendLine("FROM Tracking ")
            sb.AppendLine("INNER JOIN Cod ON Tracking.Cod = Cod.Guid ")
            sb.AppendLine("INNER JOIN VwLangText ON Tracking.Cod = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.Cods & " ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrCreated ON Tracking.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("WHERE Guid='" & oTracking.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTracking
                    .Target = New DTOBaseGuid(oDrd("Target"))
                    .Cod = New DTOCod(oDrd("Cod"))
                    SQLHelper.LoadLangTextFromDataReader(.Cod.Nom, oDrd)
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTracking.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTracking As DTOTracking, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTracking, oTrans)
            oTrans.Commit()
            oTracking.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oTracking As DTOTracking, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Tracking ")
        sb.AppendLine("WHERE Guid='" & oTracking.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTracking.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTracking
            oRow("Target") = .Target.Guid
            oRow("Cod") = .Cod.Guid
            oRow("UsrCreated") = .UsrLog.UsrCreated.Guid
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTracking As DTOTracking, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTracking, oTrans)
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


    Shared Sub Delete(oTracking As DTOTracking, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Tracking WHERE Guid='" & oTracking.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class TrackingsLoader

    Shared Function All(oTarget As DTOBaseGuid) As DTOTracking.Collection
        Dim retval As New DTOTracking.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tracking.Guid, Tracking.Cod, Tracking.Obs ")
        sb.AppendLine(", Tracking.FchCreated, Tracking.UsrCreated, UsrCreated.Nom AS UsrCreatedNickName ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine("FROM Tracking ")
        sb.AppendLine("INNER JOIN Cod ON Tracking.Cod = Cod.Guid ")
        sb.AppendLine("INNER JOIN VwLangText ON Tracking.Cod = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.Cods & " ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrCreated ON Tracking.UsrCreated = UsrCreated.Guid ")
        sb.AppendLine("WHERE Tracking.Target = '" & oTarget.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Tracking.FchCreated ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTracking(oDrd("Guid"))
            With item
                .Cod = New DTOCod(oDrd("Cod"))
                SQLHelper.LoadLangTextFromDataReader(.Cod.Nom, oDrd)
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                .IsLoaded = True
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

