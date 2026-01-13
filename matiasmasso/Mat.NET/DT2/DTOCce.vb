Public Class DTOCce
    Property Exercici As DTOExercici
    Property Cta As DTOPgcCta
    Property Amt As DTOAmt

    Public Sub New(oExercici As DTOExercici, oCta As DTOPgcCta)
        MyBase.New
        _Exercici = oExercici
        _Cta = oCta
    End Sub

    Public Sub New(oCta As DTOPgcCta, oAmt As DTOAmt)
        MyBase.New
        _Cta = oCta
        _Amt = oAmt
    End Sub
End Class
