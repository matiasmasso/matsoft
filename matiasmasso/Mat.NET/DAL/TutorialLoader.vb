Public Class TutorialLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTutorial
        Dim retval As DTOTutorial = Nothing
        Dim oTutorial As New DTOTutorial(oGuid)
        If Load(oTutorial) Then
            retval = oTutorial
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTutorial As DTOTutorial) As Boolean
        If Not oTutorial.IsLoaded And Not oTutorial.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Tutorial.Parent, Tutorial.Title, Tutorial.Excerpt, Tutorial.YouTubeId, Tutorial.Fch ")
            sb.AppendLine(", TutorialRol.Rol ")
            sb.AppendLine("FROM Tutorial ")
            sb.AppendLine("LEFT OUTER JOIN TutorialRol ON Tutorial.Guid = TutorialRol.Tutorial ")
            sb.AppendLine("WHERE Tutorial.Guid='" & oTutorial.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oTutorial.IsLoaded Then
                    With oTutorial
                        .Parent = New DTOBaseGuid(oDrd("Parent"))
                        .Title = oDrd("Title")
                        .Excerpt = SQLHelper.GetStringFromDataReader(oDrd("Excerpt"))
                        .YouTubeId = SQLHelper.GetStringFromDataReader(oDrd("YouTubeId"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                        .Rols = New List(Of DTORol)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Rol")) Then
                    oTutorial.Rols.Add(New DTORol(oDrd("Rol")))
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oTutorial.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTutorial As DTOTutorial, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTutorial, oTrans)
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

    Shared Sub Update(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        UpdateHeader(oTutorial, oTrans)
        UpdateRols(oTutorial, oTrans)
    End Sub

    Shared Sub UpdateHeader(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Tutorial ")
        sb.AppendLine("WHERE Guid='" & oTutorial.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTutorial.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTutorial
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("Title") = .Title
            oRow("Excerpt") = SQLHelper.NullableString(.Excerpt)
            oRow("YouTubeId") = SQLHelper.NullableString(.YouTubeId)
            oRow("Fch") = SQLHelper.NullableFch(.Fch)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateRols(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        DeleteRols(oTutorial, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TutorialRol ")
        sb.AppendLine("WHERE Tutorial='" & oTutorial.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oRol As DTORol In oTutorial.Rols
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Tutorial") = oTutorial.Guid
            oRow("Rol") = oRol.Id
        Next

        oDA.Update(oDs)

    End Sub



    Shared Function Delete(oTutorial As DTOTutorial, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTutorial, oTrans)
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

    Shared Sub Delete(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        DeleteRols(oTutorial, oTrans)
        DeleteHeader(oTutorial, oTrans)
    End Sub

    Shared Sub DeleteRols(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TutorialRol WHERE Tutorial='" & oTutorial.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oTutorial As DTOTutorial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Tutorial WHERE Guid='" & oTutorial.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TutorialsLoader

    Shared Function All(Optional oParent As DTOBaseGuid = Nothing, Optional oRol As DTORol = Nothing) As List(Of DTOTutorial)
        Dim retval As New List(Of DTOTutorial)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tutorial.* ")
        sb.AppendLine("FROM Tutorial ")
        If oRol Is Nothing Then
            If oParent IsNot Nothing Then
                sb.AppendLine("WHERE Tutorial.Parent = '" & oParent.Guid.ToString & "' ")
            End If
        Else
            sb.AppendLine("INNER JOIN TutorialRol ON Tutorial.Guid = TutorialRol.Tutorial ")
            sb.AppendLine("WHERE TutorialRol.Rol = " & oRol.Id & " ")
            If oParent IsNot Nothing Then
                sb.AppendLine("AND Tutorial.Parent = '" & oParent.Guid.ToString & "' ")
            End If
        End If
        sb.AppendLine("ORDER BY Tutorial.Fch")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTutorial(oDrd("Guid"))
            With item
                .Parent = New DTOBaseGuid(oDrd("Parent"))
                .Title = oDrd("Title")
                .Excerpt = SQLHelper.GetStringFromDataReader(oDrd("Excerpt"))
                .YouTubeId = SQLHelper.GetStringFromDataReader(oDrd("YouTubeId"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

