Public Class AeatModel

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid, oUser As DTOUser) As Task(Of DTOAeatModel)
        Return Await Api.Fetch(Of DTOAeatModel)(exs, "AeatModel", oGuid.ToString(), oUser.Guid.ToString())
    End Function


    Shared Function Load(ByRef oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Boolean
        If Not oAeatModel.IsLoaded And Not oAeatModel.IsNew Then
            Dim pAeatModel = Api.FetchSync(Of DTOAeatModel)(exs, "AeatModel", oAeatModel.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAeatModel)(pAeatModel, oAeatModel, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAeatModel)(oAeatModel, exs, "AeatModel")
        oAeatModel.IsNew = False
    End Function


    Shared Async Function Delete(oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAeatModel)(oAeatModel, exs, "AeatModel")
    End Function

    Shared Function Url(oAeatModel As DTOAeatModel, Optional absoluteUrl As Boolean = False) As String
        Dim oDomain = DTOWebDomain.Default(absoluteUrl)
        Dim retval = oDomain.Url("AeatModel", oAeatModel.Guid.ToString())
        Return retval
    End Function
End Class

Public Class AeatModels

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of DTOAeatModel.Collection)
        Return Await Api.Fetch(Of DTOAeatModel.Collection)(exs, "AeatModels", oUser.Guid.ToString())
    End Function

End Class
