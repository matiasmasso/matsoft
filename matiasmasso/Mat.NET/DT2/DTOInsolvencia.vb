Public Class DTOInsolvencia
    Inherits DTOBaseGuid

    Property Customer As DTOCustomer
    Property Fch As Date
    Property Nominal As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory() As DTOInsolvencia
        Dim retval As New DTOInsolvencia
        With retval
            .Fch = DateTime.Today
        End With
        Return retval
    End Function

End Class
