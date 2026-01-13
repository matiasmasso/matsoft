Public Class RolLoader


#Region "CRUD"

    Shared Function Find(id As DTORol.Ids) As DTORol
        Dim retval As DTORol = Nothing
        Dim oRol As New DTORol(id)
        If Load(oRol) Then
            retval = oRol
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRol As DTORol) As Boolean
        If Not oRol.IsLoaded And Not oRol.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM UsrRols ")
            sb.AppendLine("WHERE Rol =" & CInt(oRol.Id) & " ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRol
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom", "Nom_Cat", "Nom_Eng", "Nom_Por")
                    .Dsc = Defaults.StringOrEmpty(oDrd("Dsc"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRol.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRol As DTORol, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRol, oTrans)
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


    Shared Sub Update(oRol As DTORol, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM UsrRols ")
        sb.AppendLine("WHERE Rol=" & CInt(oRol.Id) & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Rol") = oRol.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oRol
            oRow("Nom") = SQLHelper.NullableLangText(.Nom, DTOLang.ESP)
            oRow("Nom_Cat") = SQLHelper.NullableLangText(.Nom, DTOLang.CAT)
            oRow("Nom_Eng") = SQLHelper.NullableLangText(.Nom, DTOLang.ENG)
            oRow("Nom_Por") = SQLHelper.NullableLangText(.Nom, DTOLang.POR)
            oRow("Dsc") = .Dsc
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRol As DTORol, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRol, oTrans)
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


    Shared Sub Delete(oRol As DTORol, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Rol WHERE Rol=" & CInt(oRol.Id) & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region
End Class

Public Class RolsLoader

    Shared Function All() As List(Of DTORol)
        Dim retval As New List(Of DTORol)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM UsrRols ")
        sb.AppendLine("ORDER BY Rol ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORol(oDrd("Rol"))
            With item
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom", "Nom_Cat", "Nom_Eng", "Nom_Por")
                .Dsc = Defaults.StringOrEmpty(oDrd("Dsc"))
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

