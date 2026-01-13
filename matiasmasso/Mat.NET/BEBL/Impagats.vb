Public Class Impagat


    Shared Function Find(oGuid As Guid) As DTOImpagat
        Dim retval As DTOImpagat = ImpagatLoader.Find(oGuid)
        Return retval
    End Function


    Shared Function Load(ByRef oImpagat As DTOImpagat) As Boolean
        Dim retval As Boolean = ImpagatLoader.Load(oImpagat)
        Return retval
    End Function

    Shared Function Update(oImpagat As DTOImpagat, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImpagatLoader.Update(oImpagat, exs)
        Return retval
    End Function

    Shared Function Delete(oImpagat As DTOImpagat, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImpagatLoader.Delete(oImpagat, exs)
        Return retval
    End Function

End Class



Public Class Impagats

    Shared Function All(oUser As DTOUser) As List(Of DTOImpagat)
        Dim retval As New List(Of DTOImpagat)
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Accounts
                retval = ImpagatsLoader.All(oUser.Emp, DTOImpagat.OrderBy.Vto)
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                Dim oRep As DTORep = BEBL.User.GetRep(oUser)
                retval = All(oRep)
        End Select
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oContact As DTOContact) As List(Of DTOImpagat)
        Dim retval As List(Of DTOImpagat) = ImpagatsLoader.All(oEmp, DTOImpagat.OrderBy.Vto, oContact)
        Return retval
    End Function

    Shared Function All(oRep As DTORep) As List(Of DTOImpagat)
        Dim retval As List(Of DTOImpagat) = ImpagatsLoader.All(oRep)
        Return retval
    End Function

    Shared Function Update(exs As List(Of Exception), oImpagats As List(Of DTOImpagat), ByRef oCca As DTOCca) As DTOCca
        Dim retval As DTOCca = Nothing
        If ImpagatsLoader.Update(oImpagats, oCca, exs) Then
            retval = oCca
        End If
        Return retval
    End Function

    Shared Function Kpis(oEmp As DTOEmp, fromYear As Integer) As List(Of DTOKpi)
        Return ImpagatsLoader.Kpis(oEmp, fromYear)
    End Function

End Class
