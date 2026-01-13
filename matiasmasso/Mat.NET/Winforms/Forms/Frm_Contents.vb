Public Class Frm_Contents
    Private _AllowEvents As Boolean

    Private _DefaultValue As DTONoticia
    Private _Src As DTONoticiaBase.Srcs
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTONoticia = Nothing, Optional oSrc As DTONoticiaBase.Srcs = DTOContent.Srcs.News, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New
        InitializeComponent()

        _DefaultValue = oDefaultValue
        _Src = oSrc
        _SelectionMode = oSelectionMode
    End Sub


    Private Async Sub Frm_Noticias_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ComboBoxCod.SelectedIndex = _Src
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Sub Xl_Noticias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.RequestToRefresh
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Select Case _Src
            Case DTOContent.Srcs.Blog
                Dim oPosts = Await FEB2.Blog2Posts.All(exs)
                If exs.Count = 0 Then
                    Xl_Noticias1.Load(oPosts, _Src, _SelectionMode)
                Else
                    UIHelper.WarnError(exs)
                End If
            Case DTOContent.Srcs.Content
                Dim oPosts = Await FEB2.Contents.All(exs)
                If exs.Count = 0 Then
                    Xl_Noticias1.Load(oPosts, _Src, _SelectionMode)
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Else
                Dim oNoticias = Await FEB2.Noticias.Headers(exs, _Src)
                If exs.Count = 0 Then
                    Xl_Noticias1.Load(oNoticias, _Src, _SelectionMode)
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Function

    Private Async Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        If _AllowEvents Then
            _Src = ComboBoxCod.SelectedIndex
            Await refresca()
        End If
    End Sub


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Noticias1.Filter = e.Argument
    End Sub

    Private Sub Xl_Noticias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.RequestToAddNew
        Select Case _Src
            Case DTOContent.Srcs.News
                Dim oNoticia = DTONoticia.Factory(Current.Session.User)
                oNoticia.Src = _Src
                Dim oFrm As New Frm_Noticia(oNoticia)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
            Case DTOContent.Srcs.Eventos
                Dim oEvento As New DTOEvento
                Dim oFrm As New Frm_Noticia(oEvento)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
            Case DTOContent.Srcs.SabiasQue
                Dim oSabiasQue As New DTOSabiasQuePost
                Dim oFrm As New Frm_Noticia(oSabiasQue)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
            Case DTOContent.Srcs.Blog
                Dim oBlogPost = DTOBlog2Post.Factory(Current.Session.User)
                Dim oFrm As New Frm_BlogPost(oBlogPost)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
            Case DTOContent.Srcs.Content
                Dim oContent As New DTOContent
                Dim oFrm As New Frm_Content(oContent)
                AddHandler oFrm.AfterUpdate, AddressOf refresca
                oFrm.Show()
        End Select
    End Sub

    Private Sub Xl_Noticias1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class