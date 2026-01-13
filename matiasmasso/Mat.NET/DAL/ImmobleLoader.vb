
Public Class ImmobleLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOImmoble
        Dim retval As DTOImmoble = Nothing
        Dim oImmoble As New DTOImmoble(oGuid)
        If Load(oImmoble) Then
            retval = oImmoble
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oImmoble As DTOImmoble) As Boolean
        If Not oImmoble.IsLoaded And Not oImmoble.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Immoble ")
            sb.AppendLine("INNER JOIN VwZip ON Immoble.ZipGuid = VwZip.ZipGuid ")
            sb.AppendLine("WHERE Immoble.Guid='" & oImmoble.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oImmoble
                    .Emp = New DTO.Models.Base.IdNom(oDrd("Emp"))
                    .Nom = oDrd("Nom")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Cadastre = SQLHelper.GetStringFromDataReader(oDrd("Cadastre"))
                    .Titularitat = SQLHelper.GetIntegerFromDataReader(oDrd("Titularitat"))
                    .Part = SQLHelper.GetDecimalFromDataReader(oDrd("Part"))
                    .Superficie = SQLHelper.GetDecimalFromDataReader(oDrd("Superficie"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oImmoble.IsLoaded
        Return retval
    End Function

    Shared Function Update(oImmoble As DTOImmoble, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImmoble, oTrans)
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


    Shared Sub Update(oImmoble As DTOImmoble, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Immoble ")
        sb.AppendLine("WHERE Guid='" & oImmoble.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oImmoble.Guid
            oRow("Emp") = oImmoble.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oImmoble
            oRow("Nom") = .Nom
            oRow("Adr") = .Address.Text
            oRow("ZipGuid") = SQLHelper.NullableBaseGuid(.Address.Zip)
            oRow("Cadastre") = .Cadastre
            oRow("Titularitat") = SQLHelper.NullableInt(.Titularitat)
            oRow("Part") = SQLHelper.NullableDecimal(.Part)
            oRow("Superficie") = SQLHelper.NullableDecimal(.Superficie)
            oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oImmoble As DTOImmoble, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oImmoble, oTrans)
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


    Shared Sub Delete(oImmoble As DTOImmoble, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Immoble WHERE Guid='" & oImmoble.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ImmoblesLoader

    Shared Function Bundle(oUser As DTOUser) As DTOImmoble.Bundle
        Dim retval As New DTOImmoble.Bundle
        Dim oImmoble As New DTOImmoble
        Dim oItem As New DTOImmoble.InventariItem
        retval.Immobles = New List(Of DTOImmoble)
        retval.DocfileSrcs = New List(Of DTODocFileSrc)
        retval.InventariItems = New List(Of DTOImmoble.InventariItem)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Immoble.*, DocfileSrc.*, VwDocfile.* ")
        sb.AppendLine("FROM Immoble ")
        sb.AppendLine("INNER JOIN Emp ON Immoble.Emp = Emp.Emp ")
        sb.AppendLine("INNER JOIN Email ON Email.Adr = '" & oUser.EmailAddress & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwZip ON Immoble.ZipGuid = VwZip.ZipGuid ")
        sb.AppendLine("LEFT OUTER JOIN DocfileSrc ON Immoble.Guid = DocfileSrc.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON DocfileSrc.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("ORDER BY Immoble.FchFrom DESC, DocfileSrc.SrcOrd")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oImmoble.Guid.Equals(oDrd("Guid")) Then
                oImmoble = New DTOImmoble(oDrd("Guid"))
                With oImmoble
                    .Emp = New DTO.Models.Base.IdNom(oDrd("Emp"))
                    .Nom = oDrd("Nom")
                    .Titularitat = SQLHelper.GetIntegerFromDataReader(oDrd("Titularitat"))
                    .Part = SQLHelper.GetDecimalFromDataReader(oDrd("Part"))
                    .Superficie = SQLHelper.GetDecimalFromDataReader(oDrd("Superficie"))
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Cadastre = SQLHelper.GetStringFromDataReader(oDrd("Cadastre"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                End With
                retval.Immobles.Add(oImmoble)
            End If
            If Not IsDBNull(oDrd("SrcGuid")) Then
                If Not IsDBNull(oDrd("DocfileHash")) Then
                    Dim oFile As New DTODocFileSrc()
                    With oFile
                        .Src = New DTOBaseGuid(oImmoble.Guid)
                        .Cod = oDrd("SrcCod")
                        .Docfile = SQLHelper.GetDocFileFromDataReader(oDrd)
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    End With
                    retval.DocfileSrcs.Add(oFile)
                End If
            End If
        Loop
        oDrd.Close()

        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT InventariItems.*, DocfileSrc.*, VwDocfile.* ")
        sb.AppendLine("FROM InventariItems ")
        sb.AppendLine("INNER JOIN Immoble ON InventariItems.Immoble = Immoble.Guid ")
        sb.AppendLine("INNER JOIN Emp ON Immoble.Emp = Emp.Emp ")
        sb.AppendLine("INNER JOIN Email ON Email.Adr = '" & oUser.EmailAddress & "' ")
        sb.AppendLine("LEFT OUTER JOIN DocfileSrc ON InventariItems.Guid = DocfileSrc.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON DocfileSrc.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("ORDER BY InventariItems.Guid, DocfileSrc.SrcOrd ")
        SQL = sb.ToString
        oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oItem.Guid.Equals(oDrd("Guid")) Then
                oItem = New DTOImmoble.InventariItem(oDrd("Guid"))
                With oItem
                    .Immoble = New DTOImmoble(oDrd("Immoble"))
                    .Nom = oDrd("Nom")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                End With
                retval.InventariItems.Add(oItem)
            End If
            If Not IsDBNull(oDrd("SrcGuid")) Then
                If Not IsDBNull(oDrd("DocfileHash")) Then
                    Dim oFile As New DTODocFileSrc()
                    With oFile
                        .Src = New DTOBaseGuid(oImmoble.Guid)
                        .Cod = oDrd("SrcCod")
                        .Docfile = SQLHelper.GetDocFileFromDataReader(oDrd)
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    End With
                    retval.DocfileSrcs.Add(oFile)
                End If
            End If
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function All(oEmp As Models.Base.IdNom) As List(Of DTOImmoble)
        Dim retval As New List(Of DTOImmoble)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Immoble ")
        sb.AppendLine("LEFT OUTER JOIN VwZip ON Immoble.ZipGuid = VwZip.ZipGuid ")
        sb.AppendLine("WHERE Immoble.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Immoble.FchFrom DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImmoble(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Nom = oDrd("Nom")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Cadastre = SQLHelper.GetStringFromDataReader(oDrd("Cadastre"))
                .Superficie = SQLHelper.GetDecimalFromDataReader(oDrd("Superficie"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


End Class
