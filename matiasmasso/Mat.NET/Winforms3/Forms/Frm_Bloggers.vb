Public Class Frm_Bloggers
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Bloggers
        HighlightedPostsEsp
        HighlightedPostsPor
    End Enum

    Public Sub New(oMode As DTO.Defaults.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()

        _SelectionMode = oMode
    End Sub

    Private Async Sub Frm_Bloggers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oBloggers = Await FEB.Bloggers.All(exs)
        If exs.Count = 0 Then
            Xl_Bloggers1.Load(oBloggers, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Leads1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Bloggers1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
    End Sub

    Private Async Sub Xl_Bloggers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Bloggers1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.HighlightedPostsEsp
                Await RefrescaHighlightedPosts(DTOLang.ESP)
            Case Tabs.HighlightedPostsPor
                Await RefrescaHighlightedPosts(DTOLang.POR)
        End Select
    End Sub

    Private Async Sub Xl_BloggerPostsOfTheWeek1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BloggerPostsOfTheWeekEsp.RequestToRefresh
        Await RefrescaHighlightedPosts(DTOLang.ESP)
    End Sub

    Private Async Sub Xl_BloggerPostsOfTheWeek2_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BloggerPostsOfTheWeekPor.RequestToRefresh
        Await RefrescaHighlightedPosts(DTOLang.POR)
    End Sub

    Private Async Function RefrescaHighlightedPosts(olang As DTOLang) As Task
        Dim exs As New List(Of Exception)
        Dim oPosts = Await FEB.BloggerPosts.HighlightedPosts(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Select Case olang.Id
                Case DTOLang.Ids.POR
                    Dim oFilteredPosts As List(Of DTOBloggerPost) = oPosts.Where(Function(x) x.Lang.Equals(olang)).ToList
                    Xl_BloggerPostsOfTheWeekPor.Load(oFilteredPosts)
                Case Else
                    Dim oFilteredPosts As List(Of DTOBloggerPost) = oPosts.Where(Function(x) x.Lang.Id = DTOLang.Ids.ESP).ToList
                    Xl_BloggerPostsOfTheWeekEsp.Load(oFilteredPosts)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class