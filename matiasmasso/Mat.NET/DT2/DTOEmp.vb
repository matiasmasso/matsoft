Public Class DTOEmp
    Inherits DTO.DTOEmp

    Property org As DTOContact
    Property mgz As DTOMgz
    Property taller As DTOTaller
    <JsonIgnore> Property Logo As Image




    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oId As Ids)
        MyBase.New(oId)
    End Sub

    Property org As DTOContact
    Property mgz As DTOMgz


    Public Function DefaultCountry() As DTOCountry
        Dim retval = _org.address.zip.location.zona.country
        Return retval
    End Function

    Public Shadows Function Equals(oEmp As Object) As Boolean
        Dim retval As Boolean
        If oEmp IsNot Nothing Then
            If TypeOf oEmp Is DTOEmp Then
                retval = oEmp.id = MyBase.id
            End If
        End If
        Return retval
    End Function

    Public Function AbsoluteUrl(ByVal ParamArray UrlSegments() As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendFormat("https://www.{0}", MyBase.domini)

        For intLoopIndex As Integer = 0 To UBound(UrlSegments)
            Dim sSegment As String = UrlSegments(intLoopIndex).Trim
            If Not sb.ToString.EndsWith("/") Then sb.Append("/")
            If sSegment.StartsWith("/") Then sSegment = sSegment.Substring(1)
            sb.Append(sSegment)
        Next intLoopIndex

        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
