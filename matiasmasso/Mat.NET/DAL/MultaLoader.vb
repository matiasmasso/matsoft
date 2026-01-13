Public Class MultaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOMulta
        Dim retval As DTOMulta = Nothing
        Dim oMulta As New DTOMulta(oGuid)
        If Load(oMulta) Then
            retval = oMulta
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oMulta As DTOMulta) As Boolean
        If Not oMulta.IsLoaded And Not oMulta.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Multa.Guid, Vehicle_Flota.Guid AS VehicleGuid, Vehicle_Flota.Matricula AS VehicleMatricula ")
            sb.AppendLine(", Vehicle_Flota.ModelGuid, Vehicle_Models.MarcaGuid, Vehicle_Models.Nom AS ModelNom, Vehicle_Marcas.Nom AS MarcaNom ")
            sb.AppendLine(", Multa.Emisor, Multa.Expedient, CliGral.FullNom AS EmisorFullNom ")
            sb.AppendLine(", Multa.Fch, Multa.Vto, Multa.Pagat, Multa.Eur ")
            sb.AppendLine("FROM Multa ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Multa.Emisor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Flota ON Multa.Subjecte = Vehicle_Flota.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Models ON Vehicle_Flota.ModelGuid = Vehicle_Models.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Marcas ON Vehicle_Models.MarcaGuid = Vehicle_Marcas.Guid ")
            sb.AppendLine("WHERE Multa.Guid='" & oMulta.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oMulta
                    If Not IsDBNull(oDrd("VehicleGuid")) Then
                        Dim oVehicle As New DTOVehicle(oDrd("VehicleGuid"))
                        oVehicle.Matricula = SQLHelper.GetStringFromDataReader(oDrd("VehicleMatricula"))
                        If Not IsDBNull(oDrd("ModelGuid")) Then
                            oVehicle.Model = New DTOVehicle.ModelClass(oDrd("ModelGuid"))
                            oVehicle.Model.Nom = oDrd("ModelNom")
                            If Not IsDBNull(oDrd("MarcaGuid")) Then
                                oVehicle.Model.Marca = New DTOVehicle.Marca(oDrd("MarcaGuid"))
                                oVehicle.Model.Marca.Nom = oDrd("MarcaNom")
                            End If

                        End If
                        .Subjecte = oVehicle
                        .Subjecte.Nom = oVehicle.MatriculaMarcaYModel
                    End If
                    If Not IsDBNull(oDrd("Emisor")) Then
                        .Emisor = New DTOContact(oDrd("Emisor"))
                        .Emisor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("EmisorFullNom"))
                    End If
                    .Expedient = SQLHelper.GetStringFromDataReader(oDrd("Expedient"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .Vto = SQLHelper.GetFchFromDataReader(oDrd("Vto"))
                    .Pagat = SQLHelper.GetFchFromDataReader(oDrd("Pagat"))
                    .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oMulta.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMulta As DTOMulta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMulta, oTrans)
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


    Shared Sub Update(oMulta As DTOMulta, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Multa ")
        sb.AppendLine("WHERE Guid='" & oMulta.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMulta.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMulta
            oRow("Subjecte") = SQLHelper.NullableBaseGuid(.Subjecte)
            oRow("Emisor") = SQLHelper.NullableBaseGuid(.Emisor)
            oRow("Expedient") = SQLHelper.NullableString(.Expedient)
            oRow("Fch") = SQLHelper.NullableFch(.Fch)
            oRow("Vto") = SQLHelper.NullableFch(.Vto)
            oRow("Pagat") = SQLHelper.NullableFch(.Pagat)
            oRow("Eur") = SQLHelper.NullableAmt(.Amt)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oMulta As DTOMulta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMulta, oTrans)
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


    Shared Sub Delete(oMulta As DTOMulta, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Multa WHERE Guid='" & oMulta.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class MultasLoader

    Shared Function All(oSubjecte As DTOBaseGuid) As List(Of DTOMulta)
        Dim retval As New List(Of DTOMulta)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Multa.Guid, Vehicle_Flota.Guid AS VehicleGuid, Vehicle_Flota.Matricula AS VehicleMatricula ")
        sb.AppendLine(", Multa.Emisor, Multa.Expedient, CliGral.FullNom AS EmisorFullNom ")
        sb.AppendLine(", Multa.Fch, Multa.Vto, Multa.Pagat, Multa.Eur ")
        sb.AppendLine("FROM Multa ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Multa.Emisor = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Vehicle_Flota ON Multa.Subjecte = Vehicle_Flota.Guid ")
        sb.AppendLine("WHERE Multa.Subjecte = '" & oSubjecte.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Multa.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMulta(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("VehicleGuid")) Then
                    .Subjecte = New DTOVehicle(oDrd("VehicleGuid"))
                    DirectCast(.Subjecte, DTOVehicle).Matricula = SQLHelper.GetStringFromDataReader(oDrd("VehicleMatricula"))
                End If
                If Not IsDBNull(oDrd("Emisor")) Then
                    .Emisor = New DTOContact(oDrd("Emisor"))
                    .Emisor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("EmisorFullNom"))
                End If
                .Expedient = SQLHelper.GetStringFromDataReader(oDrd("Expedient"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Vto = SQLHelper.GetFchFromDataReader(oDrd("Vto"))
                .Pagat = SQLHelper.GetFchFromDataReader(oDrd("Pagat"))
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
