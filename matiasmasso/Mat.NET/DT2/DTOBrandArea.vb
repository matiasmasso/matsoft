Public Class DTOBrandArea
    Property Guid As Guid
    Property Brand As DTOProductBrand
    Property Area As DTOArea
    Property FchFrom As Date
    Property FchTo As Date

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Shared Function Factory(oBrand As DTOProductBrand) As DTOBrandArea
        Dim retval As DTOBrandArea = Nothing
        If oBrand IsNot Nothing Then
            retval = New DTOBrandArea
            With retval
                .Guid = System.Guid.NewGuid
                .IsNew = True
                .Brand = oBrand
                .FchFrom = DateTime.Today
            End With
        End If
        Return retval
    End Function
End Class
