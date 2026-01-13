

Public Class Frm_Ranking
    Private _Ranking As DTORanking
    Private _Atlas As List(Of DTOCountry)
    Private _Allowevents As Boolean

    Public Sub New(Optional oRanking As DTORanking = Nothing)
        MyBase.New()
        Me.InitializeComponent()

        Dim oLang As DTOLang = Current.Session.Lang
        Dim oMonths As New List(Of ListItem2)
        For i As Integer = 1 To 12
            oMonths.Add(New ListItem2(i, oLang.MesAbr(i)))
        Next

        ComboBoxMonthFrom.DataSource = oMonths
        ComboBoxMonthFrom.DisplayMember = "value"
        ComboBoxMonthFrom.ValueMember = "key"

        Dim oMonths2 As New List(Of ListItem2)
        For i As Integer = 1 To 12
            oMonths2.Add(New ListItem2(i, oLang.MesAbr(i)))
        Next

        ComboBoxMonthTo.DataSource = oMonths2
        ComboBoxMonthTo.DisplayMember = "value"
        ComboBoxMonthTo.ValueMember = "key"

        If oRanking Is Nothing Then
            oRanking = New DTORanking
            With oRanking
                .Year = DTO.GlobalVariables.Today().Year
                .MonthFrom = 1
                .MonthTo = DTO.GlobalVariables.Today().Month
                .User = Current.Session.User
            End With
        End If
        _Ranking = oRanking
    End Sub

    Private Async Sub Frm_Ranking_Load(sender As Object, e As EventArgs) Handles Me.Load
        NumericUpDownYear.Maximum = DTO.GlobalVariables.Today().Year
        NumericUpDownYear.Value = DTO.GlobalVariables.Today().Year
        ComboBoxMonthFrom.SelectedValue = _Ranking.MonthFrom
        ComboBoxMonthTo.SelectedValue = _Ranking.MonthTo
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        _Allowevents = False
        With _Ranking
            .Year = NumericUpDownYear.Value
            .MonthFrom = ComboBoxMonthFrom.SelectedValue
            .MonthTo = ComboBoxMonthTo.SelectedValue
            .Product = If(CheckBoxProduct.Checked, Xl_LookupProduct1.Product, Nothing)
            .Area = If(CheckBoxZona.Checked, Xl_LookupArea1.Area, Nothing)
            .Rep = Xl_RepsCombo1.Value
            .Proveidor = Xl_ContactsComboProveidors.Value
            .Channel = If(CheckBoxChannel.Checked, Xl_LookupDistributionChannel1.DistributionChannel, Nothing)
            .GroupCcx = CheckBoxGroupCcx.Checked
        End With

        ProgressBar1.Visible = True
        _Ranking = Await FEB.Ranking.LoadItems(exs, _Ranking)
        If exs.Count = 0 Then
            _Atlas = DTORanking.Atlas(_Ranking)
            With _Ranking
                Xl_ContactsComboProveidors.Load(.Proveidors, .Proveidor, .User.Lang.Tradueix("(todos los proveedores)", "(tots els proveïdors)", "(any supplier)"))
                If .Reps IsNot Nothing Then Xl_RepsCombo1.Load(.Reps, .Rep)
                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                Xl_LookupArea1.Load(.Area)
            End With
            Xl_RankingItems1.Load(_Ranking)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
        _Allowevents = True
    End Function

    Private Async Sub ControlChanged(sender As Object, e As EventArgs) Handles Xl_ContactsComboProveidors.AfterUpdate,
        Xl_LookupProduct1.AfterUpdate,
        Xl_LookupDistributionChannel1.AfterUpdate,
        Xl_RepsCombo1.AfterUpdate,
        ComboBoxMonthFrom.SelectedIndexChanged,
        ComboBoxMonthTo.SelectedIndexChanged,
        NumericUpDownYear.ValueChanged,
        CheckBoxGroupCcx.CheckedChanged

        If _Allowevents Then Await Refresca()
    End Sub



    Private Sub PictureBoxExcel_Click(sender As Object, e As EventArgs) Handles PictureBoxExcel.Click
        Dim oSheet As MatHelper.Excel.Sheet = DTORanking.ExcelSheet(_Ranking)
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
        Await Refresca()
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
            ControlChanged(Me, New MatEventArgs(Nothing))
        End If
    End Sub

    Private Sub CheckBoxChannel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxChannel.CheckedChanged
        Dim exs As New List(Of Exception)
        If DirectCast(sender, CheckBox).Checked Then
            Xl_LookupDistributionChannel1.Visible = True
        Else
            Xl_LookupDistributionChannel1.Visible = False
            ControlChanged(Me, New MatEventArgs(Nothing))
        End If
    End Sub

End Class