Public Class Txt
    Shared Function Find(Id As DTOTxt.Ids) As DTOTxt
        Dim retval As DTOTxt = TxtLoader.Find(Id)
        Return retval
    End Function
    Shared Function Load(ByRef oTxt As DTOTxt) As Boolean
        Dim retval As Boolean = TxtLoader.Load(oTxt)
        Return retval
    End Function

    Shared Function Update(oTxt As DTOTxt, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TxtLoader.Update(oTxt, exs)
        Return retval
    End Function

    Shared Function Delete(oTxt As DTOTxt, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TxtLoader.Delete(oTxt, exs)
        Return retval
    End Function
End Class

Public Class Txts

End Class
