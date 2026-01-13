Public Class EdiversaRemadvLoader

    Shared Function Find(oGuid As Guid) As DTOEdiversaRemadv
        Dim retval As DTOEdiversaRemadv = Nothing
        Dim oEdiversaRemadv As New DTOEdiversaRemadv(oGuid)
        If Load(oEdiversaRemadv) Then
            retval = oEdiversaRemadv
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaRemadv As DTOEdiversaRemadv) As Boolean
        If Not oEdiversaRemadv.IsLoaded Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * FROM EdiRemadvHeader ")
            sb.AppendLine("WHERE Guid='" & oEdiversaRemadv.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Idx ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            oEdiversaRemadv = EdiversaRemadvsLoader.All(oDrd).First
            oDrd.Close()

        End If

        Dim retval As Boolean = oEdiversaRemadv.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaRemadv As DTOEdiversaRemadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oEdiversaRemadv, oTrans)
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


    Shared Sub Update(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        UpdateHeader(oEdiversaRemadv, oTrans)
        UpdateItems(oEdiversaRemadv, oTrans)
    End Sub

    Protected Shared Sub UpdateHeader(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM EdiRemadvHeader WHERE Guid='" & oEdiversaRemadv.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaRemadv.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaRemadv
            oRow("DocNum") = .DocNum
            oRow("FchDoc") = .FchDoc
            oRow("FchVto") = .FchVto
            oRow("DocRef") = .DocRef
            oRow("EmisorPago") = .EmisorPago.Guid
            oRow("ReceptorPago") = .ReceptorPago.Guid
            oRow("Cur") = .Amt.Cur.Tag
            oRow("Amt") = .Amt.Val
            If .Result = Nothing Then
                oRow("Result") = System.DBNull.Value
            Else
                oRow("Result") = .Result
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateItems(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        If Not oEdiversaRemadv.IsNew Then DeleteItems(oEdiversaRemadv, oTrans)

        Dim SQL As String = "SELECT * FROM EdiRemadvItem WHERE Parent='" & oEdiversaRemadv.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim idx As Integer
        For Each oItem As DTOEdiversaRemadvItem In oEdiversaRemadv.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaRemadv.Guid
            oRow("Idx") = idx

            With oItem
                oRow("ItemType") = .Type
                oRow("ItemNom") = .Nom
                oRow("ItemNum") = .Num
                oRow("ItemFch") = .Fch
                oRow("ItemCur") = .Amt.Cur.Tag
                oRow("ItemAmt") = .Amt.Val
                oRow("PndGuid") = SQLHelper.NullableBaseGuid(.Pnd)
            End With

            idx += 1
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Retrocedeix(oEdiversaRemadv As DTOEdiversaRemadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Retrocedeix(oEdiversaRemadv, oTrans)
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

    Shared Sub Retrocedeix(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        'retrocedeix fitxer Edi a pendent de processar
        Dim oEdifile As DTOEdiversaFile = EdiversaFileLoader.FromResultGuid(oEdiversaRemadv.Guid)
        EdiversaFileLoader.SetResult(oEdifile, DTOEdiversaFile.Results.Pending, Nothing, oTrans)

        DeleteItems(oEdiversaRemadv, oTrans)
        DeleteHeader(oEdiversaRemadv, oTrans)
    End Sub

    Shared Function Procesa(oEdiversaFile As DTOEdiversaFile, oEdiversaRemadv As DTOEdiversaRemadv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Procesa(oEdiversaFile, oEdiversaRemadv, oTrans)
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

    Shared Sub Procesa(oEdiversaFile As DTOEdiversaFile, oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        EdiversaFileLoader.SetResult(oEdiversaFile, DTOEdiversaFile.Results.Processed, oEdiversaRemadv, oTrans)
        Update(oEdiversaRemadv, oTrans)
    End Sub

    Protected Shared Sub DeleteItems(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiRemadvItem WHERE Parent=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oEdiversaRemadv.Guid.ToString())
    End Sub

    Protected Shared Sub DeleteHeader(oEdiversaRemadv As DTOEdiversaRemadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiRemadvHeader WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oEdiversaRemadv.Guid.ToString())
    End Sub

End Class

Public Class EdiversaRemadvsLoader

    Shared Function All(Optional IncludeProcessed As Boolean = False) As List(Of DTOEdiversaRemadv)
        Dim SQL As String = "SELECT EdiRemadvHeader.*,EdiRemadvItem.*,CliGral.FullNom FROM EdiRemadvHeader " _
                            & "INNER JOIN EdiRemadvItem ON EdiRemadvHeader.Guid = EdiRemadvItem.Parent " _
                            & "INNER JOIN CliGral ON EdiRemadvHeader.EmisorPago = CliGral.Guid "

        If Not IncludeProcessed Then
            SQL = SQL & "WHERE Result IS NULL "
        End If
        SQL = SQL & "ORDER BY EdiRemadvHeader.FchVto DESC, EdiRemadvHeader.DocNum DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As List(Of DTOEdiversaRemadv) = All(oDrd)
        oDrd.Close()

        Return retval
    End Function

    Shared Function All(ByRef oDrd As SqlDataReader) As List(Of DTOEdiversaRemadv)
        Dim retval As New List(Of DTOEdiversaRemadv)

        Dim oEdiversaRemadv As New DTOEdiversaRemadv(System.Guid.NewGuid)
        Do While oDrd.Read
            If Not oEdiversaRemadv.Guid.Equals(oDrd("Guid")) Then
                oEdiversaRemadv = New DTOEdiversaRemadv(DirectCast(oDrd("Guid"), Guid))
                Dim oEmisorPago As New DTOContact(DirectCast(oDrd("EmisorPago"), Guid))
                oEmisorPago.Nom = oDrd("FullNom")

                With oEdiversaRemadv
                    .DocNum = oDrd("DocNum")
                    .FchDoc = oDrd("FchDoc")
                    .FchVto = oDrd("FchVto")
                    .DocRef = oDrd("DocRef")
                    .EmisorPago = oEmisorPago
                    .ReceptorPago = New DTOContact(DirectCast(oDrd("ReceptorPago"), Guid))
                    '.Amt = DTOAmt.Factory(DTOCur.Factory(oDrd("Cur").ToString()), CDec(oDrd("Amt"))) 'aixó deixa Eur a zero
                    .Amt = DTOAmt.Factory(CDec(oDrd("Amt")))
                    If Not IsDBNull(oDrd("Result")) Then
                        .Result = oDrd("Result")
                    End If
                    .IsLoaded = True
                End With
                retval.Add(oEdiversaRemadv)
            End If

            Dim oItem As New DTOEdiversaRemadvItem(oEdiversaRemadv)
            With oItem
                .Idx = oDrd("Idx")
                .Type = oDrd("ItemType")
                .Nom = oDrd("ItemNom")
                .Num = oDrd("ItemNum")
                .Fch = oDrd("ItemFch")
                '.Amt = DTOAmt.Factory(DTOCur.Factory(oDrd("Cur").ToString()), CDec(oDrd("ItemAmt"))) 'aixó deixa Eur a zero
                .Amt = DTOAmt.Factory(CDec(oDrd("ItemAmt")))
                If Not IsDBNull(oDrd("PndGuid")) Then
                    .Pnd = New DTOPnd(oDrd("PndGuid"))
                End If
            End With
            oEdiversaRemadv.Items.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class


