Public Class DTOStaffHoliday
    Inherits DTOBaseGuid
    Property Emp As DTOEmp
    Property Staff As DTOStaff
    Property FchFrom As DateTime
    Property FchTo As DateTime
    Property Cod As Cods
    Property Obs As String


    Public Enum Cods
        Treball
        Festiu
        Pont
        Personal
        Recuperable
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEmp As DTOEmp, oStaff As DTOStaff)
        Dim retval As New DTOStaffHoliday
        With retval
            .Emp = oEmp
            .Staff = oStaff
            .FchFrom = DateTime.Today.Date
            .FchTo = DateTime.Today.Date
            .Cod = Cods.Festiu
        End With
        Return retval
    End Function

    Public Function TitularNom() As String
        If _Staff Is Nothing Then
            Return _Emp.Nom
        Else
            Return _Staff.Abr
        End If
    End Function

    Public Function HasSpecificHourFrom() As Boolean
        Return (_FchFrom.Hour <> 0 Or _FchFrom.Minute <> 0)
    End Function
    Public Function HasSpecificHourTo() As Boolean
        Return (_FchTo.Hour <> 23 Or _FchTo.Minute <> 59)
    End Function

    Public Function SeveralDays() As Boolean
        Return _FchFrom.DayOfYear <> _FchTo.DayOfYear
    End Function
End Class
