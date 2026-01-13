Public Class Frm_WtbolSerp
    Private _Serp As DTOWtbolSerp

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oSerp As DTOWtbolSerp)
        MyBase.New
        InitializeComponent()
        _Serp = oSerp
    End Sub

    Private Sub Frm_WtbolSerp_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Serp
            TextBoxFch.Text = Format(.Fch, "dd/MM/yy HH:mm")
            TextBoxIp.Text = .Ip
            TextBoxProduct.Text = .Product.FullNom()
            TextBoxUserAgent.Text = .UserAgent

            If .Items.Count = 0 Then
                SplitContainer1.Panel1Collapsed = True
            Else
                Xl_WtbolSerpItems1.Load(.Items)
                Xl_WtbolSerpItems1.ClearSelection()
                Dim iRowHeight As Integer = Xl_WtbolSerpItems1.RowTemplate.Height
                SplitContainer1.SplitterDistance = iRowHeight * (.Items.Count + 1) + iRowHeight * 0.4
            End If

            Dim AppendApiKey = DTOApp.Current.Id <> DTOApp.AppTypes.matNet
            Dim url As String = MatHelperStd.GoogleMapsHelper.IpLookupUrl(.Ip, PictureBox1.Size.Width, PictureBox1.Size.Height, AppendApiKey)
            PictureBox1.Load(url)
        End With
    End Sub


End Class