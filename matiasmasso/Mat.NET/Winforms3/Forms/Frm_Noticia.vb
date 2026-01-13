Public Class Frm_Noticia
    Private _Noticia As DTONoticia = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oNoticia As DTONoticia)
        MyBase.New()
        Me.InitializeComponent()
        _Noticia = oNoticia
    End Sub

    Private Async Sub Frm_Noticia_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Noticia.Load(exs, _Noticia, IncludeImages:=True) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        With _Noticia
            If .Fch = Nothing Then
                DateTimePickerFch.Value = DTO.GlobalVariables.Today()
            Else
                DateTimePickerFch.Value = .Fch
            End If
            Dim oProducts As New List(Of DTOProduct)
            If .product IsNot Nothing Then oProducts.Add(.product)
            Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
            TextBoxUrlRoot.Text = FEB.Noticia.RootUrl(_Noticia.Src, True) & "/"
            TextBoxUrl.Text = .Url.Path().Tradueix(Current.Session.Lang) ' .Url.RelativeUrl(Current.Session.Lang) ' .urlFriendlySegment
            CheckBoxProfessional.Checked = .Professional
            TextBoxVisitCount.Text = .VisitCount

            Dim oAllChannels = Await FEB.DistributionChannels.Headers(GlobalVariables.Emp, Current.Session.Lang, exs)
            If exs.Count = 0 Then
                Xl_DistributionChannels_Checklist1.Load(oAllChannels, .DistributionChannels)

                If .DestacarFrom <> Nothing Then
                    CheckBoxDestacar.Checked = True
                    DateTimePickerDestacarFrom.Enabled = True
                    DateTimePickerDestacarFrom.Value = .DestacarFrom
                    DateTimePickerDestacarTo.Enabled = True
                    DateTimePickerDestacarTo.Value = .DestacarTo
                End If

                CheckBoxVisible.Checked = .Visible
                CheckBoxPriority.Checked = .Priority = DTONoticia.PriorityLevels.High

                Xl_LangsTextTitol.Load(.Title)
                Xl_LangsTextExcerpt.Load(.Excerpt)
                Xl_LangsTextContingut.Load(.Text)
                Xl_UsrLog1.Load(.UsrLog)

                'TextBoxTitolEsp.Text = .Title.Esp
                'TextBoxTitolCat.Text = .Title.Cat
                'TextBoxTitolEng.Text = .Title.Eng
                'TextBoxTitolPor.Text = .Title.Por

                'TextBoxExcerptEsp.Text = DTOLangText.FromHtml(.Excerpt, DTOLang.ESP)
                'TextBoxExcerptCat.Text = DTOLangText.FromHtml(.Excerpt, DTOLang.CAT)
                'TextBoxExcerptEng.Text = DTOLangText.FromHtml(.Excerpt, DTOLang.ENG)
                'TextBoxExcerptPor.Text = DTOLangText.FromHtml(.Excerpt, DTOLang.POR)

                'TextBoxTextEsp.Text = DTOLangText.FromHtml(.Text, DTOLang.ESP)
                'TextBoxTextCat.Text = DTOLangText.FromHtml(.Text, DTOLang.CAT)
                'TextBoxTextEng.Text = DTOLangText.FromHtml(.Text, DTOLang.ENG)
                'TextBoxTextPor.Text = DTOLangText.FromHtml(.Text, DTOLang.POR)

                Await Xl_CategoriasDeNoticia1.Load(.categorias)
                Xl_Image265x150.Bitmap = LegacyHelper.ImageHelper.Converter(.image265x150)
                Dim keywords = Await FEB.KeyWords.All(.Guid, exs)
                If exs.Count = 0 Then
                    Xl_Keywords1.Load(keywords)
                Else
                    UIHelper.WarnError(exs)
                End If

                ComboBoxCod.SelectedIndex = .src
                Select Case .src
                    Case DTOContent.Srcs.News
                        GroupBoxEvento.Visible = False
                    Case DTOContent.Srcs.Eventos
                        GroupBoxEvento.Visible = True
                        If DirectCast(_Noticia, DTOEvento).FchFrom >= DateTimePickerFchFrom.MinDate Then
                            DateTimePickerFchFrom.Value = DirectCast(_Noticia, DTOEvento).FchFrom
                        End If
                        If DirectCast(_Noticia, DTOEvento).FchTo >= DateTimePickerFchTo.MinDate Then
                            DateTimePickerFchTo.Value = DirectCast(_Noticia, DTOEvento).FchTo
                        End If
                        If DirectCast(_Noticia, DTOEvento).Area IsNot Nothing Then
                            Dim oArea = DirectCast(_Noticia, DTOEvento).Area
                            Xl_LookupArea1.Load(oArea)
                        End If
                    Case DTOContent.Srcs.SabiasQue
                        GroupBoxEvento.Visible = False
                End Select
                ButtonDel.Enabled = Not .IsNew


                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Function

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        DateTimePickerFch.ValueChanged,
         Xl_LookupProduct1.AfterUpdate,
         TextBoxUrl.TextChanged,
             CheckBoxVisible.CheckedChanged,
              CheckBoxPriority.CheckedChanged,
               Xl_CategoriasDeNoticia1.AfterUpdate,
                Xl_Keywords1.AfterUpdate,
                 Xl_Image265x150.AfterUpdate,
                  DateTimePickerFchFrom.ValueChanged,
                   DateTimePickerFchTo.ValueChanged,
                    Xl_LookupArea1.AfterUpdate,
                     DateTimePickerDestacarFrom.ValueChanged,
                      DateTimePickerDestacarTo.ValueChanged,
                       CheckBoxDestacar.CheckedChanged,
                        Xl_DistributionChannels_Checklist1.AfterUpdate,
                         Xl_LangsTextTitol.AfterUpdate,
                          Xl_LangsTextExcerpt.AfterUpdate,
                           Xl_LangsTextContingut.AfterUpdate


        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await ValidateDestacar(exs) Then
            With _Noticia
                .Fch = DateTimePickerFch.Value
                .product = Xl_LookupProduct1.Product
                .urlFriendlySegment = TextBoxUrl.Text
                .UrlSegment.Esp = TextBoxUrl.Text
                .professional = CheckBoxProfessional.Checked
                .distributionChannels = Xl_DistributionChannels_Checklist1.SelectedValues
                .visible = CheckBoxVisible.Checked
                .priority = IIf(CheckBoxPriority.Checked, DTONoticia.PriorityLevels.High, DTONoticia.PriorityLevels.Low)
                .Title = Xl_LangsTextTitol.Value
                .Excerpt = Xl_LangsTextExcerpt.Value
                .text = Xl_LangsTextContingut.Value
                '.Title = New DTOLangText(TextBoxTitolEsp.Text, TextBoxTitolCat.Text, TextBoxTitolEng.Text, TextBoxTitolPor.Text)
                '.Excerpt = DTOLangText.Html(TextBoxExcerptEsp.Text, TextBoxExcerptCat.Text, TextBoxExcerptEng.Text, TextBoxExcerptPor.Text)
                '.Text = DTOLangText.Html(TextBoxTextEsp.Text, TextBoxTextCat.Text, TextBoxTextEng.Text, TextBoxTextPor.Text)

                If CheckBoxDestacar.Checked Then
                    .destacarFrom = DateTimePickerDestacarFrom.Value
                    .destacarTo = DateTimePickerDestacarTo.Value
                Else
                    .destacarFrom = Nothing
                    .destacarTo = Nothing
                End If

                .categorias = Xl_CategoriasDeNoticia1.CheckedItems
                .keywords = Xl_Keywords1.Values
                .Image265x150 = Xl_Image265x150.ByteArray() ' LegacyHelper.ImageHelper.Converter(Xl_Image265x150.Bitmap)

                .src = ComboBoxCod.SelectedIndex
                If .src = DTOContent.Srcs.Eventos Then
                    DirectCast(_Noticia, DTOEvento).FchFrom = DateTimePickerFchFrom.Value
                    DirectCast(_Noticia, DTOEvento).FchTo = DateTimePickerFchTo.Value
                    DirectCast(_Noticia, DTOEvento).Area = Xl_LookupArea1.Area
                End If
            End With

            If Await FEB.Noticia.Update(exs, _Noticia) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Noticia))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar la noticia")
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function ValidateDestacar(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        If CheckBoxDestacar.Checked Then
            Dim oDestacats = Await FEB.Noticias.Destacats(exs, DTOContent.Srcs.News, Current.Session.User)
            If exs.Count = 0 Then
                Dim oOverlappingNoticias As List(Of DTONoticia) = oDestacats.Where(Function(x) TimeHelper.Overlaps(x.DestacarFrom, x.DestacarTo, DateTimePickerDestacarFrom.Value, DateTimePickerDestacarTo.Value)).ToList
                If oOverlappingNoticias.Count = 0 Then
                    retval = True
                Else
                    exs.Add(New Exception("Les següents noticies estan destacades en les mateixes dates:"))
                    For Each item As DTONoticia In oOverlappingNoticias
                        exs.Add(New Exception(String.Format("{0:dd/MM/yy} {1} (destacada del {2:dd/MM/yy} al {3:dd/MM/yy})", item.Fch, item.Title, item.DestacarFrom, item.DestacarTo)))
                    Next
                End If
            End If
        Else
            retval = True
        End If
        Return retval
    End Function

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta noticia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Noticia.Delete(exs, _Noticia) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la escriptura")
            End If
        End If
    End Sub

    Private Sub ComboBoxCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        If _AllowEvents Then
            Select Case DirectCast(ComboBoxCod.SelectedIndex, DTONoticiaBase.Srcs)
                Case DTOContent.Srcs.News
                    GroupBoxEvento.Visible = False
                Case DTOContent.Srcs.Eventos
                    GroupBoxEvento.Visible = True
                Case DTOContent.Srcs.SabiasQue
                    GroupBoxEvento.Visible = False
            End Select
            TextBoxUrlRoot.Text = FEB.Noticia.RootUrl(_Noticia.Src, True) & "/"
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxDestacar_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDestacar.CheckedChanged
        If _AllowEvents Then
            DateTimePickerDestacarFrom.Enabled = CheckBoxDestacar.Checked
            DateTimePickerDestacarTo.Enabled = CheckBoxDestacar.Checked
        End If
    End Sub

    Private Sub CheckBoxProfessional_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProfessional.CheckedChanged
        If _AllowEvents Then
            Xl_DistributionChannels_Checklist1.Visible = CheckBoxProfessional.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub


End Class