Public Class DTORepCliCom
    Inherits DTOBaseGuid

    Property Rep As DTORep
    Property Customer As DTOCustomer
    Property ComCod As ComCods
    Property Fch As Date
    Property Obs As String
    Property UsrCreated As DTOUser
    Property FchCreated As DateTime

    Public Enum ComCods
        Standard
        Reduced
        Excluded
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function RepCom(oRepCliCom As DTORepCliCom, oRepProduct As DTORepProduct) As DTORepCom
        Dim retval As DTORepCom = Nothing
        Select Case oRepCliCom.ComCod
            Case DTORepCliCom.ComCods.Standard
                retval = New DTORepCom
                retval.Rep = oRepProduct.Rep
                retval.Com = oRepProduct.ComStd
            Case DTORepCliCom.ComCods.Reduced
                retval = New DTORepCom
                retval.Rep = oRepProduct.Rep
                retval.Com = oRepProduct.ComRed
        End Select
        Return retval
    End Function

    Shared Function Com(oRepCliCom As DTORepCliCom, oRepProduct As DTORepProduct) As Decimal
        Dim retval As Decimal = 0
        Select Case oRepCliCom.ComCod
            Case DTORepCliCom.ComCods.Standard
                retval = oRepProduct.ComStd
            Case DTORepCliCom.ComCods.Reduced
                retval = oRepProduct.ComRed
        End Select
        Return retval
    End Function
End Class
