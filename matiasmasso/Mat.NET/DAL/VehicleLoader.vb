Public Class VehicleLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOVehicle
        Dim retval As DTOVehicle = Nothing
        Dim oVehicle As New DTOVehicle(oGuid)
        If Load(oVehicle) Then
            retval = oVehicle
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oVehicle As DTOVehicle) As Boolean
        If Not oVehicle.IsLoaded And Not oVehicle.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Vehicle_Flota.* ")
            sb.AppendLine(", Vehicle_Marcas.Nom AS MarcaNom, Vehicle_Models.Nom AS ModelNom, Vehicle_Models.MarcaGuid ")
            sb.AppendLine(", Conductor.FullNom AS ConductorNom, Venedor.FullNom AS VenedorNom ")
            sb.AppendLine(", Contract.Num AS ContractNum, Contract.ContactGuid AS ContractCli, NomContract.RaoSocial AS ContractNom ")
            sb.AppendLine(", Insurance.Num AS InsuranceNum, Insurance.ContactGuid AS InsuranceCli, NomInsurance.RaoSocial AS InsuranceNom ")
            sb.AppendLine("FROM Vehicle_Flota ")
            sb.AppendLine("LEFT OUTER JOIN CliGral Conductor ON Vehicle_Flota.ConductorGuid=Conductor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral Venedor ON Vehicle_Flota.VenedorGuid=Venedor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Contract ON Vehicle_Flota.Contracte=Contract.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral NomContract ON Contract.ContactGuid=NomContract.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Contract Insurance ON Vehicle_Flota.Insurance=Insurance.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral NomInsurance ON Insurance.ContactGuid=NomInsurance.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Models ON Vehicle_Flota.ModelGuid=Vehicle_Models.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Vehicle_Marcas ON Vehicle_Models.MarcaGuid=Vehicle_Marcas.Guid ")
            sb.AppendLine("WHERE Vehicle_Flota.Guid='" & oVehicle.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oMarca As New DTOVehicle.Marca(oDrd("MarcaGuid"))
                oMarca.Nom = oDrd("MarcaNom")

                Dim oModel As New DTOVehicle.ModelClass(oDrd("ModelGuid"))
                oModel.Nom = SQLHelper.GetStringFromDataReader(oDrd("ModelNom"))
                oModel.Marca = oMarca

                With oVehicle
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Model = oModel
                    .Matricula = oDrd("Matricula")
                    .Bastidor = SQLHelper.GetStringFromDataReader(oDrd("Bastidor"))

                    If Not IsDBNull(oDrd("Contracte")) Then
                        .Contract = New DTOContract(New Guid(oDrd("Contracte").ToString()))
                        .Contract.Num = SQLHelper.GetStringFromDataReader(oDrd("ContractNum"))
                        .Contract.Contact = New DTOContact(oDrd("ContractCli"))
                        .Contract.Contact.Nom = oDrd("ContractNom")
                    End If

                    If Not IsDBNull(oDrd("Insurance")) Then
                        .Insurance = New DTOContract(New Guid(oDrd("Insurance").ToString()))
                        .Insurance.Num = SQLHelper.GetStringFromDataReader(oDrd("InsuranceNum"))
                        .Insurance.Contact = New DTOContact(oDrd("InsuranceCli"))
                        .Insurance.Contact.Nom = oDrd("InsuranceNom")
                    End If

                    If Not IsDBNull(oDrd("ConductorGuid")) Then
                        .Conductor = New DTOContact(oDrd("ConductorGuid"))
                        .Conductor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("ConductorNom"))
                    End If

                    If Not IsDBNull(oDrd("VenedorGuid")) Then
                        .Venedor = New DTOContact(oDrd("VenedorGuid"))
                        .Venedor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("VenedorNom"))
                    End If

                    .Alta = oDrd("Alta")
                    .Baixa = SQLHelper.GetFchFromDataReader(oDrd("Baixa"))
                    .Privat = CBool(oDrd("Privat"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oVehicle.IsLoaded
        Return retval
    End Function


    Shared Function Image(ByRef oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Vehicle_Flota.Image ")
        sb.AppendLine("FROM Vehicle_Flota ")
        sb.AppendLine("WHERE Vehicle_Flota.Guid='" & oGuid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Image")) Then
                retval = oDrd("Image")
            End If
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oVehicle As DTOVehicle, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oVehicle, oTrans)
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


    Shared Sub Update(oVehicle As DTOVehicle, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Vehicle_Flota ")
        sb.AppendLine("WHERE Guid='" & oVehicle.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oVehicle.Guid
            oRow("Emp") = oVehicle.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oVehicle
            oRow("ModelGuid") = .Model.Guid
            oRow("Matricula") = .Matricula
            oRow("Bastidor") = SQLHelper.NullableString(.Bastidor)
            oRow("Contracte") = SQLHelper.NullableBaseGuid(.Contract)
            oRow("Insurance") = SQLHelper.NullableBaseGuid(.Insurance)
            oRow("ConductorGuid") = SQLHelper.NullableBaseGuid(.Conductor)
            oRow("VenedorGuid") = SQLHelper.NullableBaseGuid(.Venedor)

            oRow("Alta") = .Alta
            oRow("Baixa") = SQLHelper.NullableFch(.Baixa)
            oRow("Privat") = .Privat
            oRow("Image") = .Image
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oVehicle As DTOVehicle, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oVehicle, oTrans)
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


    Shared Sub Delete(oVehicle As DTOVehicle, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Vehicle_Flota WHERE Guid='" & oVehicle.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class VehiclesLoader

    Shared Function All(oUser As DTOUser) As List(Of DTOVehicle)
        Dim retval As New List(Of DTOVehicle)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Vehicle_Flota.Guid, Vehicle_Flota.ModelGuid, Vehicle_Flota.ConductorGuid, Vehicle_Flota.Matricula, Vehicle_Flota.Bastidor ")
        sb.AppendLine(", Vehicle_Flota.Alta, Vehicle_Flota.Baixa ")
        sb.AppendLine(", Vehicle_Models.Nom AS ModelNom, Vehicle_Models.MarcaGuid ")
        sb.AppendLine(", Vehicle_Marcas.Nom AS MarcaNom, CliGral.RaoSocial ")
        sb.AppendLine(", Vehicle_Flota.Emp, Emp.Nom AS Titular ")
        sb.AppendLine("FROM Vehicle_Flota ")
        sb.AppendLine("INNER JOIN Vehicle_Models ON Vehicle_Flota.ModelGuid = Vehicle_Models.Guid ")
        sb.AppendLine("INNER JOIN Vehicle_Marcas ON Vehicle_Models.MarcaGuid = Vehicle_Marcas.Guid ")
        sb.AppendLine("INNER JOIN Email ON Vehicle_Flota.Emp = Email.Emp ")
        sb.AppendLine("INNER JOIN Emp ON Vehicle_Flota.Emp = Emp.Emp ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Vehicle_Flota.ConductorGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Email.adr = '" & oUser.EmailAddress & "' ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
            Case DTORol.Ids.SalesManager
                sb.AppendLine("AND (CliGral.Rol = " & DTORol.Ids.SalesManager & " ")
                sb.AppendLine("OR CliGral.Rol = " & DTORol.Ids.Comercial & ") ")
            Case DTORol.Ids.Comercial
                sb.AppendLine("AND Vehicle_Flota.ConductorGuid = '" & oUser.Contact.Guid.ToString & "' ")
            Case Else
                sb.AppendLine("WHERE 1 = 2 ")
        End Select

        sb.AppendLine("ORDER BY Vehicle_Flota.Alta DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVehicle(oDrd("Guid"))
            With item
                .emp = New DTOEmp(oDrd("Emp"))
                .emp.nom = oDrd("Titular")
                .matricula = oDrd("Matricula")
                .Bastidor = SQLHelper.GetStringFromDataReader(oDrd("Bastidor"))
                .Model = New DTOVehicle.ModelClass(oDrd("ModelGuid"))
                .Model.Nom = oDrd("ModelNom")
                .Model.Marca = New DTOVehicle.Marca(oDrd("MarcaGuid"))
                .Model.Marca.Nom = oDrd("MarcaNom")
                If Not IsDBNull(oDrd("ConductorGuid")) Then
                    .Conductor = New DTOContact(oDrd("ConductorGuid"))
                    .Conductor.FullNom = oDrd("RaoSocial")
                End If
                .Alta = SQLHelper.GetFchFromDataReader(oDrd("Alta"))
                .Baixa = SQLHelper.GetFchFromDataReader(oDrd("Baixa"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oVehicles As List(Of DTOVehicle), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehiclesLoader.Delete(oVehicles, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Vehicle_Flota ")
        sb.AppendLine("WHERE (")
        For Each item As DTOVehicle In oVehicles
            If item.UnEquals(oVehicles.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class

