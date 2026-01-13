Imports System.IO
Public Class IOHelper

    Shared Function CheckOrCreateFolder(sFolderPath As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If System.IO.Directory.Exists(sFolderPath) Then
            retval = True
        Else
            Try
                System.IO.Directory.CreateDirectory(sFolderPath)
                retval = True
            Catch ex As Exception
                exs.Add(New Exception("No s'ha pogut crear la carpera '" & sFolderPath & "'"))
                exs.Add(ex)
            End Try
        End If
        Return retval
    End Function



    Shared Function SaveStream(ByVal oBuffer As Byte(), exs As List(Of Exception), ByRef sFilename As String) As Boolean
        Dim retval As Boolean

        If sFilename = "" Then
            sFilename = GetPdfTmpFileName()
        ElseIf sFilename.Contains("\") Then
            'If IO.Directory.Exists(sFullPath) Then
            If IO.Directory.Exists(sFilename.Substring(0, sFilename.LastIndexOf("\"))) Then
            Else
                sFilename = sFilename.Substring(sFilename.LastIndexOf("\") + 1)
                sFilename = GetPdfTmpFileName(sFilename)
            End If
        Else
            sFilename = GetPdfTmpFileName(sFilename)
        End If

        If Not IsFileLocked(sFilename, IO.FileMode.Create, IO.FileAccess.Write, exs) Then
            Dim oFileStream As New IO.FileStream(sFilename, IO.FileMode.Create, System.IO.FileAccess.Write)
            Dim oBinaryWriter As New IO.BinaryWriter(oFileStream)
            oBinaryWriter.Write(oBuffer)
            oBinaryWriter.Close()
            oFileStream.Close()
            retval = True
        End If
        Return retval
    End Function



    Shared Function GetPdfTmpFileName(Optional ByVal sFileName As String = "", Optional exs As List(Of Exception) = Nothing) As String
        'Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tmp\pdf\"
        Dim sTempPath As String = FileSystemHelper.TmpFolder
        Dim sPdfPath As String = sTempPath & "\pdf\"
        Dim oTmpFolder As New System.IO.DirectoryInfo(sPdfPath)

        If oTmpFolder.Exists Then
            Try
                CleanFolder(oTmpFolder)
            Catch ex As System.IO.IOException
                Dim oProcess() As System.Diagnostics.Process = Process.GetProcessesByName("Acrobat")
                oProcess(0).Kill()
                oProcess(0).WaitForExit()
            End Try
        Else
            oTmpFolder.Create()
        End If

        If sFileName > "" Then
            Select Case TextHelper.VbRight(sFileName, 4).ToUpper
                Case ".PDF", ".JPG", ".JPEG", ".GIF", "XLSX"
                Case Else
                    sFileName += ".pdf"
            End Select
        Else
            sFileName = System.Guid.NewGuid.ToString & ".pdf"
        End If

        Dim sFullPath As String = oTmpFolder.FullName & sFileName
        If IO.File.Exists(sFullPath) Then
            Try
                IO.File.Delete(sFullPath)
            Catch ex As Exception
                If exs IsNot Nothing Then
                    exs.Add(ex)
                End If
            End Try
        End If

        Return sFullPath
    End Function

    Shared Function GetTmpFileName(sMime As String, Optional sFilename As String = "") As String
        'Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tmp\pdf\"
        Dim sFolderPath As String = String.Format("{0}\{1}\", FileSystemHelper.TmpFolder, sMime)
        Dim oTmpFolder As New System.IO.DirectoryInfo(sFolderPath)

        If oTmpFolder.Exists Then
            Try
                CleanFolder(oTmpFolder)
            Catch ex As System.IO.IOException
                If sMime = "Pdf" Then
                    Dim oProcess() As System.Diagnostics.Process = Process.GetProcessesByName("Acrobat")
                    oProcess(0).Kill()
                    oProcess(0).WaitForExit()
                End If
            End Try
        Else
            oTmpFolder.Create()
        End If

        If sFilename = "" Then sFilename = Guid.NewGuid.ToString
        If sFilename.EndsWith(sMime) Then
        Else
            sFilename = sFilename & "." & sMime
        End If

        Dim retval As String = String.Format("{0}{1}", sFolderPath, sFilename)
        If IO.File.Exists(retval) Then
            IO.File.Delete(retval)
        End If

        Return retval
    End Function
    Shared Function CleanOrCreateFolder(ByVal sPath As String) As IO.DirectoryInfo
        Dim oFolder As New IO.DirectoryInfo(sPath)
        If oFolder.Exists Then
            CleanFolder(oFolder)
        Else
            oFolder.Create()
        End If
        Return oFolder
    End Function

    Shared Sub CleanFolder(ByVal oFolder As IO.DirectoryInfo)
        Dim oFile As System.IO.FileInfo
        For Each oFile In oFolder.GetFiles()
            If DateTime.Now.Subtract(oFile.CreationTime).TotalMinutes > 15 Then
                Try

                    oFile.Delete()
                Catch ex As Exception

                End Try
            End If
        Next
    End Sub

    Shared Function WriteToTempFile(ByVal Data As String) As String
        ' Writes text to a temporary file and returns path
        Dim strFilename As String = System.IO.Path.GetTempFileName()
        Dim objFS As New System.IO.FileStream(strFilename,
    System.IO.FileMode.Append,
    System.IO.FileAccess.Write)
        ' Opens stream and begins writing
        Dim Writer As New System.IO.StreamWriter(objFS)
        Writer.BaseStream.Seek(0, System.IO.SeekOrigin.End)
        Writer.WriteLine(Data)
        Writer.Flush()
        ' Closes and returns temp path
        Writer.Close()
        Return strFilename
    End Function

    Shared Function IsFileLocked(sFilename As String, oFileMode As FileMode, oFileAccess As System.IO.FileAccess, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        Dim fs As FileStream = Nothing
        Try
            fs = New FileStream(sFilename, oFileMode, oFileAccess)
            retval = False

        Catch ex1 As ArgumentNullException
            'Path Is null.
            exs.Add(New Exception("adreça del fitxer buida"))

        Catch ex3 As PathTooLongException
            'The specified path, File name, Or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, And File names must be less than 260 characters.
            exs.Add(New Exception("el nom del fitxer es massa llarg"))
        Catch ex4 As DirectoryNotFoundException
            'The specified path Is invalid, (for example, it Is on an unmapped drive).
            exs.Add(New Exception("la ubicació del fitxer es incorrecta"))
        Catch ex6 As UnauthorizedAccessException
            'Path specified a file that Is read-only. or This operation Is Not supported on the current platform. or Path specified a directory.
            '-Or-
            'The caller does Not have the required permission.
            '-Or-
            'mode Is Create And the specified file Is a hidden file.
            exs.Add(New Exception("acces no autoritzat al fitxer"))
        Catch ex7 As ArgumentOutOfRangeException
            'mode specified an invalid value.
            exs.Add(New Exception("modalitat d'acces no valida"))
        Catch ex8 As FileNotFoundException
            'The File specified in path was Not found.
            exs.Add(New Exception("no s'ha trobat cap fitxer amb el nom '" & sFilename & "'"))
        Catch ex5 As IOException
            'An I / O error occurred while opening the file.
            exs.Add(New Exception(String.Format("error al intentar obrir el fitxer {0}." & vbCrLf & "verificar que no estigui obert en un altre pantalla.", sFilename)))
        Catch ex9 As NotSupportedException
            'Path Is in an invalid format.
            exs.Add(New Exception("format de ubicacio incorrecte"))
        Catch ex2 As ArgumentException
            'Path Is a zero-length String, contains only white space, Or contains one Or more invalid characters as defined by InvalidPathChars.
            exs.Add(New Exception("caracters no valids a la ubicació del fitxer"))
        Finally
            If fs IsNot Nothing Then
                fs.Close()
            End If
        End Try
        Return retval
    End Function
End Class
