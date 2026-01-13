Public Class Frm_Countries
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _DefaultCountry As DTOCountry

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultCountry As DTOCountry = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultCountry = oDefaultCountry

    End Sub

    Private Async Sub Frm_Countries_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oCountries As List(Of DTOCountry) = Await FEB.Countries.All(Current.Session.User.Lang, exs)
        If exs.Count = 0 Then
            Xl_Countries1.Load(oCountries, _DefaultCountry, _SelectionMode)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Countries1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Countries1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Countries1.filter = e.Argument
    End Sub
End Class