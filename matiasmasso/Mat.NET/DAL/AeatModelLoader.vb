Public Class AeatModelLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid, oUser As DTOUser) As DTOAeatModel
        Dim retval As DTOAeatModel = Nothing
        Dim oAeatMod As New DTOAeatModel(oGuid)
        If Load(oAeatMod, oUser) Then
            retval = oAeatMod
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oAeatMod As DTOAeatModel, oUser As DTOUser) As Boolean
        If Not oAeatMod.IsLoaded And Not oAeatMod.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Aeat_Mod.*, Aeat.Fch, Aeat.Guid AS DocGuid ")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine("FROM Aeat_Mod ")
            sb.AppendLine("LEFT OUTER JOIN Aeat ON Aeat_Mod.Guid = Aeat.Model ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Aeat.Hash = VwDocfile.DocfileHash ")
            sb.AppendLine("WHERE Aeat_Mod.Guid='" & oAeatMod.Guid.ToString & "' ")
            sb.AppendLine("AND Aeat.Emp = " & oUser.Emp.Id & " ")
            sb.AppendLine("ORDER BY Aeat.Fch DESC ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oAeatMod.IsLoaded Then
                    With oAeatMod
                        .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                        .Nom = oDrd("Mod")
                        .Dsc = oDrd("Dsc")
                        .PeriodType = oDrd("TPeriod")
                        .SoloInfo = oDrd("SoloInfo")
                        .VisibleBancs = oDrd("VisibleBancs")
                        .Obsolet = oDrd("Obsolet")
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("DocfileHash")) Then
                    Dim oDocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    Dim oDoc As New DTOAeatDoc.Header
                    With oDoc
                        .Guid = oDrd("DocGuid")
                        .Fch = oDrd("Fch")
                        .DownloadUrl = oDocFile.DownloadUrl(True)
                        .ThumbnailUrl = oDocFile.ThumbnailUrl(True)
                        .Features = oDocFile.Features
                    End With
                    oAeatMod.Docs.Add(oDoc)
                End If
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = oAeatMod.IsLoaded
        Return retval
    End Function


    Shared Function Update(oAeatMod As DTOAeatModel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAeatMod, oTrans)
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


    Shared Sub Update(oAeatMod As DTOAeatModel, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Aeat_Mod ")
        sb.AppendLine("WHERE Guid='" & oAeatMod.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAeatMod.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAeatMod
            oRow("Cod") = SQLHelper.NullableInt(.Cod)
            oRow("Mod") = .Nom
            oRow("Dsc") = .Dsc
            oRow("TPeriod") = .PeriodType
            oRow("SoloInfo") = .SoloInfo
            oRow("VisibleBancs") = .VisibleBancs
            oRow("Obsolet") = .Obsolet
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAeatMod As DTOAeatModel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAeatMod, oTrans)
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


    Shared Sub Delete(oAeatMod As DTOAeatModel, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Aeat_Mod WHERE Guid='" & oAeatMod.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class AeatModelsLoader


    Shared Function All(oUser As DTOUser) As DTOAeatModel.Collection
        Dim retval As New DTOAeatModel.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Aeat_Mod.*, Aeat.Fch, Aeat.Guid AS DocGuid ")
        sb.AppendLine(", VwDocfile.* ")
        sb.AppendLine("FROM Aeat_Mod ")
        sb.AppendLine("LEFT OUTER JOIN Aeat ON Aeat_Mod.Guid = Aeat.Model ")
        sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Aeat.Hash = VwDocfile.DocfileHash ")
        sb.AppendLine("WHERE Aeat.Emp IS NULL OR Aeat.Emp = " & oUser.Emp.Id & " ")
        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
            Case DTORol.Ids.banc
                sb.AppendLine("AND Aeat_Mod.VisibleBancs = 1 ")
            Case Else
                sb.AppendLine("AND Aeat.Emp = -1 ")
        End Select
        sb.AppendLine("ORDER BY Aeat_Mod.Mod, Aeat.Fch DESC ")

        Dim oAeatModel As New DTOAeatModel
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oAeatModel.Guid.Equals(oDrd("Guid")) Then
                oAeatModel = New DTOAeatModel(oDrd("Guid"))
                With oAeatModel
                    .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Mod"))
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .PeriodType = oDrd("TPeriod")
                    .SoloInfo = oDrd("SoloInfo")
                    .VisibleBancs = oDrd("VisibleBancs")
                    .Obsolet = oDrd("Obsolet")
                    .IsLoaded = True
                End With
                retval.Add(oAeatModel)
            End If
            If Not IsDBNull(oDrd("DocfileHash")) Then
                Dim oDocFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                Dim oDoc As New DTOAeatDoc.Header
                With oDoc
                    .Guid = oDrd("DocGuid")
                    .Fch = oDrd("Fch")
                    .DownloadUrl = oDocFile.DownloadUrl(True)
                    .ThumbnailUrl = oDocFile.ThumbnailUrl(True)
                    .Features = oDocFile.Features
                End With
                oAeatModel.Docs.Add(oDoc)
            End If
        Loop
        oDrd.Close()

        Return retval
    End Function

End Class

