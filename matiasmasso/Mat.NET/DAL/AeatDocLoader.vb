Public Class AeatDocLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAeatDoc
        Dim retval As DTOAeatDoc = Nothing
        Dim oAeatDoc As New DTOAeatDoc(oGuid)
        If Load(oAeatDoc) Then
            retval = oAeatDoc
        End If
        Return retval
    End Function

    Shared Function LastFromModel(oEmp As DTOEmp, oCod As DTOAeatModel.Cods) As DTOAeatDoc
        Dim retval As DTOAeatDoc = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Aeat.Guid, Aeat.Period, Aeat.Fch, Aeat.Model, Aeat.Hash, DocFile.Mime ")
        sb.AppendLine(", Aeat_Mod.Guid AS ModelGuid, Aeat_Mod.Mod AS ModelNom, Aeat_Mod.Dsc AS ModelDsc")
        sb.AppendLine(", Aeat_Mod.Id AS ModelId, Aeat_Mod.TPeriod, Aeat_Mod.SoloInfo")
        sb.AppendLine("FROM Aeat ")
        sb.AppendLine("INNER JOIN Aeat_Mod ON Aeat.Model=Aeat_Mod.Guid ")
        sb.AppendLine("LEFT OUTER JOIN DocFile ON Aeat.Hash=DocFile.Hash ")
        sb.AppendLine("WHERE Aeat.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Aeat_Mod.Cod=" & CInt(oCod) & " ")
        sb.AppendLine("ORDER BY Aeat.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oModel As New DTOAeatModel(oDrd("ModelGuid"))
            With oModel
                .Cod = oDrd("ModelId")
                .Nom = oDrd("ModelNom")
                .Dsc = oDrd("ModelDsc")
                .PeriodType = oDrd("TPeriod")
                .SoloInfo = oDrd("SoloInfo")
            End With
            retval = New DTOAeatDoc(oDrd("Guid"))
            With retval
                .Emp = oEmp
                .Model = oModel
                .Period = oDrd("period")
                .Fch = oDrd("Fch")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                    .DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
            End With
        End If
        oDrd.Close()
        Return retval
    End Function
    Shared Function LastFromModel(oEmp As DTOEmp, oAeatModel As DTOAeatModel) As DTOAeatDoc
        Dim retval As DTOAeatDoc = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Aeat.Guid ")
        sb.AppendLine("FROM Aeat ")
        sb.AppendLine("WHERE Aeat.Model='" & oAeatModel.Guid.ToString & "' ")
        sb.AppendLine("AND Aeat.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Aeat.Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOAeatDoc(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oAeatDoc As DTOAeatDoc) As Boolean
        If Not oAeatDoc.IsLoaded And Not oAeatDoc.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Aeat.Guid, Aeat.Emp, Aeat.Period, Aeat.Fch, Aeat.Model, Aeat.Hash ")
            sb.AppendLine(", Aeat_Mod.Mod AS ModelNom, Aeat_Mod.Dsc AS ModelDsc")
            sb.AppendLine("FROM Aeat ")
            sb.AppendLine("INNER JOIN Aeat_Mod ON Aeat.Model=Aeat_Mod.Guid ")
            sb.AppendLine("WHERE Aeat.Guid='" & oAeatDoc.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oAeatDoc.Guid.ToString())
            If oDrd.Read Then
                Dim oModel As New DTOAeatModel(oDrd("Model"))
                With oModel
                    .Nom = oDrd("ModelNom")
                    .Dsc = oDrd("ModelDsc")
                End With

                With oAeatDoc
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Model = oModel
                    .Period = oDrd("period")
                    .Fch = oDrd("Fch")
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile(oDrd("Hash"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAeatDoc.IsLoaded
        Return retval
    End Function

    Shared Function Update(oAeatDoc As DTOAeatDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAeatDoc, oTrans)
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


    Shared Sub Update(oAeatDoc As DTOAeatDoc, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oAeatDoc.DocFile, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Aeat ")
        sb.AppendLine("WHERE Guid='" & oAeatDoc.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAeatDoc.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAeatDoc
            oRow("Model") = .Model.Guid
            oRow("period") = .Period
            oRow("Fch") = .Fch
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
            oRow("Emp") = .Emp.Id
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAeatDoc As DTOAeatDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAeatDoc, oTrans)
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


    Shared Sub Delete(oAeatDoc As DTOAeatDoc, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Aeat WHERE Guid='" & oAeatDoc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

End Class
Public Class AeatDocsLoader
    Shared Function All(oEmp As DTOEmp, oModel As DTOAeatModel) As List(Of DTOAeatDoc)
        Dim retval As New List(Of DTOAeatDoc)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Aeat.Guid, Aeat.Period, Aeat.Fch, Aeat.Model, Aeat.Hash, DocFile.Mime ")
        sb.AppendLine(", Aeat_Mod.Mod AS ModelNom, Aeat_Mod.Dsc AS ModelDsc")
        sb.AppendLine("FROM Aeat ")
        sb.AppendLine("INNER JOIN Aeat_Mod ON Aeat.Model=Aeat_Mod.Guid ")
        sb.AppendLine("LEFT OUTER JOIN DocFile ON Aeat.Hash=DocFile.Hash ")
        sb.AppendLine("WHERE Aeat.Model='" & oModel.Guid.ToString & "' ")
        sb.AppendLine("AND Aeat.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Aeat.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAeatDoc(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Model = oModel
                .Period = oDrd("period")
                .Fch = oDrd("Fch")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                    .DocFile.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("Mime"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Exercicis(oEmp As DTOEmp) As List(Of DTOExercici)
        Dim retval As New List(Of DTOExercici)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Year(Aeat.Fch) AS Year ")
        sb.AppendLine("FROM Aeat ")
        sb.AppendLine("WHERE Aeat.Emp=" & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Year(Aeat.Fch) ")
        sb.AppendLine("ORDER BY Year DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOExercici(oEmp, oDrd("Year"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
