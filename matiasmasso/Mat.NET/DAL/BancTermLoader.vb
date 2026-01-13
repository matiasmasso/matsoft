Public Class BancTermLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancTerm
        Dim retval As DTOBancTerm = Nothing
        Dim oBancTerm As New DTOBancTerm(oGuid)
        If Load(oBancTerm) Then
            retval = oBancTerm
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oBancTerm As DTOBancTerm) As Boolean
        If Not oBancTerm.IsLoaded And Not oBancTerm.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BancTerm.*, CliBnc.Abr ")
            sb.AppendLine("FROM BancTerm ")
            sb.AppendLine("INNER JOIN CliBnc ON BancTerm.Banc = CliBnc.Guid ")
            sb.AppendLine("WHERE BancTerm.Guid=@Guid ")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oBancTerm.Guid.ToString())
            If oDrd.Read Then
                With oBancTerm
                    .Guid = oDrd("Guid")
                    .Banc = New DTOBanc(oDrd("Banc"))
                    .Banc.Abr = oDrd("Abr")
                    .Target = oDrd("Target")
                    .fch = oDrd("Fch")
                    .IndexatAlEuribor = oDrd("Euribor")
                    .Diferencial = oDrd("Diferencial")
                    .MinimDespesa = oDrd("Minim")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBancTerm.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBancTerm As DTOBancTerm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBancTerm, oTrans)
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


    Shared Sub Update(oBancTerm As DTOBancTerm, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM BancTerm WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBancTerm.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBancTerm.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBancTerm
            oRow("Banc") = .Banc.Guid
            oRow("Target") = .Target
            oRow("Fch") = .Fch
            oRow("Euribor") = .IndexatAlEuribor
            oRow("Diferencial") = .Diferencial
            oRow("Minim") = .MinimDespesa
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBancTerm As DTOBancTerm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancTerm, oTrans)
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


    Shared Sub Delete(oBancTerm As DTOBancTerm, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancTerm WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBancTerm.Guid.ToString())
    End Sub

#End Region

End Class

Public Class BancTermsLoader

    Shared Function All(oEmp As DTOEmp, oRestrictToBanc As DTOBanc) As List(Of DTOBancTerm)

        Dim retval As New List(Of DTOBancTerm)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BancTerm.Guid, BancTerm.Banc, BancTerm.Fch, BancTerm.target, CliGral.FullNom, BancTerm.Euribor, BancTerm.Diferencial, BancTerm.Minim ")
        sb.AppendLine("FROM BancTerm ")
        sb.AppendLine("INNER JOIN CliGral ON BancTerm.Banc = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        If oRestrictToBanc IsNot Nothing Then
            sb.AppendLine("AND BancTerm.Banc = '" & oRestrictToBanc.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY BancTerm.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanc As DTOBanc = Nothing
            If oRestrictToBanc Is Nothing Then
                oBanc = New DTOBanc(oDrd("Banc"))
                oBanc.FullNom = oDrd("FullNom")
            Else
                oBanc = oRestrictToBanc
            End If
            Dim oTerm As New DTOBancTerm(oDrd("Guid"))
            With oTerm
                .Banc = oBanc
                .Fch = oDrd("Fch")
                .Target = oDrd("Target")
                .IndexatAlEuribor = oDrd("Euribor")
                .Diferencial = oDrd("Diferencial")
                If Not IsDBNull(oDrd("Minim")) Then
                    .MinimDespesa = oDrd("Minim")
                End If
            End With
            retval.Add(oTerm)
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function Active(oEmp As DTOEmp) As List(Of DTOBancTerm)

        Dim retval As New List(Of DTOBancTerm)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BancTerm.Guid, BancTerm.Banc, BancTerm.Fch, BancTerm.target, CliBnc.Abr, CliGral.Cli, CliBnc.Classificacio, BancTerm.Euribor, BancTerm.Diferencial, BancTerm.Minim ")
        sb.AppendLine(", Girat.Disposat ")
        sb.AppendLine("FROM BancTerm ")
        sb.AppendLine("INNER JOIN CliBnc ON BancTerm.Banc = CliBnc.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliBnc.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN (SELECT BancTerm.Banc, MAX(BancTerm.Fch) AS LastFch FROM BancTerm GROUP BY BancTerm.Banc) X ON BancTerm.Banc = X.Banc AND BancTerm.Fch = X.LastFch ")
        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("             SELECT Ccb.ContactGuid as Banc, SUM(CASE WHEN Ccb.Dh=2 then Ccb.Eur ELSE -Ccb.Eur END) AS Disposat ")
        sb.AppendLine("             FROM Ccb ")
        sb.AppendLine("             INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid AND PgcCta.Cod  =" & CInt(DTOPgcPlan.Ctas.BancsEfectesDescomptats) & " ")
        sb.AppendLine("             INNER JOIN PgcPlan ON PgcCta.[Plan]=PgcPlan.Guid And PgcPlan.YearFrom <= Year(GETDATE()) And (PgcPlan.YearTo IS NULL Or PgcPlan.YearTo > Year(GETDATE())) ")
        sb.AppendLine("             GROUP BY Ccb.ContactGuid ")
        sb.AppendLine(") As Girat ON BancTerm.Banc = Girat.Banc ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY BancTerm.Guid, BancTerm.Banc, BancTerm.Fch, BancTerm.target, CliBnc.Abr, CliGral.Cli, CliBnc.Classificacio, BancTerm.Euribor, BancTerm.Diferencial, BancTerm.Minim ")
        sb.AppendLine(", Girat.Disposat ")
        sb.AppendLine("ORDER BY CliBnc.Abr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanc As New DTOBanc(oDrd("Banc"))
            With oBanc
                .Id = oDrd("Cli")
                .Emp = oEmp
                .Abr = oDrd("Abr")
                .Classificacio = SQLHelper.GetAmtFromDataReader(oDrd("Classificacio"))
                .Disposat = SQLHelper.GetAmtFromDataReader(oDrd("Disposat"))
            End With
            Dim oTerm As New DTOBancTerm(oDrd("Guid"))
            With oTerm
                .Banc = oBanc
                .Fch = oDrd("Fch")
                .Target = oDrd("Target")
                .IndexatAlEuribor = oDrd("Euribor")
                .Diferencial = oDrd("Diferencial")
                If Not IsDBNull(oDrd("Minim")) Then
                    .MinimDespesa = oDrd("Minim")
                End If
            End With
            retval.Add(oTerm)
        Loop
        oDrd.Close()
        Return retval

    End Function
End Class
