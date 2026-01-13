Public Class AeatMod347Loader

    Shared Function VendesFromUser(exs As List(Of Exception), oUser As DTOUser, year As Integer) As List(Of DTOAeatMod347Item)
        Dim retval As New List(Of DTOAeatMod347Item)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, Provincia.Zip AS CodProvincia ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom AS ZonaNom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 1 THEN EURLIQ ELSE 0 END) AS T1 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 2 THEN EURLIQ ELSE 0 END) AS T2 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 3 THEN EURLIQ ELSE 0 END) AS T3 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 4 THEN EURLIQ ELSE 0 END) AS T4 ")
        sb.AppendLine("FROM FRA ")
        sb.AppendLine("INNER JOIN CliGral ON FRA.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON FRA.CliGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("LEFT OUTER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Provincia ON Zona.Provincia=Provincia.Guid ")
        sb.AppendLine("WHERE FRA.yea =" & year & " ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, Provincia.Zip ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oCountry As New DTOCountry(oDrd("Country"))
                With oCountry
                    .ISO = oDrd("ISO")
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                End With
                Dim oZona As New DTOZona
                With oZona
                    .Country = oCountry
                    .Nom = oDrd("ZonaNom")
                    .ExportCod = oDrd("ExportCod")
                End With
                Dim oLocation As New DTOLocation
                With oLocation
                    .Zona = oZona
                    .Nom = oDrd("ZonaNom")
                End With
                Dim oZip As New DTOZip
                With oZip
                    .Location = oLocation
                    .ZipCod = oDrd("ZipCod")
                End With
                Dim oAddress As New DTOAddress
                With oAddress
                    .Zip = oZip
                    .Text = oDrd("Adr")
                End With
                Dim oContact As New DTOContact(oDrd("Guid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = oAddress
                End With
                Dim item As New DTOAeatMod347Item
                With item
                    .ClauOp = DTOAeatMod347Item.ClauOps.Vendes
                    .Contact = oContact
                    If Not IsDBNull(oDrd("CodProvincia")) Then
                        .CodProvincia = oDrd("CodProvincia")
                    End If
                    If Not IsDBNull(oDrd("ISO")) Then
                        .CodPais = oDrd("ISO")
                    End If
                    .T1 = oDrd("T1")
                    .T2 = oDrd("T2")
                    .T3 = oDrd("T3")
                    .T4 = oDrd("T4")
                End With
                retval.Add(item)
            Loop
        Catch ex As Exception
            exs.Add(ex)
        Finally
            If Not oDrd Is Nothing Then
                oDrd.Close()
            End If
        End Try

        Return retval
    End Function



    Shared Function Vendes(oParent As DTOAeatMod347, exc As List(Of Exception)) As List(Of DTOAeatMod347Item)
        Dim retval As New List(Of DTOAeatMod347Item)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, Provincia.Mod347 AS CodProvincia ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom AS ZonaNom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 1 THEN EURLIQ ELSE 0 END) AS T1 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 2 THEN EURLIQ ELSE 0 END) AS T2 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 3 THEN EURLIQ ELSE 0 END) AS T3 ")
        sb.AppendLine(", SUM(CASE WHEN DATEPART(Q, FRA.fch) = 4 THEN EURLIQ ELSE 0 END) AS T4 ")
        sb.AppendLine("FROM FRA ")
        sb.AppendLine("INNER JOIN CliGral ON FRA.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("LEFT OUTER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Provincia ON Zona.Provincia=Provincia.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oParent.Exercici.Emp.Id & " AND FRA.yea =" & oParent.Exercici.Year & " ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, Provincia.Mod347 ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oCountry As New DTOCountry(oDrd("Country"))
                With oCountry
                    .ISO = oDrd("ISO")
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                End With
                Dim oZona As New DTOZona
                With oZona
                    .Country = oCountry
                    .Nom = oDrd("ZonaNom")
                    .ExportCod = oDrd("ExportCod")
                End With
                Dim oLocation As New DTOLocation
                With oLocation
                    .Zona = oZona
                    .Nom = oDrd("ZonaNom")
                End With
                Dim oZip As New DTOZip
                With oZip
                    .Location = oLocation
                    .ZipCod = oDrd("ZipCod")
                End With
                Dim oAddress As New DTOAddress
                With oAddress
                    .Zip = oZip
                    .Text = oDrd("Adr")
                End With
                Dim oContact As New DTOContact(oDrd("Guid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = oAddress
                End With
                Dim item As New DTOAeatMod347Item
                With item
                    .Parent = oParent
                    .ClauOp = DTOAeatMod347Item.ClauOps.Vendes
                    .Contact = oContact
                    If Not IsDBNull(oDrd("CodProvincia")) Then
                        .CodProvincia = oDrd("CodProvincia")
                    End If
                    If Not IsDBNull(oDrd("ISO")) Then
                        .CodPais = oDrd("ISO")
                    End If
                    .T1 = oDrd("T1")
                    .T2 = oDrd("T2")
                    .T3 = oDrd("T3")
                    .T4 = oDrd("T4")
                End With
                retval.Add(item)
            Loop
        Catch ex As Exception
            exc.Add(ex)
        Finally
            If Not oDrd Is Nothing Then
                oDrd.Close()
            End If
        End Try

        Return retval
    End Function

    Shared Function CompresDetall(oExercici As DTOExercici, oContact As DTOContact, exc As List(Of Exception)) As List(Of DTOAeatMod347Cca)
        Dim retval As New List(Of DTOAeatMod347Cca)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash, ")
        sb.AppendLine("SUM(CASE WHEN BASE.DH = 1 THEN BASE.Eur ELSE - BASE.EUR END) AS BaseImponible, ")
        sb.AppendLine("SUM(CASE WHEN IVA.DH = 1 THEN IVA.Eur ELSE - IVA.EUR END) AS IVA ")
        sb.AppendLine("FROM            CCA ")
        sb.AppendLine("INNER JOIN CCB AS IVA ON CCA.Guid = IVA.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta AS IVACta ON IVA.CtaGuid=IVACta.Guid AND IVACta.Id = '47200' ")
        sb.AppendLine("LEFT OUTER JOIN CCB AS IRPF ON CCA.Guid = IRPF.CcaGuid AND IRPF.CTA LIKE '4751%' ")
        sb.AppendLine("LEFT OUTER JOIN CCB AS BASE ON CCA.Guid = BASE.CcaGuid ")
        sb.AppendLine("INNER JOIN PGCCTA AS CTABASE ON BASE.CtaGuid = CTABASE.Guid AND CTABASE.IsBaseImponibleIVA = 1 ")
        sb.AppendLine("WHERE (CCA.yea = " & oExercici.Year & ") AND (CCA.ccd <> 81) AND (IRPF.Eur IS NULL) ")
        sb.AppendLine("AND BASE.ContactGuid ='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine("ORDER BY Cca.Fch")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOAeatMod347Cca
                Dim oCca As New DTOCca(oDrd("Guid"))
                With oCca
                    .Fch = oDrd("Fch")
                    .Id = oDrd("Cca")
                    .Concept = oDrd("Txt")
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile()
                        .DocFile.Hash = oDrd("Hash")
                    End If
                End With
                With item
                    .Cca = oCca
                    .Base = DTOAmt.Factory(CDec(oDrd("BaseImponible")))
                    .Iva = DTOAmt.Factory(CDec(oDrd("IVA")))
                End With
                retval.Add(item)
            Loop
        Catch ex As Exception
            exc.Add(ex)
        Finally
            If Not oDrd Is Nothing Then
                oDrd.Close()
            End If
        End Try

        Return retval
    End Function

    Shared Function VendesDetall(oExercici As DTOExercici, oContact As DTOContact, exc As List(Of Exception)) As List(Of DTOAeatMod347Cca)
        Dim retval As New List(Of DTOAeatMod347Cca)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash, ")
        sb.AppendLine("SUM(CASE WHEN BASE.DH = 2 THEN BASE.Eur ELSE - BASE.EUR END) AS BaseImponible, ")
        sb.AppendLine("SUM(CASE WHEN IVA.DH = 2 THEN IVA.Eur ELSE - IVA.EUR END) AS IVA ")
        sb.AppendLine("FROM            CCA ")
        sb.AppendLine("INNER JOIN CCB AS IVA ON CCA.Guid = IVA.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta AS IVACta ON IVA.CtaGuid=IVACta.Guid AND IVACta.Id = '47710' ")
        sb.AppendLine("LEFT OUTER JOIN CCB AS BASE ON CCA.Guid = BASE.CcaGuid ")
        sb.AppendLine("WHERE (CCA.yea = " & oExercici.Year & ") AND (CCA.ccd <> 81) ")
        sb.AppendLine("AND BASE.ContactGuid ='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine("ORDER BY Cca.Fch")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOAeatMod347Cca
                Dim oCca As New DTOCca(oDrd("Guid"))
                With oCca
                    .fch = oDrd("Fch")
                    .id = oDrd("Cca")
                    .concept = oDrd("Txt")
                    If Not IsDBNull(oDrd("Hash")) Then
                        .docFile = New DTODocFile()
                        .docFile.hash = oDrd("Hash")
                    End If
                End With
                With item
                    .Cca = oCca
                    .Base = DTOAmt.Factory(CDec(oDrd("BaseImponible")))
                    .Iva = DTOAmt.Factory(CDec(oDrd("IVA")))
                End With
                retval.Add(item)
            Loop
        Catch ex As Exception
            exc.Add(ex)
        Finally
            If Not oDrd Is Nothing Then
                oDrd.Close()
            End If
        End Try

        Return retval
    End Function

    Shared Function Compres(oParent As DTOAeatMod347, exc As List(Of Exception)) As List(Of DTOAeatMod347Item)
        Dim retval As New List(Of DTOAeatMod347Item)
        Dim oPlan As New DTOPgcPlan()

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, Provincia.Mod347 AS CodProvincia ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom AS ZonaNom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr, ")
        sb.AppendLine("SUM(CASE WHEN DATEPART(Q, CCA.fch) = 1 THEN (CASE WHEN BASE.DH = 1 THEN BASE.Eur ELSE - BASE.EUR END) + (CASE WHEN IVA.DH = 1 THEN IVA.Eur ELSE - IVA.EUR END) ELSE 0 END) AS T1, ")
        sb.AppendLine("SUM(CASE WHEN DATEPART(Q, CCA.fch) = 2 THEN (CASE WHEN BASE.DH = 1 THEN BASE.Eur ELSE - BASE.EUR END) + (CASE WHEN IVA.DH = 1 THEN IVA.Eur ELSE - IVA.EUR END) ELSE 0 END) AS T2, ")
        sb.AppendLine("SUM(CASE WHEN DATEPART(Q, CCA.fch) = 3 THEN (CASE WHEN BASE.DH = 1 THEN BASE.Eur ELSE - BASE.EUR END) + (CASE WHEN IVA.DH = 1 THEN IVA.Eur ELSE - IVA.EUR END) ELSE 0 END) AS T3, ")
        sb.AppendLine("SUM(CASE WHEN DATEPART(Q, CCA.fch) = 4 THEN (CASE WHEN BASE.DH = 1 THEN BASE.Eur ELSE - BASE.EUR END) + (CASE WHEN IVA.DH = 1 THEN IVA.Eur ELSE - IVA.EUR END) ELSE 0 END) AS T4 ")
        sb.AppendLine("FROM            CCA ")
        sb.AppendLine("INNER JOIN CCB AS IVA ON CCA.Guid = IVA.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta AS IVACta ON IVA.CtaGuid=IVACta.Guid AND IVACta.Id = '47200' ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT CCB.CcaGuid, CCB.Eur FROM CCB INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid AND PgcCta.Id LIKE '4751%' ) IRPF ON Cca.Guid = IRPF.CcaGuid ")

        sb.AppendLine("LEFT OUTER JOIN CCB AS BASE ON CCA.Guid = BASE.CcaGuid INNER JOIN PGCCTA AS CTABASE ON BASE.CtaGuid = CTABASE.Guid AND CTABASE.IsBaseImponibleIVA = 1 ")

        'sb.AppendLine("LEFT OUTER JOIN (SELECT        B.CcaGuid, MAX(B.Eur) AS Eur ")
        'sb.AppendLine("     FROM            CCB AS B INNER JOIN ")
        'sb.AppendLine("     PGCCTA AS C ON B.CtaGuid = C.Guid ")
        'sb.AppendLine("     WHERE        (C.IsBaseImponibleIVA = 1) ")
        'sb.AppendLine("     GROUP BY B.CcaGuid) AS XBASE ON CCA.Guid = XBASE.CcaGuid ")
        'sb.AppendLine("LEFT OUTER JOIN CCB AS BASE ON BASE.CcaGuid = XBASE.CcaGuid AND BASE.Eur = XBASE.Eur ")
        'sb.AppendLine("LEFT OUTER JOIN PGCCTA AS CTABASE ON BASE.CtaGuid = CTABASE.Guid ")

        sb.AppendLine("LEFT OUTER JOIN CliGral ON BASE.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("LEFT OUTER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Provincia ON Zona.Provincia=Provincia.Guid ")
        sb.AppendLine("WHERE (CCA.yea = " & oParent.Exercici.Year & ") AND (CCA.ccd <> 81) AND (IRPF.Eur IS NULL) ")
        sb.AppendLine("AND CCA.emp = " & oParent.Exercici.Emp.Id & " AND (CliGral.Guid is not null) ")
        sb.AppendLine("GROUP BY CliGral.RaoSocial, CliGral.Guid, Provincia.Mod347 ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.ISO ")
        sb.AppendLine(", Zona.Country, Zona.Nom, Zona.ExportCod ")
        sb.AppendLine(", Location.Zona, Location.Nom ")
        sb.AppendLine(", Zip.Location, Zip.ZipCod ")
        sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial, CliGral.Guid")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oCountry As New DTOCountry(oDrd("Country"))
                With oCountry
                    .ISO = oDrd("ISO")
                    .LangNom.Esp = oDrd("Nom_Esp")
                    .LangNom.Cat = oDrd("Nom_Cat")
                    .LangNom.Eng = oDrd("Nom_Eng")
                End With
                Dim oZona As New DTOZona
                With oZona
                    .Country = oCountry
                    .Nom = oDrd("ZonaNom")
                    .ExportCod = oDrd("ExportCod")
                End With
                Dim oLocation As New DTOLocation
                With oLocation
                    .Zona = oZona
                    .Nom = oDrd("ZonaNom")
                End With
                Dim oZip As New DTOZip
                With oZip
                    .Location = oLocation
                    .ZipCod = oDrd("ZipCod")
                End With
                Dim oAddress As New DTOAddress
                With oAddress
                    .Zip = oZip
                    .Text = oDrd("Adr")
                End With
                Dim oContact As New DTOContact(oDrd("Guid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = oAddress
                End With
                Dim item As New DTOAeatMod347Item
                With item
                    .Parent = oParent
                    .ClauOp = DTOAeatMod347Item.ClauOps.Compres
                    .Contact = oContact
                    If Not IsDBNull(oDrd("CodProvincia")) Then
                        .CodProvincia = oDrd("CodProvincia")
                    End If
                    If Not IsDBNull(oDrd("ISO")) Then
                        .CodPais = oDrd("ISO")
                    End If
                    .T1 = oDrd("T1")
                    .T2 = oDrd("T2")
                    .T3 = oDrd("T3")
                    .T4 = oDrd("T4")
                End With
                retval.Add(item)
            Loop
        Catch ex As Exception
            exc.Add(ex)
        Finally
            If Not oDrd Is Nothing Then
                oDrd.Close()
            End If
        End Try

        Return retval
    End Function

    Shared Function DestinatarisCircular(oExercici As DTOExercici, oEmails As List(Of DTOEmail), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        oEmails = New List(Of DTOEmail)
        Dim oSubscriptionComptabilitat = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Comptabilitat)
        Dim oSubscriptionFacturacio = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("select email.Guid, email.adr  ")
        sb.AppendLine("from Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=CcaGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Ccb.ContactGuid=Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN SscEmail ON Email_Clis.EmailGuid=SscEmail.Email ")
        sb.AppendLine("INNER JOIN Email ON SscEmail.Email=Email.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("And Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("And Email.Obsoleto = 0 ")
        sb.AppendLine("And Email.BadMail IS NULL ")
        sb.AppendLine("And (SscEmail.SscGuid='" & oSubscriptionComptabilitat.Guid.ToString & "' OR SscEmail.SscGuid='" & oSubscriptionFacturacio.Guid.ToString & "') ")
        sb.AppendLine("GROUP BY email.Guid, email.adr ")
        sb.AppendLine("ORDER BY email.adr ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = Nothing
        Try
            oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oEmail As New DTOEmail(oDrd("Guid"))
                With oEmail
                    .EmailAddress = oDrd("adr")
                End With
                oEmails.Add(oEmail)
            Loop
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        Finally
            oDrd.Close()
        End Try

        Return retval
    End Function

End Class
