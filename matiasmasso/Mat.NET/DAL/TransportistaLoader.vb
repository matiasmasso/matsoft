Public Class TransportistaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTransportista
        Dim retval As DTOTransportista = Nothing
        Dim oTransportista As New DTOTransportista(oGuid)
        If Load(oTransportista) Then
            retval = oTransportista
        End If
        Return retval
    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim SQL As String = "SELECT Guid FROM Trp WHERE Guid='" & oContact.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oTransportista As DTOTransportista) As Boolean
        If Not oTransportista.IsLoaded And Not oTransportista.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Trp ")
            sb.AppendLine("WHERE Guid='" & oTransportista.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTransportista
                    .Abr = oDrd("Abr")
                    .Cubicaje = oDrd("Cubicaje")
                    .TrackingUrlTemplate = SQLHelper.GetStringFromDataReader(oDrd("TrackingUrlTemplate"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTransportista.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTransportista As DTOTransportista, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTransportista, oTrans)
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


    Shared Sub Update(oTransportista As DTOTransportista, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Trp ")
        sb.AppendLine("WHERE Guid='" & oTransportista.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTransportista.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTransportista
            oRow("Abr") = .Abr
            oRow("Cubicaje") = .Cubicaje
            oRow("TrackingUrlTemplate") = SQLHelper.NullableString(.TrackingUrlTemplate)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTransportista As DTOTransportista, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTransportista, oTrans)
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


    Shared Sub Delete(oTransportista As DTOTransportista, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Trp WHERE Guid='" & oTransportista.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TransportistasLoader

    Shared Function All(oEmp as DTOEmp, Optional BlOnlyActive As Boolean = False) As List(Of DTOTransportista)
        Dim retval As New List(Of DTOTransportista)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Trp.Guid, Trp.Abr, CliGral.FullNom ")
        sb.AppendLine("FROM Trp ")
        sb.AppendLine("INNER JOIN CliGral ON Trp.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Trp.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTransportista(oDrd("Guid"))
            With item
                .Abr = IIf(oDrd("Abr") = "", oDrd("FullNom"), oDrd("Abr"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

End Class
