Public Class GrfMesValue

    Shared Function Url(oUser As DTOUser, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oUser IsNot Nothing Then
            retval = UrlHelper.Image(DTO.Defaults.ImgTypes.SalesGrafic, oUser.Guid, AbsoluteUrl)
        End If
        Return retval
    End Function

    Shared Async Function Image(exs As List(Of Exception), oUser As DTOUser, ByVal iWidth As Integer, ByVal iHeight As Integer) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "GrfMesValue/Image", oUser.Guid.ToString())
    End Function

End Class
