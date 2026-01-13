Public Class Frm_Newsletter

    Private _Newsletter As DTONewsletter
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTONewsletter)
        MyBase.New()
        Me.InitializeComponent()
        _Newsletter = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Newsletter.Load(_Newsletter, exs) Then
            With _Newsletter
                If Not .IsNew Then
                    TextBoxId.Text = .Id
                End If
                If .Fch = Nothing Then
                    DateTimePicker1.Value = Today
                Else
                    DateTimePicker1.Value = .Fch
                End If
                TextBoxTitle.Text = .Title
                Xl_NewsletterSources1.Load(_Newsletter.Sources)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            DateTimePicker1.ValueChanged,
            TextBoxTitle.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Newsletter
            .Fch = DateTimePicker1.Value
            .Title = TextBoxTitle.Text
            .Sources = Xl_NewsletterSources1.Values
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Newsletter.Update(_Newsletter, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Newsletter))
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
            If Await FEB2.Newsletter.Delete(_Newsletter, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Newsletter))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_NewsletterSources1_RequestToaddNew(sender As Object, e As MatEventArgs) Handles Xl_NewsletterSources1.RequestToAddNew
        Dim oCod As DTONewsletterSource.Cods = e.Argument
        Select Case oCod
            Case DTONewsletterSource.Cods.News
                Dim oFrm As New Frm_Contents(, DTOContent.Srcs.News, DTO.Defaults.SelectionModes.Selection)
                AddHandler oFrm.onItemSelected, AddressOf onNoticiaSelected
                oFrm.Show()
            Case DTONewsletterSource.Cods.Blog
                Dim oFrm As New Frm_BlogPosts(, DTO.Defaults.SelectionModes.Selection)
                AddHandler oFrm.onItemSelected, AddressOf onBlogPostSelected
                oFrm.Show()
            Case DTONewsletterSource.Cods.Events
                Dim oFrm As New Frm_Contents(, DTOContent.Srcs.Eventos, DTO.Defaults.SelectionModes.Selection)
                AddHandler oFrm.onItemSelected, AddressOf onNoticiaSelected
                oFrm.Show()
            Case DTONewsletterSource.Cods.Promo
                Dim oFrm As New Frm_Incentius(Nothing, DTO.Defaults.SelectionModes.Selection)
                AddHandler oFrm.onItemSelected, AddressOf onPromoSelected
                oFrm.Show()
        End Select

    End Sub

    Private Sub refrescaSources(sender As Object, e As MatEventArgs)

        Xl_NewsletterSources1.Load(_Newsletter.Sources)
    End Sub

    Private Sub onNoticiaSelected(sender As Object, e As MatEventArgs)
        Dim oNoticia As DTONoticia = e.Argument
        Dim oSource As New DTONewsletterSource()
        With oSource
            .Tag = oNoticia
            .Cod = DTONewsletterSource.Cods.News
            .Title = oNoticia.Title
        End With
        _Newsletter.Sources.Add(oSource)
        Xl_NewsletterSources1.Load(_Newsletter.Sources)
    End Sub

    Private Sub onBlogPostSelected(sender As Object, e As MatEventArgs)
        Dim oBlogPost As DTOBlog2Post = e.Argument
        Dim oSource As New DTONewsletterSource()
        With oSource
            .Tag = oBlogPost
            .Cod = DTONewsletterSource.Cods.Blog
            .Title = oBlogPost.Title
        End With
        _Newsletter.Sources.Add(oSource)
        Xl_NewsletterSources1.Load(_Newsletter.Sources)
    End Sub

    Private Sub onPromoSelected(sender As Object, e As MatEventArgs)
        Dim oIncentiu As DTOIncentiu = e.Argument
        Dim oSource As New DTONewsletterSource()
        With oSource
            .Tag = oIncentiu
            .Cod = DTONewsletterSource.Cods.Promo
            .Title = oIncentiu.Title
        End With
        _Newsletter.Sources.Add(oSource)
        Xl_NewsletterSources1.Load(_Newsletter.Sources)
    End Sub
End Class


