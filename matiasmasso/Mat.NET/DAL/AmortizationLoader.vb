Public Class AmortizationLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAmortization
        Dim retval As DTOAmortization = Nothing
        Dim oAmortization As New DTOAmortization(oGuid)
        If Load(oAmortization) Then
            retval = oAmortization
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oAmortization As DTOAmortization) As Boolean
        If Not oAmortization.IsLoaded And Not oAmortization.IsNew Then
            Dim oSaldo As DTOAmt = Nothing

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Mrt.Emp, Mrt.Fch, Mrt.Cta, Mrt.Dsc, Mrt.Tipus ")
            sb.AppendLine(", Mrt.Eur, Mrt.Cur, Mrt.Pts ")
            sb.AppendLine(", Mrt.AltaCca, Mrt.BaixaCca ")
            sb.AppendLine(", Mr2.Guid AS Mr2Guid, Mr2.Fch AS Mr2Fch, Mr2.Tipus AS Mr2Tipus, Mr2.Cod AS Mr2Cod ")
            sb.AppendLine(", Mr2.Eur AS Mr2Eur, Mr2.Cur AS Mr2Cur, Mr2.Pts AS Mr2Pts, Mr2.Cca AS Mr2Cca ")
            sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp, PgcCta.Cat AS CtaCat, PgcCta.Eng AS CtaEng ")
            sb.AppendLine(", Alta.Fch AS AltaFch, Alta.Txt AS AltaTxt ")
            sb.AppendLine(", Baixa.Fch AS BaixaFch, Baixa.Txt AS BaixaTxt ")
            sb.AppendLine("FROM Mrt ")
            sb.AppendLine("INNER JOIN PgcCta ON Mrt.Cta=PgcCta.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Mr2 ON Mrt.Guid=Mr2.Parent ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS Alta ON Mrt.AltaCca=Alta.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS Baixa ON Mrt.BaixaCca=Baixa.Guid ")
            sb.AppendLine("WHERE Mrt.Guid='" & oAmortization.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Mr2.Fch ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oAmortization
                    If Not .IsLoaded Then
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Fch = oDrd("Fch")
                        .Cta = New DTOPgcCta(oDrd("Cta"))
                        With .Cta
                            .Id = oDrd("CtaId")
                            .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                        End With
                        .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                        .Tipus = oDrd("Tipus")
                        .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                        If Not IsDBNull(oDrd("AltaCca")) Then
                            .Alta = New DTOCca(oDrd("AltaCca"))
                            .Alta.Fch = SQLHelper.GetFchFromDataReader(oDrd("AltaFch"))
                            .Alta.Concept = SQLHelper.GetStringFromDataReader(oDrd("AltaTxt"))
                        End If
                        If Not IsDBNull(oDrd("BaixaCca")) Then
                            .Baixa = New DTOCca(oDrd("BaixaCca"))
                            .Baixa.Fch = SQLHelper.GetFchFromDataReader(oDrd("BaixaFch"))
                            .Baixa.Concept = SQLHelper.GetStringFromDataReader(oDrd("BaixaTxt"))
                        End If
                        .Items = New List(Of DTOAmortizationItem)
                        oSaldo = .Amt.Clone
                        .IsLoaded = True
                    End If
                End With

                If Not IsDBNull(oDrd("Mr2Guid")) Then
                    Dim item As New DTOAmortizationItem(oDrd("Mr2Guid"))
                    With item
                        .Parent = oAmortization
                        .Fch = oDrd("Mr2Fch")
                        .Tipus = oDrd("Mr2Tipus")
                        .Amt = DTOAmt.Factory(CDec(oDrd("Mr2Eur")), oDrd("Mr2Cur").ToString, CDec(oDrd("Mr2Pts")))

                        .Saldo = oSaldo.Substract(.Amt)
                        .Cod = oDrd("Mr2Cod")
                        If Not IsDBNull(oDrd("Mr2Cca")) Then
                            .Cca = New DTOCca(oDrd("Mr2Cca"))
                        End If
                    End With
                    oAmortization.Items.Add(item)
                End If

            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oAmortization.IsLoaded
        Return retval
    End Function

    Shared Function FromAlta(value As DTOCca) As DTOAmortization
        Dim retval As DTOAmortization = Nothing
        Dim SQL As String = "SELECT Guid FROM Mrt WHERE AltaCca='" & value.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOAmortization(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromBaixa(value As DTOCca) As DTOAmortization
        Dim retval As DTOAmortization = Nothing
        Dim SQL As String = "SELECT Guid FROM Mrt WHERE BaixaCca='" & value.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOAmortization(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oAmortization As DTOAmortization, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAmortization, oTrans)
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


    Shared Sub Update(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        If oAmortization.IsNew Then
            CcaLoader.Update(oAmortization.Alta, oTrans)
        End If
        UpdateHeader(oAmortization, oTrans)
        UpdateItems(oAmortization, oTrans)
    End Sub
    Shared Sub UpdateHeader(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Mrt ")
        sb.AppendLine("WHERE Guid='" & oAmortization.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAmortization.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAmortization
            oRow("Emp") = .Emp.Id
            oRow("Fch") = .Fch
            oRow("Cta") = .Cta.Guid
            oRow("Tipus") = .Tipus
            oRow("Pts") = .Amt.Val
            oRow("Cur") = .Amt.Cur.Tag
            oRow("Eur") = .Amt.Eur
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            oRow("AltaCca") = SQLHelper.NullableBaseGuid(.Alta)
            oRow("BaixaCca") = SQLHelper.NullableBaseGuid(.Baixa)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        If Not oAmortization.IsNew Then DeleteItems(oAmortization, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Mr2 ")
        sb.AppendLine("WHERE Parent='" & oAmortization.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOAmortizationItem In oAmortization.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With item
                oRow("Guid") = .Guid
                oRow("Parent") = oAmortization.Guid
                oRow("Fch") = .Fch
                oRow("Tipus") = .Tipus
                oRow("Pts") = .Amt.Val
                oRow("Cur") = .Amt.Cur.Tag
                oRow("Eur") = .Amt.Eur
                oRow("Cod") = .Cod
                oRow("Cca") = SQLHelper.NullableBaseGuid(.Cca)
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAmortization As DTOAmortization, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAmortization, oTrans)
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


    Shared Sub Delete(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        DeleteItems(oAmortization, oTrans)
        DeleteHeader(oAmortization, oTrans)
    End Sub

    Shared Sub DeleteItems(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Mr2 WHERE Parent='" & oAmortization.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oAmortization As DTOAmortization, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Mrt WHERE Guid='" & oAmortization.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class AmortizationsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOAmortization)
        Dim retval As New List(Of DTOAmortization)

        Dim oSaldo As DTOAmt = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Mrt.Guid, Mrt.Fch, Mrt.Cta, Mrt.Dsc, Mrt.Tipus ")
        sb.AppendLine(", Mrt.Eur, Mrt.Cur, Mrt.Pts ")
        sb.AppendLine(", Mrt.AltaCca, Mrt.BaixaCca ")
        sb.AppendLine(", Mr2.Guid AS Mr2Guid, Mr2.Fch AS Mr2Fch, Mr2.Tipus AS Mr2Tipus, Mr2.Cod AS Mr2Cod ")
        sb.AppendLine(", Mr2.Eur AS Mr2Eur, Mr2.Cur AS Mr2Cur, Mr2.Pts AS Mr2Pts, Mr2.Cca AS Mr2Cca ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp, PgcCta.Cat AS CtaCat, PgcCta.Eng AS CtaEng, PgcCta.Cod AS CtaCod ")
        sb.AppendLine("FROM Mrt ")
        sb.AppendLine("LEFT OUTER JOIN Mr2 ON Mrt.Guid=Mr2.Parent ")
        sb.AppendLine("INNER JOIN PgcCta ON Mrt.Cta=PgcCta.Guid ")
        sb.AppendLine("WHERE Emp='" & oEmp.Id & "' ")
        sb.AppendLine("ORDER BY PgcCta.Id, Mrt.Fch, Mrt.Guid, Mr2.Fch")

        Dim oCta As New DTOPgcCta
        Dim oAmortization As New DTOAmortization
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oAmortization.Guid.Equals(oDrd("Guid")) Then

                If Not oCta.Guid.Equals(oDrd("Cta")) Then
                    oCta = New DTOPgcCta(oDrd("Cta"))
                    With oCta
                        .Id = oDrd("CtaId")
                        .Codi = oDrd("CtaCod")
                        .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                    End With
                End If

                oAmortization = New DTOAmortization(oDrd("Guid"))
                With oAmortization
                    .Emp = oEmp
                    .Fch = oDrd("Fch")
                    .Cta = oCta
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .Tipus = oDrd("Tipus")
                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                    oSaldo = .Amt.Clone
                    If Not IsDBNull(oDrd("AltaCca")) Then
                        .Alta = New DTOCca(oDrd("AltaCca"))
                    End If
                    If Not IsDBNull(oDrd("BaixaCca")) Then
                        .Baixa = New DTOCca(oDrd("BaixaCca"))
                    End If
                    .Items = New List(Of DTOAmortizationItem)
                    .IsLoaded = True
                End With

                retval.Add(oAmortization)
            End If

            If Not IsDBNull(oDrd("Mr2Guid")) Then
                Dim item As New DTOAmortizationItem(oDrd("Mr2Guid"))
                With item
                    .Parent = oAmortization
                    .Fch = oDrd("Mr2Fch")
                    .Tipus = oDrd("Mr2Tipus")
                    .Amt = DTOAmt.Factory(CDec(oDrd("Mr2Eur")), oDrd("Mr2Cur").ToString, CDec(oDrd("Mr2Pts")))
                    .Saldo = oSaldo.Substract(.Amt)
                    .Cod = oDrd("Mr2Cod")
                    If Not IsDBNull(oDrd("Mr2Cca")) Then
                        .Cca = New DTOCca(oDrd("Mr2Cca"))
                    End If
                End With
                oAmortization.Items.Add(item)
            End If

        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function PendentsDeAmortitzar(oExercici As DTOExercici) As List(Of DTOAmortization)
        Dim retval As New List(Of DTOAmortization)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Mrt.Guid ")

        sb.AppendLine("FROM Mrt ")
        sb.AppendLine("INNER JOIN PgcCta ON Mrt.Cta=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Mr2 ON Mrt.Guid=Mr2.Parent ")
        sb.AppendLine("LEFT OUTER JOIN Cca AS Alta ON Mrt.AltaCca=Alta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca AS Baixa ON Mrt.BaixaCca=Baixa.Guid ")

        sb.AppendLine("WHERE Mrt.Emp=" & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("AND Mrt.Tipus>0 ")
        sb.AppendLine("AND Year(Mrt.FCH)<=" & oExercici.Year & " AND (Mrt.BaixaCca IS NULL OR Baixa.YEA>" & oExercici.Year & ") ") 'check que no sigui baixa de immobilitzat
        sb.AppendLine("GROUP BY Mrt.Guid, PgcCta.Id, Mrt.Fch, Mrt.Eur ")
        sb.AppendLine("HAVING (SUM(MR2.EUR) IS NULL OR (Max(Mr2.Fch)<'" & oExercici.Year & "1231' AND Mrt.Eur - SUM(Mr2.Eur) > 1)) ")
        sb.AppendLine("ORDER BY PgcCta.Id, MRT.fch")

        Dim oCta As New DTOPgcCta
        Dim oAmortization As New DTOAmortization
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAmortization(oDrd("Guid"))
            item.Emp = oExercici.Emp
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DefaultTipus(Optional year As Integer = 0) As List(Of DTOAmortizationTipus)
        Dim retval As New List(Of DTOAmortizationTipus)
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.*, MrtTipus.Pct ")
        sb.AppendLine("FROM MrtTipus ")
        sb.AppendLine("INNER JOIN PgcCta ON MrtTipus.Cod=PgcCta.Cod ")
        sb.AppendLine("INNER JOIN PgcPlan ON PgcCta.[Plan]  = PgcPlan.Guid ")
        sb.AppendLine("WHERE PgcPlan.YearFrom <=" & year & " ")
        sb.AppendLine("AND (PgcPlan.YearTo IS NULL OR PgcPlan.YearTo >=" & year & ") ")
        sb.AppendLine("ORDER BY PgcCta.Id")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCta As New DTOPgcCta(oDrd("Guid"))
            With oCta
                .Plan = New DTOPgcPlan(oDrd("Plan"))
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Act = oDrd("Act")
                .IsBaseImponibleIva = oDrd("IsBaseImponibleIva")
                .IsQuotaIva = oDrd("IsQuotaIva")
                .Codi = oDrd("Cod")

                If Not IsDBNull(oDrd("PgcClass")) Then
                    .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                End If
                If Not IsDBNull(oDrd("NextCtaGuid")) Then
                    .NextCta = New DTOPgcCta(oDrd("NextCtaGuid"))
                End If
            End With

            Dim item As New DTOAmortizationTipus()
            With item
                .Cta = oCta
                .Tipus = oDrd("Pct")
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class



Public Class AmortizacionItemLoader

    Shared Function FromCca(value As DTOCca) As DTOAmortizationItem
        Dim retval As DTOAmortizationItem = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select Mrt.Fch, Mrt.Cta, Mrt.Dsc, Mrt.Tipus ")
        sb.AppendLine(", Mrt.Eur, Mrt.Cur, Mrt.Pts ")
        sb.AppendLine(", Mrt.AltaCca, Mrt.BaixaCca ")
        sb.AppendLine(", Mr2.Guid As Mr2Guid, Mr2.Fch As Mr2Fch, Mr2.Tipus As Mr2Tipus, Mr2.Cod As Mr2Cod ")
        sb.AppendLine(", Mr2.Eur As Mr2Eur, Mr2.Cur As Mr2Cur, Mr2.Pts As Mr2Pts, Mr2.Cca As Mr2Cca ")
        sb.AppendLine(", PgcCta.Id As CtaId, PgcCta.Esp As CtaEsp, PgcCta.Cat As CtaCat, PgcCta.Eng AS CtaEng ")
        sb.AppendLine("FROM Mrt ")
        sb.AppendLine("INNER JOIN Mr2 ON Mrt.Guid=Mr2.Parent ")
        sb.AppendLine("INNER JOIN PgcCta ON Mrt.Cta=PgcCta.Guid ")
        sb.AppendLine("WHERE Mr2.Cca='" & value.Guid.ToString & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then

            Dim oAmortization As New DTOAmortization
            With oAmortization
                .Fch = oDrd("Fch")
                .Cta = New DTOPgcCta(oDrd("Cta"))
                With .Cta
                    .Id = oDrd("CtaId")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                End With
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Tipus = oDrd("Tipus")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                If Not IsDBNull(oDrd("AltaCca")) Then
                    .Alta = New DTOCca(oDrd("AltaCca"))
                End If
                If Not IsDBNull(oDrd("BaixaCca")) Then
                    .Baixa = New DTOCca(oDrd("BaixaCca"))
                End If
                .Items = New List(Of DTOAmortizationItem)
            End With

            retval = New DTOAmortizationItem(oDrd("Mr2Guid"))
            With retval
                .Parent = oAmortization
                .Fch = oDrd("Mr2Fch")
                .Tipus = oDrd("Mr2Tipus")
                .Amt = DTOAmt.Factory(CDec(oDrd("Mr2Eur")), oDrd("Mr2Cur").ToString, CDec(oDrd("Mr2Pts")))
                .Cod = oDrd("Mr2Cod")
                If Not IsDBNull(oDrd("Mr2Cca")) Then
                    .Cca = New DTOCca(oDrd("Mr2Cca"))
                End If
            End With

            oAmortization.Items.Add(retval)
        End If
        oDrd.Close()

        Return retval
    End Function

End Class

Public Class AmortizationItemLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAmortizationItem
        Dim retval As DTOAmortizationItem = Nothing
        Dim oAmortizationItem As New DTOAmortizationItem(oGuid)
        If Load(oAmortizationItem) Then
            retval = oAmortizationItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oAmortizationItem As DTOAmortizationItem) As Boolean
        If Not oAmortizationItem.IsLoaded And Not oAmortizationItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Mrt.Fch, Mrt.Cta, Mrt.Dsc, Mrt.Tipus ")
            sb.AppendLine(", Mrt.Eur, Mrt.Cur, Mrt.Pts ")
            sb.AppendLine(", Mrt.AltaCca, Mrt.BaixaCca ")
            sb.AppendLine(", Mr2.Fch AS Mr2Fch, Mr2.Tipus AS Mr2Tipus, Mr2.Cod AS Mr2Cod ")
            sb.AppendLine(", Mr2.Eur AS Mr2Eur, Mr2.Cur AS Mr2Cur, Mr2.Pts AS Mr2Pts, Mr2.Cca AS Mr2Cca ")
            sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp, PgcCta.Cat AS CtaCat, PgcCta.Eng AS CtaEng ")
            sb.AppendLine("FROM Mrt ")
            sb.AppendLine("INNER JOIN Mr2 ON Mrt.Guid=Mr2.Parent ")
            sb.AppendLine("INNER JOIN PgcCta ON Mrt.Cta=PgcCta.Guid ")
            sb.AppendLine("WHERE Mr2.Guid='" & oAmortizationItem.Guid.ToString & "' ")


            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then

                Dim oAmortization As New DTOAmortization
                With oAmortization
                    .Fch = oDrd("Fch")
                    .Cta = New DTOPgcCta(oDrd("Cta"))
                    With .Cta
                        .Id = oDrd("CtaId")
                        .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                    End With
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .Tipus = oDrd("Tipus")
                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                    If Not IsDBNull(oDrd("AltaCca")) Then
                        .Alta = New DTOCca(oDrd("AltaCca"))
                    End If
                    If Not IsDBNull(oDrd("BaixaCca")) Then
                        .Baixa = New DTOCca(oDrd("BaixaCca"))
                    End If
                End With

                With oAmortizationItem
                    .Parent = oAmortization
                    .Fch = oDrd("Mr2Fch")
                    .Tipus = oDrd("Mr2Tipus")
                    .Amt = DTOAmt.Factory(CDec(oDrd("Mr2Eur")), oDrd("Mr2Cur").ToString, CDec(oDrd("Mr2Pts")))
                    .Cod = oDrd("Mr2Cod")
                    If Not IsDBNull(oDrd("Mr2Cca")) Then
                        .Cca = New DTOCca(oDrd("Mr2Cca"))
                    End If
                End With

            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAmortizationItem.IsLoaded
        Return retval
    End Function

    Shared Function Update(oAmortizationItem As DTOAmortizationItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAmortizationItem, oTrans)
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


    Shared Sub Update(oAmortizationItem As DTOAmortizationItem, ByRef oTrans As SqlTransaction)
        CcaLoader.Update(oAmortizationItem.Cca, oTrans)

        Dim SQL As String = ""
        If oAmortizationItem.Cod = DTOAmortizationItem.Cods.Baixa Then
            SQL = "UPDATE Mrt SET BaixaCca='" & oAmortizationItem.Cca.Guid.ToString & "' WHERE Guid='" & oAmortizationItem.Parent.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Mr2 ")
        sb.AppendLine("WHERE Guid='" & oAmortizationItem.Guid.ToString & "' ")
        SQL = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAmortizationItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAmortizationItem
            oRow("Parent") = .Parent.Guid
            oRow("Fch") = .Fch
            oRow("Tipus") = .Tipus
            oRow("Eur") = CDec(.Amt.Eur)
            oRow("Cur") = .Amt.Cur.Tag
            oRow("Pts") = CDec(.Amt.Val)
            oRow("Cod") = .Cod
            oRow("Cca") = .Cca.Guid
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAmortizationItem As DTOAmortizationItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAmortizationItem, oTrans)
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


    Shared Sub Delete(oAmortizationItem As DTOAmortizationItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = ""

        SQL = "DELETE Mr2 WHERE Guid='" & oAmortizationItem.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        If oAmortizationItem.Cod = DTOAmortizationItem.Cods.Baixa Then
            SQL = "UPDATE Mrt SET BaixaCca=NULL WHERE Guid='" & oAmortizationItem.Parent.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If

        CcaLoader.Delete(oAmortizationItem.Cca, oTrans)
    End Sub


#End Region


End Class