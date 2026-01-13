Public Class DTOProductArea
    Property Area As DTOArea
    Property Product As DTOProduct


    Shared Function Factory(oProduct As DTOProduct, oArea As DTOArea) As DTOProductArea
        Dim retval As New DTOProductArea
        With retval
            .Product = oProduct
            .Area = oArea
        End With
        Return retval
    End Function
End Class
