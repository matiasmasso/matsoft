Public Class Frm_Bloggers
    Private _SelectionMode As BLL.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Bloggers
        HighlightedPosts
    End Enum

    Public Sub New(oMode As BLL.Defaults.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()

        _SelectionMode = oMode
        Dim oBloggers As List(Of DTOBlogger) = BLL.BLLBloggers.All
        Xl_Bloggers1.Load(oBloggers, _SelectionMode)
    End Sub

    Private Sub Xl_Leads1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Bloggers1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
    End Sub

    Private Sub Xl_Bloggers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Bloggers1.RequestToRefresh
        Dim oBloggers As List(Of DTOBlogger) = BLL.BLLBloggers.All
        Xl_Bloggers1.Load(oBloggers, _SelectionMode)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.HighlightedPosts
                Static BlDone As Boolean
                If Not BlDone Then
                    RefrescaHighlightedPosts()
                    BlDone = True
                End If
        End Select
    End Sub

    Private Sub Xl_BloggerPostsOfTheWeek1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BloggerPostsOfTheWeek1.RequestToRefresh
        RefrescaHighlightedPosts()
    End Sub

    Private Sub RefrescaHighlightedPosts()
        Dim oPosts As List(Of DTOBloggerPost) = BLL.BLLBloggerPosts.HighlightedPosts()
        Xl_BloggerPostsOfTheWeek1.Load(oPosts)
    End Sub

End Class