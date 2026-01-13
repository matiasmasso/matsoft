

Public Class Frm_Noticia
    Private _Noticia As Noticia = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oNoticia As Noticia)
        MyBase.New()
        Me.InitializeComponent()
        _Noticia = oNoticia
        NoticiaLoader.Load(_Noticia)
        refresca()
    End Sub

    Private Sub refresca()
        With _Noticia
            If .Fch = Nothing Then
                DateTimePickerFch.Value = Today
            Else
                DateTimePickerFch.Value = .Fch
            End If
            Xl_LookupProduct1.Product = .Product
            TextBoxUrlRoot.Text = BLL_Noticia.RootUrl(_Noticia.Cod, True) & "/"
            TextBoxUrl.Text = .UrlFriendlySegment
            CheckBoxProfessional.Checked = .Professional
            CheckBoxVisible.Checked = .Visible
            CheckBoxPriority.Checked = .PriorityLevel = Noticia.PriorityLevels.High
            TextBoxTitol.Text = .Title
            If .Excerpt > "" Then
                TextBoxExcerpt.Text = .Excerpt.Replace("<br/>", vbCrLf)
            End If
            If .Text > "" Then
                TextBoxText.Text = .Text.Replace("<br/>", vbCrLf)
            End If
            Xl_CategoriasDeNoticia1.Load(.Categorias)
            Xl_Image265x150.Bitmap = .Image265x150
            Xl_Keywords1.Load(BLL_Keyword.FromSrc(.Guid))
            ComboBoxCod.SelectedIndex = .Cod
            Select Case .Cod
                Case Noticia.Cods.News
                    GroupBoxEvento.Visible = False
                Case Noticia.Cods.Eventos
                    GroupBoxEvento.Visible = True
                    If .FchFrom >= DateTimePickerFchFrom.MinDate Then
                        DateTimePickerFchFrom.Value = .FchFrom
                    End If
                    If .FchTo >= DateTimePickerFchTo.MinDate Then
                        DateTimePickerFchTo.Value = .FchTo
                    End If
                    If .Location IsNot Nothing Then
                        Xl_Lookup_Location1.LocationValue = .Location
                    End If
                Case Noticia.Cods.SabiasQue
                    GroupBoxEvento.Visible = False
            End Select
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        DateTimePickerFch.ValueChanged, _
         Xl_LookupProduct1.AfterUpdate, _
        TextBoxTitol.TextChanged, _
         TextBoxUrl.TextChanged, _
          TextBoxExcerpt.TextChanged, _
           TextBoxText.TextChanged, _
            CheckBoxProfessional.CheckedChanged, _
             CheckBoxVisible.CheckedChanged, _
              CheckBoxPriority.CheckedChanged, _
               Xl_CategoriasDeNoticia1.AfterUpdate, _
                Xl_Keywords1.AfterUpdate, _
                 Xl_Image265x150.AfterUpdate, _
                  DateTimePickerFchFrom.ValueChanged, _
                   DateTimePickerFchTo.ValueChanged, _
                    Xl_Lookup_Location1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Noticia
            .Fch = DateTimePickerFch.Value
            .Product = Xl_LookupProduct1.Product
            .UrlFriendlySegment = TextBoxUrl.Text
            .Professional = CheckBoxProfessional.Checked
            .Visible = CheckBoxVisible.Checked
            .PriorityLevel = IIf(CheckBoxPriority.Checked, Noticia.PriorityLevels.High, Noticia.PriorityLevels.Low)
            .Title = TextBoxTitol.Text
            .Excerpt = TextBoxExcerpt.Text.Replace(vbCrLf, "<br/>")
            .Text = TextBoxText.Text.Replace(vbCrLf, "<br/>")
            .Categorias = Xl_CategoriasDeNoticia1.CheckedItems
            .Keywords = Xl_Keywords1.Values
            .Image265x150 = Xl_Image265x150.Bitmap
            .Cod = ComboBoxCod.SelectedIndex
            If .Cod = Noticia.Cods.Eventos Then
                .FchFrom = DateTimePickerFchFrom.Value
                .FchTo = DateTimePickerFchTo.Value
                .Location = Xl_Lookup_Location1.LocationValue
            End If
        End With

        Dim exs As New List(Of exception)
        If NoticiaLoader.Update(_Noticia, exs) Then
            RaiseEvent AfterUpdate(_Noticia, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la noticia")
        End If
    End Sub


    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta noticia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If NoticiaLoader.Delete(_Noticia, exs) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar la escriptura")
            End If
        End If
    End Sub

    Private Sub ComboBoxCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        If _AllowEvents Then
            Select Case CType(ComboBoxCod.SelectedIndex, Noticia.Cods)
                Case Noticia.Cods.News
                    GroupBoxEvento.Visible = False
                Case Noticia.Cods.Eventos
                    GroupBoxEvento.Visible = True
                Case Noticia.Cods.SabiasQue
                    GroupBoxEvento.Visible = False
            End Select
            TextBoxUrlRoot.Text = BLL_Noticia.RootUrl(_Noticia.Cod, True) & "/"
            ButtonOk.Enabled = True
        End If
    End Sub


End Class