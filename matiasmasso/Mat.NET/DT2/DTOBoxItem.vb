Public Class DTOBoxItem
    Public Property Title As String
    Public Property Footer As String
    Public Property ImageUrl As String
    Public Property NavigateUrl As String

    Public Property Tag As DTOBaseGuid


    Shared Function Html(oBoxItem As DTOBoxItem) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(vbTab & "<div class='BoxItemContainer'>")
        sb.AppendLine(vbTab & vbTab & "<div class='BoxItemTitle'>" & oBoxItem.Title & "</div>")
        sb.AppendLine(vbTab & vbTab & "<div class='BoxItemImgContainer' >")
        sb.AppendLine(vbTab & vbTab & vbTab & "<a href='" & oBoxItem.NavigateUrl & "'>")
        sb.AppendLine(vbTab & vbTab & vbTab & vbTab & "<img src='" & oBoxItem.ImageUrl & "'/>")
        sb.AppendLine(vbTab & vbTab & vbTab & "</a>")
        sb.AppendLine(vbTab & vbTab & "</div>")
        sb.AppendLine(vbTab & vbTab & "<div class='BoxItemFooter'>" & oBoxItem.Footer & "</div>")
        sb.AppendLine(vbTab & "</div>")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class


