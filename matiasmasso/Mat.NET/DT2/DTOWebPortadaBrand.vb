Public Class DTOWebPortadaBrand
    Inherits DTOBaseGuid

    Public Const width As Integer = 150
    Public Const height As Integer = 150

    Property Brand As DTOProductBrand
    Property Hide As Boolean
    Property Ord As Integer
    <JsonIgnore> Property Image As Image

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oBrand As DTOProductBrand)
        MyBase.New(oBrand.Guid)
        _Brand = oBrand
    End Sub
End Class
