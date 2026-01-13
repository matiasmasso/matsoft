Public Class BankLoader

    Shared Function Find(oGuid As Guid) As DTOBank
        Dim retval As DTOBank = Nothing
        Dim oBank As New DTOBank(oGuid)
        If Load(oBank) Then
            retval = oBank
        End If
        Return retval
    End Function



    Shared Function FromSwift(src As String) As DTOBank
        Dim retval As DTOBank = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("WHERE Swift=@Swift")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Swift", src)
        If oDrd.Read Then
            retval = Find(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Logo(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.Logo48 ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("WHERE Bn1.Guid = '" & oGuid.ToString() & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Logo48")) Then
                retval = ImageMime.Factory(oDrd("Logo48"))
            End If
        End If
        oDrd.Close()

        Return retval
    End Function


    Shared Function Find(oCountry As DTOCountry, sId As String) As DTOBank
        Dim retval As DTOBank = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.*, Country.ISO ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("INNER JOIN Country ON Bn1.Country = Country.Guid ")
        If oCountry.ISO = "" Then
            sb.AppendLine("WHERE Bn1.Country = '" & oCountry.Guid.ToString & "' ")
        Else
            sb.AppendLine("WHERE Country.ISO = '" & oCountry.ISO & "' ")
        End If
        sb.AppendLine("AND Bn1 = '" & sId & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOBank(oDrd("Guid"))
            With retval
                .Id = sId
                .Country = oCountry
                .Country.ISO = oDrd("ISO")
                .NomComercial = oDrd("Abr")
                .RaoSocial = oDrd("Nom")
                If Not IsDBNull(oDrd("Logo48")) Then
                    .Logo = oDrd("Logo48")
                End If
                .SEPAB2B = oDrd("Sepa")
                .Obsoleto = oDrd("Obsoleto")
            End With
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function FromCodi(sCountryISO As String, sBankDigits As String) As DTOBank
        Dim retval As DTOBank = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.Guid AS BankGuid, Bn1.Abr, Bn1.Nom AS BankNom, Bn1.Logo48, Bn1.Obsoleto AS Bn1Obsoleto ")
        sb.AppendLine(", Country.Guid AS CountryGuid, Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("INNER JOIN Country ON Bn1.Country = Country.Guid ")
        sb.AppendLine("WHERE Country.ISO = '" & sCountryISO & "' AND Bn1.Bn1 = '" & sBankDigits & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
            With oCountry
                .ISO = sCountryISO
                .LangNom.Esp = oDrd("Nom_Esp")
                .LangNom.Cat = oDrd("Nom_Cat")
                .LangNom.Eng = oDrd("Nom_Eng")
            End With

            retval = New DTOBank(oDrd("BankGuid"))
            With retval
                .NomComercial = oDrd("Abr")
                .RaoSocial = oDrd("BankNom")
                .Country = oCountry
                If Not IsDBNull(oDrd("Logo48")) Then
                    .Logo = oDrd("Logo48")
                End If
            End With
        End If
        oDrd.Close()

        Return retval
    End Function


    Shared Function Load(ByRef oBank As DTOBank) As Boolean
        Dim retval As Boolean
        If oBank IsNot Nothing Then
            If Not oBank.IsLoaded And Not oBank.IsNew Then

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT Bn1.* ")
                sb.AppendLine(", Country.Guid AS CountryGuid, Country.ISO, Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng ")
                sb.AppendLine("FROM Bn1 ")
                sb.AppendLine("LEFT OUTER JOIN Country ON Bn1.Country=Country.Guid ")
                sb.AppendLine("WHERE Bn1.Guid='" & oBank.Guid.ToString & "'")

                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    With oBank
                        .Country = CountryLoader.NewCountry(oDrd("ISO"), oDrd("CountryGuid"), oDrd("Nom_ESP"), oDrd("Nom_Cat"), oDrd("Nom_Eng"))
                        .Id = oDrd("Bn1")
                        .RaoSocial = oDrd("Nom")
                        .NomComercial = oDrd("Abr")
                        .Swift = oDrd("Swift")
                        .Tel = oDrd("Tel")
                        .Web = oDrd("Web")
                        If Not IsDBNull(oDrd("Logo48")) Then
                            .Logo = oDrd("Logo48")
                        End If
                        .SEPAB2B = oDrd("SEPA")
                        .Obsoleto = oDrd("Obsoleto")
                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()
            End If

            retval = oBank.IsLoaded
        End If
        Return retval
    End Function

    Shared Function Update(oBank As DTOBank, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oBank, oTrans)
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

    Shared Sub Update(oBank As DTOBank, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Bn1 WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBank.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBank.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBank
            oRow("Country") = .Country.Guid
            oRow("Bn1") = .Id
            oRow("Nom") = .RaoSocial
            oRow("Abr") = .NomComercial
            oRow("Swift") = .Swift
            oRow("Tel") = .Tel
            oRow("Web") = .Web
            If .Logo Is Nothing Then
                oRow("Logo48") = System.DBNull.Value
            Else
                oRow("Logo48") = .Logo
            End If
            oRow("SEPA") = .SEPAB2B
            oRow("Obsoleto") = .Obsoleto
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oBank As DTOBank, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBank, oTrans)
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


    Shared Sub Delete(oBank As DTOBank, ByRef oTrans As SqlTransaction)
        With oBank
            Dim SQL As String = "DELETE Bn1 WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBank.Guid.ToString())
        End With
    End Sub


End Class

Public Class BanksLoader

    Shared Function Countries(oLang As DTOLang) As List(Of DTOCountry)
        Dim sField As String = oLang.Tradueix("Nom_ESP", "Nom_CAT", "Nom_ENG")

        Dim retval As New List(Of DTOCountry)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BN1.Country, Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG, Country.LangISO ")
        sb.AppendLine("FROM BN1 ")
        sb.AppendLine("INNER JOIN Country ON Bn1.Country=Country.Guid ")
        sb.AppendLine("GROUP BY Bn1.Country, Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG, Country.LangISO ")
        sb.AppendLine("ORDER BY " & sField & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCountry(oDrd("Country"))
            With item
                .ISO = oDrd("ISO")
                .LangNom.Esp = oDrd("Nom_ESP")
                .LangNom.Cat = oDrd("Nom_CAT")
                .LangNom.Eng = oDrd("Nom_ENG")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("LangISO"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional IncludeObsoletos As Boolean = False) As List(Of DTOBank)
        Dim retval As New List(Of DTOBank)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.Guid, BN1, Nom, Abr, SEPA, Obsoleto ")
        sb.AppendLine(",BN1.Country, Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG, Country.LangISO ")
        sb.AppendLine("FROM BN1 ")
        If Not IncludeObsoletos Then
            sb.AppendLine("WHERE Obsoleto=0 ")
        End If
        sb.AppendLine("Order By Bn1.Bn1")
        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOCountry(System.Guid.NewGuid)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBank(oDrd("Guid"))
            With item
                .Country = New DTOCountry(oDrd("Country"))
                With .Country
                    .ISO = oDrd("ISO")
                    .LangNom.Esp = oDrd("Nom_ESP")
                    .LangNom.Cat = oDrd("Nom_Cat")
                    .LangNom.Eng = oDrd("Nom_Eng")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("LangISO"))
                End With
                .RaoSocial = oDrd("Nom")
                .NomComercial = oDrd("Abr")
                .SEPAB2B = oDrd("SEPA")
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromCountry(oCountry As DTOCountry) As List(Of DTOBank)
        Dim retval As New List(Of DTOBank)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, BN1, Nom, Abr, Swift, SEPA, Obsoleto ")
        sb.AppendLine("FROM BN1 ")
        sb.AppendLine("WHERE Country=@Country ")
        sb.AppendLine("Order By Bn1.Bn1")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Country", oCountry.Guid.ToString())
        Do While oDrd.Read
            Dim item As New DTOBank(oDrd("Guid"))
            With item
                .Country = oCountry
                .Id = oDrd("BN1")
                .RaoSocial = oDrd("Nom")
                .NomComercial = oDrd("Abr")
                .Swift = oDrd("Swift")
                .SEPAB2B = oDrd("SEPA")
                .Obsoleto = oDrd("Obsoleto")

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class BankBranchLoader
    Shared Function Find(oGuid As Guid) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing
        Dim oBranch As New DTOBankBranch(oGuid)
        If Load(oBranch) Then
            retval = oBranch
        End If
        Return retval
    End Function

    Shared Function Find(oBank As DTOBank, Id As String) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn2.Guid, Bn2.Agc, Bn2.Location, Bn2.Adr, Bn2.Swift, Bn2.Tel, Bn2.Obsoleto ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("FROM Bn2 ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Bn2.Location = VwAreaNom.Guid ")
        sb.AppendLine("WHERE Bn2.Bank = '" & oBank.Guid.ToString & "' ")
        sb.AppendLine("AND Bn2.Agc = '" & Id & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oLocation As DTOLocation = Nothing
            If Not IsDBNull(oDrd("LocationGuid")) Then
                oLocation = LocationLoader.NewLocation(oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
            End If
            retval = New DTOBankBranch(oDrd("Guid"))
            With retval
                .Bank = oBank
                .Id = oDrd("Agc")
                .Address = oDrd("Adr")
                .Location = oLocation
                .Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .Obsoleto = oDrd("Obsoleto")
                .IsLoaded = True
            End With
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Find(oCountry As DTOCountry, sBankId As String, sBranchId As String) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn1.Guid as Bn1Guid, Bn1.Bn1, Bn1.Nom as Bn1Nom, Bn1.Abr, Bn1.Swift AS Bn1BIC, Bn1.SEPA, Bn1.Obsoleto ")
        sb.AppendLine(", Bn2.Guid as Bn2Guid, Bn2.Agc, Bn2.Adr, Bn2.Location ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("FROM Bn1 ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Bn1.Guid = Bn2.Bank ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Bn2.Location = VwAreaNom.Guid ")
        sb.AppendLine("INNER JOIN Country ON Bn1.Country = Country.Guid ")
        If oCountry.ISO = "" Then
            sb.AppendLine("WHERE Country.Guid = '" & oCountry.Guid.ToString & "' ")
        Else
            sb.AppendLine("WHERE Country.ISO = '" & oCountry.ISO & "' ")
        End If
        sb.AppendLine("AND Bn1.Bn1 = '" & sBankId & "' ")
        sb.AppendLine("AND Bn2.Agc = '" & sBranchId & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oBank As DTOBank = Nothing
            If Not IsDBNull(oDrd("Bn1Guid")) Then
                oBank = New DTOBank
                With oBank
                    .Country = oCountry
                    .Id = oDrd("Bn1")
                    .RaoSocial = oDrd("Bn1Nom")
                    .NomComercial = oDrd("Abr")
                    .Swift = oDrd("Bn1BIC")
                    '.Logo = oDrd("Logo48")
                    .SEPAB2B = oDrd("SEPA")
                    .Obsoleto = oDrd("Obsoleto")
                End With
            End If
            If Not IsDBNull(oDrd("Bn2Guid")) Then
                retval = New DTOBankBranch(oDrd("Bn2Guid"))
                With retval
                    .Bank = oBank
                    .Id = oDrd("Agc")
                    .Address = oDrd("Adr")
                    If Not IsDBNull(oDrd("LocationGuid")) Then
                        .Location = LocationLoader.NewLocation(oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
                    End If
                    '.Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                    '.Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                    .Obsoleto = oDrd("Obsoleto")
                End With

            End If
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oBankBranch As DTOBankBranch) As Boolean
        Dim retval As Boolean
        If oBankBranch IsNot Nothing Then
            If Not oBankBranch.IsLoaded Then
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT Bn1.Guid, Bn1.Country, Bn1.Bn1, Bn1.Abr, Bn1.Nom, Bn1.Swift AS Bn1Swift,Bn1.SEPA, Bn1.Tel AS BankTel, Bn1.Web, Bn1.Obsoleto AS Bn1Obsoleto ")
                sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.Provincia, VwAreaNom.ProvinciaNom ")
                sb.AppendLine(", Bn2.Bank, Bn2.Agc, Bn2.Location, Bn2.Adr, Bn2.Swift AS Bn2Swift, Bn2.Tel as Bn2Tel, Bn2.Obsoleto as Bn2Obsoleto ")
                sb.AppendLine("FROM Bn2 ")
                sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Bn2.Location = VwAreaNom.Guid ")
                sb.AppendLine("WHERE Bn2.Guid='" & oBankBranch.Guid.ToString & "'")
                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    Dim oLocation As DTOLocation = Nothing
                    If Not IsDBNull(oDrd("LocationGuid")) Then
                        oLocation = LocationLoader.NewLocation(oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"), oDrd("Provincia"), oDrd("ProvinciaNom"))
                    End If

                    Dim oBank As New DTOBank(oDrd("Bank"))
                    With oBank
                        .Id = oDrd("Bn1")
                        If oLocation IsNot Nothing Then
                            .Country = oLocation.Zona.Country
                        End If
                        .NomComercial = oDrd("Abr")
                        .RaoSocial = oDrd("Nom")
                        If Not IsDBNull(oDrd("Bn1Swift")) Then
                            .Swift = oDrd("Bn1Swift")
                        End If
                        .SEPAB2B = oDrd("SEPA")
                        'If Not IsDBNull(oDrd("Logo48")) Then
                        '    .Logo = oDrd("Logo48")
                        'End If
                        .Tel = oDrd("BankTel")
                        .Web = oDrd("Web")
                        .Obsoleto = oDrd("Bn1Obsoleto")
                    End With

                    With oBankBranch
                        .Bank = oBank
                        .Id = oDrd("Agc")
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .Location = oLocation
                        .Swift = SQLHelper.GetStringFromDataReader(oDrd("Bn2Swift"))
                        .Tel = SQLHelper.GetStringFromDataReader(oDrd("Bn2Tel"))
                        .Obsoleto = oDrd("Bn2Obsoleto")
                        .IsLoaded = True
                    End With
                End If
                oDrd.Close()
            End If

            retval = oBankBranch.IsLoaded
        End If
        Return retval

    End Function

    Shared Function Update(oBranch As DTOBankBranch, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oBranch, oTrans)
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

    Shared Sub Update(oBranch As DTOBankBranch, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Bn2 WHERE Guid='" & oBranch.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBranch.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBranch
            oRow("Bank") = .Bank.Guid
            oRow("Agc") = .Id
            If .Location Is Nothing Then
                oRow("Location") = System.DBNull.Value
            Else
                oRow("Location") = .Location.Guid
            End If
            oRow("Adr") = .Address
            oRow("Tel") = .Tel
            oRow("Swift") = .Swift
            oRow("Obsoleto") = .Obsoleto
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oBranch As DTOBankBranch, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBranch, oTrans)
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


    Shared Sub Delete(oBranch As DTOBankBranch, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Bn2 WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBranch.Guid.ToString())
    End Sub



    Shared Function FromCodi(sPaisDigits As String, sBankDigits As String, sBranchDigits As String) As DTOBankBranch
        Dim retval As DTOBankBranch = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn2.Guid, Bn2.Bank, Bn2.Agc, Bn2.Location, Bn2.Adr, Bn2.Obsoleto AS Bn2Obsoleto ")
        sb.AppendLine(", Bn1.Bn1, Bn1.Abr, Bn1.Nom AS BankNom, Bn1.Logo48, Bn1.Obsoleto AS Bn1Obsoleto, Bn1.Country as Bn1Country ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("FROM Bn2 ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("INNER JOIN Country ON Bn1.Country = Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Bn2.Location = VwAreaNom.Guid ")
        sb.AppendLine("WHERE Country.ISO='" & sPaisDigits & "' AND Bn1.Bn1 = '" & sBankDigits & "' AND Bn2.Agc='" & sBranchDigits & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oLocation As DTOLocation = Nothing

            If Not IsDBNull(oDrd("LocationGuid")) Then
                oLocation = LocationLoader.NewLocation(oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
            End If

            retval = New DTOBankBranch(oDrd("Guid"))
            With retval
                .Bank = New DTOBank(oDrd("Bank"))
                .Bank.NomComercial = oDrd("Abr")
                .Bank.RaoSocial = oDrd("BankNom")
                If oLocation Is Nothing Then
                    .Bank.Country = New DTOCountry(oDrd("Bn1Country"))
                    .Bank.Country.ISO = sPaisDigits
                Else
                    .Bank.Country = oLocation.Zona.Country
                End If
                If Not IsDBNull(oDrd("Logo48")) Then
                    .Bank.Logo = oDrd("Logo48")
                End If
                .Id = oDrd("Agc")
                .Location = oLocation
                .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                .Obsoleto = oDrd("Bn2Obsoleto")
            End With
        End If
        oDrd.Close()

        Return retval
    End Function
End Class

Public Class BankBranchesLoader

    Shared Function FromBank(oBank As DTOBank) As List(Of DTOBankBranch)
        Dim retval As New List(Of DTOBankBranch)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn2.Guid, Bn2.Bank, Bn2.Agc, Bn2.Location, Bn2.Adr, Bn2.Obsoleto ")
        sb.AppendLine(", VwLocation.* ")
        sb.AppendLine("FROM Bn2 ")
        sb.AppendLine("LEFT OUTER JOIN VwLocation ON Bn2.Location = VwLocation.LocationGuid ")
        sb.AppendLine("WHERE Bank='" & oBank.Guid.ToString & "' ")
        sb.AppendLine("ORDER By Bn2.Agc")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBankBranch(oDrd("Guid"))
            With item
                .Bank = oBank
                .Id = oDrd("Agc")
                .Location = SQLHelper.GetLocationFromDataReader(oDrd)
                .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oLocation As DTOLocation) As List(Of DTOBankBranch)
        Dim retval As New List(Of DTOBankBranch)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Bn2.Guid, Bn2.Bank, Bn2.Agc, Bn2.Adr, Bn2.Obsoleto ")
        sb.AppendLine(", Bn1.Abr, Bn1.Nom ")
        sb.AppendLine("FROM Bn2 ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("WHERE Location='" & oLocation.Guid.ToString & "' ")
        sb.AppendLine("ORDER By Bn1.Nom, Bn2.Agc")

        Dim oBank As New DTOBank
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBank.Guid.Equals(oDrd("Bank")) Then
                oBank = New DTOBank(oDrd("Bank"))
                With oBank
                    .NomComercial = oDrd("Abr")
                    .RaoSocial = oDrd("Nom")
                End With
            End If
            Dim item As New DTOBankBranch(oDrd("Guid"))
            With item
                .Bank = oBank
                .Id = oDrd("Agc")
                .Location = oLocation
                .Address = oDrd("Adr")
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function reLocate(exs As List(Of Exception), oLocationTo As DTOLocation, oBankBranches As List(Of DTOBankBranch)) As Integer
        Dim retval As Integer

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Bn2 SET Bn2.Location ='" & oLocationTo.Guid.ToString & "' ")
            sb.AppendLine("WHERE (")
            For Each oBankBranch In oBankBranches
                If Not oBankBranch.Equals(oBankBranches.First) Then sb.AppendLine("OR ")
                sb.AppendLine("Bn2.Guid='" & oBankBranch.Guid.ToString & "' ")
            Next
            sb.AppendLine(")")
            Dim SQL As String = sb.ToString
            retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function
End Class
