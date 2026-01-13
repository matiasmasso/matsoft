

Public Class Frm_RepTraspas
    Private _SrcRepProducts As List(Of DTORepProduct)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oSrcRepProducts As List(Of DTORepProduct))
        MyBase.New()
        Me.InitializeComponent()
        _SrcRepProducts = oSrcRepProducts
        Refresca()
    End Sub

    Private Sub Refresca()
        Xl_RepProductsxRep1.Load(_SrcRepProducts, Xl_RepProducts.Modes.ByRep)
        TextBoxOldRep.Text = _SrcRepProducts.First.Rep.NickName
        DateTimePickerBaixa.Value = Today.AddDays(-1)
        DateTimePickerAlta.Value = Today
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oNewRepZona As RepProduct = Nothing

        Dim oRepProducts As New List(Of DTORepProduct)

        For Each oRepProduct As DTORepProduct In _SrcRepProducts
            oRepProduct.FchTo = DateTimePickerBaixa.Value
            oRepProducts.Add(oRepProduct)

            If CheckBoxTraspas.Checked Then
                Dim oNewRepProduct As DTORepProduct = BLL.BLLRepProduct.NewRepProduct(oRepProduct.Product, Xl_LookupRep1.Rep, oRepProduct.Area)
                With oNewRepProduct
                    .Cod = oRepProduct.Cod
                    If IsNumeric(TextBoxComStd.Text) Then
                        .ComStd = TextBoxComStd.Text
                    End If
                    If IsNumeric(TextBoxComRed.Text) Then
                        .ComRed = TextBoxComRed.Text
                    End If
                End With
                oRepProducts.Add(oNewRepProduct)
            End If
        Next

        Dim exs As New List(Of Exception)
        If BLL.BLLRepProducts.update(oRepProducts, exs) Then
            RaiseEvent AfterUpdate(Nothing, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la zona")
        End If

    End Sub


    Private Sub CheckBoxTraspas_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTraspas.CheckedChanged
        GroupBoxTraspas.Enabled = CheckBoxTraspas.Checked
    End Sub

    Private Sub Xl_LookupRep1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRep1.AfterUpdate
        Dim oRep As DTORep = e.Argument
        BLL.BLLRep.Load(oRep)
        With oRep
            TextBoxComRed.Text = .ComisionReducida
            TextBoxComStd.Text = .ComisionStandard
        End With

    End Sub
End Class