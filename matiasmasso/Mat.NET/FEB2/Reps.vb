Public Class Rep
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTORep)
        Return Await Api.Fetch(Of DTORep)(exs, "rep", oGuid.ToString())
    End Function

    Shared Function ExistsSync(exs As List(Of Exception), oContact As DTOContact) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "rep/exists", oContact.Guid.ToString())
    End Function

    Shared Async Function Foto(exs As List(Of Exception), oRep As DTORep) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "rep/foto", oRep.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef orep As DTORep) As Boolean
        If Not orep.IsLoaded And Not orep.IsNew Then
            Dim prep = Api.FetchSync(Of DTORep)(exs, "rep", orep.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORep)(prep, orep, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTORep) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("foto", value.foto)
            retval = Await Api.Upload(oMultipart, exs, "rep")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), orep As DTORep) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORep)(orep, exs, "rep")
    End Function

    Shared Async Function Archive(oRep As DTORep, exs As List(Of Exception)) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "rep/archive", oRep.Guid.ToString())
    End Function

    Shared Function ArchiveSync(exs As List(Of Exception), oRep As DTORep) As List(Of DTOPurchaseOrder)
        Return Api.FetchSync(Of List(Of DTOPurchaseOrder))(exs, "rep/archive", oRep.Guid.ToString())
    End Function


    Shared Function RaoSocialFacturacio(oRep As DTORep) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim exs As New List(Of Exception)
        If FEB2.Rep.Load(exs, oRep) Then
            If oRep.raoSocialFacturacio Is Nothing Then
                retval = DTOProveidor.FromContact(oRep)
            Else
                retval = oRep.raoSocialFacturacio
            End If
        End If
        Return retval
    End Function

    Shared Async Function Baixa(exs As List(Of Exception), oEmp As DTOEmp, oRep As DTORep, DtFch As Date, removePrivileges As Boolean) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "rep/Baixa", oEmp.Id, oRep.Guid.ToString, FormatFch(DtFch), OpcionalBool(removePrivileges))
    End Function


End Class

Public Class Reps
    Inherits _FeblBase

    Shared Async Function AllActiveWithIcons(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTORep))
        Dim oReps = Await AllActive(oUser, exs)
        If exs.Count = 0 And oReps.Count > 0 Then
            Dim oSprite = Await Sprite(exs, oReps)
            If exs.Count = 0 Then
                For i As Integer = 0 To oReps.Count - 1
                    oReps(i).Img48 = LegacyHelper.SpriteHelper.Extract(oSprite, i, oReps.Count)
                Next
            End If
        End If
        Return oReps
    End Function

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTORep))
        Return Await Api.Fetch(Of List(Of DTORep))(exs, "reps/fromProduct", oProduct.Guid.ToString())
    End Function

    Shared Async Function AllActive(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTORep))
        Return Await Api.Fetch(Of List(Of DTORep))(exs, "reps/active", oUser.Guid.ToString())
    End Function

    Shared Async Function Sprite(exs As List(Of Exception), oReps As List(Of DTORep)) As Task(Of Byte())
        Dim oGuids = oReps.Select(Function(x) x.Guid).ToList
        Dim retval As Byte()  = Await Api.downloadImage(oGuids, exs, "reps/sprite")
        Return retval
    End Function

    Shared Async Function Ibans(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "reps/ibans", oEmp.Id)
    End Function

    Shared Async Function Emails(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOEmail))
        Return Await Api.Fetch(Of List(Of DTOEmail))(exs, "reps/emails/fromEmp", oEmp.Id)
    End Function

    Shared Async Function Emails(exs As List(Of Exception), oArea As DTOArea, oChannel As DTODistributionChannel) As Task(Of List(Of DTOEmail))
        Return Await Api.Fetch(Of List(Of DTOEmail))(exs, "reps/emails", oArea.Guid.ToString, oChannel.Guid.ToString())
    End Function

    Shared Async Function Emails(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOEmail))
        Return Await Api.Fetch(Of List(Of DTOEmail))(exs, "reps/emails/FromProduct", oProduct.Guid.ToString())
    End Function

    Shared Async Function Saldos(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "reps/saldos", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function WithRetencions(exs As List(Of Exception), oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As Task(Of List(Of DTORep))
        Return Await Api.Fetch(Of List(Of DTORep))(exs, "reps/WithRetencions", oEmp.Id, FormatFch(FchFrom), FormatFch(FchTo))
    End Function
End Class
