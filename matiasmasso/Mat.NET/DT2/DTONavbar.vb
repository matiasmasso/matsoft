Public Class DTONavbar
    Inherits DTONavbarItem
    Property format As formats

    Public Enum Formats
        horizontal
        vertical
    End Enum

    Shared Shadows Function Factory(oFormat As DTONavbar.Formats, Optional sId As String = "") As DTONavbar
        Dim retval As New DTONavbar
        With retval
            .Id = sId
            .format = oFormat
            .Items = New List(Of DTONavbarItem)
        End With
        Return retval
    End Function

    Public Shadows Function Html() As String
        Dim navAttributes As New System.Text.StringBuilder
        navAttributes.AppendLine(" class='" & Me.format.ToString & "'")
        If MyBase.Id > "" Then
            navAttributes.AppendLine(" id='" & MyBase.Id & "'")
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<nav" & navAttributes.ToString & ">")
        sb.Append("<ul>")
        For Each oItem As DTONavbarItem In MyBase.Items
            sb.Append(oItem.Html())
        Next
        sb.AppendLine("</ul>")
        sb.AppendLine("</nav>")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

Public Class DTONavbarItem
    Property Id As String
    Property Title As String
    Property NavigateTo As String
    Property Tooltip As String
    Property Selected As Boolean

    Property Items As List(Of DTONavbarItem)

    Public Sub New()
        MyBase.New
        _Items = New List(Of DTONavbarItem)
    End Sub

    Shared Function Factory(sTitle As String, Optional sNavigateTo As String = "#", Optional BlSelected As Boolean = False) As DTONavbarItem
        Dim retval As New DTONavbarItem
        With retval
            .Title = sTitle
            .NavigateTo = sNavigateTo
            .Selected = BlSelected
            .Items = New List(Of DTONavbarItem)
        End With
        Return retval
    End Function

    Public Function AddItem(sTitle As String, Optional sNavigateTo As String = "#", Optional BlSelected As Boolean = False) As DTONavbarItem
        Dim retval = DTONavbarItem.Factory(sTitle, sNavigateTo, BlSelected)
        _Items.Add(retval)
        Return retval
    End Function

    Public Function Html() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("<li")
        sb.Append(If(_Selected, " class='selected'", ""))
        sb.Append(If(_Id > "", " id='" & _Id & "'", ""))
        sb.Append(">")
        sb.Append("<a href='" & _NavigateTo & "' ")
        sb.Append(If(_Id > "", " data-ref='" & _Id & "'", ""))
        sb.Append(">")
        sb.Append(_Title)
        sb.Append("</a>")

        If _Items.Count > 0 Then
            sb.Append("<ul>")
            For Each oItem As DTONavbarItem In _Items
                sb.Append(oItem.Html())
            Next
            sb.AppendLine("</ul>")
        End If
        sb.Append("</li>")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

