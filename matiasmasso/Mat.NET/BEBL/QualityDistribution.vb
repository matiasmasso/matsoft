Public Class QualityDistribution

    Shared Function All(oProveidor As DTOProveidor, fchFrom As Date)
        Dim retval = QualityDistributionLoader.All(oProveidor, fchFrom)
        Return retval
    End Function

End Class
