Public Class CountryLoader

    Shared Function NewCountry(sISO As String, oGuid As Guid, sCountryEsp As String, Optional sCountryCat As String = "", Optional sCountryEng As String = "", Optional sCountryPor As String = "") As DTOCountry
        Dim retval As New DTOCountry(oGuid)
        With retval
            .ISO = sISO
            .LangNom = New DTOLangText(sCountryEsp, sCountryCat, sCountryEng, sCountryPor)
        End With
        Return retval
    End Function

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCountry
        Dim retval As DTOCountry = Nothing
        Dim oCountry As New DTOCountry(oGuid)
        If Load(oCountry) Then
            retval = oCountry
        End If
        Return retval
    End Function

    Shared Function Find(ISO As String) As DTOCountry
        Dim retval As DTOCountry = Nothing

        Dim SQL As String = "SELECT * FROM Country WHERE ISO='" & ISO & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOCountry(oDrd("Guid"))
            With retval
                .ISO = ISO
                .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                .PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                .ExportCod = oDrd("ExportCod")
                .Lang = DTOLang.Factory(oDrd("LangISO"))
                .IsLoaded = True
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oCountry As DTOCountry) As Boolean
        If Not oCountry.IsLoaded And Not oCountry.IsNew Then
            If Not (oCountry.Guid = Nothing And oCountry.ISO = "") Then

                Dim SQL As String
                If oCountry.Guid = Nothing Then
                    SQL = "SELECT * FROM Country WHERE ISO='" & oCountry.ISO & "' "
                Else
                    SQL = "SELECT * FROM Country WHERE Guid='" & oCountry.Guid.ToString & "' "
                End If
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    oCountry = New DTOCountry(oDrd("Guid"))
                    With oCountry
                        .ISO = oDrd("ISO")
                        .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                        .PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                        .ExportCod = oDrd("ExportCod")
                        .Lang = DTOLang.Factory(oDrd("LangISO"))
                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()

            End If
        End If

        Dim retval As Boolean = oCountry.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCountry As DTOCountry, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCountry, oTrans)
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


    Shared Sub Update(oCountry As DTOCountry, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Country WHERE Guid='" & oCountry.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oCountry.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCountry.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCountry
            oRow("ISO") = .ISO
            oRow("Nom_Esp") = .LangNom.Esp
            oRow("Nom_Cat") = .LangNom.Cat
            oRow("Nom_Eng") = .LangNom.Eng
            oRow("Nom_Por") = .LangNom.Por
            oRow("ExportCod") = .ExportCod
            oRow("PrefixeTelefonic") = SQLHelper.NullableString(.PrefixeTelefonic)
            oRow("LangISO") = .Lang.Tag
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCountry As DTOCountry, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oCountry, oTrans)
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


    Shared Sub Delete(oCountry As DTOCountry, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Country WHERE Guid='" & oCountry.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CountriesLoader
    Shared Function All(oLang As DTOLang) As List(Of DTOCountry)
        Dim sField As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim retval As New List(Of DTOCountry)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid, Country.ISO, Country.PrefixeTelefonic, Country.ExportCod, Country.LangISO, Country.Flag ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("ORDER BY " & sField)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCountry As New DTOCountry(oDrd("Guid"))
            With oCountry
                .ISO = oDrd("ISO")
                .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                .PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                .ExportCod = oDrd("ExportCod")
                .Lang = DTOLang.Factory(oDrd("LangISO"))
            End With
            retval.Add(oCountry)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOCountry)
        Dim sField As String = oUser.Lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng", "Country.Nom_Por")
        Dim retval As New List(Of DTOCountry)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid, Country.ISO ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("INNER JOIN Zona ON Country.Guid=Zona.Country ")
        sb.AppendLine("INNER JOIN Location ON Zona.Guid=Location.Zona ")
        sb.AppendLine("INNER JOIN Zip ON Location.Guid=Zip.Location ")
        sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid=CliAdr.Zip AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN CliGral ON CliAdr.SrcGuid=CliGral.Guid AND CliGral.Emp =" & oUser.Emp.Id & " ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ChildGuid = RepProducts.Area AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.Guid=ContactClass.Guid AND ContactClass.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
            Case Else
                sb.AppendLine("WHERE Guid IS NULL ")
        End Select
        sb.AppendLine("GROUP BY Country.Guid, Country.ISO ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
        sb.AppendLine("ORDER BY " & sField)

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCountry As New DTOCountry(oDrd("Guid"))
            With oCountry
                .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                .ISO = SQLHelper.GetStringFromDataReader(oDrd("ISO"))
            End With
            retval.Add(oCountry)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Atlas(oEmp As DTOEmp, oUser As DTOUser) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim sField As String = oUser.Lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid AS CountryGuid, " & sField & " AS CountryNom ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Guid AS ZipGuid, ZipCod ")
        sb.AppendLine(", CliGral.Guid AS ContactGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("INNER JOIN Zona ON Country.Guid=Zona.Country ")
        sb.AppendLine("INNER JOIN Location ON Zona.Guid=Location.Zona ")
        sb.AppendLine("INNER JOIN Zip ON Location.Guid=Zip.Location ")
        sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid=CliAdr.Zip AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN CliGral ON CliAdr.SrcGuid=CliGral.Guid AND CliGral.Emp =" & oEmp.Id & " ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ChildGuid = RepProducts.Area AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.Guid=ContactClass.Guid AND ContactClass.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
            Case Else
                sb.AppendLine("WHERE Guid IS NULL ")
        End Select
        sb.AppendLine("GROUP BY Country.Guid, " & sField & " ")
        sb.AppendLine(", Zona.Guid, Zona.Nom ")
        sb.AppendLine(", Location.Guid, Location.Nom ")
        sb.AppendLine(", Zip.Guid, ZipCod ")
        sb.AppendLine(", CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine("ORDER BY CountryNom, ZonaNom, LocationNom, RaoSocial, NomCom ")

        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNom", "CountryNom", "CountryNom", "CountryNom")
                    .Zonas = New List(Of DTOZona)
                End With
                retval.Add(oCountry)
            End If
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                oZona = New DTOZona(oDrd("ZonaGuid"))
                With oZona
                    .Nom = oDrd("ZonaNom")
                    .Locations = New List(Of DTOLocation)
                End With
                oCountry.Zonas.Add(oZona)
            End If
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                oLocation = New DTOLocation(oDrd("LocationGuid"))
                With oLocation
                    .Nom = oDrd("LocationNom")
                    .Contacts = New List(Of DTOContact)
                End With
                oZona.Locations.Add(oLocation)
            End If
            Dim oContact As New DTOContact(oDrd("ContactGuid"))
            With oContact
                .Emp = oEmp
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
            End With
            oLocation.Contacts.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GuidNoms(oUser As DTOUser) As List(Of DTOGuidNom)
        Dim sField As String = oUser.Lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim retval As New List(Of DTOGuidNom)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid, " & sField & " AS Nom ")
        sb.AppendLine("FROM Country ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Guest, DTORol.Ids.Lead
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwAreaParent ON Country.Guid = VwAreaParent.ParentGuid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Zona ON Country.Guid = Zona.Country ")
                sb.AppendLine("INNER JOIN Location ON Zona.Guid = Location.Zona ")
                sb.AppendLine("INNER JOIN Zip ON Location.Guid = Zip.Location ")
                sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid = CliAdr.Zip AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN CliGral ON CliAdr.SrcGuid = CliGral.Guid ")
                sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ChildGuid = RepProducts.Area AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.rEP = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
            Case Else
                sb.AppendLine("INNER JOIN Zona ON Country.Guid = Zona.Country ")
                sb.AppendLine("INNER JOIN Location ON Zona.Guid = Location.Zona ")
                sb.AppendLine("INNER JOIN Zip ON Location.Guid = Zip.Location ")
                sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid = CliAdr.Zip AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN Email_Clis ON CliAdr.SrcGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY Country.Guid, " & sField & " ")
        sb.AppendLine("ORDER BY Nom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCountry As New DTOGuidNom(oDrd("Guid"), oDrd("Nom"))
            retval.Add(oCountry)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GuidNoms(oLang As DTOLang) As List(Of DTOGuidNom)
        Dim sField As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim retval As New List(Of DTOGuidNom)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid, " & sField & " AS Nom ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("ORDER BY Nom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCountry As New DTOGuidNom(oDrd("Guid"), oDrd("Nom"))
            retval.Add(oCountry)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CountriesAndZonas(oUser As DTOUser) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim sField As String = oUser.Lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid AS CountryGuid, " & sField & " AS CountryNom, Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("INNER JOIN Zona ON Country.Guid = Zona.Country ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwAreaParent ON Country.Guid = VwAreaParent.ParentGuid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Location ON Zona.Guid = Location.Zona ")
                sb.AppendLine("INNER JOIN Zip ON Location.Guid = Zip.Location ")
                sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid = CliAdr.Zip AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN CliGral ON CliAdr.SrcGuid = CliGral.Guid ")
                sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ChildGuid = RepProducts.Area AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.rEP = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
            Case Else
                sb.AppendLine("INNER JOIN Location ON Zona.Guid = Location.Zona ")
                sb.AppendLine("INNER JOIN Zip ON Location.Guid = Zip.Location ")
                sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid = CliAdr.Zip AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN Email_Clis ON CliAdr.SrcGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select
        sb.AppendLine("GROUP BY Country.Guid, " & sField & " , Zona.Guid, Zona.Nom ")
        sb.AppendLine("ORDER BY CountryNom, ZonaNom ")

        Dim oCountry As New DTOCountry
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                oCountry.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNom", "CountryNom", "CountryNom", "CountryNom")
                oCountry.Zonas = New List(Of DTOZona)
                retval.Add(oCountry)
            End If
            Dim oZona As New DTOZona(oDrd("ZonaGuid"))
            oZona.Country = oCountry
            oZona.Nom = oDrd("ZonaNom")
            oCountry.Zonas.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class
