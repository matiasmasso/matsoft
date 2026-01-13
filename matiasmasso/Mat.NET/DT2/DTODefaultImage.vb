Public Class DTODefaultImage
    Property Id As Defaults.ImgTypes
    Property Image As Image

    Shared Function Factory(oId As Defaults.ImgTypes, Optional oImage As Image = Nothing)
        Dim retval As New DTODefaultImage
        With retval
            .Id = oId
            .Image = oImage
        End With
        Return retval
    End Function
End Class
