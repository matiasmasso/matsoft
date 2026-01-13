Public Class DTOIVALiquidacio
    Property Exercici As DTOExercici
    Property Month As Integer
    Property Cca As DTOCca
    Property Items As List(Of Item)

    Shared Function Factory(oExercici As DTOExercici, iMonth As Integer) As DTOIVALiquidacio
        Dim retval As New DTOIVALiquidacio
        With retval
            .Exercici = oExercici
            .Month = iMonth
            .Items = New List(Of Item)
        End With
        Return retval
    End Function

    Public Function YearMonth() As DTOYearMonth
        Return New DTOYearMonth(_Exercici.Year, _Month)
    End Function

    Public Function Fch() As Date
        Return YearMonth.LastFch
    End Function

    Public Function CcaFactory(oUser As DTOUser, oAllCtas As List(Of DTOPgcCta)) As DTOCca
        Dim retval = DTOCca.Factory(Me.Fch, oUser, DTOCca.CcdEnum.IVA, Me.YearMonth.RawTag)
        retval.Concept = String.Format("Hisenda mod.303 declaració IVA {0} {1}", DTOLang.CAT.Mes(_Month), _Exercici.Year)

        Dim oItemRep = _Items.FirstOrDefault(Function(x) x.Cod = Item.Cods.Repercutit)
        Dim oItemReq = _Items.FirstOrDefault(Function(x) x.Cod = Item.Cods.RecarrecEquivalencia)
        Dim oItemCom = _Items.FirstOrDefault(Function(x) x.Cod = Item.Cods.IntraComunitari)
        Dim oItemSop = _Items.FirstOrDefault(Function(x) x.Cod = Item.Cods.SoportatNacional)

        retval.AddDebit(oItemRep.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRepercutitNacional))
        retval.AddDebit(oItemReq.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRecarrecEquivalencia))
        retval.AddDebit(oItemCom.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari))
        retval.AddCredit(oItemSop.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaSoportatNacional))
        retval.AddCredit(oItemCom.Quota, DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaSoportatIntracomunitari))
        retval.AddSaldo(DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.IvaDeutor))
        Return retval
    End Function

    Public Class Item
        Property Cod As Cods
        Property Base As DTOAmt
        Property Quota As DTOAmt
        Property Tipus As Decimal
        Property Saldo As DTOAmt

        Public Enum Cods
            Repercutit
            RecarrecEquivalencia
            SoportatNacional
            IntraComunitari
            Importacions
        End Enum

        Shared Function Factory(oCod As Cods, oBase As DTOAmt, Optional DcTipus As Decimal = 0, Optional oQuota As DTOAmt = Nothing, Optional oSaldo As DTOAmt = Nothing)
            Dim retval As New Item
            With retval
                .Cod = oCod
                .Base = oBase
                .Tipus = DcTipus
                .Quota = oQuota
                .Saldo = oSaldo
            End With
            Return retval
        End Function


    End Class

End Class
