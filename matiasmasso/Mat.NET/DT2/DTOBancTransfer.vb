Public Class DTOBancTransfer
    Inherits DTOBaseGuid

    Property BancEmissor As DTOBanc
    Property Cca As DTOCca
    Property Beneficiari As DTOContact
    Property BeneficiariBankBranch As DTOBankBranch
    Property Account As String
    Property Amt As DTOAmt
    Property Concepte As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
