Public Class PgcClassLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcClass
        Dim retval As DTOPgcClass = Nothing
        Dim oPgcClass As New DTOPgcClass(oGuid)
        If Load(oPgcClass) Then
            retval = oPgcClass
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcClass As DTOPgcClass) As Boolean
        If Not oPgcClass.IsLoaded And Not oPgcClass.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT PgcPlan.Nom as PlanNom, PgcClass.* ")
            sb.AppendLine(", Parent.NomEsp AS ParentEsp, Parent.NomCat AS ParentCat, Parent.NomEng AS ParentEng ")
            sb.AppendLine("FROM PgcClass ")
            sb.AppendLine("LEFT OUTER JOIN PgcClass Parent ON PgcClass.Parent=Parent.Guid")
            sb.AppendLine("INNER JOIN PgcPlan ON PgcClass.[Plan]=PgcPlan.Guid ")
            sb.AppendLine("WHERE PgcClass.Guid='" & oPgcClass.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPgcClass
                    .Plan = New DTOPgcPlan(oDrd("Plan"))
                    .Plan.Nom = oDrd("PlanNom")
                    If Not IsDBNull(oDrd("Parent")) Then
                        .Parent = New DTOPgcClass(oDrd("Parent"))
                        With .Parent
                            .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ParentEsp", "ParentCat", "ParentEng", "")
                        End With
                    End If
                    .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                    .Ord = oDrd("Ord")
                    .Level = oDrd("Level")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "")
                    .HideFigures = oDrd("HideFigures")
                    .Sumandos = DecodeSumandos(oDrd("Sumandos"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPgcClass.IsLoaded
        Return retval
    End Function

    Shared Function DecodeSumandos(src As Object) As List(Of DTOPgcClass.Cods)
        Dim retval As New List(Of DTOPgcClass.Cods)
        If Not IsDBNull(src) Then
            Dim sCods() As String = src.split(",")
            For Each sCod As String In sCods
                Dim oCod As DTOPgcClass.Cods = CType(sCod, DTOPgcClass.Cods)
                retval.Add(oCod)
            Next
        End If
        Return retval
    End Function

    Shared Function EncodeSumandos(src As List(Of DTOPgcClass.Cods)) As Object
        Dim retval As Object = System.DBNull.Value
        If src IsNot Nothing Then
            If src.Count > 0 Then
                Dim sb As New System.Text.StringBuilder
                For Each oCod As DTOPgcClass.Cods In src
                    If oCod <> src.First Then
                        sb.Append(",")
                    End If
                    sb.Append(CInt(oCod).ToString())
                Next
                retval = sb.ToString
            End If
        End If
        Return retval
    End Function

    Shared Function Update(oPgcClass As DTOPgcClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcClass, oTrans)
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


    Shared Sub Update(oPgcClass As DTOPgcClass, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcClass ")
        sb.AppendLine("WHERE Guid='" & oPgcClass.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcClass.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcClass
            oRow("Plan") = .Plan.Guid
            oRow("Parent") = SQLHelper.NullableBaseGuid(.Parent)
            oRow("Cod") = SQLHelper.NullableInt(.Cod)
            oRow("Ord") = .Ord
            oRow("Level") = .Level
            oRow("NomEsp") = SQLHelper.NullableLangText(.Nom, DTOLang.ESP)
            oRow("NomCat") = SQLHelper.NullableLangText(.Nom, DTOLang.CAT)
            oRow("NomEng") = SQLHelper.NullableLangText(.Nom, DTOLang.ENG)
            oRow("HideFigures") = .HideFigures
            oRow("Sumandos") = EncodeSumandos(.Sumandos)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPgcClass As DTOPgcClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcClass, oTrans)
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


    Shared Sub Delete(oPgcClass As DTOPgcClass, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcClass WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPgcClass.Guid.ToString())
    End Sub

#End Region

End Class

Public Class PgcClassesLoader

    Shared Function All(oPlan As DTOPgcPlan) As List(Of DTOPgcClass)
        Dim retval As New List(Of DTOPgcClass)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcClass ")
        sb.AppendLine("WHERE [Plan]='" & oPlan.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Parent, Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPgcClass(oDrd("Guid"))
            With item
                .Plan = New DTOPgcPlan(oDrd("Plan"))
                If Not IsDBNull(oDrd("Parent")) Then
                    .Parent = New DTOPgcClass(oDrd("Parent"))
                End If
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                .Ord = oDrd("Ord")
                .Level = oDrd("Level")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "")
                .HideFigures = oDrd("HideFigures")
                'If Not IsDBNull(oDrd("Sumandos")) Then Stop
                .Sumandos = PgcClassLoader.DecodeSumandos(oDrd("Sumandos"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, FromYear As Integer) As List(Of DTOPgcClass)
        Dim retval As New List(Of DTOPgcClass)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwCtaMes ")
        sb.AppendLine("WHERE VwCtaMes.Emp = " & CInt(oEmp.Id) & " ")
        sb.AppendLine("AND VwCtaMes.Year >= " & FromYear & " ")
        sb.AppendLine("ORDER BY VwCtaMes.PgcClass, VwCtaMes.id, VwCtaMes.Year ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oPgcClass As New DTOPgcClass
        Dim oPgcCta As New DTOPgcCta
        Do While oDrd.Read
            If Not oPgcClass.Guid.Equals(oDrd("PgcClass")) Then
                oPgcClass = New DTOPgcClass(oDrd("PgcClass"))
                oPgcClass.YearMonths = New List(Of DTOYearMonth)
                retval.Add(oPgcClass)
            End If
            If Not oPgcCta.Guid.Equals(oDrd("CtaGuid")) Then
                oPgcCta = New DTOPgcCta(oDrd("CtaGuid"))
                With oPgcCta
                    .Id = oDrd("Id")
                    .Act = oDrd("Act")
                    .Codi = oDrd("CtaCod")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                    .PgcClass = oPgcClass
                    .YearMonths = New List(Of DTOYearMonth)
                End With
                oPgcClass.Ctas.Add(oPgcCta)
            End If
            Dim Year As Integer = oDrd("Year")
            For mes As Integer = 1 To 12
                Dim sDebField = String.Format("D{0}", Format(mes, "00"))
                Dim sHabField = String.Format("H{0}", Format(mes, "00"))
                Dim DcDeb = SQLHelper.GetDecimalFromDataReader(oDrd(sDebField))
                Dim DcHab = SQLHelper.GetDecimalFromDataReader(oDrd(sHabField))
                Dim DcEur As Decimal = DcHab - DcDeb
                oPgcCta.YearMonths.Add(New DTOYearMonth(Year, mes, DcEur))
            Next
        Loop
        oDrd.Close()
        Return retval

    End Function

End Class

