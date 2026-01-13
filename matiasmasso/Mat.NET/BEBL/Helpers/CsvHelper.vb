Public Class CsvHelper


    Shared Function AddRow(oFile As DTOCsv) As DTOCsvRow
        Dim oRow As New DTOCsvRow
        oRow.Cells = New List(Of String)
        oFile.Rows.Add(oRow)
        Return oRow
    End Function

    Shared Sub AddCell(oRow As DTOCsvRow, sField As String)
        oRow.Cells.Add(sField)
    End Sub

    Shared Sub AddCell(oRow As DTOCsvRow, oAmt As DTOAmt)
        If oAmt Is Nothing Then
            oRow.Cells.Add("")
        Else
            oRow.Cells.Add(oAmt.Eur)
        End If
    End Sub

    Shared Shadows Function ToString(oFile As DTOCsv) As String
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DTOCsvRow In oFile.Rows
            Dim FirstField As Boolean = True
            For Each oField As String In oRow.Cells
                If FirstField Then
                    FirstField = False
                Else
                    sb.Append(";")
                End If
                If oField IsNot Nothing Then
                    Dim InsertQuotes As Boolean = oField.Contains(";")
                    If oField.Contains(vbCrLf) Then InsertQuotes = True
                    If oField.Contains(",") Then InsertQuotes = True

                    If InsertQuotes Then sb.Append(Chr(34))
                    sb.Append(oField)
                    If InsertQuotes Then sb.Append(Chr(34))
                End If
            Next
            sb.AppendLine()
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function ToByteArray(oCsv As DTOCsv) As Byte()
        Dim src As String = CsvHelper.ToString(oCsv)
        Dim retval As Byte() = New Byte(src.Length * 2 - 1) {}
        System.Buffer.BlockCopy(src.ToCharArray(), 0, retval, 0, retval.Length)
        Return retval
    End Function

    Shared Function FromFile(ByVal sFileName As String, ByRef oCsvFile As DTOCsv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oFileStream As IO.FileStream = Nothing
        Try
            oFileStream = New IO.FileStream(sFileName, IO.FileMode.Open, IO.FileAccess.Read)
            Dim oStreamReader As New System.IO.StreamReader(oFileStream, System.Text.Encoding.Default)
            Dim src As String = oStreamReader.ReadToEnd()
            oStreamReader.Close()

            Dim vblfs As Integer = src.Count(Function(c As String) c = vbLf)
            Dim vbcrlfs As Integer = src.Count(Function(c As String) c = vbCrLf)
            Dim splitchar As String = IIf(vblfs > vbcrlfs, vbLf, vbCrLf)

            Dim lines() As String = src.Split(splitchar)
            oCsvFile = New DTOCsv
            oCsvFile.Rows = New List(Of DTOCsvRow)
            For Each sLine As String In lines
                Dim oRow As New DTOCsvRow
                oRow.Cells = New List(Of String)
                For Each sCell As String In sLine.Split(";")
                    oRow.Cells.Add(sCell)
                Next
                oCsvFile.Rows.Add(oRow)
            Next
            retval = True

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function Save(ByVal sFileName As String, ByRef oCsv As DTOCsv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            If System.IO.File.Exists(sFileName) Then
                IO.File.Delete(sFileName)
            End If

            Dim oFileStream As New System.IO.FileStream(sFileName, IO.FileMode.CreateNew, IO.FileAccess.ReadWrite)
            Dim oSw As New System.IO.StreamWriter(oFileStream, System.Text.Encoding.Default)
            For Each oRow As DTOCsvRow In oCsv.Rows
                Dim sLine As String = String.Join(";", oRow.Cells.ToArray())
                oSw.WriteLine(sLine)
            Next
            oSw.Flush()
            oSw.Close()
            retval = True

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function


End Class

