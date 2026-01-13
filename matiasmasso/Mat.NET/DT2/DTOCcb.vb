Public Class DTOCcb
    Inherits DTOBaseGuid
    Property cca As DTOCca
    Property contact As DTOContact
    Property cta As DTOPgcCta
    Property amt As DTOAmt
    Property dh As DhEnum
    Property lin As Integer
    Property pnd As DTOPnd

    Public Enum DhEnum
        notSet
        debe
        haber
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oCca As DTOCca, oAmt As DTOAmt, oCta As DTOPgcCta, oContact As DTOContact, oDh As DTOCcb.DhEnum, Optional oPnd As DTOPnd = Nothing) As DTOCcb
        Dim retval As New DTOCcb
        With retval
            .Cca = oCca
            .Amt = oAmt
            .Cta = oCta
            .Contact = oContact
            .Dh = oDh
            .Pnd = oPnd
        End With
        Return retval
    End Function

    Shared Function Debit(oCcb As DTOCcb) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oCcb.Dh = DTOCcb.DhEnum.Debe Then
            retval = oCcb.Amt
        End If
        Return retval
    End Function

    Shared Function Credit(oCcb As DTOCcb) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oCcb.Dh = DTOCcb.DhEnum.Haber Then
            retval = oCcb.Amt
        End If
        Return retval
    End Function

    Shared Sub Arrastra(oCcb As DTOCcb, ByRef oDeure As DTOAmt, ByRef oHaver As DTOAmt, ByRef oSaldo As DTOAmt)
        If oCcb.Dh = DTOCcb.DhEnum.Debe Then
            oDeure.Add(oCcb.Amt)
        Else
            oHaver.Add(oCcb.Amt)
        End If

        If oCcb.Cta.Act = oCcb.Dh Then
            oSaldo.Add(oCcb.Amt)
        Else
            oSaldo.Substract(oCcb.Amt)
        End If
    End Sub


End Class
