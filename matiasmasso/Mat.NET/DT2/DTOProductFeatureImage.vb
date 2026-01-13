Public Class DTOProductFeatureImage
    Property Guid As System.Guid
    Property Ord As Integer
    <JsonIgnore> Property Image As Image

    Property IsLoaded As Boolean
    Property IsNew As Boolean
End Class
