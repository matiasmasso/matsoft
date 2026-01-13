Public Class Frm_ProductPluginItem

    Private _ProductPluginItem As DTOProductPlugin.Item
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductPlugin.Item)
        MyBase.New()
        Me.InitializeComponent()
        _ProductPluginItem = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        'If FEB.ProductPluginItem.Load(exs, _ProductPluginItem) Then
        With _ProductPluginItem
            TextBoxPluginNom.Text = .Plugin.Nom
            Dim oProducts As New List(Of DTOProduct)
            If .Product IsNot Nothing Then oProducts.Add(.Product)
            Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
            TextBoxNomEsp.Text = .LangNom.Esp
            TextBoxNomCat.Text = .LangNom.cat
            TextBoxNomEng.Text = .LangNom.eng
            TextBoxNomPor.Text = .LangNom.Por
            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(.Thumbnail)
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
        'Else
        'UIHelper.WarnError(exs)
        'Me.Close()
        'End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged,
         TextBoxNomCat.TextChanged,
          TextBoxNomEng.TextChanged,
           TextBoxNomPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductPluginItem
            .LangNom = New DTOLangText(TextBoxNomEsp.Text, TextBoxNomCat.Text, TextBoxNomEng.Text, TextBoxNomPor.Text)
            .Product = Xl_LookupProduct1.Product.Trim()
            If .Thumbnail Is Nothing Then
                .Thumbnail = LegacyHelper.ImageHelper.Converter(PictureBox1.Image)
            End If
        End With

        Dim oTrimmedItem As New DTOProductPlugin.Item(_ProductPluginItem.Guid)
        With oTrimmedItem
            .Plugin = New DTOProductPlugin(_ProductPluginItem.Plugin.Guid)
            .LangNom = New DTOLangText(TextBoxNomEsp.Text, TextBoxNomCat.Text, TextBoxNomEng.Text, TextBoxNomPor.Text)
            .Product = New DTOProduct(Xl_LookupProduct1.Product.Guid)
            If .Thumbnail Is Nothing Then
                .Thumbnail = LegacyHelper.ImageHelper.Converter(PictureBox1.Image)
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.ProductPluginItem.Update(exs, oTrimmedItem) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPluginItem))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.ProductPluginItem.Delete(exs, _ProductPluginItem) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPluginItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupProductSku_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProduct = e.Argument
        If FEB.Product.Load(oProduct, exs) Then
            With oProduct
                TextBoxNomEsp.Text = .Nom.Esp
                TextBoxNomCat.Text = .Nom.Cat
                TextBoxNomEng.Text = .Nom.Eng
                TextBoxNomPor.Text = .Nom.Por
            End With
            Dim url = oProduct.ThumbnailUrl(True)
            Try
                PictureBox1.Load(url)
            Catch ex As Exception

            End Try

            ButtonOk.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


End Class


