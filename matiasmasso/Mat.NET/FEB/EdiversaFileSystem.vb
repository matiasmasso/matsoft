Imports System.IO

Public Class EdiversaFileSystem 'FEBL, temporal

    Public Const ProcessedOrdersFolder As String = "comandes processades"
    Public Const ProcessedAlbsFolder As String = "albarans processats"
    Public Const DeletedItemsFolder As String = "eliminats"

    Public Enum FolderPaths
        root
        inbox
        outbox
        InboxDuplicateds
        InboxProcessed
        Err
    End Enum



    Shared Async Function ReadPending(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaFile))
        'llegeix els fitxers que s'han quedat al servidor pendents de baixar
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "ediversa/readpending")
    End Function

    Shared Function SaveInboxFileSync(oFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        Return Api.ExecuteSync(Of DTOEdiversaFile)(oFile, exs, "ediversafiles/saveInboxFile")
    End Function

    Shared Async Function SaveInboxFile(oFile As DTOEdiversaFile, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOEdiversaFile)(oFile, exs, "ediversafiles/saveInboxFile")
    End Function

    Shared Async Function SaveInboxFiles(oFiles As List(Of DTOEdiversaFile), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaFile))(oFiles, exs, "ediversafiles/saveInboxFiles")
    End Function

    Shared Function PendingToWriteToOutboxSync(exs As List(Of Exception)) As List(Of DTOEdiversaFile)
        Return Api.FetchSync(Of List(Of DTOEdiversaFile))(exs, "ediversa/PendingToWriteToOutbox")
    End Function

    Shared Async Function PendingToWriteToOutbox(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaFile))
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "ediversa/PendingToWriteToOutbox")
    End Function

    Shared Function UpdateSync(oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        Return Api.UpdateSync(Of DTOEdiversaFile)(oEdiversaFile, exs, "ediversafile")
    End Function

    Shared Async Function Update(oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaFile)(oEdiversaFile, exs, "ediversafile")
    End Function

    Shared Async Function ProcessaInbox(oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "ediversa/procesaInbox", oUser.Guid.ToString())
    End Function

    Shared Function ProcessaInboxSync(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "ediversa/procesaInbox", oUser.Guid.ToString())
    End Function

    Shared Function Execute(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Try
            oTask.LastLog = DTOTaskLog.Factory() 'set task as running
            If Task.LogSync(oTask, exs) Then
                Select Case oTask.Cod
                    Case DTOTask.Cods.EdiReadFromInbox
                        EdiversaFileSystem.ReadInboxSync(exs)
                    Case DTOTask.Cods.EdiWriteToOutbox
                        EdiversaFileSystem.WriteFilesToOutboxSync(exs)
                End Select

                oTask.SetResult(exs)
                TaskLog.UpdateSync(oTask, exs)
            Else
                exs.Add(New Exception("EdiversaFileSystem.ExecuteIfDue: Error al registrar el començament de tasca"))
            End If

        Catch ex As Exception
            exs.Add(New Exception("EdiversaFileSystem.ExecuteIfDue: Error al executar la tasca " & oTask.Cod.ToString()))
            exs.Add(ex)
        End Try

        Return exs.Count = 0
    End Function

    Shared Function WriteFilesToOutboxSync(exs As List(Of Exception)) As Boolean
        Try
            Dim oEdiversaFiles As List(Of DTOEdiversaFile) = EdiversaFileSystem.PendingToWriteToOutboxSync(exs)
            If oEdiversaFiles.Count > 0 Then
                For Each oEdiversaFile As DTOEdiversaFile In oEdiversaFiles
                    oEdiversaFile.LoadSegments()
                    Select Case oEdiversaFile.tag
                        Case "ORDERS_D_96A_UN_EAN008"
                            Dim sb As New System.Text.StringBuilder
                            sb.Append(oEdiversaFile.tag)
                            sb.Append("." & oEdiversaFile.segments.Find(Function(x) x.Fields(0) = "ORD").Fields(1))
                            sb.Append("." & TextHelper.VbFormat(DTO.GlobalVariables.Now(), "yyyy.MM.dd_HH.mm.ss"))
                            sb.Append(".txt")

                            oEdiversaFile.fileName = sb.ToString
                            Dim sFullPath As String = FolderPath(FolderPaths.outbox) & oEdiversaFile.fileName
                            If SaveFile(oEdiversaFile, sFullPath, exs) Then
                                oEdiversaFile.result = DTOEdiversaFile.Results.processed
                                If Not EdiversaFileSystem.UpdateSync(oEdiversaFile, exs) Then
                                    exs.Add(New Exception("fitxer " & oEdiversaFile.fileName & " desat pero base de dades no actualitzada"))
                                End If
                            Else
                                exs.Add(New Exception("No s'ha pogut desar el fitxer " & oEdiversaFile.fileName))
                            End If

                        Case "DESADV_D_96A_UN_EAN005"
                            Dim sb As New System.Text.StringBuilder
                            sb.Append(oEdiversaFile.tag)
                            sb.Append("." & oEdiversaFile.segments.Find(Function(x) x.Fields(0) = "BGM").Fields(1))
                            sb.Append("." & TextHelper.VbFormat(DTO.GlobalVariables.Now(), "yyyy.MM.dd_HH.mm.ss"))
                            sb.Append(".txt")

                            oEdiversaFile.fileName = sb.ToString
                            Dim sFullPath As String = FolderPath(FolderPaths.outbox) & oEdiversaFile.fileName
                            If SaveFile(oEdiversaFile, sFullPath, exs) Then
                                oEdiversaFile.result = DTOEdiversaFile.Results.processed
                                If Not EdiversaFileSystem.UpdateSync(oEdiversaFile, exs) Then
                                    exs.Add(New Exception("fitxer " & oEdiversaFile.fileName & " desat pero base de dades no actualitzada"))
                                End If
                            Else
                                exs.Add(New Exception("No s'ha pogut desar el fitxer " & oEdiversaFile.fileName))
                            End If

                        Case "INVOIC_D_93A_UN_EAN007", "INVOIC_D_01B_UN_EAN010"
                            Dim sb As New System.Text.StringBuilder
                            sb.Append(oEdiversaFile.Tag)
                            sb.Append("." & oEdiversaFile.Segments.Find(Function(x) x.Fields(0) = "INV").Fields(1))
                            sb.Append("." & TextHelper.VbFormat(DTO.GlobalVariables.Now(), "yyyy.MM.dd_HH.mm.ss"))
                            sb.Append(".txt")

                            oEdiversaFile.FileName = sb.ToString
                            Dim sFullPath As String = FolderPath(FolderPaths.outbox) & oEdiversaFile.FileName
                            If EdiversaFileSystem.SaveFile(oEdiversaFile, sFullPath, exs) Then
                                oEdiversaFile.Result = DTOEdiversaFile.Results.Processed
                                If Not EdiversaFileSystem.UpdateSync(oEdiversaFile, exs) Then
                                    exs.Add(New Exception("fitxer " & oEdiversaFile.FileName & " desat pero base de dades no actualitzada"))
                                End If
                            Else
                                exs.Add(New Exception("No s'ha pogut desar el fitxer " & oEdiversaFile.FileName))
                            End If

                        Case "INVRPT_D_96A_UN_EAN008"
                            Dim sb As New System.Text.StringBuilder
                            sb.Append(oEdiversaFile.Tag)
                            sb.Append("." & TextHelper.VbFormat(DTO.GlobalVariables.Now(), "yyyy.MM.dd_HH.mm.ss"))
                            sb.Append(".txt")

                            oEdiversaFile.FileName = sb.ToString
                            Dim sFullPath As String = FolderPath(FolderPaths.outbox) & oEdiversaFile.FileName
                            If EdiversaFileSystem.SaveFile(oEdiversaFile, sFullPath, exs) Then
                                oEdiversaFile.Result = DTOEdiversaFile.Results.Processed
                                If Not EdiversaFileSystem.UpdateSync(oEdiversaFile, exs) Then
                                    exs.Add(New Exception("fitxer " & oEdiversaFile.FileName & " desat pero base de dades no actualitzada"))
                                End If
                            Else
                                exs.Add(New Exception("No s'ha pogut desar el fitxer " & oEdiversaFile.FileName))
                            End If

                    End Select
                Next
            End If
        Catch ex As Exception
            exs.Add(New Exception("EdiversaFileSystem.WriteFilesToOutbox: Error " & ex.Message))
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function

    Shared Function SaveFile(oEdiversaFile As DTOEdiversaFile, sFullpath As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim sr As IO.StreamWriter = Nothing
        Try
            sr = IO.File.CreateText(sFullpath)
            For Each oSegment As DTOEdiversaSegment In oEdiversaFile.Segments
                Dim sLine = String.Join("|", oSegment.Fields)
                sr.WriteLine(sLine)
            Next
            sr.Close()
            retval = True
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al desar el fitxer {0}", sFullpath)))
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function ReadInboxSync(exs As List(Of Exception)) As Boolean ' per MatSched
        Static isReadingInbox As Boolean

        If Not isReadingInbox Then
            isReadingInbox = True

            Dim sInboxFolder As String = FolderPath(FolderPaths.inbox)
            Dim sTargetFolder = FolderPath(FolderPaths.InboxProcessed)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Read EDI Inbox")

            Try
                Dim oInboxFolder As New DirectoryInfo(sInboxFolder)
                If oInboxFolder.Exists Then
                    Dim sFullPath As String = ""
                    Dim successCount As Integer
                    Dim failCount As Integer

                    Dim oFiles As FileInfo() = oInboxFolder.GetFiles
                    If oFiles.Count = 0 Then
                        'retval.Empty("no ha arribat cap missatge EDI")
                    Else
                        'retval.Succeed("entrant {0} missatges a la safata d'entrada", oFiles.Count)
                        Dim oEdiversaFiles As New List(Of DTOEdiversaFile)
                        For Each oFile As FileInfo In oFiles
                            sFullPath = oFile.FullName
                            Try
                                Dim oEdiversaFile = DTOEdiversaFile.Factory(sFullPath)
                                oEdiversaFiles.Add(oEdiversaFile)
                            Catch ex As Exception
                                exs.Add(New Exception("error al llegir missatge EDI a " & oFile.Name))
                                exs.Add(ex)
                                failCount += 1
                            End Try
                        Next

                        For Each oEdiversaFile In oEdiversaFiles
                            If SaveInboxFileSync(oEdiversaFile, exs) Then
                                Dim sTargetFilename As String = sTargetFolder & oEdiversaFile.FileName
                                Try
                                    Dim oFile = oFiles.FirstOrDefault(Function(x) oEdiversaFile.FileName = System.IO.Path.GetFileName(x.FullName))
                                    If oFile Is Nothing Then
                                    Else
                                        oFile.MoveTo(sTargetFilename)
                                        successCount += 1
                                    End If
                                Catch ex As Exception
                                    exs.Add(New Exception("error al moure missatge EDI a " & sTargetFilename))
                                    exs.Add(ex)
                                    failCount += 1
                                End Try
                            Else
                                exs.Add(New Exception("error al desar missatge EDI " & oEdiversaFile.FileName))
                            End If
                        Next

                        If successCount = oFiles.Count Then
                            'retval.Succeed("processats {0} missatges Edi", oFiles.Count)
                        ElseIf successCount = 0 Then
                            'retval.Succeed("Error al moure " & failCount & " de " & oFiles.Count & " missatges EDI")
                            'retval.ResultCod = DTOTask.ResultCods.DoneWithErrors
                            'TODO: tornar a moure enrera els passats a processats
                        Else
                            exs.Add(New Exception("Error al moure " & failCount & " missatges EDI"))
                        End If

                    End If
                Else
                    exs.Add(New Exception(String.Format("no s'ha trobat la carpeta {0}", sInboxFolder)))
                End If

            Catch ex0 As System.Security.SecurityException
                exs.Add(New Exception("Security Exception '" & sInboxFolder & "'"))
                exs.Add(ex0)
            Catch ex1 As PathTooLongException
                exs.Add(New Exception("Path Too Long Exception '" & sInboxFolder & "'"))
                exs.Add(ex1)
            Catch ex2 As UnauthorizedAccessException
                exs.Add(New Exception("Unauthorized Access Exception '" & sInboxFolder & "'"))
                exs.Add(ex2)
            Catch ex3 As DirectoryNotFoundException
                exs.Add(New Exception("Directory Not Found Exception '" & sInboxFolder & "'"))
                exs.Add(ex3)
            Catch ex4 As ArgumentException
                exs.Add(New Exception("Directory path contains invalid characters '" & sInboxFolder & "'"))
                exs.Add(ex4)
            Catch ex As Exception
                exs.Add(New Exception("error al llegir la carpeta '" & sInboxFolder & "': " & ex.Message & " - " & ex.StackTrace))
                exs.Add(ex)
            End Try

            isReadingInbox = False
        End If

        Return exs.Count = 0
    End Function

    Shared Function RepairRoemerErrs(exs As List(Of Exception)) As Boolean
        Dim sInboxFolder As String = FolderPath(FolderPaths.Err)
        Dim oInboxFolder As New DirectoryInfo(sInboxFolder)
        If oInboxFolder.Exists Then
            Dim oFolderFiles As FileInfo() = oInboxFolder.GetFiles()
            Dim errFiles = oFolderFiles.Where(Function(x) x.Name.EndsWith(".err"))
            Dim err417EdiFiles As New List(Of FileInfo)
            For Each errFile In errFiles
                Dim oEdiversaErr = New DTOEdiversaErr(errFile.FullName)
                If oEdiversaErr.HasTextFieldWrongValue Then
                    Dim ediFilename = errFile.Name.Replace(".err", ".EDI")
                    Dim ediFile = oInboxFolder.GetFiles(ediFilename).FirstOrDefault()
                    If ediFile IsNot Nothing Then err417EdiFiles.Add(ediFile)
                End If
            Next

            For Each edifile In err417EdiFiles
                Dim src = System.IO.File.ReadAllText(edifile.FullName)
                Dim header = "UNA:+.?"
                Dim headerReplacement = "@@@"
                Dim tmp = src.Replace(header, headerReplacement)
                tmp.Replace("?", " ")
                tmp.Replace(headerReplacement, header)
                Dim path = "" '======================================= TODO
                File.WriteAllText(path, tmp)
            Next
        End If

    End Function


    Shared Function FolderPath(oFolder As FolderPaths) As String
        Dim retval As String = ""
        Select Case oFolder
            Case FolderPaths.root
                retval = "C:\Ediversa\"
            Case FolderPaths.inbox
                retval = "C:\Ediversa\rec\planos\"
            Case FolderPaths.outbox
                retval = "C:\Ediversa\send\planos\"
            Case FolderPaths.InboxDuplicateds
                retval = "C:\Ediversa\rec\planos\duplicados\"
            Case FolderPaths.InboxProcessed
                retval = "C:\Ediversa\rec\planos\procesados\"
            Case FolderPaths.Err
                retval = "C:\Ediversa\Rec\Error\"
        End Select
        Return retval
    End Function


End Class


