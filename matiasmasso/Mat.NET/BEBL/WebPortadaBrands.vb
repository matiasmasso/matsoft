Imports SixLabors.ImageSharp

Public Class WebPortadaBrand
    Shared Function Find(oBrand As DTOProductBrand) As DTOWebPortadaBrand
        Dim retval As DTOWebPortadaBrand = WebPortadaBrandLoader.Find(oBrand)
        Return retval
    End Function

    Shared Function Load(ByRef oWebPortadaBrand As DTOWebPortadaBrand) As Boolean
        Dim retval As Boolean = WebPortadaBrandLoader.Load(oWebPortadaBrand)
        Return retval
    End Function

    Shared Function Image(ByRef oWebPortadaBrand As DTOWebPortadaBrand) As Image
        Return WebPortadaBrandLoader.Image(oWebPortadaBrand)
    End Function

    Shared Function Update(oWebPortadaBrand As DTOWebPortadaBrand, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebPortadaBrandLoader.Update(oWebPortadaBrand, exs)
        Return retval
    End Function

    Shared Function Delete(oWebPortadaBrand As DTOWebPortadaBrand, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebPortadaBrandLoader.Delete(oWebPortadaBrand, exs)
        Return retval
    End Function
End Class

Public Class WebPortadaBrands

    Shared Function All(Optional oChannel As DTODistributionChannel = Nothing) As List(Of DTOWebPortadaBrand)
        Return WebPortadaBrandsLoader.All(oChannel)
    End Function

    Shared Function Sprite(Optional oChannel As DTODistributionChannel = Nothing) As Image
        Dim oImages = WebPortadaBrandsLoader.Sprite(oChannel)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages, DTOWebPortadaBrand.width, DTOWebPortadaBrand.height)
        Return retval
    End Function

    Shared Function Sort(values As List(Of DTOWebPortadaBrand), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebPortadaBrandsLoader.Sort(values, exs)
        Return retval
    End Function
End Class