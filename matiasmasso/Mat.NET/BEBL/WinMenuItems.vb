Public Class WinMenuItem

    Shared Function Find(oGuid As Guid) As DTOWinMenuItem
        Dim retval As DTOWinMenuItem = WinMenuItemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Icon(guid As Guid) As ImageMime
        Return WinMenuItemLoader.Icon(guid)
    End Function

    Shared Function Update(oWinMenuItem As DTOWinMenuItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinMenuItemLoader.Update(oWinMenuItem, exs)
        Return retval
    End Function

    Shared Function Delete(oWinMenuItem As DTOWinMenuItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinMenuItemLoader.Delete(oWinMenuItem, exs)
        Return retval
    End Function

End Class



Public Class WinMenuItems
    Shared Function All(oUser As DTOUser) As List(Of DTOWinMenuItem)
        Dim retval = WinMenuItemsLoader.All(oUser)
        Return retval
    End Function

    Shared Function Sprite(oGuids As List(Of Guid)) As Byte()
        Dim oImages = WinMenuItemsLoader.SpriteImages(oGuids)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages)
        Return retval
    End Function

    Shared Function Sprite(oUser As DTOUser) As Byte()
        Dim oImages = WinMenuItemsLoader.SpriteImages(oUser)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages)
        Return retval
    End Function

    Shared Function SaveOrder(items As List(Of DTOWinMenuItem), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinMenuItemsLoader.SaveOrder(items, exs)
        Return retval
    End Function
End Class
