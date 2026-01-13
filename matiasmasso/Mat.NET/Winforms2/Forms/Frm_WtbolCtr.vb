Public Class Frm_WtbolCtr
    Private _Value As DTOWtbolCtr

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Sub New(oCtr As DTOWtbolCtr)
        MyBase.New
        InitializeComponent()
        _Value = oCtr
    End Sub

    Private Sub Frm_WtbolCtr_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Value
            TextBoxFch.Text = Format(.Fch, "dd/MM/yyyy HH:mm")
            TextBoxSite.Text = .Site.Nom
            TextBoxProduct.Text = .Product.FullNom()
            TextBoxIP.Text = .Ip
            Dim AppendApiKey = DTOApp.Current.Id <> DTOApp.AppTypes.matNet
            Dim url As String = MatHelperStd.GoogleMapsHelper.IpLookupUrl(.Ip, PictureBox1.Size.Width, PictureBox1.Size.Height, AppendApiKey)
            PictureBox1.Load(url)
        End With
    End Sub

End Class