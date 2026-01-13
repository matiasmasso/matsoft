Public Class Mem

    Shared Function Find(oGuid As Guid) As DTOMem
        Dim retval As DTOMem = MemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oMem As DTOMem) As Boolean
        Dim retval As Boolean = MemLoader.Load(oMem)
        Return retval
    End Function

    Shared Function Sprite(oMem As DTOMem) As Byte()
        Dim oImages = MemLoader.SpriteImages(oMem)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages)
        Return retval
    End Function


    Shared Function Update(oMem As DTOMem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MemLoader.Update(oMem, exs)
        Return retval
    End Function

    Shared Function Delete(oMem As DTOMem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MemLoader.Delete(oMem, exs)
        Return retval
    End Function

End Class

Public Class Mems

    Shared Function All(Optional oContact As DTOContact = Nothing, Optional fromRep As DTORep = Nothing, Optional oCod As DTOMem.Cods = DTOMem.Cods.NotSet, Optional oUser As DTOUser = Nothing, Optional Offset As Integer = 0, Optional MaxCount As Integer = 0, Optional OnlyFromLast24H As Boolean = False, Optional year As Integer = 0) As List(Of DTOMem)
        Dim retval As List(Of DTOMem) = MemsLoader.All(oContact, fromRep, oCod, oUser, Offset, MaxCount, OnlyFromLast24H, year)
        Return retval
    End Function

    Shared Function Count(oCod As DTOMem.Cods, oUser As DTOUser) As Integer
        Dim retval As Integer = MemsLoader.Count(oCod:=oCod, oUser:=oUser)
        Return retval
    End Function

    Shared Function Impagats(oEmp As DTOEmp) As List(Of DTOMem)
        Dim retval As List(Of DTOMem) = MemsLoader.Impagats(oEmp)
        Return retval
    End Function

End Class