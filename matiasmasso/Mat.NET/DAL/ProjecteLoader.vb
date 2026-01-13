Public Class ProjecteLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOProjecte
        Dim retval As DTOProjecte = Nothing
        Dim oProjecte As New DTOProjecte(oGuid)
        If Load(oProjecte) Then
            retval = oProjecte
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProjecte As DTOProjecte) As Boolean
        If Not oProjecte.IsLoaded And Not oProjecte.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Projecte ")
            sb.AppendLine("WHERE Guid='" & oProjecte.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oProjecte
                    .Nom = oDrd("Nom")
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oProjecte.IsLoaded
        Return retval
    End Function

    Shared Function Update(oProjecte As DTOProjecte, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProjecte, oTrans)
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


    Shared Sub Update(oProjecte As DTOProjecte, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Projecte ")
        sb.AppendLine("WHERE Guid='" & oProjecte.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProjecte.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oProjecte
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProjecte As DTOProjecte, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            EmptyCcas(oProjecte, oTrans)
            Delete(oProjecte, oTrans)
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


    Shared Sub EmptyCcas(oProjecte As DTOProjecte, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Cca SET Projecte=NULL WHERE Projecte='" & oProjecte.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oProjecte As DTOProjecte, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Projecte WHERE Guid='" & oProjecte.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Items(oProjecte As DTOProjecte) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.Guid, CliGral.FullNom, Ccb.ContactGuid, Ccb.Dh, Ccb.Eur, Ccb.CtaGuid, PgcCta.Id AS CtaId, Cca.Fch, Cca.Cca, Ccb.CcaGuid, Cca.txt ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE (PgcCta.Id LIKE '2%' OR PgcCta.Id LIKE '6%') ")
        sb.AppendLine("AND Cca.Projecte='" & oProjecte.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliGral.FullNom, Cca.Fch")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .Fch = oDrd("Fch")
                .Id = oDrd("Cca")
                .Concept = oDrd("Txt")
            End With

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .Id = oDrd("CtaId")
            End With

            Dim oContact As New DTOContact(oDrd("ContactGuid"))
            With oContact
                .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End With

            Dim item As New DTOCcb(oDrd("Guid"))
            With item
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Dh = oDrd("Dh")
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function

#End Region

End Class

Public Class ProjectesLoader

    Shared Function All() As List(Of DTOProjecte)
        Dim retval As New List(Of DTOProjecte)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Projecte.Guid, Projecte.FchFrom, Projecte.FchTo, Projecte.Nom, X.Eur ")
        sb.AppendLine("FROM Projecte ")
        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("SELECT Cca.Projecte, SUM(CASE WHEN DH=1 THEN Ccb.Eur ELSE -Ccb.Eur END) AS Eur ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE PgcCta.Id LIKE '2%' OR PgcCta.Id LIKE '6%' ")
        sb.AppendLine("GROUP BY Cca.Projecte ")
        sb.AppendLine(") X ON Projecte.Guid = X.Projecte ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProjecte(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .FchFrom = oDrd("FchFrom")
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
