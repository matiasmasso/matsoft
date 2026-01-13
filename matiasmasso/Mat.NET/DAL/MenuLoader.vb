Public Class MenuLoader

    Shared Function Find(oGuid As Guid) As DTOMenu
        Dim retval As DTOMenu = Nothing
        Dim oMenu As New DTOMenu(oGuid)
        If Load(oMenu) Then
            retval = oMenu
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oMenu As DTOMenu) As Boolean
        If Not oMenu.IsLoaded And Not oMenu.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Menu ")
            sb.AppendLine("LEFT OUTER JOIN MenuRol ON Menu.Guid = MenuRol.Menu ")
            sb.AppendLine("WHERE Menu.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oMenu.Guid.ToString)
            If oDrd.Read Then
                If Not oMenu.IsLoaded Then
                    With oMenu
                        .App = oDrd("App")
                        If Not IsDBNull(oDrd("Parent")) Then
                            .Parent = New DTOMenu(oDrd("Parent"))
                        End If
                        .Ord = oDrd("Ord")
                        .NomEsp = oDrd("NomEsp")
                        .NomCat = oDrd("NomCat")
                        .NomEng = oDrd("NomEng")
                        If Not IsDBNull(oDrd("Action")) Then
                            .Action = oDrd("Action")
                        End If
                        .Items = New List(Of DTOMenu)
                        .Rols = New List(Of DTORol)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Rol")) Then
                    Dim oRol As New DTORol(oDrd("Rol"))
                    oMenu.Rols.Add(oRol)
                End If
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oMenu.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMenu As DTOMenu, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMenu, oTrans)
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

    Shared Sub Update(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        UpdateHeader(oMenu, oTrans)
        UpdateRols(oMenu, oTrans)
    End Sub

    Shared Sub UpdateHeader(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Menu WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oMenu.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMenu.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMenu
            oRow("App") = .App
            oRow("NomEsp") = .NomEsp
            oRow("NomCat") = .NomCat
            oRow("NomEng") = .NomEng
            oRow("Ord") = .Ord
            If .Action = "" Then
                oRow("Action") = System.DBNull.Value
            Else
                oRow("Action") = .Action
            End If
            If .Parent Is Nothing Then
                oRow("Parent") = System.DBNull.Value
            Else
                oRow("Parent") = .Parent.Guid
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateRols(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        If Not oMenu.IsNew Then DeleteRols(oMenu, oTrans)

        Dim SQL As String = "SELECT * FROM MenuRol WHERE Menu=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oMenu.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRol As DTORol In oMenu.Rols
            Dim oRow As DataRow = oTb.NewRow
            oRow("Menu") = oMenu.Guid
            oRow("Rol") = oRol.Id
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oMenu As DTOMenu, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMenu, oTrans)
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


    Shared Sub Delete(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        DeleteRols(oMenu, oTrans)
        DeleteHeader(oMenu, oTrans)
    End Sub

    Shared Sub DeleteRols(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE MenuRol WHERE Menu=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oMenu.Guid.ToString)
    End Sub

    Shared Sub DeleteHeader(oMenu As DTOMenu, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Menu WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oMenu.Guid.ToString)
    End Sub

End Class

Public Class MenusLoader

    Shared Function FromApp(oAppType As DTOApp.AppTypes) As List(Of DTOMenu)
        Dim retval As New List(Of DTOMenu)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Menu ")
        sb.AppendLine("LEFT OUTER JOIN MenuRol ON Menu.Guid = MenuRol.Menu ")
        sb.AppendLine("WHERE Menu.App=@App")
        sb.AppendLine("ORDER BY Parent, Ord")

        Dim oMenu As New DTOMenu
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@App", CInt(oAppType))
        Do While oDrd.Read
            If Not oMenu.Guid.Equals(oDrd("Guid")) Then
                oMenu = New DTOMenu(oDrd("Guid"))
                With oMenu
                    .App = oAppType
                    If Not IsDBNull(oDrd("Parent")) Then
                        .Parent = New DTOMenu(oDrd("Parent"))
                    End If
                    .Ord = oDrd("Ord")
                    .NomEsp = oDrd("NomEsp")
                    .NomCat = oDrd("NomCat")
                    .NomEng = oDrd("NomEng")
                    If Not IsDBNull(oDrd("Action")) Then
                        .Action = oDrd("Action")
                    End If
                    .Items = New List(Of DTOMenu)
                    .Rols = New List(Of DTORol)
                    .IsLoaded = True
                End With
                retval.Add(oMenu)
            End If

            If Not IsDBNull(oDrd("Rol")) Then
                Dim oRol As New DTORol(oDrd("Rol"))
                oMenu.Rols.Add(oRol)
            End If

        Loop
        oDrd.Close()

        Return retval
    End Function
End Class

