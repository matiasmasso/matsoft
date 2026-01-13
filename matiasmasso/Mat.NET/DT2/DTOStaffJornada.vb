Public Class DTOStaffJornada
    Inherits DTOBaseGuid
    Property Staff As DTOStaff
    Property FchFrom As DateTime
    Property FchTo As DateTime
    Property Cod As DTOStaffHoliday.Cods
    Property Obs As String

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oStaff As DTOStaff) As DTOStaffJornada
        Dim retval As New DTOStaffJornada
        With retval
            .Staff = oStaff
            .Cod = DTOStaffHoliday.Cods.Treball
        End With
        Return retval
    End Function

End Class
