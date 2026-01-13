Public Class Frm_Vacacion
    Private _Vacacion As DTOVacacion
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)
    Public Sub New(value As DTOVacacion)
        MyBase.New()
        Me.InitializeComponent()
        _Vacacion = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCombos()
        With _Vacacion
            If .MonthDayFrom IsNot Nothing Then
                ComboBoxFromDay.SelectedIndex = .MonthDayFrom.Day
                ComboBoxFromMonth.SelectedIndex = .MonthDayFrom.Month
            End If
            If .MonthDayTo IsNot Nothing Then
                ComboBoxToDay.SelectedIndex = .MonthDayTo.Day
                ComboBoxToMonth.SelectedIndex = .MonthDayTo.Month
            End If
            If .MonthDayResult IsNot Nothing Then
                If .MonthDayResult.Day = 0 And .MonthDayResult.Month = 0 Then
                Else
                    RadioButtonSpecificDay.Checked = True
                    GroupBoxResult.Visible = True
                    ComboBoxResultDay.SelectedIndex = .MonthDayResult.Day
                    ComboBoxResultMonth.SelectedIndex = .MonthDayResult.Month
                End If
            End If
        End With

        _AllowEvents = True
    End Sub

    Private Sub LoadCombos()
        ComboBoxFromDay.DataSource = DaysDataSource()
        ComboBoxFromMonth.DataSource = MonthsDataSource()
        ComboBoxToDay.DataSource = DaysDataSource()
        ComboBoxToMonth.DataSource = MonthsDataSource()
        ComboBoxResultDay.DataSource = DaysDataSource()
        ComboBoxResultMonth.DataSource = MonthsDataSource()
    End Sub

    Private Function DaysDataSource() As List(Of String)
        Dim retval As New List(Of String)
        retval.Add("-")
        For i As Integer = 1 To 31
            retval.Add(Format(i, "00"))
        Next
        Return retval
    End Function

    Private Function MonthsDataSource() As List(Of String)
        Dim retval As New List(Of String)
        retval.Add("-")
        Dim oLang As DTOLang = Current.Session.Lang
        For i As Integer = 1 To 12
            retval.Add(oLang.MesAbr(i))
        Next
        Return retval
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        ComboBoxFromDay.SelectedIndexChanged,
         ComboBoxFromMonth.SelectedIndexChanged,
          ComboBoxToDay.SelectedIndexChanged,
           ComboBoxToMonth.SelectedIndexChanged,
            ComboBoxResultDay.SelectedIndexChanged,
             ComboBoxResultMonth.SelectedIndexChanged,
              RadioButton30Days.CheckedChanged

        If _AllowEvents Then
            GroupBoxResult.Visible = RadioButtonSpecificDay.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim Day1, Month1, Day2, Month2, Day3, Month3 As Integer
        If ComboBoxFromDay.SelectedIndex >= 0 Then Day1 = ComboBoxFromDay.SelectedIndex
        If ComboBoxFromMonth.SelectedIndex >= 0 Then Month1 = ComboBoxFromMonth.SelectedIndex
        If ComboBoxToDay.SelectedIndex >= 0 Then Day2 = ComboBoxToDay.SelectedIndex
        If ComboBoxToMonth.SelectedIndex >= 0 Then Month2 = ComboBoxToMonth.SelectedIndex
        If ComboBoxResultDay.SelectedIndex >= 0 Then Day3 = ComboBoxResultDay.SelectedIndex
        If ComboBoxResultMonth.SelectedIndex >= 0 Then Month3 = ComboBoxResultMonth.SelectedIndex

        With _Vacacion
            .MonthDayFrom = New DTOMonthDay(Month1, Day1)
            .MonthDayTo = New DTOMonthDay(Month2, Day2)
            If RadioButton30Days.Checked Then
                .MonthDayResult = New DTOMonthDay(0, 0)
            Else
                .MonthDayResult = New DTOMonthDay(Month3, Day3)
            End If
        End With

        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Vacacion))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        RaiseEvent RequestToDelete(Me, New MatEventArgs(_Vacacion))
        Me.Close()
    End Sub


End Class