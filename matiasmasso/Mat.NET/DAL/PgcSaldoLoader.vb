Public Class PgcSaldoLoader

    Shared Function Load(ByRef oSaldo As DTOPgcSaldo) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(CASE WHEN DH=1 THEN EUR ELSE -EUR END) AS DEB ")
        sb.AppendLine(", SUM(CASE WHEN DH=2 THEN EUR ELSE -EUR END) AS HAB ")
        sb.AppendLine("FROM CCB ")
        sb.AppendLine("INNER JOIN Cca ON CCB.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PGCCTA ON CCB.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Ccb.CtaGuid = '" & oSaldo.Epg.Guid.ToString & "' ")
        sb.AppendLine("AND Year(Cca.Fch) = '" & oSaldo.Exercici.Year & "' ")
        sb.AppendLine("AND Ccb.ContactGuid = '" & oSaldo.Contact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        If Not IsDBNull(oDrd("Deb")) Then
            oSaldo.Debe = oDrd("Deb")
        End If
        If Not IsDBNull(oDrd("Hab")) Then
            oSaldo.Haber = oDrd("Hab")
        End If
        oDrd.Close()
        Return True
    End Function

    Shared Function FromCtaCod(oCtaCod As DTOPgcPlan.Ctas, oContact As DTOContact, DtFch As Date, Optional oEmp As DTOEmp = Nothing) As Decimal
        Dim retval As Decimal
        Dim DtFchFrom As New Date(DtFch.Year, 1, 1)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(CASE WHEN Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END) AS Eur, PgcCta.Act ")
        sb.AppendLine("FROM PgcPlan ")
        sb.AppendLine("INNER JOIN PgcCta ON PgcPlan.Guid=PgcCta.[Plan] AND PgcCta.Cod=" & oCtaCod & " AND (PgcPlan.YearTo IS NULL OR YearTo>=2015) ")
        sb.AppendLine("INNER JOIN Ccb ON PgcCta.Guid=Ccb.CtaGuid ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        If oContact Is Nothing Then
            sb.AppendLine("WHERE Ccb.ContactGuid IS NULL ")
            sb.AppendLine("AND Cca.Emp=" & oEmp.Id & " ")
        Else
            sb.AppendLine("INNER JOIN VwCcxOrMe ON Ccb.ContactGuid = VwCcxOrMe.Ccx ")
            sb.AppendLine("WHERE VwCcxOrMe.Guid  ='" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND Cca.fch BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' AND '" & Format(DtFch, "yyyyMMdd") & "'")
        sb.AppendLine("GROUP BY PgcCta.Act ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read() Then
            If Not IsDBNull(oDrd("Eur")) Then
                Dim oAct As DTOPgcCta.Acts = oDrd("Act")
                Select Case oAct
                    Case DTOPgcCta.Acts.Deutora
                        retval = oDrd("Eur")
                    Case DTOPgcCta.Acts.Creditora
                        retval = -oDrd("Eur")
                End Select
            End If
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class PgcSaldosLoader



    Shared Function Years(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Yea ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("WHERE Cca.Emp= " & oEmp.Id & " ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid= '" & oContact.Guid.ToString & "' ")
        End If
        If oCta IsNot Nothing Then
            sb.AppendLine("AND Ccb.CtaGuid= '" & oCta.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY Cca.Yea ")
        sb.AppendLine("ORDER BY Cca.Yea DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Yea"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(ByVal oExercici As DTOExercici,
                        Optional HideEmptySaldo As Boolean = False,
                        Optional oRange As DTO.Defaults.ContactRange = DTO.Defaults.ContactRange.AllContacts,
                        Optional oContact As DTOContact = Nothing
                                                          ) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.[Plan], PgcCta.Id AS CtaId, PgcCta.Cod AS CtaCod, Ccb.CtaGuid, Ccb.ContactGuid ")
        sb.AppendLine(", PgcCta.Act, PgcCta.Esp AS CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng ")
        sb.AppendLine(", CliGral.Cli, CliGral.RaoSocial, Ccb.Cur, ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE 0 END) AS Deb, ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 2 THEN Ccb.Eur ELSE 0 END) AS Hab, ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Pts ELSE 0 END) AS DivDeb, ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 2 THEN Ccb.Pts ELSE 0 END) AS DivHab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("AND Cca.Ccd < 96 ")

        Select Case oRange
            Case DTO.Defaults.ContactRange.AllContacts
            Case DTO.Defaults.ContactRange.OnlyNoContact
                sb.AppendLine("AND Ccb.ContactGuid IS NULL ")
            Case DTO.Defaults.ContactRange.OnlyThisContact
                sb.AppendLine("AND Ccb.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End Select

        sb.AppendLine("GROUP BY PgcCta.[Plan], PgcCta.Id, Ccb.CtaGuid, PgcCta.Act, PgcCta.Cod, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, Ccb.ContactGuid, CliGral.Cli, CliGral.RaoSocial, Ccb.Cur ")

        If HideEmptySaldo Then
            sb.AppendLine("HAVING SUM(CASE WHEN Ccb.DH = 1 THEN Ccb.EUR ELSE 0 END) <> SUM(CASE WHEN Ccb.DH = 2 THEN Ccb.EUR ELSE 0 END) ")
        End If

        sb.AppendLine("ORDER BY PgcCta.Id, Ccb.Cur, CliGral.RaoSocial, CliGral.Cli ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .plan = New DTOPgcPlan(oDrd("Plan"))
                .id = oDrd("CtaId")
                .act = oDrd("Act")
                .codi = oDrd("CtaCod")
                .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
            End With


            Dim oDebit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Deb")) Then
                oDebit = DTOAmt.Factory(CDec(oDrd("Deb")), oDrd("Cur").ToString, CDec(oDrd("DivDeb")))
            End If

            Dim oCredit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Hab")) Then
                oCredit = DTOAmt.Factory(CDec(oDrd("Hab")), oDrd("Cur").ToString, CDec(oDrd("DivHab")))
            End If


            Dim item As New DTOPgcSaldo
            With item
                .Exercici = oExercici
                .Epg = oCta
                If oContact Is Nothing Then
                    If Not IsDBNull(oDrd("ContactGuid")) Then
                        .Contact = New DTOContact(oDrd("ContactGuid"))
                        .Contact.id = oDrd("Cli")
                        .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    End If
                Else
                    .Contact = oContact
                End If
                .Debe = oDebit
                .Haber = oCredit
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function



    Shared Function Summary(ByVal oExercici As DTOExercici, Optional fch As Date = Nothing) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)
        If fch = Nothing Then fch = DTO.GlobalVariables.Today()
        Dim sFch = Format(fch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id AS CtaId, PgcCta.Cod AS CtaCod ")
        sb.AppendLine(", PgcCta.Act, PgcCta.Esp AS CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng, Ccb.Cur, ")
        sb.AppendLine("SUM(CASE WHEN DH = 1 THEN Ccb.Eur ELSE 0 END) AS Deb, ")
        sb.AppendLine("SUM(CASE WHEN DH = 2 THEN Ccb.Eur ELSE 0 END) AS Hab, ")
        sb.AppendLine("SUM(CASE WHEN DH = 1 THEN Ccb.Pts ELSE 0 END) AS DivDeb, ")
        sb.AppendLine("SUM(CASE WHEN DH = 2 THEN Ccb.Pts ELSE 0 END) AS DivHab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Ccd < " & DTOCca.CcdEnum.TancamentComptes & " ")
        sb.AppendLine("AND YEAR(Cca.Fch) = " & oExercici.Year & " ")
        sb.AppendLine("AND Cca.Fch <= '" & sFch & "' ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id,  PgcCta.Cod, PgcCta.Act, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, Ccb.Cur ")
        sb.AppendLine("ORDER BY PgcCta.Id, CCB.Cur")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .id = oDrd("CtaId")
                .act = oDrd("Act")
                .cod = oDrd("CtaCod")
                .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
            End With

            Dim oDebit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Deb")) Then
                oDebit = DTOAmt.Factory(CDec(oDrd("Deb")), oDrd("Cur").ToString, CDec(oDrd("DivDeb")))
            End If

            Dim oCredit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Hab")) Then
                oCredit = DTOAmt.Factory(CDec(oDrd("Hab")), oDrd("Cur").ToString, CDec(oDrd("DivHab")))
            End If

            Dim item As New DTOPgcSaldo
            With item
                .Exercici = oExercici
                .Epg = oCta
                .Debe = oDebit
                .Haber = oCredit
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function

    Shared Function SubComptes(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.ContactGuid, CliGral.FullNom, Ccb.Cur, ")
        sb.AppendLine("SUM(CASE WHEN DH = 1 THEN EUR ELSE 0 END) AS Deb, ")
        sb.AppendLine("SUM(CASE WHEN DH = 2 THEN EUR ELSE 0 END) AS Hab, ")
        sb.AppendLine("SUM(CASE WHEN DH = 1 THEN PTS ELSE 0 END) AS DivDeb, ")
        sb.AppendLine("SUM(CASE WHEN DH = 2 THEN PTS ELSE 0 END) AS DivHab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("AND Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
        sb.AppendLine("AND Cca.Ccd < " & DTOCca.CcdEnum.TancamentComptes & " ")
        sb.AppendLine("GROUP BY Ccb.ContactGuid, CliGral.FullNom, Ccb.Cur ")
        sb.AppendLine("ORDER BY CliGral.FullNom, Ccb.Cur")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.FullNom = oDrd("FullNom")
            End If

            Dim oDebit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Deb")) Then
                oDebit = DTOAmt.Factory(CDec(oDrd("Deb")), oDrd("Cur").ToString, CDec(oDrd("DivDeb")))
            End If

            Dim oCredit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Hab")) Then
                oCredit = DTOAmt.Factory(CDec(oDrd("Hab")), oDrd("Cur").ToString, CDec(oDrd("DivHab")))
            End If

            Dim item As New DTOPgcSaldo
            With item
                .Exercici = oExercici
                .Epg = oCta
                .Contact = oContact
                .Debe = oDebit
                .Haber = oCredit
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function

End Class
