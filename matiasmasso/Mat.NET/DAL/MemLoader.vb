Public Class MemLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOMem
        Dim retval As DTOMem = Nothing
        Dim oMem As New DTOMem(oGuid)
        If Load(oMem) Then
            retval = oMem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oMem As DTOMem) As Boolean
        If Not oMem.IsLoaded And Not oMem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Mem.*, CliGral.FullNom ")
            sb.AppendLine(", UsrCreated.Nom AS UsrCreatedNickname ")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine("FROM Mem ")
            sb.AppendLine("INNER JOIN CliGral ON Mem.Contact=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN DocfileSrc ON Mem.Guid = DocfileSrc.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON DocfileSrc.Hash = VwDocfile.DocfileHash ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname UsrCreated ON Mem.UsrCreated=UsrCreated.Guid ")
            sb.AppendLine("WHERE Mem.Guid='" & oMem.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY DocfileSrc.SrcOrd ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read

                If Not oMem.IsLoaded Then
                    With oMem
                        .Contact = New DTOGuidNom(oDrd("Contact"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                        .Fch = oDrd("Fch")
                        .Text = oDrd("Mem")
                        .Cod = oDrd("Cod")
                        .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                        .Docfiles = New List(Of DTODocFile)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("DocfileHash")) Then
                    Dim oDocfile = SQLHelper.GetDocFileFromDataReader(oDrd)
                    oMem.docfiles.Add(oDocfile)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oMem.IsLoaded
        Return retval
    End Function

    Shared Function SpriteImages(ByRef oMem As DTOMem) As List(Of Byte())
        Dim retval As New List(Of Byte())

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BF.Thumbnail ")
        sb.AppendLine("FROM Mem ")
        sb.AppendLine("INNER JOIN DocfileSrc ON Mem.Guid = DocfileSrc.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN DocFile BF ON DocfileSrc.Hash = BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
        sb.AppendLine("WHERE Mem.Guid='" & oMem.Guid.ToString & "'")
        sb.AppendLine("ORDER BY DocfileSrc.SrcOrd ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oThumbnail = oDrd("Thumbnail")
            retval.Add(oThumbnail)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function Update(oMem As DTOMem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMem, oTrans)
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

    Shared Sub Update(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        UpdateHeader(oMem, oTrans)
        UpdateDocfiles(oMem, oTrans)
        For Each oDocfile In oMem.docfiles.Where(Function(x) x IsNot Nothing)
            DocFileLoader.Update(oDocfile, oTrans)
        Next
    End Sub

    Shared Sub UpdateDocfiles(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        DeleteDocfiles(oMem, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DocfileSrc ")
        sb.AppendLine("WHERE SrcGuid = '" & oMem.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oDocfile In oMem.docfiles
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("SrcGuid") = oMem.Guid
            oRow("SrcOrd") = oTb.Rows.Count
            oRow("SrcCod") = DTODocFile.Cods.Mem
            oRow("Hash") = oDocfile.hash
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateHeader(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Mem ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oMem.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMem
            oRow("Fch") = .fch
            oRow("Contact") = SQLHelper.NullableBaseGuid(.contact)
            oRow("Mem") = .text
            oRow("Cod") = .Cod
            oRow("UsrCreated") = SQLHelper.NullableBaseGuid(.UsrLog.usrCreated)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oMem As DTOMem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMem, oTrans)
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

    Shared Sub Delete(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        DeleteDocfiles(oMem, oTrans)
        DeleteHeaders(oMem, oTrans)
    End Sub

    Shared Sub DeleteDocfiles(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE DocfileSrc WHERE SrcGuid = '" & oMem.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeaders(oMem As DTOMem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Mem WHERE Guid='" & oMem.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class MemsLoader

    Shared Function All(Optional oContact As DTOContact = Nothing, Optional fromRep As DTORep = Nothing, Optional oCod As DTOMem.Cods = DTOMem.Cods.NotSet, Optional oUser As DTOUser = Nothing, Optional Offset As Integer = 0, Optional MaxCount As Integer = 0, Optional OnlyFromLast24H As Boolean = False, Optional year As Integer = False) As List(Of DTOMem)
        Dim retval As New List(Of DTOMem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Mem.Guid, Mem.Fch, Mem.Contact, Mem.Mem ")
        sb.Append(", Mem.Cod, Mem.FchCreated, Mem.UsrCreated ")
        sb.Append(", CliGral.FullNom ")
        sb.Append(", Docfilesrc.Hash ")
        sb.AppendLine(", VwUsrNickname.Nom AS UsrCreatedNickname ")
        sb.AppendLine("FROM Mem ")
        sb.AppendLine("INNER JOIN CliGral ON Mem.Contact=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Docfilesrc ON Mem.Guid=Docfilesrc.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname ON Mem.UsrCreated=VwUsrNickname.Guid ")
        If fromRep IsNot Nothing Then
            sb.AppendLine("INNER JOIN Email_Clis ON Mem.UsrCreated = Email_Clis.EmailGuid AND Email_Clis.ContactGuid = '" & fromRep.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Mem.Cod=Mem.Cod ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Mem.Contact='" & oContact.Guid.ToString & "' ")
        End If
        If oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.comercial, DTORol.Ids.rep
                    sb.AppendLine("AND Mem.UsrCreated='" & oUser.Guid.ToString & "' ")
                    If oCod <> DTOMem.Cods.NotSet Then
                        sb.AppendLine("AND Mem.Cod=" & CInt(oCod) & " ")
                    End If
            End Select
        End If
        If OnlyFromLast24H Then
            sb.AppendLine("AND Mem.FchCreated > DATEADD(hh,-24,GETDATE()) ")
        End If
        If year <> 0 Then
            sb.AppendLine("AND YEAR(Mem.Fch)=" & year & " ")
        End If
        sb.AppendLine("ORDER BY Mem.FchCreated DESC ")
        sb.AppendLine("OFFSET " & Offset & " ROWS ")
        If MaxCount > 0 Then
            sb.AppendLine("FETCH NEXT " & MaxCount & " ROWS ONLY ")
        End If
        Dim SQL As String = sb.ToString
        Dim oMem As New DTOMem
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oMem.Guid.Equals(oDrd("Guid")) Then
                oMem = New DTOMem(oDrd("Guid"))
                With oMem
                    .Contact = New DTOGuidNom(oDrd("Contact"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                    .Fch = oDrd("Fch")
                    .Text = Left(SQLHelper.GetStringFromDataReader(oDrd("Mem")), 100)
                    .Cod = oDrd("Cod")
                    .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
                End With
                retval.Add(oMem)
            End If

            If Not IsDBNull(oDrd("Hash")) Then
                Dim oDocfile As New DTODocFile(oDrd("Hash"))
                oMem.Docfiles.Add(oDocfile)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Count(Optional oContact As DTOContact = Nothing, Optional oCod As DTOMem.Cods = DTOMem.Cods.NotSet, Optional oUser As DTOUser = Nothing) As Integer
        Dim retval As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT COUNT(Mem.Guid) AS MemsCount ")
        sb.AppendLine("FROM Mem ")
        sb.AppendLine("WHERE Mem.Cod=Mem.Cod ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Mem.Contact='" & oContact.Guid.ToString & "' ")
        End If
        If oCod <> DTOMem.Cods.NotSet Then
            sb.AppendLine("AND Mem.Cod=" & CInt(oCod) & " ")
        End If
        If oUser IsNot Nothing Then
            Select Case oUser.Rol.Id
                Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                    sb.AppendLine("AND Mem.UsrCreated='" & oUser.Guid.ToString & "' ")
            End Select
        End If
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        If Not IsDBNull(oDrd("MemsCount")) Then
            retval = oDrd("MemsCount")
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Impagats(oEmp As DTOEmp) As List(Of DTOMem)
        Dim retval As New List(Of DTOMem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Mem.Guid, Mem.Contact, CliGral.RaoSocial, Mem.Fch, Mem.Mem, Mem.FchCreated ")
        sb.AppendLine(", Mem.UsrCreated, VwUsrNickname.Nom AS UsrCreatedNickname ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("     SELECT Csb.CliGuid ")
        sb.AppendLine("     FROM Csb  ")
        sb.AppendLine("     INNER JOIN Impagats ON Csb.Guid=Impagats.CsbGuid ")
        sb.AppendLine("     WHERE Impagats.Status<" & CInt(DTOMem.Cods.Impagos) & " ")
        sb.AppendLine("     GROUP BY Csb.CliGuid) X ")
        sb.AppendLine("INNER JOIN Mem ON Mem.Contact=X.CliGuid  ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname ON Mem.UsrCreated=VwUsrNickname.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Mem.Contact=CliGral.Guid ")
        sb.AppendLine("WHERE Mem.cod=2 ")
        sb.AppendLine("ORDER BY Mem.Contact, Mem.Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMem(oDrd("Guid"))
            With item
                .Contact = New DTOGuidNom(oDrd("Contact"), SQLHelper.GetStringFromDataReader(oDrd("RaoSocial")))
                .Fch = oDrd("Fch")
                .Text = SQLHelper.GetStringFromDataReader(oDrd("Mem"))
                .Cod = DTOMem.Cods.Impagos
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

