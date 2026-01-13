Public Class StatQuery
    Property user As Guid
    Property reportMode As Integer
    Property keyCod As Integer
    Property valueCod As Integer
    Property filterYear As Integer
    Property filterMonth As Integer
    Property filterDay As Integer
    Property filterClient As Guid
    Property filterRep As Guid
    Property filterManufacturer As Guid
    Property filterProduct As Guid

    Property items As List(Of StatQueryItem)

    Public Sub New()
        MyBase.New
    End Sub
End Class

Public Class StatQueryItem
    Property keyCod As String
    Property keyNom As String
    Property value As Decimal
End Class
