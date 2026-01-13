Public Class Pagination
    Public Property ItemsPerPage As Integer
    Public Property ItemsCount As Integer
    Public Property CurrentPageFirstItem As Integer
    Public Property CurrentPageLastItem As Integer

    Public Property FirstPageIndex As Integer = 1
    Public Property PreviousPageIndex As Integer
    Public Property CurrentPageIndex As Integer = 1
    Public Property NextPageIndex As Integer
    Public Property LastPageIndex As Integer


    Public Sub New(iItemsCount As Integer, iItemsPerPage As Integer, Optional iPageIndex As Integer = 1)
        MyBase.New()
        If iPageIndex = 0 Then iPageIndex = 1
        _ItemsCount = iItemsCount
        _ItemsPerPage = iItemsPerPage

        _LastPageIndex = Math.Round(_ItemsCount / _ItemsPerPage, 0, MidpointRounding.AwayFromZero)
        If _LastPageIndex * _ItemsPerPage < _ItemsCount Then _LastPageIndex += 1
        If _LastPageIndex = 0 Then _LastPageIndex = 1

        _CurrentPageIndex = IIf(iPageIndex < _FirstPageIndex, _FirstPageIndex, iPageIndex)
        If _CurrentPageIndex > _LastPageIndex Then _CurrentPageIndex = _LastPageIndex

        _PreviousPageIndex = IIf(_CurrentPageIndex > _FirstPageIndex, _CurrentPageIndex - 1, _FirstPageIndex)
        _NextPageIndex = IIf(_CurrentPageIndex < _LastPageIndex, _CurrentPageIndex + 1, _LastPageIndex)


        _CurrentPageFirstItem = _ItemsPerPage * (_CurrentPageIndex - 1)
        _CurrentPageLastItem = _CurrentPageFirstItem + _ItemsPerPage - 1
        If _CurrentPageLastItem >= ItemsCount Then _CurrentPageLastItem = ItemsCount - 1
    End Sub

    Public Function IsDisplayable() As Boolean
        Return ItemsCount > ItemsPerPage
    End Function

    Public Function Html() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<ul>")

        Dim FirstPage As Integer = 1
        If _LastPageIndex > 3 And _CurrentPageIndex > 2 Then
            FirstPage = _CurrentPageIndex - 1
        End If

        Dim LastPage As Integer = FirstPage + 2
        If LastPage > _LastPageIndex Then
            LastPage = _LastPageIndex
            If LastPage > 2 Then FirstPage = LastPage - 2
        End If

        If _LastPageIndex > 1 Then
            sb.AppendLine("<li pageIndex='" & _FirstPageIndex & "'")
            If _CurrentPageIndex = _FirstPageIndex Then sb.Append(" class='disabled'")
            sb.AppendLine("><a data-pageindex='" & _FirstPageIndex & "' href='#'>")
            sb.AppendLine(HttpUtility.HtmlEncode("<<"))
            sb.AppendLine("</a></li>")

            sb.AppendLine("<li")
            If _CurrentPageIndex = _FirstPageIndex Then sb.Append(" class='disabled'")
            sb.AppendLine("><a data-pageindex='" & _PreviousPageIndex & "' href='#'>")
            sb.AppendLine(HttpUtility.HtmlEncode("<"))
            sb.AppendLine("</a></li>")
        End If

        For i As Integer = FirstPage To LastPage
            sb.Append("<li")
            If i = _CurrentPageIndex Then sb.Append(" class='active'")
            sb.Append(">")
            sb.Append("<a data-pageindex='" & i & "' href='#'>" & i.ToString & "</a>")
            sb.AppendLine("</li>")
        Next

        If _LastPageIndex > 1 Then
            sb.AppendLine("<li")
            If _CurrentPageIndex >= _LastPageIndex Then sb.Append(" class='disabled'")
            sb.AppendLine("><a data-pageindex='" & _NextPageIndex & "' href='#'>")
            sb.AppendLine(HttpUtility.HtmlEncode(">"))
            sb.AppendLine("</a></li>")

            sb.AppendLine("<li")
            If _CurrentPageIndex >= _LastPageIndex Then sb.Append(" class='disabled'")
            sb.AppendLine("><a data-pageindex='" & _LastPageIndex & "' href='#'>")
            sb.AppendLine(HttpUtility.HtmlEncode(">>"))
            sb.AppendLine("</a></li>")
        End If
        sb.AppendLine("</ul>")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
