Public Class BancSdoLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancSdo
        Dim retval As DTOBancSdo = Nothing
        Dim oBancSdo As New DTOBancSdo(oGuid)
        If Load(oBancSdo) Then
            retval = oBancSdo
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBancSdo As DTOBancSdo) As Boolean
        If Not oBancSdo.IsLoaded And Not oBancSdo.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BancSdo.Banc, CliBnc.Abr, BancSdo.Fch, BancSdo.Sdo ")
            sb.AppendLine("FROM BancSdo ")
            sb.AppendLine("INNER JOIN CliBnc ON BancSdo.Banc=CliBnc.Guid ")
            sb.AppendLine("WHERE BancSdo.Guid='" & oBancSdo.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBancSdo
                    .Banc = New DTOBanc(oDrd("Banc"))
                    .Banc.Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    .Fch = oDrd("Fch")
                    .Saldo = SQLHelper.GetAmtFromDataReader(oDrd("Sdo"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBancSdo.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBancSdo As DTOBancSdo, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBancSdo, oTrans)
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


    Shared Sub Update(oBancSdo As DTOBancSdo, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancSdo ")
        sb.AppendLine("WHERE Guid='" & oBancSdo.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBancSdo.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBancSdo
            oRow("Banc") = .Banc.Guid
            oRow("Fch") = .Fch
            oRow("Sdo") = .Saldo.Eur
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBancSdo As DTOBancSdo, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancSdo, oTrans)
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


    Shared Sub Delete(oBancSdo As DTOBancSdo, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancSdo WHERE Guid='" & oBancSdo.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class BancSdosLoader

    Shared Function Last(oEmp As DTOEmp) As List(Of DTOBancSdo)
        Dim retval As New List(Of DTOBancSdo)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BS3.Guid, CliBnc.Guid AS Banc, CliBnc.Abr, BS3.Fch, BS3.Sdo ")
        sb.AppendLine(", VwIban.* ")
        sb.AppendLine("FROM CliBnc ")
        sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwIban ON CliBnc.Guid = VwIban.IbanContactGuid AND VwIban.IbanCod =" & DTOIban.Cods.banc & " ")
        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("         SELECT BS1.Guid, BS1.Banc, BS1.Fch, BS1.Sdo ")
        sb.AppendLine("         FROM BancSdo BS1 ")
        sb.AppendLine("         INNER JOIN ( ")
        sb.AppendLine("                 SELECT BancSdo.Banc, MAX(BancSdo.Fch) AS LastFch ")
        sb.AppendLine("                 FROM BancSdo ")
        sb.AppendLine("                 GROUP BY BancSdo.Banc ")
        sb.AppendLine("                    ) BS2 ON BS1.Banc = BS2.Banc AND BS1.Fch=BS2.LastFch ")
        sb.AppendLine("     ) BS3 ON CliBnc.Guid = BS3.Banc ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " AND CliGral.Obsoleto=0 ")
        sb.AppendLine("ORDER BY CliBnc.Abr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim item As DTOBancSdo = Nothing
        Do While oDrd.Read
            If IsDBNull(oDrd("Guid")) Then
                item = New DTOBancSdo()
            Else
                item = New DTOBancSdo(oDrd("Guid"))
                item.Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                item.Saldo = SQLHelper.GetAmtFromDataReader(oDrd("Sdo"))
            End If
            With item
                .Banc = New DTOBanc(oDrd("Banc"))
                .banc.abr = oDrd("Abr")
                .banc.iban = SQLHelper.getIbanFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

