Public Class BancTransferBeneficiariLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancTransferBeneficiari
        Dim retval As DTOBancTransferBeneficiari = Nothing
        Dim oBancTransferBeneficiari As New DTOBancTransferBeneficiari(oGuid)
        If Load(oBancTransferBeneficiari) Then
            retval = oBancTransferBeneficiari
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBancTransferBeneficiari As DTOBancTransferBeneficiari) As Boolean
        If Not oBancTransferBeneficiari.IsLoaded And Not oBancTransferBeneficiari.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BancTransferBeneficiari.* ")
            sb.AppendLine(", CliGral.RaoSocial, CliGral.LangId ")
            sb.AppendLine(", Bn2.Bank, Bn1.Abr AS BankNom, Bn1.Swift, Bn2.Adr, Bn2.Location AS LocationGuid, Location.Nom AS LocationNom ")
            sb.AppendLine("FROM BancTransferBeneficiari ")
            sb.AppendLine("INNER JOIN BancTransferPool ON BancTransferBeneficiari.Pool=BancTransferPool.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON BancTransferBeneficiari.Contact=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 ON BancTransferBeneficiari.BankBranch=Bn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location=Location.Guid ")
            sb.AppendLine("WHERE BancTransferBeneficiari.Guid='" & oBancTransferBeneficiari.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBancTransferBeneficiari
                    .Parent = New DTOBancTransferPool(oDrd("Pool"))
                    .Contact = New DTOContact(oDrd("Contact"))
                    With .Contact
                        .Nom = oDrd("RaoSocial")
                        .Lang = DTOLang.Factory(oDrd("LangId").ToString())
                    End With
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        If Not IsDBNull(oDrd("LocationGuid")) Then
                            .Location = New DTOLocation(oDrd("LocationGuid"))
                            .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                        End If
                        If Not IsDBNull(oDrd("Bank")) Then
                            .Bank = New DTOBank(oDrd("Bank"))
                            .Bank.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                            .Bank.Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                        End If
                    End With
                    .Account = oDrd("Account")
                    .Concepte = oDrd("Concepte")
                    .Amt = DTOAmt.Factory(oDrd("Eur"), oDrd("Cur").ToString, oDrd("Val"))
                    '.IsOurBankAccount = oDrd("IsOurBankAccount")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBancTransferBeneficiari.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBancTransferBeneficiari As DTOBancTransferBeneficiari, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBancTransferBeneficiari, oTrans)
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


    Shared Sub Update(oBancTransferBeneficiari As DTOBancTransferBeneficiari, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancTransferBeneficiari ")
        sb.AppendLine("WHERE Guid='" & oBancTransferBeneficiari.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBancTransferBeneficiari.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBancTransferBeneficiari
            ' oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBancTransferBeneficiari As DTOBancTransferBeneficiari, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancTransferBeneficiari, oTrans)
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


    Shared Sub Delete(oBancTransferBeneficiari As DTOBancTransferBeneficiari, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancTransferBeneficiari WHERE Guid='" & oBancTransferBeneficiari.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class BancTransferBeneficiarisLoader

    Shared Function All() As List(Of DTOBancTransferBeneficiari)
        Dim retval As New List(Of DTOBancTransferBeneficiari)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancTransferBeneficiari ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBancTransferBeneficiari(oDrd("Guid"))
            With item
                '.Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oBancTransferBeneficiaris As List(Of DTOBancTransferBeneficiari), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTransferBeneficiarisLoader.Delete(oBancTransferBeneficiaris, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE BancTransferBeneficiari ")
        sb.AppendLine("WHERE (")
        For Each item As DTOBancTransferBeneficiari In oBancTransferBeneficiaris
            If item.UnEquals(oBancTransferBeneficiaris.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class
