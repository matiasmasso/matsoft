Public Class CsvHelper

    Shared Function Save(exs As List(Of Exception), oCsv As CsvFile, sfilename As String) As Boolean
        Return FileSystemHelper.SaveTextToFile(oCsv.ToString, sfilename, exs)
    End Function

    Public Class CsvFile
        Property Filename As String
        Property Rows As List(Of CsvRow)

        Public Sub New()
            MyBase.New
            _Rows = New List(Of CsvRow)
        End Sub

        Shared Function Factory(oByteArray As Byte(), Optional sFilename As String = "") As CsvFile
            Dim text = System.Text.Encoding.UTF8.GetString(oByteArray, 0, oByteArray.Length)
            Dim lines = text.Split(vbCrLf).Where(Function(x) Not String.IsNullOrEmpty(x)).ToList
            Dim retval As New CsvFile
            retval.Filename = sFilename
            For Each line In lines
                Dim oRow = CsvRow.Factory(line)
                retval.Rows.Add(oRow)
            Next
            Return retval
        End Function

        Public Function HeaderRow() As String
            Dim retval As String = ""
            If _Rows.Count > 0 Then
                retval = _Rows.First.ToString
            End If
            Return retval
        End Function

        Public Function ItemRows() As List(Of CsvRow)
            Dim retval As New List(Of CsvRow)
            For i = 1 To _Rows.Count - 1
                retval.Add(_Rows(i))
            Next
            Return retval
        End Function

        Public Function AddRow() As CsvRow
            Dim retval As New CsvRow
            _Rows.Add(retval)
            Return retval
        End Function

        Public Shadows Function ToString() As String
            Dim sb As New Text.StringBuilder
            For Each oRow In _Rows
                sb.AppendLine(oRow.ToString)
            Next
            Dim retval = sb.ToString
            Return retval
        End Function
    End Class

    Public Class CsvRow
        Property Cells As List(Of String)

        Public Sub New()
            MyBase.New
            _Cells = New List(Of String)
        End Sub

        Shared Function Factory(line As String)
            Dim retval As New CsvRow
            retval.Cells = line.Split(";").ToList
            Return retval
        End Function

        Public Sub AddCells(ParamArray cellValues As String())
            For Each value In cellValues
                _Cells.Add(value)
            Next
        End Sub

        Public Shadows Function ToString() As String
            Return String.Join(";", _Cells.ToArray)
        End Function
    End Class


End Class
