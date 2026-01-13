Public Class ExerciciLoader
    Shared Event ReportProgress As ProgressBarHandler

    Shared Function Find(oGuid As Guid) As DTOExercici
        Dim retval As DTOExercici = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Yea ")
        sb.AppendLine("WHERE Guid='" & oGuid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oEmp As New DTOEmp(oDrd("Emp"))
            Dim iYear As Integer = oDrd("Yea")
            retval = New DTOExercici(oEmp, iYear)
            retval.Guid = oDrd("Guid")
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Find(oEmp As DTOEmp, iYear As Integer) As DTOExercici
        Dim retval As DTOExercici = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid ")
        sb.AppendLine("FROM Yea ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " AND Yea=" & iYear & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOExercici(oEmp, iYear)
            retval.guid = oDrd("Guid")
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function RenumeraAssentaments(exs As List(Of Exception), oExercici As DTOExercici) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("WITH CTE AS ( ")
        sb.AppendLine("    SELECT Cca.Cca, ")
        sb.AppendLine("    ROW_NUMBER() OVER (ORDER BY Cca.Fch, Cca.Ccd, Cca.Cdn) AS RN ")
        sb.AppendLine("         FROM    Cca ")
        sb.AppendLine("	        WHERE Cca.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("	        AND Cca.Yea=" & oExercici.Year & " ")
        sb.AppendLine("         ) ")
        sb.AppendLine("UPDATE CTE ")
        sb.AppendLine("SET Cca = RN; ")

        Dim SQL As String = sb.ToString
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, exs)
        Return retval
    End Function


    Shared Function EliminaTancaments(ByVal oExercici As DTOExercici, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("DELETE Ccb FROM Ccb ")
            sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
            sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " ")
            sb.AppendLine("AND Cca.Yea=" & oExercici.Year & " ")
            sb.AppendLine("AND (Cca.Ccd=" & DTOCca.CcdEnum.TancamentBalanç & " ")
            sb.AppendLine("     OR Cca.Ccd=" & DTOCca.CcdEnum.TancamentComptes & " ")
            sb.AppendLine("     OR Cca.Ccd=" & DTOCca.CcdEnum.TancamentExplotacio & ")")

            Dim SQL As String = sb.ToString
            Dim rc As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New Text.StringBuilder
            sb.AppendLine("DELETE Cca ")
            sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " ")
            sb.AppendLine("AND Cca.Yea=" & oExercici.Year & " ")
            sb.AppendLine("AND (Cca.Ccd=" & DTOCca.CcdEnum.TancamentBalanç & " ")
            sb.AppendLine("     OR Cca.Ccd=" & DTOCca.CcdEnum.TancamentComptes & " ")
            sb.AppendLine("     OR Cca.Ccd=" & DTOCca.CcdEnum.TancamentExplotacio & ")")

            SQL = sb.ToString
            rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)

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

    Shared Function Apertura(exs As List(Of Exception), oCcas As List(Of DTOCca)) As Boolean
        Dim retval As Boolean
        If oCcas.Count = 0 Then
            exs.Add(New Exception("No hi han assentaments d'apertura"))
        Else
            Dim oExercici As DTOExercici = oCcas.First.Exercici
            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Dim idx As Integer
            Try
                ExerciciLoader.RetrocedeixAssentamentsApertura(oExercici, oTrans)
                For Each oCca As DTOCca In oCcas
                    CcaLoader.Update(oCca, oTrans)
                    idx += 1
                Next
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                exs.Add(ex)
                oTrans.Rollback()
            Finally
                oConn.Close()
            End Try
        End If
        Return retval
    End Function



    Shared Function Saldos(oExercici As DTOExercici, Optional SkipTancament As Boolean = True) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)

        Dim sb As New Text.StringBuilder
        sb.Append("SELECT Ccb.CtaGuid, PgcCta.Id AS Cta, PgcCta.Act, PgcCta.Cat, Ccb.ContactGuid, CliGral.Cli, CliGral.FullNom, Ccb.Cur ")
        sb.Append(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Pts ELSE 0 END) AS DebPts ")
        sb.Append(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE 0 END) AS DebEur ")
        sb.Append(", SUM(CASE WHEN Ccb.Dh = 2 THEN Ccb.Pts ELSE 0 END) AS HabPts ")
        sb.Append(", SUM(CASE WHEN Ccb.Dh = 2 THEN Ccb.Eur ELSE 0 END) AS HabEur ")
        sb.Append("FROM Cca ")
        sb.Append("INNER JOIN Ccb ON Ccb.CcaGuid = Cca.Guid ")
        sb.Append("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.Append("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.Append("WHERE Cca.emp=" & CInt(oExercici.Emp.Id) & " AND Cca.yea=" & oExercici.Year & " ")
        If SkipTancament Then
            sb.Append("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentComptes) & " ")
            sb.Append("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentExplotacio) & " ")
            sb.Append("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentBalanç) & " ")
        End If
        sb.Append("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.Act, PgcCta.Cat, Ccb.ContactGuid, CliGral.Cli, CliGral.FullNom, Ccb.Cur ")
        sb.Append("HAVING SUM(CASE WHEN Ccb.dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) <> 0 ")
        sb.Append("ORDER BY PgcCta.Id, CliGral.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oLastCta As New DTOPgcCta
        Dim oCur As DTOCur = DTOCur.Eur
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCtaGuid As Guid = oDrd("CtaGuid")
            If Not oCtaGuid.Equals(oLastCta.Guid) Then
                oLastCta = New DTOPgcCta(oCtaGuid)
                With oLastCta
                    .Id = oDrd("Cta")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Cat")
                    .Act = oDrd("Act")
                End With


            End If

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                Dim oContactGuid As Guid = oDrd("ContactGuid")
                oContact = New DTOContact(oContactGuid)
                With oContact
                    .Emp = oExercici.Emp
                    .Id = oDrd("Cli")
                    .FullNom = oDrd("FullNom")
                End With
            End If

            If oCur.Tag <> oDrd("Cur").ToString Then
                oCur = DTOCur.Factory(oDrd("Cur").ToString())
            End If

            Dim DcDebEur As Decimal = oDrd("DebEur")
            Dim DcDebPts As Decimal = oDrd("DebPts")
            Dim DcHabEur As Decimal = oDrd("HabEur")
            Dim DcHabPts As Decimal = oDrd("HabPts")

            Dim oSaldo As New DTOPgcSaldo
            With oSaldo
                .Exercici = oExercici
                .Epg = oLastCta
                .Contact = oContact
                .Debe = DTOAmt.Factory(DcDebEur, oCur.Tag, DcDebPts)
                .Haber = DTOAmt.Factory(DcHabEur, oCur.Tag, DcHabPts)
            End With

            If oSaldo.IsNotZero Then
                retval.Add(oSaldo)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function RetrocedeixAssentamentsApertura(exs As List(Of Exception), oExercici As DTOExercici) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            RetrocedeixAssentamentsApertura(oExercici, oTrans)

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
            oTrans.Rollback()
        Finally
            oConn.Close()
        End Try
        Return retval
    End Function

    Shared Sub RetrocedeixAssentamentsApertura(oExercici As DTOExercici, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CCB FROM CCA INNER JOIN " _
    & "CCB ON Ccb.CcaGuid = Cca.Guid " _
    & "WHERE  CCA.emp = " & CInt(oExercici.Emp.Id) & " " _
    & "AND CCA.yea = " & oExercici.Year & " " _
    & "AND (CCA.ccd=" & DTOCca.CcdEnum.AperturaExercisi & " OR CCA.ccd=" & DTOCca.CcdEnum.MigracioPlaComptable & ") "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        SQL = "DELETE CCA " _
        & "WHERE CCA.emp = " & CInt(oExercici.Emp.Id) & " " _
        & "AND CCA.yea = " & oExercici.Year & " " _
        & "AND (CCA.ccd=" & DTOCca.CcdEnum.AperturaExercisi & " OR CCA.ccd=" & DTOCca.CcdEnum.MigracioPlaComptable & ") "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

    End Sub

End Class

Public Class ExercicisLoader

    Shared Function All(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of DTOExercici)
        Dim retval As New List(Of DTOExercici)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Yea.Guid, Cca.Yea FROM Cca ")
        If oContact IsNot Nothing Or oCta IsNot Nothing Then
            sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
            If oContact IsNot Nothing Then
                sb.AppendLine("AND Ccb.ContactGuid = '" & oContact.Guid.ToString & "' ")
            End If
            If oCta IsNot Nothing Then
                sb.AppendLine("AND Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
            End If
        End If
        sb.AppendLine("INNER JOIN Yea ON Cca.Yea = Yea.Yea ")
        sb.AppendLine("WHERE Cca.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Yea.Guid, Cca.Yea ")
        sb.AppendLine("ORDER BY Cca.Yea DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim iYear As Integer = oDrd("Yea")
            Dim oGuid As Guid = oDrd("Guid")
            Dim item As New DTOExercici(oEmp, iYear)
            item.Guid = oGuid
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Years(oEmp As DTOEmp) As List(Of Integer)
        Dim retval As New List(Of Integer)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Yea FROM Cca ")
        sb.AppendLine("WHERE Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Yea ")
        sb.AppendLine("ORDER BY Yea DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As Integer = oDrd("Yea")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
