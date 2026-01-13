Public Class SegSocialGrup
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSegSocialGrup)
        Return Await Api.Fetch(Of DTOSegSocialGrup)(exs, "SegSocialGrup", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oSegSocialGrup As DTOSegSocialGrup, exs As List(Of Exception)) As Boolean
        If Not oSegSocialGrup.IsLoaded And Not oSegSocialGrup.IsNew Then
            Dim pSegSocialGrup = Api.FetchSync(Of DTOSegSocialGrup)(exs, "SegSocialGrup", oSegSocialGrup.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSegSocialGrup)(pSegSocialGrup, oSegSocialGrup, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oSegSocialGrup As DTOSegSocialGrup, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOSegSocialGrup)(oSegSocialGrup, exs, "SegSocialGrup")
        oSegSocialGrup.IsNew = False
    End Function

    Shared Async Function Delete(oSegSocialGrup As DTOSegSocialGrup, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSegSocialGrup)(oSegSocialGrup, exs, "SegSocialGrup")
    End Function
End Class

Public Class SegSocialGrups
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOSegSocialGrup))
        Return Await Api.Fetch(Of List(Of DTOSegSocialGrup))(exs, "SegSocialGrups")
    End Function

End Class

