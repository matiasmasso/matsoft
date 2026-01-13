Public Class Frm_CurExchange
    Private _Delivery As DTODelivery
    Private _Rate As DTOCurExchangeRate
    Private _AllowEvents As Boolean


    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New
        InitializeComponent()

        _Delivery = oDelivery
    End Sub

    Private Async Sub Frm_CurExchange_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Delivery.Load(_Delivery, exs) Then
            If _Delivery.Items.Count = 0 Then
                UIHelper.WarnError("Entrada buida")
                ButtonOk.Enabled = False
                DateTimePicker1.Enabled = False
            Else
                Xl_Cur1.Cur = _Delivery.Items.First.Price.Cur
                DateTimePicker1.Value = _Delivery.Fch
                Await refresca()
            End If
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _Rate = Await FEB.CurExchangeRate.Closest(Xl_Cur1.Cur, DateTimePicker1.Value, exs)
        If exs.Count = 0 Then
            Dim sText As String = Xl_Cur1.Cur.ExchangeText(_Rate)
            LabelExchange.Text = sText
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged,
         Xl_Cur1.AfterUpdate

        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Delivery.SetCurExchangeRate(exs, _Delivery, Xl_Cur1.Cur, _Rate) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class