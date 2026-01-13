Public Class Frm_Blogger

    Private _Blogger As DTOBlogger
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onPostSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Posts
    End Enum

    Public Sub New(value As DTOBlogger, Optional oMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Blogger = value
        _SelectionMode = oMode
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Blogger.Load(_Blogger, exs) Then
            With _Blogger
                TextBoxTitle.Text = .Title
                TextBoxUrl.Text = .Url
                Xl_LookupUser1.User = .Author
                If _Blogger.IsNew Then
                    ButtonOk.Enabled = True
                Else
                    ButtonDel.Enabled = True
                    Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(Await FEB2.Blogger.Logo(.Guid, exs))
                    If exs.Count > 0 Then
                        UIHelper.WarnError(exs, "error al descarregar el logo del blogger:")
                    End If
                End If
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
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

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Blogger
            .Title = TextBoxTitle.Text
            .Url = TextBoxUrl.Text
            .Author = Xl_LookupUser1.User
            .Logo = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Blogger.Update(_Blogger, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Blogger))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Blogger.Delete(_Blogger, exs) Then
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
        Dim exs As New List(Of Exception)
        If FEB2.Blogger.Load(_Blogger, exs) Then
            Xl_BloggerPosts1.Load(_Blogger.Posts, _Blogger, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


