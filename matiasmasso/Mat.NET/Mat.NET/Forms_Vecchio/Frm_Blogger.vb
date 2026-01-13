Public Class Frm_Blogger

    Private _Blogger As DTOBlogger
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onPostSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Posts
    End Enum

    Public Sub New(value As DTOBlogger, Optional oMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Blogger = value
        _SelectionMode = oMode
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL.BLLBlogger.Load(_Blogger)
        With _Blogger
            TextBoxTitle.Text = .Title
            TextBoxUrl.Text = .Url
            Xl_LookupUser1.User = .Author
            Xl_Image1.Bitmap = .Logo
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged, _
         TextBoxUrl.TextChanged, _
          Xl_LookupUser1.AfterUpdate, _
           Xl_Image1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Blogger
            .Title = TextBoxTitle.Text
            .Url = TextBoxUrl.Text
            .Author = Xl_LookupUser1.User
            .Logo = Xl_Image1.Bitmap
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLBlogger.Update(_Blogger, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Blogger))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLBlogger.Delete(_Blogger, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Blogger))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Posts
                Static Loaded As Boolean
                If Not Loaded Then
                    BLL.BLLBlogger.Load(_Blogger)
                    Xl_BloggerPosts1.Load(_Blogger.Posts, _Blogger, _SelectionMode)
                End If
                Loaded = True
        End Select
    End Sub

    Private Sub Xl_BloggerPosts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BloggerPosts1.onItemSelected
        RaiseEvent onPostSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_BloggerPosts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BloggerPosts1.RequestToRefresh
        _Blogger.IsLoaded = False
        BLL.BLLBlogger.Load(_Blogger)
        Xl_BloggerPosts1.Load(_Blogger.Posts, _Blogger, _SelectionMode)
    End Sub
End Class


