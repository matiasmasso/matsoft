
Public Class MatJSonObject
    Public Property ValuePairs As MatJSonValuePairs

    Public Sub New(ParamArray items() As String)
        MyBase.New()
        _ValuePairs = New MatJSonValuePairs
        For i As Integer = 0 To items.Length - 1 Step 2
            AddValuePair(items(i), items(i + 1))
        Next
    End Sub

    Public Function AddValuePair(sName As String, sValue As String) As MatJSonValuePair
        Dim retval As New MatJSonValuePair(sName, sValue)
        _ValuePairs.Add(retval)
        Return retval
    End Function

    Public Shadows Function ToString() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("{")
        For Each oValuePair As MatJSonValuePair In _ValuePairs
            If Not oValuePair.Equals(_ValuePairs.First) Then sb.Append(",")
            sb.Append(oValuePair.ToString())
        Next
        sb.Append("}")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function ToBase64() As String
        Dim src As String = ToString()
        Dim retval As String = CryptoHelper.ToBase64(src)
        Return retval
    End Function

    Shared Function FromBase64(src As String) As MatJSonObject
        Dim sDecoded As String = CryptoHelper.FromBase64(src)
        Dim retval As MatJSonObject = FromString(sDecoded)
        Return retval
    End Function

    Shared Function FromString(src As String) As MatJSonObject
        Dim rs As String = src.Replace("{", "").Replace("}", "")
        rs = rs.Replace(TextHelper.VbChr(34), "")
        Dim lines() As String = rs.Split(",")
        Dim oArray As New MatJSonValuePairs
        For Each line In lines
            Dim fields() As String = line.Split(":")
            Dim oVP As New MatJSonValuePair(fields(0), fields(1))
            oArray.Add(oVP)
        Next
        Dim retval As New MatJSonObject
        retval.ValuePairs = oArray
        Return retval
    End Function

    Public Function GetValue(sFieldName As String) As String
        Dim retval As String = ""
        Dim oVp As MatJSonValuePair = _ValuePairs.Find(Function(x) x.Name = sFieldName)
        If oVp IsNot Nothing Then
            retval = oVp.Value
        End If
        Return retval
    End Function
End Class

Public Class MatJSonArray
    Inherits List(Of MatJSonObject)

    Public Shadows Function ToString() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("[")
        For Each oObject As MatJSonObject In Me
            If Not oObject.Equals(Me.First) Then sb.Append(",")
            sb.Append(oObject.ToString())
        Next
        sb.Append("]")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

Public Class MatJSonValuePair
    Public Property Name As String
    Public Property Value As String

    Public Sub New(sName As String, sValue As String)
        MyBase.New()
        _Name = sName
        _Value = sValue
    End Sub

    Public Shadows Function ToString() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(TextHelper.VbChr(34) & _Name & TextHelper.VbChr(34))
        sb.Append(":")
        If _Value.StartsWith("{") Or _Value.StartsWith("[") Then
            sb.Append(_Value)
        Else
            sb.Append(TextHelper.VbChr(34) & _Value & TextHelper.VbChr(34))
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

Public Class MatJSonValuePairs
    Inherits List(Of MatJSonValuePair)
End Class
