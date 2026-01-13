Public Class Mem

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOMem)
        Return Await Api.Fetch(Of DTOMem)(exs, "Mem", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oMem As DTOMem, exs As List(Of Exception)) As Boolean
        If Not oMem.IsLoaded And Not oMem.IsNew Then
            Dim pMem = Api.FetchSync(Of DTOMem)(exs, "Mem", oMem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOMem)(pMem, oMem, exs)
                If oMem.Docfiles.Count > 0 Then
                    SetSprite(exs, oMem)
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Function SetSprite(exs As List(Of Exception), ByRef oMem As DTOMem) As Boolean
        Dim oSprite = Api.FetchImageSync(exs, "Mem/Sprite", oMem.Guid.ToString())
        If exs.Count = 0 And oSprite IsNot Nothing Then
            For idx As Integer = 0 To oMem.docfiles.Count - 1
                oMem.Docfiles(idx).Thumbnail = LegacyHelper.SpriteHelper.Extract(oSprite, idx, oMem.Docfiles.Count)
            Next
        End If
        Return exs.Count = 0
    End Function


    Shared Async Function Update(exs As List(Of Exception), value As DTOMem) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            Dim idx As Integer
            For Each oDocfile In value.docfiles
                idx += 1
                oMultipart.AddFileContent(String.Format("docfile_thumbnail_{0:000}", idx), oDocfile.Thumbnail)
                oMultipart.AddFileContent(String.Format("docfile_stream_{0:000}", idx), oDocfile.Stream)
            Next
            retval = Await Api.Upload(oMultipart, exs, "Mem")
        End If
        Return retval
    End Function


    Shared Async Function Delete(oMem As DTOMem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOMem)(oMem, exs, "Mem")
    End Function

End Class


Public Class Mems


    Shared Async Function All(exs As List(Of Exception), Optional oContact As DTOContact = Nothing, Optional oCod As DTOMem.Cods = DTOMem.Cods.NotSet, Optional oUser As DTOUser = Nothing, Optional offset As Integer = 0, Optional pagesize As Integer = 0, Optional onlyfromLast24H As Boolean = False, Optional year As Integer = 0) As Task(Of List(Of DTOMem))
        Dim oContactGuid = Guid.Empty
        If oContact IsNot Nothing Then oContactGuid = oContact.Guid
        Dim oUserGuid = Guid.Empty
        If oUser IsNot Nothing Then oUserGuid = oUser.Guid
        Return Await Api.Fetch(Of List(Of DTOMem))(exs, "Mems", oContactGuid.ToString, oCod, oUserGuid.ToString, offset, pagesize, If(onlyfromLast24H, "1", "0"), year)
    End Function

    Shared Function AllSync(exs As List(Of Exception), Optional oContact As DTOContact = Nothing, Optional oCod As DTOMem.Cods = DTOMem.Cods.NotSet, Optional oUser As DTOUser = Nothing, Optional offset As Integer = 0, Optional pagesize As Integer = 0, Optional onlyfromLast24H As Boolean = False) As List(Of DTOMem)
        Dim oContactGuid As Guid = Nothing
        If oContact IsNot Nothing Then oContactGuid = oContact.Guid
        Return Api.FetchSync(Of List(Of DTOMem))(exs, "Mems", oContactGuid.ToString, oCod, oUser.Guid.ToString, offset, pagesize, If(onlyfromLast24H, "1", "0"))
    End Function

    Shared Async Function Count(oCod As DTOMem.Cods, oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOMem))
        Return Await Api.Fetch(Of List(Of DTOMem))(exs, "Mems/Count", oCod, oUser.Guid.ToString())
    End Function

    Shared Async Function Impagats(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOMem))
        Return Await Api.Fetch(Of List(Of DTOMem))(exs, "Mems/Impagats", oEmp.Id)
    End Function

End Class
