Public Class Bank

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBank)
        Return Await Api.Fetch(Of DTOBank)(exs, "Bank", oGuid.ToString())
    End Function

    Shared Async Function FromCodi(oCountry As DTOCountry, sCodi As String, exs As List(Of Exception)) As Task(Of DTOBank)
        Return Await Api.Fetch(Of DTOBank)(exs, "Bank/FromCodi", oCountry.Guid.ToString, sCodi)
    End Function

    Shared Function logoSync(exs As List(Of Exception), oBank As DTOBank) As Byte()
        Return Api.FetchImageSync(exs, "Bank/logo", oBank.Guid.ToString())
    End Function

    Shared Async Function logo(exs As List(Of Exception), oBank As DTOBank) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Bank/logo", oBank.Guid.ToString())
    End Function


    Shared Function Load(ByRef oBank As DTOBank, exs As List(Of Exception)) As Boolean
        If Not oBank.IsLoaded And Not oBank.IsNew Then
            Dim pBank = Api.FetchSync(Of DTOBank)(exs, "Bank", oBank.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBank)(pBank, oBank, exs)
                If exs.Count = 0 Then
                    oBank.logo = FEB2.Bank.logoSync(exs, oBank)
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOBank, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.logo IsNot Nothing Then
                oMultipart.AddFileContent("logo", value.Logo)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Bank")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oBank As DTOBank, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBank)(oBank, exs, "Bank")
    End Function
End Class

Public Class Banks

    Shared Async Function All(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of List(Of DTOBank))
        Return Await Api.Fetch(Of List(Of DTOBank))(exs, "Banks", oCountry.Guid.ToString())
    End Function

    Shared Async Function Countries(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOCountry))
        Return Await Api.Fetch(Of List(Of DTOCountry))(exs, "Banks/countries", oLang.Tag)
    End Function

End Class
