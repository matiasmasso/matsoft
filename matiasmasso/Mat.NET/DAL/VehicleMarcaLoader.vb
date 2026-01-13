Public Class VehicleMarcaLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOVehicle.Marca
        Dim retval As DTOVehicle.Marca = Nothing
        Dim oVehicleMarca As New DTOVehicle.Marca(oGuid)
        If Load(oVehicleMarca) Then
            retval = oVehicleMarca
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oVehicleMarca As DTOVehicle.Marca) As Boolean
        If Not oVehicleMarca.IsLoaded And Not oVehicleMarca.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Vehicle_Marcas ")
            sb.AppendLine("WHERE Guid='" & oVehicleMarca.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oVehicleMarca
                    .Nom = oDrd("Nom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oVehicleMarca.IsLoaded
        Return retval
    End Function

    Shared Function Update(oVehicleMarca As DTOVehicle.Marca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oVehicleMarca, oTrans)
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


    Shared Sub Update(oVehicleMarca As DTOVehicle.Marca, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Vehicle_Marcas ")
        sb.AppendLine("WHERE Guid='" & oVehicleMarca.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oVehicleMarca.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oVehicleMarca
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oVehicleMarca As DTOVehicle.Marca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oVehicleMarca, oTrans)
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


    Shared Sub Delete(oVehicleMarca As DTOVehicle.Marca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Vehicle_Marcas WHERE Guid='" & oVehicleMarca.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class VehicleMarcasLoader

    Shared Function All() As List(Of DTOVehicle.Marca)
        Dim retval As New List(Of DTOVehicle.Marca)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Vehicle_Marcas.Guid AS MarcaGuid, Vehicle_Marcas.Nom AS MarcaNom ")
        sb.AppendLine(", Vehicle_Models.Guid AS ModelGuid, Vehicle_Models.Nom AS ModelNom ")
        sb.AppendLine("FROM Vehicle_Marcas ")
        sb.AppendLine("LEFT OUTER JOIN Vehicle_Models ON Vehicle_Marcas.Guid = Vehicle_Models.MarcaGuid ")
        sb.AppendLine("ORDER BY Vehicle_Marcas.Nom, Vehicle_Models.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oMarca As New DTOVehicle.Marca
        Do While oDrd.Read
            If Not oMarca.Guid.Equals(oDrd("MarcaGuid")) Then
                oMarca = New DTOVehicle.Marca(oDrd("MarcaGuid"))
                oMarca.Nom = SQLHelper.GetStringFromDataReader(oDrd("MarcaNom"))
                retval.Add(oMarca)
            End If
            If Not IsDBNull(oDrd("ModelGuid")) Then
                Dim item As New DTOVehicle.ModelClass(oDrd("ModelGuid"))
                With item
                    .Marca = oMarca
                    .Nom = oDrd("ModelNom")
                End With
                oMarca.Models.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oVehicleMarcas As List(Of DTOVehicle.Marca), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleMarcasLoader.Delete(oVehicleMarcas, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Vehicle_Marcas ")
        sb.AppendLine("WHERE (")
        For Each item As DTOVehicle.Marca In oVehicleMarcas
            If item.UnEquals(oVehicleMarcas.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class

