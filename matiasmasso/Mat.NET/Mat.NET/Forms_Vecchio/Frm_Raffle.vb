Public Class Frm_Raffle
    Private _Raffle As DTORaffle
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Pregunta
        Bases
        Guanyador
        Participants
        Jutges
    End Enum

    Public Sub New(value As DTORaffle)
        MyBase.New()
        Me.InitializeComponent()
        _Raffle = value
        BLL.BLLRaffle.Load(_Raffle)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Raffle
            TextBoxTitle.Text = .Title
            DateTimePickerFchFrom.Value = .FchFrom
            DateTimePickerFchTo.Value = .FchTo
            TextBoxUrlExterna.Text = .UrlExterna
            TextBoxQuestion.Text = .Question
            TextBoxAnswers.Text = BLL.TextHelper.StringListToMultiline(.Answers)
            LoadComboBoxRightAnswer()
            Xl_LookupProduct1.Product = .Product
            Xl_ImageFeatured.Bitmap = .ImageFbFeatured
            Xl_ImageBanner.Bitmap = .ImageBanner600
            Xl_ImageCallToAction.Bitmap = .ImageCallToAction500
            Xl_ImageWinner.Bitmap = .ImageWinner
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
            .SelectedIndex = _Raffle.RightAnswer
        End With
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged, _
        DateTimePickerFchTo.ValueChanged, _
        TextBoxUrlExterna.TextChanged, _
        TextBoxQuestion.TextChanged, _
        ComboBoxRightAnswer.SelectedIndexChanged, _
        Xl_LookupProduct1.AfterUpdate, _
        Xl_ImageFeatured.AfterUpdate, _
        Xl_ImageBanner.AfterUpdate, _
        Xl_ImageCallToAction.AfterUpdate, _
        Xl_ImageWinner.AfterUpdate, _
        TextBoxWinner.TextChanged, _
        TextBoxBases.TextChanged, _
        Xl_AmtCostPrize.AfterUpdate, _
        Xl_AmtCostPubli.AfterUpdate, _
         CheckBoxWinnerReaction.CheckedChanged, _
          CheckBoxDistributorReaction.CheckedChanged, _
           CheckBoxDelivery.CheckedChanged, _
            CheckBoxPicture.CheckedChanged, _
        DateTimePickerWinnerReaction.ValueChanged, _
         DateTimePickerDistributorReaction.ValueChanged, _
          DateTimePickerPicture.ValueChanged, _
           DateTimePickerDelivery.ValueChanged


        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of exception)
        With _Raffle
            .Title = TextBoxTitle.Text
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DateTimePickerFchTo.Value
            .Product = Xl_LookupProduct1.Product
            .UrlExterna = TextBoxUrlExterna.Text
            .Question = TextBoxQuestion.Text
            .Answers = BLL.TextHelper.StringListFromMultiline(TextBoxAnswers.Text)
            .RightAnswer = ComboBoxRightAnswer.SelectedIndex
            .ImageFbFeatured = Xl_ImageFeatured.Bitmap
            .ImageBanner600 = Xl_ImageBanner.Bitmap
            .ImageCallToAction500 = Xl_ImageCallToAction.Bitmap
            .ImageWinner = Xl_ImageWinner.Bitmap
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
        If BLL.BLLRaffle.Update(_Raffle, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Raffle))
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
            If BLL.BLLRaffle.Delete(_Raffle, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Raffle))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Participants
                Static BlDone As Boolean
                If Not BlDone Then
                    BlDone = True
                    Xl_RaffleParticipants1.Load(BLL.BLLRaffle.Participants(_Raffle), Xl_RaffleParticipants.Modes.FromRaffle)
                    Xl_Grf1.Load(BLL.BLLRaffle.GrfEnrollment(_Raffle))
                End If
            Case Tabs.Jutges
                Static BlJutges As Boolean
                If Not BlJutges Then
                    BlJutges = True
                    Xl_Jutges.Load(BLL.BLLRaffle.Jutges(_Raffle))
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

    Private Sub Xl_RaffleParticipants1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RaffleParticipants1.RequestToRefresh
        Xl_RaffleParticipants1.Load(BLL.BLLRaffle.Participants(_Raffle), Xl_RaffleParticipants.Modes.FromRaffle)
    End Sub


    Private Sub Xl_Lookup_Jutges_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_Jutge.AfterUpdate
        If e.Argument Is Nothing Then
            ButtonAddJutge.Enabled = False
        Else
            ButtonAddJutge.Enabled = True
        End If
    End Sub

    Private Sub ButtonAddJutge_Click(sender As Object, e As EventArgs) Handles ButtonAddJutge.Click
        Dim oJutges = Xl_Jutges.Values
        If oJutges.Contains(Xl_Lookup_Jutge.User) Then
            ButtonAddJutge.Enabled = False
        Else
            oJutges.Add(Xl_Lookup_Jutge.User)
            Dim exs As New List(Of Exception)
            If BLL.BLLRaffle.UpdateJutges(_Raffle, oJutges, exs) Then
                Xl_Jutges.Load(oJutges)
                ButtonAddJutge.Enabled = False
            Else
                UIHelper.WarnError(exs, "error al desar els jutges")
            End If
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
End Class


