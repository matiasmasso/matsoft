Public Class EdiversaFileLoader

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        Dim oEdiversaFile As New DTOEdiversaFile(oGuid)
        If Load(oEdiversaFile) Then
            retval = oEdiversaFile
        End If
        Return retval
    End Function

    Shared Function FromResultGuid(oResultGuid As Guid) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        Dim SQL As String = "SELECT Guid, Text, Result, ResultGuid FROM Edi WHERE ResultGuid='" & oResultGuid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oGuid As Guid = oDrd("Guid")
            retval = New DTOEdiversaFile(oGuid)
            With retval
                .Stream = oDrd("Text")
                .Result = oDrd("Result")
                If Not IsDBNull(oDrd("ResultGuid")) Then
                    .ResultBaseGuid = New DTOBaseGuid(oDrd("ResultGuid"))
                End If
                .IsLoaded = True
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromFileName(sFilenameFullPath As String) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        Dim sFilename As String = System.IO.Path.GetFileName(sFilenameFullPath)
        Dim SQL As String = "SELECT Guid FROM Edi WHERE Filename = '" & sFilename & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOEdiversaFile(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromNumComanda(oEmp As DTOEmp, year As Integer, NumComanda As String) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Guid ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("WHERE CAST(Text AS VARCHAR(50)) LIKE '%ORD|" & NumComanda & "|%' ")
        sb.AppendLine("AND YEAR(Fch)=" & year & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOEdiversaFile(oDrd("Guid"))
        End If
        oDrd.Close()
        If retval IsNot Nothing Then
            EdiversaFileLoader.Load(retval)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaFile As DTOEdiversaFile) As Boolean
        If Not oEdiversaFile.IsLoaded And Not oEdiversaFile.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Edi.* ")
            sb.AppendLine(", Sender.Guid as SenderGuid, Sender.RaoSocial as SenderNom ")
            sb.AppendLine(", Receiver.Guid as ReceiverGuid, Receiver.RaoSocial as ReceiverNom ")
            sb.AppendLine("FROM Edi ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Sender ON EDI.Sender = Sender.GLN ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Receiver ON EDI.Receiver = Receiver.GLN ")
            sb.AppendLine("WHERE Edi.Guid='" & oEdiversaFile.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oEdiversaFile
                    .FileName = SQLHelper.GetStringFromDataReader(oDrd("Filename"))
                    .Tag = SQLHelper.GetStringFromDataReader(oDrd("Tag"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                    .Amount = SQLHelper.GetAmtFromDataReader2(oDrd)
                    .Docnum = SQLHelper.GetStringFromDataReader(oDrd("Docnum"))
                    If Not IsDBNull(oDrd("Sender")) Then
                        .Sender = New DTOEdiversaContact()
                        .Sender.Ean = SQLHelper.GetEANFromDataReader(oDrd("Sender"))
                        .Sender.Nom = SQLHelper.GetStringFromDataReader(oDrd("SenderNom"))
                        If Not IsDBNull(oDrd("SenderGuid")) Then
                            .Sender.Contact = New DTOContact(oDrd("SenderGuid"))
                            .Sender.Contact.GLN = .Sender.Ean
                            .Sender.Contact.Nom = .Sender.Nom
                        End If
                    End If
                    If Not IsDBNull(oDrd("Receiver")) Then
                        .Receiver = New DTOEdiversaContact()
                        .Receiver.Ean = SQLHelper.GetEANFromDataReader(oDrd("Receiver"))
                        .Receiver.Nom = SQLHelper.GetStringFromDataReader(oDrd("ReceiverNom"))
                        If Not IsDBNull(oDrd("ReceiverGuid")) Then
                            .Receiver.Contact = New DTOContact(oDrd("ReceiverGuid"))
                            .Receiver.Contact.GLN = .Receiver.Ean
                            .Receiver.Contact.Nom = .Receiver.Nom
                        End If
                    End If
                    .IOCod = SQLHelper.GetIntegerFromDataReader(oDrd("IOCod"))
                    .Result = oDrd("Result")
                    .ResultBaseGuid = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultGuid"))
                    .Stream = oDrd("Text")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEdiversaFile.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaFile, oTrans)
            UpdateExceptions(oEdiversaFile, oTrans)
            If oEdiversaFile.ResultBaseGuid IsNot Nothing Then
                UpdateSrc(oEdiversaFile, oTrans)
            End If
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



    Shared Function Update(oEdiversaFile As DTOEdiversaFile, ByRef exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaFile, oTrans)
            If oEdiversaFile.ResultBaseGuid IsNot Nothing Then
                UpdateSrc(oEdiversaFile, oTrans)
            End If
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oEdiversaFile, ex.Message))
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub UpdateSrc(oEdiversaFile As DTOEdiversaFile, ByRef oTrans As SqlTransaction)
        If oEdiversaFile.IsNew Then
            Select Case oEdiversaFile.Tag
                Case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
                    Dim SQL = String.Format("UPDATE Pdc SET Pdc.Src = {0} WHERE Pdc.Guid = '{1}'", CInt(DTOPurchaseOrder.Sources.edi), oEdiversaFile.ResultBaseGuid.Guid.ToString())
                    SQLHelper.ExecuteNonQuery(SQL, oTrans)
            End Select
        End If
    End Sub

    Shared Sub Update(oEdiversaFile As DTOEdiversaFile, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("WHERE Guid='" & oEdiversaFile.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaFile.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaFile
            oRow("Filename") = .FileName
            oRow("Fch") = .Fch
            oRow("Tag") = .Tag
            If .Sender IsNot Nothing Then
                If .Sender.Ean IsNot Nothing Then
                    oRow("Sender") = .Sender.Ean.Value
                End If
            End If
            If .Receiver IsNot Nothing Then
                If .Receiver.Ean IsNot Nothing Then
                    oRow("Receiver") = .Receiver.Ean.Value
                End If
            End If
            SQLHelper.SetNullableAmt(.Amount, oRow)
            oRow("Docnum") = SQLHelper.NullableString(.Docnum)
            oRow("Text") = .Stream
            oRow("ResultGuid") = SQLHelper.NullableBaseGuid(.ResultBaseGuid)
            oRow("Result") = .Result
            oRow("IOCod") = .IOCod
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateExceptions(oEdiversaFile As DTOEdiversaFile, ByRef oTrans As SqlTransaction)
        DeleteExceptions(oEdiversaFile, oTrans)

        Dim SQL As String = "SELECT * FROM EdiversaExceptions WHERE Parent='" & oEdiversaFile.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each ex As DTOEdiversaException In oEdiversaFile.Exceptions
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaFile.Guid

            With ex
                oRow("Cod") = .Cod
                oRow("TagGuid") = SQLHelper.NullableBaseGuid(.Tag)
                oRow("TagCod") = .TagCod
                oRow("Msg") = SQLHelper.NullableString(.Msg)
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub SetResult(oEdiversaFile As DTOEdiversaFile, oResult As DTOEdiversaFile.Results, oResultBaseGuid As DTOBaseGuid, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Edi ")
        sb.AppendLine("SET Result=" & CInt(oResult) & " ")
        If oResultBaseGuid Is Nothing Then
            sb.AppendLine(", ResultGuid=NULL ")
        Else
            sb.AppendLine(", ResultGuid='" & oResultBaseGuid.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Guid='" & oEdiversaFile.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(Sql, oTrans)
    End Sub


    Shared Function SetResult(oEdiversaFile As DTOEdiversaFile, oResult As DTOEdiversaFile.Results, oBaseGuidResult As DTOBaseGuid, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            SetResult(oEdiversaFile, oResult, oBaseGuidResult, oTrans)
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


    Shared Function QueueInvoice(exs As List(Of Exception), oLog As DTOInvoicePrintLog) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oLog.EdiversaFile, oTrans)
            InvoiceLoader.LogPrint(oLog, oTrans)
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



    Shared Sub DeleteExceptions(oEdiversaFile As DTOEdiversaFile, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE EdiversaExceptions ")
        sb.AppendLine("WHERE EdiversaExceptions.Parent ='" & oEdiversaFile.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class
Public Class EdiversaFilesLoader

    Shared Function All(Optional sTag As String = "", Optional OnlyOpenFiles As Boolean = False, Optional iYear As Integer = 0, Optional OnlyMissingFromEdiversaOrders As Boolean = False, Optional IncludeStream As Boolean = True) As List(Of DTOEdiversaFile)
        Dim retval As New List(Of DTOEdiversaFile)

        Dim sConditions As New List(Of String)
        If OnlyOpenFiles Then
            sConditions.Add("Edi.Result=0")
        Else
        End If
        If iYear <> 0 Then
            sConditions.Add("Year(Edi.Fch)=" & iYear.ToString())
        End If
        If sTag > "" Then
            sConditions.Add("Edi.Tag='" & sTag & "'")
        End If
        If OnlyMissingFromEdiversaOrders Then
            sConditions.Add("Edi.Guid NOT IN(SELECT Guid FROM EdiversaOrderHeader)")
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Edi.Guid, Edi.Filename, Edi.Tag, Edi.Fch, Edi.FchCreated, Edi.Eur, Edi.DocNum, Edi.Sender, Edi.Receiver, Edi.IOCod, Edi.Result, Edi.ResultGuid ")
        If IncludeStream Then
            sb.AppendLine(", Edi.Text ")
        End If
        sb.AppendLine(", Sender.Guid as SenderGuid, Sender.RaoSocial as SenderNom ")
        sb.AppendLine(", Receiver.Guid as ReceiverGuid, Receiver.RaoSocial as ReceiverNom ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Sender ON EDI.Sender = Sender.GLN ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Receiver ON EDI.Receiver = Receiver.GLN ")

        If sConditions.Count > 0 Then
            For Each sCondition As String In sConditions
                If sCondition = sConditions(0) Then
                    sb.Append("WHERE ")
                Else
                    sb.Append("AND ")
                End If
                sb.AppendLine(sCondition & " ")
            Next
        End If

        sb.AppendLine("ORDER BY Edi.Fch DESC, Edi.DocNum DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaFile(oDrd("Guid"))
            With item
                .fileName = SQLHelper.GetStringFromDataReader(oDrd("Filename"))
                .tag = SQLHelper.GetStringFromDataReader(oDrd("Tag"))
                .fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .fchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                .amount = SQLHelper.GetAmtFromDataReader2(oDrd)
                .docnum = SQLHelper.GetStringFromDataReader(oDrd("Docnum"))
                If Not IsDBNull(oDrd("Sender")) Then
                    .sender = New DTOEdiversaContact()
                    .sender.Ean = SQLHelper.GetEANFromDataReader(oDrd("Sender"))
                    .sender.Nom = SQLHelper.GetStringFromDataReader(oDrd("SenderNom"))
                    If Not IsDBNull(oDrd("SenderGuid")) Then
                        .sender.Contact = New DTOContact(oDrd("SenderGuid"))
                        .sender.Contact.GLN = .sender.Ean
                        .sender.Contact.nom = .sender.Nom
                    End If
                End If
                If Not IsDBNull(oDrd("Receiver")) Then
                    .receiver = New DTOEdiversaContact()
                    .receiver.Ean = SQLHelper.GetEANFromDataReader(oDrd("Receiver"))
                    .receiver.Nom = SQLHelper.GetStringFromDataReader(oDrd("ReceiverNom"))
                    If Not IsDBNull(oDrd("ReceiverGuid")) Then
                        .receiver.Contact = New DTOContact(oDrd("ReceiverGuid"))
                        .receiver.Contact.GLN = .receiver.Ean
                        .receiver.Contact.nom = .receiver.Nom
                    End If
                End If
                .IOCod = SQLHelper.GetIntegerFromDataReader(oDrd("IOCod"))
                .result = oDrd("Result")
                .resultBaseGuid = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultGuid"))
                'If .Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString Then Stop
                If IncludeStream Then
                    .stream = oDrd("Text")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Tags(oEmp As DTOEmp, year As Integer, IOCod As DTOEdiversaFile.IOcods) As List(Of String)
        Dim retval As New List(Of String)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Edi.Tag ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("WHERE YEAR(Edi.FchCreated) = " & year & " ")
        sb.AppendLine("AND Edi.IOCod = " & IOCod & " ")
        sb.AppendLine("GROUP BY Edi.IOCod, Edi.Tag ")
        sb.AppendLine("ORDER BY Edi.IOCod, Edi.Tag ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Tag"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer, iocod As DTOEdiversaFile.IOcods, tag As String) As List(Of DTOEdiversaFile)
        Dim retval As New List(Of DTOEdiversaFile)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Edi.Guid, Edi.Filename, Edi.Tag, Edi.Fch, Edi.FchCreated ")
        sb.AppendLine(", Edi.Sender, Sender.Guid AS SenderGuid, Sender.RaoSocial AS SenderNom ")
        sb.AppendLine(", Edi.Receiver, Receiver.Guid AS ReceiverGuid, Receiver.RaoSocial AS ReceiverNom ")
        sb.AppendLine(", Edi.Result, Edi.ResultGuid, Edi.Eur, Edi.Val, Edi.Cur, Edi.Docnum  ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("LEFT OUTER JOIN CliGral Sender ON Edi.Sender = Sender.Gln ")
        sb.AppendLine("LEFT OUTER JOIN CliGral Receiver ON Edi.Receiver = Receiver.Gln ")
        sb.AppendLine("WHERE YEAR(Edi.FchCreated)=" & year & " AND Edi.Tag='" & tag & "' AND Edi.IOCod = " & iocod & " ")
        sb.AppendLine("ORDER BY Edi.FchCreated DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaFile(oDrd("Guid"))
            With item
                .fileName = SQLHelper.GetStringFromDataReader(oDrd("Filename"))
                .tag = tag
                .fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .fchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                .amount = SQLHelper.GetAmtFromDataReader2(oDrd)
                .docnum = SQLHelper.GetStringFromDataReader(oDrd("Docnum"))
                If Not IsDBNull(oDrd("Sender")) Then
                    .sender = New DTOEdiversaContact()
                    .sender.Ean = SQLHelper.GetEANFromDataReader(oDrd("Sender"))
                    .sender.Nom = SQLHelper.GetStringFromDataReader(oDrd("SenderNom"))
                    If Not IsDBNull(oDrd("SenderGuid")) Then
                        .sender.Contact = New DTOContact(oDrd("SenderGuid"))
                        .sender.Contact.GLN = .sender.Ean
                        .sender.Contact.nom = .sender.Nom
                    End If
                End If
                If Not IsDBNull(oDrd("Receiver")) Then
                    .receiver = New DTOEdiversaContact()
                    .receiver.Ean = SQLHelper.GetEANFromDataReader(oDrd("Receiver"))
                    .receiver.Nom = SQLHelper.GetStringFromDataReader(oDrd("ReceiverNom"))
                    If Not IsDBNull(oDrd("ReceiverGuid")) Then
                        .receiver.Contact = New DTOContact(oDrd("ReceiverGuid"))
                        .receiver.Contact.GLN = .receiver.Ean
                        .receiver.Contact.nom = .receiver.Nom
                    End If
                End If
                .IOCod = iocod
                .result = oDrd("Result")
                .resultBaseGuid = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultGuid"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oEdiversaFiles As List(Of DTOEdiversaFile)) As DTOTaskResult
        Dim retval As New DTOTaskResult

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each item In oEdiversaFiles
                EdiversaFileLoader.Update(item, oTrans)
            Next
            oTrans.Commit()
            retval.Succeed(oEdiversaFiles.Count & " missatges Edi registrats satisfactoriament")
        Catch ex As Exception
            oTrans.Rollback()
            retval.Fail("Error al registrar " & oEdiversaFiles.Count & " missatges Edi")
            retval.AddException(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Function PendingToWriteToOutbox() As List(Of DTOEdiversaFile)
        Dim retval As New List(Of DTOEdiversaFile)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Edi.Guid, Edi.Tag, Edi.Text,  Edi.ResultGuid ")
        sb.AppendLine(", Edi.Sender, Sender.Guid as SenderGuid, Sender.RaoSocial as SenderNom ")
        sb.AppendLine(", Edi.Fch, Edi.Filename, Edi.IOCod ")
        sb.AppendLine(", Edi.Receiver, Receiver.Guid as ReceiverGuid, Receiver.RaoSocial as ReceiverNom ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Sender ON EDI.Sender = Sender.GLN ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Receiver ON EDI.Receiver = Receiver.GLN ")
        sb.AppendLine("WHERE Edi.IOCOD=" & CInt(DTOEdiversaFile.IOcods.Outbox) & " ")
        sb.AppendLine("AND Edi.Result=" & CInt(DTOEdiversaFile.Results.Pending) & " ")
        sb.AppendLine("ORDER BY Edi.Tag, Edi.Docnum, Edi.FchCreated")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oFile As New DTOEdiversaFile(oDrd("Guid"))
            With oFile
                .Tag = oDrd("Tag")
                .FileName = SQLHelper.GetStringFromDataReader(oDrd("Filename"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                If Not IsDBNull(oDrd("Sender")) Then
                    .Sender = New DTOEdiversaContact()
                    .Sender.Ean = DTOEan.Factory(oDrd("Sender"))
                    .Sender.Nom = SQLHelper.GetStringFromDataReader(oDrd("SenderNom"))
                    .Sender.Contact = New DTOContact(oDrd("SenderGuid"))
                    .Sender.Contact.GLN = .Sender.Ean
                    .Sender.Contact.Nom = .Sender.Nom
                End If
                If Not IsDBNull(oDrd("Receiver")) Then
                    .Receiver = New DTOEdiversaContact()
                    .Receiver.Ean = DTOEan.Factory(oDrd("Receiver"))
                    .Receiver.Nom = SQLHelper.GetStringFromDataReader(oDrd("ReceiverNom"))
                    If IsDBNull(oDrd("ReceiverGuid")) Then
                    Else
                        .Receiver.Contact = New DTOContact(oDrd("ReceiverGuid"))
                        .Receiver.Contact.GLN = .Receiver.Ean
                        .Receiver.Contact.Nom = .Receiver.Nom
                    End If
                End If
                .Stream = oDrd("Text")
                .IOCod = SQLHelper.GetIntegerFromDataReader(oDrd("IOCod"))
                .Result = DTOEdiversaFile.Results.Pending
                .ResultBaseGuid = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultGuid"))
                .IsLoaded = True
            End With
            retval.Add(oFile)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Descarta(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Descarta(oEdiversaFiles, oTrans)
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

    Shared Function Delete(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaFiles, oTrans)
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


    Shared Sub Descarta(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Edi ")
        sb.AppendLine("SET Result = " & DTOEdiversaFile.Results.Deleted & " ")
        sb.AppendLine("WHERE (")
        For Each item In oEdiversaFiles
            If item.UnEquals(oEdiversaFiles.First) Then sb.Append("OR ")
            sb.AppendLine("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE Edi ")
        sb.AppendLine("WHERE (")
        For Each item In oEdiversaFiles
            If item.UnEquals(oEdiversaFiles.First) Then sb.Append("OR ")
            sb.AppendLine("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function PendingOrders() As List(Of DTOEdiversaFile)
        'per debug
        Dim retval As New List(Of DTOEdiversaFile)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT  * FROM Edi ")
        sb.AppendLine("LEFT OUTER JOIN EdiversaOrderHeader ON Edi.Guid = EdiversaOrderHeader.Guid ")
        sb.AppendLine("WHERE Edi.Tag='ORDERS_D_96A_UN_EAN008' AND Edi.Result=0 AND Edi.Resultguid IS NULL ")
        sb.AppendLine("ORDER BY FchCreated DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEdiversaFile(oDrd("Guid"))
            With item
                .FileName = oDrd("Filename")
                .Tag = oDrd("Tag")
                .Fch = oDrd("Fch")
                .IOCod = oDrd("IOCod")
                .FchCreated = oDrd("FchCreated")
                .Stream = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                .Docnum = SQLHelper.GetStringFromDataReader(oDrd("DocNum"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
