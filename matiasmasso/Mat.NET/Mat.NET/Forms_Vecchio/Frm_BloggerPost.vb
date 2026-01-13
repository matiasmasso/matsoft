Public Class Frm_BloggerPost
    Private _Post As DTOBloggerPost

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBloggerPost)
        MyBase.New()
        Me.InitializeComponent()
        _Post = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL.BLLBloggerPost.Load(_Post)
        With _Post
            Xl_Lookup_Blogger1.Blogger = .Blogger
            TextBoxTitle.Text = .Title
            TextBoxUrl.Text = .Url
            DateTimePicker1.Value = .Fch
            If .HighlightFrom <> Nothing Or .HighlightTo <> Nothing Then
                CheckBoxHighlight.Checked = True
                GroupBoxHighlight.Visible = True
                If .HighlightFrom <> Nothing Then DateTimePickerHighlightFrom.Value = .HighlightFrom
                If .HighlightTo <> Nothing Then DateTimePickerHighlightTo.Value = .HighlightTo
            End If
            Xl_Products1.Load(.Products)

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
          Xl_Lookup_Blogger1.AfterUpdate, _
           TextBoxTitle.TextChanged, _
            TextBoxUrl.TextChanged, _
             DateTimePicker1.ValueChanged, _
              DateTimePickerHighlightFrom.ValueChanged, _
               DateTimePickerHighlightTo.ValueChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Post
            .Blogger = Xl_Lookup_Blogger1.Blogger
            .Title = TextBoxTitle.Text
            .Url = TextBoxUrl.Text
            .Fch = DateTimePicker1.Value
            If CheckBoxHighlight.Checked Then
                .HighlightFrom = DateTimePickerHighlightFrom.Value
                .HighlightTo = DateTimePickerHighlightTo.Value
            Else
                .HighlightFrom = Nothing
                .HighlightTo = Nothing
            End If
            .Products = Xl_Products1.Values()
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLBloggerPost.Update(_Post, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Post))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLBloggerpost.Delete(_Post, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Post))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub



    Private Sub ButtonAddProduct_Click(sender As Object, e As EventArgs) Handles ButtonAddProduct.Click
        Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        Dim oProducts As List(Of DTOProduct) = Xl_Products1.Values
        If oProducts.Contains(oProduct) Then
            MsgBox(BLL.BLLProduct.FullNom(oProduct) & vbCrLf & "producte duplicat", MsgBoxStyle.Exclamation)
        Else
            oProducts.Add(oProduct)
            Xl_Products1.Load(oProducts)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_ProductsBase1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Products1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_Lookup_ProductBase1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub CheckBoxHighlight_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHighlight.CheckedChanged
        If _AllowEvent Then
            GroupBoxHighlight.Visible = CheckBoxHighlight.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


