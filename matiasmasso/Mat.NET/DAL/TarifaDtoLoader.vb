Public Class TarifaDtoLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTarifaDto
        Dim retval As DTOTarifaDto = Nothing
        Dim oTarifaDto As New DTOTarifaDto(oGuid)
        If Load(oTarifaDto) Then
            retval = oTarifaDto
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTarifaDto As DTOTarifaDto) As Boolean
        If Not oTarifaDto.IsLoaded And Not oTarifaDto.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT TarifaDto.* ")
            sb.AppendLine(", Tpa.Guid AS BrandGuid, Tpa.Dsc AS BrandNom ")
            sb.AppendLine(", Clx.Guid AS ContactGuid, Clx.Clx AS ContactNom ")
            sb.AppendLine(", Email.Adr, Email.Nickname ")
            sb.AppendLine("FROM TarifaDto ")
            sb.AppendLine("LEFT OUTER JOIN Tpa ON TarifaDto.Product = Tpa.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Clx ON TarifaDto.Contact = Clx.Guid ")
            sb.AppendLine("INNER JOIN Email ON TarifaDto.UsrCreated = Email.Guid ")
            sb.AppendLine("WHERE TarifaDto.Guid='" & oTarifaDto.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTarifaDto
                    If Not IsDBNull(oDrd("Product")) Then
                        .Product = New DTOProductBrand(oDrd("BrandGuid"))
                        .Product.Nom = SQLHelper.GetStringFromDataReader(oDrd("BrandNom"))
                    End If
                    If Not IsDBNull(oDrd("Contact")) Then
                        .Contact = New DTOContact(oDrd("ContactGuid"))
                        .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                    End If
                    .Fch = oDrd("Fch")
                    .Value = oDrd("Value")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .UsrCreated = New DTOUser(CType(oDrd("UsrCreated"), Guid))
                    With .UsrCreated
                        .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .NickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                    End With
                    .FchCreated = oDrd("FchCreated")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTarifaDto.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTarifaDto As DTOTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTarifaDto, oTrans)
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


    Shared Sub Update(oTarifaDto As DTOTarifaDto, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TarifaDto ")
        sb.AppendLine("WHERE Guid='" & oTarifaDto.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTarifaDto.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTarifaDto
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("Contact") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("Value") = .Value
            oRow("Fch") = .Fch
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("UsrCreated") = .UsrCreated.Guid
            oRow("FchCreated") = .FchCreated
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTarifaDto As DTOTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTarifaDto, oTrans)
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


    Shared Sub Delete(oTarifaDto As DTOTarifaDto, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TarifaDto WHERE Guid='" & oTarifaDto.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


End Class

Public Class TarifaDtosLoader
    Shared Function All(oContact As DTOContact) As List(Of DTOTarifaDto)
        Dim retval As New List(Of DTOTarifaDto)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TarifaDto.Guid, TarifaDto.Product, TarifaDto.Contact, TarifaDto.Fch, TarifaDto.Value ")
        sb.AppendLine("FROM TarifaDto ")
        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("     SELECT MAX(Fch) AS Fch ")
        sb.AppendLine("     FROM tarifaDto ")
        sb.AppendLine("     GROUP BY Product,Contact ")
        sb.AppendLine("     ) X ON TarifaDto.Fch = X.Fch ")
        sb.AppendLine("INNER JOIN ProductParent ON TarifaDto.Product = ProductParent.ParentGuid ")
        sb.AppendLine("WHERE (TarifaDto.Contact IS NULL OR TarifaDto.Contact = '" & oContact.Guid.ToString & "') ")
        sb.AppendLine("ORDER BY TarifaDto.Product, TarifaDto.Contact")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTarifaDto(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Product")) Then
                    .Product = New DTOProductBrand(oDrd("Product"))
                End If
                If Not IsDBNull(oDrd("Contact")) Then
                    .Contact = New DTOContact(oDrd("Contact"))
                End If
                .Fch = oDrd("Fch")
                .Value = oDrd("Value")
            End With
        Loop
        Return retval
    End Function
End Class