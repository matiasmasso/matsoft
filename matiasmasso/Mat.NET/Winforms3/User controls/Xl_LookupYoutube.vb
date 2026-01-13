

Public Class Xl_LookupYoutube
    Inherits Xl_LookupTextboxButton

    Private mYouTube As DTOYouTubeMovie

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property YouTube() As DTOYouTubeMovie
        Get
            Return mYouTube
        End Get
        Set(ByVal value As DTOYouTubeMovie)
            mYouTube = value
            If mYouTube Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOYouTubeMovie.FullText(mYouTube)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.YouTube = Nothing
    End Sub

    Private Sub Xl_LookupYouTube_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_YouTubeMovies(Nothing, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onYouTubeSelected
        oFrm.Show()
    End Sub

    Private Sub onYouTubeSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mYouTube = DirectCast(sender, DTOYouTubeMovie)
        MyBase.Text = DTOYouTubeMovie.FullText(mYouTube)
        RaiseEvent AfterUpdate(sender, MatEventArgs.Empty)
    End Sub

End Class
