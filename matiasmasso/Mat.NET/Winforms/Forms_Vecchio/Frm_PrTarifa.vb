

Public Class Frm_PrTarifa
    Private mTarifa As PrTarifa = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oTarifa As PrTarifa)
        MyBase.New()
        Me.InitializeComponent()
        mTarifa = oTarifa
        With mTarifa
            PictureBoxLogo.Image = .Revista.Logo
            LabelYea.Text = "tarifas " & .Yea
            LabelPageSize.Text = PrInsercio.GetSizeCodName(.SizeCod)
            If .Tarifa Is Nothing Then
                Xl_AmtTarifa.Amt = BLLApp.EmptyAmt
            Else
                Xl_AmtTarifa.Amt = .Tarifa
            End If
            Xl_PercentDto.Value = .Dto
            Xl_AmtNet.Amt = Xl_AmtTarifa.Amt.Percent(.Dto)
            Xl_PercentDt2.Value = .Dt2
            Xl_AmtLiquid.Amt = Xl_AmtNet.Amt.Percent(.Dt2)
        End With
        mAllowEvents = True
    End Sub

    Private Sub Xl_AmtTarifa_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtTarifa.AfterUpdate
        If mAllowEvents Then
            ButtonOk.Enabled = True
            Calcula()
        End If
    End Sub

    Private Sub Xl_Percent_AfterUpdate(ByVal SngPercent As Object, ByVal e As System.EventArgs) Handles _
    Xl_PercentDto.AfterUpdate, _
    Xl_PercentDt2.AfterUpdate
        If mAllowEvents Then
            ButtonOk.Enabled = True
            Calcula()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mTarifa
            .Tarifa = Xl_AmtTarifa.Amt
            .Dto = Xl_PercentDto.Value
            .Dt2 = Xl_PercentDt2.Value
            .Update()
        End With
        RaiseEvent AfterUpdate(mTarifa, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub Calcula()
        Xl_AmtNet.Amt = Xl_AmtTarifa.Amt.Percent(100 - Xl_PercentDto.Value)
        Xl_AmtLiquid.Amt = Xl_AmtNet.Amt.Percent(100 - Xl_PercentDt2.Value)
    End Sub
End Class