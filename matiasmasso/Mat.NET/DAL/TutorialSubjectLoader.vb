Public Class TutorialSubjectLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTutorialSubject
        Dim retval As DTOTutorialSubject = Nothing
        Dim oTutorialSubject As New DTOTutorialSubject(oGuid)
        If Load(oTutorialSubject) Then
            retval = oTutorialSubject
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTutorialSubject As DTOTutorialSubject) As Boolean
        If Not oTutorialSubject.IsLoaded And Not oTutorialSubject.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM TutorialSubject ")
            sb.AppendLine("WHERE Guid='" & oTutorialSubject.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTutorialSubject
                    .Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTutorialSubject.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTutorialSubject As DTOTutorialSubject, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTutorialSubject, oTrans)
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


    Shared Sub Update(oTutorialSubject As DTOTutorialSubject, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TutorialSubject ")
        sb.AppendLine("WHERE Guid='" & oTutorialSubject.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTutorialSubject.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTutorialSubject
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTutorialSubject As DTOTutorialSubject, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTutorialSubject, oTrans)
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


    Shared Sub Delete(oTutorialSubject As DTOTutorialSubject, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TutorialSubject WHERE Guid='" & oTutorialSubject.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TutorialSubjectsLoader

    Shared Function All(Optional oRol As DTORol = Nothing) As List(Of DTOTutorialSubject)
        Dim retval As New List(Of DTOTutorialSubject)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TutorialSubject ")
        If oRol IsNot Nothing Then
            sb.AppendLine("INNER JOIN Tutorial ON TutorialSubject.Guid = Tutorial.Parent ")
            sb.AppendLine("WHERE Tutorial.Guid IN (SELECT Tutorial FROM TutorialRol WHERE Rol = " & oRol.Id & " ) ")
        End If
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTutorialSubject(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
