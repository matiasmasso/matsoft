Public Class Frm_YouTubePlayer
    Private _Id As String
    Public Sub New(id As String)
        MyBase.New
        InitializeComponent()
        Me.Text = "Youtube " & id
        _Id = id
    End Sub
    Private Sub Frm_YouTubePlayer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sb As New Text.StringBuilder
        sb.AppendLine("<html><head>")
        sb.AppendLine("<meta http-equiv='X-UA-Compatible' content='IE=Edge'/>")
        sb.AppendLine("</head><body>")
        sb.AppendLine("<div>")
        sb.AppendLine("<iframe width='300' src='https://youtu.be/{0}'")
        sb.AppendLine("frameborder = '0' allow = 'autoplay; encrypted-media' allowfullscreen></iframe>")
        sb.AppendLine("</div>")
        sb.AppendLine("</body></html>")

        Dim src = String.Format(sb.ToString, _Id)
        WebBrowser1.DocumentText = src
    End Sub
End Class