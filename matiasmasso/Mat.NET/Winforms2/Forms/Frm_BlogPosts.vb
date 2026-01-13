Public Class Frm_BlogPosts
    Private _DefaultValue As DTOBlogPost
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOBlogPost = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_BlogPosts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_BlogPosts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BlogPosts1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_BlogPosts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BlogPosts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oBlogPosts = Await FEB.BlogPosts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_BlogPosts1.Load(oBlogPosts, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class