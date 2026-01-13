Public Class VehicleModelLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOVehicle.ModelClass
        Dim retval As DTOVehicle.ModelClass = Nothing
        Dim oVehicleModel As New DTOVehicle.ModelClass(oGuid)
        If Load(oVehicleModel) Then
            retval = oVehicleModel
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oVehicleModel As DTOVehicle.ModelClass) As Boolean
        If Not oVehicleModel.IsLoaded And Not oVehicleModel.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Vehicle_Models.Guid, Vehicle_Models.MarcaGuid, Vehicle_Models.Nom AS ModelNom ")
            sb.AppendLine(", Vehicle_Marcas.Nom AS MarcaNom ")
            sb.AppendLine("FROM Vehicle_Models ")
            sb.AppendLine("INNER JOIN Vehicle_Marcas ON Vehicle_Models.MarcaGuid=Vehicle_Marcas.Guid ")
            sb.AppendLine("WHERE Vehicle_Models.Guid='" & oVehicleModel.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oMarca As New DTOVehicle.Marca(oDrd("MarcaGuid"))

                oMarca.Nom = oDrd("MarcaNom")
                With oVehicleModel
                    .Marca = oMarca
                    .Nom = oDrd("ModelNom")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oVehicleModel.IsLoaded
        Return retval
    End Function

    Shared Function Update(oVehicleModel As DTOVehicle.ModelClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oVehicleModel, oTrans)
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


    Shared Sub Update(oVehicleModel As DTOVehicle.ModelClass, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Vehicle_Models ")
        sb.AppendLine("WHERE Guid='" & oVehicleModel.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oVehicleModel.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oVehicleModel
            oRow("MarcaGuid") = .Marca.Guid
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oVehicleModel As DTOVehicle.ModelClass, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oVehicleModel, oTrans)
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


    Shared Sub Delete(oVehicleModel As DTOVehicle.ModelClass, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Vehicle_Models WHERE Guid='" & oVehicleModel.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class VehicleModelsLoader

    Shared Function All(oMarca As DTOVehicle.Marca) As List(Of DTOVehicle.ModelClass)
        Dim retval As New List(Of DTOVehicle.ModelClass)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Nom ")
        sb.AppendLine("FROM Vehicle_Models ")
        sb.AppendLine("WHERE MarcaGuid='" & oMarca.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVehicle.ModelClass(oDrd("Guid"))
            With item
                .Marca = oMarca
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oVehicleModels As List(Of DTOVehicle.ModelClass), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleModelsLoader.Delete(oVehicleModels, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Vehicle_Models ")
        sb.AppendLine("WHERE (")
        For Each item As DTOVehicle.ModelClass In oVehicleModels
            If item.UnEquals(oVehicleModels.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class

