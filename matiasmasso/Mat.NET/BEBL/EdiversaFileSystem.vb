Imports System.IO

Public Class EdiversaFileSystem
    Public Const ProcessedOrdersFolder As String = "comandes processades"
    Public Const ProcessedAlbsFolder As String = "albarans processats"
    Public Const DeletedItemsFolder As String = "eliminats"

    Public Enum FolderPaths
        root
        inbox
        outbox
        InboxDuplicateds
        InboxProcessed
    End Enum

    Shared Function ReadPending(oEmp As DTOEmp, ByRef oEdiversaFiles As List(Of DTOEdiversaFile), exs As List(Of DTOEdiversaException)) As Boolean
        Dim sInboxFolder As String = FolderPath(oEmp, FolderPaths.inbox)
        Dim sTargetFolder As String = ""
        Try
            Dim oInboxFolder As New DirectoryInfo(sInboxFolder)
            If oInboxFolder.Exists Then
                Dim sFullPath As String = ""

                Dim oFiles As FileInfo() = oInboxFolder.GetFiles()
                If oFiles.Count > 0 Then
                    For Each oFile As FileInfo In oFiles
                        Try
                            sFullPath = oFile.FullName
                            Dim oEdiversaFile = FromFile(oEmp, sFullPath, exs)
                            oEdiversaFile.FchCreated = oFile.CreationTime
                            If oEdiversaFile.Sender IsNot Nothing Then
                                If oEdiversaFile.Sender.Ean IsNot Nothing Then
                                    oEdiversaFile.Sender.Nom = DTOEdiversaFile.ReadInterlocutor(oEdiversaFile.Sender.Ean).ToString
                                End If
                            End If
                            oEdiversaFiles.Add(oEdiversaFile)

                        Catch ex As Exception
                            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, "error al processar missatge EDI " & sFullPath))
                        End Try
                    Next
                End If

            Else
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, sInboxFolder, "no s'ha trobat la carpeta " & sInboxFolder & " al servidor"))
            End If

        Catch ex0 As System.Security.SecurityException
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, sInboxFolder, "sense permis per llegir la carpeta '" & sInboxFolder & "'"))
        Catch ex1 As PathTooLongException
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, sInboxFolder, "PathTooLongException '" & sInboxFolder & "'"))
        Catch ex As Exception
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, sInboxFolder, "error al llegir la carpeta '" & sInboxFolder & "'" & ex.StackTrace))
        End Try

        Return (exs.Count = 0)
    End Function

    Shared Function FromFile(oEmp As DTOEmp, sFilenameFullPath As String, exs As List(Of DTOEdiversaException)) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = EdiversaFileLoader.FromFileName(sFilenameFullPath)
        Dim sFilename As String = System.IO.Path.GetFileName(sFilenameFullPath)
        If retval Is Nothing Then
            retval = New DTOEdiversaFile
            With retval
                .FileName = sFilename
                '.src=Srcs.eDiversaRawFile
                .Stream = FileSystemHelper.GetStringContentFromFile(sFilenameFullPath)
            End With
            Restore(oEmp, retval, exs)
        Else
            retval.FileName = sFilename
        End If
        Return retval
    End Function



    Shared Function Restore(ByRef oEmp As DTOEmp, oFile As DTOEdiversaFile, exs As List(Of DTOEdiversaException)) As Boolean
        oFile.IsLoaded = False
        BEBL.EdiversaFile.Load(oFile)
        oFile.LoadSegments()
        Dim oInterlocutors As List(Of DTOContact) = EdiversaInterlocutorsLoader.Contacts
        With oFile
            .Sender = DTOEdiversaFile.GetSenderFromSegments(oFile, oInterlocutors)
            .Receiver = DTOEdiversaFile.GetReceiverFromSegments(oFile, oInterlocutors)
            .Fch = oFile.GetFch(exs)
            .Docnum = DTOEdiversaFile.ReadDocNum(oFile)
            .Amount = oFile.GetAmount(exs)
            .IOCod = oFile.GetIOCod(oEmp.Org, exs)
        End With
        Return True
    End Function

    Shared Function RootFolderPath(oEmp As DTOEmp) As String
        Dim exs As New List(Of Exception)
        Dim oDefaultValue = BEBL.Default.Find(DTODefault.Codis.eDiversaPath, oEmp)
        Dim retval As String = oDefaultValue.Value
        Return retval
    End Function

    Shared Function FolderPath(oEmp As DTOEmp, oFolder As FolderPaths) As String
        Dim retval As String = ""
        Select Case oFolder
            Case FolderPaths.root
                retval = RootFolderPath(oEmp)
            Case FolderPaths.inbox
                retval = FolderPath(oEmp, FolderPaths.root) & "rec\planos\"
            Case FolderPaths.outbox
                retval = FolderPath(oEmp, FolderPaths.root) & "send\planos\"
            Case FolderPaths.InboxDuplicateds
                retval = FolderPath(oEmp, FolderPaths.inbox) & "duplicados\"
            Case FolderPaths.InboxProcessed
                retval = FolderPath(oEmp, FolderPaths.inbox) & "procesados\"
        End Select
        Return retval
    End Function

    Shared Function MoveFileToDeletedItemsFolder(ByVal sFullFileName As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim sOldDir As String = System.IO.Path.GetDirectoryName(sFullFileName)
        Dim sOldFileName As String = System.IO.Path.GetFileName(sFullFileName)
        Dim sNewFileName As String = sOldDir & "\" & DeletedItemsFolder & "\" & sOldFileName
        Try
            My.Computer.FileSystem.MoveFile(sFullFileName, sNewFileName)
            retval = True
        Catch ex As Exception
            exs.Add(New Exception("ERROR AL MOURE EDIVERSA A LA PAPELERA" & vbCrLf & sOldDir & vbCrLf & sOldFileName & vbCrLf & sNewFileName))
            exs.Add(ex)
        End Try
        Return retval
    End Function
End Class
