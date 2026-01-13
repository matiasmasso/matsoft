Public Class EdiversaFile

    Shared Function Find(oGuid As Guid) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = EdiversaFileLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromResultGuid(oResultGuid As Guid) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = EdiversaFileLoader.FromResultGuid(oResultGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaFile As DTOEdiversaFile) As Boolean
        Dim retval As Boolean = EdiversaFileLoader.Load(oEdiversaFile)
        oEdiversaFile.LoadSegments()
        Return retval
    End Function

    Shared Function Update(oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        Return EdiversaFileLoader.Update(oEdiversaFile, exs)
    End Function


    Shared Function Restore(ByRef oFile As DTOEdiversaFile, oOrg As DTOContact, exs As List(Of Exception)) As Boolean
        Try
            BEBL.EdiversaFile.Load(oFile)
            Dim oInterlocutors As List(Of DTOContact) = EdiversaInterlocutorsLoader.Contacts
            With oFile
                .Sender = DTOEdiversaFile.GetSenderFromSegments(oFile, oInterlocutors)
                .Receiver = DTOEdiversaFile.GetReceiverFromSegments(oFile, oInterlocutors)
                .Fch = oFile.GetFch(.Exceptions)
                .Docnum = oFile.GetDocNum()
                .Amount = oFile.GetAmount(.Exceptions)
                .IOCod = oFile.GetIOCod(oOrg, .Exceptions)
            End With
            BEBL.EdiversaFile.Update(oFile, exs)
        Catch ex As Exception
            exs.Add(New Exception(String.Format("{0} error en restore: {1}", oFile.FileName, ex.Message)))
        End Try

        Return exs.Count = 0
    End Function

    Shared Function Interlocutor(src As DTOEdiversaFile) As DTOEdiversaFile.Interlocutors
        Dim retval As DTOEdiversaFile.Interlocutors = DTOEdiversaFile.Interlocutors.Unknown
        Dim oNADs As List(Of DTOEdiversaSegment) = src.Segments.Where(Function(x) x.Fields.First.StartsWith("NAD")).ToList
        For Each oNAD As DTOEdiversaSegment In oNADs
            If oNAD.Fields.Count > 0 Then
                Dim sEan As String = oNAD.Fields(1)
                If sEan.Length = 13 Then
                    Dim oCandidate = Interlocutor(DTOEan.Factory(sEan))
                    If oCandidate <> DTOEdiversaFile.Interlocutors.unknown And oCandidate <> DTOEdiversaFile.Interlocutors.matiasmasso Then
                        retval = oCandidate
                        Exit For
                    End If
                End If
            End If
        Next
        Return retval
    End Function

    Shared Function Interlocutor(Gln As DTOEan) As DTOEdiversaFile.Interlocutors
        Dim retval As DTOEdiversaFile.Interlocutors = DTOEdiversaFile.Interlocutors.Unknown
        If Gln IsNot Nothing Then
            Dim sEan As String = Gln.Value
            If sEan.Length = 13 Then
                If sEan.StartsWith(DTOEdiversaFile.PrefixBritax) Then
                    retval = DTOEdiversaFile.Interlocutors.Britax
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixElCorteIngles) Then
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixElCorteInglesPt) Then
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixAmazon) Then
                    retval = DTOEdiversaFile.Interlocutors.Amazon
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixCarrefour) Then
                    retval = DTOEdiversaFile.Interlocutors.Carrefour
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixSonae) Then
                    retval = DTOEdiversaFile.Interlocutors.Sonae
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixAlcampo) Then
                    retval = DTOEdiversaFile.Interlocutors.Alcampo
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixToysRUs) Then
                    retval = DTOEdiversaFile.Interlocutors.toysrus
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixMiFarma) Then
                    retval = DTOEdiversaFile.Interlocutors.mifarma
                End If
            End If
        End If

        Return retval
    End Function

    Shared Async Function PreProcessOrder(exs As List(Of Exception), oFile As DTOEdiversaFile) As Task(Of Boolean)
        'For debug
        Dim sb As New Text.StringBuilder
        Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso)
        BEBL.Contact.Load(oEmp.Org)
        If Not EdiversaFile.Restore(oFile, oEmp.Org, exs) Then
            'Stop
        End If
        Await BEBL.EdiversaFile.Procesa(oEmp, oFile, exs)

        Return exs.Count = 0
    End Function


    Shared Async Function Procesa(oEmp As DTOEmp, oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Select Case oEdiversaFile.Tag
            Case "ORDERS_D_96A_UN_EAN008"
                Dim oEdiversaOrder = DTOEdiversaOrder.Factory(oEdiversaFile)
                BEBL.EdiversaOrder.MergeEdiversaOrder(oEdiversaOrder)

                'Dim oEdiversaOrder = BEBL.EdiversaOrder.FromEdiversaFile(oEdiversaFile)
                retval = BEBL.EdiversaOrder.Update(oEdiversaOrder, exs)

            Case "ORDRSP_D_96A_UN_EAN005"
                'Dim oFile As New EdiFile_ORDRSP_D_96A_UN_EAN005(MyBase.Guid)
                'retval = oFile.Procesa(exs)

            Case "DESADV_D_96A_UN_EAN005", "DESADV_D_96A_UN_EAN008"
                Dim oDesadvFile = BEBL.EdiversaDesadv.Factory(oEmp, oEdiversaFile, exs)
                If exs.Count = 0 Then
                    If BEBL.EdiversaDesadv.Update(oDesadvFile, exs) Then
                        oEdiversaFile.Exceptions.AddRange(oDesadvFile.Exceptions)
                        retval = BEBL.EdiversaFile.Update(oEdiversaFile, exs)
                    End If
                End If

            Case "INVOIC_D_96A_UN_EAN008"
                Dim src = oEdiversaFile.Stream
                Dim oEdiInvoic = EdiHelperStd.Invoic.Factory(exs, src)
                Dim oInvoiceReceived = BEBL.InvoiceReceived.Factory(oEdiInvoic)
                BEBL.InvoiceReceived.Update(exs, oInvoiceReceived)
                If exs.Count = 0 Then
                    oEdiversaFile.Result = DTOEdiversaFile.Results.processed
                    oEdiversaFile.ResultBaseGuid = oInvoiceReceived
                    retval = BEBL.EdiversaFile.Update(oEdiversaFile, exs)
                End If

            Case "INVOIC_D_01B_UN_EAN010"
                'exs.Add(New Exception("processat no implementat per fitxers INVOIC_D_01B_UN_EAN010"))

            Case "INVOIC_D_93A_UN_EAN007"
                'Dim oFile As New EdiFile_INVOIC_D_93A_UN_EAN007(MyBase.Guid)
                'retval = oFile.Procesa(exs)
                'exs.Add(New Exception("processat no implementat per fitxers INVOIC_D_93A_UN_EAN007"))
            Case "REMADV_D_96A_UN_EAN003"
                retval = BEBL.EdiversaRemadv.Procesa(oEdiversaFile, oEmp, exs)

            Case "INVRPT_D_96A_UN_EAN004"
                retval = BEBL.Integracions.Edi.Invrpt.Procesa(exs, oEdiversaFile)

            Case "SLSRPT_D_96A_UN_EAN004"
                retval = BEBL.EdiversaSalesReport.Procesa(oEdiversaFile, exs)

            Case "GENRAL_D_96A_UN_EAN003"
                retval = Await BEBL.EdiversaGenral.Send(exs, oEmp, oEdiversaFile)
            Case "RECADV_D_01B_UN_EAN005"
                Dim oEdiversaRecadv = DTOEdiversaRecadv.Factory(oEdiversaFile)
                retval = BEBL.EdiversaRecadv.Update(oEdiversaRecadv, exs)
        End Select

        Return retval
    End Function

    Shared Async Function SaveInboxFile(oEmp As DTOEmp, oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Try
            Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
            Dim sb As New Text.StringBuilder

            Dim eExs As New List(Of DTOEdiversaException)
            If String.IsNullOrEmpty(oEdiversaFile.Stream) Then EdiversaFileLoader.Load(oEdiversaFile)

            'complementa els camps de ediversaFileHeader:
            If Restore(oEdiversaFile, oEmp.Org, exs) Then
                If EdiversaFileLoader.Update(oEdiversaFile, exs) Then
                    retval = Await BEBL.EdiversaFile.Procesa(oEmp, oEdiversaFile, exs)
                End If
            Else
                exs.Add(New Exception(String.Format("Error al llegir {0}", oEdiversaFile.FileName)))
                exs.AddRange(DTOEdiversaException.ToSystemExceptions(eExs))
            End If

        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al registrar {0}", oEdiversaFile.FileName)))
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function

    Shared Function QueueInvoice(exs As List(Of Exception), oLog As DTOInvoicePrintLog) As Boolean
        oLog.PrintMode = DTOInvoice.PrintModes.Edi
        Return EdiversaFileLoader.QueueInvoice(exs, oLog)
    End Function
End Class

Public Class EdiversaFiles

    Shared Function All(Optional sTag As String = "", Optional OnlyOpenFiles As Boolean = False, Optional iYear As Integer = 0, Optional OnlyMissingFromEdiversaOrders As Boolean = False) As List(Of DTOEdiversaFile)
        Return EdiversaFilesLoader.All(sTag, OnlyOpenFiles, iYear, OnlyMissingFromEdiversaOrders, IncludeStream:=False)
    End Function

    Shared Function Tags(oEmp As DTOEmp, year As Integer, IOCod As DTOEdiversaFile.IOcods) As List(Of String)
        Return EdiversaFilesLoader.Tags(oEmp, year, IOCod)
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer, iocod As DTOEdiversaFile.IOcods, tag As String) As List(Of DTOEdiversaFile)
        Return EdiversaFilesLoader.All(oEmp, year, iocod, tag)
    End Function

    Shared Function Delete(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaFilesLoader.Delete(oEdiversaFiles, exs)
        Return retval
    End Function

    Shared Function PendingToWriteToOutbox() As List(Of DTOEdiversaFile)
        Dim retval As List(Of DTOEdiversaFile) = EdiversaFilesLoader.PendingToWriteToOutbox
        Return retval
    End Function

    Shared Async Function SaveInboxFiles(oEmp As DTOEmp, oEdiversaFiles As List(Of DTOEdiversaFile), exs As List(Of Exception)) As Task(Of Boolean)
        For Each oEdiversaFile In oEdiversaFiles
            Await BEBL.EdiversaFile.SaveInboxFile(oEmp, oEdiversaFile, exs)
        Next
        Return exs.Count = 0
    End Function

    Shared Function DeleteDuplicatedOrders() As DTOTaskResult
        Return EDiversaOrdersLoader.DeleteDuplicates
    End Function

    Shared Async Function PreProcessOrders(exs As List(Of Exception)) As Task(Of Boolean)
        'For debug
        Dim sb As New Text.StringBuilder
        Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso)
        BEBL.Contact.Load(oEmp.Org)
        Dim oEdiversaFiles = EdiversaFilesLoader.PendingOrders

        For Each item In oEdiversaFiles
            If Not EdiversaFile.Restore(item, oEmp.Org, exs) Then
                'Stop
            End If
            Await BEBL.EdiversaFile.Procesa(oEmp, item, exs)

        Next
        Return exs.Count = 0
    End Function


    Shared Function Restore(oEmp As DTOEmp, ByRef oFiles As List(Of DTOEdiversaFile), exs As List(Of Exception)) As Boolean
        For Each oFile As DTOEdiversaFile In oFiles
            BEBL.EdiversaFile.Restore(oFile, oEmp.Org, exs)
        Next
        Return True
    End Function

    Shared Function Descarta(oEdiversaFiles As List(Of DTOEdiversaFile), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaFilesLoader.Descarta(oEdiversaFiles, exs)
        Return retval
    End Function
End Class
