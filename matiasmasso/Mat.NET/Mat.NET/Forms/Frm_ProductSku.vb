Public Class Frm_ProductSku

    Private _ProductSku As DTOProductSku
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductSku)
        MyBase.New()
        Me.InitializeComponent()
        _ProductSku = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _ProductSku
            Me.Text = .Id & " " & .NomLlarg
            TextBoxBrand.Text = .Category.Brand.Nom
            Xl_LookupCategory.Product = .Category
            TextBoxNomCurt.Text = .NomCurt
            TextBoxNomLlarg.Text = .Nom
            TextBoxRefProveidor.Text = .RefProveidor
            If .Ean13 IsNot Nothing Then
                TextBoxEAN.Text = .Ean13.Value
            End If
            TextBoxNomProveidor.Text = .NomProveidor
            CheckBoxObsoleto.Checked = .Obsoleto

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomCurt.TextChanged, _
         TextBoxNomLlarg.TextChanged, _
          TextBoxRefProveidor.TextChanged, _
           TextBoxEAN.TextChanged, _
            TextBoxNomProveidor.TextChanged, _
             CheckBoxObsoleto.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductSku
            .Category = Xl_LookupCategory.Product
            .NomCurt = TextBoxNomCurt.Text
            .Nom = TextBoxNomLlarg.Text
            .RefProveidor = TextBoxRefProveidor.Text
            If TextBoxEAN.Text = "" Then
                .Ean13 = Nothing
            Else
                .Ean13 = New DTOEan(TextBoxEAN.Text)
            End If
            .NomProveidor = TextBoxNomProveidor.Text
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLProductSku.Update(_ProductSku, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLProductSku.Delete(_ProductSku, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupCategory_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupCategory.AfterUpdate
        If _AllowEvent Then
            Dim oCategory As DTOProductCategory = e.Argument
            TextBoxBrand.Text = oCategory.Brand.Nom
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


