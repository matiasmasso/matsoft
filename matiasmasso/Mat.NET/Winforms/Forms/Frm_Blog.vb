Public Class Frm_Blog
    Private loadedSscs As Boolean
    Private loadedKeywords As Boolean
    Private loadedLeads As Boolean

    Private Enum Tabs
        Posts
        Subscriptors
        Keywords
        Leads
    End Enum

    Private Async Sub Frm_Blog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oBlogPosts = Await FEB2.Blog2Posts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_BlogPosts1.Load(oBlogPosts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Keywords
                If Not loadedKeywords Then
                    Dim oSearchRequests = Await FEB2.SearchRequests.All(GlobalVariables.Emp, exs)
                    If exs.Count = 0 Then
                        Xl_SearchRequests1.Load(oSearchRequests)
                        loadedKeywords = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Leads
                If Not loadedLeads Then
                    Dim oLeads = Await FEB2.Users.All(exs, Current.Session.Emp)
                    If exs.Count = 0 Then
                        oLeads = oLeads.OrderByDescending(Function(x) x.FchCreated).ToList
                        Xl_Leads1.Load(oLeads)
                        loadedLeads = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub Xl_TextboxSearchLeads_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchLeads.AfterUpdate
        Xl_Leads1.Filter = e.Argument
    End Sub
End Class