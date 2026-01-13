

Public Class Frm_Youtube
    Private _YouTube As DTOYouTubeMovie
    Private _Cache As Models.ClientCache
    Private mAllowEvents As Boolean = False
    Private mCreateQRUrlChanged As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
    End Enum

    Private Enum Cols
        Tpa
        Stp
        Art
        Text
    End Enum


    Public Sub New(ByVal oYouTube As DTOYouTubeMovie)
        MyBase.New()
        Me.InitializeComponent()
        _YouTube = oYouTube
    End Sub

    Private Async Sub Frm_Youtube_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            For Each oProduct In _YouTube.Products
                oProduct.Nom = New DTOLangText(_Cache.ProductOrSelf(oProduct.Guid).FullNom(Current.Session.Lang))
            Next
            Refresca()
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Refresca()
        Dim exs As New List(Of Exception)
        If FEB.YouTubeMovie.Load(_YouTube, exs) Then

            With _YouTube
                Xl_Langs1.Value = .Lang
                Xl_LangSet1.Load(.Langset)
                TextBoxYoutubeId.Text = .YoutubeId
                Xl_LangTextShortNom.Load(.Nom)
                Xl_LangTextShortExcerpt.Load(.Dsc)
                CheckBoxObsoleto.Checked = .Obsoleto
                Xl_Products1.Load(.Products)

                If .Duration Is Nothing Then
                Else
                    Dim dtDuration As TimeSpan = CType(.Duration, TimeSpan)
                    DateTimePickerDuration.Value = New DateTime(2020, 1, 1) + dtDuration
                End If

                Dim oImageBytes = Await FEB.YouTubeMovie.Thumbnail(.Guid, exs)
                If exs.Count = 0 Then
                    If oImageBytes IsNot Nothing Then
                        Xl_ImageMime1.Load(oImageBytes, .ThumbnailMime)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If

                If Not .IsNew Then
                    ButtonDel.Enabled = True
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            Xl_Langs1.AfterUpdate,
             Xl_LangSet1.AfterUpdate,
             TextBoxYoutubeId.TextChanged,
              Xl_LangTextShortNom.AfterUpdate,
               Xl_LangTextShortExcerpt.AfterUpdate,
                Xl_Products1.AfterUpdate,
                 CheckBoxObsoleto.CheckedChanged,
                  Xl_ImageMime1.AfterUpdate,
                   DateTimePickerDuration.ValueChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oCurrentProducts As List(Of DTOProduct) = Xl_Products1.Values
        Dim oProductsToAdd = Xl_LookupProduct1.Products.Where(Function(x) Not oCurrentProducts.Any(Function(y) y.Guid.Equals(x.Guid))).ToList()
        oCurrentProducts.AddRange(oProductsToAdd)
        Xl_Products1.Load(oCurrentProducts)
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _YouTube
            .Lang = Xl_Langs1.Value
            .LangSet = Xl_LangSet1.Value
            .YoutubeId = TextBoxYoutubeId.Text
            .Nom = Xl_LangTextShortNom.LangText
            .Dsc = Xl_LangTextShortExcerpt.LangText
            .Products = Xl_Products1.Values.Select(Function(x) New DTOProduct(x.Guid)).ToList()
            .Obsoleto = CheckBoxObsoleto.Checked

            Dim dt = DateTimePickerDuration.Value
            .Duration = New TimeSpan(dt.Hour, dt.Minute, dt.Second)

            If Xl_ImageMime1.Value Is Nothing Then
                .Thumbnail = Nothing
                .ThumbnailMime = MimeCods.NotSet
            Else
                Dim ms = New IO.MemoryStream
                .Thumbnail = Xl_ImageMime1.Value.ByteArray
                .ThumbnailMime = Xl_ImageMime1.Value.Mime
            End If

        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.YouTubeMovie.Update(_YouTube, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_YouTube))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If


    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.YouTubeMovie.Delete(_YouTube, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxCreateQRUrl_TextChanged(sender As Object, e As System.EventArgs)
        If mAllowEvents Then
            mCreateQRUrlChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
        End Select
    End Sub

    Private Sub PlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayToolStripMenuItem.Click
        UIHelper.ShowHtml(DTOYouTubeMovie.Url_YouTubeSite(_YouTube))
    End Sub


End Class