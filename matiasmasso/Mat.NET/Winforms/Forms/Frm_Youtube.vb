

Public Class Frm_Youtube
    Private _YouTube As DTOYouTubeMovie
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
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        Dim exs As New List(Of Exception)
        If FEB2.YouTubeMovie.Load(_YouTube, exs) Then
            With _YouTube
                Xl_Langs1.Value = .Lang
                TextBoxYoutubeId.Text = .YoutubeId
                TextBoxNom.Text = .Nom
                TextBoxDsc.Text = .Dsc
                CheckBoxObsoleto.Checked = .Obsoleto
                Xl_Products1.Load(.Products)

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
             TextBoxYoutubeId.TextChanged,
              TextBoxNom.TextChanged,
               TextBoxDsc.TextChanged,
                Xl_Products1.AfterUpdate,
                 CheckBoxObsoleto.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oProducts As List(Of DTOProduct) = Xl_Products1.Values
        If Not oProducts.Exists(Function(x) x.Equals(Xl_LookupProduct1.Product)) Then
            oProducts.Add(Xl_LookupProduct1.Product)
            Xl_Products1.Load(oProducts)
        End If
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
            .YoutubeId = TextBoxYoutubeId.Text
            .Nom = TextBoxNom.Text
            .Dsc = TextBoxDsc.Text
            .Products = Xl_Products1.Values
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.YouTubeMovie.Update(_YouTube, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_YouTube))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If


    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.YouTubeMovie.Delete(_YouTube, exs) Then
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


End Class