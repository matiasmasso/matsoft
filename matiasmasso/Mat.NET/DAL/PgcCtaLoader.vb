Public Class PgcCtaLoader
    Shared Function FromCod(oCod As DTOPgcPlan.Ctas, oExercici As DTOExercici) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.* ")
        sb.AppendLine("FROM PgcCta ")
        sb.AppendLine("INNER JOIN PgcPlan ON PgcCta.[Plan] = PgcPlan.Guid ")
        sb.AppendLine("WHERE PgcCta.Cod = " & CInt(oCod) & " ")
        sb.AppendLine("AND PgcPlan.YearFrom<=" & oExercici.Year & " ")
        sb.AppendLine("AND (PgcPlan.YearTo IS NULL OR PgcPlan.YearTo>=" & oExercici.Year & ") ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oPlan As New DTOPgcPlan(oDrd("Plan"))

            retval = New DTOPgcCta(oDrd("Guid"))
            With retval
                .Plan = oPlan
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .IsBaseImponibleIva = oDrd("IsBaseImponibleIva")
                .isQuotaIva = oDrd("IsQuotaIva")
                .isQuotaIrpf = oDrd("IsQuotaIrpf")
                .codi = oDrd("Cod")

                If Not IsDBNull(oDrd("PgcClass")) Then
                    .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                End If
                If Not IsDBNull(oDrd("NextCtaGuid")) Then
                    .NextCta = New DTOPgcCta(oDrd("NextCtaGuid"))
                End If
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromId(oPlan As DTOPgcPlan, sId As String) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.* ")
        sb.AppendLine("FROM PgcCta ")
        sb.AppendLine("WHERE PgcCta.[Plan] = '" & oPlan.Guid.ToString & "' ")
        sb.AppendLine("AND PgcCta.Id = '" & sId & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPgcCta(oDrd("Guid"))
            With retval
                .Plan = oPlan
                .Id = sId
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Act = oDrd("Act")
                .IsBaseImponibleIva = oDrd("IsBaseImponibleIva")
                .IsQuotaIva = oDrd("IsQuotaIva")
                .isQuotaIrpf = oDrd("IsQuotaIrpf")
                .codi = oDrd("Cod")

                If Not IsDBNull(oDrd("PgcClass")) Then
                    .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                End If
                If Not IsDBNull(oDrd("NextCtaGuid")) Then
                    .NextCta = New DTOPgcCta(oDrd("NextCtaGuid"))
                End If
            End With
        End If
        oDrd.Close()
        Return retval
    End Function


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Dim oPgcCta As New DTOPgcCta(oGuid)
        If Load(oPgcCta) Then
            retval = oPgcCta
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcCta As DTOPgcCta) As Boolean
        If Not oPgcCta.IsLoaded And Not oPgcCta.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PgcCta.*, PgcPlan.Nom AS PlanNom ")
            sb.AppendLine(", PgcClass.NomEsp as ClassEsp, PgcClass.NomCat AS ClassCat, PgcClass.NomEng AS ClassEng ")
            sb.AppendLine("FROM PgcCta ")
            sb.AppendLine("LEFT OUTER JOIN PgcPlan ON PgcCta.[Plan]=PgcPlan.Guid ")
            sb.AppendLine("LEFT OUTER JOIN PgcClass ON PgcCta.PgcClass=PgcClass.Guid ")
            sb.AppendLine("WHERE PgcCta.Guid='" & oPgcCta.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oPgcCta.Guid.ToString())
            If oDrd.Read Then


                With oPgcCta
                    If Not IsDBNull(oDrd("Plan")) Then
                        .Plan = New DTOPgcPlan(oDrd("Plan"))
                        .Plan.Nom = oDrd("PlanNom")
                    End If
                    If Not IsDBNull(oDrd("PgcClass")) Then
                        .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                        .PgcClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ClassEsp", "ClassCat", "ClassEng", "")
                    End If
                    .Id = oDrd("Id")
                    .Act = oDrd("Act")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .Act = oDrd("Act")
                    .IsBaseImponibleIva = oDrd("IsBaseImponibleIva")
                    .IsQuotaIva = oDrd("IsQuotaIva")
                    .isQuotaIrpf = oDrd("IsQuotaIrpf")
                    .codi = oDrd("Cod")
                    If Not IsDBNull(oDrd("NextCtaGuid")) Then
                        .NextCta = New DTOPgcCta(oDrd("NextCtaGuid"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPgcCta.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPgcCta As DTOPgcCta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcCta, oTrans)
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


    Shared Sub Update(oPgcCta As DTOPgcCta, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcCta ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPgcCta.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcCta.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcCta
            oRow("Id") = .Id
            oRow("Plan") = SQLHelper.NullableBaseGuid(.Plan)
            oRow("PgcClass") = SQLHelper.NullableBaseGuid(.PgcClass)
            oRow("Act") = .Act
            SQLHelper.SetNullableLangText(.Nom, oRow, "Esp", "Cat", "Eng")
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            oRow("Act") = .Act
            oRow("IsBaseImponibleIva") = .isBaseImponibleIva
            oRow("IsQuotaIva") = .isQuotaIva
            oRow("IsQuotaIrpf") = .isQuotaIrpf
            oRow("Cod") = .codi
            oRow("NextCtaGuid") = SQLHelper.NullableBaseGuid(.NextCta)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPgcCta As DTOPgcCta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcCta, oTrans)
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


    Shared Sub Delete(oPgcCta As DTOPgcCta, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcCta WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPgcCta.Guid.ToString())
    End Sub

#End Region


    Shared Function Saldo(oEmp As DTOEmp, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional ByVal DtFch As Date = Nothing) As DTOAmt
        Dim RetVal As DTOAmt = Nothing
        If oCta.Act = DTOPgcCta.Acts.NotSet Then PgcCtaLoader.Load(oCta)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(CASE WHEN CCB.DH = 1 THEN EUR ELSE - EUR END) AS Saldo ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Cca.Yea =" & IIf(DtFch = Nothing, DTO.GlobalVariables.Today().Year, DtFch.Year) & " ")
        sb.AppendLine("AND Ccb.CtaGuid='" & oCta.Guid.ToString & "' ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid='" & oContact.Guid.ToString & "' ")
        End If
        If DtFch <> Nothing Then
            sb.AppendLine("AND Cca.Fch<='" & Format(DtFch, "yyyyMMdd") & "' ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        If IsDBNull(oDrd("Saldo")) Then
            RetVal = DTOAmt.Empty()
        Else
            RetVal = DTOAmt.Factory(oDrd("Saldo"))
            If oCta.Act = DTOPgcCta.Acts.Creditora Then RetVal = RetVal.Inverse
        End If
        oDrd.Close()
        Return RetVal
    End Function

End Class

Public Class PgcCtasLoader

    Shared Function Current(year As Integer) As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.* ")
        sb.AppendLine("FROM PgcCta ")
        sb.AppendLine("INNER JOIN PgcPlan ON PgcCta.[Plan] = PgcPlan.Guid ")
        sb.AppendLine("WHERE PgcPlan.YearFrom<=" & year & " ")
        sb.AppendLine("AND (PgcPlan.YearTo IS NULL OR PgcPlan.YearTo>=" & year & ") ")
        sb.AppendLine("ORDER BY PgcCta.Id ")
        Dim SQL As String = sb.ToString
        Dim oPlan As New DTOPgcPlan()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPlan.Guid.Equals(oDrd("Plan")) Then
                oPlan = New DTOPgcPlan(oDrd("Plan"))
            End If
            Dim item As New DTOPgcCta(oDrd("Guid"))
            With item
                .Plan = oPlan
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("Cod")
                .IsBaseImponibleIva = oDrd("IsBaseImponibleIva")
                .IsQuotaIva = oDrd("IsQuotaIva")
                .isQuotaIrpf = oDrd("IsQuotaIrpf")
                If Not IsDBNull(oDrd("PgcClass")) Then
                    .pgcClass = New DTOPgcClass(DirectCast(oDrd("PgcClass"), Guid))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oPlan As DTOPgcPlan, Optional sSearchKey As String = "") As DTOPgcCta.Collection
        Dim retval As New DTOPgcCta.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("From PgcCta ")
        sb.AppendLine("WHERE [Plan] = '" & oPlan.Guid.ToString & "' ")
        If sSearchKey > "" Then
            sb.AppendLine("AND Id LIKE '" & sSearchKey & "%' ")
        End If
        sb.AppendLine("ORDER BY Id")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPgcCta(oDrd("Guid"))
            With item
                .plan = oPlan
                .id = oDrd("Id")
                .act = oDrd("Act")
                .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .codi = oDrd("Cod")
                '.Epg = SQLHelper.GetBaseGuidFromDataReader(oDrd("Epg"))
                If Not IsDBNull(oDrd("PgcClass")) Then
                    .pgcClass = New DTOPgcClass(DirectCast(oDrd("PgcClass"), Guid))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oPlan As DTOPgcPlan, oClass As DTOPgcClass) As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("From PgcCta ")
        sb.AppendLine("WHERE [Plan] = '" & oPlan.Guid.ToString & "' ")
        If oClass Is Nothing OrElse oClass.Guid = Guid.Empty Then
            'sb.AppendLine("AND PgcClass IS NULL ")
        Else
            sb.AppendLine("AND PgcClass='" & oClass.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Id")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPgcCta(oDrd("Guid"))
            With item
                .Plan = oPlan
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("Cod")
                If oClass IsNot Nothing Then
                    .PgcClass = oClass
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici, oContact As DTOContact) As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.Guid, PgcCta.[Plan], PgcCta.Id, PgcCta.Act, PgcCta.Cod, PgcCta.PgcClass ")
        sb.AppendLine(", PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        If oContact Is Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid IS NULL ")
        Else
            sb.AppendLine("AND Ccb.ContactGuid='" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY PgcCta.Guid, PgcCta.[Plan], PgcCta.Id, PgcCta.Act, PgcCta.Cod, PgcCta.PgcClass ")
        sb.AppendLine(", PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine("ORDER BY PgcCta.Id")
        Dim SQL As String = sb.ToString

        Dim oPgcPlan As New DTOPgcPlan
        Dim oPgcClass As New DTOPgcClass
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPgcPlan.Guid.Equals(oDrd("Plan")) Then
                oPgcPlan = New DTOPgcPlan(oDrd("Plan"))
            End If
            Dim item As New DTOPgcCta(oDrd("Guid"))
            With item
                .Plan = oPgcPlan
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("Cod")
                If Not IsDBNull(oDrd("PgcClass")) Then
                    .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
