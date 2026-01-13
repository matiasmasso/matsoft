
Public Class Xl_BaseQuota
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    <ComponentModel.DisplayName("Edit Quota Allowed"), ComponentModel.Browsable(True), ComponentModel.Description("If false, quota will be calculated by base*tipus/100")>
    Public Property EditQuotaAllowed As Boolean
        Get
            Return Not Xl_AmountQuota.ReadOnly
        End Get
        Set(value As Boolean)
            Xl_AmountQuota.ReadOnly = Not value
        End Set
    End Property

    Public ReadOnly Property Value As DTOBaseQuota
        Get
            Dim retval As New DTOBaseQuota(Xl_AmountBase.Amt, Xl_Percent1.Value, Xl_AmountQuota.Amt)
            Return retval
        End Get
    End Property

    Public ReadOnly Property Base As DTOAmt
        Get
            Return Xl_AmountBase.Amt
        End Get
    End Property
    Public ReadOnly Property Tipus As Decimal
        Get
            Return Xl_Percent1.Value
        End Get
    End Property
    Public ReadOnly Property Quota As DTOAmt
        Get
            Return Xl_AmountQuota.Amt
        End Get
    End Property
    Public ReadOnly Property IsEmptyBase As Boolean
        Get
            Dim retval As Boolean = Xl_AmountBase.Amt.IsZero
            Return retval
        End Get
    End Property
    Public ReadOnly Property IsEmptyQuota As Boolean
        Get
            Dim retval As Boolean
            If Xl_AmountQuota.Amt Is Nothing Then
                retval = True
            ElseIf Xl_AmountQuota.Amt.IsZero() Then
                retval = True
            End If
            Return retval
        End Get
    End Property

    Public Shadows Sub Load(value As DTOBaseQuota)
        If value IsNot Nothing Then
            Xl_AmountBase.Amt = value.baseImponible
            Xl_Percent1.Value = value.Tipus
            If value.Quota Is Nothing Then
                calculaQuota()
            Else
                Xl_AmountQuota.Amt = value.Quota
            End If
        End If
        _AllowEvents = True
    End Sub


    Private Sub Xl_Percent1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Percent1.AfterUpdate
        calculaQuota()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
    End Sub

    Private Sub Xl_AmountBase_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmountBase.AfterUpdate
        calculaQuota()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
    End Sub

    Private Sub Xl_AmountQuota_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmountQuota.AfterUpdate
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
    End Sub

    Private Sub calculaQuota()
        Dim oBase As DTOAmt = Xl_AmountBase.Amt
        Dim DcTipus As Decimal = Xl_Percent1.Value
        Dim oQuota As DTOAmt = oBase.Percent(DcTipus)
        Xl_AmountQuota.Amt = oQuota
    End Sub


End Class
