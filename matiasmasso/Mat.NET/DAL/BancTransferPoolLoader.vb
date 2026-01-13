Public Class BancTransferPoolLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancTransferPool
        Dim retval As DTOBancTransferPool = Nothing
        Dim oBancTransferPool As New DTOBancTransferPool(oGuid)
        If Load(oBancTransferPool) Then
            retval = oBancTransferPool
        End If
        Return retval
    End Function

    Shared Function FromCca(oCca As DTOCca) As DTOBancTransferPool
        Dim retval As DTOBancTransferPool = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid ")
        sb.AppendLine("FROM BancTransferPool ")
        sb.AppendLine("WHERE Cca='" & oCca.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOBancTransferPool(oDrd("Guid"))
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oBancTransferPool As DTOBancTransferPool) As Boolean
        If Not oBancTransferPool.IsLoaded And Not oBancTransferPool.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BancTransferPool.Cca, BancTransferPool.BancEmissor, BancTransferPool.Ref ")
            sb.AppendLine(", Cca.Fch, Cca.Cca AS CcaId, cca.txt ")
            sb.AppendLine(", CliBnc.Abr ")
            sb.AppendLine(", Iban.Ccc, Iban.BankBranch, Bn2.Bank, Bn1.Abr AS BankNom, Bn1.Swift ")
            sb.AppendLine("FROM BancTransferPool ")
            sb.AppendLine("INNER JOIN Cca ON BancTransferPool.Cca=Cca.Guid ")
            sb.AppendLine("INNER JOIN CliBnc ON BancTransferPool.BancEmissor=CliBnc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Iban ON BancTransferPool.BancEmissor=Iban.ContactGuid AND Iban.Cod=" & CInt(DTOIban.Cods.Banc) & " ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch=Bn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
            sb.AppendLine("WHERE BancTransferPool.Guid='" & oBancTransferPool.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBancTransferPool
                    .Cca = New DTOCca(oDrd("Cca"))
                    .Fch = oDrd("Fch")
                    .BancEmissor = New DTOBanc(oDrd("BancEmissor"))
                    With .BancEmissor
                        .Abr = oDrd("Abr")
                        .Iban = New DTOIban
                        .Iban.Digits = oDrd("CCC")
                        If Not IsDBNull(oDrd("BankBranch")) Then
                            .Iban.BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                            If Not IsDBNull(oDrd("Bank")) Then
                                .Iban.BankBranch.Bank = New DTOBank(oDrd("Bank"))
                                .Iban.BankBranch.Bank.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                                .Iban.BankBranch.Bank.Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                            End If
                        End If
                    End With
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    With .Cca
                        .Fch = oDrd("Fch")
                        .Id = oDrd("CcaId")
                        .Concept = oDrd("Txt")
                    End With
                End With
            End If

            oDrd.Close()
            LoadItems(oBancTransferPool)
            oBancTransferPool.IsLoaded = True
        End If

        Dim retval As Boolean = oBancTransferPool.IsLoaded
        Return retval
    End Function

    Shared Sub LoadItems(ByRef oBancTransferPool As DTOBancTransferPool)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BancTransferBeneficiari.* ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.LangId ")
        sb.AppendLine(", (CASE WHEN CliBnc.Abr IS NULL THEN 0 ELSE 1 END) AS IsOurBankAccount ")
        sb.AppendLine(", Bn2.Bank, Bn1.Abr AS BankNom, Bn1.Swift, Bn2.Adr, Bn2.Location AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine("FROM BancTransferBeneficiari ")
        sb.AppendLine("INNER JOIN CliGral ON BancTransferBeneficiari.Contact = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliBnc ON BancTransferBeneficiari.Contact=CliBnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON BancTransferBeneficiari.BankBranch=Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location=Location.Guid ")
        sb.AppendLine("WHERE BancTransferBeneficiari.Pool='" & oBancTransferPool.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        oBancTransferPool.Beneficiaris = New List(Of DTOBancTransferBeneficiari)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBancTransferBeneficiari(oDrd("Guid"))
            With item
                .Parent = oBancTransferPool
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
                .IsOurBankAccount = oDrd("IsOurBankAccount")
                .IsLoaded = True
            End With
            oBancTransferPool.Beneficiaris.Add(item)
        Loop

        oDrd.Close()

    End Sub

    Shared Function Update(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBancTransferPool, oTrans)
            oTrans.Commit()
            oBancTransferPool.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        If oBancTransferPool.Pnds.Count > 0 Then
            CcaLoader.SavePagament(oBancTransferPool.Cca, oBancTransferPool.Pnds, oTrans)
        Else
            CcaLoader.Update(oBancTransferPool.Cca, oTrans)
        End If
        UpdateHeader(oBancTransferPool, oTrans)
        UpdateItems(oBancTransferPool, oTrans)
    End Sub

    Shared Sub UpdateHeader(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancTransferPool ")
        sb.AppendLine("WHERE Guid='" & oBancTransferPool.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBancTransferPool.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBancTransferPool
            oRow("Cca") = .Cca.Guid
            oRow("BancEmissor") = .BancEmissor.Guid
            oRow("Ref") = SQLHelper.NullableString(.Ref)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        If Not oBancTransferPool.IsNew Then DeleteItems(oBancTransferPool, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancTransferBeneficiari ")
        sb.AppendLine("WHERE Pool='" & oBancTransferPool.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oBeneficiari As DTOBancTransferBeneficiari In oBancTransferPool.Beneficiaris
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With oBeneficiari
                oRow("Guid") = .Guid
                oRow("Pool") = oBancTransferPool.Guid
                oRow("Contact") = .Contact.Guid
                oRow("Eur") = .Amt.Eur
                oRow("Cur") = .Amt.Cur.Tag.ToString
                oRow("Val") = .Amt.Val
                oRow("BankBranch") = .BankBranch.Guid
                oRow("Account") = .Account
                oRow("Concepte") = .Concepte
            End With
        Next
        oDA.Update(oDs)
    End Sub

    Shared Function SaveRef(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim SQL As String = "UPDATE BancTransferPool SET Ref='" & oBancTransferPool.Ref & "' WHERE Guid='" & oBancTransferPool.Guid.ToString & "' "
        Try
            SQLHelper.ExecuteNonQuery(SQL, exs)
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function Delete(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancTransferPool, oTrans)
            CcaLoader.Delete(oBancTransferPool.Cca, oTrans)
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


    Shared Sub Delete(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        DeleteItems(oBancTransferPool, oTrans)
        DeleteHeader(oBancTransferPool, oTrans)
        CcaLoader.Delete(oBancTransferPool.Cca, oTrans)
    End Sub

    Shared Sub DeleteHeader(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancTransferPool WHERE Guid='" & oBancTransferPool.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oBancTransferPool As DTOBancTransferPool, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancTransferBeneficiari WHERE Pool='" & oBancTransferPool.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


End Class

Public Class BancTransferPoolsLoader

    Shared Function All(oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing) As List(Of DTOBancTransferPool)
        Dim retval As New List(Of DTOBancTransferPool)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BancTransferPool.Guid, BancTransferPool.Cca, BancTransferPool.BancEmissor, BancTransferPool.Ref ")
        sb.AppendLine(", Cca.Fch ")
        sb.AppendLine(", CliBnc.Abr ")
        sb.AppendLine(", BancTransferBeneficiari.Contact, CliGral.RaoSocial, CliGral.LangId, BancTransferBeneficiari.Concepte ")
        sb.AppendLine(", BancTransferBeneficiari.Guid AS BeneficiariGuid, BancTransferBeneficiari.Eur, BancTransferBeneficiari.Cur , BancTransferBeneficiari.Val ")
        sb.AppendLine(", BancTransferBeneficiari.BankBranch, Bn2.Bank, Bn2.Adr, Bn2.Location, Bn1.Abr, Bn1.Swift ")
        sb.AppendLine("FROM BancTransferPool ")
        sb.AppendLine("INNER JOIN Cca ON BancTransferPool.Cca=Cca.Guid ")
        sb.AppendLine("INNER JOIN CliBnc ON BancTransferPool.BancEmissor=CliBnc.Guid ")
        sb.AppendLine("INNER JOIN BancTransferBeneficiari ON BancTransferPool.Guid=BancTransferBeneficiari.Pool ")
        sb.AppendLine("INNER JOIN Bn2 ON BancTransferBeneficiari.BankBranch=Bn2.Guid ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON BancTransferBeneficiari.Contact=CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp =" & oEmp.Id & " ")
        If oBanc IsNot Nothing Then
            sb.AppendLine("AND BancTransferPool.BancEmissor='" & oBanc.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Cca.Fch DESC, CliBnc.Abr ")

        Dim oPool As New DTOBancTransferPool()
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPool.Guid.Equals(oDrd("Guid")) Then
                oPool = New DTOBancTransferPool(oDrd("Guid"))
                With oPool
                    .Cca = New DTOCca(oDrd("Cca"))
                    .Cca.fch = oDrd("Fch")
                    .BancEmissor = New DTOBanc(oDrd("BancEmissor"))
                    With .BancEmissor
                        .abr = oDrd("Abr")
                    End With
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .Beneficiaris = New List(Of DTOBancTransferBeneficiari)
                End With
                retval.Add(oPool)
            End If

            Dim item As New DTOBancTransferBeneficiari(oDrd("BeneficiariGuid"))
            With item
                .Parent = oPool
                .Contact = New DTOContact(oDrd("Contact"))
                With .Contact
                    .nom = oDrd("RaoSocial")
                    .lang = DTOLang.Factory(oDrd("LangId").ToString())
                End With
                .Amt = DTOAmt.Factory(oDrd("Eur"), oDrd("Cur").ToString, oDrd("Val"))
                .Concepte = SQLHelper.GetStringFromDataReader(oDrd("Concepte"))
                If Not IsDBNull(oDrd("Bankbranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("Bankbranch"))
                    With .BankBranch
                        .address = oDrd("Adr")
                        If Not IsDBNull(oDrd("Location")) Then
                            .location = New DTOLocation(oDrd("Location"))
                        End If
                        If Not IsDBNull(oDrd("Bank")) Then
                            .bank = New DTOBank(oDrd("Bank"))
                            With .bank
                                .nomComercial = oDrd("Abr")
                                .swift = oDrd("Swift")
                            End With
                        End If
                    End With
                End If
            End With
            oPool.Beneficiaris.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class


