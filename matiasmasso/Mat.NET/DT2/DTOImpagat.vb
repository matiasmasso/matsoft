Public Class DTOImpagat
    Inherits DTOBaseGuid

    Property csb As DTOCsb
    Property refBanc As String
    Property gastos As DTOAmt
    Property pagatACompte As DTOAmt
    Property fchAFP As Date
    Property fchSdo As Date
    'Property Insolvencia As Insolvencia
    Property status As StatusCodes
    Property asnefAlta As Date
    Property asnefBaixa As Date

    Property ccaIncobrable As DTOCca
    Property obs As String

    Property lastMemFch As Date

    Public Enum StatusCodes
        notSet
        enNegociacio
        conveni
        saldat
        insolvencia
    End Enum

    Public Enum OrderBy
        vto
        cliNomVto
    End Enum


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public ReadOnly Property Nominal As DTOAmt
        Get
            Dim retval As DTOAmt = Nothing
            If _Csb IsNot Nothing Then
                retval = _Csb.Amt
            End If
            Return retval
        End Get
    End Property

    Shared Function PendentAmbGastos(oImpagat As DTOImpagat) As DTOAmt
        Dim retval As DTOAmt = oImpagat.Nominal.Clone
        If oImpagat.PagatACompte IsNot Nothing Then
            retval = retval.Substract(oImpagat.PagatACompte)
        End If
        If oImpagat.Gastos IsNot Nothing Then
            Dim oGastos As DTOAmt = oImpagat.Gastos
            If oGastos.Eur < 40 Then oGastos = DTOAmt.Factory(40) 'minim despeses
            retval = retval.Add(oGastos)
        End If
        Return retval
    End Function

    Shared Function Pendent(oImpagat As DTOImpagat) As DTOAmt
        Dim retval As DTOAmt = oImpagat.Nominal
        If oImpagat.PagatACompte IsNot Nothing Then
            retval = retval.Substract(oImpagat.PagatACompte)
        End If
        Return retval
    End Function

    Shared Function GetGastos(oImpagat As DTOImpagat) As DTOAmt
        Dim oMinimGastos As DTOAmt = DTOAmt.Factory(40)
        Dim retval As DTOAmt = oImpagat.Gastos
        If retval Is Nothing Then
            retval = oMinimGastos
        Else
            If retval.Eur < oMinimGastos.Eur Then
                retval = oMinimGastos
            End If
        End If
        Return retval
    End Function
End Class


Public Class DTOImpagats
    Property Impagats As List(Of DTOImpagat)
    Property Cca As DTOCca
End Class