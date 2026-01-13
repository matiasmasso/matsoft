

Public Class HttpRequestHelper
    Property FileValues As List(Of FileValue)
    Property StringValues As List(Of StringValue)

    Public Sub New(oRequest As System.Web.HttpRequest)
        _FileValues = New List(Of FileValue)
        _StringValues = New List(Of StringValue)

        For Each sFileKey In oRequest.Files
            Dim idx As Integer = _FileValues.Count
            Dim oFileValue As New FileValue
            oFileValue.Key = sFileKey
            oFileValue.HttpPostedFile = oRequest.Files(idx)
            _FileValues.Add(oFileValue)
        Next

        For Each key In oRequest.Form
            Dim value = HttpContext.Current.Request.Form(key)
            Dim oStringValue As New StringValue
            oStringValue.Key = key
            oStringValue.Value = value
            _StringValues.Add(oStringValue)
        Next

    End Sub


    Public Function GetFileBytes(sKey As String) As Byte()
        Dim retval As Byte() = Nothing
        Dim oFileValue = _FileValues.FirstOrDefault(Function(x) x.Key.ToLower = sKey.ToLower)
        If oFileValue IsNot Nothing Then
            Using binaryReader As New System.IO.BinaryReader(oFileValue.HttpPostedFile.InputStream)
                retval = binaryReader.ReadBytes(oFileValue.HttpPostedFile.ContentLength)
            End Using
        End If
        Return retval
    End Function

    Public Function GetDocFile(Optional sDocfileKey As String = "docfile") As DTODocFile

        Dim retval As New DTODocFile(GetValue(sDocfileKey & "_hash"))
        With retval
            .nom = GetValue(sDocfileKey & "_nom")
            .mime = GetInt(sDocfileKey & "_mime")
            .pags = GetInt(sDocfileKey & "_pags")
            .hRes = GetInt(sDocfileKey & "_hres")
            .vRes = GetInt(sDocfileKey & "_vres")

            If ContainsStringValue(sDocfileKey & "_width") Or ContainsStringValue(sDocfileKey & "_height") Then
                .Size = New System.Drawing.Size(GetInt(sDocfileKey & "_width"), GetInt(sDocfileKey & "_height"))
            End If

            If _FileValues.Any(Function(x) x.Key.ToLower = sDocfileKey & "_thumbnail") Then
                .Thumbnail = GetFileBytes(sDocfileKey & "_thumbnail")
            End If
            If _FileValues.Any(Function(x) x.Key.ToLower = sDocfileKey & "_stream") Then
                .Stream = GetFileBytes(sDocfileKey & "_stream")
                .Length = .Stream.Length
            End If
        End With
        Return retval
    End Function



    Public Function GetInt(sKey As String) As Integer
        Dim retval As Integer = 0
        If ContainsStringValue(sKey) Then
            Dim sValue As String = GetValue(sKey)
            If IsNumeric(sValue) Then
                retval = CInt(sValue)
            End If
        End If
        Return retval
    End Function

    Public Function GetBool(sKey As String) As Boolean
        Dim retval As Boolean = False
        If ContainsStringValue(sKey) Then
            retval = GetValue(sKey)
        End If
        Return retval
    End Function

    Public Function GetValue(sKey As String) As String
        Dim retval As String = ""
        Dim item = _StringValues.FirstOrDefault(Function(x) x.Key.ToLower = sKey.ToLower)
        If item IsNot Nothing Then
            retval = item.Value
        End If
        Return retval
    End Function

    Public Function GetValues(sKey As String) As List(Of String)
        Dim retval = _StringValues.Where(Function(x) x.Key.ToLower = sKey.ToLower).
            Select(Function(y) y.Value).
            ToList
        Return retval
    End Function

    Public Function ContainsStringValue(sKey As String) As Boolean
        Dim retval = _StringValues.Any(Function(x) x.Key.ToLower = sKey.ToLower)
        Return retval
    End Function
    Public Function ContainsFileValue(sKey As String) As Boolean
        Dim retval = _FileValues.Any(Function(x) x.Key.ToLower = sKey.ToLower)
        Return retval
    End Function

    Public Class FileValue
        Property Key As String
        Property HttpPostedFile As HttpPostedFile
    End Class

    Public Class StringValue
        Property Key As String
        Property Value As String
    End Class
End Class
