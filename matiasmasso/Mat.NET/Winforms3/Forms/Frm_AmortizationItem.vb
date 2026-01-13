Public Class Frm_AmortizationItem
    Private _AmortizationItem As DTOAmortizationItem
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAmortizationItem)
        MyBase.New()
        Me.InitializeComponent()
        _AmortizationItem = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.AmortizationItem.Load(_AmortizationItem, exs) Then
            With _AmortizationItem
                TextBoxImmobilitzat.Text = .Parent.Dsc
                DateTimePicker1.Value = .Fch
                Xl_PercentTipus.Value = .Tipus
                Xl_AmountCur1.Amt = .Amt
                CheckBoxBaixa.Checked = (.Cod = DTOAmortizationItem.Cods.Baixa)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxImmobilitzat.TextChanged,
         DateTimePicker1.ValueChanged,
            CheckBoxBaixa.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _AmortizationItem
            .Fch = DateTimePicker1.Value
            .Amt = Xl_AmountCur1.Amt
            .Tipus = Xl_PercentTipus.Value
            .Cod = IIf(CheckBoxBaixa.Checked, DTOAmortizationItem.Cods.Baixa, DTOAmortizationItem.Cods.Amortitzacio)
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.AmortizationItem.Update(Current.Session.User, _AmortizationItem, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AmortizationItem))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            'If BLL.BLLAmortizationItem.Delete(_AmortizationItem, exs) Then
            'RaiseEvent AfterUpdate(Me, New MatEventArgs(_AmortizationItem))
            'Me.Close()
            'Else
            'UIHelper.WarnError(exs, "error al eliminar")
            'End If
        End If
    End Sub

    Private Sub Xl_AmountCur1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmountCur1.AfterUpdate
        If _AllowEvents Then
            _AllowEvents = False
            Dim DcValorAdquisicio As Decimal = _AmortizationItem.Parent.Amt.Eur
            Dim DcEur As Decimal = Xl_AmountCur1.Amt.Eur
            Xl_PercentTipus.Value = DcEur * 100 / DcValorAdquisicio
            ButtonOk.Enabled = True
            _AllowEvents = True
        End If
    End Sub

    Private Sub Xl_PercentTipus_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentTipus.AfterUpdate
        If _AllowEvents Then
            _AllowEvents = False
            Dim DcValorAdquisicio As Decimal = _AmortizationItem.Parent.Amt.Eur
            Dim DcTipus As Decimal = Xl_PercentTipus.Value
            Dim DcEur = DcValorAdquisicio * DcTipus / 100
            Xl_AmountCur1.Amt = DTOAmt.Factory(DcEur)
            ButtonOk.Enabled = True
            _AllowEvents = True
        End If
    End Sub
End Class


