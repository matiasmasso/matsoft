Public Class CodiMercanciaLoader


#Region "CRUD"

    Shared Function Find(Id As String) As DTOCodiMercancia
        Dim retval As DTOCodiMercancia = Nothing
        Dim oCodiMercancia As New DTOCodiMercancia(Id)
        If Load(oCodiMercancia) Then
            retval = oCodiMercancia
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCodiMercancia As DTOCodiMercancia) As Boolean
        If Not oCodiMercancia.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM CodisMercancia ")
            sb.AppendLine("WHERE id='" & oCodiMercancia.Id & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCodiMercancia
                    .Dsc = oDrd("Dsc")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCodiMercancia.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCodiMercancia As DTOCodiMercancia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCodiMercancia, oTrans)
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


    Shared Sub Update(oCodiMercancia As DTOCodiMercancia, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CodisMercancia ")
        sb.AppendLine("WHERE id='" & oCodiMercancia.Id.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = oCodiMercancia.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oCodiMercancia
            oRow("Dsc") = .Dsc
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCodiMercancia As DTOCodiMercancia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCodiMercancia, oTrans)
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


    Shared Sub Delete(oCodiMercancia As DTOCodiMercancia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CodisMercancia WHERE Id='" & oCodiMercancia.Id & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Products(oCodiMercancia As DTOCodiMercancia) As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.Guid, Cod, BrandGuid, BrandNom, CategoryGuid, CategoryNom, SkuGuid, SkuNom, CodiMercancia ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("SELECT Art.Guid AS Guid, Art.CodiMercancia ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Stp.Guid, Stp.CodiMercancia ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Tpa.Guid, Tpa.CodiMercancia ")
        sb.AppendLine("FROM  Tpa) X ")
        sb.AppendLine("INNER JOIN VwProductNom ON X.Guid = VwProductNom.Guid ")
        sb.AppendLine("WHERE X.CodiMercancia='" & oCodiMercancia.id & "' ")
        sb.AppendLine("ORDER BY  BrandNom, CategoryNom, SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProduct = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class CodisMercanciaLoader

    Shared Function All() As List(Of DTOCodiMercancia)
        Dim retval As New List(Of DTOCodiMercancia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.Id, CodisMercancia.Dsc ")
        sb.AppendLine("FROM  ( ")
        sb.AppendLine("SELECT CodisMercancia.Id FROM CodisMercancia ")
        sb.AppendLine("UNION SELECT Art.Codimercancia FROM Art ")
        sb.AppendLine("UNION SELECT Stp.Codimercancia FROM Stp ")
        sb.AppendLine("UNION SELECT Tpa.Codimercancia FROM Tpa ")
        sb.AppendLine(") X ")
        sb.AppendLine("LEFT OUTER JOIN CodisMercancia ON X.Id = CodisMercancia.Id ")
        sb.AppendLine("WHERE X.Id IS NOT NULL AND X.Id<>'' ")
        sb.AppendLine("ORDER BY X.Id ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCodiMercancia(oDrd("Id"))
            With item
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

