Public Class WinmenuItem

    Shared Function Load(ByRef oWinMenuItem As DTOWinMenuItem, exs As List(Of Exception)) As Boolean
        If Not oWinMenuItem.IsLoaded And Not oWinMenuItem.IsNew Then
            Dim pWinMenuItem = Api.FetchSync(Of DTOWinMenuItem)(exs, "WinMenuItem", oWinMenuItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWinMenuItem)(pWinMenuItem, oWinMenuItem, exs)
            End If
            oWinMenuItem.Icon = Api.FetchImageSync(exs, "WinMenuItem/icon", oWinMenuItem.Guid.ToString())
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oWinMenuItem As DTOWinMenuItem, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(oWinMenuItem, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Icon", oWinMenuItem.Icon)
            retval = Await Api.Upload(oMultipart, exs, "WinMenuItem/upload")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oWinMenuItem As DTOWinMenuItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWinMenuItem)(oWinMenuItem, exs, "WinMenuItem")
    End Function
End Class

Public Class WinMenuItems

#Region "CRUD"

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOWinMenuItem))
        Dim retval = Await Api.Fetch(Of List(Of DTOWinMenuItem))(exs, "winmenuitems", oUser.Guid.ToString())

        If exs.Count = 0 Then
            For Each item In retval
                If item.parent IsNot Nothing Then
                    Dim oParent = retval.FirstOrDefault(Function(x) x.Guid.Equals(item.parent.Guid))
                    If oParent IsNot Nothing Then
                        item.parent = oParent
                    End If
                End If
            Next

        End If

        Return retval
    End Function


    Shared Async Function Sprite(exs As List(Of Exception), items As List(Of DTOWinMenuItem)) As Task(Of System.Drawing.Image)
        Dim oGuids = items.Select(Function(x) x.Guid).ToList
        Dim oByteArray = Await Api.DownloadImageStream(Of List(Of Guid))(oGuids, exs, "winmenuitems/sprite")
        Dim ms As New System.IO.MemoryStream(oByteArray)
        Dim retval = System.Drawing.Image.FromStream(ms)
        Return retval
    End Function


    Shared Async Function Sprite_Deprecated(exs As List(Of Exception), items As List(Of DTOWinMenuItem)) As Task(Of Byte())
        Dim oGuids = items.Select(Function(x) x.Guid).ToList
        Dim retval = Await Api.downloadImage(Of List(Of Guid))(oGuids, exs, "winmenuitems/sprite")
        Return retval
    End Function

    Shared Async Function Sprite(exs As List(Of Exception), items As List(Of DTOWinMenuItem), localSpriteHash As String) As Task(Of Byte())
        Dim oGuids = items.Select(Function(x) x.Guid).ToList
        Dim urlFriendlyHash = CryptoHelper.UrlFriendlyBase64(localSpriteHash)
        Dim retval = Await Api.downloadImage(Of List(Of Guid))(oGuids, exs, "winmenuitems/sprite", urlFriendlyHash)
        Return retval
    End Function

    Shared Async Function SaveOrder(values As List(Of DTOWinMenuItem), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOWinMenuItem))(values, exs, "winmenuitems/saveOrder")
    End Function


    Shared Function Tree(items As List(Of DTOWinMenuItem)) As List(Of DTOWinMenuItem)
        Dim retval As New List(Of DTOWinMenuItem)

        For Each item As DTOWinMenuItem In items
            If item.Parent Is Nothing Then
                retval.Add(item)
            Else
                Dim oParent As DTOWinMenuItem = items.Find(Function(x) x.Equals(item.Parent))
                If oParent IsNot Nothing Then
                    If oParent.CustomTarget = DTOWinMenuItem.CustomTargets.None Then
                        item.Parent = oParent
                        oParent.Children.Add(item)
                    End If
                End If
            End If
        Next

        Return retval
    End Function

#End Region

End Class
