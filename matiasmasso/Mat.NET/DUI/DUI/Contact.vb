Public Class Contact
    Inherits Guidnom

    Property Nif As String
    Property Address As String
    Property Location As String

    Property Latitude As Double
    Property Longitude As Double

    Property Tels As List(Of Tel)
    Property Emails As List(Of Email)
End Class

Public Class Tel
    Property Num As String
    Property Obs As String
End Class

Public Class Email
    Property Address As String
    Property Obs As String
End Class