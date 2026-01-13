Imports System.IO

Public Class FileSystemHelper

    Shared Function DefaultEncoding() As System.Text.Encoding
        Dim sEncodingCode As String = "iso-8859-1"
        Dim retVal As System.Text.Encoding = System.Text.Encoding.GetEncoding(sEncodingCode)
        Return retVal
    End Function

    Shared Function GetStreamFromString(ByVal sSourceString As String) As Byte()
        'Dim myEncoder As New System.Text.ASCIIEncoding
        'Dim myEncoder As New System.Text.UTF8Encoding
        Dim myEncoder As System.Text.Encoding = DefaultEncoding()

        Dim bytes As Byte() = myEncoder.GetBytes(sSourceString)
        Return bytes
    End Function

    Shared Function GetFilenameFromFullPath(sFullPath As String, Optional IncludeExtension As Boolean = True) As String
        Dim sPathLess As String = sFullPath
        Dim iPos As Integer = sPathLess.LastIndexOf("/")
        If iPos >= 0 Then sPathLess = sPathLess.Substring(iPos + 1)
        Dim retval As String
        If IncludeExtension Then
            retval = sPathLess
        Else
            iPos = sPathLess.LastIndexOf(".")
            If iPos < 0 Then
                retval = sPathLess
            Else
                retval = sPathLess.Substring(0, iPos)
            End If
        End If
        Return retval
    End Function

    Shared Function GetFilenameFromPath(src As String) As String
        Dim retval As String = ""
        If src.Contains("\") Then
            Dim iPos As Integer = src.LastIndexOf("\")
            retval = src.Substring(iPos + 1)
        Else
            retval = src
        End If
        Return retval
    End Function



    Shared Function getTextLinesArrayFromFilename(sFilename As String) As ArrayList
        Dim retval As New ArrayList
        Dim line As String
        Dim readFile As System.IO.TextReader = New System.IO.StreamReader(sFilename)
        While True
            line = readFile.ReadLine()
            If line Is Nothing Then
                Exit While
            Else
                retval.Add(line)
            End If
        End While
        readFile.Close()
        readFile = Nothing
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

        If Not IsFileLocked(sFilename, IO.FileMode.Create, FileAccess.Write, exs) Then
            Dim oFileStream As New IO.FileStream(sFilename, IO.FileMode.Create, System.IO.FileAccess.Write)
            Dim oBinaryWriter As New IO.BinaryWriter(oFileStream)
            oBinaryWriter.Write(oBuffer)
            oBinaryWriter.Close()
            oFileStream.Close()
            retval = True
        End If
        Return retval
    End Function

    Shared Function TmpFolder() As String
        Dim retval As String = My.Computer.FileSystem.SpecialDirectories.Temp
        Return retval
    End Function

    Shared Function GetPdfTmpFileName(Optional ByVal sFileName As String = "", Optional exs As List(Of Exception) = Nothing) As String
        'Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tmp\pdf\"
        Dim sTempPath As String = My.Computer.FileSystem.SpecialDirectories.Temp
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
            Select Case Right(sFileName, 4).ToUpper
                Case ".PDF", ".JPG", ".JPEG", ".GIF", ".PNG", "XLSX"
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
            If DateDiff(DateInterval.Minute, oFile.CreationTime, Now) > 15 Then
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


    Shared Function ReadHtmlPage(ByVal url As String, exs As List(Of Exception)) As String
        Dim objResponse As System.Net.WebResponse
        Dim objRequest As System.Net.WebRequest
        Dim result As String = ""

        Try
            objRequest = System.Net.HttpWebRequest.Create(url)
            objResponse = objRequest.GetResponse()
            Dim sr As New System.IO.StreamReader(objResponse.GetResponseStream())
            result = sr.ReadToEnd()

            'clean up StreamReader
            sr.Close()
        Catch ex As Exception
            exs.Add(New Exception("No s'ha pogut descarregar el document de internet"))
            exs.Add(ex)
        End Try

        Return result
    End Function


    Shared Function PathToMyDocuments() As String
        Return Environment.GetFolderPath(Environment.SpecialFolder.Personal)
    End Function

    Shared Function PathToTmp() As String
        Dim Path As String = PathToMyDocuments() & "\tmp"
        If Not System.IO.Directory.Exists(Path) Then
            System.IO.Directory.CreateDirectory(Path)
        End If
        Return Path
    End Function

    Shared Function GetStreamFromFile(ByVal sFileName As String, ByRef oByteArray As Byte(), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oFileStream As IO.FileStream
        Dim oBinaryReader As IO.BinaryReader = Nothing
        Try
            oFileStream = New IO.FileStream(sFileName, IO.FileMode.Open, IO.FileAccess.Read)
            oBinaryReader = New IO.BinaryReader(oFileStream)
            oByteArray = oBinaryReader.ReadBytes(oFileStream.Length)
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        Finally
            If oBinaryReader IsNot Nothing Then
                oBinaryReader.Close()
            End If
            oFileStream = Nothing
        End Try
        Return retval
    End Function





    Shared Function GetProgramFilesX86Folder() As String
        Dim sKey As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion"

        Dim sName As String = "ProgramFilesDir (x86)"
        Dim sValue As String = My.Computer.Registry.GetValue(sKey, sName, Nothing)
        If sValue = "" Then
            sName = "ProgramFilesDir"
            sValue = My.Computer.Registry.GetValue(sKey, sName, Nothing)
        End If

        Return sValue
    End Function

    Shared Function GetXMLfromByteStream(ByVal oStream As Byte()) As Xml.XmlDocument
        Dim s As String = ""
        Dim iInitCount As Integer = 0
        If oStream(0) = 239 And oStream(1) = 187 Then iInitCount = 3
        For i As Integer = iInitCount To oStream.Length - 1
            s = s & Chr(oStream(i))
        Next

        Dim oDoc As New Xml.XmlDocument
        oDoc.LoadXml(s)
        Return oDoc
    End Function


    Shared Function GetStringContentFromFile(ByVal sFileName As String) As String
        Dim oFileStream As New IO.FileStream(sFileName, IO.FileMode.Open, IO.FileAccess.Read)
        Dim oStreamReader As New System.IO.StreamReader(oFileStream)
        Dim retVal As String = oStreamReader.ReadToEnd()
        oStreamReader.Close()
        oFileStream = Nothing
        Return retVal
    End Function

    Shared Function SaveTextToFile(sText As String, sFilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim sr As IO.StreamWriter
        Try
            sr = New IO.StreamWriter(sFilename, False, System.Text.Encoding.Default)
            sr.Write(sText)
            sr.Flush()
            sr.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Sub SaveStreamToFile(ByVal oSourceStream As System.IO.MemoryStream, ByVal sFilename As String)
        ' create a write stream
        Dim oFileStream As New FileStream(sFilename, FileMode.Create, FileAccess.Write)
        ' write to the stream
        Dim iLength As Integer = 256
        Dim oBuffer(iLength) As Byte
        oSourceStream.Position = 0
        Dim iBytesRead As Integer = oSourceStream.Read(oBuffer, 0, iLength)

        ' write the required bytes
        Do While (iBytesRead > 0)
            oFileStream.Write(oBuffer, 0, iBytesRead)
            iBytesRead = oSourceStream.Read(oBuffer, 0, iLength)
        Loop

        oSourceStream.Close()
        oFileStream.Close()
    End Sub


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

    Shared Function GetByteArrayFromStream(oStream As System.IO.Stream) As Byte()
        Dim streamLength As Integer = Convert.ToInt32(oStream.Length)

        Dim retval As Byte() = New Byte(streamLength) {}
        oStream.Read(retval, 0, streamLength)
        oStream.Close()

        Return retval
    End Function

    Shared Function GetTmpFileName(oMime As MimeCods, Optional sFilename As String = "") As String
        'Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tmp\pdf\"
        Dim sTempPath As String = My.Computer.FileSystem.SpecialDirectories.Temp
        Dim sFolderPath As String = String.Format("{0}\{1}\", sTempPath, oMime.ToString)
        Dim oTmpFolder As New System.IO.DirectoryInfo(sFolderPath)

        If oTmpFolder.Exists Then
            Try
                CleanFolder(oTmpFolder)
            Catch ex As System.IO.IOException
                If oMime = MimeCods.Pdf Then
                    Dim oProcess() As System.Diagnostics.Process = Process.GetProcessesByName("Acrobat")
                    oProcess(0).Kill()
                    oProcess(0).WaitForExit()
                End If
            End Try
        Else
            oTmpFolder.Create()
        End If

        If sFilename = "" Then sFilename = Guid.NewGuid.ToString
        If sFilename.EndsWith(oMime.ToString) Then
        Else
            sFilename = sFilename & "." & oMime.ToString
        End If

        Dim retval As String = String.Format("{0}{1}", sFolderPath, sFilename)
        If IO.File.Exists(retval) Then
            IO.File.Delete(retval)
        End If

        Return retval
    End Function

    Shared Function DownloadHtml(sUrl As String, ByRef sResult As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oStream As MemoryStream = Nothing
        Dim pExs As New List(Of Exception)
        If DownloadStream(sUrl, oStream, pExs) Then
            oStream.Position = 0
            Try
                Dim sr As IO.StreamReader = New IO.StreamReader(oStream)
                sResult = sr.ReadToEnd
                sr.Close()
                retval = True
            Catch ex As Exception
                exs.Add(ex)
            End Try
        Else

            exs.Add(New Exception("error al descarregar el contingut del missatge"))
            exs.Add(New Exception("verificar que la web estigui en funcionament"))
        End If
        Return retval
    End Function



    Shared Function DownloadStream(ByVal sUrl As String, ByRef oResultStream As MemoryStream, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRequest As Net.HttpWebRequest = Nothing
        Dim oResponse As Net.HttpWebResponse
        Dim oSourceStream As System.IO.Stream

        Try
            oRequest = DirectCast(Net.HttpWebRequest.Create(sUrl), Net.HttpWebRequest)
            oResponse = DirectCast(oRequest.GetResponse, Net.HttpWebResponse)
            oSourceStream = oResponse.GetResponseStream
            ' create a write stream
            oResultStream = New MemoryStream
            ' write to the stream
            Dim iLength As Integer = 256
            Dim oBuffer(iLength) As Byte
            Dim iBytesRead As Integer = oSourceStream.Read(oBuffer, 0, iLength)

            ' write the required bytes
            Do While (iBytesRead > 0)
                oResultStream.Write(oBuffer, 0, iBytesRead)
                iBytesRead = oSourceStream.Read(oBuffer, 0, iLength)
            Loop

            oSourceStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try


        Return retval
    End Function


End Class
