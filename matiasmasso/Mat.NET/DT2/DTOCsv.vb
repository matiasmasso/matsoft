Public Class DTOCsv
    Property Rows As List(Of DTOCsvRow)
    Property Title As String
    Property Filename As String

    Public Sub New(Optional sTitle As String = "", Optional sFilename As String = "")
        _Rows = New List(Of DTOCsvRow)
        _Title = sTitle
        If sFilename = "" Then sFilename = sTitle
        If sFilename = "" Then sFilename = "M+O downloaded file.csv"
        If sFilename.EndsWith(".csv") Then
        Else
            sFilename += ".csv"
        End If
        _Filename = sFilename
    End Sub

    Shared Function Factory(src As String) As DTOCsv
        Dim vblfs As Integer = src.Count(Function(c As String) c = vbLf)
        Dim vbcrlfs As Integer = src.Count(Function(c As String) c = vbCrLf)
        Dim splitchar As String = IIf(vblfs > vbcrlfs, vbLf, vbCrLf)

        Dim lines() As String = src.Split(splitchar)
        Dim retval As New DTOCsv
        For Each sLine As String In lines
            Dim oRow As New DTOCsvRow
            oRow.Cells = New List(Of String)
            For Each sCell As String In sLine.Split(";")
                oRow.Cells.Add(sCell)
            Next
            retval.Rows.Add(oRow)
        Next
        Return retval
    End Function

    Public Function AddRow() As DTOCsvRow
        Dim oRow As New DTOCsvRow
        oRow.Cells = New List(Of String)
        _Rows.Add(oRow)
        Return oRow
    End Function

    Public Shadows Function ToString() As String
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DTOCsvRow In _Rows
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

    Public Function ToByteArray() As Byte()
        Dim src As String = Me.ToString()
        Dim retval As Byte() = New Byte(src.Length * 2 - 1) {}
        System.Buffer.BlockCopy(src.ToCharArray(), 0, retval, 0, retval.Length)
        Return retval
    End Function
End Class

Public Class DTOCsvRow
    Property Cells As List(Of String)

    Public Sub New()
        _Cells = New List(Of String)
    End Sub

    Public Sub AddCell(sField As String)
        _Cells.Add(sField)
    End Sub

    Public Sub AddCell(oAmt As DTOAmt)
        If oAmt Is Nothing Then
            _Cells.Add("")
        Else
            _Cells.Add(oAmt.Eur)
        End If
    End Sub

End Class


