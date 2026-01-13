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
        'If FEB2.ProductPluginItem.Load(exs, _ProductPluginItem) Then
        With _ProductPluginItem
                TextBoxPluginNom.Text = .Plugin.nom
                Xl_LookupProduct1.Load(.product, DTOProduct.SelectionModes.SelectSku)
                TextBoxNomEsp.Text = .LangNom.esp
                TextBoxNomCat.Text = .LangNom.cat
                TextBoxNomEng.Text = .LangNom.eng
                TextBoxNomPor.Text = .LangNom.por
            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(.thumbnail)
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
            .product = Xl_LookupProduct1.Product
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.ProductPluginItem.Update(exs, _ProductPluginItem) Then
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
            If Await FEB2.ProductPluginItem.Delete(exs, _ProductPluginItem) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductPluginItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupProductSku_AfterUpdate(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSku As DTOProductSku = e.Argument
        If FEB2.ProductSku.Load(oSku, exs) Then
            With oSku
                TextBoxNomEsp.Text = .nom.Esp
                TextBoxNomCat.Text = .nom.Cat
                TextBoxNomEng.Text = .nom.Eng
                TextBoxNomPor.Text = .nom.Por
            End With
            Dim url = oSku.ThumbnailUrl(True)
            PictureBox1.Load(url)
            ButtonOk.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class


