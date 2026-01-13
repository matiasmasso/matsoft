Public Class EmptyStringDataAnnotationsModelMetadataProvider
    Inherits System.Web.Mvc.DataAnnotationsModelMetadataProvider

    Protected Overrides Function CreateMetadata(ByVal attributes As IEnumerable(Of Attribute), ByVal containerType As Type, ByVal modelAccessor As Func(Of Object), ByVal modelType As Type, ByVal propertyName As String) As ModelMetadata
        Dim modelMetadata = MyBase.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName)
        modelMetadata.ConvertEmptyStringToNull = False
        Return modelMetadata
    End Function
End Class
