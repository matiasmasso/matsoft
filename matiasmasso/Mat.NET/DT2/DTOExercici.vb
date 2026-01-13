Public Class DTOExercici
    Property Guid As Guid
    Property emp As DTOEmp
    Property year As Integer

    Public Sub New(oEmp As DTOEmp, iYear As Integer)
        MyBase.New
        _Emp = oEmp
        _Year = iYear
    End Sub


    Public Function Trimmed() As DTOExercici
        Dim retval As New DTOExercici(_Emp.Trimmed, _Year)
        Return retval
    End Function

    Public Shadows Function Equals(oExercici As Object) As Boolean
        Dim retval As Boolean
        If oExercici IsNot Nothing Then
            If TypeOf oExercici Is DTOExercici Then
                If oExercici.Emp.Equals(_Emp) Then
                    retval = oExercici.Year = _Year
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Current(oEmp As DTOEmp) As DTOExercici
        Dim retval As New DTOExercici(oEmp, Today.Year)
        Return retval
    End Function

    Shared Function Past(oEmp As DTOEmp) As DTOExercici
        Dim retval As DTOExercici = FromYear(oEmp, Today.Year - 1)
        Return retval
    End Function

    Shared Function FromYear(oEmp As DTOEmp, iYear As Integer) As DTOExercici
        Dim retval As New DTOExercici(oEmp, iYear)
        Return retval
    End Function

    Public Function Previous() As DTOExercici
        Dim retval As New DTOExercici(_Emp, _Year - 1)
        Return retval
    End Function

    Public Function [Next]() As DTOExercici
        Dim retval As New DTOExercici(_Emp, _Year + 1)
        Return retval
    End Function

    Public Function FirstFch() As Date
        Dim retval As New Date(_Year, 1, 1)
        Return retval
    End Function

    Public Function LastFch() As Date
        Dim retval As New Date(_Year, 12, 31)
        Return retval
    End Function

    Public Function LastFch1stQuarterNextYear() As Date
        Dim dtfch As New Date(_Year + 1, 3, 31)
        Return dtfch
    End Function

    Public Function LastDayOrToday() As Date
        Dim retval As Date = IIf(_Year = Today.Year, Today, New Date(_Year, 12, 31))
        Return retval
    End Function
End Class
