Public Class Frm_ProductPack
    Private _ProductPack As DTOProductSku
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Sub New(oProductPack As DTOProductSku)
        MyBase.New
        InitializeComponent()
        _ProductPack = oProductPack

        BLLProductSku.Load(_ProductPack)
        BLLProductPack.Load(_ProductPack)

        With _ProductPack
            TextBoxNomCurt.Text = .NomCurt
            TextBoxNomLlarg.Text = .NomLlarg
        End With
        Xl_LookupProduct1.Load(_ProductPack.Category, Frm_Products.SelModes.SelectProductCategory)
        Xl_ProductPackItems1.Load(_ProductPack.PackItems)
        Xl_Eur1.Amt = Xl_ProductPackItems1.Retail

        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductPack
            .NomCurt = TextBoxNomCurt.Text
            .NomLlarg = TextBoxNomLlarg.Text
            .PackItems = Xl_ProductPackItems1.Values
        End With

        Dim exs As New List(Of Exception)
        If BLLProductPack.Update(_ProductPack, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPack))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLLProductPack.Delete(_ProductPack, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPack))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomCurt.TextChanged,
         TextBoxNomLlarg.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_ProductPackItems1_afterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ProductPackItems1.afterUpdate
        If _AllowEvents Then
            Xl_Eur1.Amt = Xl_ProductPackItems1.Retail
            ButtonOk.Enabled = True
        End If
    End Sub
End Class