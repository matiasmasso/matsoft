Public Class SegSocialGrup

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOSegSocialGrup
        Dim retval As DTOSegSocialGrup = SegSocialGrupLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oSegSocialGrup As DTOSegSocialGrup) As Boolean
        Dim retval As Boolean = SegSocialGrupLoader.Load(oSegSocialGrup)
        Return retval
    End Function

    Shared Function Update(oSegSocialGrup As DTOSegSocialGrup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SegSocialGrupLoader.Update(oSegSocialGrup, exs)
        Return retval
    End Function

    Shared Function Delete(oSegSocialGrup As DTOSegSocialGrup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SegSocialGrupLoader.Delete(oSegSocialGrup, exs)
        Return retval
    End Function
#End Region

End Class

Public Class SegSocialGrups

    Shared Function All() As List(Of DTOSegSocialGrup)
        Dim retval As List(Of DTOSegSocialGrup) = SegSocialGrupsLoader.All()
        Return retval
    End Function


End Class