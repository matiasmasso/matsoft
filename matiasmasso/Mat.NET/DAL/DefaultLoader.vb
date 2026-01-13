Public Class DefaultLoader

    Shared Function Find(oCod As DTODefault.Codis, Optional oEmp As DTOEmp = Nothing) As DTODefault
        Dim retval As DTODefault = Nothing
        Dim oDefault As New DTODefault
        oDefault.Cod = oCod
        oDefault.Emp = oEmp
        If Load(oDefault) Then
            retval = oDefault
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oDefault As DTODefault) As Boolean
        If Not oDefault.IsLoaded And Not oDefault.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Value ")
            sb.AppendLine("FROM [Default] ")
            sb.AppendLine("WHERE Cod=" & CInt(oDefault.Cod) & " ")
            If oDefault.Emp Is Nothing Then
                sb.AppendLine("AND Emp=0 ")
            Else
                sb.AppendLine("AND Emp=" & CInt(oDefault.Emp.Id) & " ")
            End If

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oDefault
                    .Value = oDrd("Value")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oDefault.IsLoaded
        Return retval
    End Function

    Shared Function Update(oDefault As DTODefault, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDefault, oTrans)
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

    Shared Sub Update(oDefault As DTODefault, ByRef oTrans As SqlTransaction)
        Dim EmpId As Integer = 0
        If oDefault.Emp IsNot Nothing Then
            EmpId = oDefault.Emp.Id
        End If

        Dim SQL As String = "SELECT * FROM [Default] WHERE Cod=@Cod AND Emp=@Emp"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Cod", oDefault.Cod, "@Emp", EmpId)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Cod") = oDefault.Cod
            oRow("Emp") = EmpId
        Else
            oRow = oTb.Rows(0)
        End If

        With oDefault
            oRow("Value") = oDefault.Value
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oDefault As DTODefault, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDefault, oTrans)
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


    Shared Sub Delete(oDefault As DTODefault, ByRef oTrans As SqlTransaction)
        Dim EmpId As Integer = 0
        If oDefault.Emp IsNot Nothing Then
            EmpId = oDefault.Emp.Id
        End If

        Dim SQL As String = "DELETE [Default] WHERE Cod=@Cod AND Emp=@Emp"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Cod", oDefault.Cod, "@Emp", EmpId)
    End Sub

    Shared Function EmpAmt(oCod As DTODefault.Codis, oEmp As DTOEmp) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim oDefault As DTODefault = Find(oCod, oEmp)
        If oDefault IsNot Nothing Then
            Dim src As String = oDefault.Value
            If IsNumeric(src) Then
                retval = DTOAmt.Factory(CDec(src))
            Else
                retval = DTOAmt.Factory(0)
            End If
        End If
        Return retval
    End Function

    Shared Function EmpDecimal(oCod As DTODefault.Codis, oEmp As DTOEmp) As Decimal
        Dim retval As Decimal = 0
        Dim oDefault As DTODefault = Find(oCod, oEmp)
        If oDefault IsNot Nothing Then
            Dim src As String = oDefault.Value
            If IsNumeric(src) Then
                retval = oDefault.Value
            End If
        End If
        Return retval
    End Function

    Shared Function EmpInteger(oCod As DTODefault.Codis, oEmp As DTOEmp) As Integer
        Dim retval As Integer = 0
        Dim oDefault As DTODefault = Find(oCod, oEmp)
        If oDefault IsNot Nothing Then
            Dim src As String = oDefault.Value
            If IsNumeric(src) Then
                retval = oDefault.Value
            End If
        End If
        Return retval
    End Function

    Shared Function EmpGuid(oCod As DTODefault.Codis, oEmp As DTOEmp) As Guid
        Dim retval As Guid = Guid.Empty
        Dim oDefault As DTODefault = Find(oCod, oEmp)
        If oDefault IsNot Nothing Then
            Dim src As String = oDefault.Value
            If GuidHelper.IsGuid(src) Then
                retval = GuidHelper.GetGuid(src)
            End If
        End If
        Return retval
    End Function
End Class


Public Class DefaultsLoader

    Shared Function All(Optional oEmp As DTOEmp = Nothing) As List(Of DTODefault)
        Dim retval As New List(Of DTODefault)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cod, Value ")
        sb.AppendLine("FROM [Default] ")
        If oEmp Is Nothing Then
            sb.AppendLine("WHERE Emp=0 ")
        Else
            sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODefault()
            With item
                .Cod = oDrd("Cod")
                .Value = oDrd("Value")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function
End Class