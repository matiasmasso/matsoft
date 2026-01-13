Public Class Country
    Property Guid As Guid
    Property Nom As String
    Property Zonas As List(Of Zona)

End Class

Public Class Zona
    Property Guid As Guid
    Property Nom As String
    Property Locations As List(Of Location)
End Class

Public Class Location
    Property Guid As Guid
    Property Nom As String
    Property Contacts As List(Of Guidnom)
End Class
