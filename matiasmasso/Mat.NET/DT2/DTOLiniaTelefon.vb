Public Class DTOLiniaTelefon
    Inherits DTOBaseGuid

    Property Num As String
    Property [Alias] As String
    Property Contact As DTOContact
    Property Icc As String
    Property Imei As String
    Property Puk As String
    Property Alta As Date
    Property Baixa As Date
    Property Privat As Boolean


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


    Public Class Consum
        Inherits DTOBaseGuid

        Property Linia As DTOLiniaTelefon
        Property DocFile As DTODocFile
        Property YearMonth As DTOYearMonth

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oGuid As Guid)
            MyBase.New(oGuid)
        End Sub

        Shared Function Factory(oLiniaTelefon As DTOLiniaTelefon, oYearMonth As DTOYearMonth, oDocFile As DTODocFile) As DTOLiniaTelefon.Consum
            Dim retval As New DTOLiniaTelefon.Consum
            With retval
                .Linia = oLiniaTelefon
                .YearMonth = oYearMonth
                .DocFile = oDocFile
            End With
            Return retval
        End Function
    End Class
End Class


