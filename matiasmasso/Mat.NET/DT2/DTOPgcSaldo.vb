Public Class DTOPgcSaldo
    Property Exercici As DTOExercici
    Property Epg As DTOPgcEpgBase
    Property Contact As DTOContact
    Property Debe As DTOAmt
    Property Haber As DTOAmt
    Property Pendent As DTOAmt
    'Property SdoDeudor As DTOAmt
    'Property SdoCreditor As DTOAmt

    Public Enum Signes
        Deutor
        Creditor
    End Enum

    Shared Function Factory(oExercici As DTOExercici, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing) As DTOPgcSaldo
        Dim retval As New DTOPgcSaldo
        With retval
            .Exercici = oExercici
            .Epg = oCta
            .Contact = oContact
        End With
        Return retval
    End Function

    Public Function IsDeutor() As Boolean
        Dim retval As Boolean
        If _Debe IsNot Nothing Then
            If _Debe.IsGreaterThan(_Haber) Then retval = True
        End If
        Return retval
    End Function

    Public Function IsCreditor() As Boolean
        Dim retval As Boolean
        If _Haber IsNot Nothing Then
            If _Haber.IsGreaterThan(_Debe) Then retval = True
        End If
        Return retval
    End Function

    Public Function IsNotZero() As Boolean
        Dim retval As Boolean = IsDeutor() Or IsCreditor()
        Return retval
    End Function

    Public Function Signe() As Signes
        Dim oZero = DTOAmt.Factory()
        Dim oDebe As DTOAmt = If(_Debe, oZero)
        Dim oHaber As DTOAmt = If(_Haber, oZero)
        Dim retval As Signes = If(oDebe.isGreaterOrEqualThan(oHaber), Signes.Deutor, Signes.Creditor)
        Return retval
    End Function

    Public Function SdoDeudor() As DTOAmt
        Dim oZero = DTOAmt.Factory()
        Dim oDebe As DTOAmt = If(_Debe, oZero)
        Dim oHaber As DTOAmt = If(_Haber, oZero)
        Dim retval As DTOAmt = oDebe.Clone.Substract(oHaber)
        Return retval
    End Function

    Public Function SdoCreditor() As DTOAmt
        Dim oZero = DTOAmt.Factory()
        Dim oDebe As DTOAmt = If(_Debe, oZero)
        Dim oHaber As DTOAmt = If(_Haber, oZero)
        Dim retval As DTOAmt = oHaber.Clone.Substract(oDebe)
        Return retval
    End Function

    Shared Sub UpdateSaldo(ByRef oSaldo As DTOAmt, oCcb As DTOCcb)
        Dim oCta As DTOPgcCta = oCcb.Cta
        If oCta.Act = oCcb.Dh Then
            oSaldo.Add(oCcb.Amt)
        Else
            oSaldo.Substract(oCcb.Amt)
        End If
    End Sub

    Shared Sub AddSaldo(oBase As DTOPgcSaldo, oSaldoToAdd As DTOPgcSaldo)
        If oSaldoToAdd.Debe IsNot Nothing Then
            If oBase.Debe Is Nothing Then
                oBase.Debe = oSaldoToAdd.Debe.Clone
            Else
                oBase.Debe.Add(oSaldoToAdd.Debe)
            End If
        End If
        If oSaldoToAdd.Haber IsNot Nothing Then
            If oBase.Haber Is Nothing Then
                oBase.Haber = oSaldoToAdd.Haber.Clone
            Else
                oBase.Haber.Add(oSaldoToAdd.Haber)
            End If
        End If

    End Sub

    Shared Function Saldo(oSaldo As DTOPgcSaldo) As DTOAmt
        Dim retval As DTOAmt = Nothing

        Dim oCta As DTOPgcCta = oSaldo.Epg
        If oCta.Act = DTOPgcCta.Bal(oCta) Then
            If oCta.Act = DTOPgcCta.Acts.Deutora Then
                If oSaldo.SdoDeudor Is Nothing Then
                    If oSaldo.SdoCreditor IsNot Nothing Then
                        retval = oSaldo.SdoCreditor.Inverse
                    End If
                Else
                    retval = oSaldo.SdoDeudor.Substract(oSaldo.SdoCreditor)
                End If
            Else
                If oSaldo.SdoCreditor Is Nothing Then
                    If oSaldo.SdoDeudor IsNot Nothing Then
                        retval = oSaldo.SdoDeudor.Inverse
                    End If
                Else
                    retval = oSaldo.SdoCreditor.Substract(oSaldo.SdoDeudor)
                End If
            End If
        Else
            If oCta.Act = DTOPgcCta.Acts.Deutora Then
                If oSaldo.SdoDeudor Is Nothing Then
                    If oSaldo.SdoCreditor IsNot Nothing Then
                        retval = oSaldo.SdoCreditor.Inverse
                    End If
                Else
                    retval = oSaldo.SdoDeudor.Substract(oSaldo.SdoCreditor)
                End If
            Else
                If oSaldo.SdoCreditor Is Nothing Then
                    If oSaldo.SdoDeudor IsNot Nothing Then
                        retval = oSaldo.SdoDeudor.Inverse
                    End If
                Else
                    retval = oSaldo.SdoCreditor.Substract(oSaldo.SdoDeudor)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function ResumTotsDigits(oSaldos As List(Of DTOPgcSaldo)) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)
        Dim oLastCta As New DTOPgcEpgBase
        Dim oItem As DTOPgcSaldo = Nothing
        For Each oSaldo As DTOPgcSaldo In oSaldos
            If Not oSaldo.Epg.Equals(oLastCta) Then
                oLastCta = oSaldo.Epg
                oItem = New DTOPgcSaldo
                With oItem
                    .Exercici = oSaldo.Exercici
                    .Epg = oLastCta
                End With
                retval.Add(oItem)
            End If


            DTOPgcSaldo.AddSaldo(oItem, oSaldo)
        Next
        Return retval
    End Function
End Class
