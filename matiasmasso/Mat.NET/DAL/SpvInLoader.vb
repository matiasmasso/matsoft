Public Class SpvInLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSpvIn
        Dim retval As DTOSpvIn = Nothing
        Dim oSpvIn As New DTOSpvIn(oGuid)
        If Load(oSpvIn) Then
            retval = oSpvIn
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSpvIn As DTOSpvIn) As Boolean
        If Not oSpvIn.IsLoaded And Not oSpvIn.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SpvIn.* ")
            sb.AppendLine(", Spv.Guid AS SpvGuid, Spv.Id AS SpvId, Spv.FchAvis ")
            sb.AppendLine(", Spv.CliGuid, CliGral.FullNom ")
            sb.AppendLine(", Spv.ProductGuid, VwSkuNom.* ")
            sb.AppendLine("FROM SpvIn ")
            sb.AppendLine("LEFT OUTER JOIN Spv ON SpvIn.Guid = Spv.SpvIn ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Spv.CliGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON (Spv.ProductGuid = VwSkuNom.SkuGuid OR VwSkuNom.CategoryGuid = Spv.ProductGuid OR VwSkuNom.BrandGuid = Spv.ProductGuid) ")
            sb.AppendLine("WHERE SpvIn.Guid='" & oSpvIn.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oSpvIn.IsLoaded Then
                    With oSpvIn
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Id = oDrd("Id")
                        .Fch = oDrd("Fch")
                        .Expedicio = oDrd("Expedicio")
                        .Bultos = oDrd("Bultos")
                        .Kg = oDrd("Kg")
                        .M3 = SQLHelper.GetDecimalFromDataReader(oDrd("M3"))
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        If Not IsDBNull(oDrd("UsrGuid")) Then
                            .User = New DTOUser(DirectCast(oDrd("UsrGuid"), Guid))
                        End If
                        .IsLoaded = True
                    End With
                End If
                Dim oSpv As New DTOSpv(oDrd("SpvGuid"))
                With oSpv
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Id = CInt(oDrd("Id"))
                    .FchAvis = oDrd("FCHAVIS")

                    If Not IsDBNull(oDrd("CliGuid")) Then
                        .Customer = New DTOCustomer(oDrd("CliGuid"))
                        .customer.FullNom = oDrd("FullNom")
                    End If

                    .Product = SQLHelper.GetProductFromDataReader(oDrd)
                    If TypeOf .Product Is DTOProductSku Then
                        .Product.Nom = DirectCast(.Product, DTOProductSku).nomLlarg
                    ElseIf TypeOf .Product Is DTOProductCategory Then
                        .Product.Nom = String.Format("{0} {1}", DirectCast(.Product, DTOProductCategory).Brand.Nom, .Product.Nom)
                    End If
                End With
                oSpvIn.Spvs.Add(oSpv)
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oSpvIn.IsLoaded
        Return retval
    End Function

    Shared Function Find(oEmp As DTOEmp, yea As Integer, id As Integer) As DTOSpvIn
        Dim retval As DTOSpvIn = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SpvIn ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND YEAR(Fch)=" & yea & " ")
        sb.AppendLine("WHERE Id=" & id & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSpvIn(oDrd("Guid"))
            With retval
                .Emp = oEmp
                .Id = id
                .Fch = oDrd("Fch")
                .Expedicio = oDrd("Expedicio")
                .Bultos = oDrd("Bultos")
                .Kg = oDrd("Kg")
                .M3 = SQLHelper.GetDecimalFromDataReader(oDrd("M3"))
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                If Not IsDBNull(oDrd("UsrGuid")) Then
                    .User = New DTOUser(DirectCast(oDrd("UsrGuid"), Guid))
                End If
                .IsLoaded = True
            End With
        End If

        oDrd.Close()

        Return retval
    End Function


    Shared Function Update(ByRef oSpvIn As DTOSpvIn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSpvIn, oTrans)
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

    Shared Sub Update(ByRef oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction)
        UpdateHeader(oSpvIn, oTrans)
        If oSpvIn.Spvs IsNot Nothing Then
            RemoveSpvs(oSpvIn, oTrans)
            UpdateSpvs(oSpvIn, oTrans)
        End If
    End Sub

    Shared Sub UpdateSpvs(oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET SpvIn='" & oSpvIn.Guid.ToString & "' ")
        sb.AppendLine("WHERE (")
        For Each oSpv As DTOSpv In oSpvIn.Spvs
            If oSpv.UnEquals(oSpvIn.Spvs.First) Then
                sb.AppendLine("OR ")
            End If
            sb.AppendLine("Spv.Guid='" & oSpv.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub RemoveSpvs(oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET SpvIn=NULL ")
        sb.AppendLine("WHERE SpvIn='" & oSpvIn.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub UpdateHeader(ByRef oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SpvIn ")
        sb.AppendLine("WHERE Guid='" & oSpvIn.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSpvIn.Guid
            oRow("Emp") = oSpvIn.Emp.Id
            If oSpvIn.Id = 0 Then oSpvIn.Id = LastId(oSpvIn, oTrans) + 1
        Else
            oRow = oTb.Rows(0)
        End If

        With oSpvIn
            oRow("Id") = .Id
            oRow("Fch") = .Fch
            oRow("Yea") = .Fch.Year
            oRow("Expedicio") = .Expedicio
            oRow("Bultos") = .Bultos
            oRow("Kg") = .Kg
            oRow("M3") = SQLHelper.NullableDecimal(.M3)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("UsrGuid") = SQLHelper.NullableBaseGuid(.User)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSpvIn As DTOSpvIn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSpvIn, oTrans)
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


    Shared Sub Delete(oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction)
        RemoveSpvs(oSpvIn, oTrans)
        Dim SQL As String = "DELETE SpvIn WHERE Guid='" & oSpvIn.Guid.ToString & "' "
        Dim rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LastId(oSpvIn As DTOSpvIn, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Id AS LastId ")
        sb.AppendLine("FROM SpvIn ")
        sb.AppendLine("WHERE Emp =" & oSpvIn.Emp.Id & " ")
        sb.AppendLine("AND Yea=" & Year(oSpvIn.Fch) & " ")
        sb.AppendLine("ORDER BY Id DESC")
        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function

#End Region

End Class

Public Class SpvInsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOSpvIn)
        Dim retval As New List(Of DTOSpvIn)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SpvIn.Guid, SpvIn.Id, SpvIn.Fch, SpvIn.Expedicio, SpvIn.Bultos, SpvIn.m3, Count(Spv.Id) AS SpvCount  ")
        sb.AppendLine("FROM SpvIn ")
        sb.AppendLine("LEFT OUTER JOIN Spv ON SpvIn.Guid=Spv.Spvin ")
        sb.AppendLine("WHERE SpvIn.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY SpvIn.Guid, SpvIn.Id, SpvIn.Fch, SpvIn.Expedicio, SpvIn.Bultos, SpvIn.m3 ")
        sb.AppendLine("ORDER BY SpvIn.Fch DESC, SpvIn.Id DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSpvIn(oDrd("Guid"))
            With item
                '.Emp = oEmp per no carregar el httpresponse
                .Id = oDrd("Id")
                .Fch = oDrd("Fch")
                .Expedicio = oDrd("Expedicio")
                .Bultos = oDrd("Bultos")
                .M3 = SQLHelper.GetDecimalFromDataReader(oDrd("m3"))
                .SpvCount = SQLHelper.GetIntegerFromDataReader(oDrd("SpvCount"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
