Public Class AsinHelper
    Shared Function GenerateRandomASIN() As String
        Dim s As String = ""
        Dim r As New Random()
        Dim i As Integer
        For i = 1 To 10
            Dim c As Char = ChrW(r.Next(65, 91))
            s = s & c
        Next
        Return s
    End Function
End Class
