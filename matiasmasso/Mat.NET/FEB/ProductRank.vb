Public Class ProductRank
    Inherits _FeblBase

    Shared Async Function Load(exs As List(Of Exception), oUser As DTOUser, Optional oPeriod As DTOProductRank.Periods = DTOProductRank.Periods.Year, Optional oArea As DTOArea = Nothing, Optional unit As DTOProductRank.Units = DTOProductRank.Units.Eur) As Task(Of DTOProductRank)
        Dim retval = Await Api.Fetch(Of DTOProductRank)(exs, "ProductRank", oUser.Guid.ToString, oPeriod, OpcionalGuid(oArea), unit)
        Return retval
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oUser As DTOUser, Optional period As DTOProductRank.Periods = DTOProductRank.Periods.Year, Optional zona As Guid = Nothing, Optional brand As Guid = Nothing, Optional unit As DTOProductRank.Units = DTOProductRank.Units.Eur) As Task(Of DTOProductRank)
        Dim oArea As DTOArea = Nothing
        If zona <> Nothing Then oArea = New DTOArea(zona)
        Dim retval As DTOProductRank = Await ProductRank.Load(exs, oUser, period, oArea, unit)


        If exs.Count = 0 Then
            If brand <> Nothing Then
                retval.Items = retval.Items.Where(Function(x) x.Product.Brand.Guid.Equals(brand)).ToList
            End If
            retval.Unit = unit
            retval.Lang = oUser.lang
            retval.Zonas = New List(Of DTOGuidNom)
            Select Case oUser.Rol.id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    oUser.contacts = Await User.Contacts(exs, oUser)
                    For Each ozona In oUser.contacts.GroupBy(Function(x) DTOAddress.Zona(x.address).Guid).Select(Function(y) DTOAddress.Zona(y.First.address)).ToList
                        retval.Zonas.Add(New DTOGuidNom(ozona.Guid, ozona.nom))
                    Next

                Case DTORol.Ids.rep
                    Dim oRep = Await User.GetRep(oUser, exs)
                    Dim oRepProducts = Await RepProducts.All(exs, oUser.Emp, oRep)
                    For Each oZona In oRepProducts.GroupBy(Function(x) x.Area.guid).Select(Function(y) y.First.Area).OrderBy(Function(z) DTOArea.fromObject(z).Nom)
                        retval.Zonas.Add(New DTOGuidNom(oZona.Guid, oZona.nom))
                    Next

                Case Else
                    retval.Zonas.Add(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain))
                    retval.Zonas(retval.Zonas.Count - 1).nom = oUser.lang.Tradueix("España", "Espanya", "Spain")
                    retval.Zonas.Add(DTOCountry.Wellknown(DTOCountry.Wellknowns.Portugal))
                    retval.Zonas(retval.Zonas.Count - 1).nom = "Portugal"
                    retval.Zonas.Add(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra))
                    retval.Zonas(retval.Zonas.Count - 1).nom = "Andorra"
            End Select

        End If

        Return retval
    End Function

End Class
