Public Class Frm_Translate
    Private _LangText As DTOLangText

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oLangText As DTOLangText)
        MyBase.New
        InitializeComponent()

        _LangText = oLangText

        Xl_LangsToTranslate1.Load(_LangText, DTOLang.ESP)
        Xl_LangsToTranslate2.Load(_LangText, DTOLang.POR)
    End Sub

    Private Sub Xl_LangsToTranslateDTOLangText1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LangsToTranslate1.ValueChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate1.Value
        TextBox1.Text = _LangText.Text(oLang)
    End Sub

    Private Sub Xl_LangsToTranslate2_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LangsToTranslate2.ValueChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate2.Value
        TextBox2.Text = _LangText.Text(oLang)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate1.Value
        DTOLangText.SetText(_LangText, oLang, TextBox1.Text)
        ButtonOk.Enabled = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate2.Value
        DTOLangText.SetText(_LangText, oLang, TextBox2.Text)
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        'Select Case _LangText.SrcType
        ' Case DTOLangText.Srcs.WebMenuGroup
        ' Dim oWebMenuGroup As DTOWebMenuGroup = _LangText.Src
        ' If FEB2.WebMenuGroup.Load(exs, oWebMenuGroup) Then
        ' oWebMenuGroup.LangText = _LangText
        ' If Await FEB2.WebMenuGroup.Update(exs, oWebMenuGroup) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(oWebMenuGroup))
        ' Me.Close()
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Case DTOLangText.Srcs.WebMenuItem
        ' Dim oWebMenuItem As DTOWebMenuItem = _LangText.Src
        ' If FEB2.WebMenuItem.Load(exs, oWebMenuItem) Then
        ' oWebMenuItem.LangText = _LangText
        ' If Await FEB2.WebMenuItem.Update(exs, oWebMenuItem) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(oWebMenuItem))
        ' Me.Close()
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Case DTOLangText.Srcs.WinMenuItem
        ' Dim oWinMenuItem As DTOWinMenuItem = _LangText.Src
        ' If FEB2.WinmenuItem.Load(oWinMenuItem, exs) Then
        ' oWinMenuItem.LangText = _LangText
        ' If Await FEB2.WinmenuItem.Update(oWinMenuItem, exs) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(oWinMenuItem))
        ' Me.Close()
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        '         Else
        'UIHelper.WarnError(exs)
        ' End If
        ' Case DTOLangText.Srcs.Category
        '         Dim oCategory As DTOProductCategory = _LangText.Src
        'If FEB2.ProductCategory.Load(oCategory, exs) Then
        ' oCategory.Description = _LangText
        ' If Await FEB2.ProductCategory.Update(oCategory, exs) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(oCategory))
        ' Me.Close()
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If
        ' Case DTOLangText.Srcs.Sku
        ' Dim oSku As DTOProductSku = _LangText.Src
        ' If FEB2.ProductSku.Load(oSku, exs) Then
        ' oSku.Description = _LangText
        ' UIHelper.ToggleProggressBar(Panel1, True)
        ' If Await FEB2.ProductSku.Update(oSku, exs) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(oSku))
        ' Me.Close()
        ' Else
        ' UIHelper.ToggleProggressBar(Panel1, False)
        ' UIHelper.WarnError(exs)
        ' End If
        ' Else
        ' UIHelper.WarnError(exs)
        ' End If

        '        Case DTOLangText.Srcs.Sku
        '        Case DTOLangText.Srcs.SepaText
        '        RaiseEvent AfterUpdate(Me, New MatEventArgs(_LangText))
        '        Me.Close()
        '        End Select
    End Sub
End Class