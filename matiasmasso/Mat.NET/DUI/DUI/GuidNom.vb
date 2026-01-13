Public Class Guidnom
    Property Guid As Guid
    Property Nom As String
End Class
Public Class Guidnom2
    'per iMat, JSONDecoder de XCode ha de rebre les propietats en minuscules
    Property guid As Guid
    Property nom As String
End Class

Public Class Guidnom3
    'per iMat antic, JSONDecoder de XCode ha de rebre les propietats en majuscules
    Property Guid As Guid
    Property Nom As String
End Class

