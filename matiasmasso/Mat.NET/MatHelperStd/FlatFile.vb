Public Class FlatFile
    Private _FieldLengths As List(Of Integer)
    Property Segments As List(Of Segment)
    Property Source As String

    Property FieldLengths As List(Of Integer)
        Get
            Return _FieldLengths
        End Get
        Set(value As List(Of Integer))
            _FieldLengths = value
            For Each oSegment In _Segments
                oSegment.Fields = oSegment.GetFields(_FieldLengths)
            Next
        End Set
    End Property

    Public Sub New()
        MyBase.New
        _Segments = New List(Of Segment)
    End Sub

    Shared Function Factory(filename As String, fieldLengths As List(Of Integer)) As FlatFile
        Dim retval As New FlatFile
        retval.LoadSegmentsFromFilename(filename, fieldLengths)
        Return retval
    End Function

    Public Sub LoadSegmentsFromFilename(filename As String, fieldLengths As List(Of Integer))
        Dim sb As New Text.StringBuilder
        _Segments = New List(Of Segment)
        _FieldLengths = fieldLengths
        Using reader As New System.IO.StreamReader(filename)
            Do
                Dim line = reader.ReadLine()
                If String.IsNullOrEmpty(line) Then Exit Do

                sb.AppendLine(line)
                Dim oSegment = Segment.Factory(line, fieldLengths)
                _Segments.Add(oSegment)
            Loop
            _Source = sb.ToString
        End Using
    End Sub


    Public Class Segment
        Property src As String
        Property Fields As List(Of Field)

        Public Sub New()
            MyBase.New
            _Fields = New List(Of Field)
        End Sub

        Shared Function Factory(src As String, fieldLengths As List(Of Integer)) As Segment
            Dim retval As New Segment
            With retval
                .src = src
                .Fields = retval.GetFields(fieldLengths)
            End With
            retval.src = src
            Return retval
        End Function

        Public Function GetFields(fieldLengths As List(Of Integer)) As List(Of Field)
            Dim retval As New List(Of Field)
            Dim iPos As Integer = 0
            For i As Integer = 0 To fieldLengths.Count - 1
                Dim iLen = fieldLengths(i)
                Dim oField = Field.Factory(src, iPos, iLen)
                retval.Add(oField)
                iPos += iLen
            Next
            Return retval
        End Function

        Public Function FieldValue(idx As Integer) As String
            Dim retval As String = _Fields(idx).Value.Trim()
            Return retval
        End Function
    End Class

    Public Class Field
        Property Value As String
        Property Pos As Integer
        Property Len As Integer

        Shared Function Factory(src As String, iPos As Integer, iLen As Integer) As Field
            Dim retval As New Field
            With retval
                .Pos = iPos
                .Len = iLen
                If src.Length >= iPos + iLen Then
                    .Value = src.Substring(iPos, iLen)
                End If
            End With
            Return retval
        End Function

        Public Shadows Function ToString() As String
            Return Value
        End Function
    End Class
End Class
