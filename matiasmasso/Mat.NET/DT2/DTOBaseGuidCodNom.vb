Public Class DTOBaseGuidCodNom
    Inherits DTOBaseGuid

    Property cod As Cods
    Property nom As String

    Public Enum Cods
        NotSet
        Vehicle
        ProductBrand
        ProductCategory
        ProductSku
        LiniaTelefon
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oGuid As Guid, Optional oCod As Cods = Cods.NotSet, Optional sNom As String = "") As DTOBaseGuidCodNom
        Dim retval = New DTOBaseGuidCodNom(oGuid)
        With retval
            .Cod = oCod
            .Nom = sNom
        End With
        Return retval
    End Function
End Class
