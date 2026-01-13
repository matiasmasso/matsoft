Public Class AlbBloqueig


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAlbBloqueig)
        Return Await Api.Fetch(Of DTOAlbBloqueig)(exs, "AlbBloqueig", oGuid.ToString())
    End Function

    Shared Function Search(oContact As DTOContact, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As DTOAlbBloqueig
        Return Api.FetchSync(Of DTOAlbBloqueig)(exs, "AlbBloqueig/Search", oContact.Guid.ToString, CInt(oCodi))
    End Function


    Shared Function Load(ByRef oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Boolean
        If Not oAlbBloqueig.IsLoaded And Not oAlbBloqueig.IsNew Then
            Dim pAlbBloqueig = Api.FetchSync(Of DTOAlbBloqueig)(exs, "AlbBloqueig", oAlbBloqueig.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAlbBloqueig)(pAlbBloqueig, oAlbBloqueig, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAlbBloqueig)(oAlbBloqueig, exs, "AlbBloqueig")
        oAlbBloqueig.IsNew = False
    End Function


    Shared Async Function Delete(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAlbBloqueig)(oAlbBloqueig, exs, "AlbBloqueig")
    End Function

    Shared Async Function BloqueigStart(oUser As DTOUser, oContact As DTOContact, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oBloqueig = FEB2.AlbBloqueig.Search(oContact, oCodi, exs)
        If oBloqueig Is Nothing Then
            oBloqueig = New DTOAlbBloqueig
            With oBloqueig
                .User = oUser
                .Contact = oContact
                .Codi = oCodi
                .Fch = DateTime.Now
            End With
            retval = Await Update(oBloqueig, exs)
        Else
            Dim sUser As String = DTOUser.NicknameOrElse(oBloqueig.User)
            Dim sDoc As String = If(oBloqueig.Codi = DTOAlbBloqueig.Codis.PDC, "oberta una comanda", "obert un albará")
            Dim sMsg As String = String.Format("{0} té {1} de {2} des de {3}", sUser, sDoc, oBloqueig.Contact.FullNom, TextHelper.VbFormat(oBloqueig.Fch, "dd/MM/yy HH:mm"))
            exs.Add(New Exception(sMsg))
        End If
        Return retval
    End Function

    Shared Async Function BloqueigAllStart(oUser As DTOUser, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oBloqueig As New DTOAlbBloqueig
        With oBloqueig
            .User = oUser
            .Codi = oCodi
            .Fch = DateTime.Now
        End With
        retval = Await Update(oBloqueig, exs)
        Return retval
    End Function



    Shared Async Function BloqueigAllEnd(oUser As DTOUser, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "AlbBloqueig/BloqueigEnd", oUser.Guid.ToString, Guid.Empty.ToString, CInt(oCodi))
    End Function

    Shared Function BloqueigEnd(oUser As DTOUser, oContact As DTOContact, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "AlbBloqueig/BloqueigEnd", oUser.Guid.ToString, oContact.Guid.ToString, CInt(oCodi))
    End Function
End Class

Public Class AlbBloqueigs

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOAlbBloqueig))
        Return Await Api.Fetch(Of List(Of DTOAlbBloqueig))(exs, "AlbBloqueigs", CInt(oEmp.Id))
    End Function

End Class
