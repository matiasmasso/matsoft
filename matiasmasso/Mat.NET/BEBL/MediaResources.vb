Public Class MediaResource
    Shared Function Find(Guid As Guid) As DTOMediaResource
        Return MediaResourceLoader.Find(Guid)
    End Function

    Shared Function FromHash(hash As String) As DTOMediaResource
        Return MediaResourceLoader.FromHash(hash)
    End Function

    Shared Function Thumbnail(oMediaResource As DTOMediaResource) As Byte()
        Return MediaResourceLoader.Thumbnail(oMediaResource)
    End Function

    Shared Function Load(ByRef oMediaResource As DTOMediaResource) As Boolean
        Return MediaResourceLoader.Load(oMediaResource)
    End Function

    Shared Function Update(oMediaResource As DTOMediaResource, ByRef exs As List(Of Exception)) As Boolean
        Return MediaResourceLoader.Update(oMediaResource, exs)
    End Function

    Shared Function Delete(oMediaResource As DTOMediaResource, ByRef exs As List(Of Exception)) As Boolean
        Return MediaResourceLoader.Delete(oMediaResource, exs)
    End Function

End Class

Public Class MediaResources
    Shared Function FromProductOrParent(oProduct As DTOProduct) As List(Of DTOMediaResource)
        Return MediaResourcesLoader.All(oProduct)
    End Function

    Shared Function Sprite(oGuids As List(Of Guid)) As Byte()
        Dim oImages = MediaResourcesLoader.SpriteImages(oGuids)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages)
        Return retval
    End Function


    Shared Function ProductSpecific(oProduct As DTOProduct) As List(Of DTOMediaResource)
        Return MediaResourcesLoader.ProductSpecific(oProduct)
    End Function

    Shared Function Delete(exs As List(Of Exception), items As DTOMediaResource.Collection) As Boolean
        Return MediaResourcesLoader.Delete(exs, items)
    End Function

    Shared Function ExistsFromProductOrChildren(oProduct As DTOProduct) As Boolean
        Return MediaResourcesLoader.ExistsFromProductOrChildren(oProduct)
    End Function

    Shared Function MissingResources(filenames As List(Of String)) As List(Of DTOMediaResource)
        Return MediaResourcesLoader.MissingResources(filenames)
    End Function

End Class