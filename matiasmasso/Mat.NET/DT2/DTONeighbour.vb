Public Class DTONeighbour
    Inherits DTOContact
    Property Distance As Integer 'metres
    Property Amt As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FormattedDistance(src As Decimal) As String
        Dim retval As String = ""
        Select Case src
            Case < 100
                retval = String.Format("{0:0} m", src)
            Case < 500
                retval = String.Format("{0:0} m", 10 * Math.Round(src / 10, 0, MidpointRounding.AwayFromZero))
            Case < 1000
                retval = String.Format("{0:0} m", 100 * Math.Round(src / 100, 0, MidpointRounding.AwayFromZero))
            Case < 5000
                retval = String.Format("{0:N1} Km", src / 1000)
            Case Else
                retval = String.Format("{0:N0} Km", Math.Truncate(src / 1000))
        End Select
        Return retval
    End Function
End Class
