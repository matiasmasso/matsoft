Public Class Frm_ProductCategoryStats
    Private _Category As DTOProductCategory
    Private _Values As List(Of DTOProductStat)
    Private _Min As DTOYearMonth
    Private _Max As DTOYearMonth
    Private _AllowEvents As Boolean

    Public Sub New(oCategory As DTOProductCategory)
        MyBase.New
        InitializeComponent()

        _Category = oCategory

    End Sub

    Private Async Sub Frm_ProductCategoryStats_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Ranking " & _Category.nom.Tradueix(Current.Session.Lang)

        Dim exs As New List(Of Exception)
        _Values = Await FEB2.ProductStats.All(_Category, exs)

        If exs.Count = 0 Then
            ProgressBar1.Visible = False
            Dim sMin = _Values.SelectMany(Function(x) x.items).Min(Function(y) y.Tag)
            Dim sMax = _Values.SelectMany(Function(x) x.items).Max(Function(y) y.Tag)
            _Min = DTOYearMonth.FromTag(sMin)
            _Max = DTOYearMonth.FromTag(sMax)
            Dim iMonths = DTOYearMonth.MonthsDiff(_Min, _Max)

            With TrackBarFrom
                .Minimum = 0
                .Maximum = iMonths
                .Value = IIf(iMonths - 12 < .Minimum, .Minimum, iMonths - 12)
            End With

            With TrackBarTo
                .Minimum = 0
                .Maximum = iMonths
                .Value = .Maximum
            End With

            LabelFchFrom.Text = FchFrom.Formatted(DTOLang.CAT)
            LabelFchTo.Text = FchTo.Formatted(DTOLang.CAT)
            Xl_ProductStats1.Load(_Values, FchFrom, FchTo)
            _AllowEvents = True
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TrackBarFrom_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarFrom.ValueChanged
        If _AllowEvents Then
            LabelFchFrom.Text = FchFrom.Formatted(DTOLang.CAT)
            If TrackBarTo.Value < TrackBarFrom.Value Then TrackBarTo.Value = TrackBarFrom.Value
            Xl_ProductStats1.Load(_Values, FchFrom, FchTo)
        End If
    End Sub

    Private Sub TrackBarTo_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarTo.ValueChanged
        If _AllowEvents Then
            LabelFchTo.Text = FchTo.Formatted(DTOLang.CAT)
            If TrackBarFrom.Value > TrackBarTo.Value Then TrackBarFrom.Value = TrackBarTo.Value
            Xl_ProductStats1.Load(_Values, FchFrom, FchTo)
        End If
    End Sub

    Private Function FchFrom() As DTOYearMonth
        Return _Min.AddMonths(TrackBarFrom.Value)
    End Function

    Private Function FchTo() As DTOYearMonth
        Return _Min.AddMonths(TrackBarTo.Value)
    End Function


End Class