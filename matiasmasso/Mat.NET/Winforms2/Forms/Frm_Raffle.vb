

Public Class Frm_Raffle
    Private _Raffle As DTORaffle
    Private _TabDone As Boolean
    Private _TabWinner As Boolean
    Private _TabDist As Boolean
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Pregunta
        Bases
        Guanyador
        Participants
        Distribuidors
    End Enum

    Public Sub New(value As DTORaffle)
        MyBase.New()
        Me.InitializeComponent()
        _Raffle = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Raffle.Load(_Raffle, exs) Then
            With _Raffle
                TextBoxTitle.Text = .Title
                Xl_Langs1.Value = .Lang
                Xl_LookupCountry1.Country = .Country
                DateTimePickerFchFrom.Value = .FchFrom
                DateTimePickerFchTo.Value = .FchTo
                DateTimePickerHHmmTo.Value = .FchTo
                CheckBoxVisible.Checked = .Visible
                TextBoxUrlExterna.Text = .UrlExterna
                TextBoxQuestion.Text = .Question
                TextBoxAnswers.Text = TextHelper.StringListToMultiline(.Answers)
                LoadComboBoxRightAnswer()
                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectSku)
                Xl_ImageFeatured.Load(Await FEB.Raffle.ImageFbFeatured(exs, .Guid), 178, 125, "Imatge per Facebook")
                Xl_ImageBanner.Load(Await FEB.Raffle.ImageBanner600(exs, .Guid), 600, 0, "Banner sorteig")
                Xl_ImageCallToAction.Load(Await FEB.Raffle.ImageCallToAction500(exs, .Guid), 1000, 1000, "Call to action")
                Xl_ImageWinner.Load(Await FEB.Raffle.ImageWinner(exs, .Guid), 0, 0, "Guanyador del sorteig")
                If .Winner Is Nothing Then
                    ButtonWinner.Enabled = False
                Else
                    TextBoxWinner.Text = .Winner.User.EmailAddress
                End If

                If .CostPrize IsNot Nothing Then
                    Xl_AmtCostPrize.Value = .CostPrize
                End If

                If .CostPubli IsNot Nothing Then
                    Xl_AmtCostPubli.Value = .CostPubli
                End If

                If .FchWinnerReaction <> Nothing Then
                    CheckBoxWinnerReaction.Checked = True
                    DateTimePickerWinnerReaction.Visible = True
                    DateTimePickerWinnerReaction.Value = .FchWinnerReaction
                End If

                If .FchDistributorReaction <> Nothing Then
                    CheckBoxDistributorReaction.Checked = True
                    DateTimePickerDistributorReaction.Visible = True
                    DateTimePickerDistributorReaction.Value = .FchDistributorReaction
                End If

                If .FchPicture <> Nothing Then
                    CheckBoxPicture.Checked = True
                    DateTimePickerPicture.Visible = True
                    DateTimePickerPicture.Value = .FchPicture
                End If

                If .FchDelivery <> Nothing Then
                    CheckBoxDelivery.Checked = True
                    DateTimePickerDelivery.Visible = True
                    DateTimePickerDelivery.Value = .FchDelivery
                End If

                TextBoxBases.Text = .Bases
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadComboBoxRightAnswer()
        Dim oSrc As New List(Of String)
        oSrc.Add("(sel.leccionar la resposta correcta)")
        If TextBoxAnswers.Text.Length > 0 Then
            For Each line As String In TextBoxAnswers.Text.Split(vbCrLf)
                oSrc.Add(line)
            Next
        End If

        With ComboBoxRightAnswer
            .DataSource = oSrc
            If _Raffle.RightAnswer < oSrc.Count Then
                .SelectedIndex = _Raffle.RightAnswer
            End If
        End With
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged,
        DateTimePickerFchTo.ValueChanged,
         DateTimePickerHHmmTo.ValueChanged,
        TextBoxUrlExterna.TextChanged,
        TextBoxQuestion.TextChanged,
        ComboBoxRightAnswer.SelectedIndexChanged,
        Xl_LookupProduct1.AfterUpdate,
        Xl_ImageFeatured.AfterUpdate,
        Xl_ImageBanner.AfterUpdate,
        Xl_ImageCallToAction.AfterUpdate,
        Xl_ImageWinner.AfterUpdate,
        TextBoxWinner.TextChanged,
        TextBoxBases.TextChanged,
        Xl_AmtCostPrize.AfterUpdate,
        Xl_AmtCostPubli.AfterUpdate,
         CheckBoxWinnerReaction.CheckedChanged,
          CheckBoxDistributorReaction.CheckedChanged,
           CheckBoxDelivery.CheckedChanged,
            CheckBoxPicture.CheckedChanged,
             CheckBoxVisible.CheckedChanged,
             NumericUpDownSuplents.ValueChanged,
        DateTimePickerWinnerReaction.ValueChanged,
         DateTimePickerDistributorReaction.ValueChanged,
          DateTimePickerPicture.ValueChanged,
           DateTimePickerDelivery.ValueChanged


        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim DtFchTo As New Date(DateTimePickerFchTo.Value.Year, DateTimePickerFchTo.Value.Month, DateTimePickerFchTo.Value.Day, DateTimePickerHHmmTo.Value.Hour, DateTimePickerHHmmTo.Value.Minute, 0)
        Dim exs As New List(Of Exception)
        With _Raffle
            .Title = TextBoxTitle.Text
            .Lang = Xl_Langs1.Value
            .Country = Xl_LookupCountry1.Country
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DtFchTo
            .Visible = CheckBoxVisible.Checked
            .Product = Xl_LookupProduct1.Product.Trim()
            .UrlExterna = TextBoxUrlExterna.Text
            .Question = TextBoxQuestion.Text
            .Answers = TextHelper.StringListFromMultiline(TextBoxAnswers.Text)
            .RightAnswer = ComboBoxRightAnswer.SelectedIndex
            .imageFbFeatured = LegacyHelper.ImageHelper.Converter(Xl_ImageFeatured.Bitmap)
            .imageBanner600 = LegacyHelper.ImageHelper.Converter(Xl_ImageBanner.Bitmap)
            .imageCallToAction500 = LegacyHelper.ImageHelper.Converter(Xl_ImageCallToAction.Bitmap)
            .imageWinner = LegacyHelper.ImageHelper.Converter(Xl_ImageWinner.Bitmap)
            .Bases = TextBoxBases.Text

            .CostPrize = Xl_AmtCostPrize.Value
            .CostPubli = Xl_AmtCostPubli.Value

            If CheckBoxWinnerReaction.Checked Then
                .FchWinnerReaction = DateTimePickerWinnerReaction.Value
            Else
                .FchWinnerReaction = Nothing
            End If
            If CheckBoxDistributorReaction.Checked Then
                .FchDistributorReaction = DateTimePickerDistributorReaction.Value
            Else
                .FchDistributorReaction = Nothing
            End If
            If CheckBoxPicture.Checked Then
                .FchPicture = DateTimePickerPicture.Value
            Else
                .FchPicture = Nothing
            End If
            If CheckBoxDelivery.Checked Then
                .FchDelivery = DateTimePickerDelivery.Value
            Else
                .FchDelivery = Nothing
            End If
        End With
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.Raffle.Update(exs, _Raffle) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Raffle))
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
            If Await FEB.Raffle.Delete(_Raffle, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Raffle))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Participants
                If Not _TabDone Then
                    _TabDone = True
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oParticipants = Await FEB.RaffleParticipants.Compact(_Raffle, exs)
                    If exs.Count = 0 Then
                        UIHelper.ToggleProggressBar(Panel1, False)
                        Xl_RaffleParticipants1.Load(_Raffle, oParticipants)
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Guanyador
                If Not _TabWinner Then
                    _TabWinner = True
                End If

            Case Tabs.Distribuidors
                If Not _TabDist Then
                    _TabDist = True
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oStoreLocator = Await FEB.StoreLocator.ForRaffle(exs, _Raffle)
                    If exs.Count = 0 Then
                        Xl_RaffleStoreLocator1.Load(oStoreLocator)
                        UIHelper.ToggleProggressBar(Panel1, False)
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub ButtonWinner_Click(sender As Object, e As EventArgs) Handles ButtonWinner.Click
        Dim oFrm As New Frm_RaffleParticipant(_Raffle.Winner)
        oFrm.Show()
    End Sub

    Private Sub TextBoxAnswers_TextChanged(sender As Object, e As EventArgs) Handles TextBoxAnswers.TextChanged
        If _AllowEvent Then
            LoadComboBoxRightAnswer()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub DateTimePickerFchFrom_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFchFrom.ValueChanged
        If _AllowEvent Then
            DateTimePickerFchTo.MinDate = DateTimePickerFchFrom.Value
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub Xl_RaffleParticipants1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RaffleParticipants1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oParticipants = Await FEB.RaffleParticipants.Compact(_Raffle, exs)
        If exs.Count = 0 Then
            Xl_RaffleParticipants1.Load(_Raffle, oParticipants)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub CheckBoxWinnerReaction_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWinnerReaction.CheckedChanged
        DateTimePickerWinnerReaction.Visible = CheckBoxWinnerReaction.Checked
    End Sub

    Private Sub CheckBoxDistributorReaction_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDistributorReaction.CheckedChanged
        DateTimePickerDistributorReaction.Visible = CheckBoxDistributorReaction.Checked
    End Sub

    Private Sub CheckBoxPicture_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPicture.CheckedChanged
        DateTimePickerPicture.Visible = CheckBoxPicture.Checked
    End Sub

    Private Sub CheckBoxDelivery_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDelivery.CheckedChanged
        DateTimePickerDelivery.Visible = CheckBoxDelivery.Checked
    End Sub

    Private Sub Xl_Langs1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Langs1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _AllowEvent Then
            Dim oCountry = DTORaffle.CountryFromLang(Xl_Langs1.Value)
            If Not oCountry.Equals(Xl_LookupCountry1.Country) Then
                If FEB.Country.Load(oCountry, exs) Then
                    Xl_LookupCountry1.Country = oCountry
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_TextboxSearchParticipants_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchParticipants.AfterUpdate
        Xl_RaffleParticipants1.Filter = e.Argument
    End Sub

    Private Sub Xl_TextboxSearchStoreLocator_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchStoreLocator.AfterUpdate
        Xl_RaffleStoreLocator1.Filter = e.Argument
    End Sub
End Class


