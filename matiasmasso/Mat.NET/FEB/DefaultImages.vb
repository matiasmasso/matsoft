Public Class DefaultImage
    Inherits _FeblBase

    Shared Async Function Find(id As DTO.Defaults.ImgTypes, exs As List(Of Exception)) As Task(Of DTODefaultImage)
        Return Await Api.Fetch(Of DTODefaultImage)(exs, "DefaultImage", id)
    End Function

    Shared Async Function Image(id As DTO.Defaults.ImgTypes, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "DefaultImage/image", id)
    End Function

    Shared Function Load(ByRef oDefaultImage As DTODefaultImage, exs As List(Of Exception)) As Boolean
        Dim pDefaultImage = Api.FetchSync(Of DTODefaultImage)(exs, "DefaultImage", oDefaultImage.Id)
        If exs.Count = 0 Then
            DTOBaseGuid.CopyPropertyValues(Of DTODefaultImage)(pDefaultImage, oDefaultImage, exs)
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTODefaultImage, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Image IsNot Nothing Then
                oMultipart.AddFileContent("image", value.Image)
            End If
            retval = Await Api.Upload(oMultipart, exs, "DefaultImage")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oDefaultImage As DTODefaultImage, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODefaultImage)(oDefaultImage, exs, "DefaultImage")
    End Function
End Class

