Public Class LLibreFacturesRebudesLoader

    Shared Function All(oExercici As DTOExercici) As List(Of DTOFacturaRebuda)
        Dim retval As New List(Of DTOFacturaRebuda)
        Dim oCtaIvaSoportat As DTOPgcCta = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.IvaSoportat, oExercici)

        Dim SQL As String = "SELECT Cca.Hash, Cca.Guid as CcaGuid, Cca.Cca, Cca.AuxCca, " _
        & "CCA.fch, CCA.txt, CliGral.NIF, " _
        & "(CASE WHEN BASE.DH=1 THEN BASE.eur ELSE -BASE.EUR END) AS BASE, " _
        & "(CASE WHEN IVA.DH=1 THEN IVA.eur ELSE -IVA.EUR END) AS IVA " _
        & "FROM CCA INNER JOIN " _
        & "CCB IVA ON CCA.Guid = IVA.CcaGuid LEFT OUTER JOIN " _
        & "(SELECT  B.CcaGuid, MAX(B.eur) AS Eur FROM CCB B INNER JOIN PGCCTA C ON B.CtaGuid= C.Guid And C.IsBaseImponibleIVA = 1 GROUP BY B.CcaGuid) AS XBASE " _
        & "ON CCA.Guid = XBASE.CcaGuid LEFT OUTER JOIN " _
        & "CCB AS BASE ON BASE.CcaGuid = XBASE.CcaGuid And BASE.eur = XBASE.Eur LEFT OUTER JOIN " _
        & "CliGral ON BASE.ContactGuid = CliGral.Guid LEFT OUTER JOIN " _
        & "PGCCTA AS CTABASE ON BASE.CtaGuid Like CTABASE.Guid " _
        & "WHERE CCA.emp = " & oExercici.Emp.Id & " " _
        & "AND CCA.yea =" & oExercici.Year & " " _
        & "AND IVA.CtaGuid = '" & oCtaIvaSoportat.Guid.ToString & "' " _
        & "AND CCA.CCD<>" & CInt(DTOCca.CcdEnum.IVA).ToString & " " _
        & "ORDER BY CCA.auxCca, CCA.fch, CCA.cca"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .fch = oDrd("Fch")
                .Concept = oDrd("Txt")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString)
                End If
            End With

            Dim oItem As New DTOFacturaRebuda
            With oItem
                .Cca = oCca
                If Not IsDBNull(oDrd("Nif")) Then
                    .Nif = oDrd("Nif")
                End If
                If Not IsDBNull(oDrd("Base")) Then
                    .BaseImponible = Defaults.GetAmt(CDec(oDrd("Base")))
                End If
                If Not IsDBNull(oDrd("Iva")) Then
                    .IvaSoportat = Defaults.GetAmt(CDec(oDrd("Iva")))
                End If
            End With

            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
