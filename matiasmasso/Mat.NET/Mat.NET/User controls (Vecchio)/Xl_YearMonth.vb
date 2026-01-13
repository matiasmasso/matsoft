Public Class Xl_YearMonth
    Private mAllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        LoadMonths()
        Dim oDefaultValue As New DTOYearMonth(Today.Year, Today.Month)
        Me.YearMonth = oDefaultValue
        mAllowEvents = True
    End Sub

    Public Property YearMonth As DTOYearMonth
        Get
            Dim iYea As Integer = NumericUpDownYear.Value
            Dim iMonth As Integer = ComboBoxMonth.SelectedIndex + 1
            Dim retval As New DTOYearMonth(iYea, iMonth)
            Return retval
        End Get
        Set(value As DTOYearMonth)
            NumericUpDownYear.Value = value.Year
            ComboBoxMonth.SelectedIndex = value.Month - 1
        End Set
    End Property


    Private Sub LoadMonths()
        Dim oLang As DTOLang = BLL.BLLLang.CAT
        For i As Integer = 1 To 12
            ComboBoxMonth.Items.Add(oLang.MesAbr(i))
        Next
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        NumericUpDownYear.ValueChanged, _
         ComboBoxMonth.SelectedValueChanged

        If mAllowEvents Then
            RaiseEvent AfterUpdate(Me.YearMonth, EventArgs.Empty)
        End If
    End Sub
End Class
