Public Class BancTransferLoaderOld

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancTransferOld
        Dim retval As DTOCca = Nothing
        Dim oBancTransfer As New DTOBancTransferOld(oGuid)
        If Load(oBancTransfer) Then
            retval = oBancTransfer
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBancTransfer As DTOBancTransferOld) As Boolean
        If Not oBancTransfer.IsLoaded And Not oBancTransfer.IsNew Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Cca.Emp, Cca.Txt, Cca.Fch, Cca.Cca, Cca.AuxCca, Cca.Ccd, Cca.Cdn, Cca.Hash ")
            sb.AppendLine(", Ccb.CcaGuid, Ccb.PgcPlan, Ccb.CtaGuid ")
            sb.AppendLine(", PgcCta.Act, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, PgcCta.Cod AS CtaCod ")
            sb.AppendLine(", PgcCta.IsBaseImponibleIVA, PgcCta.IsQuotaIVA ")
            sb.AppendLine(", Ccb.ContactGuid, Ccb.Cli, Clx.Clx, Ccb.Cur, Ccb.Eur, ccb.Pts, Ccb.Dh ")
            sb.AppendLine(", Pnd.Id AS PndId, Pnd.Guid as PndGuid ")
            sb.AppendLine(", Transfer.IbanGuid, Iban.Ccc, Iban.BankBranch, Bn2.Bank, Bn2.Agc, Bn2.Adr, Bn2.Location, Bn1.Bn1, Bn1.Abr AS BankNom, Bn1.Swift ")
            sb.AppendLine("FROM Cca ")
            sb.AppendLine("INNER JOIN Ccb ON Ccb.CcaGuid = Cca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Transfer ON Ccb.Guid = Transfer.CcbGuid ")
            sb.AppendLine("LEFT OUTER JOIN Iban ON Transfer.IbanGuid = Iban.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Clx ON Ccb.ContactGuid = Clx.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnd ON Ccb.Pnd = Pnd.id ")
            sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
            sb.AppendLine("WHERE Cca.Guid = '" & oBancTransfer.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oBancTransfer.IsLoaded Then
                    With oBancTransfer
                        .Id = oDrd("Cca")
                        .AuxId = oDrd("AuxCca")
                        .Fch = oDrd("Fch")
                        .Concept = oDrd("Txt")
                        .Ccd = oDrd("Ccd")
                        .Cdn = oDrd("Cdn")
                        If Not IsDBNull(oDrd("Hash")) Then
                            .DocFile = New DTODocFile
                            .DocFile.Hash = oDrd("Hash")
                        End If
                        .Items = New List(Of DTOCcb)
                        .IsLoaded = True
                    End With
                End If


                If Not IsDBNull(oDrd("CcaGuid")) Then
                    Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
                    With oCta
                        .Id = oDrd("Id")
                        .Cod = oDrd("CtaCod")
                        .NomEsp = oDrd("Esp")
                        .NomCat = oDrd("Cat")
                        .NomEng = oDrd("Eng")
                        .IsBaseImponibleIva = oDrd("IsBaseImponibleIVA")
                        .IsQuotaIva = oDrd("IsQuotaIva")
                        .Act = oDrd("Act")
                    End With

                    Dim oContact As DTOContact = Nothing
                    If Not IsDBNull(oDrd("ContactGuid")) Then
                        oContact = New DTOContact(oDrd("ContactGuid"))
                        With oContact
                            .FullNom = oDrd("Clx")
                        End With


                    End If



                    Dim item As New DTOBancTransferItem
                    With item
                        .Cca = oBancTransfer
                        .Cta = oCta
                        .Contact = oContact
                        .Amt = Defaults.GetAmt(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                        .Dh = oDrd("dh")
                        If Not IsDBNull(oDrd("PndGuid")) Then
                            .Pnd = New DTOPnd(oDrd("PndGuid"))
                            .Pnd.Id = oDrd("PndId")
                        End If
                        If Not IsDBNull(oDrd("IbanGuid")) Then
                            .Iban = New DTOIban(oDrd("IbanGuid"))
                            .Iban.Digits = SQLHelper.GetStringFromDataReader(oDrd("Ccc"))
                            If Not IsDBNull(oDrd("BankBranch")) Then
                                .Iban.BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                                With .Iban.BankBranch
                                    .Id = oDrd("Agc")
                                    .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                    If Not IsDBNull(oDrd("Location")) Then
                                        .Location = New DTOLocation(oDrd("Location"))
                                    End If
                                End With

                                If Not IsDBNull(oDrd("Bank")) Then
                                    .Iban.BankBranch.Bank = New DTOBank(oDrd("Bank"))
                                    With .Iban.BankBranch.Bank
                                        .Id = oDrd("Bn1")
                                        .NomComercial = oDrd("BankNom")
                                        .Swift = oDrd("Swift")
                                    End With
                                End If
                            End If
                        End If
                    End With

                    If oCta.Cod = DTOPgcPlan.Ctas.bancs And oDrd("Dh") = DTOCcb.DhEnum.Haber Then
                        oBancTransfer.BancEmisorItem = item
                    Else
                        oBancTransfer.Items.Add(item)
                    End If

                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oBancTransfer.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBancTransfer As DTOBancTransferOld, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oBancTransfer, oTrans)
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

    Shared Sub Update(oBancTransfer As DTOBancTransferOld, ByRef oTrans As SqlTransaction)
        CcaLoader.Update(oBancTransfer, oTrans)
        UpdateItems(oBancTransfer, oTrans)
    End Sub


    Shared Sub UpdateItems(oBancTransfer As DTOBancTransferOld, ByRef oTrans As SqlTransaction)
        If Not oBancTransfer.IsNew Then DeleteItems(oBancTransfer, oTrans)

        Dim SQL As String = "SELECT * FROM Transfer WHERE CcaGuid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBancTransfer.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOBancTransferItem In oBancTransfer.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With item
                oRow("CcaGuid") = oBancTransfer.Guid 'to deprecate
                oRow("Lin") = .Lin 'to deprecate
                oRow("CcbGuid") = .Guid
                oRow("IbanGuid") = .Iban.Guid
                oRow("Txt") = .Concept
            End With

        Next
        oDA.Update(oDs)
    End Sub

    Shared Function SavePagament(oBancTransfer As DTOBancTransferOld, oPnds As List(Of DTOPnd), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            CcaLoader.Update(oBancTransfer, oTrans)
            UpdateItems(oBancTransfer, oTrans)
            For Each oPnd As DTOPnd In oPnds
                PndLoader.Salda(oPnd, oBancTransfer, oTrans)
            Next

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

    Shared Function Delete(oBancTransfer As DTOBancTransferOld, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancTransfer, oTrans)
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


    Shared Sub Delete(oBancTransfer As DTOBancTransferOld, ByRef oTrans As SqlTransaction)
        DeleteItems(oBancTransfer, oTrans)
        PndsLoader.SaldaBack(oBancTransfer, oTrans)
        CcaLoader.Delete(oBancTransfer, oTrans)
    End Sub


    Shared Sub DeleteItems(oBancTransfer As DTOBancTransferOld, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Transfer WHERE CcaGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBancTransfer.Guid.ToString)
    End Sub


#End Region


End Class
