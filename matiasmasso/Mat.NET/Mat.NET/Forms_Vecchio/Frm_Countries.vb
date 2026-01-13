Public Class Frm_Countries
    Private _SelectionMode As bll.dEFAULTS.SelectionModes
    Private _DefaultCountry As DTOCountry

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultCountry As DTOCountry = Nothing, Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultCountry = oDefaultCountry

        Dim oCountries As List(Of DTOCountry) = BLL.BLLCountries.All(BLL.BLLSession.Current.User.Lang)
        Xl_Countries1.Load(oCountries, _DefaultCountry, _SelectionMode)
    End Sub

    Private Sub Xl_Countries1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Countries1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

End Class