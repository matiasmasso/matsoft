Public Class Banc
    Shared Function Find(oGuid As Guid) As DTOBanc
        Dim retval As DTOBanc = BancLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oBanc As DTOBanc) As Boolean
        Dim retval As Boolean = BancLoader.Load(oBanc)
        Return retval
    End Function

    Shared Function Update(oBanc As DTOBanc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancLoader.Update(oBanc, exs)
        Return retval
    End Function

    Shared Function Delete(oBanc As DTOBanc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancLoader.Delete(oBanc, exs)
        Return retval
    End Function

End Class
Public Class Bancs
    Shared Function All(oEmp As DTOEmp) As List(Of DTOBanc)
        Dim retval = BancsLoader.All(oEmp, IncludeObsolets:=True)
        Return retval
    End Function

    Shared Function Active(oEmp As DTOEmp) As List(Of DTOBanc)
        Dim retval = BancsLoader.All(oEmp, IncludeObsolets:=False)
        Return retval
    End Function

    Shared Function BancsToReceiveTransfer(oEmp As DTOEmp) As List(Of DTOBanc)
        Return BancsLoader.BancsToReceiveTransfer(oEmp)
    End Function

    Shared Function ActiveSprite(oEmp As DTOEmp) As Byte()
        Dim oBancs As List(Of DTOBanc) = BancsLoader.All(oEmp, IncludeObsolets:=False)
        Dim oLogos = oBancs.Select(Function(x) x.Logo).ToList
        Dim retval = LegacyHelper.SpriteHelper.Factory(oLogos, 48, 48)
        Return retval
    End Function

    Shared Function Sprite(oGuids As List(Of Guid), itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim oImages = BancsLoader.Sprite(oGuids)
        Dim retval = LegacyHelper.SpriteHelper.Factory(oImages, itemWidth, itemHeight)
        Return retval
    End Function

End Class
