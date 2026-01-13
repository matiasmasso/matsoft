
Public Class User
    Property Guid As Guid
    Property Email As String
    Property Password As String
    Property Firstnom As String
    Property Cognoms As String
    Property Nickname As String
    Property Sex As Integer
    Property BirthYear As Integer
    Property Country As DUI.Country
    Property Zip As String
    Property Tel As String
    Property ChildrenCount As Integer
    Property LastChildBirth As String
    Property Rol As Integer
    Property Lang As Integer
    Property ContactGuid As String
    Property ValidationResult As Integer
End Class
Public Class User2
    Property guid As Guid
    Property email As String = ""
    Property password As String = ""
    Property firstnom As String = ""
    Property cognoms As String = ""
    Property nickname As String = ""
    Property sex As Integer = 0
    Property birthYear As Integer = 0
    Property country As DUI.Country = Nothing
    Property zip As String = ""
    Property tel As String = ""
    Property childrenCount As Integer = 0
    Property lastChildBirth As String
    Property rol As Integer = 0
    Property lang As Integer = 0
    Property contactGuid As String = ""
    Property validationResult As Integer = 0

End Class
