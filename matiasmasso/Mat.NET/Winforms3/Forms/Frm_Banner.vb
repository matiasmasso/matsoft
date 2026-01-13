Public Class Frm_Banner

    Private _Banner As DTOBanner
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBanner)
        MyBase.New()
        Me.InitializeComponent()
        _Banner = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Banner.Load(_Banner, exs) Then
            With _Banner
                TextBoxNom.Text = .Nom
                If .NavigateTo IsNot Nothing Then
                    TextBoxNavigateTo.Text = .NavigateTo.RelativeUrl(DTOLang.ESP)
                End If
                DateTimePickerFchFrom.Value = .FchFrom
                If .FchTo <> Nothing Then
                    CheckBoxFchTo.Checked = True
                    DateTimePickerFchTo.Value = .FchTo
                    DateTimePickerFchTo.Visible = True
                End If

                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                If .Lang IsNot Nothing Then
                    CheckBoxLang.Checked = True
                    Xl_Langs1.Visible = True
                    Xl_Langs1.Value = .Lang
                End If

                If .Image IsNot Nothing Then
                    Xl_Image1.Load(.Image, DTOBanner.BANNERWIDTH, DTOBanner.BANNERHEIGHT)
                End If
                'Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(.Image)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxNavigateTo.TextChanged,
         DateTimePickerFchFrom.ValueChanged,
          DateTimePickerFchTo.ValueChanged,
            Xl_Image1.AfterUpdate,
             Xl_LookupProduct1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Banner
            .Nom = TextBoxNom.Text
            .NavigateTo = DTOUrl.Factory(TextBoxNavigateTo.Text)
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            If CheckBoxLang.Checked Then
                .Lang = Xl_Langs1.Value
            Else
                .Lang = Nothing
            End If
            .Image = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .Product = Xl_LookupProduct1.Product
        End With
        If Await FEB.Banner.Update(_Banner, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Banner))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
            If Await FEB.Banner.Delete(_Banner, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Banner))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvent Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxLang_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLang.CheckedChanged
        If _AllowEvent Then
            Xl_Langs1.Visible = CheckBoxLang.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


