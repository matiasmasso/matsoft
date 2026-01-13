Public Class DTOCliProductBlocked
    Property Contact As DTOContact = Nothing
    Property Product As DTOProduct
    Property DistModel As DistModels
    Property Cod As Codis
    Property Zip As String
    Property Obs As String

    Property LastFch As Date
    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Enum DistModels
        NotSet
        Open 'lliure
        Closed 'llista tancada de distribuidors oficials
    End Enum

    Public Enum Codis
        Standard
        Exclusiva
        NoAplicable
        Exclos
        DistribuidorOficial
        AltresEnExclusiva
    End Enum

    Shared Function Factory(oContact As DTOContact, Optional oProduct As DTOProduct = Nothing) As DTOCliProductBlocked
        Dim retval As New DTOCliProductBlocked
        With retval
            .Contact = oContact
            .Product = oProduct
        End With
        Return retval
    End Function
End Class
