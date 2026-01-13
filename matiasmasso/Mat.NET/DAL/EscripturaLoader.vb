Public Class EscripturaLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOEscriptura
        Dim retval As DTOEscriptura = Nothing
        Dim oEscriptura As New DTOEscriptura(oGuid)
        If Load(oEscriptura) Then
            retval = oEscriptura
        End If
        Return retval
    End Function

    Shared Function FromCod(oEmp As DTOEmp, oCodi As DTOEscriptura.Codis) As DTOEscriptura
        Dim retval As DTOEscriptura = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Escriptura.Guid, Escriptura.Hash ")
        sb.AppendLine("FROM Escriptura ")
        sb.AppendLine("INNER JOIN CliGral ON Escriptura.Notari = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " AND Escriptura.Codi=" & CInt(oCodi) & " ")
        sb.AppendLine("ORDER BY FchFrom DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOEscriptura(oDrd("Guid"))
            With retval
                .Codi = oCodi
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                End If
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oEscriptura As DTOEscriptura) As Boolean
        If Not oEscriptura.IsLoaded And Not oEscriptura.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp, Escriptura.*, CliGral.FullNom, RM.FullNom AS NomRM ")
            sb.AppendLine("FROM Escriptura ")
            sb.AppendLine("INNER JOIN CliGral ON Escriptura.Notari = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS RM ON Escriptura.Registremercantil = RM.Guid ")
            sb.AppendLine("WHERE Escriptura.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oEscriptura.Guid.ToString())
            If oDrd.Read Then
                Dim oEmp As New DTOEmp
                oEmp.Id = oDrd("Emp")

                With oEscriptura
                    .Emp = oEmp
                    If Not IsDBNull(oDrd("Notari")) Then
                        .Notari = New DTOContact(oDrd("Notari"))
                        .Notari.FullNom = oDrd("FullNom")
                        .Notari.Emp = oEmp
                    End If
                    .NumProtocol = CInt(oDrd("NumProtocol"))
                    .FchFrom = CDate(oDrd("FchFrom"))
                    If IsDBNull(oDrd("FchTo")) Then
                        .FchTo = Nothing
                    Else
                        .FchTo = CDate(oDrd("FchTo"))
                    End If
                    If Not IsDBNull(oDrd("RegistreMercantil")) Then
                        .RegistreMercantil = New DTOContact(oDrd("registreMercantil"))
                        .RegistreMercantil.FullNom = oDrd("NomRM")
                        .RegistreMercantil.Emp = oEmp
                    End If
                    .Tomo = CInt(oDrd("Tomo"))
                    .Folio = CInt(oDrd("Folio"))
                    .Hoja = oDrd("Hoja").ToString
                    .Inscripcio = CInt(oDrd("Inscripcio"))
                    .Codi = CInt(oDrd("Codi"))
                    .Nom = oDrd("Nom").ToString
                    If IsDBNull(oDrd("Obs")) Then
                        .Obs = ""
                    Else
                        .Obs = oDrd("Obs").ToString
                    End If

                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile
                        .DocFile.Hash = oDrd("Hash")
                    End If

                    .IsLoaded = True
                End With

            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEscriptura.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEscriptura As DTOEscriptura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEscriptura, oTrans)
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

    Shared Sub Update(oEscriptura As DTOEscriptura, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oEscriptura.DocFile, oTrans)
        UpdateHeader(oEscriptura, oTrans)
    End Sub


    Shared Sub UpdateHeader(oEscriptura As DTOEscriptura, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Escriptura ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oEscriptura.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEscriptura.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEscriptura
            If .Notari Is Nothing Then
                oRow("Notari") = System.DBNull.Value
            Else
                oRow("Notari") = .Notari.Guid
            End If
            oRow("NumProtocol") = .NumProtocol
            oRow("FchFrom") = .FchFrom
            If .FchTo = Date.MinValue Or .FchTo = Date.MaxValue Then
                oRow("FchTo") = System.DBNull.Value
            Else
                oRow("FchTo") = .FchTo
            End If

            If .RegistreMercantil Is Nothing Then
                oRow("RegistreMercantil") = System.DBNull.Value
            Else
                oRow("RegistreMercantil") = .RegistreMercantil.Guid
            End If
            oRow("Tomo") = .Tomo
            oRow("Folio") = .Folio
            oRow("Hoja") = .Hoja
            oRow("Inscripcio") = .Inscripcio
            oRow("Codi") = .Codi
            oRow("Nom") = .Nom
            If .Obs = "" Then
                oRow("Obs") = System.DBNull.Value
            Else
                oRow("Obs") = .Obs
            End If

            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEscriptura As DTOEscriptura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEscriptura, oTrans)
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

    Shared Sub Delete(oEscriptura As DTOEscriptura, ByRef oTrans As SqlTransaction)
        DocFileLoader.Delete(oEscriptura.DocFile, oTrans)
        DeleteHeader(oEscriptura, oTrans)
    End Sub

    Shared Sub DeleteHeader(oEscriptura As DTOEscriptura, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Escriptura WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oEscriptura.Guid.ToString())
    End Sub

#End Region

End Class

Public Class EscripturasLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOEscriptura)
        Dim retval As New List(Of DTOEscriptura)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Escriptura.*, CliGral.FullNom, RM.FullNom AS NomRM, VwDocfile.* ")
        sb.AppendLine("FROM Escriptura ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Escriptura.Notari = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS RM ON Escriptura.RegistreMercantil = RM.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Escriptura.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY FchFrom DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEscriptura(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Notari")) Then
                    .Notari = New DTOContact(oDrd("Notari"))
                    .Notari.FullNom = oDrd("Fullnom")
                End If
                .NumProtocol = CInt(oDrd("NumProtocol"))
                .FchFrom = CDate(oDrd("FchFrom"))
                If IsDBNull(oDrd("FchTo")) Then
                    .FchTo = Nothing
                Else
                    .FchTo = CDate(oDrd("FchTo"))
                End If
                If Not IsDBNull(oDrd("RegistreMercantil")) Then
                    .RegistreMercantil = New DTOContact(oDrd("registreMercantil"))
                    .RegistreMercantil.FullNom = oDrd("NomRM")
                End If
                .Tomo = CInt(oDrd("Tomo"))
                .Folio = CInt(oDrd("Folio"))
                .Hoja = oDrd("Hoja").ToString
                .Inscripcio = CInt(oDrd("Inscripcio"))
                .Codi = CInt(oDrd("Codi"))
                .Nom = oDrd("Nom").ToString
                If IsDBNull(oDrd("Obs")) Then
                    .Obs = ""
                Else
                    .Obs = oDrd("Obs").ToString
                End If

                .DocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
