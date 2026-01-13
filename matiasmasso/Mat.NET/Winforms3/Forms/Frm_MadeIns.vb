Public Class Frm_MadeIns
    Private _Countries As List(Of DTOCountry)
    Private _Country As DTOCountry
    Private _NoCountry As DTOCountry
    Private _FilteredCountries As List(Of DTOCountry)
    Private _values As List(Of DTOProductSku)

    Private Async Sub Frm_MadeIns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Countries = Await FEB.Countries.All(Current.Session.Lang, exs)
        If exs.Count = 0 Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _values = Await FEB.ProductSkus.All(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Dim madeinSkus = _values.Where(Function(x) x.MadeIn IsNot Nothing)
            _FilteredCountries = _Countries.Where(Function(x) madeinSkus.Any(Function(y) y.MadeIn.Equals(x))).ToList
            _NoCountry = New DTOCountry(Nothing)
            _NoCountry.LangNom = New DTOLangText("(pendiente)", "(pendent)", "(missing)")
            _FilteredCountries.Insert(0, _NoCountry)
            Xl_Countries1.Load(_FilteredCountries)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Countries1_SelectionChanged(sender As Object, e As EventArgs) Handles Xl_Countries1.SelectionChanged
        Dim filteredSkus As List(Of DTOProductSku)
        If CurrentCountry.Equals(_NoCountry) Then
            filteredSkus = _values.Where(Function(x) x.MadeIn Is Nothing).ToList
        Else
            filteredSkus = _values.Where(Function(x) CurrentCountry.Equals(x.MadeIn)).ToList
        End If
        Xl_ProductSkus1.Load(filteredSkus,,,, Xl_ProductSkus.DisplayModes.BrandCategorySku)
    End Sub

    Private Function CurrentCountry() As DTOCountry
        Return Xl_Countries1.Value
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub
End Class