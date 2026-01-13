Public Class DefaultImage
    Shared Function Find(oId As DTO.Defaults.ImgTypes) As DTODefaultImage
        Dim retval = DefaultImageLoader.Find(oId)
        Return retval
    End Function
    Shared Function Image(oId As DTO.Defaults.ImgTypes) As Byte()
        Return DefaultImageLoader.Image(oId)
    End Function

    Shared Function Update(value As DTODefaultImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DefaultImageLoader.Update(value, exs)
        Return retval
    End Function

    Shared Function Delete(value As DTODefaultImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DefaultImageLoader.Delete(value, exs)
        Return retval
    End Function
End Class
