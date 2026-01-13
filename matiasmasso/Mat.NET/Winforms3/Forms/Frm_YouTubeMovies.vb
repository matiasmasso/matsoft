Public Class Frm_YouTubeMovies

    Private _Product As DTOProduct
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oProduct As DTOProduct = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Product = oProduct
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_YouTubeMovies_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_YouTubeMovies1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_YouTubeMovies1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_YouTubeMovies1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_YouTubeMovies1.RequestToAddNew
        Dim oYouTubeMovie As New DTOYouTubeMovie
        Dim oFrm As New Frm_Youtube(oYouTubeMovie)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_YouTubeMovies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_YouTubeMovies1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oYouTubeMovies = Await FEB.YouTubeMovies.All(exs, Current.Session.User, _Product)
        If exs.Count = 0 Then
            Xl_YouTubeMovies1.Load(oYouTubeMovies, Nothing, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_YouTubeMovies1.Filter = e.Argument
    End Sub
End Class