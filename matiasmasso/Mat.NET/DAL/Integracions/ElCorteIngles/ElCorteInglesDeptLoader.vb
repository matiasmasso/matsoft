Public Class ElCorteInglesDeptLoader

    Shared Function Find(oGuid As Guid) As DTO.Integracions.ElCorteIngles.Dept
        Dim retval As DTO.Integracions.ElCorteIngles.Dept = Nothing
        Dim oEciDept As New DTO.Integracions.ElCorteIngles.Dept(oGuid)
        If Load(oEciDept) Then
            retval = oEciDept
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEciDept As DTO.Integracions.ElCorteIngles.Dept) As Boolean
        If Not oEciDept.IsLoaded And Not oEciDept.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EciDept.Nom, EciDept.Id, EciDept.Tel ")
            sb.AppendLine(", EciDept.Manager, Manager.Adr AS ManagerAdr, Manager.Nickname AS ManagerNickname ")
            sb.AppendLine(", EciDept.Assistant, Assistant.Adr AS AssistantAdr, Assistant.Nickname AS AssistantNickname ")
            sb.AppendLine(", EciDept.PlantillaModSkuWeekDays ")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine("FROM EciDept ")
            sb.AppendLine("LEFT OUTER JOIN Email Manager ON EciDept.Manager = Manager.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email Assistant ON EciDept.Assistant = Assistant.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON EciDept.PlantillaModSkuDocfile = VwDocfile.DocfileHash ")
            sb.AppendLine("WHERE EciDept.Guid='" & oEciDept.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oEciDept
                    .Id = oDrd("Id")
                    .Nom = oDrd("Nom")
                    .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                    If Not IsDBNull(oDrd("Manager")) Then
                        .Manager = New DTOUser(oDrd("Manager"))
                        .Manager.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("ManagerAdr"))
                        .Manager.NickName = SQLHelper.GetStringFromDataReader(oDrd("ManagerNickname"))
                    End If
                    If Not IsDBNull(oDrd("Assistant")) Then
                        .Assistant = New DTOUser(oDrd("Assistant"))
                        .Assistant.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("AssistantAdr"))
                        .Assistant.NickName = SQLHelper.GetStringFromDataReader(oDrd("AssistantNickname"))
                    End If
                    .PlantillaModSkuWeekDays = SQLHelper.GetWeekdaysFromDataReader(oDrd("PlantillaModSkuWeekDays"))
                    .PlantillaModSkuDocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEciDept.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEciDept As DTO.Integracions.ElCorteIngles.Dept, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DocFileLoader.Update(oEciDept.PlantillaModSkuDocFile, oTrans)
            Update(oEciDept, oTrans)

            oTrans.Commit()
            oEciDept.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oEciDept As DTO.Integracions.ElCorteIngles.Dept, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EciDept ")
        sb.AppendLine("WHERE Guid='" & oEciDept.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEciDept.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEciDept
            oRow("Id") = .Id
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Tel") = SQLHelper.NullableString(.Tel)
            oRow("Manager") = SQLHelper.NullableBaseGuid(.Manager)
            oRow("Assistant") = SQLHelper.NullableBaseGuid(.Assistant)
            oRow("PlantillaModSkuWeekDays") = SQLHelper.NullableWeekdays(.PlantillaModSkuWeekDays)
            oRow("PlantillaModSkuDocFile") = SQLHelper.NullableDocFile(.PlantillaModSkuDocFile)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEciDept As DTO.Integracions.ElCorteIngles.Dept, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEciDept, oTrans)
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


    Shared Sub Delete(oEciDept As DTO.Integracions.ElCorteIngles.Dept, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EciDept WHERE Guid='" & oEciDept.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class ElCorteInglesDeptsLoader

    Shared Function All() As List(Of DTO.Integracions.ElCorteIngles.Dept)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.Dept)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EciDept.Guid, EciDept.Id, EciDept.Nom ")
        sb.AppendLine(", EciDept.PlantillaModSkuWeekDays, EciDept.PlantillaModSkuDocFile ")
        sb.AppendLine("FROM EciDept ")
        sb.AppendLine("ORDER BY EciDept.Id")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTO.Integracions.ElCorteIngles.Dept(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Nom = oDrd("Nom")
                .PlantillaModSkuWeekDays = SQLHelper.GetWeekdaysFromDataReader(oDrd("PlantillaModSkuWeekDays"))
                If Not IsDBNull(oDrd("PlantillaModSkuDocFile")) Then
                    .PlantillaModSkuDocFile = New DTODocFile(oDrd("PlantillaModSkuDocFile"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
