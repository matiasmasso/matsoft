Public Class RepCertRetencio

End Class

Public Class RepCertsRetencio
    Shared Function All(oRep As DTORep, Optional iYear As Integer = 0, Optional iQuarter As Integer = 0) As List(Of DTORepCertRetencio)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Dim oRepLiqs As List(Of DTORepLiq) = BEBL.RepLiqs.Headers(oEmp,, oRep)
        Dim retval As List(Of DTORepCertRetencio) = oRepLiqs.
            GroupBy(Function(g) TimeHelper.FormatedQuarter(g.Fch)).
                Select(Function(group) New DTORepCertRetencio With {
                    .Rep = group.First.Rep,
                    .Fch = group.Max(Function(repliq) repliq.Fch),
                    .Url = DTORepCertRetencio.GetUrl(.Rep, .Fch),
                    .RepLiqs = group.ToList
                        }).OrderByDescending(Function(x) x.Fch).ToList()

        If iYear <> 0 Then
            retval = retval.Where(Function(x) x.Fch.Year = iYear).ToList
        End If

        If iQuarter <> 0 Then
            retval = retval.Where(Function(x) DatePart(DateInterval.Quarter, x.Fch) = iQuarter).ToList
        End If

        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTORepCertRetencio)
        Dim oRepLiqs = BEBL.RepLiqs.Headers(oUser)
        Dim retval = oRepLiqs.GroupBy(Function(g) TimeHelper.FormatedQuarter(g.Fch)).
                Select(Function(group) New DTORepCertRetencio With {
                    .Rep = group.First.Rep,
                    .Fch = group.Max(Function(repliq) repliq.Fch),
                    .Url = DTORepCertRetencio.GetUrl(.Rep, .Fch),
                    .RepLiqs = group.ToList
                        }).OrderByDescending(Function(x) x.Fch).ToList()

        Return retval
    End Function

End Class
