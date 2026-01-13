Public Class ContactDoc

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOContactDoc)
        Return Await Api.Fetch(Of DTOContactDoc)(exs, "ContactDoc", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oContactDoc As DTOContactDoc, exs As List(Of Exception)) As Boolean
        If Not oContactDoc.IsLoaded And Not oContactDoc.IsNew Then
            Dim pContactDoc = Api.FetchSync(Of DTOContactDoc)(exs, "ContactDoc", oContactDoc.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContactDoc)(pContactDoc, oContactDoc, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOContactDoc, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ContactDoc")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oContactDoc As DTOContactDoc, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContactDoc)(oContactDoc, exs, "ContactDoc")
    End Function

    Shared Function Url(oContactDoc As DTOContactDoc, AbsoluteUrl As Boolean) As String
        Dim retval As String = ""
        If oContactDoc IsNot Nothing Then
            retval = FEB2.UrlHelper.Factory(AbsoluteUrl, DTODocFile.Cods.Download, CryptoHelper.StringToHexadecimal(oContactDoc.DocFile.Hash))
        End If
        Return retval
    End Function
End Class

Public Class ContactDocs

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As Task(Of List(Of DTOContactDoc))
        Return Await Api.Fetch(Of List(Of DTOContactDoc))(exs, "ContactDocs/fromUser", oUser.Guid.ToString, CInt(oType).ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact, Optional year As Integer = 0, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As Task(Of List(Of DTOContactDoc))
        Dim oContactGuid As Guid = Nothing
        If oContact IsNot Nothing Then oContactGuid = oContact.Guid
        Return Await Api.Fetch(Of List(Of DTOContactDoc))(exs, "ContactDocs", oContactGuid.ToString, year, oType)
    End Function

    Shared Async Function Mod145s(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOContactDoc))
        Return Await Api.Fetch(Of List(Of DTOContactDoc))(exs, "ContactDocs/Mod145s", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function ExcelMod145s(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Return Await Api.Fetch(Of ExcelHelper.Sheet)(exs, "ContactDocs/Mod145s/Excel", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Retencions(oContacts As List(Of DTOContact), exs As List(Of Exception)) As Task(Of List(Of DTOContactDoc))
        Return Await Api.Execute(Of List(Of DTOContact), List(Of DTOContactDoc))(oContacts, exs, "ContactDocs/Retencions")
    End Function



End Class

