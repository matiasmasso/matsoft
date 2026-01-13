Public Class DTOMailing
    Property Users As List(Of DTOUser)
    Property Rols As List(Of DTORol)
    Property Areas As List(Of DTOArea)

    Shared Function XarxaDistribuidors(src As List(Of DTOLeadChecked), oChannels As List(Of DTODistributionChannel), oBrands As List(Of DTOProductBrand)) As List(Of DTOLeadChecked)
        Dim oLeads As New List(Of DTOLeadChecked)
        If oBrands.Count > 0 Then
            For Each item As DTOLeadChecked In src
                If oBrands.Exists(Function(x) x.Guid.Equals(item.Brand.Guid)) Then
                    If Not oLeads.Exists(Function(x) x.EmailAddress = item.EmailAddress) Then
                        If oChannels.Exists(Function(x) x.Equals(item.Contact.ContactClass.DistributionChannel)) Then
                            oLeads.Add(New DTOLeadChecked With {.Guid = item.Guid, .EmailAddress = item.EmailAddress, .Lang = item.Lang, .Checked = True})
                        End If
                    End If
                End If
            Next
        End If

        Dim retval As List(Of DTOLeadChecked) = oLeads.
               GroupBy(Function(g) New With {Key g.Guid, Key g.EmailAddress, Key g.Lang, Key g.Checked}).
        Select(Function(group) New DTOLeadChecked With {.Guid = group.Key.Guid, .EmailAddress = group.Key.EmailAddress, .Lang = group.Key.Lang, .Checked = group.Key.Checked}).ToList
        Return retval
    End Function

End Class
