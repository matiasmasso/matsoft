Imports Winforms

Public Class Frm_Ranking
    Private _Ranking As DTORanking
    Private _Atlas As List(Of DTOCountry)
    Private _Allowevents As Boolean

    Public Sub New(Optional oRanking As DTORanking = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Ranking = oRanking
    End Sub

    Private Async Sub Frm_Ranking_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Ranking Is Nothing Then
            ProgressBar1.Visible = True
            _Ranking = Await FEB2.Ranking.CustomerRanking(exs, Current.Session.User)
            ProgressBar1.Visible = False
        End If

        _Atlas = DTORanking.Atlas(_Ranking)

        If exs.Count = 0 Then
            With _Ranking
                DateTimePickerTo.Value = .FchTo
                DateTimePickerFrom.Value = .FchFrom
                Xl_ContactsComboProveidors.Load(.Proveidors, .Proveidor, .User.Lang.Tradueix("(todos los proveedores)", "(tots els proveïdors)", "(any supplier)"))
                Xl_RepsCombo1.Load(.Reps, .Rep)
                Xl_LookupProduct1.Load(.Product, DTOProduct.SelectionModes.SelectAny)
                Xl_LookupArea1.Load(.Area)
                Xl_RankingItems1.Load(_Ranking)
            End With
            _Allowevents = True
        Else
            UIHelper.WarnError(exs)
            Exit Sub
        End If
    End Sub

    Private Async Sub Xl_ContactsComboProveidors_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactsComboProveidors.AfterUpdate
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            Dim oProveidor As DTOContact = e.Argument
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetProveidor(exs, _Ranking, oProveidor)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_LookupProduct1.Load(_Ranking.Product, DTOProduct.SelectionModes.SelectAny)
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            Dim oProduct As DTOProduct = e.Argument
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetProduct(exs, _Ranking, oProduct)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Async Sub Xl_LookupDistributionChannel1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupDistributionChannel1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            Dim oChannel As DTODistributionChannel = e.Argument
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetChannel(exs, _Ranking, oChannel)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Xl_RepsCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_RepsCombo1.AfterUpdate
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            Dim oRep As DTORep = e.Argument
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetRep(exs, _Ranking, oRep)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Async Sub DateTimePickerTo_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerTo.ValueChanged
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            If DateTimePickerFrom.Value > DateTimePickerTo.Value Then
                DateTimePickerFrom.Value = DateTimePickerTo.Value
            End If
            DateTimePickerFrom.MaxDate = DateTimePickerTo.Value
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetFchs(exs, _Ranking, DateTimePickerFrom.Value, DateTimePickerTo.Value)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub DateTimePickerFrom_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFrom.ValueChanged
        Dim exs As New List(Of Exception)
        If _Allowevents Then
            ProgressBar1.Visible = True
            Dim oRanking = Await FEB2.Ranking.SetFchs(exs, _Ranking, DateTimePickerFrom.Value, DateTimePickerTo.Value)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                _Ranking = oRanking
                Xl_LookupProduct1.Load(_Ranking.Product, DTOProduct.SelectionModes.SelectAny)
                Xl_RankingItems1.Load(_Ranking)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub PictureBoxExcel_Click(sender As Object, e As EventArgs) Handles PictureBoxExcel.Click
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = DTORanking.ExcelSheet(_Ranking)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny, Xl_LookupArea1.Area, _Atlas)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Async Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oArea As DTOArea = e.Argument
        Xl_LookupArea1.Load(oArea)
        ProgressBar1.Visible = True
        Dim oRanking = Await FEB2.Ranking.SetArea(exs, _Ranking, oArea)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            _Ranking = oRanking
            Xl_RankingItems1.Load(_Ranking)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub CheckBoxZona_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxZona.CheckedChanged
        Dim exs As New List(Of Exception)
        If DirectCast(sender, CheckBox).Checked Then
            Xl_LookupArea1.Visible = True
        Else
            Xl_LookupArea1.Visible = False
            onAreaSelected(Me, New MatEventArgs(Nothing))
        End If
    End Sub

    Private Sub CheckBoxProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProduct.CheckedChanged
        Dim exs As New List(Of Exception)
        If DirectCast(sender, CheckBox).Checked Then
            Xl_LookupProduct1.Visible = True
        Else
            Xl_LookupProduct1.Visible = False
            Xl_LookupProduct1_AfterUpdate(Me, New MatEventArgs(Nothing))
        End If
    End Sub

    Private Sub CheckBoxChannel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxChannel.CheckedChanged
        Dim exs As New List(Of Exception)
        If DirectCast(sender, CheckBox).Checked Then
            Xl_LookupDistributionChannel1.Visible = True
        Else
            Xl_LookupDistributionChannel1.Visible = False
            Xl_LookupDistributionChannel1_AfterUpdate(Me, New MatEventArgs(Nothing))
        End If
    End Sub
End Class