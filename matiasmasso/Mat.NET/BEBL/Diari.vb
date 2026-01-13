Public Class Diari


    Shared Sub Load(ByRef oDiari As DTODiari)
        DiariLoader.Load(oDiari)
    End Sub

    Shared Sub LoadTopBrands(ByRef oDiari As DTODiari)
        DiariLoader.LoadTopBrands(oDiari)
    End Sub

    Shared Function Years(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As List(Of DtoDiariItem) = DiariLoader.Years(oDiari)
        Return retval
    End Function

    Shared Function Months(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As List(Of DtoDiariItem) = DiariLoader.Months(oDiari)
        Return retval
    End Function

    Shared Function Days(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As List(Of DtoDiariItem) = DiariLoader.Days(oDiari)
        Return retval
    End Function

    Shared Function Orders(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As List(Of DtoDiariItem) = DiariLoader.Orders(oDiari)
        Return retval
    End Function
End Class
