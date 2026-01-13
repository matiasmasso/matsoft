Public Class PortadaImg

    Shared Function ImageMime(id As String) As ImageMime
        Return PortadaImgLoader.ImageMime(id)
    End Function

End Class



Public Class PortadaImgs
    Shared Function All() As List(Of PortadaImgModel)
        Return PortadaImgsLoader.All()
    End Function
End Class

