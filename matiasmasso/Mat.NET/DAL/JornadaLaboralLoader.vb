Public Class JornadaLaboralLoader
    Shared Function Find(oGuid As Guid) As DTOJornadaLaboral
        Dim retval As DTOJornadaLaboral = Nothing
        Dim oJornadaLaboral As New DTOJornadaLaboral(oGuid)
        If Load(oJornadaLaboral) Then
            retval = oJornadaLaboral
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oJornadaLaboral As DTOJornadaLaboral) As Boolean
        If Not oJornadaLaboral.IsLoaded And Not oJornadaLaboral.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT JornadaLaboral.Staff, JornadaLaboral.FchFrom, JornadaLaboral.FchTo, CliStaff.Abr ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine("FROM JornadaLaboral ")
            sb.AppendLine("left outer join cLIsTAFF ON JornadaLaboral.Staff = CliStaff.Guid  ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON JornadaLaboral.Staff = CliGral.Guid ")
            sb.AppendLine("WHERE JornadaLaboral.Guid='" & oJornadaLaboral.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oJornadaLaboral
                    .Staff = New DTOStaff(oDrd("Staff"))
                    .Staff.FullNom = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oJornadaLaboral.IsLoaded
        Return retval
    End Function

    Shared Function Update(oJornadaLaboral As DTOJornadaLaboral, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oJornadaLaboral, oTrans)
            oTrans.Commit()
            oJornadaLaboral.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oJornadaLaboral As DTOJornadaLaboral, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM JornadaLaboral ")
        sb.AppendLine("WHERE Guid='" & oJornadaLaboral.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oJornadaLaboral.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oJornadaLaboral
            oRow("Staff") = .Staff.Guid
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oJornadaLaboral As DTOJornadaLaboral, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oJornadaLaboral, oTrans)
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


    Shared Sub Delete(oJornadaLaboral As DTOJornadaLaboral, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE JornadaLaboral WHERE Guid='" & oJornadaLaboral.Guid.ToString & "' "
        Dim rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class JornadesLaboralsLoader

    Shared Function All(Optional oStaff As DTOStaff = Nothing) As Models.JornadesLaboralsModel
        Dim retval As New Models.JornadesLaboralsModel
        Dim staff As New Models.JornadesLaboralsModel.Staff
        If oStaff IsNot Nothing Then
            staff.Guid = oStaff.Guid
            staff.Abr = oStaff.Abr
            staff.Nom = oStaff.Nom
            retval.Staffs.Add(staff)
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT JornadaLaboral.Guid, JornadaLaboral.Staff ")
        sb.AppendLine(", JornadaLaboral.FchFrom, JornadaLaboral.FchTo ")
        sb.AppendLine(", CliGral.RaoSocial, CliStaff.Abr ")
        sb.AppendLine("FROM JornadaLaboral ")
        sb.AppendLine("INNER JOIN CliGral ON JornadaLaboral.Staff = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliStaff ON JornadaLaboral.Staff = CliStaff.Guid ")
        If oStaff IsNot Nothing Then
            sb.AppendLine("WHERE JornadaLaboral.Staff = '" & oStaff.Guid.ToString() & "' ")
        End If
        sb.AppendLine("ORDER BY JornadaLaboral.Staff, CliGral.RaoSocial, JornadaLaboral.FchFrom DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not staff.Guid.Equals(oDrd("Staff")) Then
                staff = New Models.JornadesLaboralsModel.Staff(oDrd("Staff"))
                staff.Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                staff.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                retval.Staffs.Add(staff)
            End If
            Dim item As New Models.JornadesLaboralsModel.Item
            With item
                .Guid = oDrd("Guid")
                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            staff.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Last(oStaff As DTOStaff) As DTOJornadaLaboral
        Dim retval As DTOJornadaLaboral = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 * ")
        sb.AppendLine("FROM JornadaLaboral ")
        sb.AppendLine("WHERE JornadaLaboral.Staff = '" & oStaff.Guid.ToString() & "' ")
        sb.AppendLine("ORDER BY JornadaLaboral.FchFrom DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOJornadaLaboral(oDrd("Guid"))
            With retval
                .Staff = New DTOStaff(oDrd("Staff"))
                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

End Class
