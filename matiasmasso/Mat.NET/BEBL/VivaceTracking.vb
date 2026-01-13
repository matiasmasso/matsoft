Public Class VivaceTracking

    Public Shared Async Function Fetch(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of DTO.DTODelivery)

        Dim retval = BEBL.Delivery.Find(oDelivery.Guid)
        retval.Trace = New DTO.Integracions.Vivace.Trace
        retval.Trace.Url = DTO.Integracions.Vivace.Vivace.TrackingUrl(oDelivery)
        Dim html = Await MatHelperStd.FileSystemHelper.ReadHtmlAsync(exs, retval.Trace.Url)
        retval.Trace.MoreInfoAvailable = html.IndexOf("lbValTracking") > 0
        Dim Span = MatHelperStd.TextHelper.HtmlTagById(html, "span", "lbValHistorico").FirstOrDefault()
        If (String.IsNullOrEmpty(Span)) Then
            exs.Add(New Exception("missing lbValHistorico id span on Vivace tracking page"))
        Else
            Dim iStartValue As Integer = Span.IndexOf(">")
            iStartValue = Span.IndexOf(">", iStartValue + 1) + 1
            Dim iEndValue As Integer = Span.IndexOf("</font>", iStartValue)
            Dim spanValue As String = Span.Substring(iStartValue, iEndValue - iStartValue)
            Dim textInfo = (New System.Globalization.CultureInfo("es-ES", False)).TextInfo
            Dim properCase As String = textInfo.ToTitleCase(spanValue)
            'Dim multiline As String = properCase.Replace("</Br>", "\r")
            Dim multiline As String = properCase.Replace("</Br>", vbLf)
            Dim lines As String() = multiline.Split(vbLf)
            Dim cleanLines As List(Of String) = lines.Where(Function(x) x.Contains("|")).ToList()
            retval.Trace.Items = DTO.Integracions.Vivace.Trace.Item.Collection.Factory(exs, cleanLines)
        End If
        Return retval

    End Function
End Class
