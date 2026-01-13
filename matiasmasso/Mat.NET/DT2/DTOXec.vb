Public Class DTOXec
    Inherits DTOBaseGuid

    Property Emp As DTOEmp
    Public Property Id As Long
    Public Property Lliurador As DTOContact
    Public Property Iban As DTOIban
    Public Property XecNum As String
    Public Property Vto As Date = Date.MinValue
    Public Property Amt As DTOAmt
    Public Property Pnds As List(Of DTOPnd)
    Public Property Impagats As List(Of DTOImpagat)
    Public Property StatusCod As StatusCods
    Public Property FchRecepcio As Date
    Public Property CcaRebut As DTOCca
    Public Property CodPresentacio As ModalitatsPresentacio
    Public Property CcaPresentacio As DTOCca
    Public Property NBanc As DTOBanc
    Public Property CcaVto As DTOCca
    Public Property Format As Formats

    Public Enum StatusCods
        EnCartera
        EnCirculacio
        Vençut
    End Enum

    Public Enum Formats
        Xec
        Pagare
    End Enum

    Public Enum ModalitatsPresentacio
        NotSet
        A_la_Vista
        Al_Cobro
        Al_Descompte
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEmp As DTOEmp)
        Dim retval As New DTOXec
        With retval
            .Emp = oEmp
        End With
        Return retval
    End Function
End Class
