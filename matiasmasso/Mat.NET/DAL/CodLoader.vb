Public Class CodLoader

    Shared Function Find(oGuid As Guid) As DTOCod
        Dim retval As DTOCod = Nothing
        Dim oCod As New DTOCod(oGuid)
        If Load(oCod) Then
            retval = oCod
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCod As DTOCod) As Boolean
        If Not oCod.IsLoaded And Not oCod.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Cod.Parent, Cod.Id, Cod.FchCreated, Cod.UsrCreated, UsrCreated.Nom AS UsrCreatedNickname ")
            sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
            sb.AppendLine("FROM Cod ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText ON Cod.Guid = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.Cods & " ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrCreated ON Cod.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("WHERE Cod.Guid='" & oCod.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCod
                    If Not IsDBNull(oDrd("Parent")) Then
                        .Parent = New DTOCod(oDrd("Parent"))
                    End If
                    .Id = oDrd("Id")
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd)
                    .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCod.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCod As DTOCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCod, oTrans)
            LangTextLoader.Update(oCod.Nom, oTrans)
            oTrans.Commit()
            oCod.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oCod As DTOCod, ByRef oTrans As SqlTransaction)
        If oCod.IsNew And oCod.Id = 0 Then oCod.Id = LastId(oCod, oTrans) + 1

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Cod ")
        sb.AppendLine("WHERE Guid='" & oCod.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCod.Guid
            oRow("Ord") = oCod.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oCod
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("Id") = oCod.Id
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrLog.UsrCreated)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCod As DTOCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCod, oTrans)
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


    Shared Sub Delete(oCod As DTOCod, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cod WHERE Guid='" & oCod.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LastId(oCod As DTOCod, oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Id) AS LastId ")
        sb.AppendLine("FROM Cod ")
        If oCod.Parent Is Nothing Then
            sb.AppendLine("WHERE Parent IS NULL ")
        Else
            sb.AppendLine("WHERE Parent='" & oCod.Parent.Guid.ToString & "' ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count <> 0 Then
            Dim oRow = oTb.Rows(0)
            retval = SQLHelper.GetIntegerFromDataReader(oRow("LastId"))
        End If
        Return retval
    End Function

End Class

Public Class CodsLoader

    Shared Function All(oParent As DTOCod) As DTOCod.Collection
        Dim retval As New DTOCod.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cod.Guid, Cod.Id, Cod.FchCreated, Cod.UsrCreated, UsrCreated.Nom AS UsrCreatedNickname ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine("FROM Cod ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON Cod.Guid = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.Cods & " ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrCreated ON Cod.UsrCreated = UsrCreated.Guid ")
        If oParent Is Nothing Then
            sb.AppendLine("WHERE Cod.Parent IS NULL ")
        Else
            sb.AppendLine("WHERE Cod.Parent='" & oParent.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Cod.Id ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim item As DTOCod = Nothing
        Do While oDrd.Read

            If oParent Is Nothing Then
                item = New DTOCod.Root(oDrd("Guid"))
            Else
                item = New DTOCod(oDrd("Guid"))
                item.Parent = oParent
            End If

            With item
                .Id = oDrd("Id")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd)
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Sort(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next
        sb.AppendLine()
        sb.AppendLine("UPDATE Cod SET Cod.Ord = X.Idx ")
        sb.AppendLine("FROM Cod ")
        sb.AppendLine("INNER JOIN @Table X ON Cod.Guid = X.Guid ")
        Dim SQL = sb.ToString()
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function


End Class
