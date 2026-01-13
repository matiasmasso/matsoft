Public Class InsolvenciaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOInsolvencia
        Dim retval As DTOInsolvencia = Nothing
        Dim oInsolvencia As New DTOInsolvencia(oGuid)
        If Load(oInsolvencia) Then
            retval = oInsolvencia
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oInsolvencia As DTOInsolvencia) As Boolean
        If Not oInsolvencia.IsLoaded And Not oInsolvencia.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Insolvencias.Guid, Insolvencias.Fch, Insolvencias.Nominal ")
            sb.AppendLine(",Insolvencias.Customer, CliGral.FullNom ")
            sb.AppendLine("FROM Insolvencias ")
            sb.AppendLine("INNER JOIN CliGral ON Insolvencias.Customer = CliGral.Guid ")
            sb.AppendLine("WHERE Insolvencias.Guid='" & oInsolvencia.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oInsolvencia
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .Nominal = SQLHelper.GetAmtFromDataReader(oDrd("Nominal"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oInsolvencia.IsLoaded
        Return retval
    End Function

    Shared Function Update(oInsolvencia As DTOInsolvencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oInsolvencia, oTrans)
            oTrans.Commit()
            oInsolvencia.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oInsolvencia As DTOInsolvencia, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Insolvencias ")
        sb.AppendLine("WHERE Guid='" & oInsolvencia.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oInsolvencia.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oInsolvencia
            oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("Fch") = SQLHelper.NullableFch(.Fch)
            oRow("Nominal") = SQLHelper.NullableAmt(.Nominal)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oInsolvencia As DTOInsolvencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oInsolvencia, oTrans)
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


    Shared Sub Delete(oInsolvencia As DTOInsolvencia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Insolvencias WHERE Guid='" & oInsolvencia.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function IsInsolvent(oContact As DTOContact) As Boolean
        Dim SQL As String = "SELECT Guid FROM Insolvencias WHERE Customer='" & oContact.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function
#End Region

End Class

Public Class InsolvenciasLoader

    Shared Function All() As List(Of DTOInsolvencia)
        Dim retval As New List(Of DTOInsolvencia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Insolvencias.Guid,Insolvencias.Fch,Insolvencias.Nominal ")
        sb.AppendLine(",Insolvencias.Customer, CliGral.FullNom ")
        sb.AppendLine("FROM Insolvencias ")
        sb.AppendLine("INNER JOIN CliGral ON Insolvencias.Customer = CliGral.Guid ")
        sb.AppendLine("ORDER BY Insolvencias.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOInsolvencia(oDrd("Guid"))
            With item
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Nominal = SQLHelper.GetAmtFromDataReader(oDrd("Nominal"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
