Public Class Frm_YouTubeId
    Private _Incidencia As DTOIncidencia
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oIncidencia As DTOIncidencia)
        MyBase.New
        InitializeComponent()
        _Incidencia = oIncidencia
    End Sub

    Private Sub Frm_YouTubeId_Load(sender As Object, e As EventArgs) Handles Me.Load
        LabelTitol.Text = String.Format("Incidencia {0}", _Incidencia.num)
        Dim oMimeCods = [Enum].GetValues(GetType(MimeCods))
        Dim oMimeList As New List(Of MimeCods)
        oMimeList.AddRange(oMimeCods)
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim src = TextBoxLink.Text
        Dim iPos = src.LastIndexOf("/")
        Dim id = src.Substring(iPos + 1, src.Length - iPos - 1)
        If String.IsNullOrEmpty(src) OrElse Not src.Contains("/") Then
            UIHelper.WarnError("Cal enganxar l'enllaç al video de YouTube")
        Else
            Dim oDocfile As New DTODocFile(id)
            oDocfile.mime = MimeCods.Mp4
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oDocfile))
            Me.Close()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UIHelper.ShowHtml("https://studio.youtube.com/channel/UCHNJipPlakBL_lIXEDmv0dg")
    End Sub

    Private Sub ButtonCopy_Click(sender As Object, e As EventArgs) Handles ButtonCopy.Click
        UIHelper.CopyToClipboard(LabelTitol.Text)
    End Sub


End Class