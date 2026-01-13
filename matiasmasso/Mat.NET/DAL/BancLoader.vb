Public Class BancLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBanc
        Dim retval As DTOBanc = Nothing
        Dim oBanc As New DTOBanc(oGuid)
        If Load(oBanc) Then
            retval = oBanc
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBanc As DTOBanc) As Boolean
        If Not oBanc.IsLoaded And Not oBanc.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp ")
            sb.AppendLine(", CliBnc.Abr, CliBnc.Classificacio, CliBnc.NormaRMECedent ")
            sb.AppendLine(", CliBnc.SepaCoreIdentificador, CliBnc.ComisioGestioCobrBase ")
            sb.AppendLine(", CliBnc.ConditionsUnpayments, CliBnc.ConditionsTransfers ")
            sb.AppendLine(", Iban.Guid AS IbanGuid, Iban.BankBranch, Bn2.Bank, Bn2.Adr AS Bn2Adr, VwLocation.* ")
            sb.AppendLine(", Bn1.Nom AS BankRaoSocial, Bn1.Abr as BankNom, Bn1.Swift, Bn1.Logo48, Iban.Ccc ")
            sb.AppendLine("FROM CliBnc ")
            sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Iban ON CliBnc.Guid=Iban.ContactGuid AND Iban.Cod=" & CInt(DTOIban.Cods.banc) & " ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch=Bn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLocation ON Bn2.Location=VwLocation.LocationGuid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
            sb.AppendLine("WHERE CliBnc.Guid='" & oBanc.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBanc
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    .Classificacio = SQLHelper.GetAmtFromDataReader(oDrd("Classificacio"))
                    .NormaRMECedent = SQLHelper.GetStringFromDataReader(oDrd("NormaRMECedent"))
                    .SepaCoreIdentificador = SQLHelper.GetStringFromDataReader(oDrd("SepaCoreIdentificador"))
                    .ComisioGestioCobr = SQLHelper.GetDecimalFromDataReader(oDrd("ComisioGestioCobrBase"))
                    .ConditionsUnpayments = SQLHelper.GetStringFromDataReader(oDrd("ConditionsUnpayments"))
                    .ConditionsTransfers = SQLHelper.GetStringFromDataReader(oDrd("ConditionsTransfers"))
                    If Not IsDBNull(oDrd("IbanGuid")) Then
                        .Iban = New DTOIban(oDrd("IbanGuid"))
                        .Iban.Digits = SQLHelper.GetStringFromDataReader(oDrd("CCC"))
                        If Not IsDBNull(oDrd("BankBranch")) Then
                            .Iban.BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                            .Iban.BankBranch.Address = SQLHelper.GetStringFromDataReader(oDrd("Bn2Adr"))
                            .Iban.BankBranch.Location = SQLHelper.GetLocationFromDataReader(oDrd)
                            If Not IsDBNull(oDrd("Bank")) Then
                                .Iban.BankBranch.Bank = New DTOBank(oDrd("Bank"))
                                .Iban.BankBranch.Bank.RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("BankRaoSocial"))
                                .Iban.BankBranch.Bank.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                                .Iban.BankBranch.Bank.Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                            End If
                        End If

                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBanc.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBanc As DTOBanc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBanc, oTrans)
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


    Shared Sub Update(oBanc As DTOBanc, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliBnc ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBanc.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBanc.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBanc
            oRow("Abr") = SQLHelper.NullableString(.Abr)
            oRow("Classificacio") = SQLHelper.NullableAmt(.Classificacio)
            oRow("NormaRMECedent") = SQLHelper.NullableString(.NormaRMECedent)
            oRow("SepaCoreIdentificador") = SQLHelper.NullableString(.SepaCoreIdentificador)
            oRow("ComisioGestioCobrBase") = SQLHelper.NullableDecimal(.ComisioGestioCobr)
            oRow("ConditionsUnpayments") = SQLHelper.NullableString(.ConditionsUnpayments)
            oRow("ConditionsTransfers") = SQLHelper.NullableString(.ConditionsTransfers)

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBanc As DTOBanc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBanc, oTrans)
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


    Shared Sub Delete(oBanc As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliBnc WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBanc.Guid.ToString())
    End Sub

#End Region
End Class

Public Class BancsLoader

    Shared Function All(oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of DTOBanc)
        Dim retval As New List(Of DTOBanc)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliBnc.Guid, CliBnc.Abr, CliBnc.SepaCoreIdentificador, CliBnc.Classificacio, Anticips.Disposat ")
        sb.AppendLine(", Iban.BankBranch, Bn2.Bank, Iban.Ccc, Bn1.Logo48 ")
        sb.AppendLine("FROM CliBnc ")
        sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN IBAN ON CliBnc.Guid = Iban.ContactGuid ")
        sb.AppendLine("INNER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (")
        sb.AppendLine("     SELECT Ccb.ContactGuid, SUM(CASE WHEN DH=2 THEN EUR ELSE -EUR END) AS Disposat ")
        sb.AppendLine("     FROM Ccb ")
        sb.AppendLine("     INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("     INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("     WHERE Year(Cca.Fch)=" & DTO.GlobalVariables.Today().Year & " AND PgcCta.Cod=" & DTOPgcPlan.Ctas.BancsEfectesDescomptats & " ")
        sb.AppendLine("     GROUP BY Ccb.ContactGuid ")
        sb.AppendLine("     ) Anticips ON CliBnc.Guid = Anticips.ContactGuid ")
        sb.AppendLine("WHERE CliGral.Emp =" & oEmp.Id & " ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND CliGral.Obsoleto=0 ")
        End If
        sb.AppendLine("ORDER BY CliBnc.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanc As New DTOBanc(oDrd("Guid"))
            With oBanc
                .Emp = oEmp
                .Iban = New DTOIban
                .Abr = oDrd("Abr")
                .Logo = oDrd("Logo48")
                .SepaCoreIdentificador = SQLHelper.GetStringFromDataReader(oDrd("SepaCoreIdentificador"))
                .classificacio = SQLHelper.GetAmtFromDataReader(oDrd("Classificacio"))
                .disposat = SQLHelper.GetAmtFromDataReader(oDrd("Disposat"))
                With .iban
                    .titular = New DTOContact(oDrd("Guid"))
                    .titular.Nom = oDrd("Abr")
                    .titular.emp = oEmp
                    If Not IsDBNull(oDrd("BankBranch")) Then
                        .bankBranch = New DTOBankBranch(oDrd("BankBranch"))
                        With .bankBranch
                            .bank = New DTOBank(oDrd("Bank"))
                        End With
                    End If
                    .digits = SQLHelper.GetStringFromDataReader(oDrd("CCC"))
                End With

            End With
            retval.Add(oBanc)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Logos(oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of Byte())
        Dim retval As New List(Of Byte())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.Logo48 ")
        sb.AppendLine("FROM CliBnc ")
        sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN IBAN ON CliBnc.Guid = Iban.ContactGuid ")
        sb.AppendLine("INNER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("WHERE CliGral.Emp =" & oEmp.Id & " ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND CliGral.Obsoleto=0 ")
        End If
        sb.AppendLine("ORDER BY CliBnc.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImg = oDrd("Logo48")
            retval.Add(oImg)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Logos(oBancs As List(Of DTOBanc)) As List(Of Byte()) 'for Sprite
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.ContactGuid, Bn1.Logo48 ")
        sb.AppendLine("FROM Iban ") ' IBAN ON CliBnc.Guid = Iban.ContactGuid ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("WHERE (")
        Dim idx As Integer = 0
        For Each oBanc As DTOBanc In oBancs
            If idx > 0 Then sb.AppendLine("OR ")
            sb.AppendLine("Iban.ContactGuid = '" & oBanc.Guid.ToString & "' ")
            idx += 1
        Next
        sb.AppendLine(") ")

        Dim oIbans As New List(Of DTOIban)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oIban As New DTOIban()
            With oIban
                .Titular = New DTOContact(oDrd("ContactGuid"))
                .BankBranch = New DTOBankBranch
                .BankBranch.Bank = New DTOBank
                .BankBranch.Bank.Logo = oDrd("Logo48")
            End With
            oIbans.Add(oIban)
        Loop
        oDrd.Close()


        Dim noImage As DTODefaultImage = DefaultImageLoader.Find(DTO.Defaults.ImgTypes.banclogo44)
        Dim retval As New List(Of Byte())
        For Each oBanc As DTOBanc In oBancs
            Dim oIban As DTOIban = oIbans.FirstOrDefault(Function(x) x.Titular.Equals(oBanc))
            If oIban Is Nothing Then
                retval.Add(noImage.Image)
            Else
                retval.Add(oIban.BankBranch.Bank.Logo)
            End If
        Next
        Return retval
    End Function

    Shared Function Sprite(oGuids As List(Of Guid)) As List(Of Byte())
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT Bn1.Logo48 ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("INNER JOIN Bn2 ON Bn1.Guid = Bn2.Bank ")
        sb.AppendLine("INNER JOIN Iban ON Bn2.Guid = Iban.BankBranch ")
        sb.AppendLine("INNER JOIN @Table X ON Iban.ContactGuid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            Dim oImage = oDrd("Image")
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function BancsToReceiveTransfer(oEmp As DTOEmp) As List(Of DTOBanc)
        Dim retval As New List(Of DTOBanc)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliBnc.Guid, CliBnc.Abr, VwIban.* ")
        sb.AppendLine("FROM CliBnc ")
        sb.AppendLine("INNER JOIN VwIban ON CliBnc.Guid = VwIban.IbanContactGuid ")
        sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("AND VwIban.IbanCod = " & DTOIban.Cods.banc & " ")
        sb.AppendLine("ORDER BY CliBnc.TransferReceiptPreferencia ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanc As New DTOBanc(oDrd("Guid"))
            With oBanc
                .iban = SQLHelper.getIbanFromDataReader(oDrd)
                .abr = oDrd("Abr")
            End With
            retval.Add(oBanc)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
